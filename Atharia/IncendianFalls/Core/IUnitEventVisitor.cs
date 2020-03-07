using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IUnitEventVisitor {
  void Visit(UnitUnleashBideEventAsIUnitEvent obj);
  void Visit(UnitFireBombedEventAsIUnitEvent obj);
  void Visit(UnitFireEventAsIUnitEvent obj);
  void Visit(UnitMireEventAsIUnitEvent obj);
  void Visit(UnitAttackEventAsIUnitEvent obj);
  void Visit(UnitCounteringEventAsIUnitEvent obj);
  void Visit(UnitDefyingEventAsIUnitEvent obj);
  void Visit(UnitStepEventAsIUnitEvent obj);
}

}
