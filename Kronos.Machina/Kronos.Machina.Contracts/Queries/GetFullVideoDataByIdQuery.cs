using Kronos.Machina.Contracts.Dto;
using MediatR;

namespace Kronos.Machina.Contracts.Queries
{
    public class GetFullVideoDataByIdQuery : IRequest<IEnumerable<FullVideoDataDto>>
    {
    }
}
