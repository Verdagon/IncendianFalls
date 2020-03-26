using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullIUnitEvent : IUnitEvent {
  public static NullIUnitEvent Null = new NullIUnitEvent();
  public string DStr() { return "null"; }
  public int GetDeterministicHashCode() { return 0; }
  public void VisitIUnitEvent(IUnitEventVisitor visitor) { throw new Exception("Called method on a null!"); }
  public int GetTime(){ throw new Exception("Called method on a null!"); }
}

}
