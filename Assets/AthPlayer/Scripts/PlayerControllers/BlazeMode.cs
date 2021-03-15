using System;
using System.Collections.Generic;
using Atharia.Model;
using Domino;

namespace AthPlayer {
  public class BlazeMode : IMode {
    SuperstructureWrapper ss;
    Game game;
    IModeDelegate delegat;
    ShowError showError;
    OverlayPresenter instructionsOverlay;

    public BlazeMode(
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
        showInstructions("Preparing to blaze! Select a location to set on fire.");
    }

    public void OnTileMouseClick(Location location) {
      instructionsOverlay.Close();

      if (!game.WaitingOnPlayerInput()) {
        ss.GetRoot().logger.Error("Not your turn!");
        delegat.SwitchToNormalMode();
        return;
      }

      string result = ss.RequestBlaze(game.id, location);
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
