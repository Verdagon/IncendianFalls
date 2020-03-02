using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IRequestVisitor {
  void Visit(TriggerRequestAsIRequest obj);
  void Visit(TimeAnchorMoveRequestAsIRequest obj);
  void Visit(TimeShiftRequestAsIRequest obj);
  void Visit(FollowDirectiveRequestAsIRequest obj);
  void Visit(CounterRequestAsIRequest obj);
  void Visit(DefendRequestAsIRequest obj);
  void Visit(MoveRequestAsIRequest obj);
  void Visit(CheatRequestAsIRequest obj);
  void Visit(FireRequestAsIRequest obj);
  void Visit(AttackRequestAsIRequest obj);
  void Visit(ResumeRequestAsIRequest obj);
  void Visit(InteractRequestAsIRequest obj);
  void Visit(SetupTerrainRequestAsIRequest obj);
  void Visit(SetupEmberDeepGameRequestAsIRequest obj);
  void Visit(SetupGauntletGameRequestAsIRequest obj);
  void Visit(SetupIncendianFallsGameRequestAsIRequest obj);
}

}
