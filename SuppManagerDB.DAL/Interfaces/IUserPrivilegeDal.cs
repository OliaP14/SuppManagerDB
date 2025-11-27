using System.Collections.Generic;
using SuppManagerDB.DTO;

namespace SuppManagerDB.DAL.Interfaces
{
    public interface IUserPrivilegeDal
    {
        // Повертаємо список привілеїв для користувача
        List<Privilege> GetPrivilegesForUser(int userId);
    }
}
