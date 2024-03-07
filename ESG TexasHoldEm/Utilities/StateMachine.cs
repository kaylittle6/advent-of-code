namespace TexasHoldEm.Utilities
{
  public enum GameStates
  {
    PreFlop,
    PreTurn,
    PreRiver,
    FinalBets
  }

  public enum Commands
  {
    NextPhase
  }
  
  public class ProcessState
  {
    private Dictionary<(GameStates, Commands), GameStates> _transitions =
      new Dictionary<(GameStates, Commands), GameStates>()
      {
        {(GameStates.PreFlop, Commands.NextPhase), GameStates.PreTurn},
        {(GameStates.PreTurn, Commands.NextPhase), GameStates.PreRiver},
        {(GameStates.PreRiver, Commands.NextPhase), GameStates.FinalBets},
        {(GameStates.FinalBets, Commands.NextPhase), GameStates.PreFlop},
        
      };


  }
}
