using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  class SnakeDirector {
    // This algorithm repeatedly has a snake "slither" in some direction, for some number of spaces.
    // This is that number of spaces.
    public const int SLITHER_DISTANCE = 4;
    public const int AVOID_DISTANCE_STEPS = 4; // how much we should steer away from other paths
    
    public static (SortedSet<Location>, SortedSet<Location>) addSnakes(Rand rand, Terrain terrain, bool considerCornersAdjacent, Location originLocation, float radius, SortedSet<Location> circleLocs) {
      var snakes = new List<Snake>();
      var deadSnakes = new List<Snake>();
      
      var firstSnakeInitialLocation = ListUtils.GetRandomN(new List<Location>(circleLocs), rand, 0, 4)[0];
      var firstSnakeInitialPosition = terrain.pattern.GetTileCenter(firstSnakeInitialLocation);
      var firstSnakeInitialPositionToCenter =
          terrain.pattern.GetTileCenter(new Location(0, 0, 0)).minus(firstSnakeInitialPosition);
      // Make it go towards the center...
      var firstSnakeDirectionToCenter = Direction.fromVec(firstSnakeInitialPositionToCenter);
      // ...well, *roughly* towards the center.
      var firstSnakeInitialDirection =
          new Direction(
              firstSnakeDirectionToCenter.dirNum +
              rand.Next(-(Direction.NUM / 4 - 1), (Direction.NUM / 4 - 1)));
      var firstSnake = new Snake(rand, terrain, considerCornersAdjacent, originLocation, radius, firstSnakeInitialLocation, firstSnakeInitialDirection);
      snakes.Add(firstSnake);

      growSnakes(rand, terrain, snakes, deadSnakes);

      var dijkstra = makeDijkstra(terrain, originLocation, circleLocs);
      var possibleSnakeStarts = GetPossibleSnakeStarts(rand, terrain.pattern, dijkstra, considerCornersAdjacent);

      int i = 0;
      while (i < possibleSnakeStarts.Count) {
        var (startLoc, startDir) = possibleSnakeStarts[i];
        var snake = new Snake(rand, terrain, considerCornersAdjacent, originLocation, radius, startLoc, startDir);
        snakes.Add(snake);
        growSnakes(rand, terrain, snakes, deadSnakes);
        if (snake.GetPathSoFar().Count > 0) {
          dijkstra = makeDijkstra(terrain, originLocation, circleLocs);
          possibleSnakeStarts = GetPossibleSnakeStarts(rand, terrain.pattern, dijkstra, considerCornersAdjacent);
          i = 0;
        } else {
          i++;
        }
      }

      // The locations for the main paths, made by the snakes, so 1 wide and contiguous.
      var mainLocs = new SortedSet<Location>(terrain.tiles.Keys);
      AddRightSides(terrain, circleLocs, deadSnakes);
      // All the extra locations to make the main paths wider.
      var sideLocs = new SortedSet<Location>(terrain.tiles.Keys);
      SetUtils.RemoveAll(sideLocs, mainLocs);

      // Now, undo any areas that are too small.
      // WARNING: this now means the terrain is out of sync with the snakes, don't use the snakes anymore.
      var removedLocs = RemoveSmallPaths(terrain, considerCornersAdjacent);
      // Use mainLocs and sideLocs instead.
      SetUtils.RemoveAll(mainLocs, removedLocs);
      SetUtils.RemoveAll(sideLocs, removedLocs);

      return (mainLocs, sideLocs);
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
          if (snakes[i].GetPathSoFar().Count > 0 && rand.Next() % 8 == 0) {
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
        locationAndTile.Value.components.Add(terrain.root.EffectGrassTTCCreate().AsITerrainTileComponent());
      }
    }

    static void AddRightSides(Terrain terrain, SortedSet<Location> circleLocs, List<Snake> deadSnakes) {
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
              if (!circleLocs.Contains(bAdjacentLoc)) {
                continue;
              }
              
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
                  SnakingCaveTerrainGenerator.AddTile(
                      terrain, bAdjacentLoc, SnakingCaveTerrainGenerator.PATH_HEIGHT, terrain.root.EffectGrassTTCCreate().AsITerrainTileComponent());
                }
              }
            }
          }
          previousPreviousLocation = previousLocation;
          previousLocation = newLocation;
        }
      }
    }

    static List<(Location, Direction)> GetPossibleSnakeStarts(
        Rand rand, Pattern pattern, AStarExplorer dijkstra, bool considerCornersAdjacent) {
      var possibleSnakeStarts = new List<(Location, Direction)>();
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
          possibleSnakeStarts.Add((exploredLoc, dirAwayFromExistingPaths));
        }
      }
      return ListUtils.Shuffled(possibleSnakeStarts, rand, 2);
    }

    static AStarExplorer makeDijkstra(Terrain terrain, Location originLocation, SortedSet<Location> circleLocs) {
      return new AStarExplorer(
          new SortedSet<Location>(terrain.tiles.Keys),
          (to) => terrain.pattern.GetAdjacentLocations(to, false),
          (a, b, totalCost) => circleLocs.Contains(b) && !terrain.TileExists(b),
          (loc) => false, // dont stop early
          (a) => 0, // try everything equally
          (a, b) => 1); // every step costs 1
    }

    static SortedSet<Location> RemoveSmallPaths(Terrain terrain, bool considerCornersAdjacent) {
      var undiscoveredLocs = new SortedSet<Location>(terrain.tiles.Keys);

      var removedLocs = new SortedSet<Location>();
      
      while (undiscoveredLocs.Count > 0) {
        var islandExplorer =
            new AStarExplorer(
                new SortedSet<Location>() {SetUtils.GetFirst(undiscoveredLocs)},
                (to) => terrain.pattern.GetAdjacentLocations(to, considerCornersAdjacent),
                (a, b, totalCost) => undiscoveredLocs.Contains(b),
                (a) => false, // dont stop early
                (a) => 0, // no cost to get to the goal, there is no goal
                (a, b) => 0); // no cost to go to any adjacent
        var islandLocs = islandExplorer.getClosedLocations();
        if (islandLocs.Count <= 16) {
          foreach (var islandLoc in islandLocs) {
            var tile = terrain.tiles[islandLoc];
            terrain.tiles.Remove(islandLoc);
            tile.Destruct();
            removedLocs.Add(islandLoc);
          }
        }
        foreach (var islandLoc in islandLocs) {
          undiscoveredLocs.Remove(islandLoc);
        }
      }

      return removedLocs;
    }

  }
}