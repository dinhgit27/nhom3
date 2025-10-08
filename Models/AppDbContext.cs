using Microsoft.EntityFrameworkCore;

namespace StudentManagementApi.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()// Cấu hình cho entity Student
                .HasOne(s => s.Class)// Student có 1 Class
                .WithMany(c => c.Students)// Class có nhiều Student
                .HasForeignKey(s => s.ClassId)// ClassId là khóa ngoại
                .OnDelete(DeleteBehavior.Cascade);  // Xóa sinh viên khi xóa lớp
        }
    }
}