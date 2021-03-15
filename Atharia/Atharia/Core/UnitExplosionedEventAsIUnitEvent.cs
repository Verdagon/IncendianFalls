using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnitExplosionedEventAsIUnitEvent : IUnitEvent {
  public readonly UnitExplosionedEvent obj;
  public UnitExplosionedEventAsIUnitEvent(UnitExplosionedEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIUnitEvent(IUnitEventVisitor visitor) { visitor.VisitIUnitEvent(this); }
}
public static class UnitExplosionedEventAsIUnitEventCaster {
  public static UnitExplosionedEventAsIUnitEvent AsIUnitEvent(this UnitExplosionedEvent obj) {
    return new UnitExplosionedEventAsIUnitEvent(obj);
  }
}

}
