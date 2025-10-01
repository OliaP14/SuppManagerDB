namespace SuppManagerDB.DTO
{
    public class Manufacturer
    {
        public int ManufacturerID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;

        // Foreign Key
        public int ProductID { get; set; }
    }
}
