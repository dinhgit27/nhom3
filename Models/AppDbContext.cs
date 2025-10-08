using Microsoft.EntityFrameworkCore;

namespace StudentManagementApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }

        // cấu hình tuỳ chọn bằng Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Class.Name unique (tuỳ ý)
            modelBuilder.Entity<Class>()
                .HasIndex(c => c.Name)
                .IsUnique(false);

            // Relationship: Class (1) - Students (many)
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}