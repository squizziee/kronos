using Kronos.Machina.Domain.Entities;

namespace Kronos.Machina.Infrastructure.Data.BlobStorage
{
    /// <summary>
    /// Interface for the identifier returned from persistent storage provider
    /// upon the file save.
    /// </summary>
    public interface IBlobIdentifier
    {
        /// <summary>
        /// Returns a blob name in string format suited for
        /// database provider pesistent storage (fitting into
        /// column type like <c>nvarchar</c>). Name should also
        /// be "reversible" meaning that blob storage identifier 
        /// should be constructed from the name alone.
        /// </summary>
        /// <returns></returns>
        string GetNormalizedName();

        /// <summary>
        /// Returns a name for the file inside the storage provider 
        /// according to its rules.
        /// </summary>
        /// <returns></returns>
        string GetStorageName();
    }
}
