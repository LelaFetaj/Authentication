using Authentication.Models.Entities.Roles;
using Authentication.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Data.Context
{
    public class AuthenticationDbContext : IdentityDbContext<User, Role, Guid>
    {
        private readonly IConfiguration configuration;

        public AuthenticationDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ConfigureIdentityTables(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = this.configuration
                .GetConnectionString(name: "DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }

        private static void ConfigureIdentityTables(ModelBuilder builder)
        {
            builder.Entity<Role>().ToTable(name: "Roles");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable(name: "RoleClaims");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable(name: "UserClaims");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable(name: "UserLogins");
            builder.Entity<IdentityUserRole<Guid>>().ToTable(name: "UserRoles");
            builder.Entity<IdentityUserToken<Guid>>().ToTable(name: "UserTokens");

            builder.Entity<User>(action =>
            {
                action.ToTable(name: "Users");

                action
                    .Property(prop => prop.Gender)
                    .HasConversion(
                        x => x.ToString(),
                        x => (Gender)Enum.Parse(typeof(Gender), x));

                action
                    .HasIndex(prop => prop.Email)
                    .IsUnique();

                action
                    .HasIndex(prop => prop.UserName)
                    .IsUnique();

                action.HasIndex(prop => prop.FirstName);
                action.HasIndex(prop => prop.LastName);

                action
                    .HasIndex(prop => prop.BirthDate)
                    .IsDescending();
            });

            List<Role> roles = new()
            {
                new Role {
                    Name = "Admin",
                    Id = Guid.NewGuid(),
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Role {
                    Name = "User" ,
                    Id = Guid.NewGuid(),
                    NormalizedName = "USER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            };

            builder.Entity<Role>(action =>
            {
                action.HasData(roles);
            });

        }
    }
}
