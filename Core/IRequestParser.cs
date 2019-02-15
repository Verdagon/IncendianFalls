using System;
using System.Collections.Generic;

namespace Atharia.Model {

public static class IRequestParser {
  public static IRequest Parse(ParseSource source) {
    var nextThingPeek = source.PeekNextWord();
    switch (nextThingPeek) {
      case "SnapshotRequest":
        return new SnapshotRequestAsIRequest(SnapshotRequest.Parse(source));
      case "SetupGameRequest":
        return new SetupGameRequestAsIRequest(SetupGameRequest.Parse(source));
      case "InteractRequest":
        return new InteractRequestAsIRequest(InteractRequest.Parse(source));
      case "ResumeRequest":
        return new ResumeRequestAsIRequest(ResumeRequest.Parse(source));
      case "AttackRequest":
        return new AttackRequestAsIRequest(AttackRequest.Parse(source));
      case "MoveRequest":
        return new MoveRequestAsIRequest(MoveRequest.Parse(source));
      case "DefendRequest":
        return new DefendRequestAsIRequest(DefendRequest.Parse(source));
      case "FollowDirectiveRequest":
        return new FollowDirectiveRequestAsIRequest(FollowDirectiveRequest.Parse(source));
      case "TimeShiftRequest":
        return new TimeShiftRequestAsIRequest(TimeShiftRequest.Parse(source));
      default:
        throw new Exception("Unexpected: " + nextThingPeek);
    }
  }
}

}
