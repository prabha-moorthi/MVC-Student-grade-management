using Microsoft.EntityFrameworkCore;
using StudentGradeManagement.Models;

namespace StudentGradeManagement.Data   
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}