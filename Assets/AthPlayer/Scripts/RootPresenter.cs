using System;
using System.Collections.Generic;
using AthPlayer;
using Atharia.Model;
using UnityEngine;
using UnityEngine.UI;
using Domino;
using IncendianFalls;
using UnityEngine.SceneManagement;

namespace AthPlayer {
  public delegate OverlayPresenter OverlayPresenterFactory(ShowOverlayEvent overlay);
  public delegate void ShowError(string error);
  public delegate void ShowInstructions(string error);

  public class InputSemaphore {
    public delegate void Locked();
    public event Locked OnLocked;
    public delegate void Unlocked();
    public event Unlocked OnUnlocked;

    private int count = 0;

    public bool locked { get { return count > 0; } }

    public void Lock() {
      if (count == 0) {
        OnLocked?.Invoke();
      }
      count++;
    }
    public void Unlock() {
      Asserts.Assert(count > 0);
      count--;
      if (count == 0) {
        OnUnlocked?.Invoke();
      }
    }
  }

  public class RootPresenter : MonoBehaviour {
    public static int sceneInitParamStartLevel = 7;

    SlowableTimerClock timer;
    SlowableTimerClock cinematicTimer;
    ExecutionStaller resumeStaller;
    ExecutionStaller turnStaller;

    ISuperstructure ss;
    ReplayLogger replayLogger;
    public Instantiator instantiator;
    public GameObject cameraObject;
    public GameObject stalledIndicator;
    public GameObject thinkingIndicator;

    GamePresenter gamePresenter;
    PlayerController playerController;
    Game game;
    FollowingCameraController cameraController;

    InputSemaphore inputSemaphore;

    LookPanelView lookPanelView;

    public SoundPlayer soundPlayer;

    public OverlayPaneler overlayPaneler;

    private OverlayPresenter currentErrorOverlay;
    private OverlayPresenter currentInstructionsOverlay;

    public void Start() {
      timer = new SlowableTimerClock(1f);
      cinematicTimer = new SlowableTimerClock(1f);

      inputSemaphore = new InputSemaphore();
      inputSemaphore.OnLocked += () => timer.SetTimeSpeedMultiplier(0f);
      inputSemaphore.OnUnlocked += () => timer.SetTimeSpeedMultiplier(1f);

      resumeStaller = new ExecutionStaller(timer, timer);
      turnStaller = new ExecutionStaller(timer, timer);

      turnStaller.stalledEvent += (x) => {
        stalledIndicator.SetActive(true);
      };
      turnStaller.unstalledEvent += (x) => {
        stalledIndicator.SetActive(false);
      };

      var timestamp = (int)DateTimeOffset.Now.ToUnixTimeMilliseconds();

      var modelSS = new Superstructure(new LoggerImpl());
      replayLogger = new ReplayLogger(modelSS, new string[] { "Latest.sslog", timestamp + ".sslog" });
      ss = new SuperstructureWrapper(modelSS);

      lookPanelView = new LookPanelView(cinematicTimer, overlayPaneler);

      Debug.Log("Setting up level: " + sceneInitParamStartLevel);
      var randomSeed = timestamp;
      Debug.Log("Random seed: " + randomSeed);
      //Debug.LogWarning("Hardcoding random seed!");
      //var randomSeed = 1525224206;
      //var game = ss.RequestSetupIncendianFallsGame(randomSeed, false);
      game = ss.RequestSetupEmberDeepGame(randomSeed, sceneInitParamStartLevel, false);
      //var game = ss.RequestSetupGauntletGame(randomSeed, false);

      cameraController = new FollowingCameraController(cinematicTimer, cinematicTimer, cameraObject, game);

      gamePresenter =
          new GamePresenter(
              timer,
              cinematicTimer,
              soundPlayer,
              resumeStaller,
              turnStaller,
              thinkingIndicator,
              ss,
              game,
              instantiator,
              NewOverlayPresenter,
              this.ShowError,
              this.ShowInstructions,
              cameraController);

      playerController =
          new PlayerController(
              timer,
              cinematicTimer,
              resumeStaller,
              turnStaller,
              inputSemaphore,
              ss,
              ss.GetSuperstate(game.id),
              game,
              gamePresenter,
              lookPanelView,
              overlayPaneler,
              NewOverlayPresenter,
              this.ShowError);
      playerController.Start();
    }

    private OverlayPresenter NewOverlayPresenter(ShowOverlayEvent overlay) {
      var buttons = new List<OverlayPresenter.PageButton>();
      foreach (var button in overlay.buttons) {
        buttons.Add(
          new OverlayPresenter.PageButton(
            button.label,
            () => {
              if (button.triggerName == "_exitGame") {
                replayLogger.Dispose();
                SceneManager.LoadScene("MainMenu");
              } else {
                ss.RequestTrigger(game.id, button.triggerName);
              }
            }));
      }
      return new OverlayPresenter(
        cinematicTimer,
        overlayPaneler,
        inputSemaphore,
        overlay.template,
        overlay.speakerRole,
        overlay.isFirstInSequence,
        overlay.isLastInSequence,
        overlay.isObscuring,
        overlay.text,
        buttons);
    }

    private void ShowError(string errorText) {
      if (currentErrorOverlay != null) {
        currentErrorOverlay.Close();
      }
      currentErrorOverlay =
        NewOverlayPresenter(
          new ShowOverlayEvent(
            errorText, "error", "narrator", true, true, false, new ButtonImmList()));
    }

