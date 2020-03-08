﻿using System;

namespace Domino {
  public class ExecutionStaller {
    long stallUntilMs;
    IClock clock;
    ITimer timer;

    public delegate void UnstalledHandler(object sender);
    public event UnstalledHandler unstalledEvent;
    public delegate void StalledHandler(object sender);
    public event StalledHandler stalledEvent;

    public ExecutionStaller(IClock clock, ITimer timer) {
      this.clock = clock;
      this.timer = timer;
      this.stallUntilMs = clock.GetTimeMs();
    }

    public void StallForDuration(long durationMs) {
      if (!IsStalled()) {
        stalledEvent?.Invoke(new object());
      }
      long currentTime = clock.GetTimeMs();
      long newStallUntilMs = currentTime + durationMs;
      if (newStallUntilMs > stallUntilMs) {
        stallUntilMs = newStallUntilMs;
        timer.ScheduleTimer(durationMs, () => OnTimerExpired());
      }
    }

    public void OnTimerExpired() {
      if (!IsStalled()) {
        unstalledEvent(this);
      }
    }

    public bool IsStalled() {
      return clock.GetTimeMs() < stallUntilMs;
    }
  }
}
