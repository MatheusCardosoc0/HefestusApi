using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HefestusApi.Migrations
{
    /// <inheritdoc />
    public partial class Order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderProduct",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Order",
                newName: "TotalValue");

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMensuration",
                table: "Product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Amount",
                table: "OrderProduct",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "OrderProduct",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BruteValue",
                table: "Order",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<float>(
                name: "CostOfFreight",
                table: "Order",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfCompletion",
                table: "Order",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "Discount",
                table: "Order",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Order",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "LiquidValue",
                table: "Order",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "TypeFreight",
                table: "Order",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeOrder",
                table: "Order",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitOfMensuration",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "BruteValue",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CostOfFreight",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "DateOfCompletion",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "LiquidValue",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "TypeFreight",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "TypeOrder",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "OrderProduct",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "TotalValue",
                table: "Order",
                newName: "Value");
        }
    }
}
