using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Montcrest.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedHrUserFixedHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "FullName", "MobileNumber", "PasswordHash", "Role" },
                values: new object[] { 100, "Montcrest Office", "hr@montcrest.com", "Montcrest HR", "9999999999", "$2a$11$YtzBMmmw.6i2YYVQRQuSwuxl.aUMwW0whkLEg1vLyQJfAnod2bDhK", 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 100);
        }
    }
}
