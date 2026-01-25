using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyContractsWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProc_AddContractDone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var command = @"
DROP PROCEDURE IF EXISTS dbo.add_contract_done(integer, money);

CREATE OR REPLACE PROCEDURE dbo.add_contract_done(
	IN contract_id integer,
	IN done_amount money,
	IN integrId UUID,
	OUT new_id integer)
LANGUAGE 'plpgsql'
AS $BODY$

DECLARE
    done_total money := 0;
	contract_total_sum money := 0;
	contract_status text :='';
BEGIN
	new_id := -1;
	
	SELECT c.""Total_Sum"", c.""Status"" INTO contract_total_sum, contract_status
	FROM dbo.contract c WHERE c.""Id"" = contract_id;
	
	IF contract_status ='Finished' or contract_status = 'Canceled' THEN
		Return;
	END IF;

	SELECT SUM(cd.""Done_Amount"") INTO done_total 
	FROM dbo.contract_done cd WHERE cd.""Contract_Id"" = contract_id;

	IF ((contract_total_sum - done_total) >= done_amount OR done_total IS NULL) AND done_amount <= contract_total_sum THEN
		
		INSERT INTO dbo.contract_done (""Contract_Id"", ""Done_Amount"", ""IntegrationId"")
		Values (contract_id, done_amount, integrId)
		Returning ""Id"" INTO new_id;

		SELECT SUM(""Done_Amount"") INTO done_total
    	FROM dbo.contract_done cd
    	WHERE cd.""Contract_Id"" = contract_id;

		contract_status := 'Started';

		IF done_total = contract_total_sum THEN
			contract_status := 'Finished';
		END IF;

		UPDATE dbo.contract 
		SET ""Done_Sum"" = done_total, ""Status"" = contract_status
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
            var command = @"CREATE OR REPLACE PROCEDURE dbo.add_contract_done(
	IN contract_id integer,
	IN done_amount money,
	OUT new_id integer)
LANGUAGE 'plpgsql'
AS $BODY$

DECLARE
    done_total money := 0;
	contract_total_sum money := 0;
	contract_status text :='';
BEGIN
	new_id := -1;
	
	SELECT c.""Total_Sum"", c.""Status"" INTO contract_total_sum, contract_status
	FROM dbo.contract c WHERE c.""Id"" = contract_id;
	
	IF contract_status ='Finished' or contract_status = 'Canceled' THEN
		Return;
	END IF;

	SELECT SUM(cd.""Done_Amount"") INTO done_total 
	FROM dbo.contract_done cd WHERE cd.""Contract_Id"" = contract_id;

	IF ((contract_total_sum - done_total) >= done_amount OR done_total IS NULL) AND done_amount <= contract_total_sum THEN
		
		INSERT INTO dbo.contract_done (""Contract_Id"", ""Done_Amount"")
		Values (contract_id, done_amount)
		Returning ""Id"" INTO new_id;

		SELECT SUM(""Done_Amount"") INTO done_total
    	FROM dbo.contract_done cd
    	WHERE cd.""Contract_Id"" = contract_id;

		contract_status := 'Started';

		IF done_total = contract_total_sum THEN
			contract_status := 'Finished';
		END IF;

		UPDATE dbo.contract 
		SET ""Done_Sum"" = done_total, ""Status"" = contract_status
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
    }
}
