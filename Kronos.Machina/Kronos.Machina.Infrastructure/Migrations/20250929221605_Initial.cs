using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kronos.Machina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                    UploadData_UploadStrategyId = table.Column<Guid>(type: "TEXT", nullable: true),
                    UploadData_BlobData_BlobId = table.Column<string>(type: "TEXT", nullable: false),
                    UploadData_BlobData_SanitizationData_State = table.Column<int>(type: "INTEGER", nullable: false),
                    UploadData_BlobData_SanitizationData_NextStageNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    UploadData_BlobData_SanitizationData_History__nextEntryIndex = table.Column<int>(type: "NextEntryIndex", nullable: false, defaultValue: 0),
                    Orientation = table.Column<int>(type: "INTEGER", nullable: false),
                    VideoFormatId = table.Column<Guid>(type: "TEXT", nullable: true),
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VideoData_VideoFormats_VideoFormatId",
                        column: x => x.VideoFormatId,
                        principalTable: "VideoFormats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BlobSanitizationHistoryEntry",
                columns: table => new
                {
                    _id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSuccessful = table.Column<bool>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    _historyId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlobSanitizationHistoryEntry", x => x._id);
                    table.ForeignKey(
                        name: "FK_BlobSanitizationHistoryEntry_VideoData__historyId",
                        column: x => x._historyId,
                        principalTable: "VideoData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlobSanitizationHistoryEntry__historyId",
                table: "BlobSanitizationHistoryEntry",
                column: "_historyId");

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
