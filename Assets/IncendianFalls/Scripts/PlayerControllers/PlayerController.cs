using System;
using System.Collections.Generic;
using Atharia.Model;
using UnityEngine;
using UnityEngine.UI;
using IncendianFalls;

namespace Domino {
  public delegate void ITimerCallback();

  public interface ITimer {
    float GetTime();
    void ScheduleTimer(float secondFromNow, ITimerCallback callback);
  }

  public class PlayerController :
      IUnitEffectObserver, IUnitEffectVisitor,
      IGameEffectObserver, IGameEffectVisitor,
      IGameMousedObserver, IModeDelegate {
    ISuperstructure ss;
    Superstate superstate;
    Game game;
    GamePresenter gamePresenter;
    ITimer timer;
    ExecutionStaller resumeStaller;
    ExecutionStaller turnStaller;
    PlayerPanelView playerPanelView;
    NarrationPanelView narrator;
    LookPanelView lookPanelView;
    Looker looker;
    IMode mode;
    Unit player;

    public PlayerController(
        ITimer timer,
        ExecutionStaller resumeStaller,
        ExecutionStaller turnStaller,
        ISuperstructure ss,
        Superstate superstate,
        Game game,
        GamePresenter gamePresenter,
        PlayerPanelView playerPanelView,
        NarrationPanelView messageView,
        LookPanelView lookPanelView) {
      this.ss = ss;
      this.superstate = superstate;
      this.game = game;
      this.gamePresenter = gamePresenter;
      this.timer = timer;
      this.resumeStaller = resumeStaller;
      this.turnStaller = turnStaller;
      this.playerPanelView = playerPanelView;
      this.narrator = messageView;
      this.lookPanelView = lookPanelView;

      this.game.AddObserver(this);

      looker = new Looker(lookPanelView);

      this.resumeStaller.unstalledEvent += (sender) => MaybeResume();
      this.turnStaller.unstalledEvent += (sender) => MaybeResume();

      gamePresenter.observers.Add(this);

      this.player = this.game.player;
      player.AddObserver(this);
      RefreshPlayerStatusText();
      SwitchToNormalMode();
    }

    public void Start() {
      narrator.ShowMessage("The Incendian Falls! I've finally made it.\nIf I can find Volcaetus, I can save my brother!");
      MaybeResume();
    }

    public void OnUnitMouseClick(Unit unit) {
      //Debug.Log("Clicked on unit " + unit.id + " at " + unit.location);
      var location = unit.location;
      OnTileMouseClick(location);
    }

    public void OnUnitMouseIn(Unit unit) {
      looker.Look(unit);
    }

    public void OnUnitMouseOut(Unit unit) {
      var symbolsAndLabels = new List<KeyValuePair<SymbolDescription, string>>();
      lookPanelView.SetStuff(false, "", "", symbolsAndLabels);
    }

    public void OnTileMouseClick(Location newLocation) {
      //Debug.Log("Clicked on tile " + newLocation);
      mode.OnTileMouseClick(newLocation);
    }

    public void OnTileMouseIn(Location location) {
    }

    public void OnTileMouseOut(Location location) {
    }

    public void DefendClicked() {
      mode.DefendClicked();
    }

    public void CounterClicked() {
      mode.CounterClicked();
    }

    public void FireClicked() {
      mode.FireClicked();
    }

    public void InteractClicked() {
      mode.InteractClicked();
    }

    public void AfterDidSomething() {
      MaybeResume();
    }

    private void MaybeResume() {
      for (bool processing = true; processing; ) {
        if (resumeStaller.IsStalled()) {
          return; // To be continued... via a staller getting unstalled.
        }
        if (superstate.timeShiftingState == null) {
          if (!game.player.Exists() || !game.player.alive) {
            // Player has died, bail!
            return;
          }
        }
        switch (superstate.GetStateType()) {
          case MultiverseStateType.kAfterUnitAction:
          case MultiverseStateType.kBeforeEnemyAction:
          case MultiverseStateType.kPreActingDetail:
          case MultiverseStateType.kPostActingDetail:
          case MultiverseStateType.kBetweenUnits:
            Asserts.Assert(game.player.Exists() && game.player.alive, "Can't resume if player's not alive");
            ss.RequestResume(game.id);
            break;
          case MultiverseStateType.kBeforePlayerInput:
          case MultiverseStateType.kBeforePlayerResume:
            processing = false;
            break;
          case MultiverseStateType.kTimeshiftingBackward:
          case MultiverseStateType.kTimeshiftingCloneMoving:
          case MultiverseStateType.kTimeshiftingAfterCloneMoved:
            ss.RequestTimeShift(game.id);
            resumeStaller.StallForDuration(.3f);
            turnStaller.StallForDuration(.3f);
            break;
          default:
            Asserts.Assert(false);
            break;
        }
      }

      Asserts.Assert(
          superstate.GetStateType() == MultiverseStateType.kBeforePlayerInput ||
          superstate.GetStateType() == MultiverseStateType.kBeforePlayerResume);

      if (turnStaller.IsStalled()) {
        return; // To be continued... via a staller getting unstalled.
      }

      mode.ReadyForTurn();
    }

    public void TimeShiftClicked() {
      mode.TimeShiftClicked();
    }

    public void ActivateCheat(string cheatName) {
      Debug.Log("Cheats disabled!");
      //mode.ActivateCheat(cheatName);
    }

    public void TimeAnchorMoveClicked() {
      mode.TimeAnchorMoveClicked();
    }

    private void RefreshPlayerStatusText() {
      if (game.player.Exists()) {
        playerPanelView.ShowPlayerStatus(game.level, game.player);
      } else {
        playerPanelView.Clear();
      }
    }

    public void OnUnitEffect(IUnitEffect effect) { effect.visit(this); }
    public void visitUnitCreateEffect(UnitCreateEffect effect) { }
    public void visitUnitDeleteEffect(UnitDeleteEffect effect) { }
    public void visitUnitSetLocationEffect(UnitSetLocationEffect effect) { }
    public void visitUnitSetAliveEffect(UnitSetAliveEffect effect) {
      if (effect.newValue == false) {
        narrator.ShowMessage("You have been slain, game over!\nRestart app and try again!");
      }
    }
    public void visitUnitSetLifeEndTimeEffect(UnitSetLifeEndTimeEffect effect) { }
    public void visitUnitSetNextActionTimeEffect(UnitSetNextActionTimeEffect effect) { }
    public void visitUnitSetHpEffect(UnitSetHpEffect effect) {
      RefreshPlayerStatusText();
    }
    public void visitUnitSetMpEffect(UnitSetMpEffect effect) {
      RefreshPlayerStatusText();
    }

    public void SwitchToNormalMode() {
      mode = new NormalMode(ss, superstate, game, this, narrator);
    }

    public void SwitchToTimeAnchorMoveMode() {
      mode = new TimeAnchorMoveMode(ss, superstate, game, this, narrator);
    }

    public void SwitchToFireMode() {
      mode = new FireMode(ss, superstate, game, this, narrator);
    }

    public void OnGameEffect(IGameEffect effect) { effect.visit(this);  }

    public void visitGameCreateEffect(GameCreateEffect effect) { }
    public void visitGameDeleteEffect(GameDeleteEffect effect) { }
    public void visitGameSetTimeEffect(GameSetTimeEffect effect) { }
    public void visitGameSetLevelEffect(GameSetLevelEffect effect) {
      RefreshPlayerStatusText();
    }
    public void visitGameSetPlayerEffect(GameSetPlayerEffect effect) {
      if (player.Exists()) {
        player.RemoveObserver(this);
      }
      RefreshPlayerStatusText();
      player = game.player;
      if (player.Exists()) {
        player.AddObserver(this);
      }
    }
  }
}
