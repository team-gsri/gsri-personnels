using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gsri.Personnels.Database.Migrations
{
    /// <inheritdoc />
    public partial class RaidHelper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RaidHelperId",
                table: "Operations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DiscordId",
                table: "Joueurs",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RaidHelperId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "DiscordId",
                table: "Joueurs");
        }
    }
}
