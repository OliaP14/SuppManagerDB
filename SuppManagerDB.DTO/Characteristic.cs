namespace SuppManagerDB.DTO
{
    public class Characteristic
    {
        public int CharacteristicID { get; set; }
        public float Power { get; set; }
        public string Material { get; set; } = string.Empty;
        public string Warranty { get; set; }
        public int ReleaseYear { get; set; }

        // Foreign Key
        public int ProductID { get; set; }
    }
}
