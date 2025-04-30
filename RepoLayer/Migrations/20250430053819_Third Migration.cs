using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepoLayer.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "refreshTokenExpiryTime",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "('0001-01-01T00:00:00.0000000')",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "refreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValueSql: "(N'')",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "refreshTokenExpiryTime",
                table: "Admins",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "('0001-01-01T00:00:00.0000000')",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "refreshToken",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValueSql: "(N'')",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "[books]",
                columns: table => new
                {
                    id_oid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(750)", maxLength: 750, nullable: false),
                    discountPrice = table.Column<short>(type: "smallint", nullable: false),
                    bookImage = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    admin_user_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    bookName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    author = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    quantity = table.Column<short>(type: "smallint", nullable: false),
                    price = table.Column<short>(type: "smallint", nullable: false),
                    createdAt_date = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    updatedAt_date = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    v = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books (1)", x => x.id_oid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "[books]");

            migrationBuilder.AlterColumn<DateTime>(
                name: "refreshTokenExpiryTime",
                table: "Users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "('0001-01-01T00:00:00.0000000')");

            migrationBuilder.AlterColumn<string>(
                name: "refreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValueSql: "(N'')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "refreshTokenExpiryTime",
                table: "Admins",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "('0001-01-01T00:00:00.0000000')");

            migrationBuilder.AlterColumn<string>(
                name: "refreshToken",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValueSql: "(N'')");
        }
    }
}
