using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IGameEventVisitor {
  void Visit(FlyCameraEventAsIGameEvent obj);
  void Visit(ShowOverlayEventAsIGameEvent obj);
}

}
