using System;
using Atharia.Model;
using Domino;
using UnityEngine;

namespace AthPlayer {
  public class FireBombMode : IMode {
    SuperstructureWrapper ss;
    Game game;
    IModeDelegate delegat;
    ShowError showError;
    OverlayPresenter instructionsOverlay;

    public FireBombMode(
        SuperstructureWrapper ss,
        Game game,
        IModeDelegate delegat,
        ShowInstructions showInstructions,
        ShowError showError) {
      this.ss = ss;
      this.game = game;
      this.delegat = delegat;
      this.showError = showError;

      instructionsOverlay = showInstructions("Preparing to fire bomb! Select a location.");
    }

    public void OnTileMouseClick(Location location) {
      instructionsOverlay.Close();

      if (!game.WaitingOnPlayerInput()) {
        ss.GetRoot().logger.Error("Not your turn!");
        delegat.SwitchToNormalMode();
        return;
      }

      string result = ss.RequestFireBomb(game.id, location);
      if (result.Length > 0) {
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
        showError("Canceled fire bomb!");
      } else {
        showError("You must select a location to fire bomb on. Canceling fire bomb!");
      }
    }
  }
}
