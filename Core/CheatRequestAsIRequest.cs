using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class CheatRequestAsIRequest : IRequest {
  public readonly CheatRequest obj;
  public CheatRequestAsIRequest(CheatRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void Visit(IRequestVisitor visitor) { visitor.Visit(this); }
}
public static class CheatRequestAsIRequestCaster {
  public static CheatRequestAsIRequest AsIRequest(this CheatRequest obj) {
    return new CheatRequestAsIRequest(obj);
  }
}

}
