using Kronos.Machina.Application.Services;
using Kronos.Machina.Domain.Entities;

namespace Kronos.Machina.Infrastructure.Services
{
    public class BackgroundJobScheduler : IBackgroundJobScheduler
    {
        public Task<string> ScheduleSanitization(VideoUploadData uploadData, 
            CancellationToken cancellationToken)
        {
            
        }
    }
}
