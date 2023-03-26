using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewExpoApplication.Data.SqlServer.Migrations
{
    public partial class AddDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyDomain",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyDomain",
                table: "Companies");
        }
    }
}
