using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ShowOverlayEventAsIGameEvent : IGameEvent {
  public readonly ShowOverlayEvent obj;
  public ShowOverlayEventAsIGameEvent(ShowOverlayEvent obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIGameEvent(IGameEventVisitor visitor) { visitor.VisitIGameEvent(this); }
}
public static class ShowOverlayEventAsIGameEventCaster {
  public static ShowOverlayEventAsIGameEvent AsIGameEvent(this ShowOverlayEvent obj) {
    return new ShowOverlayEventAsIGameEvent(obj);
  }
}

}
