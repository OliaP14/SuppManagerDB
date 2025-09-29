using SuppManagerDB.DTO;
using System.Collections.Generic;

namespace SuppManagerDB.DAL.Interfaces
{
    public interface IUserDal
    {
        User Create(User user);
        User GetById(int id);
        List<User> GetAll();
        bool Update(User user);
        bool Delete(int id);
    }
}
