using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class WaitForUnitEventAsIUnitEvent : IUnitEvent {
  public readonly WaitForUnitEvent obj;
  public WaitForUnitEventAsIUnitEvent(WaitForUnitEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIUnitEvent(IUnitEventVisitor visitor) { visitor.VisitIUnitEvent(this); }
}
public static class WaitForUnitEventAsIUnitEventCaster {
  public static WaitForUnitEventAsIUnitEvent AsIUnitEvent(this WaitForUnitEvent obj) {
    return new WaitForUnitEventAsIUnitEvent(obj);
  }
}

}
