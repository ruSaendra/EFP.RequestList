using EFP.RequestList.Libraries.HelperClasses;

namespace EFP.RequestList.Libraries.DataStructures.DataBase
{
    public class Request
    {
        public uint Id { get; set; }

        public double Value { get; set; }

        public double InternalValue => Value.CurrentyToInternal(Currency);

        public uint CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public uint RequestedContentId { get; set; }
        public RequestedContent RequestedContent { get; set; }
    }
}
