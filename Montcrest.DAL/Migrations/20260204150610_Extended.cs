using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Montcrest.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Extended : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmployee",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinedOn",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExamCompletedOn",
                table: "JobApplications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExamLink",
                table: "JobApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExamResult",
                table: "JobApplications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExamScore",
                table: "JobApplications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExamSentOn",
                table: "JobApplications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HrMarks",
                table: "JobApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "JobApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewdOn",
                table: "JobApplications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SelectionDate",
                table: "JobApplications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "JobApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmployee",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "JoinedOn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ExamCompletedOn",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "ExamLink",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "ExamResult",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "ExamScore",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "ExamSentOn",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "HrMarks",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "ReviewdOn",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "SelectionDate",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "JobApplications");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "FullName", "MobileNumber", "PasswordHash", "Role" },
                values: new object[] { 100, "Montcrest Office", "hr@montcrest.com", "Montcrest HR", "9999999999", "$2a$11$YtzBMmmw.6i2YYVQRQuSwuxl.aUMwW0whkLEg1vLyQJfAnod2bDhK", 2 });
        }
    }
}
