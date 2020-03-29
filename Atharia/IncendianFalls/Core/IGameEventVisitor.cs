using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IGameEventVisitor {
  void VisitIGameEvent(RevertedEventAsIGameEvent obj);
  void VisitIGameEvent(SetGameSpeedEventAsIGameEvent obj);
  void VisitIGameEvent(WaitForAnimationsEventAsIGameEvent obj);
  void VisitIGameEvent(WaitEventAsIGameEvent obj);
  void VisitIGameEvent(FlyCameraEventAsIGameEvent obj);
}

}
