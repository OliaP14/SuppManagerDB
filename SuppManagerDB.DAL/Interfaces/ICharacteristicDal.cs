using SuppManagerDB.DTO;
using System.Collections.Generic;

namespace SuppManagerDB.DAL.Interfaces
{
    public interface ICharacteristicDal
    {
        Characteristic Create(Characteristic characteristic);
        Characteristic GetById(int id);
        List<Characteristic> GetAll();
        bool Update(Characteristic characteristic);
        bool Delete(int id);

        // Додатково: всі характеристики конкретного продукту
        List<Characteristic> GetByProduct(int productId);
    }
}
