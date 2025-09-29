using Kronos.Machina.Domain.Entities;
using Kronos.Machina.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kronos.Machina.Infrastructure.Data.Repositories
{
	public class VideoDataRepository : DefaultRepository<VideoData>, IVideoDataRepository
	{
		public VideoDataRepository(VideoDatabaseContext context) : base(context)
		{
		}

		public Task AddQualityByIdAsync(Guid id, VideoImageQuality quality,
			CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

        public Task<IEnumerable<VideoData>> GetAllAsync(CancellationToken cancellationToken)
        {
			return Task.FromResult(_context.VideoData.ToList().AsEnumerable());
        }

        public Task SetBlobIdByIdAsync(Guid videoDataId, Guid blobId,
			CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public async Task UpdateBlobSanitizationStateByIdAsync(Guid id, BlobSanitizationState newState,
			CancellationToken cancellationToken = default)
		{
			var entity = await _context.VideoData
				.Include(vd => vd.UploadData.BlobData)
				.FirstOrDefaultAsync(vd => vd.Id == id, cancellationToken);

			if (entity == null)
			{
				// TODO
				throw new Exception("no such thing baby");
			}

			entity.UploadData.BlobData.SanitizationData.State = newState;
		}

		public async Task UpdateUploadStateByIdAsync(Guid id, VideoUploadState newState,
			CancellationToken cancellationToken = default)
		{
			var entity = await _context.VideoData
                .Include(vd => vd.UploadData)
                .FirstOrDefaultAsync(vd => vd.Id == id, cancellationToken);

			if (entity == null)
			{
				// TODO
				throw new Exception("no such thing baby");
			}

			entity.UploadData.State = newState;
		}
	}
}
