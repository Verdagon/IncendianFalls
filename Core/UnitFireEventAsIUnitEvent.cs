using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnitFireEventAsIUnitEvent : IUnitEvent {
  public readonly UnitFireEvent obj;
  public UnitFireEventAsIUnitEvent(UnitFireEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
         public int GetTime() { return UnitFireEventExtensions.GetTime(obj); }

  public void Visit(IUnitEventVisitor visitor) { visitor.Visit(this); }
}
public static class UnitFireEventAsIUnitEventCaster {
  public static UnitFireEventAsIUnitEvent AsIUnitEvent(this UnitFireEvent obj) {
    return new UnitFireEventAsIUnitEvent(obj);
  }
}

}