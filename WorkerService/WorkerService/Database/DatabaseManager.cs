using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

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

        public DataTable findAll()
        {
            conn.Open();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM clients ", this.conn))
            {
                // create the DataSet 
                DataTable data = new DataTable();
                // fill the DataSet using our DataAdapter 
                dataAdapter.Fill(data);
                conn.Close();
                return data;
            }
        }

        public void Dispose()
        {
            ((IDisposable)conn).Dispose();
        }
    }
}