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
      Debug.Log("Locking!");
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
    public static int sceneInitParamStartLevel = -5;

    ISuperstructure ss;
    ReplayLogger replayLogger;
    public Instantiator instantiator;
    public GameObject cameraObject;
    public GameObject stalledIndicator;
    public GameObject thinkingIndicator;

    SlowableTimerClock uiTimer;
    SlowableTimerClock cameraTimer;

    GamePresenter gamePresenter;
    Game game;
    CameraController cameraController;

    LookPanelView lookPanelView;

    InputSemaphore inputSemaphore;

    public SoundPlayer soundPlayer;

    public OverlayPaneler overlayPaneler;

    private OverlayPresenter currentErrorOverlay;
    private OverlayPresenter currentInstructionsOverlay;
    private Looker looker;

    public void Start() {
      uiTimer = new SlowableTimerClock(1.0f);
      cameraTimer = new SlowableTimerClock(1.0f);

      var timestamp = (int)DateTimeOffset.Now.ToUnixTimeMilliseconds();

      var modelSS = new Superstructure(new LoggerImpl());
      replayLogger = new ReplayLogger(modelSS, new string[] { "Latest.sslog", timestamp + ".sslog" });
      ss = new SuperstructureWrapper(modelSS);

      lookPanelView = new LookPanelView(uiTimer, overlayPaneler);

      looker = new Looker(lookPanelView);

      inputSemaphore = new InputSemaphore();

      Debug.Log("Setting up level: " + sceneInitParamStartLevel);
      var randomSeed = timestamp;
      Debug.Log("Random seed: " + randomSeed);
      //Debug.LogWarning("Hardcoding random seed!");
      //var randomSeed = 1525224206;
      //var game = ss.RequestSetupIncendianFallsGame(randomSeed, false);
      game = ss.RequestSetupEmberDeepGame(randomSeed, sceneInitParamStartLevel, false);
      //var game = ss.RequestSetupGauntletGame(randomSeed, false);

      this.cameraController =
        new CameraController(
          cameraTimer,
          cameraObject,
          new Vector3(0, 0, 0),
          new Vector3(0, -16, 8));

      gamePresenter =
          new GamePresenter(
              cameraTimer,
              soundPlayer,
              thinkingIndicator,
              ss,
              inputSemaphore,
              game,
              instantiator,
              NewOverlayPresenter,
              this.ShowError,
              this.ShowInstructions,
              cameraController,
              stalledIndicator,
              looker,
              overlayPaneler);
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
        uiTimer,
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
      uiTimer.Update();
      cameraTimer.Update();
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

      gamePresenter.Update(ray);
    }
  }
}
