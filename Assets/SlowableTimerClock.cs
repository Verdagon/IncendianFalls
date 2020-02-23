using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class SlowableTimerClock : IClock, ITimer {

  public class TimerEntry {
    public readonly long gameTimeMs;
    public readonly int id;
    public readonly ITimerCallback callback;
    public TimerEntry(long gameTimeMs, int id, ITimerCallback callback) {
      this.gameTimeMs = gameTimeMs;
      this.id = id;
      this.callback = callback;
    }
  }

  public class TimerEntryComparer : IComparer<TimerEntry> {
    public int Compare(TimerEntry x, TimerEntry y) {
      var timeDiff = x.gameTimeMs - y.gameTimeMs;
      if (timeDiff != 0) {
        return Math.Sign(timeDiff);
      }
      return Math.Sign(x.id - y.id);
    }
  }

  float lastUpdateRealTimeS;
  long nowGameTimeMs;
  float timeSpeedMultiplier;


  private SortedDictionary<TimerEntry, object> timers;
  private int nextTimerId = 1;


  public SlowableTimerClock(float timeSpeedMultiplier) {
    this.timeSpeedMultiplier = timeSpeedMultiplier;
    lastUpdateRealTimeS = Time.time;
    nowGameTimeMs = 0;
    timers = new SortedDictionary<TimerEntry, object>(new TimerEntryComparer());
  }

  public long GetTimeMs() {
    return nowGameTimeMs;
  }

  public void ScheduleTimer(long gameMsFromNow, ITimerCallback callback) {
    int timerId = nextTimerId++;
    timers.Add(new TimerEntry(GetTimeMs() + gameMsFromNow, timerId, callback), new object());
  }


  public void Update() {
    float nowRealTimeS = Time.time;
    float realTimeSSinceLastUpdate = nowRealTimeS - lastUpdateRealTimeS;
    float gameTimeSSinceLastUpdate = realTimeSSinceLastUpdate * timeSpeedMultiplier;
    long gameTimeMsSinceLastUpdate = (long)(gameTimeSSinceLastUpdate * 1000);
    nowGameTimeMs = nowGameTimeMs + gameTimeMsSinceLastUpdate;
    lastUpdateRealTimeS = nowRealTimeS;

    if (timers == null) {
      Debug.LogError("timers is null!?");
      return;
    }
    var timersCopy =
        new SortedDictionary<TimerEntry, object>(timers, new TimerEntryComparer());
    while (timersCopy.Count > 0) {
      var first = DictionaryUtils.GetFirstKey(timersCopy);
      //Logger.Warning("Frame at " + now + ", late by: " + (now - first.time) + ", executing?: " + (first.time < now));
      if (first.gameTimeMs < GetTimeMs()) {
        timersCopy.Remove(first);
        timers.Remove(first);
        first.callback();
      } else {
        break;
      }
    }
  }
  }
