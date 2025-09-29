using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuppManagerDB.DTO
{
    public class Contract
    {
        public int ContractID { get; set; }
        public string ContractNumber { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Foreign Key
        public int UserID { get; set; } 
        public int SupplierID { get; set; }
    }
}
