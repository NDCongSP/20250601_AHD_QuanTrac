using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateEntityFt036 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Q_i",
                table: "FT03",
                newName: "Q_i_2");

            migrationBuilder.AddColumn<double>(
                name: "Q_i_1",
                table: "FT03",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Q_i_1",
                table: "FT03");

            migrationBuilder.RenameColumn(
                name: "Q_i_2",
                table: "FT03",
                newName: "Q_i");
        }
    }
}
