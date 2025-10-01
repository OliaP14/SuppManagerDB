using Microsoft.Data.SqlClient;

namespace SuppManagerDB.DAL.Tests
{
    public abstract class DalTestBase : IDisposable
    {
        protected readonly SqlConnection Connection;
        protected readonly SqlTransaction Transaction;

        protected DalTestBase()
        {
            // Використовуємо тестову БД з Constants
            Connection = new SqlConnection(Constants.TEST_DATABASE_CONNECTION);
            Connection.Open();

            // Починаємо транзакцію
            Transaction = Connection.BeginTransaction();
        }

        protected SqlCommand CreateCommand(string sql)
        {
            var cmd = Connection.CreateCommand();
            cmd.Transaction = Transaction;
            cmd.CommandText = sql;
            return cmd;
        }

        public void Dispose()
        {
            // Відкат змін після кожного тесту
            Transaction.Rollback();
            Connection.Close();
        }
    }
}
