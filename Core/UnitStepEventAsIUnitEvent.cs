using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class UnitStepEventAsIUnitEvent : IUnitEvent {
  public readonly UnitStepEvent obj;
  public UnitStepEventAsIUnitEvent(UnitStepEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
       public int GetTime() { return IncendianFalls.UnitStepEventExtensions.GetTime(obj); }

  public void Visit(IUnitEventVisitor visitor) { visitor.Visit(this); }
}
public static class UnitStepEventAsIUnitEventCaster {
  public static UnitStepEventAsIUnitEvent AsIUnitEvent(this UnitStepEvent obj) {
    return new UnitStepEventAsIUnitEvent(obj);
  }
}

}
