using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GustWeb.DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class ShippingDate_nameFIX : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SnippingDate",
                table: "OrderHeaders",
                newName: "ShippingDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingDate",
                table: "OrderHeaders",
                newName: "SnippingDate");
        }
    }
}
