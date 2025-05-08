using CrudNest.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrudNest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

      

        public DbSet<Employee> Employee{ get; set; }
    }
}
