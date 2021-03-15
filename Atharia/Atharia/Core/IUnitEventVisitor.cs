using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IUnitEventVisitor {
  void VisitIUnitEvent(UnitUnleashBideEventAsIUnitEvent obj);
  void VisitIUnitEvent(UnitExplosionedEventAsIUnitEvent obj);
  void VisitIUnitEvent(UnitBurningEventAsIUnitEvent obj);
  void VisitIUnitEvent(UnitBlazedEventAsIUnitEvent obj);
  void VisitIUnitEvent(UnitFireBombedEventAsIUnitEvent obj);
  void VisitIUnitEvent(UnitExplosionEventAsIUnitEvent obj);
  void VisitIUnitEvent(UnitBlazeEventAsIUnitEvent obj);
  void VisitIUnitEvent(UnitFireEventAsIUnitEvent obj);
  void VisitIUnitEvent(UnitMireEventAsIUnitEvent obj);
  void VisitIUnitEvent(UnitAttackEventAsIUnitEvent obj);
  void VisitIUnitEvent(UnitCounteringEventAsIUnitEvent obj);
  void VisitIUnitEvent(UnitDefyingEventAsIUnitEvent obj);
  void VisitIUnitEvent(UnitStepEventAsIUnitEvent obj);
  void VisitIUnitEvent(WaitForUnitEventAsIUnitEvent obj);
}

}
