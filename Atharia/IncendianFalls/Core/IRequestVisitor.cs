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
  void Visit(DefyRequestAsIRequest obj);
  void Visit(MoveRequestAsIRequest obj);
  void Visit(CheatRequestAsIRequest obj);
  void Visit(MireRequestAsIRequest obj);
  void Visit(FireBombRequestAsIRequest obj);
  void Visit(FireRequestAsIRequest obj);
  void Visit(CancelRequestAsIRequest obj);
  void Visit(AttackRequestAsIRequest obj);
  void Visit(ResumeRequestAsIRequest obj);
  void Visit(InteractRequestAsIRequest obj);
  void Visit(SetupTerrainRequestAsIRequest obj);
  void Visit(SetupEmberDeepGameRequestAsIRequest obj);
  void Visit(SetupGauntletGameRequestAsIRequest obj);
  void Visit(SetupIncendianFallsGameRequestAsIRequest obj);
}

}
