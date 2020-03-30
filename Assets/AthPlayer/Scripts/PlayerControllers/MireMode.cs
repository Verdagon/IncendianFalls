using System;
using System.Collections.Generic;
using Atharia.Model;
using Domino;

namespace AthPlayer {
  public class MireMode : IMode {
    SuperstructureWrapper ss;
    Game game;
    IModeDelegate delegat;
    ShowError showError;
    OverlayPresenter instructionsOverlay;

    public MireMode(
        SuperstructureWrapper ss,
        Game game,
        IModeDelegate delegat,
        ShowInstructions showInstructions,
        ShowError showError) {
      this.ss = ss;
      this.game = game;
      this.delegat = delegat;
      this.showError = showError;

      instructionsOverlay =
        showInstructions("Preparing to cast Mire! Select unit to Mire.");
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

      if (!game.WaitingOnPlayerInput()) {
        ss.GetRoot().logger.Error("Not your turn!");
        delegat.SwitchToNormalMode();
        return;
      }

      var unit = FindUnitAtLocation(location);
      if (!unit.Exists()) {
        showError("No unit there. Canceling Mire!");
        delegat.SwitchToNormalMode();
        return;
      }

      string result = ss.RequestMire(game.id, unit.id);
      if (result.Length > 0) {
        showError(result);
        delegat.SwitchToNormalMode();
        return;
      }

      delegat.SwitchToNormalMode();
    }

    public void OnTileMouseHover(Location maybeHoverLocation) {

    }

    public void StartedWaitingForPlayerInput() {
    }

    public void Destroy(bool purposeful) {
      instructionsOverlay.Close();
      if (purposeful) {
        showError("Canceled Mire!");
      } else {
        showError("You must select a unit to Mire them. Canceling Mire!");
      }
    }
  }
}
