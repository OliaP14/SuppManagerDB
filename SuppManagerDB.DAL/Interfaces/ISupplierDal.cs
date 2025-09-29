using SuppManagerDB.DTO;
using System.Collections.Generic;

namespace SuppManagerDB.DAL.Interfaces
{
    public interface ISupplierDal
    {
        Supplier Create(Supplier supplier);
        
        List<Supplier> GetAll();
        bool Update(Supplier supplier);
        bool Delete(int id);
    }
}
