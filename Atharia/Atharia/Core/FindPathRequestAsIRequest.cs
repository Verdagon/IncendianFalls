using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class FindPathRequestAsIRequest : IRequest {
  public readonly FindPathRequest obj;
  public FindPathRequestAsIRequest(FindPathRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIRequest(IRequestVisitor visitor) { visitor.VisitIRequest(this); }
}
public static class FindPathRequestAsIRequestCaster {
  public static FindPathRequestAsIRequest AsIRequest(this FindPathRequest obj) {
    return new FindPathRequestAsIRequest(obj);
  }
}

}
