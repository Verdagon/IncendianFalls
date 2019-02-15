using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class SquarePattern {
    public static Pattern MakeSquarePattern() {
      return new Pattern(
        "square",
        new Vec2ListList(new Vec2List[] { // cornersByShapeIndex
          new Vec2List(new Vec2[] { // corner for shape 0
            new Vec2(-0.5f, 0.5f), // top left
            new Vec2(-0.5f, -0.5f), // low left
            new Vec2(0.5f, -0.5f), // low right
            new Vec2(0.5f, 0.5f), // top right
          }),
        }),
        new PatternTileList(new PatternTile[] { // patternTiles
          new PatternTile(
            0, 0, new Vec2(0, 0),
            new PatternSideAdjacencyList(new PatternSideAdjacency[] { // sideAdjacenciesBySideIndex
              new PatternSideAdjacency(-1, 0, 0, 2),
              new PatternSideAdjacency(0, -1, 0, 3),
              new PatternSideAdjacency(1, 0, 0, 0),
              new PatternSideAdjacency(0, 1, 0, 1),
            }),
            new PatternCornerAdjacencyListList(new PatternCornerAdjacencyList[] { // cornerAdjacenciesByCornerIndex
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 0
                new PatternCornerAdjacency(0, 1, 0, 1),
                new PatternCornerAdjacency(-1, 1, 0, 2),
                new PatternCornerAdjacency(-1, 0, 0, 3),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 1
                new PatternCornerAdjacency(-1, 0, 0, 2),
                new PatternCornerAdjacency(-1, -1, 0, 3),
                new PatternCornerAdjacency(0, -1, 0, 0),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 2
                new PatternCornerAdjacency(0, -1, 0, 3),
                new PatternCornerAdjacency(1, -1, 0, 0),
                new PatternCornerAdjacency(1, 0, 0, 1),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 3
                new PatternCornerAdjacency(1, 0, 0, 0),
                new PatternCornerAdjacency(1, 1, 0, 1),
                new PatternCornerAdjacency(0, 1, 0, 2),
              }),
            })
          )
        }),
        new Vec2(1f, 0), // xOffset
        new Vec2(0, 1f) // yOffset
      );
    }
  }
}
