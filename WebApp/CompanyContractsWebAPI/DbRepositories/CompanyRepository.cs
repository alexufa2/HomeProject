using CompanyContractsWebAPI.Models;

namespace CompanyContractsWebAPI.DbRepositories
{
    public class CompanyRepository : BaseRepository<Company>
    {
        public CompanyRepository(string? connectionString) :
            base(connectionString)
        { }

        protected override string _tableName { get => "company"; }

        protected override string GetInsertSQL()
        {
            return $"INSERT INTO {_fullTableName} (\"{nameof(Company.Name).ToLower()}\", {nameof(Company.Inn)}, {nameof(Company.Address)}, {nameof(Company.Purpose_Id)}) " +
                $"Values (@{nameof(Company.Name)}, @{nameof(Company.Inn)}, @{nameof(Company.Address)}, @{nameof(Company.Purpose_Id)})";
        }

        protected override string GetUpdateSQL()
        {
            return string.Format(@"UPDATE {0} SET 
                                ""{1}"" = @{1},
                                {2} = @{2},
                                {3} = @{3},
                                {4} = @{4}
                                WHERE {5} = @{5}",
                                _fullTableName,
                                nameof(Company.Name).ToLower(),
                                nameof(Company.Inn),
                                nameof(Company.Address),
                                nameof(Company.Purpose_Id),
                                nameof(Company.Id));
        }
    }
}
