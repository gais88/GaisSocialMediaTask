using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GaisSocialMediaTask.Data.Migrations
{
    /// <inheritdoc />
    public partial class addlikesfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Like",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Like",
                table: "Posts");
        }
    }
}
