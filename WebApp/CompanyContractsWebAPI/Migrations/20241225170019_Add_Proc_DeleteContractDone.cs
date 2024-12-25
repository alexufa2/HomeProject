using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyContractsWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Add_Proc_DeleteContractDone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var command = @"CREATE OR REPLACE PROCEDURE dbo.delete_contract_done(
	IN cdone_id integer)
LANGUAGE 'plpgsql'
AS $BODY$
DECLARE
	contract_id integer :=0;
    done_total money := 0;
BEGIN
		SELECT ""Contract_Id"" INTO contract_id
		FROM dbo.contract_done
		WHERE ""Id"" = cdone_id;
		
		DELETE FROM dbo.contract_done
		WHERE ""Id"" = cdone_id;

		SELECT SUM(""Done_Amount"") INTO done_total
    	FROM dbo.contract_done cd
    	WHERE cd.""Contract_Id"" = contract_id;

		UPDATE dbo.contract 
		SET ""Done_Sum"" = done_total
		WHERE ""Id"" = contract_id;

EXCEPTION
    WHEN OTHERS THEN
        -- Handle exceptions if needed
        RAISE EXCEPTION 'Error on delete contract_done for Id %', cdone_id;
END;
$BODY$;";

            migrationBuilder.Sql(command);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var command = @"DROP PROCEDURE IF EXISTS dbo.delete_contract_done(integer)";
            migrationBuilder.Sql(command);
        }
    }
}
