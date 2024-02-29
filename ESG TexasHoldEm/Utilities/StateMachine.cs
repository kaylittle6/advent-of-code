namespace TexasHoldEm.Utilities
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
  
  public class ProcessState
  {
    private Dictionary<(GameStates, Commands), GameStates> _transitions =
      new Dictionary<(GameStates, Commands), GameStates>()
      {
        {(GameStates.Active, Commands.Pause), GameStates.Inactive},
        {(GameStates.Inactive, Commands.Resume), GameStates.Active},
        {(GameStates.Active, Commands.Exit), GameStates.Exited},
        
        {(GameStates.Inactive, Commands.Start), GameStates.Active},
        {(GameStates.Inactive, Commands.Exit), GameStates.Exited},
        {(GameStates.Paused, Commands.Start), GameStates.Active},
        {(GameStates.Paused, Commands.End), GameStates.Inactive},
        {(GameStates.Paused, Commands.Exit), GameStates.Exited},
        {(GameStates.Exited, Commands.Exit), GameStates.Exited}
      };


  }
}
