using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kronos.Machina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Refresh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StageType",
                table: "BlobSanitizationHistoryEntry");

            migrationBuilder.AddColumn<int>(
                name: "UploadData_BlobData_SanitizationData_History__nextEntryIndex",
                table: "VideoData",
                type: "NextEntryIndex",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadData_BlobData_SanitizationData_History__nextEntryIndex",
                table: "VideoData");

            migrationBuilder.AddColumn<string>(
                name: "StageType",
                table: "BlobSanitizationHistoryEntry",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
