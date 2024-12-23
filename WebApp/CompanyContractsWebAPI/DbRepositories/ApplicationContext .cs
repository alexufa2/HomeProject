using CompanyContractsWebAPI.Models.DB;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

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

            //modelBuilder.Entity<Company>().HasData(
            //   new Company { Id = -1, Name = "ООО \"Овощ и Ко\"", Address = "г. Москва, ул. Кого-то-там, д.7а, офис 10", Inn = "1234567" },
            //   new Company { Id = -2, Name = "ЗАО \"И Рыба и Мясо\"", Address = "г. Рязань, ул. Пушкина, д. 15, офис 103", Inn = "4568904" },
            //   new Company { Id = -3, Name = "ИП \"Все и сразу\"", Address = "г. Рязань, ул. Пушкина, д. 15, офис 103", Inn = "4568904" });

            //modelBuilder.Entity<Good>().HasData(
            //   new Good { Id = -1, Name = "Морковь", Description = "Обычная морковь. Корнеплод, говорят полезна для глаз", Measurement_Unit = "1 кг" },
            //   new Good { Id = -2, Name = "Лук репчатый", Description = "Используется почти во всех блюдах", Measurement_Unit = "1 кг" },
            //   new Good { Id = -3, Name = "Говядина 1 категории", Description = "Мясо КРС", Measurement_Unit = "1 кг" },
            //   new Good { Id = -4, Name = "Курица бройлер, тушка", Description = "Общипанная и выпотрошенная тушка курицы", Measurement_Unit = "1 кг" });
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
