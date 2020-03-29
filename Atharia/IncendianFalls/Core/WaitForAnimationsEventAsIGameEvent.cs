using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class WaitForAnimationsEventAsIGameEvent : IGameEvent {
  public readonly WaitForAnimationsEvent obj;
  public WaitForAnimationsEventAsIGameEvent(WaitForAnimationsEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIGameEvent(IGameEventVisitor visitor) { visitor.VisitIGameEvent(this); }
}
public static class WaitForAnimationsEventAsIGameEventCaster {
  public static WaitForAnimationsEventAsIGameEvent AsIGameEvent(this WaitForAnimationsEvent obj) {
    return new WaitForAnimationsEventAsIGameEvent(obj);
  }
}

}
