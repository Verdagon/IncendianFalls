using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class ForestTerrainGenerator {
    public static void Generate(
        out Terrain terrain,
        out SortedDictionary<int, Room> rooms,
        Root root,
        Rand rand,
        Pattern pattern,
        int size) {
      float elevationStepHeight = .2f;

      // The "canvas" is the entire area of the level upon which we will
      // paint our layout of rooms and corridors.
      var canvasSearcher =
          new PatternExplorer(
              pattern,
              false,
              new Location(0, 0, 0),
              new RectanglePrioritizer(
                  pattern.GetTileCenter(new Location(0, 0, 0)),
                  2.0f),
              (location, position) => true);

      var roomByNumber = new SortedDictionary<int, Room>();
      var borderLocations = new SortedSet<Location>();
      var unusedLocations = new SortedSet<Location>();

      // Find a ton of tiles.
      for (int i = 0; i < size; i++) {
        Location loc = canvasSearcher.Next();
        Vec2 center = pattern.GetTileCenter(loc);
        unusedLocations.Add(loc);
      }

      //foreach (var tile in terrain.tiles) {
      //  unusedLocations.Add(tile.Key, new object());
      //}

      // 100 attempts to make rooms.
      for (int i = 0; i < 100; i++) {
        if (unusedLocations.Count == 0) {
          break;
        }
        Location startLocation = SetUtils.GetRandom(rand.Next(), unusedLocations);
        var roomSearcher = new PatternExplorer(pattern, false, startLocation);
        int minNumTilesInRoom = 15;
        int maxNumTilesInRoom = 100;

        var roomFloorLocations =
            new SortedSet<Location>(
                roomSearcher.ExploreWhile(
                    delegate (Location loc) { return unusedLocations.Contains(loc); },
                    maxNumTilesInRoom));

        var roomBorderLocations =
            TerrainUtils.FindBorderLocations(pattern, roomFloorLocations, true);
        // Border locations are not floors, but theyre not unused either.
        SetUtils.RemoveAll(roomFloorLocations, roomBorderLocations);
        SetUtils.RemoveAll(unusedLocations, roomBorderLocations);

        if (roomFloorLocations.Count >= minNumTilesInRoom &&
            roomFloorLocations.Count <= maxNumTilesInRoom) {
          SetUtils.RemoveAll(unusedLocations, roomFloorLocations);
          roomByNumber.Add(roomByNumber.Count, new Room(roomFloorLocations, roomBorderLocations));
        }
      }

      // Now that we have a bunch of rooms, let's connect them.

      //TerrainUtils.ConnectRooms(pattern, rand, roomByNumber);
      throw new Exception("put this back in");

      var tiles = root.EffectTerrainTileByLocationMutMapCreate();

      foreach (var room in roomByNumber.Values) {
        foreach (var roomFloorLocation in room.floors) {
          var tile =
              root.EffectTerrainTileCreate(
                  1, ITerrainTileComponentMutBunch.New(root));
          tile.components.Add(root.EffectGrassTTCCreate().AsITerrainTileComponent());
          tiles.Add(roomFloorLocation, tile);
        }
      }

      var allTiles = new SortedSet<Location>(tiles.Keys);
      var allAdjacent = pattern.GetAdjacentLocations(allTiles, true, true);
      SetUtils.RemoveAll(allAdjacent, allTiles);
      foreach (var borderLocation in allAdjacent) {
        var tile =
            root.EffectTerrainTileCreate(
                2, ITerrainTileComponentMutBunch.New(root));
        tile.components.Add(root.EffectGrassTTCCreate().AsITerrainTileComponent());
        tiles.Add(borderLocation, tile);
      }

      terrain = root.EffectTerrainCreate(pattern, elevationStepHeight, tiles);
      rooms = roomByNumber;
    }

    

  }
}
