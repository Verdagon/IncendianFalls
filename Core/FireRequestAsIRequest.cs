using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class FireRequestAsIRequest : IRequest {
  public readonly FireRequest obj;
  public FireRequestAsIRequest(FireRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void Visit(IRequestVisitor visitor) { visitor.Visit(this); }
}
public static class FireRequestAsIRequestCaster {
  public static FireRequestAsIRequest AsIRequest(this FireRequest obj) {
    return new FireRequestAsIRequest(obj);
  }
}

}
