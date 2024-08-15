using EFP.RequestList.Libraries.HelperClasses;

namespace EFP.RequestList.Libraries.DataStructures.DataBase
{
    public class Request
    {
        public uint Id { get; set; }

        public DateTime DateTime { get; set; }

        public double ValueBase { get; set; }

        public double ValueCurrency { get; set; }

        public uint CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public uint RequestedContentId { get; set; }
        public RequestedContent RequestedContent { get; set; }
    }
}
