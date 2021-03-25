using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {
public class Location : IComparable<Location> {
  public static readonly string NAME = "Location";
  public class EqualityComparer : IEqualityComparer<Location> {
    public bool Equals(Location a, Location b) {
      return a.Equals(b);
    }
    public int GetHashCode(Location a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<Location> {
    public int Compare(Location a, Location b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int groupX;
  public readonly int groupY;
  public readonly int indexInGroup;
  public Location(
      int groupX,
      int groupY,
      int indexInGroup) {
    this.groupX = groupX;
    this.groupY = groupY;
    this.indexInGroup = indexInGroup;
    int hash = 0;
    hash = hash * 37 + groupX;
    hash = hash * 37 + groupY;
    hash = hash * 37 + indexInGroup;
    this.hashCode = hash;

  }
  public static bool operator==(Location a, Location b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(Location a, Location b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is Location)) {
      return false;
    }
    var that = obj as Location;
    return true
               && groupX.Equals(that.groupX)
        && groupY.Equals(that.groupY)
        && indexInGroup.Equals(that.indexInGroup)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(Location that) {
    if (groupX != that.groupX) {
      return groupX.CompareTo(that.groupX);
    }
    if (groupY != that.groupY) {
      return groupY.CompareTo(that.groupY);
    }
    if (indexInGroup != that.indexInGroup) {
      return indexInGroup.CompareTo(that.indexInGroup);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "Location(" +
        groupX + ", " +
        groupY + ", " +
        indexInGroup
        + ")";

    }
    public static Location Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var groupX = source.ParseInt();
      source.Expect(",");
      var groupY = source.ParseInt();
      source.Expect(",");
      var indexInGroup = source.ParseInt();
      source.Expect(")");
      return new Location(groupX, groupY, indexInGroup);
  }
}
       
}
