using CompanyContractsWebAPI.Models;
using Dapper;
using Npgsql;

namespace CompanyContractsWebAPI.DbRepositories
{
    public abstract class BaseRepository<T>: IRepository<T> where T : class, IEntityWithId, new()
    {
        private string _connectionString { get; set; }

        protected abstract string _tableName {get;}

        protected virtual string _schemaName => "dbo";

        protected virtual string _fullTableName => $"{_schemaName}.{_tableName}";

        protected readonly string InsertSql;
        protected readonly string UpdateSql;

        public BaseRepository(string? connectionString)
        {
            if(string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            _connectionString = connectionString;
            InsertSql = GetInsertSQL();
            UpdateSql = GetUpdateSQL();
        }

        
        protected abstract string GetInsertSQL();
        
        public virtual T Create(T item)
        {
            int newId;

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                string commandText = $"{InsertSql} RETURNING id";
                newId = connection.QuerySingle<int>(commandText, item);
                connection.Close();
            }

            item.Id = newId;
            return item;
        }

        public virtual T GetById(int id)
        {
            IEnumerable<T> data;

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string commandText = $"SELECT * FROM {_fullTableName} WHERE id = @id";
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
                string commandText = $"SELECT * FROM {_fullTableName}";
                result = connection.Query<T>(commandText);
                connection.Close();
            }

            return result;
        }

        protected abstract string GetUpdateSQL();

        public virtual T Update(T item)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                connection.Query(UpdateSql, item);
                connection.Close();
            }

            return item;
        }

        public virtual bool Delete(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                
                string commandText = $"DELETE FROM {_fullTableName} WHERE id = @id";
                var queryArgs = new { Id = id };
                connection.Query(commandText, queryArgs);

                connection.Close();
            }

            return true;
        }

    }
}
