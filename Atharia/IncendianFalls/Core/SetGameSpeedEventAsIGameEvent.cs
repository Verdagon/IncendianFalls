using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class SetGameSpeedEventAsIGameEvent : IGameEvent {
  public readonly SetGameSpeedEvent obj;
  public SetGameSpeedEventAsIGameEvent(SetGameSpeedEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIGameEvent(IGameEventVisitor visitor) { visitor.VisitIGameEvent(this); }
}
public static class SetGameSpeedEventAsIGameEventCaster {
  public static SetGameSpeedEventAsIGameEvent AsIGameEvent(this SetGameSpeedEvent obj) {
    return new SetGameSpeedEventAsIGameEvent(obj);
  }
}

}
