using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public delegate void ITimerCallback();

public interface ITimer {
  int ScheduleTimer(long msFromNow, ITimerCallback callback);
  void CancelTimer(int timerId);
}
