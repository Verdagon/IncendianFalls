using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public static class IRequestParser {
  public static IRequest Parse(ParseSource source) {
    var nextThingPeek = source.PeekNextWord();
    switch (nextThingPeek) {
      case "TimeAnchorMoveRequest":
        return new TimeAnchorMoveRequestAsIRequest(TimeAnchorMoveRequest.Parse(source));
      case "TimeShiftRequest":
        return new TimeShiftRequestAsIRequest(TimeShiftRequest.Parse(source));
      case "CounterRequest":
        return new CounterRequestAsIRequest(CounterRequest.Parse(source));
      case "DefyRequest":
        return new DefyRequestAsIRequest(DefyRequest.Parse(source));
      case "MoveRequest":
        return new MoveRequestAsIRequest(MoveRequest.Parse(source));
      case "CheatRequest":
        return new CheatRequestAsIRequest(CheatRequest.Parse(source));
      case "MireRequest":
        return new MireRequestAsIRequest(MireRequest.Parse(source));
      case "FireBombRequest":
        return new FireBombRequestAsIRequest(FireBombRequest.Parse(source));
      case "FireRequest":
        return new FireRequestAsIRequest(FireRequest.Parse(source));
      case "AttackRequest":
        return new AttackRequestAsIRequest(AttackRequest.Parse(source));
      case "CommActionRequest":
        return new CommActionRequestAsIRequest(CommActionRequest.Parse(source));
      case "ResumeRequest":
        return new ResumeRequestAsIRequest(ResumeRequest.Parse(source));
      case "InteractRequest":
        return new InteractRequestAsIRequest(InteractRequest.Parse(source));
      case "SetupTerrainRequest":
        return new SetupTerrainRequestAsIRequest(SetupTerrainRequest.Parse(source));
      case "SetupRavaArcanaGameRequest":
        return new SetupRavaArcanaGameRequestAsIRequest(SetupRavaArcanaGameRequest.Parse(source));
      case "SetupEmberDeepGameRequest":
        return new SetupEmberDeepGameRequestAsIRequest(SetupEmberDeepGameRequest.Parse(source));
      case "SetupGauntletGameRequest":
        return new SetupGauntletGameRequestAsIRequest(SetupGauntletGameRequest.Parse(source));
      case "SetupIncendianFallsGameRequest":
        return new SetupIncendianFallsGameRequestAsIRequest(SetupIncendianFallsGameRequest.Parse(source));
      default:
        throw new Exception("Unexpected: " + nextThingPeek);
    }
  }
}

}
