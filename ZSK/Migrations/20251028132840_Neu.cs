using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZSK.Migrations
{
    /// <inheritdoc />
    public partial class Neu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_conversions_animalRates_AnimalRateId",
                table: "conversions");

            migrationBuilder.AddForeignKey(
                name: "FK_conversions_animalRates_AnimalRateId",
                table: "conversions",
                column: "AnimalRateId",
                principalTable: "animalRates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_conversions_animalRates_AnimalRateId",
                table: "conversions");

            migrationBuilder.AddForeignKey(
                name: "FK_conversions_animalRates_AnimalRateId",
                table: "conversions",
                column: "AnimalRateId",
                principalTable: "animalRates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
