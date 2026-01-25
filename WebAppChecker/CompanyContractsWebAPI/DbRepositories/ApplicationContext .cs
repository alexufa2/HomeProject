using CompanyContractsWebAPI.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace CompanyContractsWebAPI.DbRepositories
{
    public class ApplicationContext : DbContext
    {
        public static readonly string Schema = "dbo";
        public static readonly string Migrations = "_migrations";
        public readonly string ConnectionStr;

        public ApplicationContext(IConfiguration configuration, DbContextOptions options)
               : base(options)
        {
            ConnectionStr = GetConnection(configuration);
        }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractDone> ContractDones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(Schema);

            //modelBuilder.Entity<IntReturn>().HasNoKey();
            modelBuilder.Entity<Contract>()
                .HasIndex(c => c.IntegrationId)
                .IsUnique();

            modelBuilder.Entity<ContractDone>()
               .HasIndex(c => c.IntegrationId)
               .IsUnique();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionStr, x => x.MigrationsHistoryTable(Migrations, Schema));
        }

        public static string GetConnection(IConfiguration configuration)
        {
            return configuration.GetConnectionString("DefaultConnectionString")
                ?? throw new Exception("Connection string not found");
        }

    }
}
