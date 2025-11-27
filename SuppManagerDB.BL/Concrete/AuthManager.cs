using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using SuppManagerDB.DAL.Interfaces;
using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.DTO;

namespace SuppManagerDB.BL.Concrete
{
    public class AuthManager : IAuthManager
    {
        private readonly IUserDal _userDal;
        private readonly IUserPrivilegeDal _privDal;

        public AuthManager(IUserDal userDal, IUserPrivilegeDal privDal)
        {
            _userDal = userDal;
            _privDal = privDal;
        }

        public User? Login(string login, string password)
        {
            var user = _userDal.GetByLogin(login);
            if (user == null) return null;

            var computedHash = this.ComputeHash(password, user.Salt);

            if (!CompareHashes(computedHash, user.PasswordHash))
                return null;

            // Додаю привілею роль користувача
            var privileges = _privDal.GetPrivilegesForUser(user.UserID);
            user.Privileges = privileges; 

            return user;
        }


        public List<Privilege> GetUserPrivileges(int userId)
        {
            return _privDal.GetPrivilegesForUser(userId);
        }

        public bool HasPrivilege(int userId, string privilegeName)
        {
            var privileges = GetUserPrivileges(userId);
            return privileges.Exists(p => p.Name.Equals(privilegeName, StringComparison.OrdinalIgnoreCase));
        }

        private byte[] ComputeHash(string password, Guid salt)
        {
            using var sha = SHA256.Create();
            string combined = password + salt.ToString().ToUpper();
            byte[] bytes = Encoding.Unicode.GetBytes(combined);
            var hash = sha.ComputeHash(bytes);

            // print HEX
            var consoleHash = BitConverter.ToString(hash).Replace("-", "");

            return hash;
        }

        private bool CompareHashes(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
                if (a[i] != b[i]) return false;
            return true;
        }
    }
}
