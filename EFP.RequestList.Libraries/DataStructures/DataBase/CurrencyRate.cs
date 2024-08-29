using Microsoft.EntityFrameworkCore;

namespace EFP.RequestList.Libraries.DataStructures.DataBase
{
    /// <summary>
    /// App currency rate data.
    /// </summary>
    [Index(nameof(DateTimeStart), nameof(DateTimeEnd), AllDescending = true)]
    public class CurrencyRate: BaseEntity
    {
        /// <summary>
        /// Currency this rate applies to.
        /// </summary>
        public uint CurrencyId { get; set; }
        public Currency Currency { get; set; }

        /// <summary>
        /// Currency rate.
        /// </summary>
        public double Rate { get; set; }
        /// <summary>
        /// Currency rate viability period start.
        /// </summary>
        public DateTime DateTimeStart { get; set; }
        /// <summary>
        /// Currency rate viability period end.
        /// </summary>
        public DateTime? DateTimeEnd { get; set; } = null;
    }
}
