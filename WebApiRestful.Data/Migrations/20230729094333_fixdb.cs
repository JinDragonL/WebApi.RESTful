using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiRestful.Data.Migrations
{
    public partial class fixdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "12f06ef4-5d20-4d94-9aab-49d6385428ca");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5ba1551f-c8b5-4259-95c8-a57f11222e0a", "1bc3f821-1f03-442b-8a42-4688b99e3703" });

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "1bc3f821-1f03-442b-8a42-4688b99e3703");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "5ba1551f-c8b5-4259-95c8-a57f11222e0a");

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fullname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "632ed61a-6c5a-4b7c-aaf2-ecb93a268a82", 0, null, "adc7883a-4d36-4b1f-94fa-78c89360e1a2", "admin@ymail.com", false, null, false, null, "ADMIN@YMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEB8bZ45kjrkUCLMNw4wy7R851Kwf9Ko4m4GIXMZtEZRV7c3QukP5Z1xunhpDIjdo/A==", null, false, "f1104f59-6978-484e-ace8-38b1df963103", false, "admin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "06f5e92a-cf85-415f-b7eb-a4c459903999", null, "User", "USER" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b3fc9c06-01fa-40cb-9c34-3c5b70cb7b56", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b3fc9c06-01fa-40cb-9c34-3c5b70cb7b56", "632ed61a-6c5a-4b7c-aaf2-ecb93a268a82" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "06f5e92a-cf85-415f-b7eb-a4c459903999");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b3fc9c06-01fa-40cb-9c34-3c5b70cb7b56", "632ed61a-6c5a-4b7c-aaf2-ecb93a268a82" });

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "632ed61a-6c5a-4b7c-aaf2-ecb93a268a82");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "b3fc9c06-01fa-40cb-9c34-3c5b70cb7b56");

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeRefreshToken = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredDateAccessToken = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredDateRefreshToken = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fullname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1bc3f821-1f03-442b-8a42-4688b99e3703", 0, null, "80e5845c-4bff-4ec1-a209-c70484e9660b", "admin@ymail.com", false, null, false, null, "ADMIN@YMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEJArcyoJnZj94FYHHPogBZy2DDHY1PUYgJ954J1XJHUVwoj4vNaPgKbACDaVwosbCA==", null, false, "19c2a96f-5a33-45dd-914f-4eccd4bc5388", false, "admin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "12f06ef4-5d20-4d94-9aab-49d6385428ca", null, "User", "USER" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5ba1551f-c8b5-4259-95c8-a57f11222e0a", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "5ba1551f-c8b5-4259-95c8-a57f11222e0a", "1bc3f821-1f03-442b-8a42-4688b99e3703" });
        }
    }
}