    private void ShowInstructions(string errorText) {
      Debug.LogError("Showing instructions: " + errorText);
      if (currentInstructionsOverlay != null) {
        currentInstructionsOverlay.Close();
        currentInstructionsOverlay = null;
      }
      if (errorText.Length > 0) {
        currentInstructionsOverlay =
          NewOverlayPresenter(
            new ShowOverlayEvent(
              errorText, "instructions", "narrator", true, true, false, new ButtonImmList()));
      }
    }

    public void Update() {
      timer.Update();
      cinematicTimer.Update();

      playerController.Update();

      if (Input.GetKey(KeyCode.RightArrow)) {
        if (inputSemaphore.locked) {
          Debug.LogError("Rejecting input, locked!");
        } else {
          cameraController.MoveRight(Time.deltaTime);
        }
      }
      if (Input.GetKey(KeyCode.LeftArrow)) {
        if (inputSemaphore.locked) {
          Debug.LogError("Rejecting input, locked!");
        } else {
          cameraController.MoveLeft(Time.deltaTime);
        }
      }
      if (Input.GetKey(KeyCode.UpArrow)) {
        if (inputSemaphore.locked) {
          Debug.LogError("Rejecting input, locked!");
        } else {
          cameraController.MoveUp(Time.deltaTime);
        }
      }
      if (Input.GetKey(KeyCode.DownArrow)) {
        if (inputSemaphore.locked) {
          Debug.LogError("Rejecting input, locked!");
        } else {
          cameraController.MoveDown(Time.deltaTime);
        }
      }
      if (Input.GetKey(KeyCode.RightBracket)) {
        if (inputSemaphore.locked) {
          Debug.LogError("Rejecting input, locked!");
        } else {
          cameraController.MoveIn(Time.deltaTime);
        }
      }
      if (Input.GetKey(KeyCode.LeftBracket)) {
        if (inputSemaphore.locked) {
          Debug.LogError("Rejecting input, locked!");
        } else {
          cameraController.MoveOut(Time.deltaTime);
        }
      }

      UnityEngine.Ray ray = cameraObject.GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);

      Location hoveredLocation = null;
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit)) {
        if (hit.collider != null) {
          hoveredLocation = gamePresenter.LocationFor(hit.collider.gameObject);
        }
      }
      gamePresenter.SetHighlightedLocation(hoveredLocation);

      Unit unit = Unit.Null;
      TerrainTile tile = TerrainTile.Null;
      if (hoveredLocation != null) {
        unit = gamePresenter.UnitAtLocation(hoveredLocation);
        tile = gamePresenter.TileAtLocation(hoveredLocation);
      }
      playerController.LookAt(unit, tile);

      if (hoveredLocation != null && Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
        if (inputSemaphore.locked) {
          Debug.LogError("Rejecting input, locked!");
        } else {
          playerController.OnTileMouseClick(hoveredLocation);
        }
      }
    }

    //public void TimeAnchorMoveClicked() {
    //  thinkingIndicator.SetActive(true);
    //  timer.ScheduleTimer(0, () => {
    //    playerController.TimeAnchorMoveClicked();
    //    thinkingIndicator.SetActive(false);
    //  });
    //}

    //public void TimeShiftClicked() {
    //  thinkingIndicator.SetActive(true);
    //  timer.ScheduleTimer(0, () => {
    //    playerController.TimeShiftClicked();
    //    thinkingIndicator.SetActive(false);
    //  });
    //}

    //public void InteractClicked() {
    //  thinkingIndicator.SetActive(true);
    //  timer.ScheduleTimer(0, () => {
    //    playerController.InteractClicked();
    //    thinkingIndicator.SetActive(false);
    //  });
    //}

    //public void DefyClicked() {
    //  thinkingIndicator.SetActive(true);
    //  timer.ScheduleTimer(0, () => {
    //    playerController.DefyClicked();
    //    thinkingIndicator.SetActive(false);
    //  });
    //}

    //public void CounterClicked() {
    //  thinkingIndicator.SetActive(true);
    //  timer.ScheduleTimer(0, () => {
    //    playerController.CounterClicked();
    //    thinkingIndicator.SetActive(false);
    //  });
    //}

    //public void FireClicked() {
    //  thinkingIndicator.SetActive(true);
    //  timer.ScheduleTimer(0, () => {
    //    playerController.FireClicked();
    //    thinkingIndicator.SetActive(false);
    //  });
    //}

    //public void FireBombClicked() {
    //  thinkingIndicator.SetActive(true);
    //  timer.ScheduleTimer(0, () => {
    //    playerController.FireBombClicked();
    //    thinkingIndicator.SetActive(false);
    //  });
    //}

    //public void MireClicked() {
    //  thinkingIndicator.SetActive(true);
    //  timer.ScheduleTimer(0, () => {
    //    playerController.MireClicked();
    //    thinkingIndicator.SetActive(false);
    //  });
    //}

    //public void CancelClicked() {
    //  thinkingIndicator.SetActive(true);
    //  timer.ScheduleTimer(0, () => {
    //    playerController.CancelClicked();
    //    thinkingIndicator.SetActive(false);
    //  });
    //}

  }
}
