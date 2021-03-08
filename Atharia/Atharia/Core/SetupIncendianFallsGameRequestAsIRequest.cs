using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class SetupIncendianFallsGameRequestAsIRequest : IRequest {
  public readonly SetupIncendianFallsGameRequest obj;
  public SetupIncendianFallsGameRequestAsIRequest(SetupIncendianFallsGameRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIRequest(IRequestVisitor visitor) { visitor.VisitIRequest(this); }
}
public static class SetupIncendianFallsGameRequestAsIRequestCaster {
  public static SetupIncendianFallsGameRequestAsIRequest AsIRequest(this SetupIncendianFallsGameRequest obj) {
    return new SetupIncendianFallsGameRequestAsIRequest(obj);
  }
}

}
