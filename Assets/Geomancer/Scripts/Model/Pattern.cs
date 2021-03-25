using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {
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
    
    
    public int DistanceBetween(Location locA, Location locB) {
      return GetTileCenter(locA).distance(GetTileCenter(locB));
    }

    public Vec2 GetTileCenter(Location loc) {
      return xOffset.mul(loc.groupX)
          .plus(yOffset.mul(loc.groupY))
          .plus(patternTiles[loc.indexInGroup].translate);
    }

    private static double DegreesToRadians(double angle) {
      return Math.PI * angle / 180.0;
    }

    public List<Vec2> GetRelativeCornerPositions(Location loc) {
      var patternTile = patternTiles[loc.indexInGroup];
      int shapeIndex = patternTile.shapeIndex;
      double rotateRadians = patternTile.rotateRadianards / 1000f;
      var corners = cornersByShapeIndex[shapeIndex];

      List<Vec2> results = new List<Vec2>();

      for (int i = 0; i < corners.Count; i++) {
        Vec2 unrotatedCorner = corners[i];
        Vec2 rotatedCorner =
            new Vec2(
                (int)(unrotatedCorner.x * Math.Cos(rotateRadians) -
                    unrotatedCorner.y * Math.Sin(rotateRadians)),
                (int)(unrotatedCorner.y * Math.Cos(rotateRadians) +
                    unrotatedCorner.x * Math.Sin(rotateRadians)));
        results.Add(rotatedCorner);
      }

      return results;
    }

    public List<Vec2> GetCornerPositions(Location loc) {
      var center = GetTileCenter(loc);
      List<Vec2> results = new List<Vec2>();
      foreach (var relativeCorner in GetRelativeCornerPositions(loc)) {
        results.Add(center.plus(relativeCorner));
      }
      return results;
    }

    public List<Location> getRelativeAdjacentLocations(int tileIndex, bool adjacentCornersToo) {
      SortedDictionary<Location, object> result = new SortedDictionary<Location, object>();
      PatternTile tile = patternTiles[tileIndex];
      foreach (var sideAdjacency in tile.sideIndexToSideAdjacencies) {
        var location =
            new Location(
                sideAdjacency.groupRelativeX,
                sideAdjacency.groupRelativeY,
                sideAdjacency.tileIndex);
        if (!result.ContainsKey(location)) {
          result.Add(location, new object());
        }
      }
      if (adjacentCornersToo) {
        foreach (var cornerAdjacencies in tile.cornerAdjacenciesByCornerIndex) {
          foreach (var cornerAdjacency in cornerAdjacencies) {
            var location = new Location(cornerAdjacency.groupRelativeX, cornerAdjacency.groupRelativeY, cornerAdjacency.tileIndex);
            if (!result.ContainsKey(location)) {
              result.Add(location, new object());
            }
          }
        }
      }
      return new List<Location>(result.Keys);
    }

    public List<Location> GetAdjacentLocations(Location loc, bool considerCornersAdjacent) {
      List<Location> result = new List<Location>();
      foreach (Location relativeLoc in getRelativeAdjacentLocations(loc.indexInGroup, considerCornersAdjacent)) {
        result.Add(new Location(
          loc.groupX + relativeLoc.groupX,
          loc.groupY + relativeLoc.groupY,
          relativeLoc.indexInGroup));
      }
      return result;
    }

    public SortedSet<Location> GetAdjacentLocations(
                SortedSet<Location> sourceLocs,
        bool includeSourceLocs,
        bool considerCornersAdjacent) {
      var result = new SortedSet<Location>();
      foreach (var originalLocation in sourceLocs) {
        var adjacents = GetAdjacentLocations(originalLocation, considerCornersAdjacent);
        if (includeSourceLocs) {
          adjacents.Add(originalLocation);
        }
        foreach (var adjacentLocation in adjacents) {
          if (!includeSourceLocs && sourceLocs.Contains(adjacentLocation))
            continue;
          result.Add(adjacentLocation);
        }
      }
      return result;
    }

    public bool LocationsAreAdjacent(Location a, Location b, bool considerCornersAdjacent) {
      var locsAdjacentToA = GetAdjacentLocations(a, considerCornersAdjacent);
      return 0 <= locsAdjacentToA.FindIndex(delegate (Location hay) { return hay == b; });
    }

    public int GetDistanceBetween(Location locA, Location locB) {
      return GetTileCenter(locA).distance(GetTileCenter(locB));
    }
}
       
}
