using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RulesEnginePOC.Migrations
{
    /// <inheritdoc />
    public partial class fix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rule_Rule_ParentRuleId",
                table: "Rule");

            migrationBuilder.DeleteData(
                table: "Entitlement",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.InsertData(
                table: "Entitlement",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "PartsForEstimating" });

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_Rule_ParentRuleId",
                table: "Rule",
                column: "ParentRuleId",
                principalTable: "Rule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rule_Rule_ParentRuleId",
                table: "Rule");

            migrationBuilder.DeleteData(
                table: "Entitlement",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "Entitlement",
                columns: new[] { "Id", "Name" },
                values: new object[] { 99, "NotRealEntitlement" });

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_Rule_ParentRuleId",
                table: "Rule",
                column: "ParentRuleId",
                principalTable: "Rule",
                principalColumn: "Id");
        }
    }
}
