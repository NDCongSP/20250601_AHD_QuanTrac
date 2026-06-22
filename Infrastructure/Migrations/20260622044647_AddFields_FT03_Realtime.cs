using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFields_FT03_Realtime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string[] cols = new[] { "Z2_CS2", "Z2_CS1", "Qc_CS2", "Qc_CS1", "DoMo_Cua3", "DoMo_Cua2", "DoMo_Cua1", "Ap2_CS2", "Ap2_CS1", "Ap1_CS2", "Ap1_CS1", "A2_CS2", "A1_CS1", "A1_CS2", "A2_CS1" };
            foreach (var c in cols)
            {
                migrationBuilder.AddColumn<double>(
                    name: c,
                    table: "FT03",
                    type: "float",
                    nullable: true);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string[] cols = new[] { "Z2_CS2", "Z2_CS1", "Qc_CS2", "Qc_CS1", "DoMo_Cua3", "DoMo_Cua2", "DoMo_Cua1", "Ap2_CS2", "Ap2_CS1", "Ap1_CS2", "Ap1_CS1", "A2_CS2", "A1_CS1", "A1_CS2", "A2_CS1" };
            foreach (var c in cols)
            {
                migrationBuilder.DropColumn(
                    name: c,
                    table: "FT03");
            }
        }
    }
}
