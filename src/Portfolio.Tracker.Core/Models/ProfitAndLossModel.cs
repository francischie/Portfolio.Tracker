using System;

namespace Portfolio.Tracker.Core.Models
{
    public class ProfitAndLossModel
    {
        public string Symbol { get; set; }
        public DateTime AsOfDate { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal MarketValue { get; set; }
        public decimal PreviousClose { get; set; }
        public decimal DailyProfitAndLost { get; set; }
        public decimal InceptionProfitAndLost { get; set; }
    }
}