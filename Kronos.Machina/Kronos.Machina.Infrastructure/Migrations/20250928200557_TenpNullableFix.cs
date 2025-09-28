using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kronos.Machina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TenpNullableFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoData_VideoFormats_VideoFormatId",
                table: "VideoData");

            migrationBuilder.AlterColumn<Guid>(
                name: "VideoFormatId",
                table: "VideoData",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoData_VideoFormats_VideoFormatId",
                table: "VideoData",
                column: "VideoFormatId",
                principalTable: "VideoFormats",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoData_VideoFormats_VideoFormatId",
                table: "VideoData");

            migrationBuilder.AlterColumn<Guid>(
                name: "VideoFormatId",
                table: "VideoData",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoData_VideoFormats_VideoFormatId",
                table: "VideoData",
                column: "VideoFormatId",
                principalTable: "VideoFormats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
