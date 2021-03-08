using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class WaitForCameraEventAsIGameEvent : IGameEvent {
  public readonly WaitForCameraEvent obj;
  public WaitForCameraEventAsIGameEvent(WaitForCameraEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIGameEvent(IGameEventVisitor visitor) { visitor.VisitIGameEvent(this); }
}
public static class WaitForCameraEventAsIGameEventCaster {
  public static WaitForCameraEventAsIGameEvent AsIGameEvent(this WaitForCameraEvent obj) {
    return new WaitForCameraEventAsIGameEvent(obj);
  }
}

}
