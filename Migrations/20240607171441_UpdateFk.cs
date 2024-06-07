using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RulesEnginePOC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rule_Rule_RuleNameFK",
                table: "Rule");

            migrationBuilder.RenameColumn(
                name: "RuleNameFK",
                table: "Rule",
                newName: "ParentRuleId");

            migrationBuilder.RenameIndex(
                name: "IX_Rule_RuleNameFK",
                table: "Rule",
                newName: "IX_Rule_ParentRuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_Rule_ParentRuleId",
                table: "Rule",
                column: "ParentRuleId",
                principalTable: "Rule",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rule_Rule_ParentRuleId",
                table: "Rule");

            migrationBuilder.RenameColumn(
                name: "ParentRuleId",
                table: "Rule",
                newName: "RuleNameFK");

            migrationBuilder.RenameIndex(
                name: "IX_Rule_ParentRuleId",
                table: "Rule",
                newName: "IX_Rule_RuleNameFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_Rule_RuleNameFK",
                table: "Rule",
                column: "RuleNameFK",
                principalTable: "Rule",
                principalColumn: "Id");
        }
    }
}
