using Kronos.Machina.Domain.Entities;

namespace Kronos.Machina.Domain.Misc.Sanitization
{
    /// <summary>
    /// Manages sanitization cycle for a supposed video file.
    /// </summary>
    public interface IBlobSanitizationOrchestrator
    {
        /// <summary>
        /// Starts the cycle of sanitization for a provided video file.
        /// Should be called after new <see cref="VideoData"/> was initialized and
        /// saved to database.
        /// </summary>
        /// <param name="videoData">New <see cref="VideoData"/> entity that was just added to database.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RequestSanitizationAsync(VideoData videoData, CancellationToken cancellationToken = default);

        /// <summary>
        /// Serves as a messenger to the orchestrator that a stage was completed
        /// and there is a decision to be made.
        /// </summary>
        /// <param name="stageResult">Work result of the stage that just finished</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RequestActionAsync(SanitizationStageResult stageResult, 
            CancellationToken cancellationToken = default);
    }
}
