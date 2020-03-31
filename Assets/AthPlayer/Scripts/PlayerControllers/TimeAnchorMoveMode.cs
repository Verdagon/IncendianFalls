using System;
using System.Collections.Generic;
using Atharia.Model;
using Domino;

namespace AthPlayer {
  public class TimeAnchorMoveMode : IMode {
    SuperstructureWrapper ss;
    Game game;
    IModeDelegate delegat;
    ShowError showError;
    OverlayPresenter instructionsOverlay;

    public TimeAnchorMoveMode(
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
        showInstructions("Placing a time anchor! Select an adjacent space to move to, to make room for it.");
    }

    public void OnTileMouseClick(Location newLocation) {
      instructionsOverlay.Close();

      if (!game.WaitingOnPlayerInput()) {
        ss.GetRoot().logger.Error("Not your turn!");
        return;
      }

      string result = ss.RequestTimeAnchorMove(game.id, newLocation);
      if (result != "") {
        showError(result);
        delegat.SwitchToNormalMode();
        return;
      }

      delegat.SwitchToNormalMode();
    }

    public void Update(Location maybeHoverLocation) {

    }

    public void StartedWaitingForPlayerInput() {
    }

    public void Destroy(bool purposeful) {
      instructionsOverlay.Close();
      if (purposeful) {
        showError("Canceled time anchor!");
      } else {
        showError("You must move off the time anchor to place it. Canceling time anchor!");
      }
    }
  }
}
