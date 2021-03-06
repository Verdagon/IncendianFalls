using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnitCounteringEventAsIUnitEvent : IUnitEvent {
  public readonly UnitCounteringEvent obj;
  public UnitCounteringEventAsIUnitEvent(UnitCounteringEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIUnitEvent(IUnitEventVisitor visitor) { visitor.VisitIUnitEvent(this); }
}
public static class UnitCounteringEventAsIUnitEventCaster {
  public static UnitCounteringEventAsIUnitEvent AsIUnitEvent(this UnitCounteringEvent obj) {
    return new UnitCounteringEventAsIUnitEvent(obj);
  }
}

}
