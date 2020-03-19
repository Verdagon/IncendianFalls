using System;
using Atharia.Model;
using Domino;

namespace AthPlayer {
  public class FireMode : IMode {
    ISuperstructure ss;
    Superstate superstate;
    Game game;
    IModeDelegate delegat;
    ShowError showError;
    OverlayPresenter instructionsOverlay;

    public FireMode(
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
            "Preparing to fire! Select a unit to fire at.",
            "instructions", "narrator", true, true, false, new ButtonImmList()));
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
      instructionsOverlay.Close();

      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        ss.GetRoot().logger.Error("Not your turn!");
        delegat.AfterDidSomething();
        delegat.SwitchToNormalMode();
        return;
      }

      var unit = FindUnitAtLocation(location);
      if (!unit.Exists()) {
        showError("No unit there. Canceling fire!");
        delegat.SwitchToNormalMode();
        delegat.AfterDidSomething();
        return;
      }

      string result = ss.RequestFire(game.id, unit.id);
      if (result.Length > 0) {
        showError(result);
        delegat.SwitchToNormalMode();
        delegat.AfterDidSomething();
        return;
      }

      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void DefyClicked() {
      showError("You must select a unit to fire on them. Canceling fire!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void ActivateCheat(string cheatName) {
      showError("You must select a unit to fire on them. Canceling fire!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void FireClicked() {
      showError("Canceled fire!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void CancelClicked() {
      showError("Canceled fire!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void FireBombClicked() {
      showError("You must select a unit to fire on them. Canceling fire!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void MireClicked() {
      showError("You must select a unit to fire on them. Canceling fire!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void CounterClicked() {
      showError("You must select a unit to fire on them. Canceling fire!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void InteractClicked() {
      showError("You must select a unit to fire on them. Canceling fire!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void TimeShiftClicked() {
      showError("You must select a unit to fire on them. Canceling fire!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void TimeAnchorMoveClicked() {
      showError("You must select a unit to fire on them. Canceling fire!");
      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void ReadyForTurn() {
      Asserts.Assert(false); // curiosity
    }
  }
}
