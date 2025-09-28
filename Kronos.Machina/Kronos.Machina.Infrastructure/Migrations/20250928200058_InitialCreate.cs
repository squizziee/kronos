using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kronos.Machina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UploadStrategies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    IsSoftDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadStrategies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoFormats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Signature = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Extension = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    IsSoftDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoFormats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UploadData_State = table.Column<int>(type: "INTEGER", nullable: false),
                    UploadData_UploadStrategyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UploadData_BlobData_BlobId = table.Column<string>(type: "TEXT", nullable: false),
                    UploadData_BlobData_SanitizationData_State = table.Column<int>(type: "INTEGER", nullable: false),
                    Orientation = table.Column<int>(type: "INTEGER", nullable: false),
                    VideoFormatId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AvailableImageQuality = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    IsSoftDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoData_UploadStrategies_UploadData_UploadStrategyId",
                        column: x => x.UploadData_UploadStrategyId,
                        principalTable: "UploadStrategies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoData_VideoFormats_VideoFormatId",
                        column: x => x.VideoFormatId,
                        principalTable: "VideoFormats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlobSanitizationHistoryEntry",
                columns: table => new
                {
                    BlobSanitizationHistorySanitizationDataBlobDataVideoUploadDataVideoDataId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    StageType = table.Column<string>(type: "TEXT", nullable: false),
                    IsSuccessful = table.Column<bool>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlobSanitizationHistoryEntry", x => new { x.BlobSanitizationHistorySanitizationDataBlobDataVideoUploadDataVideoDataId, x.Id });
                    table.ForeignKey(
                        name: "FK_BlobSanitizationHistoryEntry_VideoData_BlobSanitizationHistorySanitizationDataBlobDataVideoUploadDataVideoDataId",
                        column: x => x.BlobSanitizationHistorySanitizationDataBlobDataVideoUploadDataVideoDataId,
                        principalTable: "VideoData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideoData_UploadData_UploadStrategyId",
                table: "VideoData",
                column: "UploadData_UploadStrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoData_VideoFormatId",
                table: "VideoData",
                column: "VideoFormatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlobSanitizationHistoryEntry");

            migrationBuilder.DropTable(
                name: "VideoData");

            migrationBuilder.DropTable(
                name: "UploadStrategies");

            migrationBuilder.DropTable(
                name: "VideoFormats");
        }
    }
}
