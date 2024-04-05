using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP3.Migrations
{
    public partial class migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Score_AspNetUsers_UserId",
                table: "Score");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Score",
                table: "Score");

            migrationBuilder.RenameTable(
                name: "Score",
                newName: "Scores");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Scores",
                newName: "TimeInSeconds");

            migrationBuilder.RenameIndex(
                name: "IX_Score_UserId",
                table: "Scores",
                newName: "IX_Scores_UserId");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Scores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Scores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Pseudo",
                table: "Scores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ScoreValue",
                table: "Scores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scores",
                table: "Scores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_AspNetUsers_UserId",
                table: "Scores",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_AspNetUsers_UserId",
                table: "Scores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Scores",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "Pseudo",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "ScoreValue",
                table: "Scores");

            migrationBuilder.RenameTable(
                name: "Scores",
                newName: "Score");

            migrationBuilder.RenameColumn(
                name: "TimeInSeconds",
                table: "Score",
                newName: "Value");

            migrationBuilder.RenameIndex(
                name: "IX_Scores_UserId",
                table: "Score",
                newName: "IX_Score_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Score",
                table: "Score",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Score_AspNetUsers_UserId",
                table: "Score",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
