using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class MoveRequestAsIRequest : IRequest {
  public readonly MoveRequest obj;
  public MoveRequestAsIRequest(MoveRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitIRequest(IRequestVisitor visitor) { visitor.VisitIRequest(this); }
}
public static class MoveRequestAsIRequestCaster {
  public static MoveRequestAsIRequest AsIRequest(this MoveRequest obj) {
    return new MoveRequestAsIRequest(obj);
  }
}

}
