using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class RevertedEventAsIGameEvent : IGameEvent {
  public readonly RevertedEvent obj;
  public RevertedEventAsIGameEvent(RevertedEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIGameEvent(IGameEventVisitor visitor) { visitor.VisitIGameEvent(this); }
}
public static class RevertedEventAsIGameEventCaster {
  public static RevertedEventAsIGameEvent AsIGameEvent(this RevertedEvent obj) {
    return new RevertedEventAsIGameEvent(obj);
  }
}

}
