using SuppManagerDB.DTO;
using System.Collections.Generic;

namespace SuppManagerDB.BL.Interfaces
{
    public interface IProductManager
    {
        Product Create(Product product);


        // Товари конкретного постачальника
        List<Product> GetBySupplier(int supplierId);

        // Для заповнення ComboBox'ів
        List<Category> GetAllCategories();
        List<Manufacturer> GetAllManufacturers();

        bool Delete(int id);

        // Оновлення інформації про товар
        bool Update(Product product);

    }
}
