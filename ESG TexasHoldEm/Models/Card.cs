using TexasHoldEm.Interfaces;

namespace TexasHoldEm.Models
{
    public class Card(string cardNumber, string cardSuit) : ICard
    { 
      public string CardSuit { get; } = cardSuit;
      public string CardNumber { get; } = cardNumber;
      public string Display => $"{CardNumber} of {CardSuit}";
      public int CardValue { get; }
    }
}
