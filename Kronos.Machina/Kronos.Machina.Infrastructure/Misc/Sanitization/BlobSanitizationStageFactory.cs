using Kronos.Machina.Infrastructure.Jobs;

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
                _ => throw new Exception("aaaa"),// TODO
            };
        }
    }
}
