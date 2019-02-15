using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class NullIUnitEvent : IUnitEvent {
  public static NullIUnitEvent Null = new NullIUnitEvent();
  public string DStr() { return "null"; }
  public void Visit(IUnitEventVisitor visitor) { throw new Exception("Called method on a null!"); }
  public int GetTime(){ throw new Exception("Called method on a null!"); }
}

}
