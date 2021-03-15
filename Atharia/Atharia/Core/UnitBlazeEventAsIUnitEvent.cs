using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnitBlazeEventAsIUnitEvent : IUnitEvent {
  public readonly UnitBlazeEvent obj;
  public UnitBlazeEventAsIUnitEvent(UnitBlazeEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIUnitEvent(IUnitEventVisitor visitor) { visitor.VisitIUnitEvent(this); }
}
public static class UnitBlazeEventAsIUnitEventCaster {
  public static UnitBlazeEventAsIUnitEvent AsIUnitEvent(this UnitBlazeEvent obj) {
    return new UnitBlazeEventAsIUnitEvent(obj);
  }
}

}
