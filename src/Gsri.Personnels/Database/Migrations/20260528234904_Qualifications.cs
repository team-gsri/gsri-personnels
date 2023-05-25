using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gsri.Personnels.Database.Migrations
{
    /// <inheritdoc />
    public partial class Qualifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Competences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Duree = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Joueurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Pseudonyme = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Joueurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Qualifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<Guid>(type: "TEXT", nullable: false),
                    From = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Until = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    JoueurId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompetenceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Qualifications_Competences_CompetenceId",
                        column: x => x.CompetenceId,
                        principalTable: "Competences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Qualifications_Joueurs_JoueurId",
                        column: x => x.JoueurId,
                        principalTable: "Joueurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Competences_Name",
                table: "Competences",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Joueurs_Pseudonyme",
                table: "Joueurs",
                column: "Pseudonyme",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_CompetenceId",
                table: "Qualifications",
                column: "CompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_JoueurId",
                table: "Qualifications",
                column: "JoueurId");

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_Key",
                table: "Qualifications",
                column: "Key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.DropTable(
                name: "Competences");

            migrationBuilder.DropTable(
                name: "Joueurs");
        }
    }
}
