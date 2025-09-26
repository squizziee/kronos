using Kronos.Machina.Domain.Entities;

namespace Kronos.Machina.Infrastructure.ConfigOptions
{
    public record SanitizedBlobZoneInfo
    {
        public required string BlobPath { get; init; }
        public required Dictionary<VideoImageQuality, string> QualityPaths { get; set; }
    }
}
