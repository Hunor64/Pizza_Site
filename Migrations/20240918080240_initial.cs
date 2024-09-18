using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizza_Site.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PizzaStore",
                columns: table => new
                {
                    User_Name = table.Column<string>(type: "TEXT", nullable: false),
                    User_Password = table.Column<string>(type: "TEXT", nullable: true),
                    User_Email = table.Column<string>(type: "TEXT", nullable: true),
                    User_MobileNumber = table.Column<string>(type: "TEXT", nullable: true),
                    User_Address = table.Column<string>(type: "TEXT", nullable: true),
                    Is_Admin = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaStore", x => x.User_Name);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PizzaStore");
        }
    }
}
