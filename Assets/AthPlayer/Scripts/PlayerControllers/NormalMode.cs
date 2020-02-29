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
    NarrationPanelView narrator;

    public NormalMode(
        ISuperstructure ss,
        Superstate superstate,
        Game game,
        IModeDelegate delegat,
        NarrationPanelView narrator) {
      this.ss = ss;
      this.superstate = superstate;
      this.game = game;
      this.delegat = delegat;
      this.narrator = narrator;
    }

    public void OnTileMouseClick(Location newLocation) {
      narrator.ClearMessage();
      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        ss.GetRoot().logger.Error("Not your turn!");
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

      narrator.ShowMessage(moveResult);
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

    public void DefendClicked() {
      narrator.ClearMessage();
      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        ss.GetRoot().logger.Error("Not your turn!");
        delegat.AfterDidSomething();
        return;
      }
      string defendResult = ss.RequestDefend(game.id);
      if (defendResult != "") {
        narrator.ShowMessage(defendResult);
        delegat.AfterDidSomething();
        return;
      }

      delegat.AfterDidSomething();
    }

    public void ActivateCheat(string cheatName) {
      narrator.ClearMessage();
      string result = ss.RequestCheat(game.id, cheatName);
      if (result.Length > 0) {
        narrator.ShowMessage(result);
        delegat.AfterDidSomething();
        return;
      }
      delegat.AfterDidSomething();
    }

    public void CounterClicked() {
      narrator.ClearMessage();
      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        ss.GetRoot().logger.Error("Not your turn!");
        delegat.AfterDidSomething();
        return;
      }
      string counterResult = ss.RequestCounter(game.id);
      if (counterResult.Length > 0) {
        narrator.ShowMessage(counterResult);
        delegat.AfterDidSomething();
        return;
      }

      delegat.AfterDidSomething();
    }

    public void FireClicked() {
      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        ss.GetRoot().logger.Error("Not your turn!");
        delegat.AfterDidSomething();
        return;
      }

      narrator.ClearMessage();
      delegat.SwitchToFireMode();
    }

    public void InteractClicked() {
      narrator.ClearMessage();
      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        ss.GetRoot().logger.Error("Not your turn!");
        delegat.AfterDidSomething();
        return;
      }
      string interactResult = ss.RequestInteract(game.id);
      if (interactResult != "") {
        narrator.ShowMessage(interactResult);
        delegat.AfterDidSomething();
        return;
      }

      delegat.AfterDidSomething();
    }

    public void TimeShiftClicked() {
      narrator.ClearMessage();
      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        ss.GetRoot().logger.Error("Not your turn!");
        delegat.AfterDidSomething();
        return;
      }
      string timeShiftResult = ss.RequestTimeShift(game.id);
      if (timeShiftResult != "") {
        narrator.ShowMessage(timeShiftResult);
        delegat.AfterDidSomething();
        return;
      }
      ss.GetRoot().logger.Info("time shifted, new state: " + superstate.GetStateType());
      delegat.AfterDidSomething();
    }

    public void TimeAnchorMoveClicked() {
      narrator.ClearMessage();
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
