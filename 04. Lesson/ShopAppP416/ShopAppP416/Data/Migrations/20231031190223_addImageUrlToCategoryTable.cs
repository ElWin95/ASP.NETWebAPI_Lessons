using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopAppP416.Data.Migrations
{
    public partial class addImageUrlToCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 31, 19, 2, 23, 177, DateTimeKind.Utc).AddTicks(8429),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 28, 19, 7, 19, 908, DateTimeKind.Utc).AddTicks(187));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrL",
                table: "Categories",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrL",
                table: "Categories");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 28, 19, 7, 19, 908, DateTimeKind.Utc).AddTicks(187),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 31, 19, 2, 23, 177, DateTimeKind.Utc).AddTicks(8429));
        }
    }
}
