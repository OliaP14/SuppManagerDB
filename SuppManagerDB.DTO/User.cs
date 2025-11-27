namespace SuppManagerDB.DTO
{
    public class User
    {
        public int UserID { get; set; }
        public string Login { get; set; } = string.Empty;

       // public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();

        public Guid Salt { get; set; }
        public DateTime RowInsertTime { get; set; }
        public DateTime? RowUpdateTime { get; set; }

        public List<Privilege>? Privileges { get; set; }

    }
}
