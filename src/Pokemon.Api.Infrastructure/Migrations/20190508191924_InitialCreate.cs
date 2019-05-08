using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokemon.Api.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pokemon",
                columns: table => new
                {
                    PokemonId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Index = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Types = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon", x => x.PokemonId);
                });

            migrationBuilder.CreateTable(
                name: "Evolution",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PokemonId = table.Column<int>(nullable: false),
                    Pokemon = table.Column<int>(nullable: false),
                    Event = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evolution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evolution_Pokemon_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemon",
                        principalColumn: "PokemonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Move",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PokemonId = table.Column<int>(nullable: false),
                    Level = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Attack = table.Column<string>(nullable: true),
                    Accuracy = table.Column<string>(nullable: true),
                    PP = table.Column<string>(nullable: true),
                    EffectPercent = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Move", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Move_Pokemon_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemon",
                        principalColumn: "PokemonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evolution_PokemonId",
                table: "Evolution",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_Move_PokemonId",
                table: "Move",
                column: "PokemonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evolution");

            migrationBuilder.DropTable(
                name: "Move");

            migrationBuilder.DropTable(
                name: "Pokemon");
        }
    }
}
