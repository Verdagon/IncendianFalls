using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IRequestVisitor {
  void VisitIRequest(TriggerRequestAsIRequest obj);
  void VisitIRequest(TimeAnchorMoveRequestAsIRequest obj);
  void VisitIRequest(TimeShiftRequestAsIRequest obj);
  void VisitIRequest(FollowDirectiveRequestAsIRequest obj);
  void VisitIRequest(CounterRequestAsIRequest obj);
  void VisitIRequest(DefyRequestAsIRequest obj);
  void VisitIRequest(MoveRequestAsIRequest obj);
  void VisitIRequest(CheatRequestAsIRequest obj);
  void VisitIRequest(MireRequestAsIRequest obj);
  void VisitIRequest(FireBombRequestAsIRequest obj);
  void VisitIRequest(FireRequestAsIRequest obj);
  void VisitIRequest(CancelRequestAsIRequest obj);
  void VisitIRequest(AttackRequestAsIRequest obj);
  void VisitIRequest(ResumeRequestAsIRequest obj);
  void VisitIRequest(InteractRequestAsIRequest obj);
  void VisitIRequest(SetupTerrainRequestAsIRequest obj);
  void VisitIRequest(SetupEmberDeepGameRequestAsIRequest obj);
  void VisitIRequest(SetupGauntletGameRequestAsIRequest obj);
  void VisitIRequest(SetupIncendianFallsGameRequestAsIRequest obj);
}

}
