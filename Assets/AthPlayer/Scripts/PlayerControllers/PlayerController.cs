using System;
using System.Collections.Generic;
using Atharia.Model;
using UnityEngine;
using AthPlayer;
using static AthPlayer.OverlayPresenter;

namespace Domino {
  public class PlayerController :
      IUnitEffectObserver, IUnitEffectVisitor,
      IGameEffectObserver, IGameEffectVisitor,
    ISorcerousUCEffectObserver, ISorcerousUCEffectVisitor,
      IModeDelegate {
    private delegate void WaitingInput();

    IClock cinematicTimer;
    InputSemaphore inputSemaphore;
    SuperstructureWrapper ss;
    EffectBroadcaster broadcaster;
    FollowingCameraController cameraController;
    Game game;
    ITimer timer;
    PlayerPanelView playerPanelView;
    OverlayPresenterFactory overlayPresenterFactory;
    ShowInstructions showInstructions;
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
        InputSemaphore inputSemaphore,
        SuperstructureWrapper ss,
        EffectBroadcaster broadcaster,
        Game game,
        Looker looker,
        OverlayPaneler overlayPaneler,
        OverlayPresenterFactory overlayPresenterFactory,
        CameraController innerCameraController,
        ShowInstructions showInstructions,
        ShowError showError,
        GameObject thinkingIndicator) {
      this.broadcaster = broadcaster;
      this.ss = ss;
      this.inputSemaphore = inputSemaphore;
      this.cinematicTimer = cinematicTimer;
      this.showInstructions = showInstructions;
      this.game = game;
      this.overlayPaneler = overlayPaneler;
      this.timer = timer;
      this.looker = looker;
      this.overlayPresenterFactory = overlayPresenterFactory;
      this.showError = showError;
      this.thinkingIndicator = thinkingIndicator;

      cameraController = new FollowingCameraController(innerCameraController, broadcaster, game);

      this.game.AddObserver(broadcaster, this);

      this.hideInput = false;
      this.player = Unit.Null;

      UpdateHideInput();
      UpdatePlayer();
      UpdatePlayerPanel();

      SwitchToNormalMode();
    }

    public void LookAt(Unit maybeUnit, TerrainTile maybeTerrainTile) {
      looker.Look(maybeUnit, maybeTerrainTile);
    }

    public void OnLevelLoaded() {
      if (game.player.Exists()) {
        cameraController.StartMovingCameraTo(game.level.terrain.GetTileCenter(game.player.location).ToUnity(), 0);
      }
    }

    private void DoIfAllowedAndWhenReady(WaitingInput inputCallback) {
      if (inputSemaphore.locked) {
        Debug.LogError("Rejecting input, locked!");
        // Someday we should show a little icon here or something.
      } else {
        inputCallback();

        //// We might be timeshifting or something, told by superstate.
        //// But even if the core is ready for input, we might be finishing up
        //// some animations, told by turnStaller.
        //if (superstate.GetStateType() == MultiverseStateType.kBeforePlayerInput && !turnStaller.IsStalled()) {
        //  thinkingIndicator.SetActive(true);
        //  timer.ScheduleTimer(0, () => {
        //    Asserts.Assert(!inputSemaphore.locked, "curiosity a");
        //    // If this gets triggered, then perhaps we're sending input from somewhere else, in this 0ms window?
        //    Asserts.Assert(superstate.GetStateType() == MultiverseStateType.kBeforePlayerInput, "curiosity b");
        //    Asserts.Assert(!turnStaller.IsStalled(), "curiosity c");
        //    inputCallback();
        //    thinkingIndicator.SetActive(false);
        //  });
        //} else {
        //  waitingInput = () => {
        //    thinkingIndicator.SetActive(true);
        //    timer.ScheduleTimer(0, () => {
        //      inputCallback();
        //      thinkingIndicator.SetActive(false);
        //    });
        //  };
        //}
      }
    }

    public void OnTileMouseClick(Location newLocation) {
      //Debug.Log("Clicked on tile " + newLocation);

      DoIfAllowedAndWhenReady(() => mode.OnTileMouseClick(newLocation));
    }

    public void AfterDidSomething() {
      //MaybeResume();
    }

    //private void MaybeResume() {
    //  for (bool processing = true; processing; ) {
    //    if (resumeStaller.IsStalled()) {
    //      return; // To be continued... via a staller getting unstalled.
    //    }
    //    if (superstate.timeShiftingState == null) {
    //      if (!game.player.Exists() || !game.player.alive) {
    //        // Player has died, bail!
    //        return;
    //      }
    //    }
    //    switch (superstate.GetStateType()) {
    //      case MultiverseStateType.kAfterUnitAction:
    //      case MultiverseStateType.kBeforeEnemyAction:
    //      case MultiverseStateType.kPreActingDetail:
    //      case MultiverseStateType.kPostActingDetail:
    //      case MultiverseStateType.kBetweenUnits:
    //        Asserts.Assert(game.player.Exists() && game.player.alive, "Can't resume if player's not alive");
    //        ss.RequestResume(game.id);
    //        break;
    //      case MultiverseStateType.kBeforePlayerInput:
    //      case MultiverseStateType.kBeforePlayerResume:
    //        processing = false;
    //        break;
    //      case MultiverseStateType.kTimeshiftingBackward:
    //      case MultiverseStateType.kTimeshiftingCloneMoving:
    //      case MultiverseStateType.kTimeshiftingAfterCloneMoved:
    //        ss.RequestTimeShift(game.id);
    //        resumeStaller.StallForDuration(300);
    //        turnStaller.StallForDuration(300);
    //        break;
    //      default:
    //        Asserts.Assert(false);
    //        break;
    //    }
    //  }

