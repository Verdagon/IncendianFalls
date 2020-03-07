using System;
using System.Collections.Generic;
using AthPlayer;
using Atharia.Model;
using UnityEngine;
using UnityEngine.UI;
using Domino;
using IncendianFalls;

namespace AthPlayer {
  public class RootPresenter : MonoBehaviour {

    SlowableTimerClock timer;
    SlowableTimerClock cinematicTimer;
    ExecutionStaller resumeStaller;
    ExecutionStaller turnStaller;

    ISuperstructure ss;
    public Instantiator instantiator;
    public GameObject cameraObject;

    GamePresenter gamePresenter;
    PlayerController playerController;
    FollowingCameraController cameraController;

    public LookPanelView lookPanelView;

    public PlayerPanelView playerPanelView;

    public NarrationPanelView messageView;

    public OverlayPanelView overlayPanelView;

    public SoundPlayer soundPlayer;

    public void Start() {
      timer = new SlowableTimerClock(1f);
      cinematicTimer = new SlowableTimerClock(1f);

      resumeStaller = new ExecutionStaller(timer, timer);
      turnStaller = new ExecutionStaller(timer, timer);

      var timestamp = (int)DateTimeOffset.Now.ToUnixTimeMilliseconds();

      var modelSS = new Superstructure(new LoggerImpl());
      new ReplayLogger(modelSS, new string[] { "Latest.sslog", timestamp + ".sslog" });
      ss = new SuperstructureWrapper(modelSS);

      //var randomSeed = timestamp;
      Debug.LogWarning("Hardcoding random seed!");
      var randomSeed = 1525224206;
      //var game = ss.RequestSetupIncendianFallsGame(randomSeed, false);
      var game = ss.RequestSetupEmberDeepGame(randomSeed, false);
      //var game = ss.RequestSetupGauntletGame(randomSeed, false);

      cameraController = new FollowingCameraController(cinematicTimer, cinematicTimer, cameraObject, game);

      gamePresenter =
          new GamePresenter(
              timer, cinematicTimer, soundPlayer, resumeStaller, turnStaller, ss, game, instantiator, messageView, overlayPanelView, cameraController);

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
              lookPanelView.GetComponent<LookPanelView>());
      playerController.Start();
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
      if (Input.GetKeyUp(KeyCode.I)) {
        InteractClicked();
      }
      if (Input.GetKeyUp(KeyCode.D)) {
        DefyClicked();
      }
      if (Input.GetKeyUp(KeyCode.C)) {
        CounterClicked();
      }
      if (Input.GetKeyUp(KeyCode.F)) {
        FireClicked();
      }
      if (Input.GetKeyUp(KeyCode.S)) {
        MireClicked();
      }
      if (Input.GetKeyUp(KeyCode.Slash)) {
        playerController.ActivateCheat("warptoend");
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

      Unit unit = null;
      if (hoveredLocation != null) {
        unit = gamePresenter.UnitAtLocation(hoveredLocation);
      }
      playerController.LookAt(unit);

      if (hoveredLocation != null && Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
        playerController.OnTileMouseClick(hoveredLocation);
      }
    }

    public void TimeAnchorMoveClicked() {
      playerController.TimeAnchorMoveClicked();
    }

    public void TimeShiftClicked() {
      playerController.TimeShiftClicked();
    }

    public void InteractClicked() {
      playerController.InteractClicked();
    }

    public void DefyClicked() {
      playerController.DefyClicked();
    }

    public void CounterClicked() {
      playerController.CounterClicked();
    }

    public void FireClicked() {
      playerController.FireClicked();
    }

    public void MireClicked() {
      playerController.MireClicked();
    }

  }
}
