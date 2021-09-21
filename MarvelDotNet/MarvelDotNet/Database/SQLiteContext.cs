using MarvelDotNet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelDotNet.Database
{
    public class SQLiteContext: DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public readonly String DbName = "clients"; 
        public String DbPath { get; set; }

        public SQLiteContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}{DbName}.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source ={DbPath}");
        }
    }
}
