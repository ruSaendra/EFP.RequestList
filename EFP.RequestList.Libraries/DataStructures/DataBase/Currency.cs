namespace EFP.RequestList.Libraries.DataStructures.DataBase
{
    /// <summary>
    /// App currency data.
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// Currency ID
        /// </summary>
        public uint Id { get; set; }
        /// <summary>
        /// Currency name
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// List of currency rates relative to internal.
        /// </summary>
        public List<CurrencyRate> CurrencyRates { get; set; } = [];

        /// <summary>
        /// Current currency rate
        /// </summary>
        public CurrencyRate CurrentRate => GetCurrentRate();

        private CurrencyRate GetCurrentRate()
        {
            if (CurrencyRates.Count == 0)
                DataBaseManager.QueryRates(this);

            return CurrencyRates.OrderByDescending(cr => cr.DateTimeStart).First();
        }
    }
}
