using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class BlazeRequestAsIRequest : IRequest {
  public readonly BlazeRequest obj;
  public BlazeRequestAsIRequest(BlazeRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIRequest(IRequestVisitor visitor) { visitor.VisitIRequest(this); }
}
public static class BlazeRequestAsIRequestCaster {
  public static BlazeRequestAsIRequest AsIRequest(this BlazeRequest obj) {
    return new BlazeRequestAsIRequest(obj);
  }
}

}
