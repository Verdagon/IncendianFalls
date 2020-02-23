using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class TimeAnchorMoveRequestAsIRequest : IRequest {
  public readonly TimeAnchorMoveRequest obj;
  public TimeAnchorMoveRequestAsIRequest(TimeAnchorMoveRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void Visit(IRequestVisitor visitor) { visitor.Visit(this); }
}
public static class TimeAnchorMoveRequestAsIRequestCaster {
  public static TimeAnchorMoveRequestAsIRequest AsIRequest(this TimeAnchorMoveRequest obj) {
    return new TimeAnchorMoveRequestAsIRequest(obj);
  }
}

}
