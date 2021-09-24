using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using MarvelDotNet.Models;

namespace MarvelDotNet.Database
{
    class DatabaseManager : IDisposable
    {

        public readonly String DbPath = Environment.GetEnvironmentVariable("DB_PATH") ?? "(local)";
        public readonly String DbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "test";
        public readonly String DbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "SA";
        public readonly String DbPass = Environment.GetEnvironmentVariable("DB_PASS") ?? "Admin_123";

        public SqlConnection conn;

        public DatabaseManager(string dbPath = null, string dbName = null, string dbUser = null, string dbPass = null)
        {
            DbPath = dbPath ?? DbPath;
            DbName = dbName ?? DbName;
            DbUser = dbUser ?? DbUser;
            DbPass = dbPass ?? DbPass;
            this.conn = new SqlConnection(this.getDBConnection());
        }

        private String getDBConnection()
        {
            String DbConnection = $"Server={DbPath};Database={DbName};user id = {DbUser}; password = {DbPass};Integrated Security=False";
            return DbConnection;
        }

        public void findAll()
        {
            conn.Open();
            String queryString = "SELECT * FROM clients";
            SqlCommand command = new SqlCommand(queryString, conn);
            //command.Parameters.AddWithValue("@pricePoint", paramValue);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                for(int i = 0; i< reader.FieldCount;i++)
                {
                    Console.Write($"{reader.GetName(i)} - ");
                    Console.Write($"{reader[i]} |");
                }
                Console.Write("\n");
            }
            reader.Close();
            conn.Close();
        }
        public void findByCPF(String cpf)
        {
            conn.Open();
            String queryString = "SELECT * FROM clients WHERE CPF=@cpf";
            SqlCommand command = new SqlCommand(queryString, conn);
            command.Parameters.AddWithValue("@cpf", cpf);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write($"{reader.GetName(i)} - ");
                    Console.Write($"{reader[i]} |");
                }
                Console.Write("\n");
            }
            reader.Close();
            conn.Close();
        }

        internal void updateClient(string cpf, string param, string v)
        {
            conn.Open();
            String queryString = $"UPDATE clients SET {param} = @value WHERE CPF = @cpf ";
            SqlCommand command = new SqlCommand(queryString, conn);
            command.Parameters.AddWithValue("@cpf", cpf);
            command.Parameters.AddWithValue("@value", v);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public void createClient(Client client) {
            conn.Open();
            String queryString = "INSERT INTO clients (Name, Age, CPF, Email, Phone, Address) VALUES (@name, @age, @cpf, @email, @phone, @address)";
            SqlCommand command = new SqlCommand(queryString, conn);
            command.Parameters.AddWithValue("@name", client.Name);
            command.Parameters.AddWithValue("@age", client.Age);
            command.Parameters.AddWithValue("@cpf", client.CPF);
            command.Parameters.AddWithValue("@email", client.Email);
            command.Parameters.AddWithValue("@address", client.Address);
            command.Parameters.AddWithValue("@phone", client.Phone);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public void deleteClient(String cpf)
        {
            conn.Open();
            String queryString = "DELETE FROM clients WHERE CPF = @cpf ";
            SqlCommand command = new SqlCommand(queryString, conn);
            command.Parameters.AddWithValue("@cpf", cpf);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public void Dispose()
        {
            ((IDisposable)conn).Dispose();
        }
    }
}
