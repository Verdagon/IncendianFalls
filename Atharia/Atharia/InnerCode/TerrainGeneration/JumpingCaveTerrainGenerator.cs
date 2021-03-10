using System;
using System.Collections.Generic;
using System.Diagnostics;
using Atharia.Model;

namespace IncendianFalls {
  // An anglice is an angle slice number.
  // 0 means 0 degrees
  // 1 means 15 degrees
  // and so on to 345.
  struct Direction : IComparable<Direction> {
    public const int NUM = 16;
    private const int RIGHT = 0;
    private const int UP = NUM / 4;
    private const int LEFT = NUM * 2 / 4;
    private const int DOWN = NUM * 3 / 4;

    public readonly int dirNum;

    public Direction(int dirNum) {
      this.dirNum = (dirNum + 2 * NUM) % NUM;
    }
    
    public static Direction operator +(Direction a, int i) => new Direction((a.dirNum + i) % NUM);
    public static Direction operator -(Direction a, int i) => new Direction((a.dirNum + NUM - (i % NUM)) % NUM);
    
    // I think we could make this super-optimized (and deterministic) if we wanted to:
    // int xOverYTimes1000 = x * 1000 / y;
    // int yOverXTimes1000 = y * 1000 / x;
    // then remember whether x was positive, y was positive, and if |x| > |y|.
    // then we only have to deal with an eighth of the world.
    // That's like 4 if-statements, which we can even simplify into multiply-with-bool
    // expressions.
    // Then reconstruct the final resulting slice number based on all those.
    public static Direction fromXY(float x, float y) {
      double radians = Math.Atan2(y, x);
      int dirNum = (int)(radians * NUM / (2 * Math.PI));
      return new Direction(dirNum);
    }
    public static Direction fromVec(Vec2 vec) {
      return fromXY(vec.x, vec.y);
    }

    // We can optimize this into a lookup table
    public Vec2 toVec() {
      double radians = dirNum * (2 * Math.PI / NUM);
      return new Vec2((float)Math.Cos(radians), (float)Math.Sin(radians));
    }

    public override int GetHashCode() { return dirNum; }
    public override bool Equals(object obj) {
      if (obj is Direction) {
        return dirNum == ((Direction) obj).dirNum;
      } else {
        return false;
      }
    }
    
    public int CompareTo(Direction that) {
      return dirNum.CompareTo(that.dirNum);
    }
  }
  
  public class JumpingCaveTerrainGenerator {
    // This algorithm repeatedly has a snake "slither" in some direction, for some number of spaces.
    // This is that number of spaces.
    private const int SLITHER_DISTANCE = 4;
    private const int AVOID_DISTANCE_STEPS = 4; // how much we should steer away from other paths

    private class Snake {
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
        var (weightsByDirection, directionToThatWayPath) = GetDirectionWeights(SLITHER_DISTANCE, true, snakeCurrentDirection);

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
          GetDirectionWeights(SLITHER_DISTANCE, true, snakeCurrentDirection);
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
            AddTile(terrain, newLocation, 3);
          }

          snakeCurrentLocation = newLocation;
        }
        
