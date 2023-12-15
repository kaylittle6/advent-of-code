namespace MiscPractice
{
  public class HandRankings
  {
    public Dictionary<string, bool> HandRanks { get; set; }

    public HandRankings()
    {
      HandRanks = new Dictionary<string, bool>()
      {
        { "High Card", false },
        { "Pair", false },
        { "Two Pair", false },
        { "Three of a Kind", false },
        { "Straight", false },
        { "Flush", false },
        { "Full House", false },
        { "Four of a Kind", false },
        { "Straight Flush", false },
        { "Royal Flush", false }
      };
    }
  }
}
