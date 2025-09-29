namespace Kronos.Machina.Infrastructure.ConfigOptions
{
    public record PipelineConfig
    {
        public required ICollection<PipelineStageConfig> Stages { get; set; }
    }

    public record PipelineStageConfig
    {
        public required string Id { get; set; }
        public required uint Order  { get; set; }
        public required Dictionary<string, string> Args { get; set; }
    }
}
