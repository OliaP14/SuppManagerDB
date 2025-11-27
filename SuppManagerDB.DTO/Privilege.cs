namespace SuppManagerDB.DTO
{
    public class Privilege
    {
        public int PrivilegeID { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime RowInsertTime { get; set; }
    }
}
