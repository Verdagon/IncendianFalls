using System;
using System.Collections.Generic;
using Atharia.Model;
using Domino;

namespace AthPlayer {
  public class ExplosionMode : IMode {
    SuperstructureWrapper ss;
    Game game;
    IModeDelegate delegat;
    ShowError showError;
    OverlayPresenter instructionsOverlay;

    public ExplosionMode(
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
        showInstructions("Preparing to set an explosion! Select a location.");
    }

    public void OnTileMouseClick(Location location) {
      instructionsOverlay.Close();

      if (!game.WaitingOnPlayerInput()) {
        ss.GetRoot().logger.Error("Not your turn!");
        delegat.SwitchToNormalMode();
        return;
      }

      string result = ss.RequestExplosion(game.id, location);
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
        showError("Canceled blaze!");
      } else {
        showError("You must select a location to blaze. Canceling blaze!");
      }
    }
  }
}
