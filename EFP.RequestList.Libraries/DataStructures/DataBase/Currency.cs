namespace EFP.RequestList.Libraries.DataStructures.DataBase
{
    public class Currency
    {
        public uint Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<CurrencyRate> CurrencyRates { get; set; } = [];

        public CurrencyRate CurrentRate => GetCurrentRate();

        private CurrencyRate GetCurrentRate()
        {
            if (CurrencyRates.Count == 0)
                DataBaseManager.QueryRates(this);

            return CurrencyRates.OrderByDescending(cr => cr.DateTimeStart).First();
        }
    }
}
