using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizza_Site.Migrations
{
    /// <inheritdoc />
    public partial class PizzaStorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PizzaStorage",
                columns: table => new
                {
                    ingredientName = table.Column<string>(type: "TEXT", nullable: false),
                    ingredientAmount = table.Column<int>(type: "INTEGER", nullable: false),
                    isAvailable = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaStorage", x => x.ingredientName);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PizzaStorage");
        }
    }
}
