using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HefestusApi.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreAtributesInPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ICMSContributor",
                table: "Person",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonType",
                table: "Person",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "ICMSContributor",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "PersonType",
                table: "Person");
        }
    }
}
