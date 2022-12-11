using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.WebApiRestful.Data.Migrations
{
    public partial class addFiledCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeRefreshToken",
                table: "UserToken",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeRefreshToken",
                table: "UserToken");
        }
    }
}
