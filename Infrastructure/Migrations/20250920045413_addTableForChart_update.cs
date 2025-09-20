using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addTableForChart_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FT05",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateOperatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateOperatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    X_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CTDD = table.Column<double>(type: "float", nullable: true),
                    MNKT = table.Column<double>(type: "float", nullable: true),
                    MNTK = table.Column<double>(type: "float", nullable: true),
                    MNDBT = table.Column<double>(type: "float", nullable: true),
                    DPL = table.Column<double>(type: "float", nullable: true),
                    DPPH = table.Column<double>(type: "float", nullable: true),
                    HCCN = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FT05", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FT06",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Z = table.Column<int>(type: "int", nullable: true),
                    Col00 = table.Column<double>(type: "float", nullable: true),
                    Col01 = table.Column<double>(type: "float", nullable: true),
                    Col02 = table.Column<double>(type: "float", nullable: true),
                    Col03 = table.Column<double>(type: "float", nullable: true),
                    Col04 = table.Column<double>(type: "float", nullable: true),
                    Col05 = table.Column<double>(type: "float", nullable: true),
                    Col06 = table.Column<double>(type: "float", nullable: true),
                    Col07 = table.Column<double>(type: "float", nullable: true),
                    Col08 = table.Column<double>(type: "float", nullable: true),
                    Col09 = table.Column<double>(type: "float", nullable: true),
                    Col10 = table.Column<double>(type: "float", nullable: true),
                    Col11 = table.Column<double>(type: "float", nullable: true),
                    Col12 = table.Column<double>(type: "float", nullable: true),
                    Col13 = table.Column<double>(type: "float", nullable: true),
                    Col14 = table.Column<double>(type: "float", nullable: true),
                    Col15 = table.Column<double>(type: "float", nullable: true),
                    Col16 = table.Column<double>(type: "float", nullable: true),
                    Col17 = table.Column<double>(type: "float", nullable: true),
                    Col18 = table.Column<double>(type: "float", nullable: true),
                    Col19 = table.Column<double>(type: "float", nullable: true),
                    Col20 = table.Column<double>(type: "float", nullable: true),
                    Col21 = table.Column<double>(type: "float", nullable: true),
                    Col22 = table.Column<double>(type: "float", nullable: true),
                    Col23 = table.Column<double>(type: "float", nullable: true),
                    Col24 = table.Column<double>(type: "float", nullable: true),
                    Col25 = table.Column<double>(type: "float", nullable: true),
                    Col26 = table.Column<double>(type: "float", nullable: true),
                    Col27 = table.Column<double>(type: "float", nullable: true),
                    Col28 = table.Column<double>(type: "float", nullable: true),
                    Col29 = table.Column<double>(type: "float", nullable: true),
                    Col30 = table.Column<double>(type: "float", nullable: true),
                    Col31 = table.Column<double>(type: "float", nullable: true),
                    Col32 = table.Column<double>(type: "float", nullable: true),
                    Col33 = table.Column<double>(type: "float", nullable: true),
                    Col34 = table.Column<double>(type: "float", nullable: true),
                    Col35 = table.Column<double>(type: "float", nullable: true),
                    Col36 = table.Column<double>(type: "float", nullable: true),
                    Col37 = table.Column<double>(type: "float", nullable: true),
                    Col38 = table.Column<double>(type: "float", nullable: true),
                    Col39 = table.Column<double>(type: "float", nullable: true),
                    Col40 = table.Column<double>(type: "float", nullable: true),
                    Col41 = table.Column<double>(type: "float", nullable: true),
                    Col42 = table.Column<double>(type: "float", nullable: true),
                    Col43 = table.Column<double>(type: "float", nullable: true),
                    Col44 = table.Column<double>(type: "float", nullable: true),
                    Col45 = table.Column<double>(type: "float", nullable: true),
                    Col46 = table.Column<double>(type: "float", nullable: true),
                    Col47 = table.Column<double>(type: "float", nullable: true),
                    Col48 = table.Column<double>(type: "float", nullable: true),
                    Col49 = table.Column<double>(type: "float", nullable: true),
                    Col50 = table.Column<double>(type: "float", nullable: true),
                    Col51 = table.Column<double>(type: "float", nullable: true),
                    Col52 = table.Column<double>(type: "float", nullable: true),
                    Col53 = table.Column<double>(type: "float", nullable: true),
                    Col54 = table.Column<double>(type: "float", nullable: true),
                    Col55 = table.Column<double>(type: "float", nullable: true),
                    Col56 = table.Column<double>(type: "float", nullable: true),
                    Col57 = table.Column<double>(type: "float", nullable: true),
                    Col58 = table.Column<double>(type: "float", nullable: true),
                    Col59 = table.Column<double>(type: "float", nullable: true),
                    Col60 = table.Column<double>(type: "float", nullable: true),
                    Col61 = table.Column<double>(type: "float", nullable: true),
                    Col62 = table.Column<double>(type: "float", nullable: true),
                    Col63 = table.Column<double>(type: "float", nullable: true),
                    Col64 = table.Column<double>(type: "float", nullable: true),
                    Col65 = table.Column<double>(type: "float", nullable: true),
                    Col66 = table.Column<double>(type: "float", nullable: true),
                    Col67 = table.Column<double>(type: "float", nullable: true),
                    Col68 = table.Column<double>(type: "float", nullable: true),
                    Col69 = table.Column<double>(type: "float", nullable: true),
                    Col70 = table.Column<double>(type: "float", nullable: true),
                    Col71 = table.Column<double>(type: "float", nullable: true),
                    Col72 = table.Column<double>(type: "float", nullable: true),
                    Col73 = table.Column<double>(type: "float", nullable: true),
                    Col74 = table.Column<double>(type: "float", nullable: true),
                    Col75 = table.Column<double>(type: "float", nullable: true),
                    Col76 = table.Column<double>(type: "float", nullable: true),
                    Col77 = table.Column<double>(type: "float", nullable: true),
                    Col78 = table.Column<double>(type: "float", nullable: true),
                    Col79 = table.Column<double>(type: "float", nullable: true),
                    Col80 = table.Column<double>(type: "float", nullable: true),
                    Col81 = table.Column<double>(type: "float", nullable: true),
                    Col82 = table.Column<double>(type: "float", nullable: true),
                    Col83 = table.Column<double>(type: "float", nullable: true),
                    Col84 = table.Column<double>(type: "float", nullable: true),
                    Col85 = table.Column<double>(type: "float", nullable: true),
                    Col86 = table.Column<double>(type: "float", nullable: true),
                    Col87 = table.Column<double>(type: "float", nullable: true),
                    Col88 = table.Column<double>(type: "float", nullable: true),
                    Col89 = table.Column<double>(type: "float", nullable: true),
                    Col90 = table.Column<double>(type: "float", nullable: true),
                    Col91 = table.Column<double>(type: "float", nullable: true),
                    Col92 = table.Column<double>(type: "float", nullable: true),
                    Col93 = table.Column<double>(type: "float", nullable: true),
                    Col94 = table.Column<double>(type: "float", nullable: true),
                    Col95 = table.Column<double>(type: "float", nullable: true),
                    Col96 = table.Column<double>(type: "float", nullable: true),
                    Col97 = table.Column<double>(type: "float", nullable: true),
                    Col98 = table.Column<double>(type: "float", nullable: true),
                    Col99 = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FT06", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FT07",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoPhai = table.Column<double>(type: "float", nullable: true),
                    BoTrai = table.Column<double>(type: "float", nullable: true),
                    Q300 = table.Column<double>(type: "float", nullable: true),
                    Q400 = table.Column<double>(type: "float", nullable: true),
                    Q600 = table.Column<double>(type: "float", nullable: true),
                    Q2800 = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FT07", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FT05");

            migrationBuilder.DropTable(
                name: "FT06");

            migrationBuilder.DropTable(
                name: "FT07");
        }
    }
}
