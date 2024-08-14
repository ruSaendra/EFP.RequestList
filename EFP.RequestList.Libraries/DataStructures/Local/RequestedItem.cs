using EFP.RequestList.Libraries.Enums;

namespace EFP.RequestList.Libraries.DataStructures.Local
{
    public class RequestedItem
    {
        public string Name { get; set; } = string.Empty;
        public double Value { get; set; }
        public RequestedContentType Type { get; set; }
    }
}
