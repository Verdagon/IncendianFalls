using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class PatternTile : IComparable<PatternTile> {
  public static readonly string NAME = "PatternTile";
  public class EqualityComparer : IEqualityComparer<PatternTile> {
    public bool Equals(PatternTile a, PatternTile b) {
      return a.Equals(b);
    }
    public int GetHashCode(PatternTile a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<PatternTile> {
    public int Compare(PatternTile a, PatternTile b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
       public readonly int shapeIndex;
  public readonly float rotateDegrees;
  public readonly Vec2 translate;
  public readonly PatternSideAdjacencyList sideAdjacenciesBySideIndex;
  public readonly PatternCornerAdjacencyListList cornerAdjacenciesByCornerIndex;
  public PatternTile(
      int shapeIndex,
      float rotateDegrees,
      Vec2 translate,
      PatternSideAdjacencyList sideAdjacenciesBySideIndex,
      PatternCornerAdjacencyListList cornerAdjacenciesByCornerIndex) {
    this.shapeIndex = shapeIndex;
    this.rotateDegrees = rotateDegrees;
    this.translate = translate;
    this.sideAdjacenciesBySideIndex = sideAdjacenciesBySideIndex;
    this.cornerAdjacenciesByCornerIndex = cornerAdjacenciesByCornerIndex;
    int hash = 0;
    hash = hash * 37 + shapeIndex.GetDeterministicHashCode();
    hash = hash * 37 + rotateDegrees.GetDeterministicHashCode();
    hash = hash * 37 + translate.GetDeterministicHashCode();
    hash = hash * 37 + sideAdjacenciesBySideIndex.GetDeterministicHashCode();
    hash = hash * 37 + cornerAdjacenciesByCornerIndex.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(PatternTile a, PatternTile b) {
    return a.Equals(b);
  }
  public static bool operator!=(PatternTile a, PatternTile b) {
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is PatternTile)) {
      return false;
    }
    var that = obj as PatternTile;
    return true
             && shapeIndex.Equals(that.shapeIndex)
        && rotateDegrees.Equals(that.rotateDegrees)
        && translate.Equals(that.translate)
        && sideAdjacenciesBySideIndex.Equals(that.sideAdjacenciesBySideIndex)
        && cornerAdjacenciesByCornerIndex.Equals(that.cornerAdjacenciesByCornerIndex)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(PatternTile that) {
    if (shapeIndex != that.shapeIndex) {
      return shapeIndex.CompareTo(that.shapeIndex);
    }
    if (rotateDegrees != that.rotateDegrees) {
      return rotateDegrees.CompareTo(that.rotateDegrees);
    }
    if (translate != that.translate) {
      return translate.CompareTo(that.translate);
    }
    if (sideAdjacenciesBySideIndex != that.sideAdjacenciesBySideIndex) {
      return sideAdjacenciesBySideIndex.CompareTo(that.sideAdjacenciesBySideIndex);
    }
    if (cornerAdjacenciesByCornerIndex != that.cornerAdjacenciesByCornerIndex) {
      return cornerAdjacenciesByCornerIndex.CompareTo(that.cornerAdjacenciesByCornerIndex);
    }
    return 0;
  }
  public string DStr() {
    return "PatternTile(" +
        shapeIndex.DStr() + ", " +
        rotateDegrees.DStr() + ", " +
        translate.DStr() + ", " +
        sideAdjacenciesBySideIndex.DStr() + ", " +
        cornerAdjacenciesByCornerIndex.DStr()
        + ")";

    }
    public static PatternTile Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var shapeIndex = source.ParseInt();
      source.Expect(",");
      var rotateDegrees = source.ParseFloat();
      source.Expect(",");
      var translate = Vec2.Parse(source);
      source.Expect(",");
      var sideAdjacenciesBySideIndex = PatternSideAdjacencyList.Parse(source);
      source.Expect(",");
      var cornerAdjacenciesByCornerIndex = PatternCornerAdjacencyListList.Parse(source);
      source.Expect(")");
      return new PatternTile(shapeIndex, rotateDegrees, translate, sideAdjacenciesBySideIndex, cornerAdjacenciesByCornerIndex);
  }
}
     
}