    //  Asserts.Assert(
    //      superstate.GetStateType() == MultiverseStateType.kBeforePlayerInput ||
    //      superstate.GetStateType() == MultiverseStateType.kBeforePlayerResume);

    //  if (turnStaller.IsStalled()) {
    //    return; // To be continued... via a staller getting unstalled.
    //  }

    //  if (superstate.GetStateType() == MultiverseStateType.kBeforePlayerResume) {
    //    ss.RequestFollowDirective(game.id);
    //    AfterDidSomething();
    //  } else {
    //    if (waitingInput != null) {
    //      var lam = waitingInput;
    //      waitingInput = null;
    //      lam();
    //    } else {
    //      return; // To be continued... via a player action.
    //    }
    //  }
    //}

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
      //if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
      //  Asserts.Assert(false, "wat");
      //  showError("(Player not ready to act yet.)");
      //  AfterDidSomething();
      //  return;
      //}
      switch (capabilityId) {
        case PlayerPanelView.TIME_ANCHOR_MOVE_CAPABILITY_ID:
          modeCapabilityId = capabilityId;
          mode = new TimeAnchorMoveMode(ss, game, this, showInstructions, showError);
          break;
        case PlayerPanelView.REVERT_CAPABILITY_ID:
          string timeShiftResult = ss.RequestTimeShift(game.id);
          if (timeShiftResult != "") {
            showError(timeShiftResult);
            AfterDidSomething();
            return;
          }
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
            showError("Can't fire bomb, find a Fire Rod first!");
            return;
          }
          modeCapabilityId = capabilityId;
          mode = new FireBombMode(ss, game, this, showInstructions, showError);
          break;
        case PlayerPanelView.FIRE_CAPABILITY_ID:
          modeCapabilityId = capabilityId;
          mode = new FireMode(ss, game, this, showInstructions, showError);
          break;
        case PlayerPanelView.MIRE_CAPABILITY_ID:
          if (game.player.components.GetAllSlowRod().Count == 0) {
            showError("Can't mire, find a Mire Staff first!");
            return;
          }
          modeCapabilityId = capabilityId;
          mode = new MireMode(ss, game, this, showInstructions, showError);
          break;
        default:
          Debug.LogError("unknown capability id!");
          break;
      }
    }

    public void OnUnitEffect(IUnitEffect effect) { effect.visitIUnitEffect(this); }
    public void visitUnitCreateEffect(UnitCreateEffect effect) { }
    public void visitUnitDeleteEffect(UnitDeleteEffect effect) { }
    public void visitUnitSetLocationEffect(UnitSetLocationEffect effect) { }
    public void visitUnitSetMaxHpEffect(UnitSetMaxHpEffect effect) {
      if (effect.newValue != 0) {
        var overlayPresenter =
          overlayPresenterFactory(
            new NormalCommTemplate().AsICommTemplate(),
            new List<PageText>() {  new PageText( "You have died!", new UnityEngine.Color(1, 1, 1)) },
            new List<PageButton>() {new PageButton("Alas...", () => {
              throw new NotImplementedException();
              //exit?.Invoke();
            }) });
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
    public void OnSorcerousUCEffect(ISorcerousUCEffect effect) { effect.visitISorcerousUCEffect(this); }
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
      mode = new NormalMode(ss, game, this, showError);
      modeCapabilityId = 0;
    }

    public void OnGameEffect(IGameEffect effect) { effect.visitIGameEffect(this);  }

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
        Debug.Log("hideInput changed: " + hideInput + " to " + newHideInput);
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
          sorcerous.RemoveObserver(broadcaster, this);
        }
        player.RemoveObserver(broadcaster, this);
      }
      player = game.player;
      if (player.Exists()) {
        player.AddObserver(broadcaster, this);
        var sorcerous = player.components.GetOnlySorcerousUCOrNull();
        if (sorcerous.Exists()) {
          sorcerous.AddObserver(broadcaster, this);
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

    public void visitUnitSetEvventEffect(UnitSetEvventEffect effect) { }
    public void visitGameSetActingUnitEffect(GameSetActingUnitEffect effect) { }
    public void visitGameSetPauseBeforeNextUnitEffect(GameSetPauseBeforeNextUnitEffect effect) { }
    public void visitGameSetActionNumEffect(GameSetActionNumEffect effect) { }
    public void visitGameSetEvventEffect(GameSetEvventEffect effect) { }
  }
}
