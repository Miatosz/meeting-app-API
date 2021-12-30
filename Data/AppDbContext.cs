using MeetingAppAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingAppAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {  }

        public DbSet<Category> Categories {get; set;}
        public DbSet<Event> Events {get; set;}
        public DbSet<User> Users {get; set;}
        public DbSet<Comment> Comments {get; set;}
    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>()
                .HasMany<Event>(e => e.ListOfParticipatingEvents)
                .WithMany(d => d.Users);
                

                
            modelBuilder.Entity<Event>()
                        .HasMany<User>(e => e.Users)
                        .WithMany(d => d.ListOfParticipatingEvents);

                
            modelBuilder.Entity<Event>()
                        .HasOne<User>(e => e.CreatedBy);

    
    
        }  
    
    
    }
}
