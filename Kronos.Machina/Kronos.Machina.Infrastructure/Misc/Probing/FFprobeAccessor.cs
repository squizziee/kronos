using FFMpegCore;
using Kronos.Machina.Infrastructure.ConfigOptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Kronos.Machina.Infrastructure.Misc.Probing
{
    public class FFprobeAccessor : IProbeAccessor
    {
        private readonly FFmpegConfig _config;
        private readonly ILogger<FFprobeAccessor> _logger;

        public FFprobeAccessor(IOptionsSnapshot<FFmpegConfig> options,
            ILogger<FFprobeAccessor> logger)
        {
            _config = options.Value;
            _logger = logger;
        }

        public ProbeResult Probe(string blobPath)
        {
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = _config.ProbePath,
                    Arguments = $"-v error -show_entries \"format=duration:stream=index,codec_name,codec_type,width,height,bit_rate,avg_frame_rate,bits_per_raw_sample,channels,channel_layout,sample_rate\" -of json {blobPath}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                var process = new Process { StartInfo = startInfo };
                process.Start();

                var json = process.StandardOutput.ReadToEnd();

                //process.WaitForExit();

                return ProbeResult.Deserialize(json);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("FFprobe failed to start, error: {err}",
                    ex.Message);

                throw;
            }
        }
    }
}
