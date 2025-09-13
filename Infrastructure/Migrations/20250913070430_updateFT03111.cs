using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateFT03111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Q_Di",
                table: "FT03",
                newName: "Q_di");

            migrationBuilder.RenameColumn(
                name: "Q_Den",
                table: "FT03",
                newName: "Q_den");

            migrationBuilder.RenameColumn(
                name: "W_Ho",
                table: "FT03",
                newName: "W_tt");

            migrationBuilder.RenameColumn(
                name: "Total_Fllow",
                table: "FT03",
                newName: "API_ThanhLuong");

            migrationBuilder.RenameColumn(
                name: "Qtr",
                table: "FT03",
                newName: "W_tr");

            migrationBuilder.RenameColumn(
                name: "LuuLuongTong",
                table: "FT03",
                newName: "W_di");

            migrationBuilder.RenameColumn(
                name: "LuuLuong",
                table: "FT03",
                newName: "W_den");

            migrationBuilder.AddColumn<double>(
                name: "API_DM_HoDT",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_Doi95",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_DongBan",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_KaTum",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_LocNinh",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_LocThanh",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_LocThien",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_MinhHoa",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_MinhTam",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_TanHa",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_TanHoa1",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_TanHoa2",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_TanThanh",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Q_cs1",
                table: "FT03",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Q_cs2",
                table: "FT03",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Q_cs3",
                table: "FT03",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Q_denta",
                table: "FT03",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Q_i",
                table: "FT03",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Q_tr",
                table: "FT03",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Q_tt",
                table: "FT03",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "W1_ho",
                table: "FT03",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "W1_ho_old",
                table: "FT03",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "W2_ho",
                table: "FT03",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "W2_ho_old",
                table: "FT03",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "W_cs1",
                table: "FT03",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "W_cs2",
                table: "FT03",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "W_cs3",
                table: "FT03",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "API_DM_HoDT",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_Doi95",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_DongBan",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_KaTum",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_LocNinh",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_LocThanh",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_LocThien",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_MinhHoa",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_MinhTam",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_TanHa",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_TanHoa1",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_TanHoa2",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_TanThanh",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Q_cs1",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Q_cs2",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Q_cs3",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Q_denta",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Q_i",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Q_tr",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Q_tt",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "W1_ho",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "W1_ho_old",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "W2_ho",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "W2_ho_old",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "W_cs1",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "W_cs2",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "W_cs3",
                table: "FT03");

            migrationBuilder.RenameColumn(
                name: "Q_di",
                table: "FT03",
                newName: "Q_Di");

            migrationBuilder.RenameColumn(
                name: "Q_den",
                table: "FT03",
                newName: "Q_Den");

            migrationBuilder.RenameColumn(
                name: "W_tt",
                table: "FT03",
                newName: "W_Ho");

            migrationBuilder.RenameColumn(
                name: "W_tr",
                table: "FT03",
                newName: "Qtr");

            migrationBuilder.RenameColumn(
                name: "W_di",
                table: "FT03",
                newName: "LuuLuongTong");

            migrationBuilder.RenameColumn(
                name: "W_den",
                table: "FT03",
                newName: "LuuLuong");

            migrationBuilder.RenameColumn(
                name: "API_ThanhLuong",
                table: "FT03",
                newName: "Total_Fllow");
        }
    }
}
