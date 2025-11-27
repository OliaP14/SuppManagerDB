using SuppManagerDB.DTO;
using System.Collections.Generic;

namespace SuppManagerDB.BL.Interfaces
{
    public interface ISupplierManager
    {
        List<Supplier> GetAll();
        Supplier Create(Supplier supplier);
        bool Update(Supplier supplier);
        bool Delete(int supplierID);

        // 🔎 Пошук
        List<Supplier> Search(string text);

        // 🚫 Блокувати / Розблокувати
        bool SetStatus(int supplierID, bool isActive);
    }
}
