using SuppManagerDB.DTO;
using System.Collections.Generic;

namespace SuppManagerDB.DAL.Interfaces
{
    public interface IManufacturerDal
    {
        Manufacturer Create(Manufacturer manufacturer);
        Manufacturer GetById(int id);
        List<Manufacturer> GetAll();
        bool Update(Manufacturer manufacturer);
        bool Delete(int id);

        // Додатково: всі виробники конкретного продукту
        List<Manufacturer> GetByProduct(int productId);
    }
}
