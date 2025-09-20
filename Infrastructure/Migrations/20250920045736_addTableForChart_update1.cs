using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addTableForChart_update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "FT05");

            migrationBuilder.DropColumn(
                name: "CreateOperatorId",
                table: "FT05");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FT05");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "FT05");

            migrationBuilder.DropColumn(
                name: "UpdateOperatorId",
                table: "FT05");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "FT05",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateOperatorId",
                table: "FT05",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FT05",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "FT05",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateOperatorId",
                table: "FT05",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
