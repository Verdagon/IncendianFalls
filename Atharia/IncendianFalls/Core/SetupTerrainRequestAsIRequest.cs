using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class SetupTerrainRequestAsIRequest : IRequest {
  public readonly SetupTerrainRequest obj;
  public SetupTerrainRequestAsIRequest(SetupTerrainRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIRequest(IRequestVisitor visitor) { visitor.VisitIRequest(this); }
}
public static class SetupTerrainRequestAsIRequestCaster {
  public static SetupTerrainRequestAsIRequest AsIRequest(this SetupTerrainRequest obj) {
    return new SetupTerrainRequestAsIRequest(obj);
  }
}

}
