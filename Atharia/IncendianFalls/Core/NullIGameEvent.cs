using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullIGameEvent : IGameEvent {
  public static NullIGameEvent Null = new NullIGameEvent();
  public string DStr() { return "null"; }
  public int GetDeterministicHashCode() { return 0; }
  public void Visit(IGameEventVisitor visitor) { throw new Exception("Called method on a null!"); }
}

}
