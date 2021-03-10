using System;
using System.Collections.Generic;
using System.Diagnostics;
using Atharia.Model;

namespace IncendianFalls {
  public class SnakingCaveTerrainGenerator {
    // This algorithm repeatedly has a snake "slither" in some direction, for some number of spaces.
    // This is that number of spaces.
    public const int SLITHER_DISTANCE = 4;
    public const int AVOID_DISTANCE_STEPS = 4; // how much we should steer away from other paths

    public static Terrain Generate(
        SSContext context,
        Root root,
        Pattern pattern,
        Rand rand,
        bool considerCornersAdjacent,
        float radius) {
      float elevationStepHeight = .2f;

      var terrain =
          root.EffectTerrainCreate(
              pattern,
              elevationStepHeight,
              root.EffectTerrainTileByLocationMutMapCreate());

      var originLocation = new Location(0, 0, 0);
      
      // Get a random location in the level's circle.
      var circleLocations = GetCircle(context, pattern, new Location(0, 0, 0), radius);

      var snakes = new List<Snake>();
      var deadSnakes = new List<Snake>();
      
      var firstSnakeInitialLocation = ListUtils.GetRandomN(new List<Location>(circleLocations), rand, 0, 1)[0];
      var firstSnakeInitialDirection = new Direction(rand.Next() % Direction.NUM);
      var firstSnake = new Snake(rand, terrain, considerCornersAdjacent, originLocation, radius, firstSnakeInitialLocation, firstSnakeInitialDirection);
      snakes.Add(firstSnake);

      growSnakes(rand, terrain, snakes, deadSnakes);

      var dijkstra = makeDijkstra(terrain, originLocation, circleLocations);
      var possibleStarts = GetPossibleStarts(rand, pattern, dijkstra, considerCornersAdjacent);

      int i = 0;
      while (i < possibleStarts.Count) {
        var (startLoc, startDir) = possibleStarts[i];
        var snake = new Snake(rand, terrain, considerCornersAdjacent, originLocation, radius, startLoc, startDir);
        snakes.Add(snake);
        growSnakes(rand, terrain, snakes, deadSnakes);
        if (snake.GetPathSoFar().Count > 0) {
          dijkstra = makeDijkstra(terrain, originLocation, circleLocations);
          possibleStarts = GetPossibleStarts(rand, pattern, dijkstra, considerCornersAdjacent);
          i = 0;
        } else {
          i++;
        }
      }
      
      // Now, undo any paths that are less than 10.
      // start here
      
      AddRightSides(terrain, deadSnakes);
      
      // foreach (var start in possibleStarts) {
      //   if (!terrain.TileExists(start.Item1)) {
      //     var tile = AddTile(terrain, start.Item1, 3);
      //     tile.components.Add(terrain.root.EffectMagmaTTCCreate().AsITerrainTileComponent());
      //   }
      // }

      return terrain;
    }

    static AStarExplorer makeDijkstra(Terrain terrain, Location originLocation, SortedSet<Location> circleLocations) {
      return new AStarExplorer(
          terrain.pattern,
          new SortedSet<Location>(terrain.tiles.Keys),
          false,
          (a, b, totalCost) => circleLocations.Contains(b) && !terrain.TileExists(b),
          (loc) => false, // dont stop early
          (a) => 0, // try everything equally
          (a, b) => 1); // every step costs 1
    }

    static bool getIsolatedSnakeStart(
        out Location retStartLoc, out Direction retStartDir, Pattern pattern, AStarExplorer dijkstraMap) {
      var exploredLocations = dijkstraMap.getClosedLocations();
      var furthestLocation = SetUtils.GetFirst(exploredLocations);
      var furthestLocationCost = dijkstraMap.GetCostTo(furthestLocation);
      foreach (var loc in exploredLocations) {
        var locCost = dijkstraMap.GetCostTo(loc);
        if (locCost > furthestLocationCost) {
          furthestLocation = loc;
          furthestLocationCost = locCost;
        }
      }
      if (furthestLocationCost <= AVOID_DISTANCE_STEPS + 1 + SLITHER_DISTANCE) {
        retStartLoc = new Location(0, 0, 0);
        retStartDir = new Direction(0);
        return false;
      }
      var pathToFurthest = dijkstraMap.GetPathTo(furthestLocation);
      retStartLoc = pathToFurthest[0];
      foreach (var loc in pathToFurthest) {
        retStartLoc = loc;
        if (dijkstraMap.GetCostTo(retStartLoc) >= AVOID_DISTANCE_STEPS + 1) {
          break;
        }
      }
      Asserts.Assert(dijkstraMap.GetCostTo(retStartLoc) >= AVOID_DISTANCE_STEPS + 1);
      retStartDir =
          Direction.fromVec(pattern.GetTileCenter(furthestLocation).minus(pattern.GetTileCenter(retStartLoc)));
      return true;
    }

    static void growSnakes(Rand rand, Terrain terrain, List<Snake> snakes, List<Snake> deadSnakes) {
      while (snakes.Count > 0) {
        for (int i = snakes.Count - 1; i >= 0; i--) {
          if (rand.Next() % 8 == 0) {
            var newSnake = snakes[i].fork();
            if (newSnake != null) {
              snakes.Add(newSnake);
            }
          } else {
            var stillAlive = snakes[i].slither();
            if (!stillAlive) {
              deadSnakes.Add(snakes[i]);
              snakes.RemoveAt(i);
            }
          }
        }
      }
      
      foreach (var locationAndTile in terrain.tiles) {
        locationAndTile.Value.components.Add(terrain.root.EffectMudTTCCreate().AsITerrainTileComponent());
      }
    }

