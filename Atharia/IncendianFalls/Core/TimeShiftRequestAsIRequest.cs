using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class TimeShiftRequestAsIRequest : IRequest {
  public readonly TimeShiftRequest obj;
  public TimeShiftRequestAsIRequest(TimeShiftRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIRequest(IRequestVisitor visitor) { visitor.VisitIRequest(this); }
}
public static class TimeShiftRequestAsIRequestCaster {
  public static TimeShiftRequestAsIRequest AsIRequest(this TimeShiftRequest obj) {
    return new TimeShiftRequestAsIRequest(obj);
  }
}

}
