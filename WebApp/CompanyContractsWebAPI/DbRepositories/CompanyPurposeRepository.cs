using CompanyContractsWebAPI.Models;

namespace CompanyContractsWebAPI.DbRepositories
{
    public class CompanyPurposeRepository : BaseRepository<CompanyPurpose>
    {
        public CompanyPurposeRepository(string? connectionString) :
            base(connectionString)
        { }

        protected override string _tableName { get => "company_purpose"; }

        protected override string GetInsertSQL()
        {
            return $"INSERT INTO {_fullTableName} (\"{nameof(CompanyPurpose.Name).ToLower()}\", {nameof(CompanyPurpose.Description)}) " +
                $"Values (@{nameof(CompanyPurpose.Name)}, @{nameof(CompanyPurpose.Description)})";
        }

        protected override string GetUpdateSQL()
        {
            return string.Format(@"UPDATE {0} SET 
                                ""{1}"" = @{1},
                                {2} = @{2}
                                WHERE {3} = @{3}",
                                _fullTableName,
                                nameof(CompanyPurpose.Name).ToLower(),
                                nameof(CompanyPurpose.Description),
                                nameof(CompanyPurpose.Id));
        }
    }
}
