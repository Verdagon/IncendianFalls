using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class CellularAutomataTerrainGenerator {
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
              considerCornersAdjacent,
              elevationStepHeight,
              root.EffectTerrainTileByLocationMutMapCreate());

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      AddCircle(context, terrain, new Location(0, 0, 0), radius);

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      CellularAutomata(context, rand, terrain, considerCornersAdjacent);

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      return terrain;
    }

    public static void AddCircle(SSContext context, Terrain terrain, Location originLocation, float radius) {
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      var searcher = new PatternExplorer(context, terrain.pattern, false, originLocation);

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      while (true) {
        context.Flare(context.root.GetDeterministicHashCode().ToString());

        Location loc = searcher.Next(context);
        context.Flare(context.root.GetDeterministicHashCode().ToString());
        Vec2 center = terrain.pattern.GetTileCenter(loc);
        context.Flare(context.root.GetDeterministicHashCode().ToString());
        if (center.distance(new Vec2(0, 0)) <= radius) {
          context.Flare(context.root.GetDeterministicHashCode().ToString());
          if (!terrain.tiles.ContainsKey(loc)) {
            context.Flare(context.root.GetDeterministicHashCode().ToString());
            AddTile(context, terrain, loc, 0);
            context.Flare(context.root.GetDeterministicHashCode().ToString());
          }
          context.Flare(context.root.GetDeterministicHashCode().ToString());
        } else {
          context.Flare(context.root.GetDeterministicHashCode().ToString());
          break;
        }
        context.Flare(context.root.GetDeterministicHashCode().ToString());
      }
      context.Flare(context.root.GetDeterministicHashCode().ToString());

    }

    private static void CellularAutomata(SSContext context, Rand rand, Terrain terrain, bool considerCornersAdjacent) {
      TerrainUtils.randify(rand, terrain, 2);

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      for (int i = 0; i < 2; i++) {
        CellularAutomataModeIteration(context, terrain, considerCornersAdjacent);
      }

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      //CellularAutomataAverageIteration(terrain);
      FinishUp(context, rand, terrain, considerCornersAdjacent);
    }

    public static void FinishUp(SSContext context, Rand rand, Terrain terrain, bool considerCornersAdjacent) {
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
      TerrainUtils.ConnectRooms(context, terrain.pattern, rand, rooms);
      for (int i = numRoomsBeforeConnecting; i < rooms.Count; i++) {
        var newRoom = rooms[i];
        foreach (var location in newRoom) {
          AddTile(context, terrain, location, 1);
          //terrain.tiles[location].components.Add(terrain.root.EffectRocksTTCCreate().AsITerrainTileComponent());
        }
      }
    }

    private static void AddTile(SSContext context, Terrain terrain, Location location, int elevation) {
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      var tile =
        terrain.root.EffectTerrainTileCreate(
              NullITerrainTileEvent.Null,
              elevation,
              ITerrainTileComponentMutBunch.New(terrain.root));
      context.Flare(context.root.GetDeterministicHashCode().ToString());

      terrain.tiles.Add(location, tile);
      context.Flare(context.root.GetDeterministicHashCode().ToString());
    }

    public static void CellularAutomataModeIteration(SSContext context, Terrain terrain, bool considerCornersAdjacent) {
      var newElevationByLocation = new SortedDictionary<Location, int>();

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      foreach (var locationAndTile in terrain.tiles) {
        context.Flare(context.root.GetDeterministicHashCode().ToString());

        var location = locationAndTile.Key;
        var tile = locationAndTile.Value;

        var numOccurrencesByElevation = new SortedDictionary<int, int>();
        numOccurrencesByElevation[tile.elevation] = 1;
        context.Flare(context.root.GetDeterministicHashCode().ToString());

        foreach (var neighbor in terrain.GetAdjacentExistingLocations(location, considerCornersAdjacent)) {
          var neighborElevation = terrain.tiles[neighbor].elevation;
          numOccurrencesByElevation.TryGetValue(neighborElevation, out int previousOccurrences);
          numOccurrencesByElevation[neighborElevation] = previousOccurrences + 1;
          context.Flare(context.root.GetDeterministicHashCode().ToString());

        }

        context.Flare(context.root.GetDeterministicHashCode().ToString());

        int mostCommonElevation = tile.elevation; // Chosen arbitrarily
        foreach (var elevationAndNumOccurrences in numOccurrencesByElevation) {
          context.Flare(context.root.GetDeterministicHashCode().ToString());

          var elevation = elevationAndNumOccurrences.Key;
          var numOccurrences = elevationAndNumOccurrences.Value;
          if (numOccurrences > numOccurrencesByElevation[mostCommonElevation]) {
            mostCommonElevation = elevation;
          }
        }
        context.Flare(context.root.GetDeterministicHashCode().ToString());

        Asserts.Assert(mostCommonElevation != -1);
        newElevationByLocation[location] = mostCommonElevation;
      }

      foreach (var locationAndTile in terrain.tiles) {
        context.Flare(context.root.GetDeterministicHashCode().ToString());

        var location = locationAndTile.Key;
        var tile = locationAndTile.Value;
        tile.elevation = newElevationByLocation[location];
      }
      context.Flare(context.root.GetDeterministicHashCode().ToString());

    }

    private static void CellularAutomataAverageIteration(Terrain terrain) {
      var newElevationByLocation = new SortedDictionary<Location, int>();

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
