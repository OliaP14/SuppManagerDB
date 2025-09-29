using SuppManagerDB.DTO;
using System.Collections.Generic;

namespace SuppManagerDB.DAL.Interfaces
{
    public interface IProductDal
    {
        Product Create(Product product);
        Product GetById(int id);
        List<Product> GetAll();
        bool Update(Product product);
        bool Delete(int id);

        // Додатково: отримаємо товари по постачальнику
        List<Product> GetBySupplier(int supplierId);
        // Додатково: отримаємо товари по категорії
        List<Product> GetByCategory(int categoryId);
    }
}
