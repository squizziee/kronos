using Kronos.Machina.Contracts.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Kronos.Machina.Contracts.Commands
{
    public record UploadVideoCommand : IRequest<UploadVideoCommandResponseDto>
    {
        public required IFormFile Source { get; set; }
        public Guid UploadStrategyId { get; set; }
    }
}
