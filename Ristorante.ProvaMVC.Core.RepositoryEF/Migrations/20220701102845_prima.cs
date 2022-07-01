using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ristorante.ProvaMVC.Core.RepositoryEF.Migrations
{
    public partial class prima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Piatto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipologia = table.Column<int>(type: "int", nullable: false),
                    Prezzo = table.Column<double>(type: "float", nullable: false),
                    IdMenu = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piatto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Piatto_Menu_IdMenu",
                        column: x => x.IdMenu,
                        principalTable: "Menu",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Utente",
                columns: new[] { "Id", "Password", "Role", "Username" },
                values: new object[] { 1, "1234", 0, "renata@mail.it" });

            migrationBuilder.InsertData(
                table: "Utente",
                columns: new[] { "Id", "Password", "Role", "Username" },
                values: new object[] { 2, "12345", 1, "mario@mail.it" });

            migrationBuilder.CreateIndex(
                name: "IX_Piatto_IdMenu",
                table: "Piatto",
                column: "IdMenu");

            migrationBuilder.CreateIndex(
                name: "IX_Utente_Username",
                table: "Utente",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Piatto");

            migrationBuilder.DropTable(
                name: "Utente");

            migrationBuilder.DropTable(
                name: "Menu");
        }
    }
}
