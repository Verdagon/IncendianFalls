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
    private delegate void WaitingInput();

    IClock cinematicTimer;
    InputSemaphore inputSemaphore;
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
    GameObject thinkingIndicator;
    Unit player;
    bool hideInput;
    WaitingInput waitingInput;

    IMode mode;
    int modeCapabilityId; // 0 means theres no capability associated with the current mode.

    public PlayerController(
        ITimer timer,
        IClock cinematicTimer,
        ExecutionStaller resumeStaller,
        ExecutionStaller turnStaller,
    InputSemaphore inputSemaphore,
    ISuperstructure ss,
        Superstate superstate,
        Game game,
        GamePresenter gamePresenter,
        LookPanelView lookPanelView,
        OverlayPaneler overlayPaneler,
        OverlayPresenterFactory overlayPresenterFactory,
        ShowError showError,
        GameObject thinkingIndicator) {
      this.ss = ss;
      this.inputSemaphore = inputSemaphore;
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
      this.thinkingIndicator = thinkingIndicator;

      this.game.AddObserver(this);

      looker = new Looker(lookPanelView);

      this.resumeStaller.unstalledEvent += (sender) => MaybeResume();
      this.turnStaller.unstalledEvent += (sender) => MaybeResume();

      this.hideInput = false;
      this.player = Unit.Null;

      UpdateHideInput();
      UpdatePlayer();
      UpdatePlayerPanel();

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

    private void DoIfAllowedAndWhenReady(WaitingInput inputCallback) {
      if (inputSemaphore.locked) {
        Debug.LogError("Rejecting input, locked!");
      } else {
        // We might be timeshifting or something, told by superstate.
        // But even if the core is ready for input, we might be finishing up
        // some animations, told by turnStaller.
        if (superstate.GetStateType() == MultiverseStateType.kBeforePlayerInput && !turnStaller.IsStalled()) {
          thinkingIndicator.SetActive(true);
          timer.ScheduleTimer(0, () => {
            Asserts.Assert(!inputSemaphore.locked, "curiosity a");
            // If this gets triggered, then perhaps we're sending input from somewhere else, in this 0ms window?
            Asserts.Assert(superstate.GetStateType() == MultiverseStateType.kBeforePlayerInput, "curiosity b");
            Asserts.Assert(!turnStaller.IsStalled(), "curiosity c");
            inputCallback();
            thinkingIndicator.SetActive(false);
          });
        } else {
          waitingInput = () => {
            thinkingIndicator.SetActive(true);
            timer.ScheduleTimer(0, () => {
              inputCallback();
              thinkingIndicator.SetActive(false);
            });
          };
        }
      }
    }

    public void OnTileMouseClick(Location newLocation) {
      //Debug.Log("Clicked on tile " + newLocation);

      DoIfAllowedAndWhenReady(() => mode.OnTileMouseClick(newLocation));
    }

    //public void OnTileMouseIn(Location location) {
    //}

    //public void OnTileMouseOut(Location location) {
    //}

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

      if (superstate.GetStateType() == MultiverseStateType.kBeforePlayerResume) {
        ss.RequestFollowDirective(game.id);
        AfterDidSomething();
      } else {
        if (waitingInput != null) {
          var lam = waitingInput;
          waitingInput = null;
          lam();
        } else {
          return; // To be continued... via a player action.
        }
      }
    }

    public void ActivateCheat(string cheatName) {
      string result = ss.RequestCheat(game.id, cheatName);
      if (result.Length > 0) {
        showError(result);
        AfterDidSomething();
        return;
      }
      AfterDidSomething();
    }

    private delegate void KeyAction();
    public void Update() {
      var lambdaByKey = new Dictionary<KeyCode, KeyAction>() {
        { KeyCode.A, () => ActivateCapability(PlayerPanelView.TIME_ANCHOR_MOVE_CAPABILITY_ID) },
        { KeyCode.R, () => ActivateCapability(PlayerPanelView.REVERT_CAPABILITY_ID) },
        { KeyCode.E, () => ActivateCapability(PlayerPanelView.INTERACT_CAPABILITY_ID) },
        { KeyCode.D, () => ActivateCapability(PlayerPanelView.DEFEND_CAPABILITY_ID) },
        { KeyCode.C, () => ActivateCapability(PlayerPanelView.COUNTER_CAPABILITY_ID) },
        { KeyCode.F, () => ActivateCapability(PlayerPanelView.FIRE_BOMB_CAPABILITY_ID) },
        { KeyCode.B, () => ActivateCapability(PlayerPanelView.FIRE_BOMB_CAPABILITY_ID) },
        { KeyCode.S, () => ActivateCapability(PlayerPanelView.MIRE_CAPABILITY_ID) },
        { KeyCode.Escape, () => Cancel(true) },
        { KeyCode.Slash, () => ActivateCheat("warptoend") },
        { KeyCode.Equals, () => ActivateCheat("poweroverwhelming") },
        { KeyCode.Alpha8, () => ActivateCheat("gimmeblastrod") },
        { KeyCode.Alpha6, () => ActivateCheat("gimmeslowrod") },
        { KeyCode.Alpha7, () => ActivateCheat("gimmearmor") },
        { KeyCode.Alpha9, () => ActivateCheat("gimmesword") },
        { KeyCode.Insert, () => ActivateCheat("healinglove") },
      };
      foreach (var keyAndLambda in lambdaByKey) {
        var key = keyAndLambda.Key;
        var lambda = keyAndLambda.Value;
        if (Input.GetKeyDown(key)) {
          DoIfAllowedAndWhenReady(() => lambda());
        }
      }
    }

    public void SwitchToCapability(int capabilityId) {
      ActivateCapability(capabilityId);
    }

    private void Cancel(bool purposeful) {
      mode.Cancel(purposeful);
      SwitchToNormalMode();
    }

    private void ActivateCapability(int capabilityId) {
      if (capabilityId == 0) {
        Cancel(true);
        return;
      }
      if (modeCapabilityId == capabilityId) {
        // They clicked the same button, cancel.
        Cancel(true);
        return;
      }
      if (modeCapabilityId != 0) {
        Cancel(false);
        // continue
      }
      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        Asserts.Assert(false, "wat");
        showError("(Player not ready to act yet.)");
        AfterDidSomething();
        return;
      }
      switch (capabilityId) {
        case PlayerPanelView.TIME_ANCHOR_MOVE_CAPABILITY_ID:
          modeCapabilityId = capabilityId;
          mode = new TimeAnchorMoveMode(ss, superstate, game, this, overlayPresenterFactory, showError);
          break;
        case PlayerPanelView.REVERT_CAPABILITY_ID:
          string timeShiftResult = ss.RequestTimeShift(game.id);
          if (timeShiftResult != "") {
            showError(timeShiftResult);
            AfterDidSomething();
            return;
          }
          ss.GetRoot().logger.Info("time shifted, new state: " + superstate.GetStateType());
          AfterDidSomething();
          break;
        case PlayerPanelView.INTERACT_CAPABILITY_ID:
          string interactResult = ss.RequestInteract(game.id);
          if (interactResult != "") {
            showError(interactResult);
            AfterDidSomething();
            return;
          }
          AfterDidSomething();
          break;
        case PlayerPanelView.DEFEND_CAPABILITY_ID:
          string defendResult = ss.RequestDefy(game.id);
          if (defendResult != "") {
            showError(defendResult);
            AfterDidSomething();
            return;
          }
          AfterDidSomething();
          break;
        case PlayerPanelView.COUNTER_CAPABILITY_ID:
          string counterResult = ss.RequestCounter(game.id);
          if (counterResult.Length > 0) {
            showError(counterResult);
            AfterDidSomething();
            return;
          }
          AfterDidSomething();
          break;
        case PlayerPanelView.FIRE_BOMB_CAPABILITY_ID:
          if (game.player.components.GetAllBlastRod().Count == 0) {
            overlayPresenterFactory(new ShowOverlayEvent("Can't fire bomb, find a Fire Rod first!", "error", "narrator", false, false, false, new ButtonImmList()));
            return;
          }
          modeCapabilityId = capabilityId;
          mode = new FireBombMode(ss, superstate, game, this, overlayPresenterFactory, showError);
          break;
        case PlayerPanelView.FIRE_CAPABILITY_ID:
          modeCapabilityId = capabilityId;
          mode = new FireMode(ss, superstate, game, this, overlayPresenterFactory, showError);
          break;
        case PlayerPanelView.MIRE_CAPABILITY_ID:
          if (game.player.components.GetAllSlowRod().Count == 0) {
            overlayPresenterFactory(new ShowOverlayEvent("Can't mire, find a Mire Staff first!", "error", "narrator", false, false, false, new ButtonImmList()));
            return;
          }
          modeCapabilityId = capabilityId;
          mode = new MireMode(ss, superstate, game, this, overlayPresenterFactory, showError);
          break;
        default:
          Debug.LogError("unknown capability id!");
          break;
      }
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
      modeCapabilityId = 0;
    }

    public void OnGameEffect(IGameEffect effect) { effect.visit(this);  }

    public void visitGameCreateEffect(GameCreateEffect effect) { }
    public void visitGameDeleteEffect(GameDeleteEffect effect) { }
    public void visitGameSetTimeEffect(GameSetTimeEffect effect) { }
    public void visitGameSetHideInputEffect(GameSetHideInputEffect effect) {
      UpdateHideInput();
    }
    public void visitGameSetInstructionsEffect(GameSetInstructionsEffect effect) { }
    public void visitGameSetLevelEffect(GameSetLevelEffect effect) {
      if (playerPanelView != null) {
        playerPanelView.UpdatePlayerStatus();
      }
    }
    public void visitGameSetPlayerEffect(GameSetPlayerEffect effect) {
      UpdatePlayer();
    }

    private void UpdateHideInput() {
      bool newHideInput = game.hideInput;
      if (hideInput != newHideInput) {
        if (newHideInput) {
          inputSemaphore.Lock();
        } else {
          inputSemaphore.Unlock();
        }
        hideInput = newHideInput;
      }
      UpdatePlayerPanel();
    }

    private void UpdatePlayer() {
      if (player.Exists()) {
        var sorcerous = player.components.GetOnlySorcerousUCOrNull();
        if (sorcerous.Exists()) {
          sorcerous.RemoveObserver(this);
        }
        player.RemoveObserver(this);
      }
      player = game.player;
      if (player.Exists()) {
        player.AddObserver(this);
        var sorcerous = player.components.GetOnlySorcerousUCOrNull();
        if (sorcerous.Exists()) {
          sorcerous.AddObserver(this);
        }
      }
      UpdatePlayerPanel();
    }

    private void UpdatePlayerPanel() {
      bool shouldHavePlayerPanel = player.Exists() && !hideInput;
      bool havePlayerPanel = (playerPanelView != null);
      if (shouldHavePlayerPanel != havePlayerPanel) {
        if (shouldHavePlayerPanel) {
          playerPanelView = new PlayerPanelView(cinematicTimer, overlayPaneler, looker, player);
          playerPanelView.CapabilityButtonClicked += ActivateCapability;
        } else {
          playerPanelView.Destroy();
          playerPanelView = null;
        }
      }
    }
  }
}
