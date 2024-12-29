using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TNM.Migrations
{
    /// <inheritdoc />
    public partial class AddTestAddedRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "TestAddeds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TestAddeds_TestId",
                table: "TestAddeds",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestAddeds_Tests_TestId",
                table: "TestAddeds",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestAddeds_Tests_TestId",
                table: "TestAddeds");

            migrationBuilder.DropIndex(
                name: "IX_TestAddeds_TestId",
                table: "TestAddeds");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "TestAddeds");
        }
    }
}
