using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateentityAPICHanDapThanhAn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "API_ChanDap",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "API_ThanhAn",
                table: "FT03",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "API_ChanDap",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "API_ThanhAn",
                table: "FT03");
        }
    }
}
