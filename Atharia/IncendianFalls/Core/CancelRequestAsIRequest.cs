using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class CancelRequestAsIRequest : IRequest {
  public readonly CancelRequest obj;
  public CancelRequestAsIRequest(CancelRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void Visit(IRequestVisitor visitor) { visitor.Visit(this); }
}
public static class CancelRequestAsIRequestCaster {
  public static CancelRequestAsIRequest AsIRequest(this CancelRequest obj) {
    return new CancelRequestAsIRequest(obj);
  }
}

}
