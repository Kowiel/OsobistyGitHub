using Microsoft.EntityFrameworkCore.Migrations;

namespace PrzepisyDlaCiebie.Migrations
{
    public partial class AddPrzepisyToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "przepisy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazwaPrzepisu = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TrescPrzepisu = table.Column<string>(type: "nvarchar(4000)", nullable: false),
                    NazwaZdjecia = table.Column<string>(type: "nvarchar(1000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_przepisy", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "przepisy");
        }
    }
}
