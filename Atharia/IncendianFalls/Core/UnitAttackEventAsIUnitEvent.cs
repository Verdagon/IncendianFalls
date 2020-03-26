using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnitAttackEventAsIUnitEvent : IUnitEvent {
  public readonly UnitAttackEvent obj;
  public UnitAttackEventAsIUnitEvent(UnitAttackEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
         public int GetTime() { return UnitAttackEventExtensions.GetTime(obj); }

  public void VisitIUnitEvent(IUnitEventVisitor visitor) { visitor.VisitIUnitEvent(this); }
}
public static class UnitAttackEventAsIUnitEventCaster {
  public static UnitAttackEventAsIUnitEvent AsIUnitEvent(this UnitAttackEvent obj) {
    return new UnitAttackEventAsIUnitEvent(obj);
  }
}

}
