using Kronos.Machina.Infrastructure.Jobs.Sanitization;

namespace Kronos.Machina.Infrastructure.Misc.Sanitization
{
    public class BlobSanitizationStageFactory
    {
        public BlobSanitizationStage GetStageInstance(string id)
        {
            return id switch
            {
                "SignatureValidation" => new () 
                { 
                    StageType = typeof(SignatureValidationBlobSanitizationJob)
                },
                "InvalidBlob" => new ()
                {
                    StageType = typeof(InvalidBlobDeletionJob)
                },
                "ProbeValidation" => new ()
                {
                    StageType = typeof(ProbeAnalysisBlobSanitizationJob)
                },
                _ => throw new Exception("aaaa"),// TODO
            };
        }
    }
}
