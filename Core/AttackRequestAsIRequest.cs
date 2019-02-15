using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class AttackRequestAsIRequest : IRequest {
  public readonly AttackRequest obj;
  public AttackRequestAsIRequest(AttackRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
     
  public void Visit(IRequestVisitor visitor) { visitor.Visit(this); }
}
public static class AttackRequestAsIRequestCaster {
  public static AttackRequestAsIRequest AsIRequest(this AttackRequest obj) {
    return new AttackRequestAsIRequest(obj);
  }
}

}
