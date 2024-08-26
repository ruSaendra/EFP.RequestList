using EFP.RequestList.Libraries.Enums;

namespace EFP.RequestList.Libraries.DataStructures.DataBase
{
    /// <summary>
    /// Requested contend data.
    /// </summary>
    public class RequestedContent
    {
        /// <summary>
        /// Requested content ID.
        /// </summary>
        public uint Id { get; set; }
        /// <summary>
        /// Requested content name.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Requested content description.
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Requested content name.
        /// </summary>
        public RequestedContentType Type { get; set; }

        /// <summary>
        /// Requests made for this content.
        /// </summary>
        public List<Request> Requests { get; set; } = [];
    }
}
