using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    country_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ranking_Systems",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    system_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ranking_Systems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "universities",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    university_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_universities", x => x.id);
                    table.ForeignKey(
                        name: "FK_universities_countries_country_id",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ranking_Criterias",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ranking_system_id = table.Column<int>(type: "integer", nullable: false),
                    criteria_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ranking_Criterias", x => x.id);
                    table.ForeignKey(
                        name: "FK_ranking_Criterias_ranking_Systems_ranking_system_id",
                        column: x => x.ranking_system_id,
                        principalTable: "ranking_Systems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "university_Years",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    university_id = table.Column<int>(type: "integer", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    num_students = table.Column<int>(type: "integer", nullable: false),
                    student_staff_ratio = table.Column<int>(type: "integer", nullable: false),
                    pct_international_students = table.Column<int>(type: "integer", nullable: false),
                    pct_female_students = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_university_Years", x => x.Id);
                    table.ForeignKey(
                        name: "FK_university_Years_universities_university_id",
                        column: x => x.university_id,
                        principalTable: "universities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "university_rankings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    university_id = table.Column<int>(type: "integer", nullable: false),
                    ranking_criteria_id = table.Column<int>(type: "integer", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    score = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_university_rankings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_university_rankings_ranking_Criterias_ranking_criteria_id",
                        column: x => x.ranking_criteria_id,
                        principalTable: "ranking_Criterias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_university_rankings_universities_university_id",
                        column: x => x.university_id,
                        principalTable: "universities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ranking_Criterias_ranking_system_id",
                table: "ranking_Criterias",
                column: "ranking_system_id");

            migrationBuilder.CreateIndex(
                name: "IX_universities_country_id",
                table: "universities",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_university_rankings_ranking_criteria_id",
                table: "university_rankings",
                column: "ranking_criteria_id");

            migrationBuilder.CreateIndex(
                name: "IX_university_rankings_university_id",
                table: "university_rankings",
                column: "university_id");

            migrationBuilder.CreateIndex(
                name: "IX_university_Years_university_id",
                table: "university_Years",
                column: "university_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "university_rankings");

            migrationBuilder.DropTable(
                name: "university_Years");

            migrationBuilder.DropTable(
                name: "ranking_Criterias");

            migrationBuilder.DropTable(
                name: "universities");

            migrationBuilder.DropTable(
                name: "ranking_Systems");

            migrationBuilder.DropTable(
                name: "countries");
        }
    }
}
