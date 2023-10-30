using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sg_rentals.Migrations
{
    /// <inheritdoc />
    public partial class bookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Car_Brands_BrandId",
            //    table: "Car");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Car_CarBrandModels_CarBrandModelId",
            //    table: "Car");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Car",
            //    table: "Car");

            //migrationBuilder.RenameTable(
            //    name: "Car",
            //    newName: "Cars");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Car_CarBrandModelId",
            //    table: "Cars",
            //    newName: "IX_Cars_CarBrandModelId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Car_BrandId",
            //    table: "Cars",
            //    newName: "IX_Cars_BrandId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Cars",
            //    table: "Cars",
            //    column: "Id");

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Price = table.Column<decimal>(type: "DECIMAL", nullable: false),
                    DateStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    CarId = table.Column<int>(type: "INTEGER", nullable: false),
                    HouseId = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    //table.ForeignKey(
                    //    name: "FK_Bookings_Cars_CarId",
                    //    column: x => x.CarId,
                    //    principalTable: "Cars",
                    //    principalColumn: "Id",
                    //    onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    //table.ForeignKey(
                    //    name: "FK_Bookings_Houses_HouseId",
                    //    column: x => x.HouseId,
                    //    principalTable: "Houses",
                    //    principalColumn: "Id",
                    //    onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CarId",
                table: "Bookings",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_HouseId",
                table: "Bookings",
                column: "HouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarBrandModels_CarBrandModelId",
                table: "Cars",
                column: "CarBrandModelId",
                principalTable: "CarBrandModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarBrandModels_CarBrandModelId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "Cars",
                newName: "Car");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_CarBrandModelId",
                table: "Car",
                newName: "IX_Car_CarBrandModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_BrandId",
                table: "Car",
                newName: "IX_Car_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car",
                table: "Car",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Brands_BrandId",
                table: "Car",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_CarBrandModels_CarBrandModelId",
                table: "Car",
                column: "CarBrandModelId",
                principalTable: "CarBrandModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
