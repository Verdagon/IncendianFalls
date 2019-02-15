using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class InteractRequestAsIRequest : IRequest {
  public readonly InteractRequest obj;
  public InteractRequestAsIRequest(InteractRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
     
  public void Visit(IRequestVisitor visitor) { visitor.Visit(this); }
}
public static class InteractRequestAsIRequestCaster {
  public static InteractRequestAsIRequest AsIRequest(this InteractRequest obj) {
    return new InteractRequestAsIRequest(obj);
  }
}

}
