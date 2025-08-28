namespace Kronos.Machina.Domain.Entities
{
    /// <summary>
    /// Embodies blob sanitization cycle stages. Includes four required stages
    /// and one auxiliary.
    /// </summary>
    public enum BlobSanitizationState
    {
        /// <summary>
        /// Blob state before any sanitization was done.
        /// </summary>
        Unsanitized,

        /// <summary>
        /// Blob state after the signature was confirmed.
        /// </summary>
        SignatureConfirmed,

        /// <summary>
        /// Blob state after the file was scanned by specialized software.
        /// </summary>
        FormatConfirmed,

        /// <summary>
        /// Blob state after the file was scanned for embedded malicious code.
        /// It is the final state of sanitization and the only acceptable state
        /// for any further actions.
        /// </summary>
        DataConfirmed,

        /// <summary>
        /// Blob state after any sanitization errors during sanitization cycle.
        /// </summary>
        Invalid,
    }
}
