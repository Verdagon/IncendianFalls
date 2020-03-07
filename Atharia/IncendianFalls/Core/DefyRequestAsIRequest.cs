using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DefyRequestAsIRequest : IRequest {
  public readonly DefyRequest obj;
  public DefyRequestAsIRequest(DefyRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void Visit(IRequestVisitor visitor) { visitor.Visit(this); }
}
public static class DefyRequestAsIRequestCaster {
  public static DefyRequestAsIRequest AsIRequest(this DefyRequest obj) {
    return new DefyRequestAsIRequest(obj);
  }
}

}
