using Kronos.Machina.Domain.Entities;
using Kronos.Machina.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kronos.Machina.Infrastructure.Data.Repositories
{
    public class VideoFormatRepository : DefaultRepository<VideoFormat>, IVideoFormatRepository
    {
        public VideoFormatRepository(VideoDatabaseContext context) : base(context)
        {
        }

        public async Task<IEnumerable<VideoFormat>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.VideoFormats
                .ToListAsync(cancellationToken);
        }

        public async Task<VideoFormat?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.VideoFormats
                .Where(format => format.Name.Equals(name))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public override Task UpdateAsync(VideoFormat entity, CancellationToken cancellationToken = default)
        {
            var entry = _context.VideoFormats
                .Entry(entity);

            entry.Entity.Name = entity.Name;
            entry.Entity.Signature = entity.Signature;
            entry.Entity.Extension = entity.Extension;

            return Task.CompletedTask;
        }
    }
}
