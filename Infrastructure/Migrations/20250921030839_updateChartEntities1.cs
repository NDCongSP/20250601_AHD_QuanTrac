using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateChartEntities1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MNTK",
                table: "FT05",
                newName: "L_MNTK");

            migrationBuilder.RenameColumn(
                name: "MNKT",
                table: "FT05",
                newName: "L_MNKT");

            migrationBuilder.RenameColumn(
                name: "MNDBT",
                table: "FT05",
                newName: "L_MNDBT");

            migrationBuilder.RenameColumn(
                name: "HCCN",
                table: "FT05",
                newName: "L_HCCN");

            migrationBuilder.RenameColumn(
                name: "DPPH",
                table: "FT05",
                newName: "L_DPPH");

            migrationBuilder.RenameColumn(
                name: "DPL",
                table: "FT05",
                newName: "L_DPL");

            migrationBuilder.RenameColumn(
                name: "CTDD",
                table: "FT05",
                newName: "L_CTDD");

            migrationBuilder.AddColumn<double>(
                name: "A_VungA",
                table: "FT05",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "A_VungB",
                table: "FT05",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "A_VungC",
                table: "FT05",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "A_VungA",
                table: "FT05");

            migrationBuilder.DropColumn(
                name: "A_VungB",
                table: "FT05");

            migrationBuilder.DropColumn(
                name: "A_VungC",
                table: "FT05");

            migrationBuilder.RenameColumn(
                name: "L_MNTK",
                table: "FT05",
                newName: "MNTK");

            migrationBuilder.RenameColumn(
                name: "L_MNKT",
                table: "FT05",
                newName: "MNKT");

            migrationBuilder.RenameColumn(
                name: "L_MNDBT",
                table: "FT05",
                newName: "MNDBT");

            migrationBuilder.RenameColumn(
                name: "L_HCCN",
                table: "FT05",
                newName: "HCCN");

            migrationBuilder.RenameColumn(
                name: "L_DPPH",
                table: "FT05",
                newName: "DPPH");

            migrationBuilder.RenameColumn(
                name: "L_DPL",
                table: "FT05",
                newName: "DPL");

            migrationBuilder.RenameColumn(
                name: "L_CTDD",
                table: "FT05",
                newName: "CTDD");
        }
    }
}
