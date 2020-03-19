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
      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        showError("(Player not ready to act yet.)");
        delegat.AfterDidSomething();
        return;
      }

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

    public void DefyClicked() {
      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        showError("(Player not ready to act yet.)");
        delegat.AfterDidSomething();
        return;
      }
      string defendResult = ss.RequestDefy(game.id);
      if (defendResult != "") {
        showError(defendResult);
        delegat.AfterDidSomething();
        return;
      }

      delegat.AfterDidSomething();
    }

    public void ActivateCheat(string cheatName) {
      string result = ss.RequestCheat(game.id, cheatName);
      if (result.Length > 0) {
        showError(result);
        delegat.AfterDidSomething();
        return;
      }
      delegat.AfterDidSomething();
    }

    public void CounterClicked() {
      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {

        showError("(Player not ready to act yet.)");
        delegat.AfterDidSomething();
        return;
      }
      string counterResult = ss.RequestCounter(game.id);
      if (counterResult.Length > 0) {
        showError(counterResult);
        delegat.AfterDidSomething();
        return;
      }

      delegat.AfterDidSomething();
    }

    public void CancelClicked() {
      string cancelResult = ss.RequestCancel(game.id);
      if (cancelResult.Length > 0) {
        showError(cancelResult);
        return;
      }
    }

    public void FireClicked() {
      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        
        showError("(Player not ready to act yet.)");
        delegat.AfterDidSomething();
        return;
      }

      delegat.SwitchToFireMode();
    }

    public void FireBombClicked() {
      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        
        showError("(Player not ready to act yet.)");
        delegat.AfterDidSomething();
        return;
      }

      delegat.SwitchToFireBombMode();
    }

    public void MireClicked() {
      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        
        showError("(Player not ready to act yet.)");
        delegat.AfterDidSomething();
        return;
      }

      delegat.SwitchToMireMode();
    }

    public void InteractClicked() {
      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        
        showError("(Player not ready to act yet.)");
        delegat.AfterDidSomething();
        return;
      }
      string interactResult = ss.RequestInteract(game.id);
      if (interactResult != "") {
        showError(interactResult);
        delegat.AfterDidSomething();
        return;
      }

      delegat.AfterDidSomething();
    }

    public void TimeShiftClicked() {
      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        
        showError("(Player not ready to act yet.)");
        delegat.AfterDidSomething();
        return;
      }
      string timeShiftResult = ss.RequestTimeShift(game.id);
      if (timeShiftResult != "") {
        showError(timeShiftResult);
        delegat.AfterDidSomething();
        return;
      }
      ss.GetRoot().logger.Info("time shifted, new state: " + superstate.GetStateType());
      delegat.AfterDidSomething();
    }

    public void TimeAnchorMoveClicked() {
      delegat.SwitchToTimeAnchorMoveMode();
    }

    public void ReadyForTurn() {
      if (superstate.GetStateType() == MultiverseStateType.kBeforePlayerResume) {
        ss.RequestFollowDirective(game.id);
        delegat.AfterDidSomething();
      } else {
        return; // To be continued... via a player action.
      }
    }
  }
}
