namespace SuppManagerDB.DTO
{
    public class Supplier
    {
        public int SupplierID { get; set; }   // Primary Key
        public string Name { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public bool Status { get; set; } 
    }
}
