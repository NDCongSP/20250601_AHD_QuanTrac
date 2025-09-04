using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDBFT03AndFT04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FT05");

            migrationBuilder.DropColumn(
                name: "Al_Door1",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Al_Door2",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Auto",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "DC1_Over",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "DC1_Running",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "DC2_Over",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "DC2_Running",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "DC3_Over",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "DC3_Running",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door1_Close",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door1_Closing",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door1_Open",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door1_Opening",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door1_PressureHigh",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door1_PressureLow",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door2_Close",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door2_Closing",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door2_Open",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door2_Opening",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door2_PressureHigh",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door2_PressureLow",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock1_1Close",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock1_1Open",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock1_2Close",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock1_2Open",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock1_Closing",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock1_Opening",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock2_1Close",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock2_1Open",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock2_2Close",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock2_2Open",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock2_Closing",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock2_Opening",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Local",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Local_Stop",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Man",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Remote",
                table: "FT03");

            migrationBuilder.AlterColumn<bool>(
                name: "Value",
                table: "FT04",
                type: "bit",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "FT04",
                type: "float",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Al_Door1",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Al_Door2",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Auto",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DC1_Over",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DC1_Running",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DC2_Over",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DC2_Running",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DC3_Over",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DC3_Running",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door1_Close",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door1_Closing",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door1_Open",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door1_Opening",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door1_PressureHigh",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door1_PressureLow",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door2_Close",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door2_Closing",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door2_Open",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door2_Opening",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door2_PressureHigh",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door2_PressureLow",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock1_1Close",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock1_1Open",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock1_2Close",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock1_2Open",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock1_Closing",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock1_Opening",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock2_1Close",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock2_1Open",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock2_2Close",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock2_2Open",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock2_Closing",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock2_Opening",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Local",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Local_Stop",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Man",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Remote",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FT05",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    C000 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateOperatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateOperatorId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FT05", x => x.Id);
                });
        }
    }
}
