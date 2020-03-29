using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {
public class ElevatedLocation : IComparable<ElevatedLocation> {
  public static readonly string NAME = "ElevatedLocation";
  public class EqualityComparer : IEqualityComparer<ElevatedLocation> {
    public bool Equals(ElevatedLocation a, ElevatedLocation b) {
      return a.Equals(b);
    }
    public int GetHashCode(ElevatedLocation a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<ElevatedLocation> {
    public int Compare(ElevatedLocation a, ElevatedLocation b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly Location location;
  public readonly int elevation;
  public ElevatedLocation(
      Location location,
      int elevation) {
    this.location = location;
    this.elevation = elevation;
    int hash = 0;
    hash = hash * 37 + location.GetDeterministicHashCode();
    hash = hash * 37 + elevation.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(ElevatedLocation a, ElevatedLocation b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(ElevatedLocation a, ElevatedLocation b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is ElevatedLocation)) {
      return false;
    }
    var that = obj as ElevatedLocation;
    return true
               && location.Equals(that.location)
        && elevation.Equals(that.elevation)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(ElevatedLocation that) {
    if (location != that.location) {
      return location.CompareTo(that.location);
    }
    if (elevation != that.elevation) {
      return elevation.CompareTo(that.elevation);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "ElevatedLocation(" +
        location.DStr() + ", " +
        elevation.DStr()
        + ")";

    }
    public static ElevatedLocation Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var location = Location.Parse(source);
      source.Expect(",");
      var elevation = source.ParseInt();
      source.Expect(")");
      return new ElevatedLocation(location, elevation);
  }
}
       
}
