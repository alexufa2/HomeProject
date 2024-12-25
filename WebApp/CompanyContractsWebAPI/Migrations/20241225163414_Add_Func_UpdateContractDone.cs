using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyContractsWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Add_Func_UpdateContractDone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var command = @"CREATE OR REPLACE FUNCTION dbo.update_contract_done(
	cdone_id integer,
	contract_id integer,
	done_amount money)
    RETURNS integer
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
DECLARE
	res_id integer :=-1;
    done_total money := 0;
	contract_total_sum money := 0;
BEGIN
	SELECT c.""Total_Sum"" INTO contract_total_sum
	FROM dbo.contract c WHERE c.""Id"" = contract_id;

	SELECT SUM(cd.""Done_Amount"") INTO done_total 
	FROM dbo.contract_done cd WHERE cd.""Contract_Id"" = contract_id and cd.""Id"" != cdone_id;

	IF (contract_total_sum - done_total) >= done_amount THEN
		
		UPDATE dbo.contract_done
		SET ""Done_Amount"" = done_amount
		WHERE ""Id"" = cdone_id;

		SELECT SUM(""Done_Amount"") INTO done_total
    	FROM dbo.contract_done cd
    	WHERE cd.""Contract_Id"" = contract_id;

		UPDATE dbo.contract 
		SET ""Done_Sum"" = done_total
		WHERE ""Id"" = contract_id;

		res_id := 1;
	End IF;

	return res_id;
EXCEPTION
    WHEN OTHERS THEN
        -- Handle exceptions if needed
        RAISE EXCEPTION 'Error on update contract_done for Id: %;contractId: % with amount: % ',cdone_id, contract_id, done_amount;
END;
$BODY$;";

            migrationBuilder.Sql(command);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var command = "DROP FUNCTION IF EXISTS dbo.update_contract_done(integer, integer, money)";
            migrationBuilder.Sql(command);
        }
    }
}
