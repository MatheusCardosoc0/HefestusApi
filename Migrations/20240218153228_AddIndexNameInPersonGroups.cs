using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HefestusApi.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexNameInPersonGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PersonGroups_Name",
                table: "PersonGroup",
                column: "Name");

            migrationBuilder.Sql("CREATE INDEX idx_personGroup_name_lower ON \"PersonGroup\" (LOWER(\"Name\"));");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PersonGroups_Name",
                table: "PersonGroup");

            migrationBuilder.Sql("DROP INDEX IF EXISTS idx_personGroup_name_lower;");
        }
    }
}
