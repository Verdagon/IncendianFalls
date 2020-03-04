using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IGameEventVisitor {
  void Visit(NarrateEventAsIGameEvent obj);
  void Visit(SetGameSpeedEventAsIGameEvent obj);
  void Visit(WaitEventAsIGameEvent obj);
  void Visit(FlyCameraEventAsIGameEvent obj);
  void Visit(ShowOverlayEventAsIGameEvent obj);
}

}
