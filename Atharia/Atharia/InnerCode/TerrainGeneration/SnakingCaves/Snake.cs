using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  class Snake {
      private Rand rand;
      private Terrain terrain;
      private bool considerCornersAdjacent;
      private Location originLocation;
      private float radius;
      public readonly Location snakeInitialLocation;
      private Location snakeCurrentLocation;
      private Direction snakeCurrentDirection;
      private SortedSet<Location> previousPreviousSlither = new SortedSet<Location>();
      private SortedSet<Location> previousSlither = new SortedSet<Location>();
      private List<Location> pathSoFar = new List<Location>();

      public Snake(Rand rand, Terrain terrain, bool considerCornersAdjacent, Location originLocation, float radius, Location initialLocation, Direction initialDirection) {
        this.rand = rand;
        this.terrain = terrain;
        this.considerCornersAdjacent = considerCornersAdjacent;
        this.originLocation = originLocation;
        this.radius = radius;
        this.snakeInitialLocation = initialLocation;
        this.snakeCurrentLocation = initialLocation;
        this.snakeCurrentDirection = initialDirection;
      }

      public List<Location> GetPathSoFar() {
        return new List<Location>(pathSoFar);
      }

      // Returns whether its still alive
      public bool slither() {
        // Lets make it so we dont want to go straight, but we do like turning slightly, but don't like turning a lot.
        var (weightsByDirection, directionToThatWayPath) = GetDirectionWeights(SnakeDirector.SLITHER_DISTANCE, true, snakeCurrentDirection);

        // for debugging
        var backupWeightsByDirection = new SortedDictionary<Direction, int>(weightsByDirection);
        
        int totalWeight = 0;
        for (int dirNum = 0; dirNum < Direction.NUM; dirNum++) {
          var direction = new Direction(dirNum);
          weightsByDirection[direction] = Math.Max(0, weightsByDirection[direction]);
          totalWeight += weightsByDirection[direction];
        }

        string weightsStr = "weights:";
        for (int dirNum = 0; dirNum < Direction.NUM; dirNum++) {
          weightsStr += " " + dirNum + ":" + weightsByDirection[new Direction(dirNum)];
        }
        Console.WriteLine(weightsStr);

        if (totalWeight == 0) {
          // End the snake!
          GetDirectionWeights(SnakeDirector.SLITHER_DISTANCE, true, snakeCurrentDirection);
          return false;
        } else {
          int choiceInWeights = rand.Next() % totalWeight;
          var chosenDirection = new Direction(0);
          for (int dirNum = 0; dirNum < Direction.NUM; dirNum++) {
            var direction = new Direction(dirNum);
            if (choiceInWeights < weightsByDirection[direction]) {
              chosenDirection = direction;
              break;
            }
            choiceInWeights -= weightsByDirection[direction];
          }
          
          CommitSlither(chosenDirection, directionToThatWayPath[chosenDirection], false);
          
          // Still alive!
          return true;
        }
      }

      private void CommitSlither(Direction chosenDirection, List<Location> thatWayPath, bool overlapOkay) {
        previousPreviousSlither = previousSlither;
        previousSlither = new SortedSet<Location>();

        foreach (var newLocation in thatWayPath) {
          if (terrain.tiles.ContainsKey(newLocation)) {
            Asserts.Assert(overlapOkay);
          } else {
            previousSlither.Add(newLocation);
            pathSoFar.Add(newLocation);
            IntertwiningCaveTerrainGenerator.AddTile(terrain, newLocation, IntertwiningCaveTerrainGenerator.PATH_HEIGHT);
          }

          snakeCurrentLocation = newLocation;
        }
        
        Asserts.Assert(thatWayPath.Count > 0);
        snakeCurrentDirection = chosenDirection;
      }

      public Snake fork() {
        var (directionWeights, directionToThatWayPath) =
            GetDirectionWeights(SnakeDirector.SLITHER_DISTANCE * 1.5f, false, new Direction(0));

        var leftDirection = snakeCurrentDirection + 3;
        var rightDirection = snakeCurrentDirection - 3;
        var leftOpen = directionWeights[leftDirection] > 0;
        var rightOpen = directionWeights[rightDirection] > 0;
        if (!leftOpen || !rightOpen) {
          return null;
        }

        var leftPath = directionToThatWayPath[leftDirection];
        var rightPath = directionToThatWayPath[rightDirection];
        
        CommitSlither(leftDirection, leftPath, false);
        SetUtils.AddAll(previousPreviousSlither, rightPath);

        var newSnake =
            new Snake(
                rand, terrain, considerCornersAdjacent, originLocation, radius, snakeCurrentLocation,
                rightDirection);
        // Overlap is okay, because the left path and the right path might share a tile
        newSnake.CommitSlither(rightDirection, rightPath, true);
        SetUtils.AddAll(newSnake.previousPreviousSlither, previousPreviousSlither);

        return newSnake;
      }

      private (SortedDictionary<Direction, int>, SortedDictionary<Direction, List<Location>>)
          GetDirectionWeights(float slitherDistance, bool leanTowards, Direction leanTowardsDirection) {
        Vec2 originPosition = terrain.pattern.GetTileCenter(originLocation);
        Vec2 snakeCurrentPosition = terrain.pattern.GetTileCenter(snakeCurrentLocation);
        
        var weightsByDirection = new SortedDictionary<Direction, int>();
        if (leanTowards) {
          for (int i = 0; i < Direction.NUM; i++) {
            weightsByDirection[new Direction(i)] = -10;
          }
          // So far, we can't go in any direction, they're all negative.
          // Now lets make some of them positive so we go that way.
          weightsByDirection[leanTowardsDirection + 0] = 5;
          weightsByDirection[leanTowardsDirection - 1] = 15;
          weightsByDirection[leanTowardsDirection + 1] = 15;
          weightsByDirection[leanTowardsDirection - 2] = 20;
          weightsByDirection[leanTowardsDirection + 2] = 20;
          weightsByDirection[leanTowardsDirection - 3] = 5;
          weightsByDirection[leanTowardsDirection + 3] = 5;
          weightsByDirection[leanTowardsDirection - 4] = 0;
          weightsByDirection[leanTowardsDirection + 4] = 0;
        } else {
          for (int i = 0; i < Direction.NUM; i++) {
            weightsByDirection[new Direction(i)] = 10;
          }
        }

        // If we're near the edge, then weigh against the direction of the edge.
        Vec2 centerToCurrentPosition = snakeCurrentPosition.minus(originPosition);
        if (centerToCurrentPosition.length() > Math.Max(radius - 5, radius * .8f)) {
          Direction directionToOutside = Direction.fromVec(centerToCurrentPosition);
          weightsByDirection[directionToOutside + 0] += -11;
          weightsByDirection[directionToOutside - 1] += -11;
          weightsByDirection[directionToOutside + 1] += -11;
          weightsByDirection[directionToOutside - 2] += -6;
          weightsByDirection[directionToOutside + 2] += -6;
          weightsByDirection[directionToOutside - 3] += -1;
          weightsByDirection[directionToOutside + 3] += -1;
          weightsByDirection[directionToOutside - 4] += 0;
          weightsByDirection[directionToOutside + 4] += 0;
          weightsByDirection[directionToOutside - 5] += 4;
          weightsByDirection[directionToOutside + 5] += 4;
          weightsByDirection[directionToOutside - 6] += 9;
          weightsByDirection[directionToOutside + 6] += 9;
          weightsByDirection[directionToOutside - 7] += 9;
          weightsByDirection[directionToOutside + 7] += 9;
          weightsByDirection[directionToOutside + 8] += 9;
        }

        // This will explore everything within one slither.
        // Find everything within one slither past that, so we can identify any possible other slithers to avoid.
        // This doesn't use distance anywhere, just number of steps.
        var nearbyLocationsExplorer =
            new AStarExplorer(
                new SortedSet<Location>() { snakeCurrentLocation },
                (to) => terrain.pattern.GetAdjacentLocations(to, considerCornersAdjacent),
                // the + 0.5 is just to avoid floating rounding errors
                (a, b, totalCost) => totalCost <= slitherDistance * 2 + 0.5f,
                (a) => false, // Dont stop early
                (a) => 0, // Every step is equally towards the "goal" (there is no goal)
                (a, b) => 1); // Each step costs 1
        var nearbyLocations = nearbyLocationsExplorer.getClosedLocations();
        // A foreign tile is a tile that we didnt just place (in the last two slithers).
        var nearbyForeignTiles = new SortedSet<Location>();
        foreach (var nearbyLocation in nearbyLocations) {
          if (!terrain.tiles.ContainsKey(nearbyLocation)) {
            // We're only interested in tiles that already exist.
            continue;
          }
          if (previousSlither.Contains(nearbyLocation) || previousPreviousSlither.Contains(nearbyLocation)) {
            // We're not interested in tiles that were placed in the previous two slithers.
            continue;
          }
          // If we're here, its a tile that was placed before the last two slithers.
          nearbyForeignTiles.Add(nearbyLocation);
        }
        // These are locations that are near the foreign locations, so we want to steer away from them.
        var unusableLocationsExplorer =
            new AStarExplorer(
                nearbyForeignTiles,
                (to) => terrain.pattern.GetAdjacentLocations(to, considerCornersAdjacent),
                (a, b, totalCost) => {
                  // Only consider locations that are actually nearby the start location and are within AVOID_DISTANCE_STEPS
                  // steps of the locations to avoid.
                  // The + 0.5 is to avoid float rounding errors.
                  return nearbyLocations.Contains(b) && totalCost < SnakeDirector.AVOID_DISTANCE_STEPS + 0.5;
                },
                (a) => false, // Dont stop early
                (a) => 0, // Every step is equally towards the "goal" (there is no goal)
                (a, b) => 1); // Each step costs 1
        var unusableLocations = unusableLocationsExplorer.getClosedLocations();
        // Let's also exclude the tiles from the last two slithers (though we wont avoid the tiles near them).
        SetUtils.AddAll(unusableLocations, previousPreviousSlither);
        SetUtils.AddAll(unusableLocations, previousSlither);
        // Let's also exclude the tiles that are out of bounds.
        foreach (var nearbyLocation in nearbyLocations) {
          if (terrain.pattern.GetTileCenter(nearbyLocation).distance(originPosition) > radius) {
            unusableLocations.Add(nearbyLocation);
          }
        }
        
        var directionToThatWayPath =
            DirectionToThatWayPathMap.GetDirectionToThatWayPathMap(
                terrain.pattern, considerCornersAdjacent, snakeCurrentLocation);
        foreach (var directionAndThatWayPath in directionToThatWayPath) {
          var direction = directionAndThatWayPath.Key;
          var thatWayPath = directionAndThatWayPath.Value;
          foreach (var thatWayLocation in thatWayPath) {
            if (unusableLocations.Contains(thatWayLocation)) {
              weightsByDirection[direction] -= 100;
              break;
            }
          }
        }

        return (weightsByDirection, directionToThatWayPath);
      }
    }
    
}