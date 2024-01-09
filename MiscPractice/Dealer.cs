namespace MiscPractice
{
  public class Dealer
  {
    public int SmallBlind { get; set; } = 25;
    public int BigBlind { get; set; } = 50;
    public int DealerButton { get; set; } = 0;
    public int SmallBlindIndex { get; set; } = 1;
    public int BigBlindIndex { get; set; } = 2;
    public HandRankings HandRankings { get; set; }

    public Dealer()
    {
      HandRankings = new HandRankings();
    }

    public void DistributePlayerMoney(Game game, int totalMoney)
    {
      int splitMoney = totalMoney / game.Players.Count();
      
      foreach (var player in game.Players)
      {
        player.Money = splitMoney;
      }

      if (game.Players.Count < 3)
      {
        DealerButton = SmallBlindIndex;
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
          var randomIndex = random.Next(game.Deck.Count);
          var randomCard = game.Deck.ElementAt(randomIndex);

          player.Cards.Add(randomCard.Value);
          game.Deck.Remove(randomCard.Key);
        }
      }
    }

    public void DealFlopCards(Game game)
    {



    }

    public void DealTurnOrRiverCard(Game game)
    {



    }

    public void RaiseBlinds()
    {
      SmallBlind *= 2;
      BigBlind *= 2;
    }

    public void ResetBlinds()
    {
      SmallBlind = 25;
      BigBlind = 50;
    }

    public void CollectBlinds(Game game)
    {
      game.Players[SmallBlindIndex].Money -= SmallBlind;
      game.Players[BigBlindIndex].Money -= BigBlind;

      SmallBlindIndex = SmallBlindIndex + 1 >= game.Players.Count ? 0 : ++SmallBlindIndex;
      BigBlindIndex = BigBlindIndex + 1 >= game.Players.Count ? 0 : ++BigBlindIndex;
    }

    public void RoundOfBets(Game game)
    {
      foreach (var player in game.Players)
      {
        if (player.IsNPC)
        {
          var result = player.MakePreFlopBet(game);

          if (result == "Fold")
          {
            player.Cards.Clear();
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
              var callBet = game.CurrentBet;

              if (player.Money <= callBet)
              {
                game.TotalPot += player.Money;
                player.Money = 0;
              }
              else
              {
                game.TotalPot += callBet;
                player.Money -= callBet;
              }
              break;

            case "2":
              var raiseBet = game.CurrentBet != game.Dealer.BigBlind 
                ? game.CurrentBet * 3 
                : game.Dealer.BigBlind * 3;

              if (player.Money <= raiseBet)
              {
                game.TotalPot += player.Money;
                player.Money = 0;
              }
              else
              {
                game.TotalPot += raiseBet;
                player.Money -= raiseBet;
              }
              break;

            case "3":
              player.Cards.Clear();
              break;
          }
        }
      }
    }
  }
}
