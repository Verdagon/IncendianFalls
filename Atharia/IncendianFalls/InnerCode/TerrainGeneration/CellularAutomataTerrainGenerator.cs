using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class CellularAutomataTerrainGenerator {
    public static Terrain Generate(Root root, Pattern pattern, Rand rand, bool considerCornersAdjacent, float radius) {
      float elevationStepHeight = .2f;

      var terrain =
          root.EffectTerrainCreate(
              pattern,
              elevationStepHeight,
              root.EffectTerrainTileByLocationMutMapCreate());

      AddCircle(terrain, new Location(0, 0, 0), radius);

      CellularAutomata(rand, terrain, considerCornersAdjacent);

      return terrain;
    }

    public static void AddCircle(Terrain terrain, Location originLocation, float radius) {
      var searcher = new PatternExplorer(terrain.pattern, false, originLocation);

      while (true) {
        Location loc = searcher.Next();
        Vec2 center = terrain.pattern.GetTileCenter(loc);
        if (center.distance(new Vec2(0, 0)) <= radius) {
          if (!terrain.tiles.ContainsKey(loc)) {
            AddTile(terrain, loc, 0);
          }
        } else {
          break;
        }
      }
    }

    private static void CellularAutomata(Rand rand, Terrain terrain, bool considerCornersAdjacent) {
      TerrainUtils.randify(rand, terrain, 2);

      for (int i = 0; i < 2; i++) {
        CellularAutomataModeIteration(terrain, considerCornersAdjacent);
      }

      //CellularAutomataAverageIteration(terrain);
      FinishUp(rand, terrain, considerCornersAdjacent);
    }

    public static void FinishUp(Rand rand, Terrain terrain, bool considerCornersAdjacent) {
      var locationsToRemove = new List<Location>();
      foreach (var locationAndTile in terrain.tiles) {
        if (locationAndTile.Value.elevation == 0) {
          locationsToRemove.Add(locationAndTile.Key);
        }
      }

      foreach (var locationToRemove in locationsToRemove) {
        var tile = terrain.tiles[locationToRemove];
        terrain.tiles.Remove(locationToRemove);
        tile.Destruct();
      }

      var rooms = TerrainUtils.IdentifyRooms(terrain, considerCornersAdjacent);
      var numRoomsBeforeConnecting = rooms.Count;
      TerrainUtils.ConnectRooms(terrain.pattern, rand, rooms);
      for (int i = numRoomsBeforeConnecting; i < rooms.Count; i++) {
        var newRoom = rooms[i];
        foreach (var location in newRoom) {
          AddTile(terrain, location, 1);
        }
      }
    }

    private static void AddTile(Terrain terrain, Location location, int elevation) {
      var tile =
        terrain.root.EffectTerrainTileCreate(
              elevation,
              ITerrainTileComponentMutBunch.New(terrain.root));
      terrain.tiles.Add(location, tile);
    }

    public static void CellularAutomataModeIteration(Terrain terrain, bool considerCornersAdjacent) {
      var newElevationByLocation = new Dictionary<Location, int>();

      foreach (var locationAndTile in terrain.tiles) {
        var location = locationAndTile.Key;
        var tile = locationAndTile.Value;

        var numOccurrencesByElevation = new Dictionary<int, int>();
        numOccurrencesByElevation[tile.elevation] = 1;
        foreach (var neighbor in terrain.GetAdjacentExistingLocations(location, considerCornersAdjacent)) {
          var neighborElevation = terrain.tiles[neighbor].elevation;
          numOccurrencesByElevation.TryGetValue(neighborElevation, out int previousOccurrences);
          numOccurrencesByElevation[neighborElevation] = previousOccurrences + 1;
        }

        int mostCommonElevation = tile.elevation; // Chosen arbitrarily
        foreach (var elevationAndNumOccurrences in numOccurrencesByElevation) {
          var elevation = elevationAndNumOccurrences.Key;
          var numOccurrences = elevationAndNumOccurrences.Value;
          if (numOccurrences > numOccurrencesByElevation[mostCommonElevation]) {
            mostCommonElevation = elevation;
          }
        }
        Asserts.Assert(mostCommonElevation != -1);
        newElevationByLocation[location] = mostCommonElevation;
      }

      foreach (var locationAndTile in terrain.tiles) {
        var location = locationAndTile.Key;
        var tile = locationAndTile.Value;
        tile.elevation = newElevationByLocation[location];
      }
    }

    private static void CellularAutomataAverageIteration(Terrain terrain) {
      var newElevationByLocation = new Dictionary<Location, int>();

      foreach (var locationAndTile in terrain.tiles) {
        var location = locationAndTile.Key;
        var tile = locationAndTile.Value;

        int numNearby = 1;
        int elevationsSum = tile.elevation;

        foreach (var neighbor in terrain.GetAdjacentExistingLocations(location, false)) {
          numNearby++;
          var neighborElevation = terrain.tiles[neighbor].elevation;
          elevationsSum += neighborElevation;
        }
        int averageElevation = elevationsSum / numNearby;

        newElevationByLocation[location] = averageElevation;
      }

      foreach (var locationAndTile in terrain.tiles) {
        var location = locationAndTile.Key;
        var tile = locationAndTile.Value;
        tile.elevation = newElevationByLocation[location];
      }
    }
  }
}
