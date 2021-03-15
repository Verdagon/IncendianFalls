using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnitExplosionEventAsIUnitEvent : IUnitEvent {
  public readonly UnitExplosionEvent obj;
  public UnitExplosionEventAsIUnitEvent(UnitExplosionEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIUnitEvent(IUnitEventVisitor visitor) { visitor.VisitIUnitEvent(this); }
}
public static class UnitExplosionEventAsIUnitEventCaster {
  public static UnitExplosionEventAsIUnitEvent AsIUnitEvent(this UnitExplosionEvent obj) {
    return new UnitExplosionEventAsIUnitEvent(obj);
  }
}

}
