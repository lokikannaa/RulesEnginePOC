using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RulesEnginePOC.Migrations
{
    /// <inheritdoc />
    public partial class fix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Entitlement",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Entitlement",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "PartType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Entitlement",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "PartsForEstimating");

            migrationBuilder.InsertData(
                table: "Entitlement",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "PartsForEstimating" });
        }
    }
}
