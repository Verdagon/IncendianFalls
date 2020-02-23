using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnitShieldingEventAsIUnitEvent : IUnitEvent {
  public readonly UnitShieldingEvent obj;
  public UnitShieldingEventAsIUnitEvent(UnitShieldingEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
         public int GetTime() { return UnitShieldingEventExtensions.GetTime(obj); }

  public void Visit(IUnitEventVisitor visitor) { visitor.Visit(this); }
}
public static class UnitShieldingEventAsIUnitEventCaster {
  public static UnitShieldingEventAsIUnitEvent AsIUnitEvent(this UnitShieldingEvent obj) {
    return new UnitShieldingEventAsIUnitEvent(obj);
  }
}

}
