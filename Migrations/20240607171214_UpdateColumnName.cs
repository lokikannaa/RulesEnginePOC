using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RulesEnginePOC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Rule",
                newName: "RuleName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RuleName",
                table: "Rule",
                newName: "Name");
        }
    }
}
