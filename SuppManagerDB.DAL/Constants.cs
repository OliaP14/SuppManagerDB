namespace SuppManagerDB.DAL
{
    public static class Constants
    {
        // Connection strings
        public const string DATABASE_CONNECTION = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuppManagerDB;Integrated Security=True;TrustServerCertificate=True";
        public const string TEST_DATABASE_CONNECTION = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuppManagerTestDB;Integrated Security=True;TrustServerCertificate=True";

        // Choose connection based on environment variable
        public static readonly string DB_CONNECTION =
            Environment.GetEnvironmentVariable("IS_TEST_RUNNING") == "true"
            ? TEST_DATABASE_CONNECTION
            : DATABASE_CONNECTION;
    }
}
