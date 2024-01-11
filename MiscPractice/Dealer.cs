namespace MiscPractice
{
  public class Dealer
  {
    private Game _game { get; set; }

    public Dealer(Game game)
    {
      _game = game;  
    }

    public void DistributePlayerMoney(int totalMoney)
    {
      int splitMoney = totalMoney / _game.Players.Count();
      
      foreach (var player in _game.Players)
      {
        player.Money = splitMoney;
      }

      if (_game.Players.Count < 3)
      {
        GameState.DealerButtonIndex = GameState.SmallBlindIndex;
      }
    }

    public Dictionary<string, Card> CreateDeck()
    {
      Dictionary<string, Card> deck = new Dictionary<string, Card>();
      string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
      string[] suits = { "Hearts", "Diamonds", "Spades", "Clubs" };

      foreach (var value in values)
      {
        foreach (var suit in suits)
        {
          var cardName = $"{value} of {suit}";
          deck.Add(cardName, new Card(value, suit));
        }
      }

      return deck;
    }

    public void DealInitialCards(Game game)
    {
      for (int c = 0; c < 2; c++)
      {
        foreach (var player in game.Players)
        {
          Random random = new();
          var randomIndex = random.Next(game!.Deck!.Count);
          var randomCard = game.Deck.ElementAt(randomIndex);

          player.HoleCards.Add(randomCard.Value);
          game.Deck.Remove(randomCard.Key);
        }
      }
    }

    public void DealFlopCards()
    {



    }

    public void DealTurnOrRiverCard()
    {



    }

    public void RaiseBlinds()
    {
      GameState.CurrentSmallBlind *= 2;
      GameState.CurrentBigBlind *= 2;
    }

    public void ResetBlinds()
    {
      GameState.CurrentSmallBlind = 25;
      GameState.CurrentBigBlind = 50;
    }

    public void CollectBlinds()
    {
      _game.Players[GameState.SmallBlindIndex].Money -= GameState.CurrentSmallBlind;
      _game.Players[GameState.BigBlindIndex].Money -= GameState.CurrentBigBlind;

      GameState.SmallBlindIndex = GameState.SmallBlindIndex + 1 >= _game.Players.Count ? 0 : ++GameState.SmallBlindIndex;
      GameState.BigBlindIndex = GameState.BigBlindIndex + 1 >= _game.Players.Count ? 0 : ++GameState.BigBlindIndex;
    }

    public void RoundOfBets()
    {
      foreach (var player in _game.Players)
      {
        if (player.IsNPC)
        {
          var result = player.MakePreFlopBet(game);

          if (result == "Fold")
          {
            player.HoleCards.Clear();
          }
        }
        else
        {
          Console.WriteLine($"{player.Name}, what would you like to do?");
          Console.WriteLine();
          Console.Write("1. Call -- 2. Raise -- 3. Fold");
          Console.WriteLine();

          var response = Console.ReadLine();

          switch (response)
          {
            case "1":
              var callBet = GameState.CurrentBet;

              if (player.Money <= callBet)
              {
                GameState.CurrentPotTotal += player.Money;
                player.Money = 0;
              }
              else
              {
                GameState.CurrentPotTotal += callBet;
                player.Money -= callBet;
              }
              break;

            case "2":
              var raiseBet = GameState.CurrentBet != GameState.CurrentBigBlind 
                ? GameState.CurrentBet * 3 
                : GameState.CurrentBigBlind * 3;

              if (player.Money <= raiseBet)
              {
                GameState.CurrentPotTotal += player.Money;
                player.Money = 0;
              }
              else
              {
                GameState.CurrentPotTotal += raiseBet;
                player.Money -= raiseBet;
              }
              break;

            case "3":
              player.HoleCards.Clear();
              break;
          }
        }
      }
    }
  }
}
