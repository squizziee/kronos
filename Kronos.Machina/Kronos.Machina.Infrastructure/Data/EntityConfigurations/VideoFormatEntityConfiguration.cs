using Kronos.Machina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kronos.Machina.Infrastructure.Data.EntityConfigurations
{
    public class VideoFormatEntityConfiguration : IEntityTypeConfiguration<VideoFormat>
    {
        public void Configure(EntityTypeBuilder<VideoFormat> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(256);

            builder
                .Property(x => x.Extension)
                .HasMaxLength(10);

            builder
                .Property(x => x.Signature)
                .HasMaxLength(30);

            builder
                .HasData
                (
                    new VideoFormat()
                    {
                        Id = new Guid("12345678-1234-1234-1234-123456789abc"),
                        Name = "MP4",
                        Extension = ".mp4",
                        Signature = [0x00, 0x00, 0x00, 0x18, 0x66, 0x74, 0x79, 0x70],
                        MandatorySignatureByteIndexes = [4, 5, 6, 7]
                    },
                    new VideoFormat()
                    {
                        Id = new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                        Name = "AVI",
                        Extension = ".avi",
                        Signature = [0x52, 0x49, 0x46, 0x46, 0x00, 0x00, 0x00, 0x00, 0x41, 0x56, 0x49, 0x20],
                        MandatorySignatureByteIndexes = [0, 1, 2, 3, 8, 9, 10, 11]
                    },
                    new VideoFormat()
                    {
                        Id = new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                        Name = "MOV (QuickTime)",
                        Extension = ".mov",
                        Signature = [0x00, 0x00, 0x00, 0x14, 0x66, 0x74, 0x79, 0x70, 0x71, 0x74, 0x20, 0x20],
                        MandatorySignatureByteIndexes = [4, 5, 6, 7, 8, 9]
                    },
                    new VideoFormat()
                    {
                        Id = new Guid("550e8400-e29b-41d4-a716-446655440000"),
                        Name = "MPEG",
                        Extension = ".mpeg",
                        Signature = [0x00, 0x00, 0x01, 0xBA],
                        MandatorySignatureByteIndexes = [0, 1, 2, 3]
                    }
                );
        }
    }
}
