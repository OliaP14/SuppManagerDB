using SuppManagerDB.DTO;
using Microsoft.Data.SqlClient;
using SuppManagerDB.DAL.Interfaces;

namespace SuppManagerDB.DAL.Concrete;
public class SupplierDal : ISupplierDal
{

    public Supplier Create(Supplier supplier)
    {
        SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuppManagerDB;Integrated Security=True;Encrypt=True");

        connection.Open();

        SqlCommand command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Suppliers (Name, Info, Location, Status) OUTPUT INSERTED.SupplierID VALUES (@Name, @Info, @Location, @Status)";

        command.Parameters.AddWithValue("@Name", supplier.Name);
        command.Parameters.AddWithValue("@Info", supplier.Info);
        command.Parameters.AddWithValue("@Location", supplier.Location);
        command.Parameters.AddWithValue("@Status", supplier.Status);


        supplier.SupplierID = (int)command.ExecuteScalar();

        connection.Close();

        return supplier;
    }

    public bool Update(Supplier supplier)
    {
        SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuppManagerDB;Integrated Security=True;Encrypt=True");
        connection.Open();
        SqlCommand command = connection.CreateCommand();
        command.CommandText = "UPDATE Suppliers SET Name = @Name, Info = @Info, Location = @Location, Status = @Status WHERE SupplierID = @SupplierID";
        command.Parameters.AddWithValue("@Name", supplier.Name);
        command.Parameters.AddWithValue("@Info", supplier.Info);
        command.Parameters.AddWithValue("@Location", supplier.Location);
        command.Parameters.AddWithValue("@Status", supplier.Status);
        command.Parameters.AddWithValue("@SupplierID", supplier.SupplierID);
        int rowsAffected = command.ExecuteNonQuery();
        connection.Close();
        return rowsAffected > 0;
    }
    public bool Delete(int SupplierID)
    {
        SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuppManagerDB;Integrated Security=True;Encrypt=True");
        connection.Open();
        SqlCommand command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Suppliers WHERE SupplierID = @SupplierID";
        command.Parameters.AddWithValue("@SupplierID", SupplierID);
        int rowsAffected = command.ExecuteNonQuery();
        connection.Close();
        return rowsAffected > 0;
    }

    public List<Supplier> GetAll()
    {
        
        SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuppManagerDB;Integrated Security=True;Encrypt=True");
        
        connection.Open();
        
        SqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT SupplierID, Name, Info, Location, Status FROM Suppliers";

        SqlDataReader reader = command.ExecuteReader();

        List<Supplier> suppliers = new List<Supplier>();

        while (reader.Read())
        {
            Supplier supplier = new Supplier
            {
                SupplierID =(int) reader["SupplierID"],
                Name = (string)reader["Name"],
                Info = (string)reader["Info"],
                Location = (string)reader["Location"],
                Status = (bool)reader["Status"]
            };
            suppliers.Add(supplier);
        }
        reader.Close();
        connection.Close();
        return suppliers;
    }

    public Supplier GetById(int SupplierID)
    {
        Supplier supplier = null;

        using var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuppManagerDB;Integrated Security=True;Encrypt=True");
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT SupplierID, Name, Info, Location, Status FROM Suppliers WHERE SupplierID = @SupplierID";
        command.Parameters.AddWithValue("@SupplierID", SupplierID);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            supplier = new Supplier
            {
                SupplierID = (int)reader["SupplierID"],
                Name = (string)reader["Name"],
                Info = (string)reader["Info"],
                Location = (string)reader["Location"],
                Status = (bool)reader["Status"]
            };
        }

        return supplier;
    }


    public bool UpdateSupplierStatus(Supplier supplier)
    {
        using var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuppManagerDB;Integrated Security=True;Encrypt=True");
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "UPDATE Suppliers SET Status = @Status WHERE SupplierID = @SupplierID";
        command.Parameters.AddWithValue("@Status", supplier.Status);
        command.Parameters.AddWithValue("@SupplierID", supplier.SupplierID);

        int rowsAffected = command.ExecuteNonQuery();
        return rowsAffected > 0;
    }

}
