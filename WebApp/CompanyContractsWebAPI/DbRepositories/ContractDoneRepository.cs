using CompanyContractsWebAPI.Models.DB;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data.Common;

namespace CompanyContractsWebAPI.DbRepositories
{
    public class ContractDoneRepository : BaseRepository<ContractDone>, IContractDoneRepository
    {
        public ContractDoneRepository(ApplicationContext applicationContext) :
            base(applicationContext)
        { }

        public override ContractDone Create(ContractDone item)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_applicationContext.ConnectionStr))
            {
                con.Open();

                using (DbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "dbo.add_contract_done";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    var contractid = new NpgsqlParameter("contract_id", NpgsqlTypes.NpgsqlDbType.Integer);
                    contractid.Value = item.Contract_Id;
                    cmd.Parameters.Add(contractid);

                    var amount = new NpgsqlParameter("done_amount", NpgsqlTypes.NpgsqlDbType.Money);
                    amount.Value = item.Done_Amount;
                    cmd.Parameters.Add(amount);

                    var integrId = new NpgsqlParameter("integrid", NpgsqlTypes.NpgsqlDbType.Uuid);
                    integrId.Value = item.IntegrationId;
                    cmd.Parameters.Add(integrId);

                    var idParam = new NpgsqlParameter("new_id", NpgsqlTypes.NpgsqlDbType.Integer);
                    idParam.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(idParam);

                    cmd.ExecuteNonQuery();

                    item.Id = (int)cmd.Parameters["new_id"].Value;
                }

                con.Close();
            }

            //_applicationContext.Database.ExecuteSqlRaw(
            //    "CALL dbo.add_contract_done(@contract_Id, @done_amount, @new_id)",
            //    contractid, amount, idParam);

            return item;
        }

        public override ContractDone Update(ContractDone item)
        {
            var idParam = new NpgsqlParameter()
            {
                ParameterName = "@id",
                Value = item.Id,
                NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer
            };

            var contractIdParam = new NpgsqlParameter()
            {
                ParameterName = "@contract_Id",
                Value = item.Contract_Id,
                NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer
            };

            var doneAmountParam = new NpgsqlParameter()
            {
                ParameterName = "@done_amount",
                Value = item.Done_Amount,
                NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Money
            };

            var result = _applicationContext.Set<IntReturn>().FromSqlRaw(
                "select dbo.update_contract_done(@id, @contract_Id, @done_amount) as Value",
                idParam, contractIdParam, doneAmountParam)
                .AsEnumerable()
                .First().Value;

            //var result = _applicationContext.Database.ExecuteSqlRaw(
            //    "select dbo.update_contract_done(@id, @contract_Id, @done_amount) as Value",
            //    idParam, contractIdParam, doneAmountParam);

            return item;
        }

        public override bool Delete(int id)
        {
            var idParam = new NpgsqlParameter()
            {
                ParameterName = "@id",
                Value = id,
                NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer
            };

            _applicationContext.Database.ExecuteSqlRaw("CALL dbo.delete_contract_done(@id)", idParam);

            return true;
        }

        public IEnumerable<ContractDone> GetByContractId(int contractId)
        {
            return _applicationContext.ContractDones.Where(w => w.Contract_Id == contractId);
        }
    }
}
