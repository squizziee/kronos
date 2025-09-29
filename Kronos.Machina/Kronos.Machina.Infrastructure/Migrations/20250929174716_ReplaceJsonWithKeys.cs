using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kronos.Machina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceJsonWithKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlobSanitizationHistoryEntry_VideoData_BlobSanitizationHistorySanitizationDataBlobDataVideoUploadDataVideoDataId",
                table: "BlobSanitizationHistoryEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlobSanitizationHistoryEntry",
                table: "BlobSanitizationHistoryEntry");

            migrationBuilder.RenameColumn(
                name: "BlobSanitizationHistorySanitizationDataBlobDataVideoUploadDataVideoDataId",
                table: "BlobSanitizationHistoryEntry",
                newName: "_historyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BlobSanitizationHistoryEntry",
                newName: "_id");

            migrationBuilder.AlterColumn<int>(
                name: "_id",
                table: "BlobSanitizationHistoryEntry",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlobSanitizationHistoryEntry",
                table: "BlobSanitizationHistoryEntry",
                column: "_id");

            migrationBuilder.CreateIndex(
                name: "IX_BlobSanitizationHistoryEntry__historyId",
                table: "BlobSanitizationHistoryEntry",
                column: "_historyId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlobSanitizationHistoryEntry_VideoData__historyId",
                table: "BlobSanitizationHistoryEntry",
                column: "_historyId",
                principalTable: "VideoData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlobSanitizationHistoryEntry_VideoData__historyId",
                table: "BlobSanitizationHistoryEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlobSanitizationHistoryEntry",
                table: "BlobSanitizationHistoryEntry");

            migrationBuilder.DropIndex(
                name: "IX_BlobSanitizationHistoryEntry__historyId",
                table: "BlobSanitizationHistoryEntry");

            migrationBuilder.RenameColumn(
                name: "_historyId",
                table: "BlobSanitizationHistoryEntry",
                newName: "BlobSanitizationHistorySanitizationDataBlobDataVideoUploadDataVideoDataId");

            migrationBuilder.RenameColumn(
                name: "_id",
                table: "BlobSanitizationHistoryEntry",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "BlobSanitizationHistoryEntry",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlobSanitizationHistoryEntry",
                table: "BlobSanitizationHistoryEntry",
                columns: new[] { "BlobSanitizationHistorySanitizationDataBlobDataVideoUploadDataVideoDataId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_BlobSanitizationHistoryEntry_VideoData_BlobSanitizationHistorySanitizationDataBlobDataVideoUploadDataVideoDataId",
                table: "BlobSanitizationHistoryEntry",
                column: "BlobSanitizationHistorySanitizationDataBlobDataVideoUploadDataVideoDataId",
                principalTable: "VideoData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
