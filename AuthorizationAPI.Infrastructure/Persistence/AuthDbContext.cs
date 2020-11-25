using AuthorizationAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationAPI.Infrastructure.Persistence
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            :base(options) {  }
            
        public DbSet<User> Users { get; set; }
        public DbSet<AuthenticationType> AuthenticationTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthenticationType>().HasData(
                new {Id = 1, AuthenticationTypeName = "AuthenticationAPI"},
                new {Id = 2, AuthenticationTypeName = "GoogleAPI"},
                new {Id = 3, AuthenticationTypeName = "AuthenticationAPI"}
            );
        }
    }
}