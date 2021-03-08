using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SetupTerrainRequest : IComparable<SetupTerrainRequest> {
  public static readonly string NAME = "SetupTerrainRequest";
  public class EqualityComparer : IEqualityComparer<SetupTerrainRequest> {
    public bool Equals(SetupTerrainRequest a, SetupTerrainRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(SetupTerrainRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<SetupTerrainRequest> {
    public int Compare(SetupTerrainRequest a, SetupTerrainRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly Pattern pattern;
  public SetupTerrainRequest(
      Pattern pattern) {
    this.pattern = pattern;
    int hash = 0;
    hash = hash * 37 + pattern.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(SetupTerrainRequest a, SetupTerrainRequest b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(SetupTerrainRequest a, SetupTerrainRequest b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is SetupTerrainRequest)) {
      return false;
    }
    var that = obj as SetupTerrainRequest;
    return true
               && pattern.Equals(that.pattern)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(SetupTerrainRequest that) {
    if (pattern != that.pattern) {
      return pattern.CompareTo(that.pattern);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "SetupTerrainRequest(" +
        pattern.DStr()
        + ")";

    }
    public static SetupTerrainRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var pattern = Pattern.Parse(source);
      source.Expect(")");
      return new SetupTerrainRequest(pattern);
  }
}
       
}
