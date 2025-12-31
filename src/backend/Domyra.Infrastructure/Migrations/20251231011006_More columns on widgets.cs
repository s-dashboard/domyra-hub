using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domyra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Morecolumnsonwidgets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cols",
                table: "widgets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "element_name",
                table: "widgets",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "html",
                table: "widgets",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "kind",
                table: "widgets",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "rows",
                table: "widgets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "script_url",
                table: "widgets",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cols",
                table: "widgets");

            migrationBuilder.DropColumn(
                name: "element_name",
                table: "widgets");

            migrationBuilder.DropColumn(
                name: "html",
                table: "widgets");

            migrationBuilder.DropColumn(
                name: "kind",
                table: "widgets");

            migrationBuilder.DropColumn(
                name: "rows",
                table: "widgets");

            migrationBuilder.DropColumn(
                name: "script_url",
                table: "widgets");
        }
    }
}
