using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TNM.Migrations
{
    /// <inheritdoc />
    public partial class dodawanieQuestionTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuestionTitle",
                table: "Questions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionTitle",
                table: "Questions");
        }
    }
}
