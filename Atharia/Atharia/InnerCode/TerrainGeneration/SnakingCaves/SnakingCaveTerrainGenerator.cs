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
      var circleLocs = GetCircle(context, pattern, new Location(0, 0, 0), radius);

      var (mainLocs, sideLocs) = addSnakes(rand, terrain, considerCornersAdjacent, originLocation, radius, circleLocs);
      
      var untestedPossibleBridgeEndLocs = pattern.GetAdjacentLocations(mainLocs, false, considerCornersAdjacent);
      SetUtils.RemoveAll(untestedPossibleBridgeEndLocs, sideLocs);
      SetUtils.RetainAll(untestedPossibleBridgeEndLocs, circleLocs);
      
      var locToPossibleBridgeEndingThereMap = new SortedDictionary<Location, List<Bridge>>();
      var locToPossibleBridgesTouchingThereMap = new SortedDictionary<Location, List<Bridge>>();
      foreach (var untestedPossibleBridgeEndLoc in untestedPossibleBridgeEndLocs) {
        var discoveredPossibleBridgesEndingHere =
            BridgeMaker.getBridges(pattern, circleLocs, mainLocs, sideLocs, untestedPossibleBridgeEndLoc);
        foreach (var bridge in discoveredPossibleBridgesEndingHere) {
          foreach (var loc in bridge.getEndLocations()) {
            if (locToPossibleBridgeEndingThereMap.TryGetValue(loc, out List<Bridge> existingPossibleBridgesEndingHere)) {
              existingPossibleBridgesEndingHere.Add(bridge);
            } else {
              locToPossibleBridgeEndingThereMap.Add(loc, new List<Bridge>(){ bridge });
            }
          }
          foreach (var loc in bridge.getPathLocations()) {
            if (locToPossibleBridgesTouchingThereMap.TryGetValue(loc, out List<Bridge> existingPossibleBridgesTouchingHere)) {
              existingPossibleBridgesTouchingHere.Add(bridge);
            } else {
              locToPossibleBridgesTouchingThereMap.Add(loc, new List<Bridge>{ bridge });
            }
          }
        }
      }
      
      var nonEmptyLocs = new SortedSet<Location>(terrain.tiles.Keys);
      var nonEmptyAndAdjacentLocs = pattern.GetAdjacentLocations(nonEmptyLocs, true, false);
      var overlayLocs = new SortedSet<Location>(circleLocs);
      SetUtils.RemoveAll(overlayLocs, nonEmptyAndAdjacentLocs);
      foreach (var overlayLoc in overlayLocs) {
        if (locToPossibleBridgeEndingThereMap.TryGetValue(overlayLoc, out List<Bridge> bridgesEndingHere)) {
          var newBridge = bridgesEndingHere[0];
          implementBridge(terrain, newBridge);
          
          // If any bridge ends or paths in any of these locations, get rid of them.
          var otherBridgeBlastZone = new SortedSet<Location>(newBridge.getAllLocations());
          otherBridgeBlastZone = pattern.GetAdjacentLocations(otherBridgeBlastZone, true, false);
      
          // Remove all bridges that end in any of these locations.
          var bridgesToRemove = new List<Bridge>();
          foreach (var bridgeEndLoc in otherBridgeBlastZone) {
            if (locToPossibleBridgesTouchingThereMap.TryGetValue(bridgeEndLoc, out List<Bridge> bridgesPathingThere)) {
              foreach (var bridgePathingThere in bridgesPathingThere) {
                bridgesToRemove.Add(bridgePathingThere);
              }
            }
          }
          while (bridgesToRemove.Count > 0) {
            var bridge = bridgesToRemove[0];
            // These bridges are being removed, so remove references to them in other ends' and paths' lists.
            foreach (var bridgeEndingThereEndLoc in bridge.getEndLocations()) {
              locToPossibleBridgeEndingThereMap[bridgeEndingThereEndLoc].Remove(bridge);
              if (locToPossibleBridgeEndingThereMap[bridgeEndingThereEndLoc].Count == 0) {
                locToPossibleBridgeEndingThereMap.Remove(bridgeEndingThereEndLoc);
              }
            }
            foreach (var bridgePathingTherePathLoc in bridge.getPathLocations()) {
              locToPossibleBridgesTouchingThereMap[bridgePathingTherePathLoc].Remove(bridge);
              if (locToPossibleBridgesTouchingThereMap[bridgePathingTherePathLoc].Count == 0) {
                locToPossibleBridgesTouchingThereMap.Remove(bridgePathingTherePathLoc);
              }
            }
            while (bridgesToRemove.Remove(bridge)) { }
          }
        } else {
          if (!terrain.TileExists(overlayLoc)) {
            var tile = AddTile(terrain, overlayLoc, 3);
            tile.components.Add(terrain.root.EffectMudTTCCreate().AsITerrainTileComponent());
          }
        }
      }

      // foreach (var overlayLoc in overlayLocs) {
      //   if (locToPossibleBridgeEndingThereMap.ContainsKey(overlayLoc)) {
      //     var tile = AddTile(terrain, overlayLoc, locToPossibleBridgeEndingThereMap[overlayLoc].Count);
      //     tile.components.Add(terrain.root.EffectMudTTCCreate().AsITerrainTileComponent());
      //   } else {
      //     var tile = AddTile(terrain, overlayLoc, 3);
      //     tile.components.Add(terrain.root.EffectMudTTCCreate().AsITerrainTileComponent());
      //   }
      // }
      
      
      // // var tile = AddTile(terrain, untestedPossibleBridgeStartLoc, 3);
      // // tile.components.Add(terrain.root.EffectMudTTCCreate().AsITerrainTileComponent());
      // foreach (var loc in locToPossibleBridgeEndingThereMap.Keys) {
      //   if (!terrain.TileExists(loc)) {
      //     var tile = AddTile(terrain, loc, locToPossibleBridgeEndingThereMap[loc].Count);
      //     tile.components.Add(terrain.root.EffectMudTTCCreate().AsITerrainTileComponent());
      //   }
      // }

      // we'll need to make any locations next to ADEH -1, not just IJKL. See BridgeMaking2.png for an example.

      return terrain;
    }

    static void implementBridge(Terrain terrain, Bridge bridge) {
      foreach (var bridgeLoc in bridge.getAllLocations()) {
        if (!terrain.TileExists(bridgeLoc)) {
          var newTile = AddTile(terrain, bridgeLoc, 3);
          newTile.components.Add(terrain.root.EffectMudTTCCreate().AsITerrainTileComponent());
        }
      }

      terrain.tiles[bridge.bLoc].elevation = 1;
      terrain.tiles[bridge.cLoc].elevation = 1;
      terrain.tiles[bridge.fLoc].elevation = 1;
      terrain.tiles[bridge.gLoc].elevation = 1;
      
      terrain.tiles[bridge.aLoc].elevation = 5;
      terrain.tiles[bridge.dLoc].elevation = 5;
      terrain.tiles[bridge.eLoc].elevation = 5;
      terrain.tiles[bridge.hLoc].elevation = 5;
      
      terrain.tiles[bridge.iLoc].elevation = 4;
      terrain.tiles[bridge.jLoc].elevation = 4;
      terrain.tiles[bridge.kLoc].elevation = 4;
      terrain.tiles[bridge.lLoc].elevation = 4;
      
      terrain.tiles[bridge.mLoc].elevation = 2;
      terrain.tiles[bridge.nLoc].elevation = 2;
      terrain.tiles[bridge.oLoc].elevation = 2;
      terrain.tiles[bridge.pLoc].elevation = 2;
      
      
    }

    static (SortedSet<Location>, SortedSet<Location>) addSnakes(Rand rand, Terrain terrain, bool considerCornersAdjacent, Location originLocation, float radius, SortedSet<Location> circleLocs) {
      
      var snakes = new List<Snake>();
      var deadSnakes = new List<Snake>();
      
      var firstSnakeInitialLocation = ListUtils.GetRandomN(new List<Location>(circleLocs), rand, 0, 4)[0];
      var firstSnakeInitialDirection = new Direction(rand.Next() % Direction.NUM);
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
      AddRightSides(terrain, deadSnakes);
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

    static SortedSet<Location> RemoveSmallPaths(Terrain terrain, bool considerCornersAdjacent) {
      var undiscoveredLocs = new SortedSet<Location>(terrain.tiles.Keys);

      var removedLocs = new SortedSet<Location>();
      
      while (undiscoveredLocs.Count > 0) {
        var islandExplorer =
            new AStarExplorer(
                terrain.pattern,
                new SortedSet<Location>() {SetUtils.GetFirst(undiscoveredLocs)},
                considerCornersAdjacent,
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

    static AStarExplorer makeDijkstra(Terrain terrain, Location originLocation, SortedSet<Location> circleLocs) {
      return new AStarExplorer(
          terrain.pattern,
          new SortedSet<Location>(terrain.tiles.Keys),
          false,
          (a, b, totalCost) => circleLocs.Contains(b) && !terrain.TileExists(b),
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
                  tile.components.Add(terrain.root.EffectMudTTCCreate().AsITerrainTileComponent());
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
