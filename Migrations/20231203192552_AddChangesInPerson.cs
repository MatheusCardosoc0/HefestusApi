using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HefestusApi.Migrations
{
    /// <inheritdoc />
    public partial class AddChangesInPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "urlImage",
                table: "Person",
                newName: "UrlImage");

            migrationBuilder.RenameColumn(
                name: "isBlocked",
                table: "Person",
                newName: "IsBlocked");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UrlImage",
                table: "Person",
                newName: "urlImage");

            migrationBuilder.RenameColumn(
                name: "IsBlocked",
                table: "Person",
                newName: "isBlocked");
        }
    }
}
