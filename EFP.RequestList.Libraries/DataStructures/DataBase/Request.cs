using EFP.RequestList.Libraries.HelperClasses;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace EFP.RequestList.Libraries.DataStructures.DataBase
{
    /// <summary>
    /// Content request data.
    /// </summary>
    [Index(nameof(ArchiveTimeStamp), AllDescending = true)]
    public class Request: BaseEntity
    {
        /// <summary>
        /// Content request time.
        /// </summary>
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Request value in internal currency.
        /// </summary>
        public double ValueBase { get; set; } = 0;
        /// <summary>
        /// Request value in used currency.
        /// </summary>
        public double? ValueCurrency { get; set; } = 0;
        /// <summary>
        /// Currency used for making request.
        /// </summary>
        public uint? CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        /// <summary>
        /// Requested content.
        /// </summary>
        public uint RequestedContentId { get; set; }
        public RequestedContent RequestedContent { get; set; }
        /// <summary>
        /// Requesting user alias.
        /// </summary>
        public uint? UserAliasId { get; set; }
        public UserAlias? UserAlias { get; set; }
        /// <summary>
        /// Request description.
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// DateTime of request archivation.
        /// </summary>
        public DateTime? ArchiveTimeStamp { get; set;}
        /// <summary>
        /// Whether request is archived.
        /// </summary>
        public bool IsArchived => ArchiveTimeStamp != null;

        public Request() { }

        /// <summary>
        /// Constructor with internal currency as a base.
        /// </summary>
        /// <param name="value"></param>
        public Request(double value)
        {
            ValueBase = value;
        }

        /// <summary>
        /// Cunstructor with specified currency as a base.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="currency"></param>
        public Request(double value, Currency currency)
        {
            Currency = currency;
            ValueCurrency = value;
            ValueBase = ((double)ValueCurrency).CurrentyToInternal(Currency, DateTime);
        }
    }
}
