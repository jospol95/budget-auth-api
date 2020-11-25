using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthorizationAPI.Infrastructure.Migrations
{
    public partial class migrationAuthTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AuthenticationTypes",
                columns: new[] { "Id", "AuthenticationTypeName" },
                values: new object[] { 1, "AuthenticationAPI" });

            migrationBuilder.InsertData(
                table: "AuthenticationTypes",
                columns: new[] { "Id", "AuthenticationTypeName" },
                values: new object[] { 2, "GoogleAPI" });

            migrationBuilder.InsertData(
                table: "AuthenticationTypes",
                columns: new[] { "Id", "AuthenticationTypeName" },
                values: new object[] { 3, "AuthenticationAPI" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuthenticationTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AuthenticationTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AuthenticationTypes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
