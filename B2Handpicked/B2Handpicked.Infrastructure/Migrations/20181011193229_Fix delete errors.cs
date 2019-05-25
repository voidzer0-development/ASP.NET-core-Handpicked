using Microsoft.EntityFrameworkCore.Migrations;

namespace B2Handpicked.Infrastructure.Migrations
{
    public partial class Fixdeleteerrors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactPersons_Customers_CustomerId",
                table: "ContactPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Labels_LabelId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Deal_Customers_CustomerId",
                table: "Deal");

            migrationBuilder.DropForeignKey(
                name: "FK_DealEmployee_Deal_DealId",
                table: "DealEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_DealEmployee_Employees_EmployeeId",
                table: "DealEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Labels_LabelId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Labels_LabelId",
                table: "Invoices");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPersons_Customers_CustomerId",
                table: "ContactPersons",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Labels_LabelId",
                table: "Customers",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Deal_Customers_CustomerId",
                table: "Deal",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DealEmployee_Deal_DealId",
                table: "DealEmployee",
                column: "DealId",
                principalTable: "Deal",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DealEmployee_Employees_EmployeeId",
                table: "DealEmployee",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Labels_LabelId",
                table: "Employees",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Labels_LabelId",
                table: "Invoices",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactPersons_Customers_CustomerId",
                table: "ContactPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Labels_LabelId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Deal_Customers_CustomerId",
                table: "Deal");

            migrationBuilder.DropForeignKey(
                name: "FK_DealEmployee_Deal_DealId",
                table: "DealEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_DealEmployee_Employees_EmployeeId",
                table: "DealEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Labels_LabelId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Labels_LabelId",
                table: "Invoices");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPersons_Customers_CustomerId",
                table: "ContactPersons",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Labels_LabelId",
                table: "Customers",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deal_Customers_CustomerId",
                table: "Deal",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DealEmployee_Deal_DealId",
                table: "DealEmployee",
                column: "DealId",
                principalTable: "Deal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DealEmployee_Employees_EmployeeId",
                table: "DealEmployee",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Labels_LabelId",
                table: "Employees",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Labels_LabelId",
                table: "Invoices",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
