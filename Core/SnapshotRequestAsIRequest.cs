using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class SnapshotRequestAsIRequest : IRequest {
  public readonly SnapshotRequest obj;
  public SnapshotRequestAsIRequest(SnapshotRequest obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
     
  public void Visit(IRequestVisitor visitor) { visitor.Visit(this); }
}
public static class SnapshotRequestAsIRequestCaster {
  public static SnapshotRequestAsIRequest AsIRequest(this SnapshotRequest obj) {
    return new SnapshotRequestAsIRequest(obj);
  }
}

}
