using Kronos.Machina.Domain.Entities;

namespace Kronos.Machina.Application.Misc.Sanitization
{
    public record SanitizationStageResult
    {
        public required VideoData VideoData { get; init; }
        public required bool IsSuccessful { get; init; }
        public AggregateException? AggregateException { get; init; }
        public string? Message { get; init; }
        public required Type StageType { get; init; }
    }
}
