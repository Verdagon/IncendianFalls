using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnitBlazedEventAsIUnitEvent : IUnitEvent {
  public readonly UnitBlazedEvent obj;
  public UnitBlazedEventAsIUnitEvent(UnitBlazedEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIUnitEvent(IUnitEventVisitor visitor) { visitor.VisitIUnitEvent(this); }
}
public static class UnitBlazedEventAsIUnitEventCaster {
  public static UnitBlazedEventAsIUnitEvent AsIUnitEvent(this UnitBlazedEvent obj) {
    return new UnitBlazedEventAsIUnitEvent(obj);
  }
}

}
