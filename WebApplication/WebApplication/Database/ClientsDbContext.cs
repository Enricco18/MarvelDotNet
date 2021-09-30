using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Database
{
    public class ClientsDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public ClientsDbContext(DbContextOptions<ClientsDbContext> options) : base(options)
        {
        }

    }
}
