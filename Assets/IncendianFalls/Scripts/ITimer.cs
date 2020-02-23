using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public delegate void ITimerCallback();

public interface ITimer {
  void ScheduleTimer(long msFromNow, ITimerCallback callback);
}
