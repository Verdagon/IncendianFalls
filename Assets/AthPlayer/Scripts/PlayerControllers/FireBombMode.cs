using System;
using Atharia.Model;
using Domino;

namespace AthPlayer {
  public class FireBombMode : IMode {
    ISuperstructure ss;
    Superstate superstate;
    Game game;
    IModeDelegate delegat;
    NarrationPanelView narrator;

    public FireBombMode(
        ISuperstructure ss,
        Superstate superstate,
        Game game,
        IModeDelegate delegat,
        NarrationPanelView narrator) {
      this.ss = ss;
      this.superstate = superstate;
      this.game = game;
      this.delegat = delegat;
      this.narrator = narrator;

      narrator.ShowMessage("Preparing to fire bomb! Select a location.");
    }

    private Unit FindUnitAtLocation(Location location) {
      foreach (var unit in game.level.units) {
        if (unit.location.Equals(location)) {
          return unit;
        }
      }
      return Unit.Null;
    }

    public void OnTileMouseClick(Location location) {
      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        ss.GetRoot().logger.Error("Not your turn!");
        delegat.AfterDidSomething();
        delegat.SwitchToNormalMode();
        return;
      }

      string result = ss.RequestFireBomb(game.id, location);
      if (result.Length > 0) {
        narrator.ShowMessage(result);
        delegat.SwitchToNormalMode();
        delegat.AfterDidSomething();
        return;
      }

      narrator.ClearMessage();
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void DefyClicked() {
      narrator.ShowMessage("You must select a location to fire bomb on. Canceling fire bomb!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void ActivateCheat(string cheatName) {
      narrator.ShowMessage("You must select a location to fire bomb on. Canceling fire bomb!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void FireBombClicked() {
      narrator.ShowMessage("Canceled fire bomb!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void FireClicked() {
      narrator.ShowMessage("You must select a location to fire bomb on. Canceling fire bomb!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void MireClicked() {
      narrator.ShowMessage("You must select a location to fire bomb on. Canceling fire bomb!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void CounterClicked() {
      narrator.ShowMessage("You must select a location to fire bomb on. Canceling fire bomb!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void InteractClicked() {
      narrator.ShowMessage("You must select a location to fire bomb on. Canceling fire bomb!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void TimeShiftClicked() {
      narrator.ShowMessage("You must select a location to fire bomb on. Canceling fire bomb!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void TimeAnchorMoveClicked() {
      narrator.ShowMessage("You must select a location to fire bomb on. Canceling fire bomb!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void ReadyForTurn() {
      Asserts.Assert(false); // curiosity
    }
  }
}
