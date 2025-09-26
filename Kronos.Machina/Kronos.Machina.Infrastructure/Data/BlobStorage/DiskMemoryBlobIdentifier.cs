using Kronos.Machina.Domain.Entities;

namespace Kronos.Machina.Infrastructure.Data.BlobStorage
{
    public struct DiskMemoryBlobIdentifier : IBlobIdentifier
    {
        private readonly string _name;
        private const string SUFFIX = ".v.blob";

        private DiskMemoryBlobIdentifier(string name)
        {
            _name = name;
        }

        public static DiskMemoryBlobIdentifier Construct(string databaseId)
        {
            return new DiskMemoryBlobIdentifier(databaseId + SUFFIX);
        }

        public static DiskMemoryBlobIdentifier NewIdentifier()
        {
            return new DiskMemoryBlobIdentifier(Guid.NewGuid().ToString() + SUFFIX);
        }

        public static DiskMemoryBlobIdentifier NewIdentifier(Guid guid)
        {
            return new DiskMemoryBlobIdentifier(guid.ToString() + SUFFIX);
        }

        public readonly string GetNormalizedName()
        {
            return _name.Substring(0, _name.Length - SUFFIX.Length);
        }

        public string GetStorageName()
        {
            return _name;
        }
    }
}
