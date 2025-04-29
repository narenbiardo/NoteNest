using EnsolversChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace EnsolversChallenge.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Note> Notes => Set<Note>();
        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Note>()
                .HasMany(n => n.Categories)
                .WithMany(c => c.Notes)
                .UsingEntity(j => j.ToTable("NoteCategories"));

            modelBuilder.Entity<Note>(entity =>
            {
                entity.Property(n => n.CreateDate)
                .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(n => n.Title)
                .HasMaxLength(50)
                .IsRequired();
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(c => c.Name)
                .HasMaxLength(25)
                .IsRequired();

                entity.Property(c => c.Description)
                .HasMaxLength(50);
            });
        }
    }
}
