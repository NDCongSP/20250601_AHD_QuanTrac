using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateFT032 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Door1_Aperture_Final",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Door1_Aperture_Offset",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Door2_Aperture_Final",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Door2_Aperture_Offset",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Fllow_Door1_Final",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Fllow_Door1_Offset",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Fllow_Door2_Final",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Fllow_Door2_Offset",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Fllow_Ho_Final",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Fllow_Ho_Offset",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "HT_Cylinder1_1_Final",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "HT_Cylinder1_1_Offset",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "HT_Cylinder1_2_Final",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "HT_Cylinder1_2_Offset",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "HT_Cylinder2_1_Final",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "HT_Cylinder2_1_Offset",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "HT_Cylinder2_2_Final",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "HT_Cylinder2_2_Offset",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Pressure_Oil_Door1_Final",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Pressure_Oil_Door1_Offset",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Pressure_Oil_Door2_Final",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Pressure_Oil_Door2_Offset",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "S1_Temp_Oil_Final",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "S1_Temp_Oil_Offset",
                table: "FT03",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Door1_Aperture_Final",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door1_Aperture_Offset",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door2_Aperture_Final",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Door2_Aperture_Offset",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Fllow_Door1_Final",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Fllow_Door1_Offset",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Fllow_Door2_Final",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Fllow_Door2_Offset",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Fllow_Ho_Final",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Fllow_Ho_Offset",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "HT_Cylinder1_1_Final",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "HT_Cylinder1_1_Offset",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "HT_Cylinder1_2_Final",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "HT_Cylinder1_2_Offset",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "HT_Cylinder2_1_Final",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "HT_Cylinder2_1_Offset",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "HT_Cylinder2_2_Final",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "HT_Cylinder2_2_Offset",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Pressure_Oil_Door1_Final",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Pressure_Oil_Door1_Offset",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Pressure_Oil_Door2_Final",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Pressure_Oil_Door2_Offset",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "S1_Temp_Oil_Final",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "S1_Temp_Oil_Offset",
                table: "FT03");
        }
    }
}
