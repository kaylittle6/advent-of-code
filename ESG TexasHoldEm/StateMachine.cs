namespace TexasHoldEm
{
  public static class StateMachine
  {
    public enum GameStates
    {
      Active,
      Inactive,
      Paused,
      Exited
    }

    public enum Commands
    {
      Start,
      End,
      Pause,
      Resume,
      Exit
    }
  }
  
}
