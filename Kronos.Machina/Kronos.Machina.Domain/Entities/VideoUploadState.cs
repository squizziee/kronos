namespace Kronos.Machina.Domain.Entities
{
    /// <summary>
    /// Describes a state of a video in upload context of speech. It also
    /// shows the lifecycle of a video inside the system.
    /// <br/><br/>
    /// The first stage of a video is always a blob of raw data (<c>BlobOnly</c>) 
    /// that is placed in the quarantine zone of the disk.Further actions are permitted
    /// only if the blob is sanitized (scanned for malware) and its state is changed to <c>SanitizedBlob</c>.
    /// Video processing is allowed no earlier than this part of lifecycle. The processing of the video is determined by
    /// strategy chosen.  The change of state to <c>ProcessingPrimary</c> is initiated regardless of strategy.
    /// When primary processing is done the state  changes to <c>PrimaryReady</c> and surface-level moderation 
    /// is requested from other service. After this step is out of the way, two courses of action are viable:
    /// if the video is considered valid after surface-level moderation, the state is changed to <c>ProcessingRemaining</c>;
    /// on the other hand, if the video was not in fact valid, the state is changed to <c>Invalid</c>.
    /// After the video processing is completed, the video upload lifecycle ends at <c>Complete</c> state.
    /// </summary>
    public enum VideoUploadState
    {
        BlobOnly,
        SanitizedBlob,
        ProcessingPrimary,
        PrimaryReady,
        ProcessingRemaining,
        Complete,
        Invalid
    }
}
