namespace Kronos.Machina.Domain.Entities
{
    public enum BlobSanitizationState
    {
        Unsanitized,
        SignatureConfirmed,
        FormatConfirmed,
        DataConfirmed,
        Invalid,
    }
}
