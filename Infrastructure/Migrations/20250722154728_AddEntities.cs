using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FT01",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    C000 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C001 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateOperatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateOperatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FT01", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FT02",
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
                    table.PrimaryKey("PK_FT02", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FT03",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    StationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateOperatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateOperatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    Remote = table.Column<bool>(type: "bit", nullable: true),
                    Local = table.Column<bool>(type: "bit", nullable: true),
                    Auto = table.Column<bool>(type: "bit", nullable: true),
                    Man = table.Column<bool>(type: "bit", nullable: true),
                    Local_Stop = table.Column<bool>(type: "bit", nullable: true),
                    DC1_Running = table.Column<bool>(type: "bit", nullable: true),
                    DC2_Running = table.Column<bool>(type: "bit", nullable: true),
                    DC3_Running = table.Column<bool>(type: "bit", nullable: true),
                    Door1_Opening = table.Column<bool>(type: "bit", nullable: true),
                    Door1_Closing = table.Column<bool>(type: "bit", nullable: true),
                    Door2_Opening = table.Column<bool>(type: "bit", nullable: true),
                    Door2_Closing = table.Column<bool>(type: "bit", nullable: true),
                    Door3_Opening = table.Column<bool>(type: "bit", nullable: true),
                    Door3_Closing = table.Column<bool>(type: "bit", nullable: true),
                    Door4_Opening = table.Column<bool>(type: "bit", nullable: true),
                    Door4_Closing = table.Column<bool>(type: "bit", nullable: true),
                    Door5_Opening = table.Column<bool>(type: "bit", nullable: true),
                    Door5_Closing = table.Column<bool>(type: "bit", nullable: true),
                    Door6_Opening = table.Column<bool>(type: "bit", nullable: true),
                    Door6_Closing = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock1_Opening = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock1_Closing = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock2_Opening = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock2_Closing = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock3_Opening = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock3_Closing = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock4_Opening = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock4_Closing = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock5_Opening = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock5_Closing = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock6_Opening = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock6_Closing = table.Column<bool>(type: "bit", nullable: true),
                    Door1_Open = table.Column<bool>(type: "bit", nullable: true),
                    Door1_Close = table.Column<bool>(type: "bit", nullable: true),
                    Door2_Open = table.Column<bool>(type: "bit", nullable: true),
                    Door2_Close = table.Column<bool>(type: "bit", nullable: true),
                    Door3_Open = table.Column<bool>(type: "bit", nullable: true),
                    Door3_Close = table.Column<bool>(type: "bit", nullable: true),
                    Door4_Open = table.Column<bool>(type: "bit", nullable: true),
                    Door4_Close = table.Column<bool>(type: "bit", nullable: true),
                    Door5_Open = table.Column<bool>(type: "bit", nullable: true),
                    Door5_Close = table.Column<bool>(type: "bit", nullable: true),
                    Door6_Open = table.Column<bool>(type: "bit", nullable: true),
                    Door6_Close = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock1_1Open = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock1_1Close = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock1_2Open = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock1_2Close = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock2_1Open = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock2_1Close = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock2_2Open = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock2_2Close = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock3_1Open = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock3_1Close = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock3_2Open = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock3_2Close = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock4_1Open = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock4_1Close = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock4_2Open = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock4_2Close = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock5_1Open = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock5_1Close = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock5_2Open = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock5_2Close = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock6_1Open = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock6_1Close = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock6_2Open = table.Column<bool>(type: "bit", nullable: true),
                    Doorlock6_2Close = table.Column<bool>(type: "bit", nullable: true),
                    DC1_Over = table.Column<bool>(type: "bit", nullable: true),
                    DC2_Over = table.Column<bool>(type: "bit", nullable: true),
                    DC3_Over = table.Column<bool>(type: "bit", nullable: true),
                    Door1_PressureHigh = table.Column<bool>(type: "bit", nullable: true),
                    Door1_PressureLow = table.Column<bool>(type: "bit", nullable: true),
                    Door2_PressureHigh = table.Column<bool>(type: "bit", nullable: true),
                    Door2_PressureLow = table.Column<bool>(type: "bit", nullable: true),
                    Door3_PressureHigh = table.Column<bool>(type: "bit", nullable: true),
                    Door3_PressureLow = table.Column<bool>(type: "bit", nullable: true),
                    Door4_PressureHigh = table.Column<bool>(type: "bit", nullable: true),
                    Door4_PressureLow = table.Column<bool>(type: "bit", nullable: true),
                    Door5_PressureHigh = table.Column<bool>(type: "bit", nullable: true),
                    Door5_PressureLow = table.Column<bool>(type: "bit", nullable: true),
                    Door6_PressureHigh = table.Column<bool>(type: "bit", nullable: true),
                    Door6_PressureLow = table.Column<bool>(type: "bit", nullable: true),
                    HT_Cylinder1_1 = table.Column<bool>(type: "bit", nullable: true),
                    HT_Cylinder1_2 = table.Column<bool>(type: "bit", nullable: true),
                    HT_Cylinder2_1 = table.Column<bool>(type: "bit", nullable: true),
                    HT_Cylinder2_2 = table.Column<bool>(type: "bit", nullable: true),
                    HT_Cylinder3_1 = table.Column<bool>(type: "bit", nullable: true),
                    HT_Cylinder3_2 = table.Column<bool>(type: "bit", nullable: true),
                    HT_Cylinder4_1 = table.Column<bool>(type: "bit", nullable: true),
                    HT_Cylinder4_2 = table.Column<bool>(type: "bit", nullable: true),
                    HT_Cylinder5_1 = table.Column<bool>(type: "bit", nullable: true),
                    HT_Cylinder5_2 = table.Column<bool>(type: "bit", nullable: true),
                    HT_Cylinder6_1 = table.Column<bool>(type: "bit", nullable: true),
                    HT_Cylinder6_2 = table.Column<bool>(type: "bit", nullable: true),
                    Door1_Aperture = table.Column<bool>(type: "bit", nullable: true),
                    Door2_Aperture = table.Column<bool>(type: "bit", nullable: true),
                    Door3_Aperture = table.Column<bool>(type: "bit", nullable: true),
                    Door4_Aperture = table.Column<bool>(type: "bit", nullable: true),
                    Door5_Aperture = table.Column<bool>(type: "bit", nullable: true),
                    Door6_Aperture = table.Column<bool>(type: "bit", nullable: true),
                    Temp_Oil = table.Column<int>(type: "int", nullable: true),
                    Run_Pump1 = table.Column<bool>(type: "bit", nullable: true),
                    Run_Pump2 = table.Column<bool>(type: "bit", nullable: true),
                    Run_Pump3 = table.Column<bool>(type: "bit", nullable: true),
                    Open_Door1 = table.Column<bool>(type: "bit", nullable: true),
                    Close_Door1 = table.Column<bool>(type: "bit", nullable: true),
                    Open_Door2 = table.Column<bool>(type: "bit", nullable: true),
                    Close_Door2 = table.Column<bool>(type: "bit", nullable: true),
                    Open_Door3 = table.Column<bool>(type: "bit", nullable: true),
                    Close_Door3 = table.Column<bool>(type: "bit", nullable: true),
                    Open_Door4 = table.Column<bool>(type: "bit", nullable: true),
                    Close_Door4 = table.Column<bool>(type: "bit", nullable: true),
                    Open_Door5 = table.Column<bool>(type: "bit", nullable: true),
                    Close_Door5 = table.Column<bool>(type: "bit", nullable: true),
                    Open_Door6 = table.Column<bool>(type: "bit", nullable: true),
                    Close_Door6 = table.Column<bool>(type: "bit", nullable: true),
                    Open_Doorlock1 = table.Column<bool>(type: "bit", nullable: true),
                    Close_Doorlock1 = table.Column<bool>(type: "bit", nullable: true),
                    Open_Doorlock2 = table.Column<bool>(type: "bit", nullable: true),
                    Close_Doorlock2 = table.Column<bool>(type: "bit", nullable: true),
                    Open_Doorlock3 = table.Column<bool>(type: "bit", nullable: true),
                    Close_Doorlock3 = table.Column<bool>(type: "bit", nullable: true),
                    Open_Doorlock4 = table.Column<bool>(type: "bit", nullable: true),
                    Close_Doorlock4 = table.Column<bool>(type: "bit", nullable: true),
                    Open_Doorlock5 = table.Column<bool>(type: "bit", nullable: true),
                    Close_Doorlock5 = table.Column<bool>(type: "bit", nullable: true),
                    Open_Doorlock6 = table.Column<bool>(type: "bit", nullable: true),
                    Close_Doorlock6 = table.Column<bool>(type: "bit", nullable: true),
                    Stop_Remote = table.Column<bool>(type: "bit", nullable: true),
                    Setting_Door1_Aperture = table.Column<int>(type: "int", nullable: true),
                    Setting_Door2_Aperture = table.Column<int>(type: "int", nullable: true),
                    Setting_Door3_Aperture = table.Column<int>(type: "int", nullable: true),
                    Setting_Door4_Aperture = table.Column<int>(type: "int", nullable: true),
                    Setting_Door5_Aperture = table.Column<int>(type: "int", nullable: true),
                    Setting_Door6_Aperture = table.Column<int>(type: "int", nullable: true),
                    Fllow_Door1 = table.Column<int>(type: "int", nullable: true),
                    Fllow_Door2 = table.Column<int>(type: "int", nullable: true),
                    Fllow_Door3 = table.Column<int>(type: "int", nullable: true),
                    Fllow_Door4 = table.Column<int>(type: "int", nullable: true),
                    Fllow_Door5 = table.Column<int>(type: "int", nullable: true),
                    Fllow_Door6 = table.Column<int>(type: "int", nullable: true),
                    Total_Fllow = table.Column<int>(type: "int", nullable: true),
                    Fllow_Ho = table.Column<int>(type: "int", nullable: true),
                    Fllow_DauTieng = table.Column<int>(type: "int", nullable: true),
                    Fllow_BenSuc = table.Column<int>(type: "int", nullable: true),
                    Fllow_SonDai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FT03", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FT05",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OvenId = table.Column<int>(type: "int", nullable: false),
                    OvenName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    ProfileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StepId = table.Column<int>(type: "int", nullable: false),
                    StepName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Setpoint = table.Column<double>(type: "float", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    Minutes = table.Column<int>(type: "int", nullable: false),
                    Seconds = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ACK = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ACKDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Actived = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateOperatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateOperatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FT05", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FT06",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    C000 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateOperatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateOperatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FT06", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FT01");

            migrationBuilder.DropTable(
                name: "FT02");

            migrationBuilder.DropTable(
                name: "FT03");

            migrationBuilder.DropTable(
                name: "FT05");

            migrationBuilder.DropTable(
                name: "FT06");
        }
    }
}
