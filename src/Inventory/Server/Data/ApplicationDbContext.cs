using Duende.IdentityServer.EntityFramework.Options;
using Inventory.Shared;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;

namespace Inventory.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<Employee>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Asset>()
               .ToTable("Assets")
               .HasOne(a => a.Manufacture)
               .WithMany(b => b.Assets);

            builder.Entity<Employee>()
                .ToTable("Employees")
                .HasOne(a => a.Station)
                .WithMany(b => b.Employees);

            builder.Entity<Manufacture>()
                .ToTable("Manufactures")
                .HasMany(a => a.Assets)
                .WithOne(b => b.Manufacture);

            builder.ApplyConfiguration(new RoleConfiguration());

            builder.Entity<IdentityUserRole<string>>()
                .ToTable("UserRoles");

            builder.Entity<IdentityUserClaim<string>>()
                .ToTable("UserClaims");

            builder.Entity<IdentityUserLogin<string>>()
                .ToTable("UserLogins");

            builder.Entity<IdentityUserToken<string>>()
                .ToTable("UserTokens");

            builder.Entity<IdentityRoleClaim<string>>()
                .ToTable("RoleClaims");

            builder.Entity<Station>()
                .ToTable("Stations")
                .HasMany(a => a.Employees)
                .WithOne(b => b.Station);


            builder.Entity<Asset>()
                .Navigation(a => a.Manufacture)
                .AutoInclude();

            builder.Entity<Employee>()
                .Navigation(e => e.Station)
                .AutoInclude();
        }
        public virtual DbSet<Asset>? Assets { get; set; }
        public virtual DbSet<Employee>? Employees { get; set; }
        public virtual DbSet<Manufacture>? Manufactures { get; set; }
        public virtual DbSet<Station>? Stations { get; set; }
    }

    class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> b)
        {
            b.ToTable("Roles");
        }
    }
}