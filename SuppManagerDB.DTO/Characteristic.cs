using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuppManagerDB.DTO
{
    public class Characteristic
    {
        public int CharacteristicID { get; set; }
        public string Power { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public int Warranty { get; set; }
        public int ReleaseYear { get; set; }

        // Foreign Key
        public int ProductID { get; set; }
    }
}
