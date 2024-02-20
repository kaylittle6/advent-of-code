﻿namespace BJ
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

    private string GetCardDisplay() => $"{CardNumber} of {CardSuit}";

    private int GetCardValue()
    {
      if (int.TryParse(CardNumber, out var value))
      {
        return value;
      }
      else if (CardNumber == "Ace")
      {
        return 11;
      }
      else
      {
        return 10;
      }
    }

    public void ChangeAceValueToOne()
    {
      CardValue = IsAce ? 1 : CardValue;
    }
  }
}
