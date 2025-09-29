using Kronos.Machina.Domain.Entities;

namespace Kronos.Machina.Application.Misc.Sanitization
{
    public record SanitizationStageResult
    {
        public required VideoData VideoData { get; init; }
        public required bool IsSuccessful { get; init; }
        public bool IsInvalidStateResolutionStage { get; init; }
        public Exception? Exception { get; init; }
        public string? Message { get; init; }
        public required Type StageType { get; init; }
        public bool IsTerminal { get; init; } = false;
    }
}
