using SuppManagerDB.DTO;
using Microsoft.Data.SqlClient;
using SuppManagerDB.DAL.Interfaces;

namespace SuppManagerDB.DAL.Concrete
{
    public class CharacteristicDal : ICharacteristicDal
    {
        public Characteristic Create(Characteristic characteristic)
        {
            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);
            
            connection.Open();
            
            SqlCommand command = connection.CreateCommand();
            
            command.CommandText = "INSERT INTO Characteristics (Power, Material, Warranty, ReleaseYear, ProductID) OUTPUT INSERTED.CharacteristicID VALUES (@Power, @Material, @Warranty, @ReleaseYear, @ProductID)";

            command.Parameters.AddWithValue("@Power", characteristic.Power);
            command.Parameters.AddWithValue("@Material", characteristic.Material);
            command.Parameters.AddWithValue("@Warranty", characteristic.Warranty);
            command.Parameters.AddWithValue("@ReleaseYear", characteristic.ReleaseYear);
            command.Parameters.AddWithValue("@ProductID", characteristic.ProductID);

            characteristic.CharacteristicID = (int)command.ExecuteScalar();
            
            connection.Close();

            return characteristic;
        }
        public List<Characteristic> GetAll()
        {
            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);
            
            connection.Open();
            
            SqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT CharacteristicID, Power, Material, Warranty, ReleaseYear, ProductID FROM Characteristics";

            SqlDataReader reader = command.ExecuteReader();
            List<Characteristic> characteristics = new List<Characteristic>();
            while (reader.Read())
            {
                Characteristic characteristic = new Characteristic
                {
                    CharacteristicID = (int)reader["CharacteristicID"],
                    Power = (float)reader["Power"],
                    Material = (string)reader["Material"],
                    Warranty = (string)reader["Warranty"],
                    ReleaseYear = (int)reader["ReleaseYear"],
                    ProductID = (int)reader["ProductID"]
                };
                characteristics.Add(characteristic);
            }
            reader.Close();
            connection.Close();
            return characteristics;
        }
        public bool Update(Characteristic characteristic)
        {
            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = " UPDATE Characteristics SET Power = @Power, Material = @Material, Warranty = @Warranty, ReleaseYear = @ReleaseYear, ProductID = @ProductID WHERE CharacteristicID = @CharacteristicID";

            command.Parameters.AddWithValue("@Power", characteristic.Power);
            command.Parameters.AddWithValue("@Material", characteristic.Material);
            command.Parameters.AddWithValue("@Warranty", characteristic.Warranty);
            command.Parameters.AddWithValue("@ReleaseYear", characteristic.ReleaseYear);
            command.Parameters.AddWithValue("@ProductID", characteristic.ProductID);
            command.Parameters.AddWithValue("@CharacteristicID", characteristic.CharacteristicID);

            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected > 0;
        }
        public bool Delete(int characteristicID)
        {
            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Characteristics WHERE CharacteristicID = @CharacteristicID";
            command.Parameters.AddWithValue("@CharacteristicID", characteristicID);
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected > 0;
        }

        public Characteristic GetById(int CharacteristicID)
        {
            Characteristic characteristic = null;

            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT CharacteristicID, Power, Material, Warranty, ReleaseYear, ProductID FROM Characteristics WHERE CharacteristicID = @CharacteristicID";
            command.Parameters.AddWithValue("@CharacteristicID", CharacteristicID);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                characteristic = new Characteristic
                {
                    CharacteristicID = (int)reader["CharacteristicID"],
                    Power = (float)reader["Power"],
                    Material = (string)reader["Material"],
                    Warranty = (string)reader["Warranty"],
                    ReleaseYear = (int)reader["ReleaseYear"],
                    ProductID = (int)reader["ProductID"]
                };
            }

            return characteristic;
        }

        public List<Characteristic> GetByProductId(int productId)
        {
            var list = new List<Characteristic>();

            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT CharacteristicID, Power, Material, Warranty, ReleaseYear, ProductID FROM Characteristics WHERE ProductID = @ProductID";
            command.Parameters.AddWithValue("@ProductID", productId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Characteristic
                {
                    CharacteristicID = Convert.ToInt32(reader["CharacteristicID"]),
                    Power = reader["Power"] != DBNull.Value ? Convert.ToSingle(reader["Power"]) : 0f,
                    Material = reader["Material"] != DBNull.Value ? reader["Material"].ToString() : string.Empty,
                    Warranty = reader["Warranty"] != DBNull.Value ? reader["Warranty"].ToString() : string.Empty,
                    ReleaseYear = reader["ReleaseYear"] != DBNull.Value ? Convert.ToInt32(reader["ReleaseYear"]) : 0,
                    ProductID = Convert.ToInt32(reader["ProductID"]),

                });
            }

            connection.Close();
            return list;
        }

        public List<Characteristic> GetByProduct(int productId)
        {
            return GetByProductId(productId);
        }

    }
}
