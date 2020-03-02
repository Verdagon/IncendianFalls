using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class FlyCameraEventAsIGameEvent : IGameEvent {
  public readonly FlyCameraEvent obj;
  public FlyCameraEventAsIGameEvent(FlyCameraEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void Visit(IGameEventVisitor visitor) { visitor.Visit(this); }
}
public static class FlyCameraEventAsIGameEventCaster {
  public static FlyCameraEventAsIGameEvent AsIGameEvent(this FlyCameraEvent obj) {
    return new FlyCameraEventAsIGameEvent(obj);
  }
}

}
