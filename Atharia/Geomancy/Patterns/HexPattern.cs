using Geomancer.Model;
using System;
using System.Collections.Generic;

namespace Geomancer {
  public class HexPattern {
    public static Pattern MakeHexPattern() {
      return new Pattern(
        "hex",
        new Vec2ImmListImmList(new Vec2ImmList[] { // cornersByShapeIndex
          new Vec2ImmList(new Vec2[] { // corner for shape 0
            new Vec2(-0.310f, 0.537f), // top left
            new Vec2(-0.620f, 0f), // left
            new Vec2(-0.310f, -0.537f), // low left
            new Vec2(0.310f, -0.537f), // low right
            new Vec2(0.620f, 0f), // right
            new Vec2(0.310f, 0.537f), // top right
          }),
        }),
        new PatternTileImmList(new PatternTile[] { // patternTiles
          new PatternTile(
            0, 0, new Vec2(0, 0),
            new PatternSideAdjacencyImmList(new PatternSideAdjacency[] { // sideAdjacenciesBySideIndex
              new PatternSideAdjacency(0, 1, 0, 3),
              new PatternSideAdjacency(-1, 1, 0, 4),
              new PatternSideAdjacency(-1, 0, 0, 5),
              new PatternSideAdjacency(0, -1, 0, 0),
              new PatternSideAdjacency(1, -1, 0, 1),
              new PatternSideAdjacency(1, 0, 0, 2),
            }),
            new PatternCornerAdjacencyImmListImmList(new PatternCornerAdjacencyImmList[] { // cornerAdjacenciesByCornerIndex
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 0
                new PatternCornerAdjacency(0, 1, 0, 2),
                new PatternCornerAdjacency(-1, 1, 0, 4),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 1
                new PatternCornerAdjacency(-1, 1, 0, 3),
                new PatternCornerAdjacency(-1, 0, 0, 5),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 2
                new PatternCornerAdjacency(-1, 0, 0, 4),
                new PatternCornerAdjacency(0, -1, 0, 0),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 3
                new PatternCornerAdjacency(0, -1, 0, 5),
                new PatternCornerAdjacency(1, -1, 0, 1),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 4
                new PatternCornerAdjacency(1, -1, 0, 0),
                new PatternCornerAdjacency(1, 0, 0, 2),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 5
                new PatternCornerAdjacency(1, 0, 0, 1),
                new PatternCornerAdjacency(0, 1, 0, 3),
              }),
            })
          )
        }),
        new Vec2(.310f * 3, .537f), // xOffset
        new Vec2(0, 1.074f) // yOffset
      );
    }
  }
}
