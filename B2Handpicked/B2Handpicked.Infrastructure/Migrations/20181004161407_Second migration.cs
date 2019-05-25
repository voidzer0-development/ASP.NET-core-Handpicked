using Microsoft.EntityFrameworkCore.Migrations;

namespace B2Handpicked.Infrastructure.Migrations
{
    public partial class Secondmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactPersons_Labels_LabelId",
                table: "ContactPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_Deal_Labels_LabelId",
                table: "Deal");

            migrationBuilder.DropIndex(
                name: "IX_Deal_LabelId",
                table: "Deal");

            migrationBuilder.DropIndex(
                name: "IX_ContactPersons_LabelId",
                table: "ContactPersons");

            migrationBuilder.DropColumn(
                name: "LabelId",
                table: "Deal");

            migrationBuilder.DropColumn(
                name: "LabelId",
                table: "ContactPersons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LabelId",
                table: "Deal",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LabelId",
                table: "ContactPersons",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deal_LabelId",
                table: "Deal",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersons_LabelId",
                table: "ContactPersons",
                column: "LabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPersons_Labels_LabelId",
                table: "ContactPersons",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deal_Labels_LabelId",
                table: "Deal",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
