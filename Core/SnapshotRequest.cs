using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SnapshotRequest : IComparable<SnapshotRequest> {
  public static readonly string NAME = "SnapshotRequest";
  public class EqualityComparer : IEqualityComparer<SnapshotRequest> {
    public bool Equals(SnapshotRequest a, SnapshotRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(SnapshotRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<SnapshotRequest> {
    public int Compare(SnapshotRequest a, SnapshotRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public SnapshotRequest(
) {
    int hash = 0;
    this.hashCode = hash;

  }
  public static bool operator==(SnapshotRequest a, SnapshotRequest b) {
    return a.Equals(b);
  }
  public static bool operator!=(SnapshotRequest a, SnapshotRequest b) {
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is SnapshotRequest)) {
      return false;
    }
    var that = obj as SnapshotRequest;
    return true
               ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(SnapshotRequest that) {
    return 0;
  }
  public string DStr() {
    return "SnapshotRequest()";
    }
    public static SnapshotRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      source.Expect(")");
      return new SnapshotRequest();
  }
}
       
}
