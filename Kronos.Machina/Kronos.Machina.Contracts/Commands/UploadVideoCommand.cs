using MediatR;
using Microsoft.AspNetCore.Http;

namespace Kronos.Machina.Contracts.Commands
{
    public record UploadVideoCommand : IRequest
    {
        public required IFormFile Source { get; set; }
    }
}
