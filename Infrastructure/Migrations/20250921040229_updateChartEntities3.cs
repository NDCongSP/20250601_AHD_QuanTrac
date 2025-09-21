using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateChartEntities3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Z_Thuc",
                table: "FT07",
                newName: "Z_Thuc_Y");

            migrationBuilder.AddColumn<string>(
                name: "Z_Thuc_X",
                table: "FT07",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Z_Thuc_X",
                table: "FT07");

            migrationBuilder.RenameColumn(
                name: "Z_Thuc_Y",
                table: "FT07",
                newName: "Z_Thuc");
        }
    }
}
