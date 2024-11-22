using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class Addeduserintobudgetandtrxentites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Category_BudgetTypeCategoryId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Category_TransactionTypeCategoryId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Category_CategoryName",
                table: "Category");

            migrationBuilder.AlterColumn<long>(
                name: "TransactionTypeCategoryId",
                table: "Transactions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransactionDate",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OwnerUserId",
                table: "Transactions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Category",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Budgets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Budgets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BudgetTypeCategoryId",
                table: "Budgets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BudgetName",
                table: "Budgets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Budgets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OwnerUserId",
                table: "Budgets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_OwnerUserId",
                table: "Transactions",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CategoryName",
                table: "Category",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_OwnerUserId",
                table: "Budgets",
                column: "OwnerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Category_BudgetTypeCategoryId",
                table: "Budgets",
                column: "BudgetTypeCategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Users_OwnerUserId",
                table: "Budgets",
                column: "OwnerUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Category_TransactionTypeCategoryId",
                table: "Transactions",
                column: "TransactionTypeCategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_OwnerUserId",
                table: "Transactions",
                column: "OwnerUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Category_BudgetTypeCategoryId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Users_OwnerUserId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Category_TransactionTypeCategoryId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_OwnerUserId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_OwnerUserId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Category_CategoryName",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_OwnerUserId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "OwnerUserId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OwnerUserId",
                table: "Budgets");

            migrationBuilder.AlterColumn<long>(
                name: "TransactionTypeCategoryId",
                table: "Transactions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransactionDate",
                table: "Transactions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Category",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Budgets",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Budgets",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "BudgetTypeCategoryId",
                table: "Budgets",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "BudgetName",
                table: "Budgets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Budgets",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CategoryName",
                table: "Category",
                column: "CategoryName",
                unique: true,
                filter: "[CategoryName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Category_BudgetTypeCategoryId",
                table: "Budgets",
                column: "BudgetTypeCategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Category_TransactionTypeCategoryId",
                table: "Transactions",
                column: "TransactionTypeCategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId");
        }
    }
}
