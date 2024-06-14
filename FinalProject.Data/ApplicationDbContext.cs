using FinalProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // aeseni reebia idk vaaabshe
            modelBuilder.SeedPosts();
            modelBuilder.SeedUsers();
            modelBuilder.SeedRoles();
            modelBuilder.SeedUserRoles();

            modelBuilder.Entity<Post>()
                  .ToTable("Posts");
            modelBuilder.Entity<Post>().HasKey(p => p.Id);

            modelBuilder.Entity<Post>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Post>()
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Post>()
                .Property(p => p.Description)
                .IsRequired();

            modelBuilder.Entity<Post>()
                .Property(p => p.CreationTime)
                .IsRequired();

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Author)
                .WithMany()
                .HasForeignKey(p => p.AuthorId)
                .IsRequired();

            modelBuilder.Entity<Post>()
                .Property(p => p.State)
                .IsRequired();

            modelBuilder.Entity<Post>()
                .Property(p => p.Status)
                .IsRequired();

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId);

            modelBuilder.Entity<Comment>()
                .ToTable("Comments");

            modelBuilder.Entity<Comment>().HasKey(p => p.Id);


            modelBuilder.Entity<Comment>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Comment>()
                .Property(c => c.Text)
                .IsRequired();

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .IsRequired();

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .ToTable("AspNetUsers");

            //modelBuilder.Entity<User>().HasKey(p => p.Id);

            modelBuilder.Entity<User>()
                .Property(c => c.FirstName)
                .IsRequired();
            
            modelBuilder.Entity<User>()
                .Property(c => c.LastName)
                .IsRequired();
            
            modelBuilder.Entity<User>()
                .Property(c => c.IsBlocked)
                .IsRequired();
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
