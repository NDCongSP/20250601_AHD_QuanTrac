using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTableFt07_ParamettersForCalcular : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Close_Door1",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Close_Door2",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Close_Door3",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Close_Door4",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Close_Door5",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Close_Door6",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Close_Doorlock1",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Close_Doorlock2",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Close_Doorlock3",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Close_Doorlock4",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Close_Doorlock5",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Close_Doorlock6",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door3_Aperture",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door3_Close",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door3_Closing",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door3_Open",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door3_Opening",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door3_PressureHigh",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door3_PressureLow",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door4_Aperture",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door4_Close",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door4_Closing",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door4_Open",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door4_Opening",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door4_PressureHigh",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door4_PressureLow",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door5_Aperture",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door5_Close",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door5_Closing",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door5_Open",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door5_Opening",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door5_PressureHigh",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door5_PressureLow",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door6_Aperture",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door6_Close",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door6_Closing",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door6_Open",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door6_Opening",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door6_PressureHigh",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door6_PressureLow",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock3_1Close",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock3_1Open",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock3_2Close",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock3_2Open",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock3_Closing",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock3_Opening",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock4_1Close",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock4_1Open",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock4_2Close",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock4_2Open",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock4_Closing",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock4_Opening",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock5_1Close",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock5_1Open",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock5_2Close",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock5_2Open",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock5_Closing",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock5_Opening",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock6_1Close",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock6_1Open",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock6_2Close",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock6_2Open",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock6_Closing",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Doorlock6_Opening",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Fllow_BenSuc",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Fllow_DauTieng",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Fllow_Door3",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Fllow_Door4",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Fllow_Door5",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Fllow_Door6",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "HT_Cylinder3_1",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "HT_Cylinder3_2",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "HT_Cylinder4_1",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "HT_Cylinder4_2",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "HT_Cylinder5_1",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "HT_Cylinder5_2",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "HT_Cylinder6_1",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "HT_Cylinder6_2",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Open_Door1",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Open_Door2",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Open_Door3",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Open_Door4",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Open_Door5",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Open_Door6",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Open_Doorlock1",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Open_Doorlock2",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Open_Doorlock3",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Open_Doorlock4",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Open_Doorlock5",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Open_Doorlock6",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Run_Pump1",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Run_Pump2",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Setting_Door1_Aperture",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Setting_Door2_Aperture",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Setting_Door3_Aperture",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Setting_Door4_Aperture",
                table: "FT03");

            migrationBuilder.RenameColumn(
                name: "Temp_Oil",
                table: "FT03",
                newName: "S1_Temp_Oil");

            migrationBuilder.RenameColumn(
                name: "Stop_Remote",
                table: "FT03",
                newName: "Al_Door2");

            migrationBuilder.RenameColumn(
                name: "Setting_Door6_Aperture",
                table: "FT03",
                newName: "Pressure_Oil_Door2");

            migrationBuilder.RenameColumn(
                name: "Setting_Door5_Aperture",
                table: "FT03",
                newName: "Pressure_Oil_Door1");

            migrationBuilder.RenameColumn(
                name: "Run_Pump3",
                table: "FT03",
                newName: "Al_Door1");

            migrationBuilder.AlterColumn<string>(
                name: "Fllow_SonDai",
                table: "FT03",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fllow_BinhNham",
                table: "FT03",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fllow_BinhNham2",
                table: "FT03",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fllow_HL_TXL",
                table: "FT03",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fllow_TL_CDD",
                table: "FT03",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Flow_BenSuc",
                table: "FT03",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Flow_DauTieng",
                table: "FT03",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LogBaseInterval",
                table: "FT03",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "LuuLuong",
                table: "FT03",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LuuLuongTong",
                table: "FT03",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "FT03",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Q_Den",
                table: "FT03",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Q_Di",
                table: "FT03",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "W_Ho",
                table: "FT03",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "FT07",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    C000 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateOperatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateOperatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FT07", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FT07");

            migrationBuilder.DropColumn(
                name: "Fllow_BinhNham",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Fllow_BinhNham2",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Fllow_HL_TXL",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Fllow_TL_CDD",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Flow_BenSuc",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Flow_DauTieng",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "LogBaseInterval",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "LuuLuong",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "LuuLuongTong",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Q_Den",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Q_Di",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "W_Ho",
                table: "FT03");

            migrationBuilder.RenameColumn(
                name: "S1_Temp_Oil",
                table: "FT03",
                newName: "Temp_Oil");

            migrationBuilder.RenameColumn(
                name: "Pressure_Oil_Door2",
                table: "FT03",
                newName: "Setting_Door6_Aperture");

            migrationBuilder.RenameColumn(
                name: "Pressure_Oil_Door1",
                table: "FT03",
                newName: "Setting_Door5_Aperture");

            migrationBuilder.RenameColumn(
                name: "Al_Door2",
                table: "FT03",
                newName: "Stop_Remote");

            migrationBuilder.RenameColumn(
                name: "Al_Door1",
                table: "FT03",
                newName: "Run_Pump3");

            migrationBuilder.AlterColumn<int>(
                name: "Fllow_SonDai",
                table: "FT03",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Close_Door1",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Close_Door2",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Close_Door3",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Close_Door4",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Close_Door5",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Close_Door6",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Close_Doorlock1",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Close_Doorlock2",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Close_Doorlock3",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Close_Doorlock4",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Close_Doorlock5",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Close_Doorlock6",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door3_Aperture",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door3_Close",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door3_Closing",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door3_Open",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door3_Opening",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door3_PressureHigh",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door3_PressureLow",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door4_Aperture",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door4_Close",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door4_Closing",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door4_Open",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door4_Opening",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door4_PressureHigh",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door4_PressureLow",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door5_Aperture",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door5_Close",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door5_Closing",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door5_Open",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door5_Opening",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door5_PressureHigh",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door5_PressureLow",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door6_Aperture",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door6_Close",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door6_Closing",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door6_Open",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door6_Opening",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door6_PressureHigh",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Door6_PressureLow",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock3_1Close",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock3_1Open",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock3_2Close",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock3_2Open",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock3_Closing",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock3_Opening",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock4_1Close",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock4_1Open",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock4_2Close",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock4_2Open",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock4_Closing",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock4_Opening",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock5_1Close",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock5_1Open",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock5_2Close",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock5_2Open",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock5_Closing",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock5_Opening",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock6_1Close",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock6_1Open",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock6_2Close",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock6_2Open",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock6_Closing",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Doorlock6_Opening",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Fllow_BenSuc",
                table: "FT03",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Fllow_DauTieng",
                table: "FT03",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Fllow_Door3",
                table: "FT03",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Fllow_Door4",
                table: "FT03",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Fllow_Door5",
                table: "FT03",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Fllow_Door6",
                table: "FT03",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HT_Cylinder3_1",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HT_Cylinder3_2",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HT_Cylinder4_1",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HT_Cylinder4_2",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HT_Cylinder5_1",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HT_Cylinder5_2",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HT_Cylinder6_1",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HT_Cylinder6_2",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Open_Door1",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Open_Door2",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Open_Door3",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Open_Door4",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Open_Door5",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Open_Door6",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Open_Doorlock1",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Open_Doorlock2",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Open_Doorlock3",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Open_Doorlock4",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Open_Doorlock5",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Open_Doorlock6",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Run_Pump1",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Run_Pump2",
                table: "FT03",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Setting_Door1_Aperture",
                table: "FT03",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Setting_Door2_Aperture",
                table: "FT03",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Setting_Door3_Aperture",
                table: "FT03",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Setting_Door4_Aperture",
                table: "FT03",
                type: "int",
                nullable: true);
        }
    }
}
