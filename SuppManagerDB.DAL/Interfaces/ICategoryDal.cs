using SuppManagerDB.DTO;
using System.Collections.Generic;

namespace SuppManagerDB.DAL.Interfaces
{
    public interface ICategoryDal
    {
        Category Create(Category category);
        Category GetById(int id);
        List<Category> GetAll();
        bool Update(Category category);
        bool Delete(int id);
    }
}
