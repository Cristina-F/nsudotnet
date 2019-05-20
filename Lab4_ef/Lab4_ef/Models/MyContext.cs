using Microsoft.EntityFrameworkCore;

namespace Lab4_ef.Models {
    public class MyContext : DbContext{
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }

        public MyContext(DbContextOptions options) : base(options) {
        }
    }
}