namespace Kronos.Machina.Infrastructure.ConfigOptions
{
    public record VideoTypeSignatures
    {
        public required byte[] MP4 { get; init; }
        public required byte[] M4A { get; init; }
        public required byte[] AVI { get; init; }
        public required byte[] MOV { get; init; }
        public required byte[] MPEG { get; init; }
        public required byte[] MPEG2 { get; init; }
    }
}
