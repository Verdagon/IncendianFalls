using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class WaitEventAsIGameEvent : IGameEvent {
  public readonly WaitEvent obj;
  public WaitEventAsIGameEvent(WaitEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIGameEvent(IGameEventVisitor visitor) { visitor.VisitIGameEvent(this); }
}
public static class WaitEventAsIGameEventCaster {
  public static WaitEventAsIGameEvent AsIGameEvent(this WaitEvent obj) {
    return new WaitEventAsIGameEvent(obj);
  }
}

}
