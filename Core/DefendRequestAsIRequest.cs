using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class DefendRequestAsIRequest : IRequest {
  public readonly DefendRequest obj;
  public DefendRequestAsIRequest(DefendRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
     
  public void Visit(IRequestVisitor visitor) { visitor.Visit(this); }
}
public static class DefendRequestAsIRequestCaster {
  public static DefendRequestAsIRequest AsIRequest(this DefendRequest obj) {
    return new DefendRequestAsIRequest(obj);
  }
}

}
