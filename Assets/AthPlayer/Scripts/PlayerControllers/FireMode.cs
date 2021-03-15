using System;
using System.Collections.Generic;
using Atharia.Model;
using Domino;

namespace AthPlayer {
  public class FireMode : IMode {
    SuperstructureWrapper ss;
    Game game;
    IModeDelegate delegat;
    ShowError showError;
    OverlayPresenter instructionsOverlay;

    public FireMode(
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
        showInstructions("Preparing to fire! Select a unit to fire at.");
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
        showError("No unit there. Canceling fire!");
        delegat.SwitchToNormalMode();
        return;
      }

      string result = ss.RequestFire(game.id, unit.id);
      if (result.Length > 0) {
        showError(result);
        delegat.SwitchToNormalMode();
        return;
      }

      delegat.SwitchToNormalMode();
    }

    public void Update(Location maybeHoverLocation) {

    }

    public void Destroy(bool purposeful) {
      instructionsOverlay.Close();
      if (purposeful) {
        showError("Canceled fire!");
      } else {
        showError("You must select a unit to fire on them. Canceling fire!");
      }
    }
  }
}
