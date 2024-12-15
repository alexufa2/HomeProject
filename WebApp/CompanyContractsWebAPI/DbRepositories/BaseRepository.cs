using Dapper;
using Npgsql;

namespace CompanyContractsWebAPI.DbRepositories
{
    public abstract class BaseRepository<T>: IRepository<T> where T : class
    {
        private string _connectionString { get; set; }

        protected abstract string _tableName {get;}

        protected virtual string _schemaName => "dbo";

        protected virtual string _fullTableName => $"{_schemaName}.{_tableName}";

        public BaseRepository(string? connectionString)
        {
            if(string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            _connectionString = connectionString;
        }

        
        protected abstract string GetInsertSQL();
        
        public virtual int Create(T item)
        {
            int result;

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                string commandText = $"{GetInsertSQL()} RETURNING id";
                result = connection.QuerySingle<int>(commandText, item);
                connection.Close();
            }

            return result;
        }

        public virtual T GetById(int id)
        {
            IEnumerable<T> data;

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string commandText = $"SELECT * FROM {_schemaName}.{_tableName} WHERE id = @id";
                var queryArgs = new { Id = id };
                data = connection.Query<T>(commandText, queryArgs);

                connection.Close();
            }

            if (data == null || !data.Any())
                return null;

            return data.FirstOrDefault();
        }

        public virtual IEnumerable<T> GetAll()
        {
            IEnumerable<T> result;

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                string commandText = $"SELECT * FROM {_schemaName}.{_tableName}";
                result = connection.Query<T>(commandText);
                connection.Close();
            }

            return result;
        }

        protected abstract string GetUpdateSQL();

        public virtual bool Update(T item)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                string commandText = GetUpdateSQL();
                connection.Query(commandText, item);
                connection.Close();
            }

            return true;
        }

        public virtual bool Delete(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                
                string commandText = $"DELETE FROM {_schemaName}.{_tableName} WHERE id = @id";
                var queryArgs = new { Id = id };
                connection.Query(commandText, queryArgs);

                connection.Close();
            }

            return true;
        }

    }
}
