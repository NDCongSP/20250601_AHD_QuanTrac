using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateFT03112 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fllow_TL_CDD",
                table: "FT03",
                newName: "API_Fllow_TL_CDD");

            migrationBuilder.RenameColumn(
                name: "Fllow_SonDai",
                table: "FT03",
                newName: "API_Fllow_SonDai");

            migrationBuilder.RenameColumn(
                name: "Fllow_HL_TXL",
                table: "FT03",
                newName: "API_Fllow_HL_TXL");

            migrationBuilder.RenameColumn(
                name: "Fllow_DauTieng",
                table: "FT03",
                newName: "API_Fllow_DauTieng");

            migrationBuilder.RenameColumn(
                name: "Fllow_BinhNham2",
                table: "FT03",
                newName: "API_Fllow_BinhNham2");

            migrationBuilder.RenameColumn(
                name: "Fllow_BinhNham",
                table: "FT03",
                newName: "API_Fllow_BinhNham");

            migrationBuilder.RenameColumn(
                name: "Fllow_BenSuc",
                table: "FT03",
                newName: "API_Fllow_BenSuc");

            migrationBuilder.AlterColumn<double>(
                name: "Q_i",
                table: "FT03",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<double>(
                name: "Q_i_total",
                table: "FT03",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Q_i_total",
                table: "FT03");

            migrationBuilder.RenameColumn(
                name: "API_Fllow_TL_CDD",
                table: "FT03",
                newName: "Fllow_TL_CDD");

            migrationBuilder.RenameColumn(
                name: "API_Fllow_SonDai",
                table: "FT03",
                newName: "Fllow_SonDai");

            migrationBuilder.RenameColumn(
                name: "API_Fllow_HL_TXL",
                table: "FT03",
                newName: "Fllow_HL_TXL");

            migrationBuilder.RenameColumn(
                name: "API_Fllow_DauTieng",
                table: "FT03",
                newName: "Fllow_DauTieng");

            migrationBuilder.RenameColumn(
                name: "API_Fllow_BinhNham2",
                table: "FT03",
                newName: "Fllow_BinhNham2");

            migrationBuilder.RenameColumn(
                name: "API_Fllow_BinhNham",
                table: "FT03",
                newName: "Fllow_BinhNham");

            migrationBuilder.RenameColumn(
                name: "API_Fllow_BenSuc",
                table: "FT03",
                newName: "Fllow_BenSuc");

            migrationBuilder.AlterColumn<double>(
                name: "Q_i",
                table: "FT03",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
