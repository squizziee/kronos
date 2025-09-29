using AutoMapper;
using Kronos.Machina.Domain.Entities;
using Kronos.Machina.Contracts.Dto;

namespace Kronos.Machina.Application.Mappers
{
    public class VideoDataMapperProfile : Profile
    {
        public VideoDataMapperProfile()
        {
            CreateMap<VideoData, FullVideoDataDto>();
            CreateMap<VideoUploadData, VideoUploadDataDto>();
            CreateMap<BlobData, BlobDataDto>();
            CreateMap<SanitizationData, SanitizationDataDto>();
            CreateMap<BlobSanitizationHistory, BlobSanitizationHistoryDto>();
            CreateMap<BlobSanitizationHistoryEntry, BlobSanitizationHistoryEntryDto>();
            CreateMap<VideoFormat, VideoFormatDto>();
        }
    }
}
