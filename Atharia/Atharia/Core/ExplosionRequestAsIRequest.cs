using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ExplosionRequestAsIRequest : IRequest {
  public readonly ExplosionRequest obj;
  public ExplosionRequestAsIRequest(ExplosionRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIRequest(IRequestVisitor visitor) { visitor.VisitIRequest(this); }
}
public static class ExplosionRequestAsIRequestCaster {
  public static ExplosionRequestAsIRequest AsIRequest(this ExplosionRequest obj) {
    return new ExplosionRequestAsIRequest(obj);
  }
}

}
