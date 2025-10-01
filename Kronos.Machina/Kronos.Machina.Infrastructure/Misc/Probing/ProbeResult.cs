using System.Globalization;
using System.Text.Json;

namespace Kronos.Machina.Infrastructure.Misc.Probing
{
    public readonly struct ProbeResult
    {
        public required TimeSpan Duration { get; init; }
        public required int Width { get; init; }
        public required int Height { get; init; }
        public required double FramesPerSecond { get; init; }
        public required int VideoBitrate { get; init; }
        public required string VideoCodecName { get; init; }
        public required int BitDepth { get; init; }
        public required string AudioCodecName { get; init; }
        public required string AudioChannels { get; init; }
        public required int AudioSampleRate { get; init; }
        public required int AudioBitrate { get; init; }

        public static ProbeResult Deserialize(string json)
        {
            var deserialized = JsonSerializer
                .Deserialize<FFmpegResponse>(json);

            var videoChannel = 
                (deserialized!.streams)!
                    .Where(stream => stream.codec_type == "video").FirstOrDefault();

            var audioChannel =
                (deserialized!.streams)!
                    .Where(stream => stream.codec_type == "audio").FirstOrDefault();

            if (videoChannel == null)
            {
                // TODO
                throw new Exception("olololo");
            }

            if (audioChannel == null)
            {
                // TODO
                throw new Exception("olololo");
            }

            var tmp = new ProbeResult
            {
                Duration = TimeSpan.FromSeconds(double.Parse(deserialized.format.duration, 
                    CultureInfo.InvariantCulture)),
                Width = videoChannel.width!.Value,
                Height = videoChannel.height!.Value,
                // format is "avg_frame_rate": "30/1", so we take 30 out of the
                // "30/1"
                FramesPerSecond = Double.Parse((videoChannel.avg_frame_rate as string)!
                    .Split('/')[0]),
                VideoBitrate = int.Parse(videoChannel.bit_rate!),
                VideoCodecName = videoChannel.codec_name!,
                BitDepth = int.Parse(videoChannel.bits_per_raw_sample!),
                AudioCodecName = audioChannel.codec_name!,
                AudioChannels = $"{audioChannel.channels}, {audioChannel.channel_layout}",
                AudioBitrate = int.Parse(audioChannel.bit_rate!),
                AudioSampleRate = int.Parse(audioChannel.sample_rate!)
            };

            return tmp;
        }
    }

    file class FFmpegResponse
    {
        public IEnumerable<FFmpegResponseChannel> streams { get; set; } = null!;
        public FFmpegResponseFormat format { get; set; } = null!;
    }

    file class FFmpegResponseFormat
    {
        public string duration { get; set; }
    }

    file class FFmpegResponseChannel
    {
        public string? codec_name { get; set; }
        public string? codec_type { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public string? avg_frame_rate { get; set; }
        public string? bit_rate { get; set; }
        public string? bits_per_raw_sample { get; set; }

        public string? sample_rate { get; set; }
        public int? channels { get; set; }
        public string? channel_layout { get; set; }
    }
}
