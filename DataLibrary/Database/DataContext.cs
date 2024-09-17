using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Projects> Projects { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<SubTasks> SubTasks { get; set; }
        public DbSet<SubDetails> SubDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Projects>()
               .HasMany(p => p.Task)
               .WithOne(t => t.Project)
               .HasForeignKey(t => t.Project_Id)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tasks>()
                .HasMany(t => t.SubTask)
                .WithOne(s => s.Task)
                .HasForeignKey(s => s.Task_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubTasks>()
                .HasMany(s => s.SubDetail)
                .WithOne(d => d.SubTask)
                .HasForeignKey(d => d.SubT_Id)
                .OnDelete(DeleteBehavior.Cascade);



            base.OnModelCreating(modelBuilder);

        }
    }
}
