using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiRestful.Data.Migrations
{
    public partial class roletouser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "6b61026c-c371-41eb-a611-eb7feb64c01d");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "58f2708c-aefa-4849-93bc-6ce81324a9ca");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "7eff15e8-37b9-4b67-b040-3662728ddaad");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { "6cea7b79-fb3b-4386-818a-4447bd3f4041", 0, null, "3ee73c3f-9511-4f13-be1e-269fc1218f9d", "admin@ymail.com", false, null, false, null, null, null, "AQAAAAIAAYagAAAAEK5e+nloKnwlf0fwgIt6wn05nV5EdLyJVBxwWclL3YPobCsDCRvgZzRbcGzdBNFd/w==", null, false, "b6ac74b7-e360-49fa-9c74-8672046db205", false, "admin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8d03dd53-010f-454c-9476-cdd49ca3efcc", null, "User", "User" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "54a64e99-c242-44f0-aff2-d19a69d2fca9", null, "Admin", "Admin" });
        }
    }
}
