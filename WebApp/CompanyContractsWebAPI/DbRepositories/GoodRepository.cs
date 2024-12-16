using CompanyContractsWebAPI.Models;

namespace CompanyContractsWebAPI.DbRepositories
{
    public class GoodRepository : BaseRepository<Good>
    {
        public GoodRepository(string? connectionString) :
            base(connectionString)
        { }

        protected override string _tableName { get => "good"; }

        protected override string GetInsertSQL()
        {
            return $"INSERT INTO {_fullTableName} (\"{nameof(Good.Name).ToLower()}\", {nameof(Good.Description)}, {nameof(Good.Measurement_Unit)}) " +
                $"Values (@{nameof(Good.Name)}, @{nameof(Good.Description)}, @{nameof(Good.Measurement_Unit)})";
        }

        protected override string GetUpdateSQL()
        {
            return string.Format(@"UPDATE {0} SET 
                                ""{1}"" = @{1},
                                {2} = @{2},
                                {3} = @{3}
                                WHERE {4} = @{4}",
                                _fullTableName,
                                nameof(Good.Name).ToLower(),
                                nameof(Good.Description),
                                nameof(Good.Measurement_Unit),
                                nameof(Good.Id));
        }
    }
}
