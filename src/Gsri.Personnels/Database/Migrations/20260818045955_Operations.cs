using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gsri.Personnels.Database.Migrations
{
    /// <inheritdoc />
    public partial class Operations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    When = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<Guid>(type: "TEXT", nullable: false),
                    OperationId = table.Column<int>(type: "INTEGER", nullable: false),
                    JoueurId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participations_Joueurs_JoueurId",
                        column: x => x.JoueurId,
                        principalTable: "Joueurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participations_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operations_When",
                table: "Operations",
                column: "When",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participations_JoueurId",
                table: "Participations",
                column: "JoueurId");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_Key",
                table: "Participations",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participations_OperationId",
                table: "Participations",
                column: "OperationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participations");

            migrationBuilder.DropTable(
                name: "Operations");
        }
    }
}
