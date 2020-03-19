using System;
using Atharia.Model;
using Domino;

namespace AthPlayer {
  public class TimeAnchorMoveMode : IMode {
    ISuperstructure ss;
    Superstate superstate;
    Game game;
    IModeDelegate delegat;
    ShowError showError;
    OverlayPresenter instructionsOverlay;

    public TimeAnchorMoveMode(
        ISuperstructure ss,
        Superstate superstate,
        Game game,
        IModeDelegate delegat,
        OverlayPresenterFactory overlayPresenterFactory,
        ShowError showError) {
      this.ss = ss;
      this.superstate = superstate;
      this.game = game;
      this.delegat = delegat;
      this.showError = showError;

      instructionsOverlay =
        overlayPresenterFactory(
          new ShowOverlayEvent(
            "Placing a time anchor! Select an adjacent space to move to, to make room for it.",
            "instructions", "narrator", true, true, false, new ButtonImmList()));
    }

    public void OnTileMouseClick(Location newLocation) {
      instructionsOverlay.Close();

      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        ss.GetRoot().logger.Error("Not your turn!");
        delegat.AfterDidSomething();
        return;
      }

      string result = ss.RequestTimeAnchorMove(game.id, newLocation);
      if (result != "") {
        showError(result);
        delegat.SwitchToNormalMode();
        delegat.AfterDidSomething();
        return;
      }

      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void DefyClicked() {
      instructionsOverlay.Close();
      showError("You must move off the time anchor to place it. Canceling time anchor!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void CancelClicked() {
      instructionsOverlay.Close();
      showError("Canceling time anchor!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void ActivateCheat(string cheatName) {
      instructionsOverlay.Close();
      showError("You must move off the time anchor to place it. Canceling time anchor!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void FireClicked() {
      instructionsOverlay.Close();
      showError("You must move off the time anchor to place it. Canceling time anchor!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void FireBombClicked() {
      instructionsOverlay.Close();
      showError("You must move off the time anchor to place it. Canceling time anchor!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void MireClicked() {
      instructionsOverlay.Close();
      showError("You must move off the time anchor to place it. Canceling time anchor!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void CounterClicked() {
      instructionsOverlay.Close();
      showError("You must move off the time anchor to place it. Canceling time anchor!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void InteractClicked() {
      instructionsOverlay.Close();
      showError("You must move off the time anchor to place it. Canceling time anchor!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void TimeShiftClicked() {
      instructionsOverlay.Close();
      showError("You must move off the time anchor to place it. Canceling time anchor!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void TimeAnchorMoveClicked() {
      instructionsOverlay.Close();
      showError("Canceled time anchor!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void ReadyForTurn() {
      Asserts.Assert(false); // curiosity
    }
  }
}
