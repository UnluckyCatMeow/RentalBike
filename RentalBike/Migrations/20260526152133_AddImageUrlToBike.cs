using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalBike.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToBike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Bikes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Bikes");
        }
    }
}
