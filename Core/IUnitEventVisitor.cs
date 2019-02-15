using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IUnitEventVisitor {
  void Visit(UnitStepEventAsIUnitEvent obj);
  void Visit(UnitAttackEventAsIUnitEvent obj);
}

}
