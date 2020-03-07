using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class MireRequestAsIRequest : IRequest {
  public readonly MireRequest obj;
  public MireRequestAsIRequest(MireRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void Visit(IRequestVisitor visitor) { visitor.Visit(this); }
}
public static class MireRequestAsIRequestCaster {
  public static MireRequestAsIRequest AsIRequest(this MireRequest obj) {
    return new MireRequestAsIRequest(obj);
  }
}

}
