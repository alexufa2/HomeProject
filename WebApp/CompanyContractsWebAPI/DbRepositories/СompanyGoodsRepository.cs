using CompanyContractsWebAPI.Models;

namespace CompanyContractsWebAPI.DbRepositories
{
    public class CompanyGoodsRepository : BaseRepository<CompanyGoods>
    {
        public CompanyGoodsRepository(string? connectionString) :
           base(connectionString)
        { }

        protected override string _tableName => "company_goods";

        protected override string GetInsertSQL()
        {
            return $"INSERT INTO {_fullTableName} ({nameof(CompanyGoods.Company_Id)}, {nameof(CompanyGoods.Good_Id)}, {nameof(CompanyGoods.Price)}) " +
                $"Values (@{nameof(CompanyGoods.Company_Id)}, @{nameof(CompanyGoods.Good_Id)}, @{nameof(CompanyGoods.Price)})";
        }

        protected override string GetUpdateSQL()
        {
            return string.Format(@"UPDATE {0} SET 
                                {1} = @{1},
                                {2} = @{2},
                                {3} = @{3}
                                WHERE {4} = @{4}",
                                _fullTableName,
                                nameof(CompanyGoods.Company_Id),
                                nameof(CompanyGoods.Good_Id),
                                nameof(CompanyGoods.Price),
                                nameof(CompanyGoods.Id));
        }
    }
}
