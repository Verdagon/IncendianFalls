using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public static class IGameEventParser {
  public static IGameEvent Parse(ParseSource source) {
    var nextThingPeek = source.PeekNextWord();
    switch (nextThingPeek) {
      case "RevertedEvent":
        return new RevertedEventAsIGameEvent(RevertedEvent.Parse(source));
      case "SetGameSpeedEvent":
        return new SetGameSpeedEventAsIGameEvent(SetGameSpeedEvent.Parse(source));
      case "WaitEvent":
        return new WaitEventAsIGameEvent(WaitEvent.Parse(source));
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
