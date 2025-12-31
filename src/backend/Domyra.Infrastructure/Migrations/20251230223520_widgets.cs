using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domyra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class widgets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "widgets",
                columns: table => new
                {
                    alias = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    state = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_widgets", x => x.alias);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "widgets");
        }
    }
}
