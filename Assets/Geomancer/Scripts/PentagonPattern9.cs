using System;
using System.Collections.Generic;
using Geomancer.Model;

namespace Geomancer {
  public class PentagonPattern9 {
    public static Pattern makePentagon9Pattern() {
      return new Pattern(
        "pentagon9",
        new Vec2ImmListImmList(new Vec2ImmList[] { // cornersByShapeIndex
          new Vec2ImmList(new Vec2[] { // corner for shape 0
            new Vec2(-132, -497),
            new Vec2(378, -497),
            new Vec2(732, 313),
            new Vec2(-130, 504),
            new Vec2(-855, 0)
          }),
          new Vec2ImmList(new Vec2[] { // corner for shape 1
            new Vec2(132, -497),
            new Vec2(855, -1),
            new Vec2(130, 504),
            new Vec2(-732, 313),
            new Vec2(-378, -497)
          })
        }),
        new PatternTileImmList(new PatternTile[] { // patternTiles
          new PatternTile(
            0, 0, new Vec2(0, 0),
            new PatternSideAdjacencyImmList(new PatternSideAdjacency[] { // sideAdjacenciesBySideIndex
              new PatternSideAdjacency(0, -1, 6, 4),
              new PatternSideAdjacency(1, 0, 1, 1),
              new PatternSideAdjacency(0, 0, 2, 1),
              new PatternSideAdjacency(0, 0, 1, 3),
              new PatternSideAdjacency(-1, -1, 7, 1)
            }),
            new PatternCornerAdjacencyImmListImmList(new PatternCornerAdjacencyImmList[] { // cornerAdjacenciesByCornerIndex
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 0
                new PatternCornerAdjacency(-1, -1, 7, 1),
                new PatternCornerAdjacency(0, -1, 6, 0)
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 1
                new PatternCornerAdjacency(0, -1, 6, 4),
                new PatternCornerAdjacency(1, 0, 1, 2)
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 2
                new PatternCornerAdjacency(0, 0, 2, 2),
                new PatternCornerAdjacency(1, 0, 1, 1),
                new PatternCornerAdjacency(1, 0, 3, 3),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 3
                new PatternCornerAdjacency(0, 0, 1, 4),
                new PatternCornerAdjacency(0, 0, 2, 1),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 4
                new PatternCornerAdjacency(0, 0, 1, 3),
                new PatternCornerAdjacency(-1, -1, 6, 3),
                new PatternCornerAdjacency(-1, -1, 7, 2),
              }),
            })
          ),
          new PatternTile( // tile 1
            1, 1772, new Vec2(-697, 774),
            new PatternSideAdjacencyImmList(new PatternSideAdjacency[] { // sideAdjacenciesBySideIndex
              new PatternSideAdjacency(0, 0, 3, 3),
              new PatternSideAdjacency(-1, 0, 0, 1),
              new PatternSideAdjacency(-1, -1, 6, 3),
              new PatternSideAdjacency(0, 0, 0, 3),
              new PatternSideAdjacency(0, 0, 2, 0),
            }),
            new PatternCornerAdjacencyImmListImmList(new PatternCornerAdjacencyImmList[] { // cornerAdjacenciesByCornerIndex
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 0
                new PatternCornerAdjacency(0, 0, 2, 0),
                new PatternCornerAdjacency(0, 0, 3, 4),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 4
                new PatternCornerAdjacency(-1, 0, 0, 2),
                new PatternCornerAdjacency(-1, 0, 2, 2),
                new PatternCornerAdjacency(0, 0, 3, 3),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 3
                new PatternCornerAdjacency(-1, -1, 6, 4),
                new PatternCornerAdjacency(-1, 0, 0, 1),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 2
                new PatternCornerAdjacency(0, 0, 0, 4),
                new PatternCornerAdjacency(-1, -1, 7, 2),
                new PatternCornerAdjacency(-1, -1, 6, 3),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 1
                new PatternCornerAdjacency(0, 0, 0, 3),
                new PatternCornerAdjacency(0, 0, 2, 1),
              }),
            })
          ),
          new PatternTile( // tile 2
            0, 4913, new Vec2(274, 972),
            new PatternSideAdjacencyImmList(new PatternSideAdjacency[] { // sideAdjacenciesBySideIndex
              new PatternSideAdjacency(0, 0, 1, 4),
              new PatternSideAdjacency(0, 0, 0, 2),
              new PatternSideAdjacency(1, 0, 3, 2),
              new PatternSideAdjacency(0, 0, 4, 0),
              new PatternSideAdjacency(0, 0, 3, 4),
            }),
            new PatternCornerAdjacencyImmListImmList(new PatternCornerAdjacencyImmList[] { // cornerAdjacenciesByCornerIndex
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 0
                new PatternCornerAdjacency(0, 0, 1, 0),
                new PatternCornerAdjacency(0, 0, 3, 4),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 1
                new PatternCornerAdjacency(0, 0, 0, 3),
                new PatternCornerAdjacency(0, 0, 1, 4),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 2
                new PatternCornerAdjacency(0, 0, 0, 2),
                new PatternCornerAdjacency(1, 0, 1, 1),
                new PatternCornerAdjacency(1, 0, 3, 3),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 3
                new PatternCornerAdjacency(0, 0, 4, 1),
                new PatternCornerAdjacency(1, 0, 3, 2),
                new PatternCornerAdjacency(1, 0, 5, 2),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 4
                new PatternCornerAdjacency(0, 0, 3, 0),
                new PatternCornerAdjacency(0, 0, 4, 0),
              }),
            })
          ),
          new PatternTile( // tile 3
            0, 8055, new Vec2(-406, 1846),
            new PatternSideAdjacencyImmList(new PatternSideAdjacency[] { // sideAdjacenciesBySideIndex
              new PatternSideAdjacency(0, 0, 4, 4),
              new PatternSideAdjacency(0, 0, 5, 2),
              new PatternSideAdjacency(-1, 0, 2, 2),
              new PatternSideAdjacency(0, 0, 1, 0),
              new PatternSideAdjacency(0, 0, 2, 4),
            }),
            new PatternCornerAdjacencyImmListImmList(new PatternCornerAdjacencyImmList[] { // cornerAdjacenciesByCornerIndex
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 0
                new PatternCornerAdjacency(0, 0, 2, 4),
                new PatternCornerAdjacency(0, 0, 4, 0),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 1
                new PatternCornerAdjacency(0, 0, 4, 4),
                new PatternCornerAdjacency(0, 0, 5, 3),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 2
                new PatternCornerAdjacency(-1, 0, 2, 3),
                new PatternCornerAdjacency(-1, 0, 4, 1),
                new PatternCornerAdjacency(0, 0, 5, 2),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 3
                new PatternCornerAdjacency(0, 0, 1, 1),
                new PatternCornerAdjacency(-1, 0, 0, 2),
                new PatternCornerAdjacency(-1, 0, 2, 2),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 4
                new PatternCornerAdjacency(0, 0, 1, 0),
                new PatternCornerAdjacency(0, 0, 2, 0),
              }),
            })
          ),
          new PatternTile( // tile 4
            1, 4913, new Vec2(573, 2028),
            new PatternSideAdjacencyImmList(new PatternSideAdjacency[] { // sideAdjacenciesBySideIndex
              new PatternSideAdjacency(0, 0, 2, 3),
              new PatternSideAdjacency(1, 0, 5, 1),
              new PatternSideAdjacency(0, 0, 7, 3),
              new PatternSideAdjacency(0, 0, 5, 3),
              new PatternSideAdjacency(0, 0, 3, 0),
            }),
            new PatternCornerAdjacencyImmListImmList(new PatternCornerAdjacencyImmList[] { // cornerAdjacenciesByCornerIndex
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 0
                new PatternCornerAdjacency(0, 0, 2, 4),
                new PatternCornerAdjacency(0, 0, 3, 0),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 4
                new PatternCornerAdjacency(0, 0, 2, 3),
                new PatternCornerAdjacency(1, 0, 3, 2),
                new PatternCornerAdjacency(1, 0, 5, 2),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 3
                new PatternCornerAdjacency(0, 0, 7, 4),
                new PatternCornerAdjacency(1, 0, 5, 1),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 2
                new PatternCornerAdjacency(0, 0, 5, 4),
                new PatternCornerAdjacency(0, 0, 6, 2),
                new PatternCornerAdjacency(0, 0, 7, 3),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 1
                new PatternCornerAdjacency(0, 0, 3, 1),
                new PatternCornerAdjacency(0, 0, 5, 3),
              }),
            })
          ),
          new PatternTile( // tile 5
            0, 3142, new Vec2(-115, 2817),
            new PatternSideAdjacencyImmList(new PatternSideAdjacency[] { // sideAdjacenciesBySideIndex
              new PatternSideAdjacency(-1, 0, 7, 4),
              new PatternSideAdjacency(-1, 0, 4, 1),
              new PatternSideAdjacency(0, 0, 3, 1),
              new PatternSideAdjacency(0, 0, 4, 3),
              new PatternSideAdjacency(0, 0, 6, 1),
            }),
            new PatternCornerAdjacencyImmListImmList(new PatternCornerAdjacencyImmList[] { // cornerAdjacenciesByCornerIndex
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 0
                new PatternCornerAdjacency(-1, 0, 7, 0),
                new PatternCornerAdjacency(0, 0, 6, 1),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 1
                new PatternCornerAdjacency(-1, 0, 4, 2),
                new PatternCornerAdjacency(-1, 0, 7, 4),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 2
                new PatternCornerAdjacency(0, 0, 3, 2),
                new PatternCornerAdjacency(-1, 0, 2, 3),
                new PatternCornerAdjacency(-1, 0, 4, 1),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 3
                new PatternCornerAdjacency(0, 0, 3, 1),
                new PatternCornerAdjacency(0, 0, 4, 4),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 4
                new PatternCornerAdjacency(0, 0, 6, 2),
                new PatternCornerAdjacency(0, 0, 7, 3),
                new PatternCornerAdjacency(0, 0, 4, 3),
              }),
            })
          ),
          new PatternTile( // tile 6
            1, 3142, new Vec2(866, 3317),
            new PatternSideAdjacencyImmList(new PatternSideAdjacency[] { // sideAdjacenciesBySideIndex
              new PatternSideAdjacency(-1, 0, 7, 0),
              new PatternSideAdjacency(0, 0, 5, 4),
              new PatternSideAdjacency(0, 0, 7, 2),
              new PatternSideAdjacency(1, 1, 1, 2),
              new PatternSideAdjacency(0, 1, 0, 0),
            }),
            new PatternCornerAdjacencyImmListImmList(new PatternCornerAdjacencyImmList[] { // cornerAdjacenciesByCornerIndex
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 0
                new PatternCornerAdjacency(0, 1, 0, 0),
                new PatternCornerAdjacency(-1, 0, 7, 1),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 4
                new PatternCornerAdjacency(0, 0, 5, 0),
                new PatternCornerAdjacency(-1, 0, 7, 0),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 3
                new PatternCornerAdjacency(0, 0, 5, 4),
                new PatternCornerAdjacency(0, 0, 4, 3),
                new PatternCornerAdjacency(0, 0, 7, 3),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 2
                new PatternCornerAdjacency(0, 0, 7, 2),
                new PatternCornerAdjacency(1, 1, 1, 3),
                new PatternCornerAdjacency(1, 1, 0, 4),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 1
                new PatternCornerAdjacency(0, 1, 0, 1),
                new PatternCornerAdjacency(1, 1, 1, 2),
              }),
            })
          ),
          new PatternTile( // tile 7
            1, 0, new Vec2(1466, 2500),
            new PatternSideAdjacencyImmList(new PatternSideAdjacency[] { // sideAdjacenciesBySideIndex
              new PatternSideAdjacency(1, 0, 6, 0),
              new PatternSideAdjacency(1, 1, 0, 4),
              new PatternSideAdjacency(0, 0, 6, 2),
              new PatternSideAdjacency(0, 0, 4, 2),
              new PatternSideAdjacency(1, 0, 5, 0),
            }),
            new PatternCornerAdjacencyImmListImmList(new PatternCornerAdjacencyImmList[] { // cornerAdjacenciesByCornerIndex
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 0
                new PatternCornerAdjacency(1, 0, 5, 0),
                new PatternCornerAdjacency(1, 0, 6, 1),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 4
                new PatternCornerAdjacency(1, 0, 6, 0),
                new PatternCornerAdjacency(1, 1, 0, 0),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 3
                new PatternCornerAdjacency(0, 0, 6, 3),
                new PatternCornerAdjacency(1, 1, 1, 3),
                new PatternCornerAdjacency(1, 1, 0, 4),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 2
                new PatternCornerAdjacency(0, 0, 4, 3),
                new PatternCornerAdjacency(0, 0, 5, 4),
                new PatternCornerAdjacency(0, 0, 6, 2),
              }),
              new PatternCornerAdjacencyImmList(new PatternCornerAdjacency[] { // corner 1
                new PatternCornerAdjacency(0, 0, 4, 2),
                new PatternCornerAdjacency(1, 0, 5, 1),
              }),
            })
          )
        }),
        new Vec2(1599, -1307), // xOffset
        new Vec2(866, 4307) // yOffset
      );
    }
  }
}
