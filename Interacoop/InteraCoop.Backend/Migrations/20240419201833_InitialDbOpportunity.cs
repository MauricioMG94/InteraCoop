using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteraCoop.Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbOpportunity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OpportunityId",
                table: "Campaigns",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Interactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientIdentifier = table.Column<int>(type: "int", nullable: false),
                    UserCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InteractionType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    InteractionCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ObservationsInteraction = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Office = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AuditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Opportunities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    OpportunityObservations = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedAcquisitionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opportunities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_OpportunityId",
                table: "Campaigns",
                column: "OpportunityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Opportunities_OpportunityId",
                table: "Campaigns",
                column: "OpportunityId",
                principalTable: "Opportunities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Opportunities_OpportunityId",
                table: "Campaigns");

            migrationBuilder.DropTable(
                name: "Interactions");

            migrationBuilder.DropTable(
                name: "Opportunities");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_OpportunityId",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "OpportunityId",
                table: "Campaigns");
        }
    }
}
