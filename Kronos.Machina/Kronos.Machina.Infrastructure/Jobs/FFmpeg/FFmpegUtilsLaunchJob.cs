using Kronos.Machina.Infrastructure.ConfigOptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using System.Diagnostics;

namespace Kronos.Machina.Infrastructure.Jobs.FFmpeg
{
    public class FFmpegUtilsLaunchJob : IJob
    {
        private readonly FFmpegConfig _config;
        private readonly ILogger<FFmpegUtilsLaunchJob> _logger;
        public static Process? Process { get; private set; }

        public FFmpegUtilsLaunchJob(IOptionsSnapshot<FFmpegConfig> options,
            ILogger<FFmpegUtilsLaunchJob> logger)
        {
            _config = options.Value;
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("FFprobe is about to start, path provided: {path}", 
                _config.ProbePath);

            var startInfo = new ProcessStartInfo
            {
                FileName = _config.ProbePath,
                Arguments = "",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                Process = new Process { StartInfo = startInfo };
                Process.Start();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("FFprobe failed to start, error: {err}", 
                    ex.Message);

                Environment.Exit(1);
            }

            return Task.CompletedTask;
        }

        public static void KillProcess()
        {
            Process?.Kill();
        }
    }
}
