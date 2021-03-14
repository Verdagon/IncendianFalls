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
        ICommTemplate template,
        List<PageText> pageTexts,
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
      Debug.Log("Unlocking!");
      Asserts.Assert(count > 0);
      count--;
      if (count == 0) {
        OnUnlocked?.Invoke();
      }
    }
  }

  public class RootPresenter : MonoBehaviour {
    public static int sceneInitParamStartLevel = 0;

    Superstructure serverSS;
    ReplayLogger replayLogger;
    public Instantiator instantiator;
    public GameObject cameraObject;
    public GameObject stalledIndicator;
    public GameObject thinkingIndicator;

    SlowableTimerClock uiTimer;
    SlowableTimerClock cameraTimer;

    GameController gamePresenter;
    CameraController cameraController;

    LookPanelView lookPanelView;

    InputSemaphore inputSemaphore;

    public SoundPlayer soundPlayer;

    public GameObject panelRootGameObject;

    OverlayPaneler overlayPaneler;

    private OverlayPresenter currentErrorOverlay;

    public void Start() {
      uiTimer = new SlowableTimerClock(1.0f);
      cameraTimer = new SlowableTimerClock(1.0f);

      var timestamp = (int)DateTimeOffset.Now.ToUnixTimeMilliseconds();

      serverSS = new Superstructure(new LoggerImpl());
      replayLogger = new ReplayLogger(serverSS, new string[] { "Latest.sslog", timestamp + ".sslog" });

      inputSemaphore = new InputSemaphore();

      overlayPaneler = new OverlayPaneler(panelRootGameObject, instantiator, uiTimer);
      //gameOverlay = overlayPaneler.MakePanel(uiTimer, 0, 0, 100, 100, gridWidth, gridHeight, .6667f);

      lookPanelView = new LookPanelView(overlayPaneler, PlayerPanelView.PANEL_GH, 0);

      // fullscreen would be from -1,-1 to w+2 h+2.

      this.cameraController =
        new CameraController(
          cameraTimer,
          cameraObject,
          new Vector3(0, 0, 0),
          new Vector3(0, -16, 8));

      Debug.Log("Setting up level: " + sceneInitParamStartLevel);
      var randomSeed = timestamp;
      gamePresenter =
          new GameController(
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
        // TODO: dont use the model's template objects
        ICommTemplate template,
        List<PageText> pageTexts,
        List<PageButton> buttons) {
      return new OverlayPresenter(uiTimer, overlayPaneler, inputSemaphore, template, pageTexts, buttons);
    }

    private void ShowError(string text) {
      if (currentErrorOverlay != null) {
        currentErrorOverlay.Close();
      }
      currentErrorOverlay =
        NewOverlayPresenter(
          new ErrorCommTemplate().AsICommTemplate(),
          new List<PageText>() { new PageText(text, new UnityEngine.Color(1, .5f, .5f)) },
          new List<PageButton>());
    }

    private OverlayPresenter ShowInstructions(string text) {
      return NewOverlayPresenter(
        new InstructionsCommTemplate().AsICommTemplate(),
        new List<PageText>() { new PageText(text, new UnityEngine.Color(1, 1, 1)) },
        new List<PageButton>());
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
