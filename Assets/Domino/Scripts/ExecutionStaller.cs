using System;

namespace Domino {
  public class ExecutionStaller {
    float stallUntil;
    ITimer timer;

    public delegate void UnstalledHandler(object sender);
    public event UnstalledHandler unstalledEvent;

    public ExecutionStaller(ITimer timer) {
      this.timer = timer;
      this.stallUntil = timer.GetTime();
    }

    public void StallForDuration(float duration) {
      float currentTime = timer.GetTime();
      float newStallUntil = currentTime + duration;
      if (newStallUntil > stallUntil) {
        stallUntil = newStallUntil;
        timer.ScheduleTimer(duration, () => OnTimerExpired());
      }
    }

    public void OnTimerExpired() {
      if (!IsStalled()) {
        unstalledEvent(this);
      }
    }

    public bool IsStalled() {
      return timer.GetTime() < stallUntil;
    }
  }
}
