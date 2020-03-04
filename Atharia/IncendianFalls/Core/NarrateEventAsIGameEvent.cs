using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NarrateEventAsIGameEvent : IGameEvent {
  public readonly NarrateEvent obj;
  public NarrateEventAsIGameEvent(NarrateEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void Visit(IGameEventVisitor visitor) { visitor.Visit(this); }
}
public static class NarrateEventAsIGameEventCaster {
  public static NarrateEventAsIGameEvent AsIGameEvent(this NarrateEvent obj) {
    return new NarrateEventAsIGameEvent(obj);
  }
}

}
