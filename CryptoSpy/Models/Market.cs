using System;

namespace CryptoSpy.Models
{
    public class Market
    {
        public string ExchangeId { get; set; }
        public string Rank { get; set; }
        public CurrencySymbols BaseSymbol { get; set; }
        public string baseId { get; set; }
        public CurrencySymbols QuoteSymbol { get; set; }
        public string QuoteId { get; set; }
        public decimal PriceQuote { get; set; }
        public decimal PriceUsd { get; set; }
        public decimal VolumeUsd24Hr { get; set; }
        public decimal PercentExchangeVolume { get; set; }
        public decimal TradesCount24Hr { get; set; }
        public DateTime Updated { get; set; }
    }
}
