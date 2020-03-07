using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class FireBombRequestAsIRequest : IRequest {
  public readonly FireBombRequest obj;
  public FireBombRequestAsIRequest(FireBombRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void Visit(IRequestVisitor visitor) { visitor.Visit(this); }
}
public static class FireBombRequestAsIRequestCaster {
  public static FireBombRequestAsIRequest AsIRequest(this FireBombRequest obj) {
    return new FireBombRequestAsIRequest(obj);
  }
}

}
