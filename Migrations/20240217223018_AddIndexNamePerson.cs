using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HefestusApi.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexNamePerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Persons_Name",
                table: "Person",
                column: "Name");
            migrationBuilder.Sql("CREATE INDEX idx_persons_name_lower ON \"Person\" (LOWER(\"Name\"));");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Persons_Name",
                table: "Person");
            migrationBuilder.Sql("DROP INDEX IF EXISTS idx_persons_name_lower ON \"Person\";");
        }
    }
}
