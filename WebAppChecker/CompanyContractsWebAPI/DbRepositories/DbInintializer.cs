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
        }
    }
}
