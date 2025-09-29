using Kronos.Machina.Application.Misc.Sanitization;
using Kronos.Machina.Contracts.CommonExceptions;
using Kronos.Machina.Domain.Entities;
using Kronos.Machina.Domain.Repositories;
using Kronos.Machina.Infrastructure.Data.BlobStorage;
using Kronos.Machina.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Kronos.Machina.Infrastructure.Jobs
{
    /// <summary>
    /// The purpose of this sanitization step is to verify the magic numbers in file headers.
    /// The meaning of word <i>"signature"</i> in this case is a series of "magic" 8-bit numbers
    /// (bytes) that are present in every valid video files' header. The way sanitization is 
    /// done is by bytewise signature comparison against supported video formats whose signatures 
    /// are stored in database and represented by <see cref="IVideoFormatRepository"/> .
    /// </summary>
    public class SignatureValidationBlobSanitizationJob : IJob
    {
        private readonly IBlobStorage _blobStorage;
        private readonly IVideoDataRepository _videoDataRepository;
        private readonly IVideoFormatRepository _videoFormatRepository;
        private readonly IBlobSanitizationOrchestrator _blobSanitizationOrchestrator;
        private readonly ILogger<SignatureValidationBlobSanitizationJob> _logger;

        public SignatureValidationBlobSanitizationJob(IBlobStorage blobStorage,
            IVideoDataRepository videoDataRepository,
            IVideoFormatRepository videoFormatRepository,
            IBlobSanitizationOrchestrator blobSanitizationOrchestrator,
            ILogger<SignatureValidationBlobSanitizationJob> logger)
        {
            _blobStorage = blobStorage;
            _videoDataRepository = videoDataRepository;
            _videoFormatRepository = videoFormatRepository;
            _blobSanitizationOrchestrator = blobSanitizationOrchestrator;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var videoDataStrGuid = context.JobDetail.JobDataMap.GetString("VideoDataId");

            if (!Guid.TryParse(videoDataStrGuid, out var videoDataId))
            {
                throw new ArgumentException("VideoDataId provided is not a valid Guid");
            }

            var videoData = await _videoDataRepository.GetByIdAsync(videoDataId,
                context.CancellationToken);

            if (videoData == null)
            {
                throw new ResourceNotFoundException($"No video data with Guid {videoData} was found");
            }

            try
            {
                var blobIdentifier = DiskMemoryBlobIdentifier.Construct(videoData.UploadData.BlobData.BlobId);

                // 20 bytes is enough for any type of suported video file signature
                var blobHeaderFragment = await _blobStorage.GetBlobDataAsync(blobIdentifier, 0, 20,
                    context.CancellationToken);

                var format = await CompareSignaturesAsync(blobHeaderFragment, context.CancellationToken);

                videoData.VideoFormat = format;
                videoData.UploadData.BlobData.SanitizationData.State =
                    BlobSanitizationState.SignatureConfirmed;

                videoData.UploadData.BlobData.SanitizationData
                    .History.AddEntry($"Signature verified, format: {format.Name}", true);

                await _videoFormatRepository.SaveChangesAsync(context.CancellationToken);

                _logger.LogInformation("Signature validation for VideoData {id} succeeded",
                    videoDataId);
            }
            catch (InvalidSignatureForVideoTypeException ex)
            {
                videoData.UploadData.BlobData.SanitizationData
                    .History.AddEntry("Signature not verified, invalid file", true);

                await _videoFormatRepository.SaveChangesAsync(context.CancellationToken);

                _logger.LogError("Signature validation for VideoData {id} failed: {err}",
                    videoDataId, ex.Message);
                // TODO
                // Schedule blob deletion
            }


            await _blobSanitizationOrchestrator.RequestActionAsync
            (
                new SanitizationStageResult()
                {
                    IsSuccessful = true,
                    VideoData = videoData,
                    StageType = typeof(SignatureValidationBlobSanitizationJob)
                }
            );
        }

        /// <summary>
        /// Compares header to all supported signatures and returns the video type according
        /// to provided header. Throws if no such signature is supported.
        /// </summary>
        /// <param name="header">Header of the file currently being sanitized.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Video format based on signature found in provided header.</returns>
        /// <exception cref="InvalidSignatureForVideoTypeException"></exception>
        private async Task<VideoFormat> CompareSignaturesAsync(byte[] header,
            CancellationToken cancellationToken = default)
        {
            var formats = await _videoFormatRepository.GetAllAsync(cancellationToken);

            VideoFormat? actualFormat = null;

            foreach (var format in formats)
            {
                if (CompareBytewise(header, format.Signature))
                {
                    actualFormat = format;
                    break;
                }
            }

            if (actualFormat == null)
            {
                throw new InvalidSignatureForVideoTypeException("Not a video header");
            }

            return actualFormat;
        }

        /// <summary>
        /// Compares header to signature byte by byte.
        /// </summary>
        /// <param name="actualBytes">Provided header. Needs to be sufficient size (equal or bigger than
        /// the biggest signature supported).</param>
        /// <param name="formatSignature">Valid format signature.</param>
        /// <returns><c>True</c> if signature was found inside the provided header, <c>False</c>
        /// otherwise.</returns>
        private static bool CompareBytewise(byte[] actualBytes, byte[] formatSignature)
        {
            if (formatSignature.Length > actualBytes.Length)
            {
                return false;
            }

            int sum = 0;

            for (int i = 0; i < formatSignature.Length; i++)
            {
                sum += actualBytes[i] - formatSignature[i];
            }

            return sum == 0;
        }
    }
}
