namespace EFP.RequestList.Libraries.DataStructures.DataBase
{
    public class CurrencyRate
    {
        public uint Id { get; set; }

        public uint CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public double Rate { get; set; }

        public DateTime DateTimeStart { get; set; }

        public DateTime? DateTimeEnd { get; set; } = null;
    }
}
