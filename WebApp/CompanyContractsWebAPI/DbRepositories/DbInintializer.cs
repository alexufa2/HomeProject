using CompanyContractsWebAPI.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace CompanyContractsWebAPI.DbRepositories
{
    public static class DbInintializer
    {
        public static void Initialize(ApplicationContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (context.Companyes.Any() || context.Goods.Any())
                return;

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Companyes.AddRange(
                    [
                        new Company { Name = "ООО \"Овощ и Ко\"", Address = "г. Москва, ул. Кого-то-там, д.7а, офис 10", Inn = "1234567" },
                        new Company { Name = "ЗАО \"И Рыба и Мясо\"", Address = "г. Рязань, ул. Пушкина, д. 15, офис 103", Inn = "4568904" },
                        new Company { Name = "ИП \"Все и сразу\"", Address = "г. Рязань, ул. Пушкина, д. 15, офис 103", Inn = "4568904" }
                    ]);

                    context.SaveChanges();

                    context.Goods.AddRange(
                        [
                            new Good { Name = "Морковь", Description = "Обычная морковь. Корнеплод, говорят полезна для глаз", Measurement_Unit = "1 кг" },
                            new Good { Name = "Лук репчатый", Description = "Используется почти во всех блюдах", Measurement_Unit = "1 кг" },
                            new Good { Name = "Говядина 1 категории", Description = "Мясо КРС", Measurement_Unit = "1 кг" },
                            new Good { Name = "Курица бройлер, тушка", Description = "Общипанная и выпотрошенная тушка курицы", Measurement_Unit = "1 кг" }
                        ]);

                    context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }

        }
    }
}
