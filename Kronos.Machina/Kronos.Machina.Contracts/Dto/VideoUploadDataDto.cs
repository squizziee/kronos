namespace Kronos.Machina.Contracts.Dto
{
    public record VideoUploadDataDto
    {
        public required string State { get; set; }
        public required BlobDataDto BlobData { get; set; }
    }
}
