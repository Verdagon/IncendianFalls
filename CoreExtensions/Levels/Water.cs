using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class WaterGeneration {

  //  public static void FillWater(
  //      SSContext context,
  //      Rand rand,
  //      Terrain terrain,
  //      Location fallStartLocation) {
  //    SortedSet<Location> waterLocations = new SortedSet<Location>();

  //    for (int i = 0; ; i++) {
  //      context.logger.Info("Doing iteration!");
  //      SpreadWater(context, rand, terrain, fallStartLocation, waterLocations);

  //      var southernmostEmpty = GetSouthernmostEmptyNeighbor(context, rand, terrain, waterLocations);
  //      if (southernmostEmpty != null &&
  //          terrain.pattern.GetTileCenter(southernmostEmpty).y < 0) {
  //        context.logger.Info("found a southernmost empty: " + southernmostEmpty);
  //        break;
  //      }

  //      var waterLocationsByElevation = GroupByElevation(terrain, waterLocations);

  //      var lowestElevation = GetFirstKey(waterLocationsByElevation);
  //      var lowestElevationLocations = waterLocationsByElevation[lowestElevation];
  //      waterLocationsByElevation.Remove(lowestElevation);
  //      foreach (var location in lowestElevationLocations) {
  //        terrain.tiles[location].elevation++;
  //        AddToSetMultimap(waterLocationsByElevation, lowestElevation + 1, location);
  //        SpreadWater(context, rand, terrain, location, waterLocations);
  //      }

  //      var waterLocationsUnderPressure = new SortedSet<Location>();
  //      foreach (var waterLocation in waterLocations) {
  //        if (IsWaterUnderPressure(context, rand, terrain, waterLocation, waterLocations)) {
  //          waterLocationsUnderPressure.Add(waterLocation);
  //        }
  //      }
  //      foreach (var waterLocation in waterLocationsUnderPressure) {
  //        terrain.tiles[waterLocation].elevation++;
  //        AddToSetMultimap(waterLocationsByElevation, lowestElevation + 1, waterLocation);
  //        SpreadWater(context, rand, terrain, waterLocation, waterLocations);
  //      }
  //    }

  //    foreach (var location in waterLocations) {
  //      var tile = terrain.tiles[location];
  //      tile.classId = "water";
  //    }
  //  }

  //  private static int GetFirstKey(SortedDictionary<int, SortedSet<Location>> map) {
  //    foreach (var thing in map) {
  //      return thing.Key;
  //    }
  //    Asserts.Assert(false);
  //    return 0;
  //  }

  //  private static void AddToSetMultimap(SortedDictionary<int, SortedSet<Location>> map, int key, Location value) {
  //    SortedSet<Location> locs;
  //    if (!map.TryGetValue(key, out locs)) {
  //      locs = new SortedSet<Location>();
  //      map.Add(key, locs);
  //    }
  //    locs.Add(value);
  //  }

  //  private static SortedDictionary<int, SortedSet<Location>> GroupByElevation(
  //      Terrain terrain,
  //      SortedSet<Location> locs) {
  //    var locsByElevation = new SortedDictionary<int, SortedSet<Location>>();
  //    foreach (var location in locs) {
  //      var tile = terrain.tiles[location];
  //      AddToSetMultimap(locsByElevation, tile.elevation, location);
  //    }
  //    return locsByElevation;
  //  }

  //  public static Location GetSouthernmostEmptyNeighbor(
  //      SSContext context,
  //      Rand rand,
  //      Terrain terrain,
  //      SortedSet<Location> waterLocations) {
  //    var adjacentLocs =
  //        terrain.pattern.GetAdjacentLocations(waterLocations, false);

  //    Location southernmostEmptyFound = null;
  //    foreach (var adjacentLoc in adjacentLocs) {
  //      if (!terrain.tiles.ContainsKey(adjacentLoc)) {
  //        southernmostEmptyFound =
  //            GetSouthernmost(terrain.pattern, southernmostEmptyFound, adjacentLoc);
  //      }
  //    }
  //    return southernmostEmptyFound;
  //  }

  //  public static bool IsWaterUnderPressure(
  //      SSContext context,
  //      Rand rand,
  //      Terrain terrain,
  //      Location startLocation,
  //      SortedSet<Location> waterLocations) {
  //    var startTile = terrain.tiles[startLocation];
  //    int numWaterNeighborsNotAboveMe = 0;
  //    foreach (var adjacentLoc in terrain.GetAdjacentExistingLocations(startLocation, false)) {
  //      var adjacentTile = terrain.tiles[adjacentLoc];
  //      if (adjacentTile.elevation <= startTile.elevation) {
  //        if (waterLocations.Contains(adjacentLoc)) {
  //          numWaterNeighborsNotAboveMe++;
  //        }
  //      }
  //    }
  //    return numWaterNeighborsNotAboveMe < 2;
  //  }

  ////public static void PressureSpreadWater(
  ////    SSContext context,
  ////    Rand rand,
  ////    Terrain terrain,
  ////    Location startLocation,
  ////    SortedSet<Location> waterLocations) {
  ////  waterLocations.Add(startLocation);
  ////  var startTile = terrain.tiles[startLocation];


  ////  var waterNeighborsNotAboveMe = new HashSet<Location>();
  ////  var groundNeighborsNotAboveMeByElevation = new SortedDictionary<int, SortedSet<Location>>();

  ////  foreach (var adjacentLoc in terrain.GetAdjacentExistingLocations(startLocation, false)) {
  ////    var adjacentTile = terrain.tiles[adjacentLoc];
  ////    if (adjacentTile.elevation <= startTile.elevation) {
  ////      if (waterLocations.Contains(adjacentLoc)) {
  ////        waterNeighborsNotAboveMe.Add(adjacentLoc);
  ////      } else {
  ////        AddToSetMultimap(groundNeighborsNotAboveMeByElevation, adjacentTile.elevation, adjacentLoc);
  ////      }
  ////    }
  ////  }

  ////  while (numNeighboringWatersNotAboveMe < 2 && groundNeighborsNotAboveMeByElevation.Count > 0) {
  ////    var lowestGroundNeighborsNotAboveMeElevation = GetFirstKey(groundNeighborsNotAboveMeByElevation);
  ////    var lowestGroundNeighborsLocations = groundNeighborsNotAboveMeByElevation[lowestGroundNeighborsNotAboveMeElevation];
  ////    groundNeighborsNotAboveMeByElevation.Remove(lowestGroundNeighborsNotAboveMeElevation);

  ////    foreach (var lowestGroundNeighborLocation in lowestGroundNeighborsLocations) {
  ////      SpreadWater(context, rand, terrain, lowestGroundNeighborLocation, waterLocations);
  ////      numNeighboringWatersNotAboveMe++;
  ////      break;
  ////    }
  ////  }
  ////}

  //public static void SpreadWater(
    //    SSContext context,
    //    Rand rand,
    //    Terrain terrain,
    //    Location startLocation,
    //    SortedSet<Location> waterLocations) {
    //  waterLocations.Add(startLocation);
    //  var startTile = terrain.tiles[startLocation];

    //  var groundNeighborsNotAboveMeByElevation = new SortedDictionary<int, SortedSet<Location>>();

    //  foreach (var adjacentLoc in terrain.GetAdjacentExistingLocations(startLocation, false)) {
    //    var adjacentTile = terrain.tiles[adjacentLoc];
    //    if (adjacentTile.elevation <= startTile.elevation) {
    //      if (!waterLocations.Contains(adjacentLoc)) {
    //        AddToSetMultimap(groundNeighborsNotAboveMeByElevation, adjacentTile.elevation, adjacentLoc);
    //      }
    //    }
    //  }

    //  if (groundNeighborsNotAboveMeByElevation.Count > 0) {
    //    var lowestGroundNeighborsNotAboveMeElevation = GetFirstKey(groundNeighborsNotAboveMeByElevation);
    //    var lowestGroundNeighborsLocations = groundNeighborsNotAboveMeByElevation[lowestGroundNeighborsNotAboveMeElevation];
    //    groundNeighborsNotAboveMeByElevation.Remove(lowestGroundNeighborsNotAboveMeElevation);

    //    foreach (var lowestGroundNeighborLocation in lowestGroundNeighborsLocations) {
    //      SpreadWater(context, rand, terrain, lowestGroundNeighborLocation, waterLocations);
    //      break;
    //    }
    //  }
    //}

    //private static Location GetSouthernmost(Pattern pattern, Location a, Location b) {
    //  if (a == null)
    //    return b;
    //  if (b == null)
    //    return a;
    //  var aPos = pattern.GetTileCenter(a);
    //  var bPos = pattern.GetTileCenter(b);
    //  if (aPos.y < bPos.y) {
    //    return a;
    //  } else {
    //    return b;
    //  }
    //}


  }
}