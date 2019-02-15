using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class PentagonPattern9 {
    public static Pattern makePentagon9Pattern() {
      return new Pattern(
        "pentagon9",
        new Vec2ListList(new Vec2List[] { // cornersByShapeIndex
          new Vec2List(new Vec2[] { // corner for shape 0
            new Vec2(-0.1322435067f, -0.4965076715f),
            new Vec2(0.3777818011f, -0.4965076715f),
            new Vec2(0.7323878613f, 0.3125858406f),
            new Vec2(-0.1300622573f, 0.50379034f),
            new Vec2(-0.8551640757f, -0.0007977013127f)
          }),
          new Vec2List(new Vec2[] { // corner for shape 1
            new Vec2(0.1322435067f, -0.4965076715f),
            new Vec2(0.8551640757f, -0.0007977013127f),
            new Vec2(0.1300622573f, 0.50379034f),
            new Vec2(-0.7323878613f, 0.3125858406f),
            new Vec2(-0.3777818011f, -0.4965076715f)
          })
        }),
        new PatternTileList(new PatternTile[] { // patternTiles
          new PatternTile(
            0, 0, new Vec2(0, 0),
            new PatternSideAdjacencyList(new PatternSideAdjacency[] { // sideAdjacenciesBySideIndex
              new PatternSideAdjacency(0, -1, 6, 4),
              new PatternSideAdjacency(1, 0, 1, 1),
              new PatternSideAdjacency(0, 0, 2, 1),
              new PatternSideAdjacency(0, 0, 1, 3),
              new PatternSideAdjacency(-1, -1, 7, 1)
            }),
            new PatternCornerAdjacencyListList(new PatternCornerAdjacencyList[] { // cornerAdjacenciesByCornerIndex
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 0
                new PatternCornerAdjacency(-1, -1, 7, 1),
                new PatternCornerAdjacency(0, -1, 6, 0)
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 1
                new PatternCornerAdjacency(0, -1, 6, 4),
                new PatternCornerAdjacency(1, 0, 1, 2)
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 2
                new PatternCornerAdjacency(0, 0, 2, 2),
                new PatternCornerAdjacency(1, 0, 1, 1),
                new PatternCornerAdjacency(1, 0, 3, 3),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 3
                new PatternCornerAdjacency(0, 0, 1, 4),
                new PatternCornerAdjacency(0, 0, 2, 1),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 4
                new PatternCornerAdjacency(0, 0, 1, 3),
                new PatternCornerAdjacency(-1, -1, 6, 3),
                new PatternCornerAdjacency(-1, -1, 7, 2),
              }),
            })
          ),
          new PatternTile( // tile 1
            1, 101.5f, new Vec2(-0.6966363824f, 0.773766964f),
            new PatternSideAdjacencyList(new PatternSideAdjacency[] { // sideAdjacenciesBySideIndex
              new PatternSideAdjacency(0, 0, 3, 3),
              new PatternSideAdjacency(-1, 0, 0, 1),
              new PatternSideAdjacency(-1, -1, 6, 3),
              new PatternSideAdjacency(0, 0, 0, 3),
              new PatternSideAdjacency(0, 0, 2, 0),
            }),
            new PatternCornerAdjacencyListList(new PatternCornerAdjacencyList[] { // cornerAdjacenciesByCornerIndex
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 0
                new PatternCornerAdjacency(0, 0, 2, 0),
                new PatternCornerAdjacency(0, 0, 3, 4),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 4
                new PatternCornerAdjacency(-1, 0, 0, 2),
                new PatternCornerAdjacency(-1, 0, 2, 2),
                new PatternCornerAdjacency(0, 0, 3, 3),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 3
                new PatternCornerAdjacency(-1, -1, 6, 4),
                new PatternCornerAdjacency(-1, 0, 0, 1),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 2
                new PatternCornerAdjacency(0, 0, 0, 4),
                new PatternCornerAdjacency(-1, -1, 7, 2),
                new PatternCornerAdjacency(-1, -1, 6, 3),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 1
                new PatternCornerAdjacency(0, 0, 0, 3),
                new PatternCornerAdjacency(0, 0, 2, 1),
              }),
            })
          ),
          new PatternTile( // tile 2
            0, 281.5f, new Vec2(0.2738541546f, 0.971740549f),
            new PatternSideAdjacencyList(new PatternSideAdjacency[] { // sideAdjacenciesBySideIndex
              new PatternSideAdjacency(0, 0, 1, 4),
              new PatternSideAdjacency(0, 0, 0, 2),
              new PatternSideAdjacency(1, 0, 3, 2),
              new PatternSideAdjacency(0, 0, 4, 0),
              new PatternSideAdjacency(0, 0, 3, 4),
            }),
            new PatternCornerAdjacencyListList(new PatternCornerAdjacencyList[] { // cornerAdjacenciesByCornerIndex
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 0
                new PatternCornerAdjacency(0, 0, 1, 0),
                new PatternCornerAdjacency(0, 0, 3, 4),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 1
                new PatternCornerAdjacency(0, 0, 0, 3),
                new PatternCornerAdjacency(0, 0, 1, 4),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 2
                new PatternCornerAdjacency(0, 0, 0, 2),
                new PatternCornerAdjacency(1, 0, 1, 1),
                new PatternCornerAdjacency(1, 0, 3, 3),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 3
                new PatternCornerAdjacency(0, 0, 4, 1),
                new PatternCornerAdjacency(1, 0, 3, 2),
                new PatternCornerAdjacency(1, 0, 5, 2),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 4
                new PatternCornerAdjacency(0, 0, 3, 0),
                new PatternCornerAdjacency(0, 0, 4, 0),
              }),
            })
          ),
          new PatternTile( // tile 3
            0, 461.5f, new Vec2(-0.4063642295f, 1.846307043f),
            new PatternSideAdjacencyList(new PatternSideAdjacency[] { // sideAdjacenciesBySideIndex
              new PatternSideAdjacency(0, 0, 4, 4),
              new PatternSideAdjacency(0, 0, 5, 2),
              new PatternSideAdjacency(-1, 0, 2, 2),
              new PatternSideAdjacency(0, 0, 1, 0),
              new PatternSideAdjacency(0, 0, 2, 4),
            }),
            new PatternCornerAdjacencyListList(new PatternCornerAdjacencyList[] { // cornerAdjacenciesByCornerIndex
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 0
                new PatternCornerAdjacency(0, 0, 2, 4),
                new PatternCornerAdjacency(0, 0, 4, 0),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 1
                new PatternCornerAdjacency(0, 0, 4, 4),
                new PatternCornerAdjacency(0, 0, 5, 3),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 2
                new PatternCornerAdjacency(-1, 0, 2, 3),
                new PatternCornerAdjacency(-1, 0, 4, 1),
                new PatternCornerAdjacency(0, 0, 5, 2),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 3
                new PatternCornerAdjacency(0, 0, 1, 1),
                new PatternCornerAdjacency(-1, 0, 0, 2),
                new PatternCornerAdjacency(-1, 0, 2, 2),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 4
                new PatternCornerAdjacency(0, 0, 1, 0),
                new PatternCornerAdjacency(0, 0, 2, 0),
              }),
            })
          ),
          new PatternTile( // tile 4
            1, 281.5f, new Vec2(0.5729603125f, 2.028195672f),
            new PatternSideAdjacencyList(new PatternSideAdjacency[] { // sideAdjacenciesBySideIndex
              new PatternSideAdjacency(0, 0, 2, 3),
              new PatternSideAdjacency(1, 0, 5, 1),
              new PatternSideAdjacency(0, 0, 7, 3),
              new PatternSideAdjacency(0, 0, 5, 3),
              new PatternSideAdjacency(0, 0, 3, 0),
            }),
            new PatternCornerAdjacencyListList(new PatternCornerAdjacencyList[] { // cornerAdjacenciesByCornerIndex
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 0
                new PatternCornerAdjacency(0, 0, 2, 4),
                new PatternCornerAdjacency(0, 0, 3, 0),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 4
                new PatternCornerAdjacency(0, 0, 2, 3),
                new PatternCornerAdjacency(1, 0, 3, 2),
                new PatternCornerAdjacency(1, 0, 5, 2),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 3
                new PatternCornerAdjacency(0, 0, 7, 4),
                new PatternCornerAdjacency(1, 0, 5, 1),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 2
                new PatternCornerAdjacency(0, 0, 5, 4),
                new PatternCornerAdjacency(0, 0, 6, 2),
                new PatternCornerAdjacency(0, 0, 7, 3),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 1
                new PatternCornerAdjacency(0, 0, 3, 1),
                new PatternCornerAdjacency(0, 0, 5, 3),
              }),
            })
          ),
          new PatternTile( // tile 5
            0, 180f, new Vec2(-0.1148420649f, 2.817164191f),
            new PatternSideAdjacencyList(new PatternSideAdjacency[] { // sideAdjacenciesBySideIndex
              new PatternSideAdjacency(-1, 0, 7, 4),
              new PatternSideAdjacency(-1, 0, 4, 1),
              new PatternSideAdjacency(0, 0, 3, 1),
              new PatternSideAdjacency(0, 0, 4, 3),
              new PatternSideAdjacency(0, 0, 6, 1),
            }),
            new PatternCornerAdjacencyListList(new PatternCornerAdjacencyList[] { // cornerAdjacenciesByCornerIndex
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 0
                new PatternCornerAdjacency(-1, 0, 7, 0),
                new PatternCornerAdjacency(0, 0, 6, 1),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 1
                new PatternCornerAdjacency(-1, 0, 4, 2),
                new PatternCornerAdjacency(-1, 0, 7, 4),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 2
                new PatternCornerAdjacency(0, 0, 3, 2),
                new PatternCornerAdjacency(-1, 0, 2, 3),
                new PatternCornerAdjacency(-1, 0, 4, 1),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 3
                new PatternCornerAdjacency(0, 0, 3, 1),
                new PatternCornerAdjacency(0, 0, 4, 4),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 4
                new PatternCornerAdjacency(0, 0, 6, 2),
                new PatternCornerAdjacency(0, 0, 7, 3),
                new PatternCornerAdjacency(0, 0, 4, 3),
              }),
            })
          ),
          new PatternTile( // tile 6
            1, 180f, new Vec2(0.8657324889f, 3.317168873f),
            new PatternSideAdjacencyList(new PatternSideAdjacency[] { // sideAdjacenciesBySideIndex
              new PatternSideAdjacency(-1, 0, 7, 0),
              new PatternSideAdjacency(0, 0, 5, 4),
              new PatternSideAdjacency(0, 0, 7, 2),
              new PatternSideAdjacency(1, 1, 1, 2),
              new PatternSideAdjacency(0, 1, 0, 0),
            }),
            new PatternCornerAdjacencyListList(new PatternCornerAdjacencyList[] { // cornerAdjacenciesByCornerIndex
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 0
                new PatternCornerAdjacency(0, 1, 0, 0),
                new PatternCornerAdjacency(-1, 0, 7, 1),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 4
                new PatternCornerAdjacency(0, 0, 5, 0),
                new PatternCornerAdjacency(-1, 0, 7, 0),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 3
                new PatternCornerAdjacency(0, 0, 5, 4),
                new PatternCornerAdjacency(0, 0, 4, 3),
                new PatternCornerAdjacency(0, 0, 7, 3),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 2
                new PatternCornerAdjacency(0, 0, 7, 2),
                new PatternCornerAdjacency(1, 1, 1, 3),
                new PatternCornerAdjacency(1, 1, 0, 4),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 1
                new PatternCornerAdjacency(0, 1, 0, 1),
                new PatternCornerAdjacency(1, 1, 1, 2),
              }),
            })
          ),
          new PatternTile( // tile 7
            1, 0, new Vec2(1.466444828f, 2.500023412f),
            new PatternSideAdjacencyList(new PatternSideAdjacency[] { // sideAdjacenciesBySideIndex
              new PatternSideAdjacency(1, 0, 6, 0),
              new PatternSideAdjacency(1, 1, 0, 4),
              new PatternSideAdjacency(0, 0, 6, 2),
              new PatternSideAdjacency(0, 0, 4, 2),
              new PatternSideAdjacency(1, 0, 5, 0),
            }),
            new PatternCornerAdjacencyListList(new PatternCornerAdjacencyList[] { // cornerAdjacenciesByCornerIndex
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 0
                new PatternCornerAdjacency(1, 0, 5, 0),
                new PatternCornerAdjacency(1, 0, 6, 1),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 4
                new PatternCornerAdjacency(1, 0, 6, 0),
                new PatternCornerAdjacency(1, 1, 0, 0),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 3
                new PatternCornerAdjacency(0, 0, 6, 3),
                new PatternCornerAdjacency(1, 1, 1, 3),
                new PatternCornerAdjacency(1, 1, 0, 4),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 2
                new PatternCornerAdjacency(0, 0, 4, 3),
                new PatternCornerAdjacency(0, 0, 5, 4),
                new PatternCornerAdjacency(0, 0, 6, 2),
              }),
              new PatternCornerAdjacencyList(new PatternCornerAdjacency[] { // corner 1
                new PatternCornerAdjacency(0, 0, 4, 2),
                new PatternCornerAdjacency(1, 0, 5, 1),
              }),
            })
          )
        }),
        new Vec2(1.598954903f, -1.307432738f), // xOffset
        new Vec2(0.8657324889f, 4.306577432f) // yOffset
      );
    }
  }
}
