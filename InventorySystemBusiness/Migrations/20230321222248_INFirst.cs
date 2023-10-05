using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InventorySystemBusiness.Migrations
{
    public partial class INFirst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ID", "Created", "Description", "ImagePath", "Name", "Price", "Updated" },
                values: new object[] { 1, new DateTime(2023, 3, 22, 3, 22, 48, 3, DateTimeKind.Local).AddTicks(9108), "Teddy Toy Full Size", "/Images/ProductImages/abc.jpg", "Teddy Toy", 2.3599999999999999, new DateTime(2023, 3, 22, 3, 22, 48, 6, DateTimeKind.Local).AddTicks(2064) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
