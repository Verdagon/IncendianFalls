using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IUnitEventVisitor {
  void Visit(UnitUnleashBideEventAsIUnitEvent obj);
  void Visit(UnitFireEventAsIUnitEvent obj);
  void Visit(UnitAttackEventAsIUnitEvent obj);
  void Visit(UnitCounteringEventAsIUnitEvent obj);
  void Visit(UnitShieldingEventAsIUnitEvent obj);
  void Visit(UnitStepEventAsIUnitEvent obj);
}

}
