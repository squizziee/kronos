using Kronos.Machina.Domain.Entities;

namespace Kronos.Machina.Infrastructure.Data.BlobStorage
{
    public struct DeafultBlobIdentifier : IBlobIdentifier
    {
        private readonly Guid _databaseId;

        public DeafultBlobIdentifier(Guid databaseId)
        {
            _databaseId = databaseId;
        }

        public DeafultBlobIdentifier(string storageName)
        {
            _databaseId = Guid.Parse(storageName.Split(".")[0]);
        }

        public readonly string ToStorageName()
        {
            return _databaseId.ToString() + ".blob";
        }
    }
}
