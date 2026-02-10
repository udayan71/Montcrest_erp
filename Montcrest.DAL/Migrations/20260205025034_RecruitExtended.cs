using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Montcrest.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RecruitExtended : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReviewdOn",
                table: "JobApplications",
                newName: "ReviewedOn");

            migrationBuilder.RenameColumn(
                name: "HrMarks",
                table: "JobApplications",
                newName: "HrRemarks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedOn",
                table: "Users",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "ExamResult",
                table: "JobApplications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReviewedOn",
                table: "JobApplications",
                newName: "ReviewdOn");

            migrationBuilder.RenameColumn(
                name: "HrRemarks",
                table: "JobApplications",
                newName: "HrMarks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedOn",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExamResult",
                table: "JobApplications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
