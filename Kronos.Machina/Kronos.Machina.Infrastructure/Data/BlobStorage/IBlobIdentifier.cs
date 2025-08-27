using Kronos.Machina.Domain.Entities;

namespace Kronos.Machina.Infrastructure.Data.BlobStorage
{
    public interface IBlobIdentifier
    {
        string ToStorageName();
    }
}
