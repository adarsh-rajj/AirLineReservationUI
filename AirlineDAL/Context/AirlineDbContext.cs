using AirlineDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirlineDAL.Context
{
    public class AirlineDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<AirlineReservation> AirlineReservation { get; set; }

        public string ConnectionString { get; }
        public AirlineDbContext()
        {
            ConnectionString = "Server=MSI\\SQLEXPRESS; Database=AirLineReservation; Trusted_Connection=True; TrustServerCertificate=True; Integrated Security=true;";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    UserName = "flight001",
                    Password = "indigo",
                });

            modelBuilder.Entity<AirlineReservation>(options =>
            {
                options.Property(x => x.PaymentMode).HasConversion<string>();
                options.Property(x => x.Status).HasConversion<string>();
                options.Property(x => x.Gender).HasConversion<string>();
            });
        }
    }
}
