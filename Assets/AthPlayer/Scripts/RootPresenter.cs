using System;
using System.Collections.Generic;
using AthPlayer;
using Atharia.Model;
using UnityEngine;
using UnityEngine.UI;
using Domino;
using IncendianFalls;
using UnityEngine.SceneManagement;
using static AthPlayer.OverlayPresenter;

namespace AthPlayer {
  public delegate OverlayPresenter
    OverlayPresenterFactory(
      string template,
      string role,
      bool isFirstInSequence,
      bool isLastInSequence,
      bool isObscuring,
      string unwrappedText,
      List<PageButton> buttons);
  public delegate void ShowError(string error);
  public delegate OverlayPresenter ShowInstructions(string error);

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

    Superstructure serverSS;
    ReplayLogger replayLogger;
    public Instantiator instantiator;
    public GameObject cameraObject;
    public GameObject stalledIndicator;
    public GameObject thinkingIndicator;

    SlowableTimerClock uiTimer;
    SlowableTimerClock cameraTimer;

    GamePresenter gamePresenter;
    CameraController cameraController;

    LookPanelView lookPanelView;

    InputSemaphore inputSemaphore;

    public SoundPlayer soundPlayer;

    public OverlayPaneler overlayPaneler;

    private OverlayPresenter currentErrorOverlay;

    public void Start() {
      uiTimer = new SlowableTimerClock(1.0f);
      cameraTimer = new SlowableTimerClock(1.0f);

      var timestamp = (int)DateTimeOffset.Now.ToUnixTimeMilliseconds();

      serverSS = new Superstructure(new LoggerImpl());
      replayLogger = new ReplayLogger(serverSS, new string[] { "Latest.sslog", timestamp + ".sslog" });

      lookPanelView = new LookPanelView(uiTimer, overlayPaneler);

      inputSemaphore = new InputSemaphore();

      this.cameraController =
        new CameraController(
          cameraTimer,
          cameraObject,
          new Vector3(0, 0, 0),
          new Vector3(0, -16, 8));

      Debug.Log("Setting up level: " + sceneInitParamStartLevel);
      var randomSeed = timestamp;
      gamePresenter =
          new GamePresenter(
              cameraTimer,
              soundPlayer,
              thinkingIndicator,
              serverSS,
              inputSemaphore,
              instantiator,
              this.NewOverlayPresenter,
              this.ShowError,
              this.ShowInstructions,
              cameraController,
              stalledIndicator,
              lookPanelView,
              overlayPaneler,
              randomSeed,
              sceneInitParamStartLevel);
      gamePresenter.exitClicked += () => {
        replayLogger.Dispose();
        SceneManager.LoadScene("MainMenu");
      };
    }

    private OverlayPresenter NewOverlayPresenter(
        string template,
        string role,
        bool isFirstInSequence,
        bool isLastInSequence,
        bool isObscuring,
        string unwrappedText,
        List<PageButton> buttons) {
      return new OverlayPresenter(uiTimer, overlayPaneler, inputSemaphore, template, role, isFirstInSequence, isLastInSequence, isObscuring, unwrappedText, buttons);
    }

    private void ShowError(string errorText) {
      if (currentErrorOverlay != null) {
        currentErrorOverlay.Close();
      }
      currentErrorOverlay =
        NewOverlayPresenter("error", "narrator", true, true, false, errorText, new List<PageButton>());
    }

    private OverlayPresenter ShowInstructions(string errorText) {
      return NewOverlayPresenter("instructions", "narrator", true, true, false, errorText, new List<PageButton>());
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
