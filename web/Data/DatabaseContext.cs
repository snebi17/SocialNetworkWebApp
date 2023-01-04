using Microsoft.EntityFrameworkCore;
using web.Entities;

namespace web.Data 
{
    public class DatabaseContext : DbContext
    {
        protected readonly IConfiguration configuration;
        public DatabaseContext(IConfiguration configuration) 
        {
            this.configuration = configuration;
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
        {
            options.UseSqlServer(configuration.GetConnectionString("DatabaseContext"));
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Posts)
                        .WithOne(p => p.Author)
                        .HasForeignKey(p => p.AuthorId);
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Comments)
                        .WithOne(c => c.Author)
                        .HasForeignKey(c => c.AuthorId);
            modelBuilder.Entity<Post>()
                        .HasMany(p => p.Comments)
                        .WithOne(c => c.Post)
                        .HasForeignKey(c => c.PostId);
            // modelBuilder.Entity<User>()
            //             .ToTable("User")
            //             .Property(u => u.Id)
            //             .ValueGeneratedOnAdd();
            // modelBuilder.Entity<Post>()
            //             .ToTable("Post")
            //             .Property(p => p.Id)
            //             .ValueGeneratedOnAdd();
            // modelBuilder.Entity<Comment>()
            //             .ToTable("Comment")
            //             .Property(c => c.Id)
            //             .ValueGeneratedOnAdd();

        }
    }
}