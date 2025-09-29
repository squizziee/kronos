using Kronos.Machina.Infrastructure.Jobs.Sanitization;

namespace Kronos.Machina.Infrastructure.Misc.Sanitization
{
    public class BlobSanitizationStageFactory
    {
        public BlobSanitizationStage GetStageInstance(string id)
        {
            return id switch
            {
                "SignatureValidation" => new BlobSanitizationStage() 
                { 
                    StageType = typeof(SignatureValidationBlobSanitizationJob)
                },
                "InvalidBlob" => new BlobSanitizationStage()
                {
                    StageType = typeof(InvalidBlobDeletionJob)
                },
                _ => throw new Exception("aaaa"),// TODO
            };
        }
    }
}
