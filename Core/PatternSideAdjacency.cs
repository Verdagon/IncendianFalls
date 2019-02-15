using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class PatternSideAdjacency : IComparable<PatternSideAdjacency> {
  public static readonly string NAME = "PatternSideAdjacency";
  public class EqualityComparer : IEqualityComparer<PatternSideAdjacency> {
    public bool Equals(PatternSideAdjacency a, PatternSideAdjacency b) {
      return a.Equals(b);
    }
    public int GetHashCode(PatternSideAdjacency a) {
      return a.GetHashCode();
    }
  }
  public class Comparer : IComparer<PatternSideAdjacency> {
    public int Compare(PatternSideAdjacency a, PatternSideAdjacency b) {
      return a.CompareTo(b);
    }
  }
       public readonly int groupRelativeX;
  public readonly int groupRelativeY;
  public readonly int tileIndex;
  public readonly int sideIndex;
  public PatternSideAdjacency(
      int groupRelativeX,
      int groupRelativeY,
      int tileIndex,
      int sideIndex) {
    this.groupRelativeX = groupRelativeX;
    this.groupRelativeY = groupRelativeY;
    this.tileIndex = tileIndex;
    this.sideIndex = sideIndex;

  }
  public static bool operator==(PatternSideAdjacency a, PatternSideAdjacency b) {
    return a.Equals(b);
  }
  public static bool operator!=(PatternSideAdjacency a, PatternSideAdjacency b) {
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is PatternSideAdjacency)) {
      return false;
    }
    var that = obj as PatternSideAdjacency;
    return true
             && groupRelativeX.Equals(that.groupRelativeX)
        && groupRelativeY.Equals(that.groupRelativeY)
        && tileIndex.Equals(that.tileIndex)
        && sideIndex.Equals(that.sideIndex)
        ;
  }
  public override int GetHashCode() {
    int hash = 0;
    hash = hash * 1337;
    hash = hash + groupRelativeX.GetHashCode();
    hash = hash * 1337;
    hash = hash + groupRelativeY.GetHashCode();
    hash = hash * 1337;
    hash = hash + tileIndex.GetHashCode();
    hash = hash * 1337;
    hash = hash + sideIndex.GetHashCode();
    return hash;
  }
  public int CompareTo(PatternSideAdjacency that) {
    if (groupRelativeX != that.groupRelativeX) {
      return groupRelativeX.CompareTo(that.groupRelativeX);
    }
    if (groupRelativeY != that.groupRelativeY) {
      return groupRelativeY.CompareTo(that.groupRelativeY);
    }
    if (tileIndex != that.tileIndex) {
      return tileIndex.CompareTo(that.tileIndex);
    }
    if (sideIndex != that.sideIndex) {
      return sideIndex.CompareTo(that.sideIndex);
    }
    return 0;
  }
  public string DStr() {
    return "PatternSideAdjacency(" +
        groupRelativeX + ", " +
        groupRelativeY + ", " +
        tileIndex + ", " +
        sideIndex
        + ")";

    }
    public static PatternSideAdjacency Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var groupRelativeX = source.ParseInt();
      source.Expect(",");
      var groupRelativeY = source.ParseInt();
      source.Expect(",");
      var tileIndex = source.ParseInt();
      source.Expect(",");
      var sideIndex = source.ParseInt();
      source.Expect(")");
      return new PatternSideAdjacency(groupRelativeX, groupRelativeY, tileIndex, sideIndex);
  }
}
     
}
