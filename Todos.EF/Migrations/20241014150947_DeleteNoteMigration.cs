using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todos.EF.Migrations
{
    /// <inheritdoc />
    public partial class DeleteNoteMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Note_NoteId",
                table: "Item");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "Note",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2024, 10, 14, 15, 9, 47, 232, DateTimeKind.Unspecified).AddTicks(7020), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2023, 8, 5, 13, 14, 20, 687, DateTimeKind.Unspecified).AddTicks(8393), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Note_NoteId",
                table: "Item",
                column: "NoteId",
                principalTable: "Note",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Note_NoteId",
                table: "Item");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "Note",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2023, 8, 5, 13, 14, 20, 687, DateTimeKind.Unspecified).AddTicks(8393), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2024, 10, 14, 15, 9, 47, 232, DateTimeKind.Unspecified).AddTicks(7020), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Note_NoteId",
                table: "Item",
                column: "NoteId",
                principalTable: "Note",
                principalColumn: "Id");
        }
    }
}
