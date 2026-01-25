using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CompanyContractsWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Conatracts_and_ContractsDone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "contract",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Company_Name = table.Column<string>(type: "text", nullable: false),
                    Good_Name = table.Column<string>(type: "text", nullable: false),
                    Good_Count = table.Column<int>(type: "integer", nullable: false),
                    Total_Sum = table.Column<decimal>(type: "numeric", nullable: false),
                    Done_Sum = table.Column<decimal>(type: "numeric", nullable: false),
                    StatusName = table.Column<string>(type: "text", nullable: false),
                    IntegrationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "contract_done",
                schema: "dbo",
                columns: table => new
                {
                    Contract_Id = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Done_Amount = table.Column<decimal>(type: "money", nullable: false),
                    IntegrationId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "IX_contract_IntegrationId",
                schema: "dbo",
                table: "contract",
                column: "IntegrationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_contract_done_Contract_Id",
                schema: "dbo",
                table: "contract_done",
                column: "Contract_Id");

            migrationBuilder.CreateIndex(
                name: "IX_contract_done_IntegrationId",
                schema: "dbo",
                table: "contract_done",
                column: "IntegrationId",
                unique: true);
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
