using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class TriggerRequestAsIRequest : IRequest {
  public readonly TriggerRequest obj;
  public TriggerRequestAsIRequest(TriggerRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIRequest(IRequestVisitor visitor) { visitor.VisitIRequest(this); }
}
public static class TriggerRequestAsIRequestCaster {
  public static TriggerRequestAsIRequest AsIRequest(this TriggerRequest obj) {
    return new TriggerRequestAsIRequest(obj);
  }
}

}
