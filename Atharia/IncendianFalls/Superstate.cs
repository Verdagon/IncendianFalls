using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public class Superstate {
    //public class NavigatingState {
    //  public readonly List<Location> path;

    //  public NavigatingState(List<Location> path) {
    //    this.path = path;
    //  }
    //}

    public Game game;

    // Views
    public LevelSuperstate levelSuperstate;

    // Extra-model state

    // 0th element is the first turn of the game.
    public List<RootIncarnation> previousTurns;
    // The requests to get to each turn. There will be one less element in here
    // than in previousTurns. For example, if previousTurns has 2, this will have 1.
    public List<IRequest> requests;
    public List<int> anchorTurnIndices;

    //public NavigatingState navigatingState;

    public Superstate(
        Game game,
        LevelSuperstate levelSuperstate,
        List<RootIncarnation> previousTurns,
        List<IRequest> requests,
        List<int> anchorTurnIndices
        //NavigatingState navigatingState
      ) {
      this.game = game;
      this.levelSuperstate = levelSuperstate;
      this.previousTurns = previousTurns;
      this.requests = requests;
      this.anchorTurnIndices = anchorTurnIndices;
      //this.navigatingState = navigatingState;
    }
  }
}
