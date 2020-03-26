using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnitUnleashBideEventAsIUnitEvent : IUnitEvent {
  public readonly UnitUnleashBideEvent obj;
  public UnitUnleashBideEventAsIUnitEvent(UnitUnleashBideEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
         public int GetTime() { return UnitUnleashBideEventExtensions.GetTime(obj); }

  public void VisitIUnitEvent(IUnitEventVisitor visitor) { visitor.VisitIUnitEvent(this); }
}
public static class UnitUnleashBideEventAsIUnitEventCaster {
  public static UnitUnleashBideEventAsIUnitEvent AsIUnitEvent(this UnitUnleashBideEvent obj) {
    return new UnitUnleashBideEventAsIUnitEvent(obj);
  }
}

}
