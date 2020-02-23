using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUnitEvent {
  string DStr();
  int GetDeterministicHashCode();
  void Visit(IUnitEventVisitor visitor);
  int GetTime();
}

}
