using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyContractsWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class CompanyGoodPrice_Tables_Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Purpose_Id",
                schema: "dbo",
                table: "company");

            migrationBuilder.CreateTable(
                name: "company_good_price",
                schema: "dbo",
                columns: table => new
                {
                    Company_Id = table.Column<int>(type: "integer", nullable: false),
                    Good_Id = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company_good_price", x => new { x.Company_Id, x.Good_Id });
                    table.ForeignKey(
                        name: "FK_company_good_price_company_Company_Id",
                        column: x => x.Company_Id,
                        principalSchema: "dbo",
                        principalTable: "company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_company_good_price_good_Good_Id",
                        column: x => x.Good_Id,
                        principalSchema: "dbo",
                        principalTable: "good",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_company_good_price_Good_Id",
                schema: "dbo",
                table: "company_good_price",
                column: "Good_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "company_good_price",
                schema: "dbo");

            migrationBuilder.AddColumn<int>(
                name: "Purpose_Id",
                schema: "dbo",
                table: "company",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
