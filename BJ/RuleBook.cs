using System.Runtime.InteropServices;

namespace BJ
{
  public class RuleBook
  {
    // 0 = Bust, 1 = Win, 2 = No Action
    public bool[] CheckForResult(Player player)
    {
      bool[] results = new bool[3];

      // Did player Bust (more than 21 total)?
      results[0] = player.Cards.Sum(cv => cv.CardValue) > 21
        ? true : false;

      // Did player Win (21 total)?
      results[1] = player.Cards.Sum(cv => cv.CardValue) == 21
        ? true : false;

      // No Action (less than 21 total)?
      results[2] = player.Cards.Sum(cv => cv.CardValue) < 21
        ? true : false;

      return results;
    }
  }
}
