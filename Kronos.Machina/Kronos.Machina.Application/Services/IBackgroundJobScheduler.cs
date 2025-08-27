using Kronos.Machina.Domain.Entities;

namespace Kronos.Machina.Application.Services
{
    public interface IBackgroundJobScheduler
    {
        Task<string> ScheduleSanitization(VideoUploadData uploadData, 
            CancellationToken cancellationToken);
    }
}
