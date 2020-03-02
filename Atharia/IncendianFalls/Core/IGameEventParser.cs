using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public static class IGameEventParser {
  public static IGameEvent Parse(ParseSource source) {
    var nextThingPeek = source.PeekNextWord();
    switch (nextThingPeek) {
      case "FlyCameraEvent":
        return new FlyCameraEventAsIGameEvent(FlyCameraEvent.Parse(source));
      case "ShowOverlayEvent":
        return new ShowOverlayEventAsIGameEvent(ShowOverlayEvent.Parse(source));
      default:
        throw new Exception("Unexpected: " + nextThingPeek);
    }
  }
}

}
