using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateFT03114 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "API_ThanhLuong_Total",
                table: "FT03",
                newName: "API_D_ThanhLuong_Total");

            migrationBuilder.RenameColumn(
                name: "API_ThanhLuong",
                table: "FT03",
                newName: "API_D_ThanhLuong");

            migrationBuilder.RenameColumn(
                name: "API_TanThanh_Total",
                table: "FT03",
                newName: "API_D_TanThanh_Total");

            migrationBuilder.RenameColumn(
                name: "API_TanThanh",
                table: "FT03",
                newName: "API_D_TanThanh");

            migrationBuilder.RenameColumn(
                name: "API_TanHoa2_Total",
                table: "FT03",
                newName: "API_D_TanHoa2_Total");

            migrationBuilder.RenameColumn(
                name: "API_TanHoa2",
                table: "FT03",
                newName: "API_D_TanHoa2");

            migrationBuilder.RenameColumn(
                name: "API_TanHoa1_Total",
                table: "FT03",
                newName: "API_D_TanHoa1_Total");

            migrationBuilder.RenameColumn(
                name: "API_TanHoa1",
                table: "FT03",
                newName: "API_D_TanHoa1");

            migrationBuilder.RenameColumn(
                name: "API_TanHa_Total",
                table: "FT03",
                newName: "API_D_TanHa_Total");

            migrationBuilder.RenameColumn(
                name: "API_TanHa",
                table: "FT03",
                newName: "API_D_TanHa");

            migrationBuilder.RenameColumn(
                name: "API_MinhTam_Total",
                table: "FT03",
                newName: "API_D_MinhTam_Total");

            migrationBuilder.RenameColumn(
                name: "API_MinhTam",
                table: "FT03",
                newName: "API_D_MinhTam");

            migrationBuilder.RenameColumn(
                name: "API_MinhHoa_Total",
                table: "FT03",
                newName: "API_D_MinhHoa_Total");

            migrationBuilder.RenameColumn(
                name: "API_MinhHoa",
                table: "FT03",
                newName: "API_D_MinhHoa");

            migrationBuilder.RenameColumn(
                name: "API_LocThien_Total",
                table: "FT03",
                newName: "API_D_LocThien_Total");

            migrationBuilder.RenameColumn(
                name: "API_LocThien",
                table: "FT03",
                newName: "API_D_LocThien");

            migrationBuilder.RenameColumn(
                name: "API_LocThanh_Total",
                table: "FT03",
                newName: "API_D_LocThanh_Total");

            migrationBuilder.RenameColumn(
                name: "API_LocThanh",
                table: "FT03",
                newName: "API_D_LocThanh");

            migrationBuilder.RenameColumn(
                name: "API_LocNinh_Total",
                table: "FT03",
                newName: "API_D_LocNinh_Total");

            migrationBuilder.RenameColumn(
                name: "API_LocNinh",
                table: "FT03",
                newName: "API_D_LocNinh");

            migrationBuilder.RenameColumn(
                name: "API_KaTum_Total",
                table: "FT03",
                newName: "API_D_KaTum_Total");

            migrationBuilder.RenameColumn(
                name: "API_KaTum",
                table: "FT03",
                newName: "API_D_KaTum");

            migrationBuilder.RenameColumn(
                name: "API_DongBan_Total",
                table: "FT03",
                newName: "API_D_DongBan_Total");

            migrationBuilder.RenameColumn(
                name: "API_DongBan",
                table: "FT03",
                newName: "API_D_DongBan");

            migrationBuilder.RenameColumn(
                name: "API_Doi95_Total",
                table: "FT03",
                newName: "API_D_Doi95_Total");

            migrationBuilder.RenameColumn(
                name: "API_Doi95",
                table: "FT03",
                newName: "API_D_Doi95");

            migrationBuilder.RenameColumn(
                name: "API_DM_HoDT_Total",
                table: "FT03",
                newName: "API_D_DM_HoDT_Total");

            migrationBuilder.RenameColumn(
                name: "API_DM_HoDT",
                table: "FT03",
                newName: "API_D_DM_HoDT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "API_D_ThanhLuong_Total",
                table: "FT03",
                newName: "API_ThanhLuong_Total");

            migrationBuilder.RenameColumn(
                name: "API_D_ThanhLuong",
                table: "FT03",
                newName: "API_ThanhLuong");

            migrationBuilder.RenameColumn(
                name: "API_D_TanThanh_Total",
                table: "FT03",
                newName: "API_TanThanh_Total");

            migrationBuilder.RenameColumn(
                name: "API_D_TanThanh",
                table: "FT03",
                newName: "API_TanThanh");

            migrationBuilder.RenameColumn(
                name: "API_D_TanHoa2_Total",
                table: "FT03",
                newName: "API_TanHoa2_Total");

            migrationBuilder.RenameColumn(
                name: "API_D_TanHoa2",
                table: "FT03",
                newName: "API_TanHoa2");

            migrationBuilder.RenameColumn(
                name: "API_D_TanHoa1_Total",
                table: "FT03",
                newName: "API_TanHoa1_Total");

            migrationBuilder.RenameColumn(
                name: "API_D_TanHoa1",
                table: "FT03",
                newName: "API_TanHoa1");

            migrationBuilder.RenameColumn(
                name: "API_D_TanHa_Total",
                table: "FT03",
                newName: "API_TanHa_Total");

            migrationBuilder.RenameColumn(
                name: "API_D_TanHa",
                table: "FT03",
                newName: "API_TanHa");

            migrationBuilder.RenameColumn(
                name: "API_D_MinhTam_Total",
                table: "FT03",
                newName: "API_MinhTam_Total");

            migrationBuilder.RenameColumn(
                name: "API_D_MinhTam",
                table: "FT03",
                newName: "API_MinhTam");

            migrationBuilder.RenameColumn(
                name: "API_D_MinhHoa_Total",
                table: "FT03",
                newName: "API_MinhHoa_Total");

            migrationBuilder.RenameColumn(
                name: "API_D_MinhHoa",
                table: "FT03",
                newName: "API_MinhHoa");

            migrationBuilder.RenameColumn(
                name: "API_D_LocThien_Total",
                table: "FT03",
                newName: "API_LocThien_Total");

            migrationBuilder.RenameColumn(
                name: "API_D_LocThien",
                table: "FT03",
                newName: "API_LocThien");

            migrationBuilder.RenameColumn(
                name: "API_D_LocThanh_Total",
                table: "FT03",
                newName: "API_LocThanh_Total");

            migrationBuilder.RenameColumn(
                name: "API_D_LocThanh",
                table: "FT03",
                newName: "API_LocThanh");

            migrationBuilder.RenameColumn(
                name: "API_D_LocNinh_Total",
                table: "FT03",
                newName: "API_LocNinh_Total");

            migrationBuilder.RenameColumn(
                name: "API_D_LocNinh",
                table: "FT03",
                newName: "API_LocNinh");

            migrationBuilder.RenameColumn(
                name: "API_D_KaTum_Total",
                table: "FT03",
                newName: "API_KaTum_Total");

            migrationBuilder.RenameColumn(
                name: "API_D_KaTum",
                table: "FT03",
                newName: "API_KaTum");

            migrationBuilder.RenameColumn(
                name: "API_D_DongBan_Total",
                table: "FT03",
                newName: "API_DongBan_Total");

            migrationBuilder.RenameColumn(
                name: "API_D_DongBan",
                table: "FT03",
                newName: "API_DongBan");

            migrationBuilder.RenameColumn(
                name: "API_D_Doi95_Total",
                table: "FT03",
                newName: "API_Doi95_Total");

            migrationBuilder.RenameColumn(
                name: "API_D_Doi95",
                table: "FT03",
                newName: "API_Doi95");

            migrationBuilder.RenameColumn(
                name: "API_D_DM_HoDT_Total",
                table: "FT03",
                newName: "API_DM_HoDT_Total");

            migrationBuilder.RenameColumn(
                name: "API_D_DM_HoDT",
                table: "FT03",
                newName: "API_DM_HoDT");
        }
    }
}
