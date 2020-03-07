using System;
using Atharia.Model;
using Domino;

namespace AthPlayer {
  public class MireMode : IMode {
    ISuperstructure ss;
    Superstate superstate;
    Game game;
    IModeDelegate delegat;
    NarrationPanelView narrator;

    public MireMode(
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

      narrator.ShowMessage("Preparing to fire! Select an unit to fire at.");
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

      var unit = FindUnitAtLocation(location);
      if (!unit.Exists()) {
        narrator.ShowMessage("No unit there. Canceling slow!");
        delegat.SwitchToNormalMode();
        delegat.AfterDidSomething();
        return;
      }

      string result = ss.RequestMire(game.id, unit.id);
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
      narrator.ShowMessage("You must select a unit to slow them. Canceling slow!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void ActivateCheat(string cheatName) {
      narrator.ShowMessage("You must select a unit to slow them. Canceling slow!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void FireClicked() {
      narrator.ShowMessage("Canceled fire!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void MireClicked() {
      narrator.ShowMessage("You must select a unit to slow them. Canceling slow!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void CounterClicked() {
      narrator.ShowMessage("You must select a unit to slow them. Canceling slow!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void InteractClicked() {
      narrator.ShowMessage("You must select a unit to slow them. Canceling slow!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void TimeShiftClicked() {
      narrator.ShowMessage("You must select a unit to slow them. Canceling slow!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void TimeAnchorMoveClicked() {
      narrator.ShowMessage("You must select a unit to slow them. Canceling slow!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void ReadyForTurn() {
      Asserts.Assert(false); // curiosity
    }
  }
}
