using System;
using PortfolioTracker.Core.Enums;

namespace Portfolio.Tracker.Core.Entities
{
    public class PortfolioEntity
    {
        public string Symbol { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType TransactionType { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Cost => Quantity * Price;

    }
}