namespace SuppManagerDB.DTO
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public decimal Price { get; set; }

        // Foreign Keys
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
    }
}
