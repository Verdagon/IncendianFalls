using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IGameEventVisitor {
  void VisitIGameEvent(RevertedEventAsIGameEvent obj);
  void VisitIGameEvent(SetGameSpeedEventAsIGameEvent obj);
  void VisitIGameEvent(WaitForCameraEventAsIGameEvent obj);
  void VisitIGameEvent(WaitForEverythingEventAsIGameEvent obj);
  void VisitIGameEvent(WaitEventAsIGameEvent obj);
  void VisitIGameEvent(FlyCameraEventAsIGameEvent obj);
}

}
