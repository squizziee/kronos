using AutoMapper;
using Kronos.Machina.Contracts.Dto;
using Kronos.Machina.Contracts.Queries;
using Kronos.Machina.Domain.Entities;
using Kronos.Machina.Domain.Repositories;
using MediatR;

namespace Kronos.Machina.Application.Handlers
{
    public class GetFullVideoDataByIdQueryHandler : 
        IRequestHandler<GetFullVideoDataByIdQuery, IEnumerable<FullVideoDataDto>>
    {
        private readonly IVideoDataRepository _videoDataRepository;
        private readonly IMapper _mapper;

        public GetFullVideoDataByIdQueryHandler(IVideoDataRepository videoDataRepository,
            IMapper mapper)
        {
            _videoDataRepository = videoDataRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FullVideoDataDto>> Handle(GetFullVideoDataByIdQuery request, 
            CancellationToken cancellationToken)
        {
            var data = await _videoDataRepository.GetAllAsync(cancellationToken);

            return data.Select(_mapper.Map<VideoData, FullVideoDataDto>);
        }
    }
}
