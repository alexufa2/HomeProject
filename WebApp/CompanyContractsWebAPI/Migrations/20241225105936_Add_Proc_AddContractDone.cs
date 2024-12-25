using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyContractsWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Add_Proc_AddContractDone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var command = @"CREATE OR REPLACE PROCEDURE dbo.add_contract_done(
	IN contract_id integer,
	IN done_amount money,
	OUT new_id integer)
LANGUAGE 'plpgsql'
AS $BODY$
DECLARE
    done_total money := 0;
	contract_total_sum money := 0;
BEGIN
	new_id := -1;

	SELECT c.""Total_Sum"" INTO contract_total_sum
	FROM dbo.contract c WHERE c.""Id"" = contract_id;

	SELECT SUM(cd.""Done_Amount"") INTO done_total 
	FROM dbo.contract_done cd WHERE cd.""Contract_Id"" = contract_id;

	IF (contract_total_sum - done_total) >= done_amount THEN
		
		INSERT INTO dbo.contract_done (""Contract_Id"", ""Done_Amount"")
		Values (contract_id, done_amount)
		Returning ""Id"" INTO new_id;

		SELECT SUM(""Done_Amount"") INTO done_total
    	FROM dbo.contract_done cd
    	WHERE cd.""Contract_Id"" = contract_id;

		UPDATE dbo.contract 
		SET ""Done_Sum"" = done_total
		WHERE ""Id"" = contract_id;
	End IF;

EXCEPTION
    WHEN OTHERS THEN
        -- Handle exceptions if needed
        RAISE EXCEPTION 'Error on adding contract_done for contractId % with amount: % ', contract_id, done_amount;
END;
$BODY$;";

            migrationBuilder.Sql(command);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var command = "DROP PROCEDURE dbo.add_contract_done";
            migrationBuilder.Sql(command);
        }
    }
}
