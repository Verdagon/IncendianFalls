using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IUnitEventVisitor {
  void VisitIUnitEvent(UnitUnleashBideEventAsIUnitEvent obj);
  void VisitIUnitEvent(UnitFireBombedEventAsIUnitEvent obj);
  void VisitIUnitEvent(UnitFireEventAsIUnitEvent obj);
  void VisitIUnitEvent(UnitMireEventAsIUnitEvent obj);
  void VisitIUnitEvent(UnitAttackEventAsIUnitEvent obj);
  void VisitIUnitEvent(UnitCounteringEventAsIUnitEvent obj);
  void VisitIUnitEvent(UnitDefyingEventAsIUnitEvent obj);
  void VisitIUnitEvent(UnitStepEventAsIUnitEvent obj);
  void VisitIUnitEvent(WaitForUnitEventAsIUnitEvent obj);
}

}
