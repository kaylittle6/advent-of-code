namespace BJ
{
  public class Card
  {
    public string? Display => GetCardDisplay();
    public string CardNumber { get; set; }
    public string CardSuit { get; set; }
    public int? CardValue
    {
      get
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
            return 10;
          case "J":
            return 11;
          case "Q":
            return 12;
          case "K":
            return 13;
          case "A":
            return 14;
          default:
            return null;
        }
      }
    }

    public Card(string cardNumber, string cardSuit)
    {
      CardNumber = cardNumber;
      CardSuit = cardSuit;
    }

    private string GetCardDisplay()
    {
      return $"{CardNumber}{CardSuit.First()}";
    }
  }
}
