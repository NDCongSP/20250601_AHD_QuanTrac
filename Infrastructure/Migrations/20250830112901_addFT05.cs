using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addFT05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FT06",
                table: "FT06");

            migrationBuilder.RenameTable(
                name: "FT06",
                newName: "FT05");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FT05",
                table: "FT05",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FT05",
                table: "FT05");

            migrationBuilder.RenameTable(
                name: "FT05",
                newName: "FT06");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FT06",
                table: "FT06",
                column: "Id");
        }
    }
}
