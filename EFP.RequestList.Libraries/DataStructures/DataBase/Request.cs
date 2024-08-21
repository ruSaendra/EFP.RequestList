using EFP.RequestList.Libraries.HelperClasses;

namespace EFP.RequestList.Libraries.DataStructures.DataBase
{
    public class Request
    {
        public uint Id { get; set; }

        public DateTime DateTime { get; set; } = DateTime.UtcNow;

        public double ValueBase { get; set; } = 0;

        public double ValueCurrency { get; set; } = 0;

        public uint CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public uint RequestedContentId { get; set; }
        public RequestedContent RequestedContent { get; set; }

        public string Description { get; set; }

        public Request() { }

        public Request(double value, Currency currency)
        {
            Currency = currency;
            ValueCurrency = value;
            ValueBase = ValueCurrency.CurrentyToInternal(Currency, DateTime);
        }
    }
}
