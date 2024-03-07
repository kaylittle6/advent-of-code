using TexasHoldEm.Models;

namespace TexasHoldEm.Static
{
  public static class PlayerNames
  {
    public static List<string> NameList =
    [
      "Yadiel",
      "Matthias",
      "Samuel",
      "Donte",
      "Lamont",
      "Guillermo",
      "Lane",
      "Enzo",
      "Brendon",
      "Grayson",
      "Gilberto",
      "Ricardo",
      "Zora",
      "Ruth",
      "Marlowe",
      "Jade",
      "Chasity",
      "Heidi",
      "Jaylen",
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
