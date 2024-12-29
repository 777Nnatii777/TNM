using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TNM.Migrations
{
    /// <inheritdoc />
    public partial class AddTestAddedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestAddedId",
                table: "TestAssignments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TestAddedId",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TestAddeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccessCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AuthorId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AdedUserId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TestStatus = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAddeds", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TestAssignments_TestAddedId",
                table: "TestAssignments",
                column: "TestAddedId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TestAddedId",
                table: "Questions",
                column: "TestAddedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_TestAddeds_TestAddedId",
                table: "Questions",
                column: "TestAddedId",
                principalTable: "TestAddeds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestAssignments_TestAddeds_TestAddedId",
                table: "TestAssignments",
                column: "TestAddedId",
                principalTable: "TestAddeds",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_TestAddeds_TestAddedId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_TestAssignments_TestAddeds_TestAddedId",
                table: "TestAssignments");

            migrationBuilder.DropTable(
                name: "TestAddeds");

            migrationBuilder.DropIndex(
                name: "IX_TestAssignments_TestAddedId",
                table: "TestAssignments");

            migrationBuilder.DropIndex(
                name: "IX_Questions_TestAddedId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "TestAddedId",
                table: "TestAssignments");

            migrationBuilder.DropColumn(
                name: "TestAddedId",
                table: "Questions");
        }
    }
}
