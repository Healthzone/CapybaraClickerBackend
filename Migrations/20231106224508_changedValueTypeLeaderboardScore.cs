using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapybaraClickerBackend.Migrations
{
    /// <inheritdoc />
    public partial class changedValueTypeLeaderboardScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Score",
                table: "Leaderboards",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Score",
                table: "Leaderboards",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
