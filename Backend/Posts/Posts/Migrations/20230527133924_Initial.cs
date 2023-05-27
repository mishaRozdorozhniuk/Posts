using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Posts.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    LastName = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Body = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    UserGid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserGid",
                        column: x => x.UserGid,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserGid",
                table: "Posts",
                column: "UserGid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
