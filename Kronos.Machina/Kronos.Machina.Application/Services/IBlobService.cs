using Microsoft.AspNetCore.Http;

namespace Kronos.Machina.Application.Services
{
    public interface IBlobService
    {
        Task<string?> SaveToBlob(IFormFile source, CancellationToken cancellationToken = default);
    }
}
