using System.Linq;
using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.DAL.Interfaces;
using SuppManagerDB.DTO;

namespace SuppManagerDB.BL.Concrete
{
    public class SupplierManager : ISupplierManager
    {
        private readonly ISupplierDal _dal;

        public SupplierManager(ISupplierDal dal)
        {
            _dal = dal;
        }

        public List<Supplier> GetAll()
        {
            return _dal.GetAll();
        }

        public Supplier Create(Supplier supplier)
        {
            // Валідація
            if (string.IsNullOrWhiteSpace(supplier.Name))
                throw new Exception("Назва постачальника не може бути пустою.");

            if (string.IsNullOrWhiteSpace(supplier.Info))
                throw new Exception("Інформація повинна містити телефон або email.");

            supplier.Status = true; // active by default
            return _dal.Create(supplier);
        }

        public bool Update(Supplier supplier)
        {
            if (supplier.SupplierID <= 0)
                throw new Exception("Некоректний ID постачальника.");

            return _dal.Update(supplier);
        }

        public bool Delete(int supplierID)
        {
            return _dal.Delete(supplierID);
        }

        public List<Supplier> Search(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return _dal.GetAll();

            text = text.ToLower();

            return _dal.GetAll()
                       .Where(s => s.Name.ToLower().Contains(text)
                                || s.Info.ToLower().Contains(text)
                                || s.Location.ToLower().Contains(text))
                       .ToList();
        }

        public bool SetStatus(int supplierID, bool isActive)
        {
            var supplier = _dal.GetById(supplierID);
            if (supplier == null) return false;

            supplier.Status = isActive;
            return _dal.UpdateSupplierStatus(supplier);
        }
    }
}
