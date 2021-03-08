using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class CliffTerrainGeneratorOld {
    //public static Terrain Generate(
    //    SSContext context,
    //    Rand rand,
    //    Pattern pattern,
    //    int size) {
    //  float elevationStepHeight = .4f;

    //  // ############
    //  // ~###########
    //  // ~~##########
    //  // ~~~#########
    //  // ~~~~########
    //  // ~~~~~#######
    //  // ~~~~~~######
    //  // #~~~~~~#####
    //  // ##~~~~~~####
    //  // ###~~~~~~###
    //  // ####~~~~~~##
    //  // #####~~~~~~#
    //  // ######~~~~~~
    //  // #######~~~~~
    //  // ########~~~~
    //  // #########~~~
    //  // ##########~~
    //  // ###########~
    //  // That's the general idea we're going for.
    //  // Notice how the ~s are 6 high, and the land, before being shoved apart by the
    //  // river, was 12 high (and is now 18 high).
    //  // the gorgeHeight is the river height over land height. 6/12=.5
    //  float gorgeHeightRatio = 0.5f;
    //  int gorgeDepth = 10;

    //  // The "canvas" is the entire area of the level upon which we will
    //  // paint our layout of rooms and corridors.
    //  var canvasSearcher =
    //      new PatternExplorer(
    //          pattern,
    //          false,
    //          new Location(0, 0, 0),
    //          new RectanglePrioritizer(
    //              pattern.GetTileCenter(new Location(0, 0, 0)),
    //              1.0f + gorgeHeightRatio));

    //  float mapMinX = 0;
    //  float mapMinY = 0;
    //  float mapMaxX = 0;
    //  float mapMaxY = 0;

    //  var tiles = context.root.EffectTerrainTileByLocationMutMapCreate();

    //  // Find a ton of tiles.
    //  for (int i = 0; i < size; i++) {
    //    Location location = canvasSearcher.Next();
    //    var tile =
    //        context.root.EffectTerrainTileCreate(
    //            1, true, "cliff", ITerrainTileComponentMutBunch.New(context.root));
    //    tiles.Add(location, tile);

    //    var center = pattern.GetTileCenter(location);
    //    mapMinX = Math.Min(mapMinX, center.x);
    //    mapMinY = Math.Min(mapMinY, center.y);
    //    mapMaxX = Math.Max(mapMaxX, center.x);
    //    mapMaxY = Math.Max(mapMaxY, center.y);
    //  }

    //  float mapWidth = mapMaxX - mapMinX;
    //  float mapHeight = mapMaxY - mapMinY;
    //  // When looking down from above, this is the slope of the line. It's ydiff / xdiff.
    //  float gorgeSlope = mapHeight / mapWidth;
    //  float gorgeHeight = mapHeight * gorgeHeightRatio;

    //  var terrain = context.root.EffectTerrainCreate(pattern, elevationStepHeight, tiles);

    //  TerrainUtils.slopify(terrain, new Vec2(0, -1), 1.2f);

    //  foreach (var entry in tiles) {
    //    var location = entry.Key;
    //    context.logger.Info("Have a tile: " + location);
    //    var terrainTile = entry.Value;

    //    // A gorge slice is a vertical slice of the gorge.
    //    // ############
    //    // ~###########
    //    // ~~##########
    //    // ~~~#########
    //    // ~~~~########
    //    // ~~~~*#######
    //    // ~~~~*~######
    //    // #~~~*~~#####
    //    // ##~~*~~~####
    //    // ###~*~~~~###
    //    // ####*~~~~~##
    //    // #####~~~~~~#
    //    // ######~~~~~~
    //    // #######~~~~~
    //    // ########~~~~
    //    // #########~~~
    //    // ##########~~
    //    // ###########~
    //    // That * is an example of a gorge slice.

    //    var center = pattern.GetTileCenter(location);
    //    float x = center.x;
    //    float y = center.y;

    //    float thisXGorgeSliceTopY = gorgeSlope * x + gorgeHeight / 2;
    //    float thisXGorgeSliceBottomY = gorgeSlope * x - gorgeHeight / 2;

    //    float fractionFromGorgeBottom =
    //        (y - thisXGorgeSliceBottomY) / gorgeHeight;
    //    if (fractionFromGorgeBottom < 0 || fractionFromGorgeBottom > 1) {
    //      continue;
    //    }

    //    // The gorge isn't V or U shaped, it's... \__/ shaped.
    //    // It's a shape that goes from (0, 1) to (.25, 0) to (.75, 0) to (1, 1).
    //    float tileElevationFractionInGorge = 0;
    //    if (fractionFromGorgeBottom < .25) {
    //      tileElevationFractionInGorge = -4 * fractionFromGorgeBottom + 1;
    //    } else if (fractionFromGorgeBottom <= .75) {
    //      tileElevationFractionInGorge = 0;
    //    } else {
    //      tileElevationFractionInGorge = 4 * fractionFromGorgeBottom - 3;
    //    }

    //    int tileElevationInGorge =
    //        (int)Math.Floor(tileElevationFractionInGorge * gorgeDepth);
    //    int lowerBy = gorgeDepth - tileElevationInGorge;

    //    terrainTile.elevation -= lowerBy;
    //  }

    //  Vec2 fallStartPosition = new Vec2(mapWidth / 2, mapWidth / 2 * gorgeSlope);
    //  // Will change this to find the nearest to fallStartPosition.
    //  Location fallStartLocation = new Location(0, 0, 0);
    //  foreach (var entry in tiles) {
    //    var location = entry.Key;
    //    var center = pattern.GetTileCenter(location);
    //    if (center.distance(fallStartPosition) < pattern.GetTileCenter(fallStartLocation).distance(fallStartPosition)) {
    //      fallStartLocation = location;
    //    }
    //  }

    //  WaterGeneration.FillWater(context, rand, terrain, fallStartLocation);

    //  // TerrainUtils.randify(rand, terrain, 3);

    //  return terrain;
    //}

    //public static Terrain OldGenerate(
    //    SSContext context,
    //    Rand rand,
    //    Pattern pattern,
    //    int size) {
    //  float elevationStepHeight = .4f;
    //  // This is different from the ForestTerrainGenerator in that rooms can
    //  // share their borders. The borders aren't technically part of the rooms.
    //  // There will often be just one space between the rooms; the border.

    //  var roomByNumber = new SortedDictionary<int, Room>();

    //  var unusedLocations = new SortedSet<Location>();
    //  var borderLocations = new SortedSet<Location>();
    //  var roomLocations = new SortedSet<Location>();

    //  // The "canvas" is the entire area of the level upon which we will
    //  // paint our layout of rooms and corridors.
    //  var canvasSearcher =
    //      new PatternExplorer(
    //          pattern,
    //          false,
    //          new Location(0, 0, 0),
    //          new RectanglePrioritizer(
    //              pattern.GetTileCenter(new Location(0, 0, 0)),
    //              1.0f));

    //  // Find a ton of tiles.
    //  for (int i = 0; i < size; i++) {
    //    Location loc = canvasSearcher.Next();
    //    Vec2 center = pattern.GetTileCenter(loc);
    //    unusedLocations.Add(loc);
    //  }

    //  //foreach (var tile in terrain.tiles) {
    //  //  unusedLocations.Add(tile.Key, new object());
    //  //}

    //  // 100 attempts to make rooms.
    //  for (int i = 0; i < 100; i++) {
    //    Location startLocation = SetUtils.GetRandom(rand.Next(), unusedLocations);
    //    var roomSearcher =
    //        new PatternExplorer(
    //            pattern,
    //            false,
    //            startLocation,
    //            new OvalPrioritizer(
    //                pattern.GetTileCenter(startLocation),
    //                1.5f));
    //    int minNumTilesInRoom = 6;
    //    int maxNumTilesInRoom = 40;

    //    var roomFloorLocations =
    //        new SortedSet<Location>(
    //            roomSearcher.ExploreWhile(
    //                delegate (Location loc) { return !roomLocations.Contains(loc); },
    //                maxNumTilesInRoom));

    //    var roomBorderLocations =
    //        TerrainUtils.FindBorderLocations(pattern, roomFloorLocations, true);
    //    // Border locations are not floors, but theyre not unused either.
    //    SetUtils.RemoveAll(roomFloorLocations, roomBorderLocations);
    //    SetUtils.RemoveAll(unusedLocations, roomBorderLocations);

    //    if (roomFloorLocations.Count >= minNumTilesInRoom &&
    //        roomFloorLocations.Count <= maxNumTilesInRoom) {
    //      SetUtils.AddAll(roomLocations, roomFloorLocations);
    //      SetUtils.RemoveAll(unusedLocations, roomFloorLocations);

    //      SetUtils.AddAll(borderLocations, roomBorderLocations);
    //      SetUtils.RemoveAll(unusedLocations, roomBorderLocations);

    //      roomByNumber.Add(roomByNumber.Count, new Room(roomFloorLocations));
    //    }
    //  }

    //  // Now that we have a bunch of rooms, let's connect them.

    //  //ConnectRooms(pattern, rand, roomByNumber);

    //  var tiles = context.root.EffectTerrainTileByLocationMutMapCreate();

    //  foreach (var room in roomByNumber.Values) {
    //    foreach (var roomFloorLocation in room.floors) {
    //      var tile =
    //          context.root.EffectTerrainTileCreate(
    //              1, true, "cliff", ITerrainTileComponentMutBunch.New(context.root));
    //      tiles.Add(roomFloorLocation, tile);
    //    }
    //  }

    //  var allTiles = new SortedSet<Location>(tiles.Keys);
    //  var allAdjacent = pattern.GetAdjacentLocations(allTiles, true);
    //  SetUtils.RemoveAll(allAdjacent, allTiles);
    //  foreach (var borderLocation in allAdjacent) {
    //    var tile =
    //        context.root.EffectTerrainTileCreate(
    //            2, false, "cliff", ITerrainTileComponentMutBunch.New(context.root));
    //    tiles.Add(borderLocation, tile);
    //  }

    //  var terrain = context.root.EffectTerrainCreate(pattern, elevationStepHeight, tiles);

    //  TerrainUtils.slopify(terrain, new Vec2(0, -1), 1.2f);

    //  foreach (var room in roomByNumber.Values) {
    //    // To later divide by the number of tiles, to get the average elevation
    //    int elevationsSum = 0;
    //    foreach (var roomFloorLocation in room.floors) {
    //      var tile = tiles[roomFloorLocation];
    //      elevationsSum += tile.elevation;
    //    }
    //    int averageElevation = (int)Math.Ceiling((float)elevationsSum / room.floors.Count);
    //    foreach (var roomFloorLocation in room.floors) {
    //      tiles[roomFloorLocation].elevation = averageElevation;
    //    }
    //  }

    //  TerrainUtils.randify(rand, terrain, 3);

    //  return terrain;
    //}

    

    //static void ConnectRooms(Pattern pattern, Rand rand, SortedDictionary<int, Room> roomByNumber) {
    //  // This function will be adding the corridors to roomByNumber.

    //  SortedDictionary<Location, int> roomNumberByLocation = new SortedDictionary<Location, int>();
    //  foreach (var numberAndRoom in roomByNumber) {
    //    foreach (var roomFloorLocation in numberAndRoom.Value.floors) {
    //      roomNumberByLocation.Add(roomFloorLocation, numberAndRoom.Key);
    //    }
    //  }

    //  // I would just use integers but C# has no typedefs >:(
    //  var regions = new SortedSet<string>();

    //  var regionByRoomNumber = new SortedDictionary<int, String>();
    //  var roomNumbersByRegion = new SortedDictionary<String, SortedSet<int>>();

    //  foreach (var roomNumberAndRoom in roomByNumber) {
    //    int roomNumber = roomNumberAndRoom.Key;
    //    String region = "region" + roomNumber;
    //    regionByRoomNumber.Add(roomNumber, region);
    //    var roomNumbersInRegion = new SortedSet<int>();
    //    roomNumbersInRegion.Add(roomNumber);
    //    roomNumbersByRegion.Add(region, roomNumbersInRegion);
    //    regions.Add(region);
    //    //Logger.Info("Made region " + region);
    //  }

    //  while (true) {
    //    var distinctRegions = new SortedSet<String>(regionByRoomNumber.Values);
    //    //Logger.Info(distinctRegions.Count + " distinct regions!");
    //    if (distinctRegions.Count < 2) {
    //      break;
    //    }
    //    var twoRegions = SetUtils.GetFirstN(distinctRegions, 2);
    //    String regionA = twoRegions[0];
    //    String regionB = twoRegions[1];
    //    //Logger.Info("Will aim to connect regions " + regionA + " and " + regionB);

    //    int regionARoomNumber = SetUtils.GetRandom(rand.Next(), roomNumbersByRegion[regionA]);
    //    var regionARoom = roomByNumber[regionARoomNumber];
    //    var regionALocation = SetUtils.GetRandom(rand.Next(), regionARoom.floors);

    //    int regionBRoomNumber = SetUtils.GetRandom(rand.Next(), roomNumbersByRegion[regionB]);
    //    var regionBRoom = roomByNumber[regionBRoomNumber];
    //    var regionBLocation = SetUtils.GetRandom(rand.Next(), regionBRoom.floors);

    //    // Now lets drive from regionALocation to regionBLocation, and see what happens on the
    //    // way there.
    //    var explorer =
    //        new PatternExplorer(
    //            pattern,
    //            false,
    //            regionALocation,
    //            new LinearPrioritizer(pattern.GetTileCenter(regionBLocation)));
    //    List<Location> path = new List<Location>();
    //    while (true) {
    //      Location currentLocation = explorer.Next();
    //      if (!roomNumberByLocation.ContainsKey(currentLocation)) {
    //        // It means we're in open space, keep going.
    //        path.Add(currentLocation);
    //      } else {
    //        int currentRoomNumber = roomNumberByLocation[currentLocation];
    //        String currentRegion = regionByRoomNumber[currentRoomNumber];
    //        if (currentRegion == regionA) {
    //          // Keep going, but restart the path here.
    //          path = new List<Location>();
    //        } else if (currentRegion != regionA) {
    //          // currentRegionNumber is probably regionBNumber, but isn't necessarily... we could
    //          // have just come across a random other region.
    //          // Either way, we hit something, so we stop now.
    //          break;
    //        }
    //      }
    //    }

    //    String combinedRegion = "region" + regions.Count;
    //    regions.Add(combinedRegion);

    //    int newRoomNumber = roomByNumber.Count;
    //    roomByNumber.Add(newRoomNumber, new Room(new SortedSet<Location>(path)));
    //    foreach (var pathLocation in path) {
    //      roomNumberByLocation.Add(pathLocation, newRoomNumber);
    //    }
    //    regionByRoomNumber.Add(newRoomNumber, combinedRegion);
    //    // We'll fill in regionNumberByRoomNumber and roomNumbersByRegionNumber shortly.

    //    // So, now we have a path that we know connects some regions. However, it might be
    //    // accidentally connecting more than two! It could have grazed past another region without
    //    // us realizing it.
    //    // So now, figure out all the regions that this path touches.

    //    var pathAdjacentLocations = pattern.GetAdjacentLocations(new SortedSet<Location>(path), false);
    //    var pathAdjacentRegions = new SortedSet<String>();
    //    foreach (var pathAdjacentLocation in pathAdjacentLocations) {
    //      if (roomNumberByLocation.ContainsKey(pathAdjacentLocation)) {
    //        int roomNumber = roomNumberByLocation[pathAdjacentLocation];
    //        String region = regionByRoomNumber[roomNumber];
    //        pathAdjacentRegions.Add(region);
    //      }
    //    }

    //    var roomNumbersInCombinedRegion = new SortedSet<int>();
    //    roomNumbersInCombinedRegion.Add(newRoomNumber);
    //    foreach (var pathAdjacentRegion in pathAdjacentRegions) {
    //      if (pathAdjacentRegion == combinedRegion) {
    //        // The new room is already part of this region
    //        continue;
    //      }
    //      foreach (var pathAdjacentRoomNumber in roomNumbersByRegion[pathAdjacentRegion]) {
    //        //Logger.Info("Overwriting " + pathAdjacentRoomNumber + "'s region to " + combinedRegion);
    //        regionByRoomNumber[pathAdjacentRoomNumber] = combinedRegion;
    //        roomNumbersInCombinedRegion.Add(pathAdjacentRoomNumber);
    //      }
    //      roomNumbersByRegion.Remove(pathAdjacentRegion);
    //    }
    //    roomNumbersByRegion.Add(combinedRegion, roomNumbersInCombinedRegion);

    //    String roomNums = "";
    //    foreach (var pathAdjacentRoomNumber in roomNumbersInCombinedRegion) {
    //      if (roomNums != "") {
    //        roomNums = roomNums + ", ";
    //      }
    //      roomNums = roomNums + pathAdjacentRoomNumber;
    //    }
    //    //Logger.Info("Region " + combinedRegion + " now has room numbers: " + roomNums);
    //    roomNumbersByRegion[combinedRegion] = roomNumbersInCombinedRegion;
    //  }
    //}
  }
}
