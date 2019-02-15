using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IUnitEvent {
  string DStr();
  void Visit(IUnitEventVisitor visitor);
  int GetTime();
}

}
