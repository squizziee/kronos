namespace Kronos.Machina.Contracts.Dto
{
    public record VideoFormatDto
    {
        public required string Name { get; set; }
        public required string Extension { get; set; }
    }
}
