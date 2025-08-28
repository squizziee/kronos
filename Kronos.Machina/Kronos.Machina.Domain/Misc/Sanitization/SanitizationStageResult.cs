using Kronos.Machina.Domain.Entities;

namespace Kronos.Machina.Domain.Misc.Sanitization
{
    public record SanitizationStageResult
    {
        public required VideoData VideoData { get; init; }
        public required bool IsSuccessful { get; init; }
        public AggregateException? AggregateException { get; init; }
        public string? Message { get; init; }
    }
}
