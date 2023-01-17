using LaMiaPizzeriaEFRelazione1n.Models;
using Microsoft.EntityFrameworkCore;

namespace LaMiaPizzeriaEFRelazione1n.DataBase
{
    public class PizzeriaContext : DbContext
    {
        public DbSet<Pizza> Pizza { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Database=PizzeriaRelazione1n;" +
            "Integrated Security=True;TrustServerCertificate=True");
        }
    }
}
