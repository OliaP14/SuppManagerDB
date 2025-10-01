using Microsoft.Data.SqlClient;
using SuppManagerDB.DTO;
using SuppManagerDB.DAL.Interfaces;

namespace SuppManagerDB.DAL.Concrete
{
    public class ContractDal : IContractDal
    {
        public Contract Create(Contract contract)
        {
            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);

            connection.Open();

            SqlCommand command = connection.CreateCommand();

            command.CommandText = "INSERT INTO Contracts (SupplierID, ContractNumber, StartDate, EndDate, UserID) OUTPUT INSERTED.ContractID VALUES (@SupplierID, @ContractNumber, @StartDate, @EndDate, @UserID)";

            command.Parameters.AddWithValue("@SupplierID", contract.SupplierID);
            command.Parameters.AddWithValue("@ContractNumber", contract.ContractNumber);
            command.Parameters.AddWithValue("@StartDate", contract.StartDate);
            command.Parameters.AddWithValue("@EndDate", contract.EndDate);
            command.Parameters.AddWithValue("@UserID", contract.UserID);

            contract.ContractID = (int)command.ExecuteScalar();
            connection.Close();
            return contract;

        }
        public List<Contract> GetAll()
        {
            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT ContractID, SupplierID, ContractNumber, StartDate, EndDate, UserID FROM Contracts";
            SqlDataReader reader = command.ExecuteReader();
            List<Contract> contracts = new List<Contract>();
            while (reader.Read())
            {
                Contract contract = new Contract
                {
                    ContractID = (int)reader["ContractID"],
                    SupplierID = (int)reader["SupplierID"],
                    ContractNumber = (string)reader["ContractNumber"],
                    StartDate = (DateTime)reader["StartDate"],
                    EndDate = (DateTime)reader["EndDate"],
                    UserID = (int)reader["UserID"]
                };
                contracts.Add(contract);
            }
            reader.Close();
            connection.Close();
            return contracts;
        }

        public bool Update(Contract contract)
        {
            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();  
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Contracts SET SupplierID = @SupplierID, ContractNumber = @ContractNumber,StartDate = @StartDate, EndDate = @EndDate, UserID = @UserID WHERE ContractID = @ContractID";

            command.Parameters.AddWithValue("@SupplierID", contract.SupplierID);
            command.Parameters.AddWithValue("@ContractNumber", contract.ContractNumber);
            command.Parameters.AddWithValue("@StartDate", contract.StartDate);
            command.Parameters.AddWithValue("@EndDate", contract.EndDate);
            command.Parameters.AddWithValue("@UserID", contract.UserID);
            command.Parameters.AddWithValue("@ContractID", contract.ContractID);

            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected > 0;
        }

        public bool Delete(int ContractID)
        {
            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Contracts WHERE ContractID = @ContractID";
            command.Parameters.AddWithValue("@ContractID", ContractID);
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected > 0;
        }

        public Contract GetById(int id)
        {
            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT ContractID, SupplierID, ContractNumber, StartDate, EndDate, UserID FROM Contracts WHERE ContractID = @ContractID";
            command.Parameters.AddWithValue("@ContractID", id);
            SqlDataReader reader = command.ExecuteReader();
            Contract? contract = null;
            if (reader.Read())
            {
                contract = new Contract
                {
                    ContractID = (int)reader["ContractID"],
                    SupplierID = (int)reader["SupplierID"],
                    ContractNumber = (string)reader["ContractNumber"],
                    StartDate = (DateTime)reader["StartDate"],
                    EndDate = (DateTime)reader["EndDate"],
                    UserID = (int)reader["UserID"]
                };
            }
            reader.Close();
            connection.Close();
            return contract ;
        }

        public List<Contract> GetBySupplier(int supplierId)
        {
            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT ContractID, SupplierID, ContractNumber, StartDate, EndDate, UserID FROM Contracts WHERE SupplierID = @SupplierID";
            command.Parameters.AddWithValue("@SupplierID", supplierId);
            SqlDataReader reader = command.ExecuteReader();
            List<Contract> contracts = new List<Contract>();
            while (reader.Read())
            {
                Contract contract = new Contract
                {
                    ContractID = (int)reader["ContractID"],
                    SupplierID = (int)reader["SupplierID"],
                    ContractNumber = (string)reader["ContractNumber"],
                    StartDate = (DateTime)reader["StartDate"],
                    EndDate = (DateTime)reader["EndDate"],
                    UserID = (int)reader["UserID"]
                };
                contracts.Add(contract);
            }
            reader.Close();
            connection.Close();
            return contracts;
        }

        public List<Contract> GetByUser(int userId)
        {
            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT ContractID, SupplierID, ContractNumber, StartDate, EndDate, UserID FROM Contracts WHERE UserID = @UserID";
            command.Parameters.AddWithValue("@UserID", userId);
            SqlDataReader reader = command.ExecuteReader();
            List<Contract> contracts = new List<Contract>();
            while (reader.Read())
            {
                Contract contract = new Contract
                {
                    ContractID = (int)reader["ContractID"],
                    SupplierID = (int)reader["SupplierID"],
                    ContractNumber = (string)reader["ContractNumber"],
                    StartDate = (DateTime)reader["StartDate"],
                    EndDate = (DateTime)reader["EndDate"],
                    UserID = (int)reader["UserID"]
                };
                contracts.Add(contract);
            }
            reader.Close();
            connection.Close();
            return contracts;
        }
    }
}
