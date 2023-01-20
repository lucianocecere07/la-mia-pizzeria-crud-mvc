using Azure;
using LaMiaPizzeriaEFRelazione1n.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LaMiaPizzeriaEFRelazione1n.DataBase
{
    public class PizzeriaContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Pizza> Pizza { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Ingrediente> Ingredienti { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Database=PizzeriaRelazione1n;" +
            "Integrated Security=True;TrustServerCertificate=True");
        }
    }
}
