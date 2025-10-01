using SuppManagerDB.DTO;
using Microsoft.Data.SqlClient;
using SuppManagerDB.DAL.Interfaces;

namespace SuppManagerDB.DAL.Concrete
{
    public class UserDal : IUserDal
    {
        public User Create(User user)
        {
            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Users (UserName, PasswordHash, Role) OUTPUT INSERTED.UserID VALUES (@UserName, @PasswordHash, @Role)";

            command.Parameters.AddWithValue("@UserName", user.UserName);
            command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
            command.Parameters.AddWithValue("@Role", user.Role);

            user.UserID = (int)command.ExecuteScalar();

            connection.Close();

            return user;
        }

        public List<User> GetAll()
        {

            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT UserID, UserName, PasswordHash, Role FROM Users";

            SqlDataReader reader = command.ExecuteReader();

            List<User> users = new List<User>();

            while (reader.Read())
            {
                User user = new User
                {
                    UserID = (int)reader["UserID"],
                    UserName = (string)reader["UserName"],
                    PasswordHash = (string)reader["PasswordHash"],
                    Role = (string)reader["Role"]
                };
                users.Add(user);
            }
            reader.Close();
            connection.Close();
            return users;
        }

        public bool Update(User user)
        {
            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Users SET UserName  = @UserName, PasswordHash = @PasswordHash, Role = @Role WHERE UserID = @UserID";

            command.Parameters.AddWithValue("@UserName", user.UserName);
            command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
            command.Parameters.AddWithValue("@Role", user.Role);
            command.Parameters.AddWithValue("@UserID", user.UserID);
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected > 0;
        }

        public bool Delete(int UserID)
        {
            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Users WHERE UserID = @UserID";
            command.Parameters.AddWithValue("@UserID", UserID);
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected > 0;
        }

        public User GetById(int UserID)
        {
            User user = null;

            using var connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT UserID, UserName, PasswordHash, Role FROM Users WHERE UserID = @UserID";
            command.Parameters.AddWithValue("@UserID", UserID);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                user = new User
                {
                    UserID = (int)reader["UserID"],
                    UserName = (string)reader["UserName"],
                    PasswordHash = (string)reader["PasswordHash"],
                    Role = (string)reader["Role"]
                };
            }

            return user;
        }
    }  
}
