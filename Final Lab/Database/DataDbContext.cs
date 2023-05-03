using Final_Lab.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Lab.Database
{
    public class DataDbContext:DbContext
    {
        // Constructure
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) { }

        // Table Employees
        public DbSet<Employees> Employees { get; set; }

        // Table Positions
        public DbSet<Positions> Positions { get; set; }
    }
}