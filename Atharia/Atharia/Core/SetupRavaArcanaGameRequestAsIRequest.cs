using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class SetupRavaArcanaGameRequestAsIRequest : IRequest {
  public readonly SetupRavaArcanaGameRequest obj;
  public SetupRavaArcanaGameRequestAsIRequest(SetupRavaArcanaGameRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIRequest(IRequestVisitor visitor) { visitor.VisitIRequest(this); }
}
public static class SetupRavaArcanaGameRequestAsIRequestCaster {
  public static SetupRavaArcanaGameRequestAsIRequest AsIRequest(this SetupRavaArcanaGameRequest obj) {
    return new SetupRavaArcanaGameRequestAsIRequest(obj);
  }
}

}
