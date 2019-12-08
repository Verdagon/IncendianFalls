using System;
using System.Collections.Generic;
using IncendianFalls;
using Atharia.Model;
using UnityEngine;
using UnityEngine.UI;
using Domino;

namespace IncendianFalls {
  public class TimerEntry {
    public readonly float time;
    public readonly int id;
    public readonly ITimerCallback callback;
    public TimerEntry(float time, int id, ITimerCallback callback) {
      this.time = time;
      this.id = id;
      this.callback = callback;
    }
  }

  public class TimerEntryComparer : IComparer<TimerEntry> {
    public int Compare(TimerEntry x, TimerEntry y) {
      var timeDiff = x.time - y.time;
      if (timeDiff != 0) {
        return Math.Sign(timeDiff);
      }
      return Math.Sign(x.id - y.id);
    }
  }

  public class RootPresenter : MonoBehaviour, ITimer {
    ExecutionStaller resumeStaller;
    ExecutionStaller turnStaller;

    ISuperstructure ss;
    public Instantiator instantiator;
    public GameObject cameraObject;

    GamePresenter gamePresenter;
    PlayerController playerController;
    FollowingCameraController cameraController;

    private DateTime startTime;
    private SortedDictionary<TimerEntry, object> timers;
    private int nextTimerId = 1;
    
    public LookPanelView lookPanelView;

    public PlayerPanelView playerPanelView;

    public NarrationPanelView messageView;

    public SoundPlayer soundPlayer;

    public void Start() {
      startTime = System.DateTime.UtcNow;
      timers = new SortedDictionary<TimerEntry, object>(new TimerEntryComparer());
      resumeStaller = new ExecutionStaller(this);
      turnStaller = new ExecutionStaller(this);

      var timestamp = (int)DateTimeOffset.Now.ToUnixTimeMilliseconds();

      var modelSS = new Superstructure(new LoggerImpl());
      new ReplayLogger(modelSS, new string[] { "Latest.sslog", timestamp + ".sslog" });
      ss = new SuperstructureWrapper(modelSS);

      var randomSeed = timestamp;
      //var randomSeed = 1525224206;
      var game = ss.RequestSetupGame(randomSeed, false, false);
      gamePresenter =
          new GamePresenter(
              this, soundPlayer, resumeStaller, turnStaller, ss, game, instantiator, messageView);

      playerController =
          new PlayerController(
              this,
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

      cameraController = new FollowingCameraController(cameraObject, game);
    }

    public float GetTime() {
      DateTime now = System.DateTime.UtcNow;
      float difference = (float)now.Subtract(startTime).TotalMilliseconds / 1000.0f;
      return difference;
    }

    public void ScheduleTimer(float secondsFromNow, ITimerCallback callback) {
      int timerId = nextTimerId++;
      float now = GetTime();
      timers.Add(new TimerEntry(now + secondsFromNow, timerId, callback), new object());
    }

    public void Update() {
      if (timers == null) {
        Debug.LogError("timers is null!?");
        return;
      }
      var timersCopy =
          new SortedDictionary<TimerEntry, object>(timers, new TimerEntryComparer());
      while (timersCopy.Count > 0) {
        float now = GetTime();
        var first = DictionaryUtils.GetFirstKey(timersCopy);
        //Logger.Warning("Frame at " + now + ", late by: " + (now - first.time) + ", executing?: " + (first.time < now));
        if (first.time < now) {
          timersCopy.Remove(first);
          timers.Remove(first);
          first.callback();
        } else {
          break;
        }
      }

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
        DefendClicked();
      }
      if (Input.GetKeyUp(KeyCode.C)) {
        CounterClicked();
      }
      if (Input.GetKeyUp(KeyCode.F)) {
        FireClicked();
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

    public void DefendClicked() {
      playerController.DefendClicked();
    }

    public void CounterClicked() {
      playerController.CounterClicked();
    }

    public void FireClicked() {
      playerController.FireClicked();
    }

  }
}
