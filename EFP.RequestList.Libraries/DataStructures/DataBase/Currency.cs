﻿namespace EFP.RequestList.Libraries.DataStructures.DataBase
{
    public class Currency
    {
        public uint Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<CurrencyRate> CurrencyRates { get; set; } = [];
    }
}
