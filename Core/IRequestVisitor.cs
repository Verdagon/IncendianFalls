using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IRequestVisitor {
  void Visit(SnapshotRequestAsIRequest obj);
  void Visit(SetupGameRequestAsIRequest obj);
  void Visit(InteractRequestAsIRequest obj);
  void Visit(ResumeRequestAsIRequest obj);
  void Visit(AttackRequestAsIRequest obj);
  void Visit(MoveRequestAsIRequest obj);
  void Visit(DefendRequestAsIRequest obj);
  void Visit(FollowDirectiveRequestAsIRequest obj);
  void Visit(TimeShiftRequestAsIRequest obj);
}

}
