using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullIRequest : IRequest {
  public static NullIRequest Null = new NullIRequest();
  public string DStr() { return "null"; }
  public int GetDeterministicHashCode() { return 0; }
  public void VisitIRequest(IRequestVisitor visitor) { throw new Exception("Called method on a null!"); }
}

}
