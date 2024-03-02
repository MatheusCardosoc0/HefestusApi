using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HefestusApi.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PaymentOptions_Name",
                table: "PaymentOptions",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentConditions_Name",
                table: "PaymentCondition",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PaymentOptions_Name",
                table: "PaymentOptions");

            migrationBuilder.DropIndex(
                name: "IX_PaymentConditions_Name",
                table: "PaymentCondition");
        }
    }
}
