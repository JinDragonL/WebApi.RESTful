using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiRestful.Data.Migrations
{
    public partial class removeusertbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

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
                table: "ApplicationUser",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fullname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "10bfa6e7-bded-4e3e-8463-af8eda42a41e", 0, null, "da9a0851-2578-4b08-acf1-1a5a5b32e273", "admin@ymail.com", false, null, false, null, null, null, "AQAAAAIAAYagAAAAEO30hK2ywEkjx/O0cXBHWsJ0caZn/82YJiel1ckzU0QFcjP8U2Wbi+ys5xAS7DrhvA==", null, false, "40ac5c99-30c7-4962-9de2-51ba7b649e2f", false, "admin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "97d8c63f-4ca8-4475-a096-317efd838884", null, "User", "User" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "34849bc0-f7c2-4ac7-9ff0-ae07c7f92c73", null, "Admin", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "10bfa6e7-bded-4e3e-8463-af8eda42a41e");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "34849bc0-f7c2-4ac7-9ff0-ae07c7f92c73");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "97d8c63f-4ca8-4475-a096-317efd838884");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastLoggedIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

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
    }
}
