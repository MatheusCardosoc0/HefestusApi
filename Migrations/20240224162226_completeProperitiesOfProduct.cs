using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HefestusApi.Migrations
{
    /// <inheritdoc />
    public partial class completeProperitiesOfProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceTotal",
                table: "Product",
                newName: "WholesalePrice");

            migrationBuilder.RenameColumn(
                name: "MaxPriceSale",
                table: "Product",
                newName: "PromotionalPrice");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Product",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageCost",
                table: "Product",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Batch",
                table: "Product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GTIN",
                table: "Product",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GTINtrib",
                table: "Product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MinWholesalePrice",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "NCM",
                table: "Product",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlImage",
                table: "Product",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageCost",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Batch",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "GTIN",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "GTINtrib",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "MinWholesalePrice",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "NCM",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "UrlImage",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "WholesalePrice",
                table: "Product",
                newName: "PriceTotal");

            migrationBuilder.RenameColumn(
                name: "PromotionalPrice",
                table: "Product",
                newName: "MaxPriceSale");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Product",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
