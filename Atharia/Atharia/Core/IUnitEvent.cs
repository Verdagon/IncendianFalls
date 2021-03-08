using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUnitEvent {
  string DStr();
  int GetDeterministicHashCode();
  void VisitIUnitEvent(IUnitEventVisitor visitor);
}

}
