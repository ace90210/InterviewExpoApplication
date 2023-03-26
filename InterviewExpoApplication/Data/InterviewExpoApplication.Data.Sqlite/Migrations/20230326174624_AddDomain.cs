using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewExpoApplication.Data.Sqlite.Migrations
{
    public partial class AddDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyDomain",
                table: "Companies",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyDomain",
                table: "Companies");
        }
    }
}
