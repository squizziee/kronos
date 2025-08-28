using Kronos.Machina.Domain.Entities;

namespace Kronos.Machina.Application.Services
{
    public interface IBackgroundJobScheduler
    {
        Task<string> ScheduleSignatureValidationAsync(VideoData videoData, 
            CancellationToken cancellationToken = default);
    }
}
