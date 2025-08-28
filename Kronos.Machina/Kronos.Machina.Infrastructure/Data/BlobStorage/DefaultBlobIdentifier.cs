using Kronos.Machina.Domain.Entities;

namespace Kronos.Machina.Infrastructure.Data.BlobStorage
{
    public struct DefaultBlobIdentifier : IBlobIdentifier
    {
        private readonly Guid _databaseId;

        public DefaultBlobIdentifier(Guid databaseId)
        {
            _databaseId = databaseId;
        }

        public DefaultBlobIdentifier(string storageName)
        {
            _databaseId = Guid.Parse(storageName.Split(".")[0]);
        }

        public readonly string ToStorageName()
        {
            return _databaseId.ToString() + ".blob";
        }
    }
}
