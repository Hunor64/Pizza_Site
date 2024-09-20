using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizza_Site.Migrations
{
    /// <inheritdoc />
    public partial class IdDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PizzasDescription",
                table: "PizzasDescription");

            migrationBuilder.DropColumn(
                name: "pizzaId",
                table: "PizzasDescription");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "PizzasDescription",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "pizzaName",
                table: "PizzasDescription",
                newName: "PizzaName");

            migrationBuilder.RenameColumn(
                name: "ingredients",
                table: "PizzasDescription",
                newName: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "imagePath",
                table: "PizzasDescription",
                newName: "ImagePath");

            migrationBuilder.AlterColumn<string>(
                name: "PizzaName",
                table: "PizzasDescription",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PizzasDescription",
                table: "PizzasDescription",
                column: "PizzaName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PizzasDescription",
                table: "PizzasDescription");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "PizzasDescription",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Ingredients",
                table: "PizzasDescription",
                newName: "ingredients");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "PizzasDescription",
                newName: "imagePath");

            migrationBuilder.RenameColumn(
                name: "PizzaName",
                table: "PizzasDescription",
                newName: "pizzaName");

            migrationBuilder.AlterColumn<string>(
                name: "pizzaName",
                table: "PizzasDescription",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "pizzaId",
                table: "PizzasDescription",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PizzasDescription",
                table: "PizzasDescription",
                column: "pizzaId");
        }
    }
}
