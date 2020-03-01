using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class OverlayActionRequestAsIRequest : IRequest {
  public readonly OverlayActionRequest obj;
  public OverlayActionRequestAsIRequest(OverlayActionRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void Visit(IRequestVisitor visitor) { visitor.Visit(this); }
}
public static class OverlayActionRequestAsIRequestCaster {
  public static OverlayActionRequestAsIRequest AsIRequest(this OverlayActionRequest obj) {
    return new OverlayActionRequestAsIRequest(obj);
  }
}

}
