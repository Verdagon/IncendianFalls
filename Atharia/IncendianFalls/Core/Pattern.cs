using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class Pattern : IComparable<Pattern> {
  public static readonly string NAME = "Pattern";
  public class EqualityComparer : IEqualityComparer<Pattern> {
    public bool Equals(Pattern a, Pattern b) {
      return a.Equals(b);
    }
    public int GetHashCode(Pattern a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<Pattern> {
    public int Compare(Pattern a, Pattern b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly string name;
  public readonly Vec2ImmListImmList cornersByShapeIndex;
  public readonly PatternTileImmList patternTiles;
  public readonly Vec2 xOffset;
  public readonly Vec2 yOffset;
  public Pattern(
      string name,
      Vec2ImmListImmList cornersByShapeIndex,
      PatternTileImmList patternTiles,
      Vec2 xOffset,
      Vec2 yOffset) {
    this.name = name;
    this.cornersByShapeIndex = cornersByShapeIndex;
    this.patternTiles = patternTiles;
    this.xOffset = xOffset;
    this.yOffset = yOffset;
    int hash = 0;
    hash = hash * 37 + name.GetDeterministicHashCode();
    hash = hash * 37 + cornersByShapeIndex.GetDeterministicHashCode();
    hash = hash * 37 + patternTiles.GetDeterministicHashCode();
    hash = hash * 37 + xOffset.GetDeterministicHashCode();
    hash = hash * 37 + yOffset.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(Pattern a, Pattern b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(Pattern a, Pattern b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is Pattern)) {
      return false;
    }
    var that = obj as Pattern;
    return true
               && name.Equals(that.name)
        && cornersByShapeIndex.Equals(that.cornersByShapeIndex)
        && patternTiles.Equals(that.patternTiles)
        && xOffset.Equals(that.xOffset)
        && yOffset.Equals(that.yOffset)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(Pattern that) {
    if (name != that.name) {
      return name.CompareTo(that.name);
    }
    if (cornersByShapeIndex != that.cornersByShapeIndex) {
      return cornersByShapeIndex.CompareTo(that.cornersByShapeIndex);
    }
    if (patternTiles != that.patternTiles) {
      return patternTiles.CompareTo(that.patternTiles);
    }
    if (xOffset != that.xOffset) {
      return xOffset.CompareTo(that.xOffset);
    }
    if (yOffset != that.yOffset) {
      return yOffset.CompareTo(that.yOffset);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "Pattern(" +
        name.DStr() + ", " +
        cornersByShapeIndex.DStr() + ", " +
        patternTiles.DStr() + ", " +
        xOffset.DStr() + ", " +
        yOffset.DStr()
        + ")";

    }
    public static Pattern Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var name = source.ParseStr();
      source.Expect(",");
      var cornersByShapeIndex = Vec2ImmListImmList.Parse(source);
      source.Expect(",");
      var patternTiles = PatternTileImmList.Parse(source);
      source.Expect(",");
      var xOffset = Vec2.Parse(source);
      source.Expect(",");
      var yOffset = Vec2.Parse(source);
      source.Expect(")");
      return new Pattern(name, cornersByShapeIndex, patternTiles, xOffset, yOffset);
  }
}
       
}
