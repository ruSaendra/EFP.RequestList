namespace EFP.RequestList.Libraries.DataStructures.DataBase
{
    /// <summary>
    /// App currency data.
    /// </summary>
    public class Currency: BaseEntity
    {
        /// <summary>
        /// Currency name
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// List of currency rates relative to internal.
        /// </summary>
        public List<CurrencyRate> CurrencyRates { get; set; } = [];

        /// <summary>
        /// Get current currency rate
        /// </summary>
        /// <returns></returns>
        public CurrencyRate GetCurrentRate() 
            => CurrencyRates
                .OrderByDescending(cr => cr.DateTimeStart)
                .First();

        public CurrencyRate GetRateAtTimeStamp(DateTime timeStamp) 
            => CurrencyRates
                .Where(cr => cr.DateTimeStart < timeStamp && (cr.DateTimeEnd == null || cr.DateTimeEnd > timeStamp))
                .OrderByDescending(cr => cr.DateTimeStart)
                .First();
    }
}
