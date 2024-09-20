using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizza_Site.Migrations
{
    /// <inheritdoc />
    public partial class AddPizzaDescriptionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Is_Admin",
                table: "PizzaStore",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "PizzasDescription",
                columns: table => new
                {
                    pizzaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzasDescription", x => x.pizzaId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PizzasDescription");

            migrationBuilder.AlterColumn<bool>(
                name: "Is_Admin",
                table: "PizzaStore",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
