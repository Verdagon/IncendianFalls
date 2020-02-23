using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class PatternCornerAdjacency : IComparable<PatternCornerAdjacency> {
  public static readonly string NAME = "PatternCornerAdjacency";
  public class EqualityComparer : IEqualityComparer<PatternCornerAdjacency> {
    public bool Equals(PatternCornerAdjacency a, PatternCornerAdjacency b) {
      return a.Equals(b);
    }
    public int GetHashCode(PatternCornerAdjacency a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<PatternCornerAdjacency> {
    public int Compare(PatternCornerAdjacency a, PatternCornerAdjacency b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int groupRelativeX;
  public readonly int groupRelativeY;
  public readonly int tileIndex;
  public readonly int cornerIndex;
  public PatternCornerAdjacency(
      int groupRelativeX,
      int groupRelativeY,
      int tileIndex,
      int cornerIndex) {
    this.groupRelativeX = groupRelativeX;
    this.groupRelativeY = groupRelativeY;
    this.tileIndex = tileIndex;
    this.cornerIndex = cornerIndex;
    int hash = 0;
    hash = hash * 37 + groupRelativeX.GetDeterministicHashCode();
    hash = hash * 37 + groupRelativeY.GetDeterministicHashCode();
    hash = hash * 37 + tileIndex.GetDeterministicHashCode();
    hash = hash * 37 + cornerIndex.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(PatternCornerAdjacency a, PatternCornerAdjacency b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(PatternCornerAdjacency a, PatternCornerAdjacency b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is PatternCornerAdjacency)) {
      return false;
    }
    var that = obj as PatternCornerAdjacency;
    return true
               && groupRelativeX.Equals(that.groupRelativeX)
        && groupRelativeY.Equals(that.groupRelativeY)
        && tileIndex.Equals(that.tileIndex)
        && cornerIndex.Equals(that.cornerIndex)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(PatternCornerAdjacency that) {
    if (groupRelativeX != that.groupRelativeX) {
      return groupRelativeX.CompareTo(that.groupRelativeX);
    }
    if (groupRelativeY != that.groupRelativeY) {
      return groupRelativeY.CompareTo(that.groupRelativeY);
    }
    if (tileIndex != that.tileIndex) {
      return tileIndex.CompareTo(that.tileIndex);
    }
    if (cornerIndex != that.cornerIndex) {
      return cornerIndex.CompareTo(that.cornerIndex);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "PatternCornerAdjacency(" +
        groupRelativeX.DStr() + ", " +
        groupRelativeY.DStr() + ", " +
        tileIndex.DStr() + ", " +
        cornerIndex.DStr()
        + ")";

    }
    public static PatternCornerAdjacency Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var groupRelativeX = source.ParseInt();
      source.Expect(",");
      var groupRelativeY = source.ParseInt();
      source.Expect(",");
      var tileIndex = source.ParseInt();
      source.Expect(",");
      var cornerIndex = source.ParseInt();
      source.Expect(")");
      return new PatternCornerAdjacency(groupRelativeX, groupRelativeY, tileIndex, cornerIndex);
  }
}
       
}
