using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class Pattern : IComparable<Pattern> {
  public static readonly string NAME = "Pattern";
  public class EqualityComparer : IEqualityComparer<Pattern> {
    public bool Equals(Pattern a, Pattern b) {
      return a.Equals(b);
    }
    public int GetHashCode(Pattern a) {
      return a.GetHashCode();
    }
  }
  public class Comparer : IComparer<Pattern> {
    public int Compare(Pattern a, Pattern b) {
      return a.CompareTo(b);
    }
  }
       public readonly string name;
  public readonly Vec2ListList cornersByShapeIndex;
  public readonly PatternTileList patternTiles;
  public readonly Vec2 xOffset;
  public readonly Vec2 yOffset;
  public Pattern(
      string name,
      Vec2ListList cornersByShapeIndex,
      PatternTileList patternTiles,
      Vec2 xOffset,
      Vec2 yOffset) {
    this.name = name;
    this.cornersByShapeIndex = cornersByShapeIndex;
    this.patternTiles = patternTiles;
    this.xOffset = xOffset;
    this.yOffset = yOffset;

  }
  public static bool operator==(Pattern a, Pattern b) {
    return a.Equals(b);
  }
  public static bool operator!=(Pattern a, Pattern b) {
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
    int hash = 0;
    hash = hash * 1337;
    hash = hash + name.GetHashCode();
    hash = hash * 1337;
    hash = hash + cornersByShapeIndex.GetHashCode();
    hash = hash * 1337;
    hash = hash + patternTiles.GetHashCode();
    hash = hash * 1337;
    hash = hash + xOffset.GetHashCode();
    hash = hash * 1337;
    hash = hash + yOffset.GetHashCode();
    return hash;
  }
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
  public string DStr() {
    return "Pattern(" +
        '"' + name + '"' + ", " +
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
      var cornersByShapeIndex = Vec2ListList.Parse(source);
      source.Expect(",");
      var patternTiles = PatternTileList.Parse(source);
      source.Expect(",");
      var xOffset = Vec2.Parse(source);
      source.Expect(",");
      var yOffset = Vec2.Parse(source);
      source.Expect(")");
      return new Pattern(name, cornersByShapeIndex, patternTiles, xOffset, yOffset);
  }
}
     
}
