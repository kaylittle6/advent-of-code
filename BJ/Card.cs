namespace BJ
{
  public class Card
  {
    public string Display => GetCardDisplay();
    public string CardNumber { get; set; }
    private string CardSuit { get; set; }
    public int CardValue { get; private set; }
    public bool IsAce => CardNumber == "Ace";
    public bool IsFaceDown { get; set; }

    public Card(string cardNumber, string cardSuit)
    {
      CardNumber = cardNumber;
      CardSuit = cardSuit;
      CardValue = GetCardValue();
    }

    private string GetCardDisplay()
    {
      return $"{CardNumber} of {CardSuit}";
    }

    private int GetCardValue()
    {
      switch (CardNumber)
      {
        case "2":
          return 2;
        case "3":
          return 3;
        case "4":
          return 4;
        case "5":
          return 5;
        case "6":
          return 6;
        case "7":
          return 7;
        case "8":
          return 8;
        case "9":
          return 9;
        case "10":
        case "Jack":
        case "Queen":
        case "King":
          return 10;
        case "Ace":
          return 11;
        default:
          return 0;
      }
    }

    public void ChangeAceValueToOne()
    {
      if (IsAce)
      {
        CardValue = 1;
      }
    }
  }
}
