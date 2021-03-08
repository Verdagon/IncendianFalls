using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ResumeRequestAsIRequest : IRequest {
  public readonly ResumeRequest obj;
  public ResumeRequestAsIRequest(ResumeRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIRequest(IRequestVisitor visitor) { visitor.VisitIRequest(this); }
}
public static class ResumeRequestAsIRequestCaster {
  public static ResumeRequestAsIRequest AsIRequest(this ResumeRequest obj) {
    return new ResumeRequestAsIRequest(obj);
  }
}

}
