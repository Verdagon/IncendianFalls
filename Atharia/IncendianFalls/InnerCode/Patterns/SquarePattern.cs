using Atharia.Model;
using System;
using System.Collections.Generic;

namespace IncendianFalls {
  public class SquarePattern {
    public static Pattern MakeSquarePattern() {
      return new Pattern(
        "square",
        new Vec2ImmListImmList(new Vec2ImmList[] { // cornersByShapeIndex
          new Vec2ImmList(new Vec2[] { // corner for shape 0
            //new Vec2(-0.5f, 0.5f), // top left
            //new Vec2(-0.5f, -0.5f), // low left
            //new Vec2(0.5f, -0.5f), // low right
            //new Vec2(0.5f, 0.5f), // top right
            
            new Vec2(0f, 1f), // top
            new Vec2(-1f, 0f), // left
            new Vec2(0f, -1f), // bottom
            new Vec2(1f, 0f), // right
          }),
        }),
        new PatternTileImmList(new PatternTile[] { // patternTiles
          new PatternTile(
            0, 0, new Vec2(0, 0),
            new PatternSideAdjacencyImmList(new PatternSideAdjacency[] { // sideAdjacenciesBySideIndex
              new PatternSideAdjacency(-1, 0, 0, 2),
              new PatternSideAdjacency(0, -1, 0, 3),
              new PatternSideAdjacency(1, 0, 0, 0),
              new PatternSideAdjacency(0, 1, 0, 1),
            }),
            new PatternCornerAdjacencyImmListImmList(new PatternCornerAdjacencyImmList[] { // cornerAdjacenciesByCornerIndex
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 0
                new PatternCornerAdjacency(0, 1, 0, 1),
                new PatternCornerAdjacency(-1, 1, 0, 2),
                new PatternCornerAdjacency(-1, 0, 0, 3),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 1
                new PatternCornerAdjacency(-1, 0, 0, 2),
                new PatternCornerAdjacency(-1, -1, 0, 3),
                new PatternCornerAdjacency(0, -1, 0, 0),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 2
                new PatternCornerAdjacency(0, -1, 0, 3),
                new PatternCornerAdjacency(1, -1, 0, 0),
                new PatternCornerAdjacency(1, 0, 0, 1),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 3
                new PatternCornerAdjacency(1, 0, 0, 0),
                new PatternCornerAdjacency(1, 1, 0, 1),
                new PatternCornerAdjacency(0, 1, 0, 2),
              }),
            })
          )
        }),
        //new Vec2(1f, 0), // xOffset
        //new Vec2(0, 1f) // yOffset
        new Vec2(1.41f / 2, -1.41f / 2), // xOffset
        new Vec2(1.41f / 2, 1.41f / 2) // yOffset
      );
    }
  }
}
