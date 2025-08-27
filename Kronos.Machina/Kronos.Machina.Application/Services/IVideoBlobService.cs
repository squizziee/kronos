using Microsoft.AspNetCore.Http;

namespace Kronos.Machina.Application.Services
{
    public interface IVideoBlobService
    {
        Task<Guid> SaveToBlob(IFormFile source);
    }
}
