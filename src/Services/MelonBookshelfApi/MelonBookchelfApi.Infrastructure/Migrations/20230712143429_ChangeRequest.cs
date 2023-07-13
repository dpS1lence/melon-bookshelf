using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MelonBookchelfApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryStatus",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryStatus",
                table: "Requests");
        }
    }
}
