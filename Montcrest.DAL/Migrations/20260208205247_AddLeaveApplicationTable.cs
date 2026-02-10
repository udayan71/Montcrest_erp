using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Montcrest.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddLeaveApplicationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeaveType",
                table: "LeaveApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveType",
                table: "LeaveApplications");
        }
    }
}
