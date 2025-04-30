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
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Note>()
                .HasMany(n => n.Categories)
                .WithMany(c => c.Notes)
                .UsingEntity<Dictionary<string, object>>(
                    "NoteCategories",
                    j => j
                        .HasOne<Category>()
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade),
                   j => j
                        .HasOne<Note>()
                        .WithMany()
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Restrict)
        );

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

            modelBuilder.Entity<User>()
                .HasMany(u => u.Notes)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Categories)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(c => c.Username)
                .HasMaxLength(50)
                .IsRequired();

                entity.Property(c => c.PasswordHash)
                .HasMaxLength(256)
                .IsRequired();
            });
        }
    }
}