        Asserts.Assert(thatWayPath.Count > 0);
        snakeCurrentDirection = chosenDirection;
      }

      public Snake fork() {
        var (directionWeights, directionToThatWayPath) =
            GetDirectionWeights(SLITHER_DISTANCE * 1.5f, false, new Direction(0));

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
                terrain.pattern,
                new SortedSet<Location>() { snakeCurrentLocation },
                considerCornersAdjacent,
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
                terrain.pattern,
                nearbyForeignTiles,
                considerCornersAdjacent,
                (a, b, totalCost) => {
                  // Only consider locations that are actually nearby the start location and are within AVOID_DISTANCE_STEPS
                  // steps of the locations to avoid.
                  // The + 0.5 is to avoid float rounding errors.
                  return nearbyLocations.Contains(b) && totalCost < AVOID_DISTANCE_STEPS + 0.5;
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
        
        var directionToThatWayPath = GetDirectionToThatWayPathMap(terrain.pattern, considerCornersAdjacent, snakeCurrentLocation);
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
    
    public static Terrain Generate(
        SSContext context,
        Root root,
        Pattern pattern,
        Rand rand,
        bool considerCornersAdjacent,
        float radius) {
      float elevationStepHeight = .2f;

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      var terrain =
          root.EffectTerrainCreate(
              pattern,
              elevationStepHeight,
              root.EffectTerrainTileByLocationMutMapCreate());

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      // var terrain = new SortedDictionary<Location, int>();
      
      // AddCircle(context, terrain, new Location(0, 0, 0), radius);

      var originLocation = new Location(0, 0, 0);
      var originPosition = pattern.GetTileCenter(originLocation);
      
      // Get a random location in the level's circle.
      var circleLocations = GetCircle(context, pattern, new Location(0, 0, 0), radius);

      var snakes = new List<Snake>();
      var snakeInitialLocation = ListUtils.GetRandomN(new List<Location>(circleLocations), rand, 0, 1)[0];
      var snakeInitialDirection = new Direction(rand.Next() % Direction.NUM);
      var firstSnake =
          new Snake(
              rand, terrain, considerCornersAdjacent, originLocation, radius, snakeInitialLocation, snakeInitialDirection);
      snakes.Add(firstSnake);

      var deadSnakes = new List<Snake>();

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
      
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      foreach (var locationAndTile in terrain.tiles) {
        locationAndTile.Value.components.Add(context.root.EffectMudTTCCreate().AsITerrainTileComponent());
      }

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
            var aPos = pattern.GetTileCenter(aLoc);
            var bPos = pattern.GetTileCenter(bLoc);
            var cPos = pattern.GetTileCenter(cLoc);
            var abVec = bPos.minus(aPos);
            var bcVec = cPos.minus(bPos);

            var abRightDir = Math.Atan2(abVec.y, abVec.x) - Math.PI / 2;
            var bcRightDir = Math.Atan2(bcVec.y, bcVec.x) - Math.PI / 2;
            var abRightVec = new Vec2((float)Math.Cos(abRightDir), (float)Math.Sin(abRightDir));
            var bcRightVec = new Vec2((float)Math.Cos(bcRightDir), (float)Math.Sin(bcRightDir));
            
            // if a->b->c is acute, then grab the locations that are parallel to BOTH a->b AND b->c.
            // if a->b->c is obtuse (or 180), then grab the locations that are parallel to EITHER a->b OR b->c.
            var abcAcute = abRightVec.dot(bcVec) >= 0;
            
            foreach (var bAdjacentLoc in pattern.GetAdjacentLocations(bLoc, true)) {
              var bAdjacentPos = pattern.GetTileCenter(bAdjacentLoc);
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
                  tile.components.Add(context.root.EffectFloorTTCCreate().AsITerrainTileComponent());
                }
              }
            }
          }
          previousPreviousLocation = previousLocation;
          previousLocation = newLocation;
        }
      }

      // var adjacentLocs =
      //     terrain.pattern.GetAdjacentLocations(
      //         new SortedSet<Location>(terrain.tiles.Keys), false, considerCornersAdjacent);
      // foreach (var adjacentLoc in adjacentLocs) {
      //   var tile = AddTile(terrain, adjacentLoc, 3);
      //   tile.components.Add(context.root.EffectDirtTTCCreate().AsITerrainTileComponent());
      // }
      
      return terrain;
    }

    static SortedDictionary<Direction, List<Location>> GetDirectionToThatWayPathMap(
        Pattern pattern,
        bool considerCornersAdjacent,
        Location fromLocation) {
      var fromPosition = pattern.GetTileCenter(fromLocation);
      var explorer =
          new AStarExplorer(
              pattern,
              new SortedSet<Location>() { fromLocation },
              considerCornersAdjacent,
              (a, b, totalCost) => pattern.GetTileCenter(fromLocation).distance(pattern.GetTileCenter(b)) < SLITHER_DISTANCE,
              (a) => false,
              (a) => 0,
              (a, b) => pattern.GetTileCenter(a).distance(pattern.GetTileCenter(b)));
      var directionToTargetPosition = new Vec2[Direction.NUM];
      var directionToThatWayLocationAndError = new (Location, float)[Direction.NUM];
      for (int i = 0; i < Direction.NUM; i++) {
        var direction = new Direction(i);
        Vec2 targetPosition = fromPosition.plus(new Vec2(direction.toVec().x * 3, direction.toVec().y * 3));
        directionToTargetPosition[direction.dirNum] = targetPosition;
        // Just to give it an arbitrary initial location
        var initialLocation = fromLocation;
        var initialLocationError = pattern.GetTileCenter(initialLocation).distance(targetPosition);
        directionToThatWayLocationAndError[direction.dirNum] = (initialLocation, initialLocationError);
      }
      foreach (var thisLocation in explorer.getClosedLocations()) {
        for (int dirNum = 0; dirNum < directionToTargetPosition.Length; dirNum++) {
          var targetPosition = directionToTargetPosition[dirNum];
          var thisLocationError = pattern.GetTileCenter(thisLocation).distance(targetPosition);
          var existingClosestLocationError = directionToThatWayLocationAndError[dirNum].Item2;
          if (thisLocationError < existingClosestLocationError) {
            directionToThatWayLocationAndError[dirNum] = (thisLocation, thisLocationError);
          }
        }
      }

      var directionToThatWayPath = new SortedDictionary<Direction, List<Location>>();
      for (int dirNum = 0; dirNum < Direction.NUM; dirNum++) {
        var thatWayLocation = directionToThatWayLocationAndError[dirNum].Item1;
        var thatWayPath = explorer.GetPathTo(thatWayLocation);
        Asserts.Assert(thatWayPath.Count > 0);
        directionToThatWayPath.Add(new Direction(dirNum), thatWayPath);
      }
      return directionToThatWayPath;
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

    private static TerrainTile AddTile(Terrain terrain, Location location, int elevation) {
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
