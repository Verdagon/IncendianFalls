using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnitDefyingEventAsIUnitEvent : IUnitEvent {
  public readonly UnitDefyingEvent obj;
  public UnitDefyingEventAsIUnitEvent(UnitDefyingEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
         public int GetTime() { return UnitDefyingEventExtensions.GetTime(obj); }

  public void Visit(IUnitEventVisitor visitor) { visitor.Visit(this); }
}
public static class UnitDefyingEventAsIUnitEventCaster {
  public static UnitDefyingEventAsIUnitEvent AsIUnitEvent(this UnitDefyingEvent obj) {
    return new UnitDefyingEventAsIUnitEvent(obj);
  }
}

}
