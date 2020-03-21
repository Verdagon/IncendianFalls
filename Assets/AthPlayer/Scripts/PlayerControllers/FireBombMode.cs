using System;
using Atharia.Model;
using Domino;
using UnityEngine;

namespace AthPlayer {
  public class FireBombMode : IMode {
    ISuperstructure ss;
    Superstate superstate;
    Game game;
    IModeDelegate delegat;
    ShowError showError;
    OverlayPresenter instructionsOverlay;

    public FireBombMode(
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
            "Preparing to fire bomb! Select a location.",
            "instructions", "narrator", true, true, false, new ButtonImmList()));
    }

    public void OnTileMouseClick(Location location) {
      instructionsOverlay.Close();

      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        ss.GetRoot().logger.Error("Not your turn!");
        delegat.AfterDidSomething();
        delegat.SwitchToNormalMode();
        return;
      }

      string result = ss.RequestFireBomb(game.id, location);
      if (result.Length > 0) {
        showError(result);
        delegat.SwitchToNormalMode();
        delegat.AfterDidSomething();
        return;
      }

      delegat.SwitchToNormalMode();
      delegat.AfterDidSomething();
    }

    public void Cancel(bool purposeful) {
      if (purposeful) {
        showError("Canceled fire bomb!");
      } else {
        showError("You must select a location to fire bomb on. Canceling fire bomb!");
      }
    }
  }
}
