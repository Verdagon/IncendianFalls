using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class FollowDirectiveRequestAsIRequest : IRequest {
  public readonly FollowDirectiveRequest obj;
  public FollowDirectiveRequestAsIRequest(FollowDirectiveRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
     
  public void Visit(IRequestVisitor visitor) { visitor.Visit(this); }
}
public static class FollowDirectiveRequestAsIRequestCaster {
  public static FollowDirectiveRequestAsIRequest AsIRequest(this FollowDirectiveRequest obj) {
    return new FollowDirectiveRequestAsIRequest(obj);
  }
}

}
