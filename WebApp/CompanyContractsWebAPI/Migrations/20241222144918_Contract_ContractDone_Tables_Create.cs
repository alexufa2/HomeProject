using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CompanyContractsWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Contract_ContractDone_Tables_Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contract",
                schema: "dbo",
                columns: table => new
                {
                    Company_Id = table.Column<int>(type: "integer", nullable: false),
                    Good_Id = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Good_Count = table.Column<int>(type: "integer", nullable: false),
                    Total_Sum = table.Column<decimal>(type: "money", nullable: false),
                    Done_Sum = table.Column<decimal>(type: "money", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contract_company_Company_Id",
                        column: x => x.Company_Id,
                        principalSchema: "dbo",
                        principalTable: "company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_contract_good_Good_Id",
                        column: x => x.Good_Id,
                        principalSchema: "dbo",
                        principalTable: "good",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contract_done",
                schema: "dbo",
                columns: table => new
                {
                    Contract_Id = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Done_Amount = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract_done", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contract_done_contract_Contract_Id",
                        column: x => x.Contract_Id,
                        principalSchema: "dbo",
                        principalTable: "contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contract_Company_Id",
                schema: "dbo",
                table: "contract",
                column: "Company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_contract_Good_Id",
                schema: "dbo",
                table: "contract",
                column: "Good_Id");

            migrationBuilder.CreateIndex(
                name: "IX_contract_done_Contract_Id",
                schema: "dbo",
                table: "contract_done",
                column: "Contract_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contract_done",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "contract",
                schema: "dbo");
        }
    }
}
