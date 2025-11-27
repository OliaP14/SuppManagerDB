using SuppManagerDB.DTO;

namespace SuppManagerDB.BL.Interfaces
{
    public interface IAuthManager
    {
        User? Login(string login, string password);

        List<Privilege> GetUserPrivileges(int userId);

        bool HasPrivilege(int userId, string privilegeName);
    }
}
