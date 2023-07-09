using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiRestful.Data.Migrations
{
    public partial class seeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "4eb4e43d-e578-4150-ba65-b704f74d6981");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "d0b46bed-7db4-4d9a-9c13-bb43c1dce503");

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fullname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b74636d6-97ff-49f0-a3fe-5e1da79446f8", 0, null, "396dbffc-1545-44c3-8519-a0292d9b0d59", "admin@ymail.com", false, null, false, null, null, null, "AQAAAAIAAYagAAAAEABAiS8fSWPj8gw16RwFBPGkRER/k+yygTC8LqQK3z7WD1nzdj6yMA221fOfBryzYQ==", null, false, "0d6b00ef-a2a8-4514-b47d-ddb4fe57f0e8", false, "admin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "594d700f-a270-46ea-82cb-e0265fefbfa3", null, "User", "User" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d444cf98-1b68-4d75-be76-997e41df4fed", null, "Admin", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "b74636d6-97ff-49f0-a3fe-5e1da79446f8");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "594d700f-a270-46ea-82cb-e0265fefbfa3");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "d444cf98-1b68-4d75-be76-997e41df4fed");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d0b46bed-7db4-4d9a-9c13-bb43c1dce503", null, "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4eb4e43d-e578-4150-ba65-b704f74d6981", null, "User", "USER" });
        }
    }
}
