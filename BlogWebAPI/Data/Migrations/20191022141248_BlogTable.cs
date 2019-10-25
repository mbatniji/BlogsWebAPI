using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogWebAPI.Data.Migrations
{
    public partial class BlogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Subtitle = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Blog",
                columns: new[] { "Id", "Author", "Body", "CreateDate", "ImageUrl", "Subtitle", "Title" },
                values: new object[] { 1, "e803093a-0c3e-44dc-bdd0-1cf4190d4c1a", "body", new DateTime(2019, 10, 22, 17, 12, 47, 487, DateTimeKind.Local).AddTicks(710), "https://media.licdn.com/dms/image/C4D0BAQHXLZiGuQOGhA/company-logo_200_200/0?e=2159024400&v=beta&t=IR3o7r2Lq_ZfG15XdvBDwmmFWnKpP9cU0yFP1z1zlXw", "Sub Text", "Text" });

            migrationBuilder.InsertData(
                table: "Blog",
                columns: new[] { "Id", "Author", "Body", "CreateDate", "ImageUrl", "Subtitle", "Title" },
                values: new object[] { 2, "e803093a-0c3e-44dc-bdd0-1cf4190d4c1a", "body2", new DateTime(2019, 10, 22, 17, 12, 47, 493, DateTimeKind.Local).AddTicks(1959), "https://media.licdn.com/dms/image/C4D0BAQHXLZiGuQOGhA/company-logo_200_200/0?e=2159024400&v=beta&t=IR3o7r2Lq_ZfG15XdvBDwmmFWnKpP9cU0yFP1z1zlXw", "Sub Text2", "Text2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
