using System;
using Atharia.Model;
using Domino;

namespace AthPlayer {
  public class TimeAnchorMoveMode : IMode {
    ISuperstructure ss;
    Superstate superstate;
    Game game;
    IModeDelegate delegat;
    NarrationPanelView narrator;

    public TimeAnchorMoveMode(
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

      narrator.ShowMessage("Placing a time anchor! Select an adjacent space to move to, to make room for it.");
    }

    public void OnTileMouseClick(Location newLocation) {
      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        ss.GetRoot().logger.Error("Not your turn!");
        delegat.AfterDidSomething();
        return;
      }

      string result = ss.RequestTimeAnchorMove(game.id, newLocation);
      if (result != "") {
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
      narrator.ShowMessage("You must move off the time anchor to place it. Canceling time anchor!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void ActivateCheat(string cheatName) {
      narrator.ShowMessage("You must move off the time anchor to place it. Canceling time anchor!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void FireClicked() {
      narrator.ShowMessage("You must move off the time anchor to place it. Canceling time anchor!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void MireClicked() {
      narrator.ShowMessage("You must move off the time anchor to place it. Canceling time anchor!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void CounterClicked() {
      narrator.ShowMessage("You must move off the time anchor to place it. Canceling time anchor!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void InteractClicked() {
      narrator.ShowMessage("You must move off the time anchor to place it. Canceling time anchor!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void TimeShiftClicked() {
      narrator.ShowMessage("You must move off the time anchor to place it. Canceling time anchor!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void TimeAnchorMoveClicked() {
      narrator.ShowMessage("Canceled time anchor!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void ReadyForTurn() {
      Asserts.Assert(false); // curiosity
    }
  }
}
