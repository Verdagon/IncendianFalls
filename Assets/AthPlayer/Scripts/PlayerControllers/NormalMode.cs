using System;
using Atharia.Model;
using Domino;
using UnityEngine;

namespace AthPlayer {
  public class NormalMode : IMode {
    SuperstructureWrapper ss;
    Game game;
    IModeDelegate delegat;
    ShowError showError;

    public NormalMode(
        SuperstructureWrapper ss,
        Game game,
        IModeDelegate delegat,
    ShowError showError) {
      this.ss = ss;
      this.game = game;
      this.delegat = delegat;
      this.showError = showError;
    }

    public void OnTileMouseClick(Location newLocation) {
      Asserts.Assert(game.WaitingOnPlayerInput(), "Player not ready to act yet.");

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
            if (!unit.Alive()) {
              continue;
            }
            return ss.RequestAttack(game.id, unit.id);
          }
        }
      }
      return "No unit there!";
    }

    public void Cancel(bool purposeful) {
      Asserts.Assert(false); // implement, have the navigating in NormalMode.
      //string cancelResult = ss.RequestCancel(game.id);
      //if (cancelResult.Length > 0) {
      //  showError(cancelResult);
      //  return;
      //}
    }
  }
}
