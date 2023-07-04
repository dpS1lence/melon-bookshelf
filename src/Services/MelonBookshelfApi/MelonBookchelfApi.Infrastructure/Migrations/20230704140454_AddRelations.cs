using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MelonBookchelfApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_ResourceCategories_ResourceCategoryId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_ResourceCategoryId",
                table: "Resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestsUpvoters",
                table: "RequestsUpvoters");

            migrationBuilder.DropIndex(
                name: "IX_RequestsUpvoters_ResourceID",
                table: "RequestsUpvoters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestsFollowers",
                table: "RequestsFollowers");

            migrationBuilder.DropIndex(
                name: "IX_RequestsFollowers_ResourceID",
                table: "RequestsFollowers");

            migrationBuilder.DropColumn(
                name: "ResourceCategoryId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RequestsUpvoters");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RequestsFollowers");

            migrationBuilder.DropColumn(
                name: "RequestID",
                table: "Requests");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "RequestsUpvoters",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "RequestsFollowers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestsUpvoters",
                table: "RequestsUpvoters",
                columns: new[] { "ResourceID", "UserID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestsFollowers",
                table: "RequestsFollowers",
                columns: new[] { "ResourceID", "UserID" });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_CategoryID",
                table: "Resources",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestsUpvoters_UserID",
                table: "RequestsUpvoters",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestsFollowers_UserID",
                table: "RequestsFollowers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_UserID",
                table: "Requests",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_UserID",
                table: "Requests",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestsFollowers_AspNetUsers_UserID",
                table: "RequestsFollowers",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestsUpvoters_AspNetUsers_UserID",
                table: "RequestsUpvoters",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_ResourceCategories_CategoryID",
                table: "Resources",
                column: "CategoryID",
                principalTable: "ResourceCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_UserID",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestsFollowers_AspNetUsers_UserID",
                table: "RequestsFollowers");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestsUpvoters_AspNetUsers_UserID",
                table: "RequestsUpvoters");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_ResourceCategories_CategoryID",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_CategoryID",
                table: "Resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestsUpvoters",
                table: "RequestsUpvoters");

            migrationBuilder.DropIndex(
                name: "IX_RequestsUpvoters_UserID",
                table: "RequestsUpvoters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestsFollowers",
                table: "RequestsFollowers");

            migrationBuilder.DropIndex(
                name: "IX_RequestsFollowers_UserID",
                table: "RequestsFollowers");

            migrationBuilder.DropIndex(
                name: "IX_Requests_UserID",
                table: "Requests");

            migrationBuilder.AddColumn<int>(
                name: "ResourceCategoryId",
                table: "Resources",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "RequestsUpvoters",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RequestsUpvoters",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "RequestsFollowers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RequestsFollowers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Requests",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "RequestID",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestsUpvoters",
                table: "RequestsUpvoters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestsFollowers",
                table: "RequestsFollowers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ResourceCategoryId",
                table: "Resources",
                column: "ResourceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestsUpvoters_ResourceID",
                table: "RequestsUpvoters",
                column: "ResourceID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestsFollowers_ResourceID",
                table: "RequestsFollowers",
                column: "ResourceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_ResourceCategories_ResourceCategoryId",
                table: "Resources",
                column: "ResourceCategoryId",
                principalTable: "ResourceCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
