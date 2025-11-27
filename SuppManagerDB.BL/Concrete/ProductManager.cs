using System.Collections.Generic;
using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.DAL.Interfaces;
using SuppManagerDB.DTO;

namespace SuppManagerDB.BL.Concrete
{
    public class ProductManager : IProductManager
    {
        private readonly IProductDal _productDal;
        private readonly ICategoryDal _categoryDal;
        private readonly IManufacturerDal _manufacturerDal;

        public ProductManager(
            IProductDal productDal,
            ICategoryDal categoryDal,
            IManufacturerDal manufacturerDal)
        {
            _productDal = productDal;
            _categoryDal = categoryDal;
            _manufacturerDal = manufacturerDal;
        }

        public Product Create(Product product)
        {
            if (product == null)
                throw new Exception("Товар не може бути порожнім");

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new Exception("Назва товару не може бути порожньою");

            if (product.Price <= 0)
                throw new Exception("Ціна має бути більшою за нуль");

            if (product.SupplierID <= 0)
                throw new Exception("Не обрано постачальника");

            return _productDal.Create(product);
        }

        public List<Product> GetBySupplier(int supplierId)
        {
            return _productDal.GetBySupplier(supplierId);
        }

        public List<Category> GetAllCategories()
        {
            return _categoryDal.GetAll();
        }

        public List<Manufacturer> GetAllManufacturers()
        {
            return _manufacturerDal.GetAll();
        }

        public bool Delete(int id)
        {
            return _productDal.Delete(id);
        }
        public bool Update(Product product)
        {
            if (product.ProductID <= 0)
                throw new Exception("Некоректний товар для оновлення");
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new Exception("Назва товару не може бути порожньою");
            if (product.Price <= 0)
                throw new Exception("Ціна має бути більшою за 0");
            if (product.SupplierID <= 0)
                throw new Exception("Товар повинен мати постачальника");

            return _productDal.Update(product);
        }


    }
}
