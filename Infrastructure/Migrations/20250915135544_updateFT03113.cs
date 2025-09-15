using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateFT03113 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "API_DM_HoDT_Total",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_Doi95_Total",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_DongBan_Total",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_KaTum_Total",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_LocNinh_Total",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_LocThanh_Total",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_LocThien_Total",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_MinhHoa_Total",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_MinhTam_Total",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_TanHa_Total",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_TanHoa1_Total",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_TanHoa2_Total",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_TanThanh_Total",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_ThanhLuong_Total",
                table: "FT03",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "API_DM_HoDT_Total",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_Doi95_Total",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_DongBan_Total",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_KaTum_Total",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_LocNinh_Total",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_LocThanh_Total",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_LocThien_Total",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_MinhHoa_Total",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_MinhTam_Total",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_TanHa_Total",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_TanHoa1_Total",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_TanHoa2_Total",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_TanThanh_Total",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_ThanhLuong_Total",
                table: "FT03");
        }
    }
}
