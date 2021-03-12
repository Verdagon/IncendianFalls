using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class RegionConnection {
    public readonly Location thisRegionLoc;
    public readonly Location firstWaterStepLoc;
    public readonly Location secondWaterStepLoc;
    public readonly Location otherRegionLoc;

    public RegionConnection(
        Location thisRegionLoc,
        Location firstWaterStepLoc,
        Location secondWaterStepLoc,
        Location otherRegionLoc) {
      this.thisRegionLoc = thisRegionLoc;
      this.firstWaterStepLoc = firstWaterStepLoc;
      this.secondWaterStepLoc = secondWaterStepLoc;
      this.otherRegionLoc = otherRegionLoc;
    }

    public SortedSet<Location> getAllLocations() {
      return new SortedSet<Location> {thisRegionLoc, firstWaterStepLoc, secondWaterStepLoc, otherRegionLoc};
    }
  }
  
  public class RegionConnectorDFS {
    private readonly Terrain terrain;
    private readonly SortedSet<Location> waterLocs;
    private readonly SortedSet<Location> bridgeLocs;
    private readonly SortedDictionary<Location, int> locToRegionIdMap;

    RegionConnectorDFS(
        Terrain terrain,
        SortedSet<Location> waterLocs,
        SortedSet<Location> bridgeLocs,
        SortedDictionary<Location, int> locToRegionIdMap) {
      this.terrain = terrain;
      this.waterLocs = waterLocs;
      this.bridgeLocs = bridgeLocs;
      this.locToRegionIdMap = locToRegionIdMap;
    }

    // See BridgeMaking.png for what points A-H are 
    public static List<RegionConnection> getRegionConnections(
        Terrain terrain,
        SortedSet<Location> waterLocs,
        SortedSet<Location> bridgeLocs,
        SortedDictionary<Location, int> locToRegionIdMap,
        Location thisRegionLoc) {
      var results = new List<RegionConnection>();
      new RegionConnectorDFS(terrain, waterLocs, bridgeLocs, locToRegionIdMap)
          .CheckThisRegionLocAndFindRest(results, thisRegionLoc);
      return results;
    }

    void CheckThisRegionLocAndFindRest(List<RegionConnection> results, Location thisRegionLoc) {
      if (waterLocs.Contains(thisRegionLoc)) {
        return;
      }
      if (bridgeLocs.Contains(thisRegionLoc)) {
        return;
      }
      FindFirstWaterStep(results, thisRegionLoc);
    }

    void FindFirstWaterStep(List<RegionConnection> results, Location thisRegionLoc) {
      var firstWaterStepLocs = new SortedSet<Location>(terrain.GetAdjacentExistingLocations(thisRegionLoc, false));
      SetUtils.RemoveAll(firstWaterStepLocs, new SortedSet<Location>{thisRegionLoc});
      foreach (var firstWaterStepLoc in firstWaterStepLocs) {
        if (!waterLocs.Contains(firstWaterStepLoc)) {
          continue;
        }
        FindSecondWaterStep(results, thisRegionLoc, firstWaterStepLoc);
      }
    }

    void FindSecondWaterStep(List<RegionConnection> results, Location thisRegionLoc, Location firstWaterStepLoc) {
      var secondWaterStepLocs = new SortedSet<Location>(terrain.GetAdjacentExistingLocations(firstWaterStepLoc, false));
      SetUtils.RemoveAll(secondWaterStepLocs, new SortedSet<Location>{thisRegionLoc, firstWaterStepLoc});
      foreach (var secondWaterStepLoc in secondWaterStepLocs) {
        if (!waterLocs.Contains(secondWaterStepLoc)) {
          continue;
        }
        FindOtherRegionLoc(results, thisRegionLoc, firstWaterStepLoc, secondWaterStepLoc);
      }
    }

    void FindOtherRegionLoc(List<RegionConnection> results, Location thisRegionLoc, Location firstWaterStepLoc, Location secondWaterStepLoc) {
      var otherRegionLocs = new SortedSet<Location>(terrain.GetAdjacentExistingLocations(secondWaterStepLoc, false));
      SetUtils.RemoveAll(otherRegionLocs, new SortedSet<Location>{thisRegionLoc, firstWaterStepLoc, secondWaterStepLoc});
      foreach (var otherRegionLoc in otherRegionLocs) {
        if (!locToRegionIdMap.ContainsKey(otherRegionLoc)) {
          continue;
        }
        var thisLocRegionId = locToRegionIdMap[thisRegionLoc];
        var otherLocRegionId = locToRegionIdMap[otherRegionLoc];
        if (thisLocRegionId == otherLocRegionId) {
          continue;
        }

        var thisRegionLocElevation = terrain.tiles[thisRegionLoc].elevation;
        var otherRegionLocElevation = terrain.tiles[otherRegionLoc].elevation;
        var elevationDifference = Math.Abs(thisRegionLocElevation - otherRegionLocElevation);
        // Because the two intermediate steps can only span 3 elevation difference.
        Asserts.Assert(elevationDifference <= 3);
        
        var result = new RegionConnection(thisRegionLoc, firstWaterStepLoc, secondWaterStepLoc, otherRegionLoc);
        results.Add(result);
      }
    }
  }
}
