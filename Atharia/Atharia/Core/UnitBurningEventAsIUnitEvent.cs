using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnitBurningEventAsIUnitEvent : IUnitEvent {
  public readonly UnitBurningEvent obj;
  public UnitBurningEventAsIUnitEvent(UnitBurningEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIUnitEvent(IUnitEventVisitor visitor) { visitor.VisitIUnitEvent(this); }
}
public static class UnitBurningEventAsIUnitEventCaster {
  public static UnitBurningEventAsIUnitEvent AsIUnitEvent(this UnitBurningEvent obj) {
    return new UnitBurningEventAsIUnitEvent(obj);
  }
}

}
