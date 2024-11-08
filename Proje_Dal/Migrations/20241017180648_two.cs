using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Proje_Dal.Migrations
{
    public partial class two : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "429d413c-5be5-4819-ad9c-30f36e495ef9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b47306a-c8fc-423d-b374-87274353722a");

            migrationBuilder.AddColumn<DateTime>(
                name: "StatuTime",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1b7a9fbe-0edb-4706-96b0-9b43ed7daf98", "0ceb5fad-9bdf-4f48-a77c-5be4a91d4a13", "Member", "MEMBER" },
                    { "c1bf683e-8f89-4c4b-8faa-eb35df774817", "ad35bcaf-82d1-4924-b057-136c6e8db240", "Admın", "ADMIN" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b7a9fbe-0edb-4706-96b0-9b43ed7daf98");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1bf683e-8f89-4c4b-8faa-eb35df774817");

            migrationBuilder.DropColumn(
                name: "StatuTime",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "429d413c-5be5-4819-ad9c-30f36e495ef9", "614319ff-c16b-4d4c-949d-75168077d382", "Member", "MEMBER" },
                    { "8b47306a-c8fc-423d-b374-87274353722a", "c0d92bbb-3fd9-4516-a366-5bf1ec6af7b0", "Admın", "ADMIN" }
                });
        }
    }
}
