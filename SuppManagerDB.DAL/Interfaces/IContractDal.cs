using SuppManagerDB.DTO;
using System.Collections.Generic;

namespace SuppManagerDB.DAL.Interfaces
{
    public interface IContractDal
    {
        Contract Create(Contract contract);
        Contract GetById(int id);
        List<Contract> GetAll();
        bool Update(Contract contract);
        bool Delete(int id);

        // Додатково: отримати контракти по постачальнику
        List<Contract> GetBySupplier(int supplierId);
        // Додатково: отримати контракти по користувачу
        List<Contract> GetByUser(int userId);
    }
}