    static void AddRightSides(Terrain terrain, List<Snake> deadSnakes) {
      foreach (var snake in deadSnakes) {
        var previousPreviousLocation = snake.snakeInitialLocation;
        var previousLocation = previousPreviousLocation;
        var pathSoFar = snake.GetPathSoFar();
        foreach (var newLocation in pathSoFar) {
          // Skip first one because we dont know where it came from
          if (previousLocation != previousPreviousLocation) {
            // we're turning a->b->c.
            var aLoc = previousPreviousLocation;
            var bLoc = previousLocation;
            var cLoc = newLocation;
            var aPos = terrain.pattern.GetTileCenter(aLoc);
            var bPos = terrain.pattern.GetTileCenter(bLoc);
            var cPos = terrain.pattern.GetTileCenter(cLoc);
            var abVec = bPos.minus(aPos);
            var bcVec = cPos.minus(bPos);

            var abRightDir = Math.Atan2(abVec.y, abVec.x) - Math.PI / 2;
            var bcRightDir = Math.Atan2(bcVec.y, bcVec.x) - Math.PI / 2;
            var abRightVec = new Vec2((float)Math.Cos(abRightDir), (float)Math.Sin(abRightDir));
            var bcRightVec = new Vec2((float)Math.Cos(bcRightDir), (float)Math.Sin(bcRightDir));
            
            var abcAcute = abRightVec.dot(bcVec) >= 0;
            // if a->b->c is acute, then grab the locations that are to the right of BOTH a->b AND b->c.
            // if a->b->c is obtuse (or 180), then grab the locations that are to the right of EITHER a->b OR b->c.
            // This should avoid us grabbing locations on the left accidentally when there are acute angles.
            
            foreach (var bAdjacentLoc in terrain.pattern.GetAdjacentLocations(bLoc, true)) {
              var bAdjacentPos = terrain.pattern.GetTileCenter(bAdjacentLoc);
              var bToAdjacent = bAdjacentPos.minus(bPos);
              var rightOfAB = abRightVec.dot(bToAdjacent) >= 0;
              var rightOfBC = bcRightVec.dot(bToAdjacent) >= 0;

              bool isRightOfB = false;
              if (abcAcute) {
                isRightOfB = rightOfAB && rightOfBC;
              } else {
                isRightOfB = rightOfAB || rightOfBC;
              }
              if (isRightOfB) {
                if (!terrain.tiles.ContainsKey(bAdjacentLoc)) {
                  var tile = AddTile(terrain, bAdjacentLoc, 3);
                  tile.components.Add(terrain.root.EffectFloorTTCCreate().AsITerrainTileComponent());
                }
              }
            }
          }
          previousPreviousLocation = previousLocation;
          previousLocation = newLocation;
        }
      }
    }

    static List<(Location, Direction)> GetPossibleStarts(
        Rand rand, Pattern pattern, AStarExplorer dijkstra, bool considerCornersAdjacent) {
      var possibleStarts = new List<(Location, Direction)>();
      foreach (var exploredLoc in dijkstra.getClosedLocations()) {
        var cost = dijkstra.GetCostTo(exploredLoc);
        if (cost >= AVOID_DISTANCE_STEPS + 1 && cost < AVOID_DISTANCE_STEPS + 2) {
          var path = dijkstra.GetPathTo(exploredLoc);
          if (path.Count < 2) {
            continue;
          }
          // This is closer to existing paths
          var fromLoc = path[path.Count - 2];
          Asserts.Assert(pattern.LocationsAreAdjacent(exploredLoc, fromLoc, considerCornersAdjacent));
          var dirAwayFromExistingPaths =
              Direction.fromVec(pattern.GetTileCenter(exploredLoc).minus(pattern.GetTileCenter(fromLoc)));
          possibleStarts.Add((exploredLoc, dirAwayFromExistingPaths));
        }
      }
      return ListUtils.Shuffled(possibleStarts, rand, 2);
    }

    public static SortedSet<Location> GetCircle(SSContext context, Pattern pattern, Location originLocation, float radius) {
      var searcher = new PatternExplorer(context, pattern, false, originLocation);
      var locationsSoFar = new SortedSet<Location>();
      while (true) {
        Location loc = searcher.Next(context);
        Vec2 center = pattern.GetTileCenter(loc);
        if (center.distance(new Vec2(0, 0)) <= radius) {
          Asserts.Assert(!locationsSoFar.Contains(loc));
          locationsSoFar.Add(loc);
        } else {
          break;
        }
      }
      return locationsSoFar;
    }

    public static TerrainTile AddTile(Terrain terrain, Location location, int elevation) {
      var tile =
        terrain.root.EffectTerrainTileCreate(
              NullITerrainTileEvent.Null,
              elevation,
              ITerrainTileComponentMutBunch.New(terrain.root));
      terrain.tiles.Add(location, tile);
      return tile;
    }
  }
}
