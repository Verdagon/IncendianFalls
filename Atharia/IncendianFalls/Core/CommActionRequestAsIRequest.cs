using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class CommActionRequestAsIRequest : IRequest {
  public readonly CommActionRequest obj;
  public CommActionRequestAsIRequest(CommActionRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIRequest(IRequestVisitor visitor) { visitor.VisitIRequest(this); }
}
public static class CommActionRequestAsIRequestCaster {
  public static CommActionRequestAsIRequest AsIRequest(this CommActionRequest obj) {
    return new CommActionRequestAsIRequest(obj);
  }
}

}
