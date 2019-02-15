using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class TimeShiftRequestAsIRequest : IRequest {
  public readonly TimeShiftRequest obj;
  public TimeShiftRequestAsIRequest(TimeShiftRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
     
  public void Visit(IRequestVisitor visitor) { visitor.Visit(this); }
}
public static class TimeShiftRequestAsIRequestCaster {
  public static TimeShiftRequestAsIRequest AsIRequest(this TimeShiftRequest obj) {
    return new TimeShiftRequestAsIRequest(obj);
  }
}

}
