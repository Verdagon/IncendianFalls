using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class SetupGauntletGameRequestAsIRequest : IRequest {
  public readonly SetupGauntletGameRequest obj;
  public SetupGauntletGameRequestAsIRequest(SetupGauntletGameRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIRequest(IRequestVisitor visitor) { visitor.VisitIRequest(this); }
}
public static class SetupGauntletGameRequestAsIRequestCaster {
  public static SetupGauntletGameRequestAsIRequest AsIRequest(this SetupGauntletGameRequest obj) {
    return new SetupGauntletGameRequestAsIRequest(obj);
  }
}

}
