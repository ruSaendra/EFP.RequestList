using EFP.RequestList.Libraries.Enums;

namespace EFP.RequestList.Libraries.DataStructures.DataBase
{
    public class RequestedContent
    {
        public uint Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public RequestedContentType Type { get; set; }

        public List<Request> Requests { get; set; } = [];
    }
}
