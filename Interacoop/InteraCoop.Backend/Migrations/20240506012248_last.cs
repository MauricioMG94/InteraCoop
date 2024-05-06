using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteraCoop.Backend.Migrations
{
    /// <inheritdoc />
    public partial class last : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clients_Name",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ClientIdentifier",
                table: "Interactions");

            migrationBuilder.DropColumn(
                name: "UserCode",
                table: "Interactions");

            migrationBuilder.AddColumn<int>(
                name: "InteractionId",
                table: "Clients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_Id",
                table: "Opportunities",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_Id",
                table: "Interactions",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Id",
                table: "Clients",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_InteractionId",
                table: "Clients",
                column: "InteractionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Interactions_InteractionId",
                table: "Clients",
                column: "InteractionId",
                principalTable: "Interactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Interactions_InteractionId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Opportunities_Id",
                table: "Opportunities");

            migrationBuilder.DropIndex(
                name: "IX_Interactions_Id",
                table: "Interactions");

            migrationBuilder.DropIndex(
                name: "IX_Clients_Id",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_InteractionId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "InteractionId",
                table: "Clients");

            migrationBuilder.AddColumn<int>(
                name: "ClientIdentifier",
                table: "Interactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserCode",
                table: "Interactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Name",
                table: "Clients",
                column: "Name",
                unique: true);
        }
    }
}
