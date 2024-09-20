using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizza_Site.Migrations
{
    /// <inheritdoc />
    public partial class PizzaDescriptionUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imagePath",
                table: "PizzasDescription",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ingredients",
                table: "PizzasDescription",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pizzaName",
                table: "PizzasDescription",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "price",
                table: "PizzasDescription",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagePath",
                table: "PizzasDescription");

            migrationBuilder.DropColumn(
                name: "ingredients",
                table: "PizzasDescription");

            migrationBuilder.DropColumn(
                name: "pizzaName",
                table: "PizzasDescription");

            migrationBuilder.DropColumn(
                name: "price",
                table: "PizzasDescription");
        }
    }
}
