using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class SetupGameRequestAsIRequest : IRequest {
  public readonly SetupGameRequest obj;
  public SetupGameRequestAsIRequest(SetupGameRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
     
  public void Visit(IRequestVisitor visitor) { visitor.Visit(this); }
}
public static class SetupGameRequestAsIRequestCaster {
  public static SetupGameRequestAsIRequest AsIRequest(this SetupGameRequest obj) {
    return new SetupGameRequestAsIRequest(obj);
  }
}

}
