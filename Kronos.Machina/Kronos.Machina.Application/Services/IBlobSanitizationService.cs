namespace Kronos.Machina.Application.Services
{
    public interface IBlobSanitizationService
    {
        Task<bool> Sanitize(Guid blobId, CancellationToken cancellationToken);
        Task<bool> SanitizeAndCleanUp(Guid blobId, CancellationToken cancellationToken);
    }
}
