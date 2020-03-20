using System;
using System.Collections.Generic;
using Atharia.Model;
using UnityEngine;
using AthPlayer;

namespace Domino {
  public class PlayerController :
      IUnitEffectObserver, IUnitEffectVisitor,
      IGameEffectObserver, IGameEffectVisitor,
    ISorcerousUCEffectObserver, ISorcerousUCEffectVisitor,
      IModeDelegate {
    IClock cinematicTimer;
    ISuperstructure ss;
    Superstate superstate;
    Game game;
    GamePresenter gamePresenter;
    ITimer timer;
    ExecutionStaller resumeStaller;
    ExecutionStaller turnStaller;
    PlayerPanelView playerPanelView;
    LookPanelView lookPanelView;
    OverlayPresenterFactory overlayPresenterFactory;
    ShowError showError;
    Looker looker;
    OverlayPaneler overlayPaneler;
    IMode mode;
    Unit player;

    public PlayerController(
        ITimer timer,
        IClock cinematicTimer,
        ExecutionStaller resumeStaller,
        ExecutionStaller turnStaller,
        ISuperstructure ss,
        Superstate superstate,
        Game game,
        GamePresenter gamePresenter,
        LookPanelView lookPanelView,
        OverlayPaneler overlayPaneler,
        OverlayPresenterFactory overlayPresenterFactory,
        ShowError showError) {
      this.ss = ss;
      this.superstate = superstate;
      this.cinematicTimer = cinematicTimer;
      this.game = game;
      this.overlayPaneler = overlayPaneler;
      this.gamePresenter = gamePresenter;
      this.timer = timer;
      this.resumeStaller = resumeStaller;
      this.turnStaller = turnStaller;
      this.lookPanelView = lookPanelView;
      this.overlayPresenterFactory = overlayPresenterFactory;
      this.showError = showError;

      this.game.AddObserver(this);

      looker = new Looker(lookPanelView);

      this.resumeStaller.unstalledEvent += (sender) => MaybeResume();
      this.turnStaller.unstalledEvent += (sender) => MaybeResume();

      this.player = this.game.player;
      player.AddObserver(this);
      var sorcerous = player.components.GetOnlySorcerousUCOrNull();
      if (sorcerous.Exists()) {
        sorcerous.AddObserver(this);
      }
      playerPanelView = new PlayerPanelView(cinematicTimer, overlayPaneler, looker, player);

      SwitchToNormalMode();
    }

    public void Start() {
      MaybeResume();
    }

    public void LookAt(Unit maybeUnit, TerrainTile maybeTerrainTile) {
      looker.Look(maybeUnit, maybeTerrainTile);
    }

    //public void OnUnitMouseClick(Unit unit) {
    //  //Debug.Log("Clicked on unit " + unit.id + " at " + unit.location);
    //  var location = unit.location;
    //  OnTileMouseClick(location);
    //}

    //public void OnUnitMouseIn(Unit unit) {
    //  looker.Look(unit);
    //}

    //public void OnUnitMouseOut(Unit unit) {
    //  var symbolsAndLabels = new List<KeyValuePair<SymbolDescription, string>>();
    //  lookPanelView.SetStuff(false, "", "", symbolsAndLabels);
    //}

    public void OnTileMouseClick(Location newLocation) {
      //Debug.Log("Clicked on tile " + newLocation);
      mode.OnTileMouseClick(newLocation);
    }

    //public void OnTileMouseIn(Location location) {
    //}

    //public void OnTileMouseOut(Location location) {
    //}

    public void DefyClicked() {
      mode.DefyClicked();
    }

    public void CounterClicked() {
      mode.CounterClicked();
    }

    public void FireClicked() {
      mode.FireClicked();
    }

    public void FireBombClicked() {
      mode.FireBombClicked();
    }

    public void MireClicked() {
      mode.MireClicked();
    }

    public void CancelClicked() {
      mode.CancelClicked();
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
            resumeStaller.StallForDuration(300);
            turnStaller.StallForDuration(300);
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
      //Debug.Log("Cheats disabled!");
      mode.ActivateCheat(cheatName);
    }

    public void TimeAnchorMoveClicked() {
      mode.TimeAnchorMoveClicked();
    }

    public void OnUnitEffect(IUnitEffect effect) { effect.visit(this); }
    public void visitUnitCreateEffect(UnitCreateEffect effect) { }
    public void visitUnitDeleteEffect(UnitDeleteEffect effect) { }
    public void visitUnitSetLocationEffect(UnitSetLocationEffect effect) { }
    public void visitUnitSetMaxHpEffect(UnitSetMaxHpEffect effect) {}
    public void visitUnitSetAliveEffect(UnitSetAliveEffect effect) {
      if (effect.newValue == false) {
        var overlayPresenter = overlayPresenterFactory(
          new ShowOverlayEvent(
            "You have died!",
            "normal",
            "narrator",
            false,
            false,
            false,
            new ButtonImmList(new List<Button>() { new Button("Alas...", "_exitGame") })));
        // Do nothing with it, itll kill itself.
      }
    }
    public void visitUnitSetLifeEndTimeEffect(UnitSetLifeEndTimeEffect effect) { }
    public void visitUnitSetNextActionTimeEffect(UnitSetNextActionTimeEffect effect) { }
    public void visitUnitSetHpEffect(UnitSetHpEffect effect) {
      if (playerPanelView != null) {
        playerPanelView.UpdatePlayerStatus();
      }
    }
    public void OnSorcerousUCEffect(ISorcerousUCEffect effect) { effect.visit(this); }
    public void visitSorcerousUCCreateEffect(SorcerousUCCreateEffect effect) { }
    public void visitSorcerousUCDeleteEffect(SorcerousUCDeleteEffect effect) { }
    public void visitSorcerousUCSetMpEffect(SorcerousUCSetMpEffect effect) {
      if (playerPanelView != null) {
        playerPanelView.UpdatePlayerStatus();
      }
    }
    public void visitSorcerousUCSetMaxMpEffect(SorcerousUCSetMaxMpEffect effect) {
      if (playerPanelView != null) {
        playerPanelView.UpdatePlayerStatus();
      }
    }

    public void SwitchToNormalMode() {
      mode = new NormalMode(ss, superstate, game, this, showError);
    }

    public void SwitchToTimeAnchorMoveMode() {
      mode = new TimeAnchorMoveMode(ss, superstate, game, this, overlayPresenterFactory, showError);
    }

    public void SwitchToFireMode() {
      mode = new FireMode(ss, superstate, game, this, overlayPresenterFactory, showError);
    }

    public void SwitchToFireBombMode() {
      if (game.player.components.GetAllBlastRod().Count == 0) {
        overlayPresenterFactory(new ShowOverlayEvent("Can't fire bomb, find a Fire Rod first!", "error", "narrator", false, false, false, new ButtonImmList()));
        return;
      }
      mode = new FireBombMode(ss, superstate, game, this, overlayPresenterFactory, showError);
    }

    public void SwitchToMireMode() {
      if (game.player.components.GetAllSlowRod().Count == 0) {
        overlayPresenterFactory(new ShowOverlayEvent("Can't mire, find a Mire Staff first!", "error", "narrator", false, false, false, new ButtonImmList()));
        return;
      }
      mode = new MireMode(ss, superstate, game, this, overlayPresenterFactory, showError);
    }

    public void OnGameEffect(IGameEffect effect) { effect.visit(this);  }

    public void visitGameCreateEffect(GameCreateEffect effect) { }
    public void visitGameDeleteEffect(GameDeleteEffect effect) { }
    public void visitGameSetTimeEffect(GameSetTimeEffect effect) { }
    public void visitGameSetInstructionsEffect(GameSetInstructionsEffect effect) { }
    public void visitGameSetLevelEffect(GameSetLevelEffect effect) {
      if (playerPanelView != null) {
        playerPanelView.UpdatePlayerStatus();
      }
    }
    public void visitGameSetPlayerEffect(GameSetPlayerEffect effect) {
      if (player.Exists()) {
        playerPanelView.Clear();
        playerPanelView = null;
        player.RemoveObserver(this);
      }
      player = game.player;
      if (player.Exists()) {
        player.AddObserver(this);
        playerPanelView = new PlayerPanelView(cinematicTimer, overlayPaneler, looker, player);
      }
    }
  }
}
