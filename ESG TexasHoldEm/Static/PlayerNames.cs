using TexasHoldEm.Models;

namespace TexasHoldEm.Static
{
  public static class PlayerNames
  {
    public static List<string> NameList =
    [
      "Ed",
      "Alex",
      "Samuel",
      "Donte",
      "Lamont",
      "Guillermo",
      "Kyle",
      "Enzo",
      "Brendon",
      "Grayson",
      "Gilberto",
      "Ricardo",
      "Matt",
      "Chris",
      "Marlowe",
      "Jade",
      "Chasity",
      "Heidi",
      "Jessie",
      "Harper",
      "Meghan",
      "Nadia",
      "Deja",
      "Lilianna",
      "Emelia",
      "Cherish",
      "Kaylin",
      "Seraphina",
      "Eleanora",
      "Paula"
    ];
    
    public static string PickName()
    {
      var randomIndex = new Random().Next(30);
      return NameList[randomIndex];
    }
  }
}
