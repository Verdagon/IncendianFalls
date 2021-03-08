using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class WaitForEverythingEventAsIGameEvent : IGameEvent {
  public readonly WaitForEverythingEvent obj;
  public WaitForEverythingEventAsIGameEvent(WaitForEverythingEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIGameEvent(IGameEventVisitor visitor) { visitor.VisitIGameEvent(this); }
}
public static class WaitForEverythingEventAsIGameEventCaster {
  public static WaitForEverythingEventAsIGameEvent AsIGameEvent(this WaitForEverythingEvent obj) {
    return new WaitForEverythingEventAsIGameEvent(obj);
  }
}

}
