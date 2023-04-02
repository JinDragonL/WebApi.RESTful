using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiRestful.Data.Migrations
{
    public partial class removeisactive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DBLog");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DBLog",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
