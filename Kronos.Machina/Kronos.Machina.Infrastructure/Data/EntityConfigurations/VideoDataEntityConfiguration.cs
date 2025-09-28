using Kronos.Machina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Kronos.Machina.Infrastructure.Data.EntityConfigurations
{
    public class VideoDataEntityConfiguration : IEntityTypeConfiguration<VideoData>
    {
        public void Configure(EntityTypeBuilder<VideoData> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.VideoFormat)
                .WithMany()
                .HasForeignKey(x => x.VideoFormatId);

            builder
                .Property(vd => vd.AvailableImageQuality)
                .HasConversion(
                    aiq => JsonSerializer.Serialize(aiq, JsonSerializerOptions.Default),
                    aiq => JsonSerializer.Deserialize<ICollection<VideoImageQuality>>
                        (aiq, JsonSerializerOptions.Default)!
                );

            builder
                .OwnsOne(
                    vd => vd.UploadData, 
                    builder => builder.OwnsOne(
                        ud => ud.BlobData, 
                        builder => builder.OwnsOne(
                            bd => bd.SanitizationData,
                            builder => builder.OwnsOne(
                                sd => sd.History,
                                builder => builder.OwnsMany(
                                    h => h.Entries
                                )
                            )
                        )
                    )
                );
        }
    }
}
