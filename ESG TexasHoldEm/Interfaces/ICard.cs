namespace TexasHoldEm.Interfaces
{
  public interface ICard
  {
    string CardSuit { get; }
    string CardNumber { get; }
    string Display => $"{CardNumber} of {CardSuit}";
    int CardValue { get; }
  }
}

