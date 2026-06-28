using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityFT403Them4TagChoAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Q_cua1",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Q_cua2",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Q_cua3",
                table: "FT03",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Q_k5_700",
                table: "FT03",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Q_cua1",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Q_cua2",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Q_cua3",
                table: "FT03");

            migrationBuilder.DropColumn(
                name: "Q_k5_700",
                table: "FT03");
        }
    }
}
