using SuppManagerDB.DTO;
using Microsoft.Data.SqlClient;
using SuppManagerDB.DAL.Interfaces;

namespace SuppManagerDB.DAL.Concrete
{
    public class UserDal : IUserDal
    {
        public User Create(User user)
        {
            using var connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Users (Login, Email, PasswordHash, Salt)
                OUTPUT INSERTED.UserID
                VALUES (@Login, @Email, @PasswordHash, @Salt);
            ";

            command.Parameters.AddWithValue("@Login", user.Login);
            command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
            command.Parameters.AddWithValue("@Salt", user.Salt);

            user.UserID = (int)command.ExecuteScalar();
            return user;
        }

        public List<User> GetAll()
        {
            var users = new List<User>();

            using var connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT UserID, Login, Email, PasswordHash, Salt, RowInsertTime, RowUpdateTime
                FROM Users;
            ";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new User
                {
                    UserID = (int)reader["UserID"],
                    Login = (string)reader["Login"],
                    PasswordHash = (byte[])reader["PasswordHash"],
                    Salt = (Guid)reader["Salt"],
                    RowInsertTime = (DateTime)reader["RowInsertTime"],
                    RowUpdateTime = reader["RowUpdateTime"] == DBNull.Value ? null : (DateTime?)reader["RowUpdateTime"]
                });
            }
            return users;
        }

        public bool Update(User user)
        {
            using var connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE Users
                SET Login = @Login,
                    Email = @Email,
                    PasswordHash = @PasswordHash,
                    Salt = @Salt,
                    RowUpdateTime = GETDATE()
                WHERE UserID = @UserID;
            ";

            command.Parameters.AddWithValue("@Login", user.Login);
            command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
            command.Parameters.AddWithValue("@Salt", user.Salt);
            command.Parameters.AddWithValue("@UserID", user.UserID);

            return command.ExecuteNonQuery() > 0;
        }

        public bool Delete(int userID)
        {
            using var connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Users WHERE UserID = @UserID";
            command.Parameters.AddWithValue("@UserID", userID);

            return command.ExecuteNonQuery() > 0;
        }

        public User GetById(int userID)
        {
            using var connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT UserID, Login, Email, PasswordHash, Salt, RowInsertTime, RowUpdateTime
                FROM Users
                WHERE UserID = @UserID;
            ";
            command.Parameters.AddWithValue("@UserID", userID);

            using var reader = command.ExecuteReader();
            if (!reader.Read()) return null;

            return new User
            {
                UserID = (int)reader["UserID"],
                Login = (string)reader["Login"],
                PasswordHash = (byte[])reader["PasswordHash"],
                Salt = (Guid)reader["Salt"],
                RowInsertTime = (DateTime)reader["RowInsertTime"],
                RowUpdateTime = reader["RowUpdateTime"] == DBNull.Value ? null : (DateTime?)reader["RowUpdateTime"]
            };
        }

        // 👉 НОВИЙ МЕТОД ДЛЯ ЛОГІНУ
        public User? GetByLogin(string login)
        {
            using var connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT UserID, Login, Password, Salt, RowInsertTime, RowUpdateTime
                FROM Users
                WHERE Login = @Login;
            ";
            command.Parameters.AddWithValue("@Login", login);

            using var reader = command.ExecuteReader();
            if (!reader.Read()) return null;

            return new User
            {
                UserID = (int)reader["UserID"],
                Login = (string)reader["Login"],
                PasswordHash = (byte[])reader["Password"],
                Salt = (Guid)reader["Salt"],
                RowInsertTime = (DateTime)reader["RowInsertTime"],
                RowUpdateTime = reader["RowUpdateTime"] == DBNull.Value ? null : (DateTime?)reader["RowUpdateTime"]
            };
        }
    }
}
