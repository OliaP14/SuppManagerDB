using SuppManagerDB.DTO;

namespace SuppManagerDB.DAL.Interfaces
{
    public interface ISupplierDal
    {
        Supplier Create(Supplier supplier);
        
        List<Supplier> GetAll();
        Supplier GetById(int SupplierID);
        bool Update(Supplier supplier);
        bool Delete(int SupplierID);
    }
}
