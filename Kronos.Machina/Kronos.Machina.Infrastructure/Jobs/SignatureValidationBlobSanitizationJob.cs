using Kronos.Machina.Domain.Repositories;
using Kronos.Machina.Infrastructure.ConfigOptions;
using Kronos.Machina.Infrastructure.Data.BlobStorage;
using Kronos.Machina.Infrastructure.Exceptions;
using Microsoft.Extensions.Options;
using Quartz;

namespace Kronos.Machina.Infrastructure.Jobs
{
    // TODO 
    // store formats in database
    /// <summary>
    /// The purpose of this sanitization step is to verify the magic numbers in file headers.
    /// The meaning of word <i>"signature"</i> in this case is a series of "magic" 8-bit numbers
    /// (bytes) that are present in every valid video files' header. The way sanitization is 
    /// done is by bytewise signature comparison against supported video formats whose signatures 
    /// are stored in <c>SanitizerSettings.json</c> file.
    /// </summary>
    public class SignatureValidationBlobSanitizationJob : IJob
    {
        private readonly VideoTypeSignatures _videoTypeSignatures;
        private readonly IBlobStorage _blobStorage;
        private readonly IVideoDataRepository _videoDataRepository;

        public SignatureValidationBlobSanitizationJob(IOptionsSnapshot<VideoTypeSignatures> options,
            IBlobStorage blobStorage,
            IVideoDataRepository videoDataRepository)
        {
            _videoTypeSignatures = options.Value;
            _blobStorage = blobStorage;
            _videoDataRepository = videoDataRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var dataMap = context.JobDetail.JobDataMap;
            var blobIdentifier = dataMap.Get("BlobIdentifier") as IBlobIdentifier;

            if (blobIdentifier == null) {
                throw new Exception("No blob identifier provided");
            }

            try
            {
                var blobHeader = await _blobStorage.GetBlobDataAsync(blobIdentifier, 0, 20);
                var format = CompareHeaders(blobHeader);
            }
            catch (InvalidSignatureForVideoTypeException ex)
            {
                // TODO
                // Schedule blob deletion
            }
        }

        private string CompareHeaders(byte[] header)
        {
            if (CompareBytewise(header, _videoTypeSignatures.MP4))
            {
                return "MP4";
            }
            else if (CompareBytewise(header, _videoTypeSignatures.M4A))
            {
                return "M4A";
            }
            else if (CompareBytewise(header, _videoTypeSignatures.AVI))
            {
                return "AVI";
            }
            else if (CompareBytewise(header, _videoTypeSignatures.MOV))
            {
                return "MOV";
            }
            else if (CompareBytewise(header, _videoTypeSignatures.MPEG))
            {
                return "MPEG";
            }
            else if (CompareBytewise(header, _videoTypeSignatures.MPEG2))
            {
                return "MPEG";
            }

            throw new InvalidSignatureForVideoTypeException("Not a video header");
        }

        private bool CompareBytewise(byte[] actualBytes, byte[] formatSignature)
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
