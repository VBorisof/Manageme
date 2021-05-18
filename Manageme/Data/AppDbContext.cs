using Manageme.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Manageme.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Category> Categories { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(e => 
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("now() at time zone 'utc'");
                e.Property(x => x.ModifiedAt)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("now() at time zone 'utc'");

                e.HasMany(x => x.Reminders);
                e.HasMany(x => x.Categories);
            });

            builder.Entity<Reminder>(e => 
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("now() at time zone 'utc'");
                e.Property(x => x.ModifiedAt)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("now() at time zone 'utc'");

                e.HasOne(x => x.User)
                    .WithMany(x => x.Reminders)
                    .HasForeignKey(x => x.UserId);

                e.HasOne(x => x.Category)
                    .WithMany()
                    .HasForeignKey(x => x.CategoryId);
            });
            
            builder.Entity<Category>(e => 
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("now() at time zone 'utc'");
                e.Property(x => x.ModifiedAt)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("now() at time zone 'utc'");

                e.HasOne(x => x.User)
                    .WithMany(x => x.Categories)
                    .HasForeignKey(x => x.UserId);
            });
        }
    }
}

