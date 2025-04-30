using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepoLayer.Migrations
{
    public partial class AddedBooksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_books (1)",
                table: "[books]");

            migrationBuilder.DropColumn(
                name: "id_oid",
                table: "[books]");

            migrationBuilder.DropColumn(
                name: "admin_user_id",
                table: "[books]");

            migrationBuilder.DropColumn(
                name: "v",
                table: "[books]");

            migrationBuilder.RenameTable(
                name: "[books]",
                newName: "Books");

            migrationBuilder.RenameColumn(
                name: "updatedAt_date",
                table: "Books",
                newName: "updatedAtDate");

            migrationBuilder.RenameColumn(
                name: "createdAt_date",
                table: "Books",
                newName: "createdAtDate");

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

            migrationBuilder.AlterColumn<int>(
                name: "quantity",
                table: "Books",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "price",
                table: "Books",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "discountPrice",
                table: "Books",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(750)",
                oldMaxLength: 750);

            migrationBuilder.AlterColumn<string>(
                name: "bookName",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "bookImage",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "author",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedAtDate",
                table: "Books",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAtDate",
                table: "Books",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "bookId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "bookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "bookId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "[books]");

            migrationBuilder.RenameColumn(
                name: "updatedAtDate",
                table: "[books]",
                newName: "updatedAt_date");

            migrationBuilder.RenameColumn(
                name: "createdAtDate",
                table: "[books]",
                newName: "createdAt_date");

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

            migrationBuilder.AlterColumn<short>(
                name: "quantity",
                table: "[books]",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<short>(
                name: "price",
                table: "[books]",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<short>(
                name: "discountPrice",
                table: "[books]",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "[books]",
                type: "nvarchar(750)",
                maxLength: 750,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "bookName",
                table: "[books]",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "bookImage",
                table: "[books]",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "author",
                table: "[books]",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "updatedAt_date",
                table: "[books]",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "createdAt_date",
                table: "[books]",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "id_oid",
                table: "[books]",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "admin_user_id",
                table: "[books]",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "v",
                table: "[books]",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_books (1)",
                table: "[books]",
                column: "id_oid");
        }
    }
}
