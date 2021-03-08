using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnitFireBombedEventAsIUnitEvent : IUnitEvent {
  public readonly UnitFireBombedEvent obj;
  public UnitFireBombedEventAsIUnitEvent(UnitFireBombedEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIUnitEvent(IUnitEventVisitor visitor) { visitor.VisitIUnitEvent(this); }
}
public static class UnitFireBombedEventAsIUnitEventCaster {
  public static UnitFireBombedEventAsIUnitEvent AsIUnitEvent(this UnitFireBombedEvent obj) {
    return new UnitFireBombedEventAsIUnitEvent(obj);
  }
}

}
