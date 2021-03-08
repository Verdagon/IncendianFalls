using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class CounterRequestAsIRequest : IRequest {
  public readonly CounterRequest obj;
  public CounterRequestAsIRequest(CounterRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIRequest(IRequestVisitor visitor) { visitor.VisitIRequest(this); }
}
public static class CounterRequestAsIRequestCaster {
  public static CounterRequestAsIRequest AsIRequest(this CounterRequest obj) {
    return new CounterRequestAsIRequest(obj);
  }
}

}
