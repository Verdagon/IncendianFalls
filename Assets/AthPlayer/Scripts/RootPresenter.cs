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
  public class RootPresenter : MonoBehaviour {
    public static int sceneInitParamStartLevel = 0;

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

    OldOverlayPresenter overlayPresenter;
    GamePresenter gamePresenter;
    PlayerController playerController;
    FollowingCameraController cameraController;

    public LookPanelView lookPanelView;

    public PlayerPanelView playerPanelView;

    public NarrationPanelView messageView;

    public OldOverlayPanelView overlayPanelView;

    public SoundPlayer soundPlayer;

    public OverlayPaneler overlayPaneler;

    public void Start() {
      timer = new SlowableTimerClock(1f);
      cinematicTimer = new SlowableTimerClock(1f);

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

      var randomSeed = timestamp;
      Debug.Log("Random seed: " + randomSeed);
      //Debug.LogWarning("Hardcoding random seed!");
      //var randomSeed = 1525224206;
      //var game = ss.RequestSetupIncendianFallsGame(randomSeed, false);
      var game = ss.RequestSetupEmberDeepGame(randomSeed, sceneInitParamStartLevel, false);
      //var game = ss.RequestSetupGauntletGame(randomSeed, false);

      cameraController = new FollowingCameraController(cinematicTimer, cinematicTimer, cameraObject, game);

      overlayPresenter = new OldOverlayPresenter(timer, cinematicTimer, ss, game, overlayPaneler, overlayPanelView);
      overlayPresenter.Exit += () => {
        replayLogger.Dispose();
        SceneManager.LoadScene("MainMenu");
      };

      gamePresenter =
          new GamePresenter(
              timer, cinematicTimer, soundPlayer, resumeStaller, turnStaller, thinkingIndicator, ss, game, instantiator, messageView, overlayPresenter, cameraController);

      playerController =
          new PlayerController(
              timer,
              resumeStaller,
              turnStaller,
              ss,
              ss.GetSuperstate(game.id),
              game,
              gamePresenter,
              playerPanelView,
              messageView,
              lookPanelView.GetComponent<LookPanelView>(),
              overlayPresenter);
      playerController.Start();

      overlayPresenter.ShowTopOverlayWithButtons(
        "A long time ago, before the phoenix flew the skies, before the volcanoes roamed the coasts, and before the sylvans raised their towers, there was only the Chronicler.\n\nAges passed, mountains rose, and the first trees and animals woke for the first time. The Chronicler recorded it all.");
    }

    public void Update() {
      timer.Update();
      cinematicTimer.Update();

      if (Input.GetKeyUp(KeyCode.A)) {
        TimeAnchorMoveClicked();
      }
      if (Input.GetKeyUp(KeyCode.R)) {
        TimeShiftClicked();
      }
      if (Input.GetKeyUp(KeyCode.E)) {
        InteractClicked();
      }
      if (Input.GetKeyUp(KeyCode.D)) {
        DefyClicked();
      }
      if (Input.GetKeyUp(KeyCode.C)) {
        CounterClicked();
      }
      if (Input.GetKeyUp(KeyCode.F)) {
        FireBombClicked();
      }
      if (Input.GetKeyUp(KeyCode.B)) {
        FireBombClicked();
      }
      if (Input.GetKeyUp(KeyCode.S)) {
        MireClicked();
      }
      if (Input.GetKeyUp(KeyCode.Escape)) {
        CancelClicked();
      }
      if (Input.GetKeyUp(KeyCode.Slash)) {
        playerController.ActivateCheat("warptoend");
      }
      if (Input.GetKeyUp(KeyCode.Equals)) {
        playerController.ActivateCheat("poweroverwhelming");
      }
      if (Input.GetKeyUp(KeyCode.Alpha8)) {
        playerController.ActivateCheat("gimmeblastrod");
      }
      if (Input.GetKeyUp(KeyCode.Alpha6)) {
        playerController.ActivateCheat("gimmeslowrod");
      }
      if (Input.GetKeyUp(KeyCode.Alpha7)) {
        playerController.ActivateCheat("gimmearmor");
      }
      if (Input.GetKeyUp(KeyCode.Alpha9)) {
        playerController.ActivateCheat("gimmesword");
      }

      if (Input.GetKey(KeyCode.RightArrow)) {
        cameraController.MoveRight(Time.deltaTime);
      }
      if (Input.GetKey(KeyCode.LeftArrow)) {
        cameraController.MoveLeft(Time.deltaTime);
      }
      if (Input.GetKey(KeyCode.UpArrow)) {
        cameraController.MoveUp(Time.deltaTime);
      }
      if (Input.GetKey(KeyCode.DownArrow)) {
        cameraController.MoveDown(Time.deltaTime);
      }
      if (Input.GetKey(KeyCode.RightBracket)) {
        cameraController.MoveIn(Time.deltaTime);
      }
      if (Input.GetKey(KeyCode.LeftBracket)) {
        cameraController.MoveOut(Time.deltaTime);
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
        playerController.OnTileMouseClick(hoveredLocation);
      }
    }

    public void TimeAnchorMoveClicked() {
      thinkingIndicator.SetActive(true);
      timer.ScheduleTimer(0, () => {
        playerController.TimeAnchorMoveClicked();
        thinkingIndicator.SetActive(false);
      });
    }

    public void TimeShiftClicked() {
      thinkingIndicator.SetActive(true);
      timer.ScheduleTimer(0, () => {
        playerController.TimeShiftClicked();
        thinkingIndicator.SetActive(false);
      });
    }

    public void InteractClicked() {
      thinkingIndicator.SetActive(true);
      timer.ScheduleTimer(0, () => {
        playerController.InteractClicked();
        thinkingIndicator.SetActive(false);
      });
    }

    public void DefyClicked() {
      thinkingIndicator.SetActive(true);
      timer.ScheduleTimer(0, () => {
        playerController.DefyClicked();
        thinkingIndicator.SetActive(false);
      });
    }

    public void CounterClicked() {
      thinkingIndicator.SetActive(true);
      timer.ScheduleTimer(0, () => {
        playerController.CounterClicked();
        thinkingIndicator.SetActive(false);
      });
    }

    public void FireClicked() {
      thinkingIndicator.SetActive(true);
      timer.ScheduleTimer(0, () => {
        playerController.FireClicked();
        thinkingIndicator.SetActive(false);
      });
    }

    public void FireBombClicked() {
      thinkingIndicator.SetActive(true);
      timer.ScheduleTimer(0, () => {
        playerController.FireBombClicked();
        thinkingIndicator.SetActive(false);
      });
    }

    public void MireClicked() {
      thinkingIndicator.SetActive(true);
      timer.ScheduleTimer(0, () => {
        playerController.MireClicked();
        thinkingIndicator.SetActive(false);
      });
    }

    public void CancelClicked() {
      thinkingIndicator.SetActive(true);
      timer.ScheduleTimer(0, () => {
        playerController.CancelClicked();
        thinkingIndicator.SetActive(false);
      });
    }

  }
}
