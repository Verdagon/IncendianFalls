using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IUnitEventVisitor {
  void Visit(UnitAttackEventAsIUnitEvent obj);
  void Visit(UnitStepEventAsIUnitEvent obj);
}

}
