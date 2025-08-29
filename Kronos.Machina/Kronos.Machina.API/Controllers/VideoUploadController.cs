using Kronos.Machina.Contracts.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kronos.Machina.API.Controllers
{
    [Route("api/video/upload")]
    [ApiController]
    public class VideoUploadController : ControllerBase
    {
        private readonly ISender _sender;
        public VideoUploadController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> UploadVideoFile(UploadVideoCommand request, 
            CancellationToken cancellationToken)
        {
            await _sender.Send(request, cancellationToken);

            return Ok();
        }
    }
}
