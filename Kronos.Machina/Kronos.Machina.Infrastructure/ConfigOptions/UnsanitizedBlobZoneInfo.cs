namespace Kronos.Machina.Infrastructure.ConfigOptions
{
    public record UnsanitizedBlobZoneInfo
    {
        public required string BlobPath { get; init; }
    }
}
