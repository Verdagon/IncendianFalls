using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class SetupEmberDeepGameRequestAsIRequest : IRequest {
  public readonly SetupEmberDeepGameRequest obj;
  public SetupEmberDeepGameRequestAsIRequest(SetupEmberDeepGameRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void Visit(IRequestVisitor visitor) { visitor.Visit(this); }
}
public static class SetupEmberDeepGameRequestAsIRequestCaster {
  public static SetupEmberDeepGameRequestAsIRequest AsIRequest(this SetupEmberDeepGameRequest obj) {
    return new SetupEmberDeepGameRequestAsIRequest(obj);
  }
}

}
