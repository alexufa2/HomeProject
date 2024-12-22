using CompanyContractsWebAPI.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace CompanyContractsWebAPI.DbRepositories
{
    public class ApplicationContext : DbContext
    {
        public static readonly string Schema = "dbo";
        public static readonly string Migrations = "_migrations";
        protected readonly string _connection;

        public ApplicationContext(IConfiguration configuration, DbContextOptions options)
               : base(options)
        {
            _connection = GetConnection(configuration);
        }
        public DbSet<Company> Companyes { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<CompanyGoodPrice> CompanyGoodPrices { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractDone> ContractDones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(Schema);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connection, x => x.MigrationsHistoryTable(Migrations, Schema));
        }

        public static string GetConnection(IConfiguration configuration)
        {
            return configuration.GetConnectionString("DefaultConnectionString")
                ?? throw new Exception("Connection string not found");
        }

    }
}
