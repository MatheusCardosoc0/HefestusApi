using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HefestusApi.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "BruteCost",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "LiquidCost",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MaxPriceSale",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MinPriceSale",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubGroups_Name",
                table: "ProductSubGroup",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroups_Name",
                table: "ProductGroups",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFamilies_Name",
                table: "ProductFamily",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Product",
                column: "Name");

            migrationBuilder.Sql("CREATE INDEX IX_Products_Name_Lower ON \"Product\" (LOWER(\"Name\"));");

            migrationBuilder.Sql("CREATE INDEX IX_ProductSubGroups_Name_Lower ON \"ProductSubGroup\" (LOWER(\"Name\"));");

            migrationBuilder.Sql("CREATE INDEX IX_ProductGroups_Name_Lower ON \"ProductGroups\" (LOWER(\"Name\"));");

            migrationBuilder.Sql("CREATE INDEX IX_ProductFamily_Name_Lower ON \"ProductFamily\" (LOWER(\"Name\"));");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductSubGroups_Name",
                table: "ProductSubGroup");
            migrationBuilder.Sql("DROP INDEX IF EXISTS IX_ProductSubGroups_Name_Lower ON ProductSubGroup;");

            migrationBuilder.DropIndex(
                name: "IX_ProductGroups_Name",
                table: "ProductGroups");

            migrationBuilder.DropIndex(
                name: "IX_ProductFamilies_Name",
                table: "ProductFamily");

            migrationBuilder.DropIndex(
                name: "IX_Products_Name",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "BruteCost",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "LiquidCost",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "MaxPriceSale",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "MinPriceSale",
                table: "Product");

            migrationBuilder.Sql("DROP INDEX IF EXISTS IX_ProductGroups_Name_Lower ON ProductGroups;");

            migrationBuilder.Sql("DROP INDEX IF EXISTS IX_ProductFamily_Name_Lower ON ProductFamily;");

            migrationBuilder.Sql("DROP INDEX IF EXISTS IX_Products_Name_Lower ON Product;");
        }
    }
}
