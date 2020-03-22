using System;
using Atharia.Model;
using Domino;
using UnityEngine;

namespace AthPlayer {
  public class NormalMode : IMode {
    ISuperstructure ss;
    Superstate superstate;
    Game game;
    IModeDelegate delegat;
    ShowError showError;

    public NormalMode(
        ISuperstructure ss,
        Superstate superstate,
        Game game,
        IModeDelegate delegat,
    ShowError showError) {
      this.ss = ss;
      this.superstate = superstate;
      this.game = game;
      this.delegat = delegat;
      this.showError = showError;
    }

    public void OnTileMouseClick(Location newLocation) {
      Asserts.Assert(superstate.GetStateType() == MultiverseStateType.kBeforePlayerInput, "Player not ready to act yet.");

      string attackResult = AttackAt(newLocation);
      if (attackResult == "") {
        delegat.AfterDidSomething();
        return;
      }

      string moveResult = NavigateTo(newLocation);
      if (moveResult == "") {
        delegat.AfterDidSomething();
        return;
      }

      showError(moveResult);
      delegat.AfterDidSomething();
    }

    private string NavigateTo(Location target) {
      return ss.RequestMove(game.id, target);
    }

    private string AttackAt(Location target) {
      if (this.game.level.terrain.pattern.LocationsAreAdjacent(game.player.location, target, game.level.ConsiderCornersAdjacent())) {
        foreach (var unit in this.game.level.units) {
          if (unit.location == target) {
            if (!unit.alive) {
              continue;
            }
            return ss.RequestAttack(game.id, unit.id);
          }
        }
      }
      return "No unit there!";
    }

    public void Cancel(bool purposeful) {
      string cancelResult = ss.RequestCancel(game.id);
      if (cancelResult.Length > 0) {
        showError(cancelResult);
        return;
      }
    }
  }
}
