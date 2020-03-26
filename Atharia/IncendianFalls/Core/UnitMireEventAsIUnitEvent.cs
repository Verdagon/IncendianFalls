using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnitMireEventAsIUnitEvent : IUnitEvent {
  public readonly UnitMireEvent obj;
  public UnitMireEventAsIUnitEvent(UnitMireEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
         public int GetTime() { return UnitMireEventExtensions.GetTime(obj); }

  public void VisitIUnitEvent(IUnitEventVisitor visitor) { visitor.VisitIUnitEvent(this); }
}
public static class UnitMireEventAsIUnitEventCaster {
  public static UnitMireEventAsIUnitEvent AsIUnitEvent(this UnitMireEvent obj) {
    return new UnitMireEventAsIUnitEvent(obj);
  }
}

}
