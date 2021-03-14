using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using Atharia.Model;

namespace IncendianFalls {
  public class IntertwiningCaveTerrainGenerator {
    public const int PATH_HEIGHT = 5;
    public const int WATER_HEIGHT = 1;
    
    public static Terrain Generate(
        SSContext context,
        Root root,
        Pattern pattern,
        bool considerCornersAdjacent,
        Rand rand,
        float radius) {
      float elevationStepHeight = .2f;

      var terrain =
          root.EffectTerrainCreate(
              pattern,
              considerCornersAdjacent,
              elevationStepHeight,
              root.EffectTerrainTileByLocationMutMapCreate());

      var originLocation = new Location(0, 0, 0);
      
      // Get a random location in the level's circle.
      var circleLocs = GetCircle(context, pattern, new Location(0, 0, 0), radius);

      var (mainLocs, sideLocs) = SnakeDirector.addSnakes(rand, terrain, considerCornersAdjacent, originLocation, radius, circleLocs);
      
      var pathLocs = new SortedSet<Location>(mainLocs);
      SetUtils.AddAll(pathLocs, sideLocs);
      // foreach (var pathLoc in pathLocs) {
      //   terrain.tiles[pathLoc].components.Add(terrain.root.EffectGrassTTCCreate().AsITerrainTileComponent());
      // }

      var (overlayLocs, bridges) =
          Bridger.addBridgesAndOverlay(terrain, considerCornersAdjacent, circleLocs, mainLocs, sideLocs);

      
      foreach (var circleLoc in circleLocs) {
        if (!terrain.TileExists(circleLoc)) {
          AddTile(terrain, circleLoc, WATER_HEIGHT, terrain.root.EffectWaterTTCCreate().AsITerrainTileComponent());
        }
      }

      overlayLocs = removeCentersOfLargeIslands(rand, terrain, circleLocs, overlayLocs);
      
      var bridgeLocs = new SortedSet<Location>();
      foreach (var bridge in bridges) {
        SetUtils.AddAll(bridgeLocs, bridge.getAllLocations());
      }

      var connections = connectEverything(rand, terrain, circleLocs, pathLocs, overlayLocs, bridgeLocs);
      var connectionLocs = new SortedSet<Location>();
      foreach (var connection in connections) {
        SetUtils.AddAll(connectionLocs, connection.getAllLocations());
      }

      // Now lets noise a bit the tiles that don't much matter for understandability...
      // Don't noise bridges or connections or the tiles next to them. Otherwise, add 0-1 elevation!
      var noiseLocs = new SortedSet<Location>();
      SetUtils.AddAll(noiseLocs, pathLocs);
      SetUtils.AddAll(noiseLocs, overlayLocs);
      
      var bridgeAndAdjacentLocs = terrain.GetAdjacentExistingLocations(bridgeLocs, true, true);
      var connectionAndAdjacentLocs = terrain.GetAdjacentExistingLocations(connectionLocs, true, true);
      SetUtils.RemoveAll(noiseLocs, bridgeAndAdjacentLocs);
      SetUtils.RemoveAll(noiseLocs, connectionAndAdjacentLocs);

      foreach (var noiseLoc in noiseLocs) {
        terrain.tiles[noiseLoc].elevation += rand.Next(0, 1);
      }

      foreach (var loc in terrain.tiles.Keys) {
        if (terrain.tiles[loc].IsWalkable()) {
          // if (terrain.tiles[loc].components.Count == 0) {
             // if (rand.Next() % 100 < 40) {
              terrain.tiles[loc].components.Add(rand.root.EffectGrassTTCCreate().AsITerrainTileComponent());
            // }
          // }
        }
      }
      
      // we'll need to make any locations next to ADEH -1, not just IJKL. See BridgeMaking2.png for an example.

      return terrain;
    }

    static List<RegionConnection> connectEverything(
        Rand rand,
        Terrain terrain,
        SortedSet<Location> circleLocs,
        SortedSet<Location> pathLocs,
        SortedSet<Location> overlayLocs,
        SortedSet<Location> bridgeLocs) {
      // We read from pathLocs instead of the terrain because some of the path locs were lowered
      // as part of the bridges.

      var (regionIdToLocsMap, locToRegionIdMap) = regionize(terrain, pathLocs, overlayLocs);

      var waterLocs = new SortedSet<Location>(circleLocs);
      SetUtils.RemoveAll(waterLocs, pathLocs);
      SetUtils.RemoveAll(waterLocs, overlayLocs);
      SetUtils.RemoveAll(waterLocs, bridgeLocs);

      var locToPossibleConnectionMap = new SortedDictionary<Location, List<RegionConnection>>();
      var regionIdToOtherRegionIdToPossibleConnectionMap =
          new SortedDictionary<int, SortedDictionary<int, List<RegionConnection>>>();
      foreach (var thisLoc in circleLocs) {
        if (!locToRegionIdMap.ContainsKey(thisLoc)) {
          continue;
        }
        
        var possibleConnections =
          RegionConnectorDFS.getRegionConnections(terrain, waterLocs, bridgeLocs, locToRegionIdMap, thisLoc);
        
        var thisRegionId = locToRegionIdMap[thisLoc];
        foreach (var possibleConnection in possibleConnections) {
          var otherRegionId = locToRegionIdMap[possibleConnection.otherRegionLoc];
          
          // Record that we can go from this region to other region with this connection
          if (!regionIdToOtherRegionIdToPossibleConnectionMap.ContainsKey(thisRegionId)) {
            regionIdToOtherRegionIdToPossibleConnectionMap.Add(thisRegionId, new SortedDictionary<int, List<RegionConnection>>());
          }
          if (!regionIdToOtherRegionIdToPossibleConnectionMap[thisRegionId].ContainsKey(otherRegionId)) {
            regionIdToOtherRegionIdToPossibleConnectionMap[thisRegionId]
                .Add(otherRegionId, new List<RegionConnection>());
          }
          regionIdToOtherRegionIdToPossibleConnectionMap[thisRegionId][otherRegionId].Add(possibleConnection);
          
          // Record the inverse; we can go from the other region to this region with this connection
          if (!regionIdToOtherRegionIdToPossibleConnectionMap.ContainsKey(otherRegionId)) {
            regionIdToOtherRegionIdToPossibleConnectionMap.Add(otherRegionId, new SortedDictionary<int, List<RegionConnection>>());
          }
          if (!regionIdToOtherRegionIdToPossibleConnectionMap[otherRegionId].ContainsKey(thisRegionId)) {
            regionIdToOtherRegionIdToPossibleConnectionMap[otherRegionId]
                .Add(thisRegionId, new List<RegionConnection>());
          }
          regionIdToOtherRegionIdToPossibleConnectionMap[otherRegionId][thisRegionId].Add(possibleConnection);
          
          // Now for every location that this connection touches, note that that location has a possible connection here
          foreach (var loc in possibleConnection.getAllLocations()) {
            if (!locToPossibleConnectionMap.ContainsKey(loc)) {
              locToPossibleConnectionMap.Add(loc, new List<RegionConnection>(possibleConnections));
            }
            locToPossibleConnectionMap[loc].AddRange(possibleConnections);
          }
        }
      }

      var regionIdToRealmIdMap = new SortedDictionary<int, string>();
      for (int regionId = 0; regionId < regionIdToLocsMap.Count; regionId++) {
        var realmId = "realm" + regionId; 
        regionIdToRealmIdMap.Add(regionId, realmId);
      }

      var numOriginalRegions = regionIdToLocsMap.Count;

      var implementedConnections = new List<RegionConnection>();
      var allAlreadyConnectionLocs = new SortedSet<Location>();
      
      while (new SortedSet<string>(regionIdToRealmIdMap.Values).Count > 1) {
        for (int thisRegionId = 0; ; thisRegionId++) {
          if (thisRegionId == numOriginalRegions) {
            // if we got here without being able to join anything to anything else, then we're in trouble,
            // some regions cant be connected.
            Asserts.Assert(false);
          }
          
          var thisRealmId = regionIdToRealmIdMap[thisRegionId];

          if (regionIdToOtherRegionIdToPossibleConnectionMap.TryGetValue(
              thisRegionId, out SortedDictionary<int, List<RegionConnection>> otherRegionIdToPossibleConnectionMap)) {
            var otherRegionIdsWithPossibleConnections = otherRegionIdToPossibleConnectionMap.Keys;
          
            var otherRealmRegionIdsWithPossibleConnections = new SortedSet<int>();
            foreach (var otherRegionIdWithPossibleConnections in otherRegionIdsWithPossibleConnections) {
              if (thisRealmId != regionIdToRealmIdMap[otherRegionIdWithPossibleConnections]) {
                otherRealmRegionIdsWithPossibleConnections.Add(otherRegionIdWithPossibleConnections);
              }
            }
            if (otherRealmRegionIdsWithPossibleConnections.Count == 0) {
              // We may have maxed out our possible ways to connect this region with anything.
              // Try again with the next region.
              continue;
            }
            var otherRegionId = SetUtils.GetFirst(otherRealmRegionIdsWithPossibleConnections);
            var possibleConnectionsFromThisRegionToThatRegion =
                otherRegionIdToPossibleConnectionMap[otherRegionId];
            var connection = ListUtils.GetRandomN(possibleConnectionsFromThisRegionToThatRegion, rand, 1, 1)[0];

            foreach (var loc in connection.getAllLocations()) {
              Asserts.Assert(!allAlreadyConnectionLocs.Contains(loc));
            }
            SetUtils.AddAll(allAlreadyConnectionLocs, connection.getAllLocations());
            
            implementConnection(
                terrain, regionIdToLocsMap, locToRegionIdMap, regionIdToRealmIdMap, locToPossibleConnectionMap,
                regionIdToOtherRegionIdToPossibleConnectionMap, connection);
            implementedConnections.Add(connection);
            // We connected our realm to another realm!
            // Revisit whether we need to continue with this entire endeavor. We might only have one realm left.
            break;
          }
        }
      }

      return implementedConnections;
    }

    static void implementConnection(
        Terrain terrain,
        List<SortedSet<Location>> regionIdToLocsMap,
        SortedDictionary<Location, int> locToRegionIdMap,
        SortedDictionary<int, string> regionIdToRealmIdMap,
        SortedDictionary<Location, List<RegionConnection>> locToPossibleConnectionMap,
        SortedDictionary<int, SortedDictionary<int, List<RegionConnection>>> regionIdToOtherRegionIdToPossibleConnectionMap,
        RegionConnection connection) {
      var thisRegionLocElevation = terrain.tiles[connection.thisRegionLoc].elevation;
      var otherRegionLocElevation = terrain.tiles[connection.otherRegionLoc].elevation;
      var elevationDirection = Math.Sign(otherRegionLocElevation - thisRegionLocElevation);
      
      var firstStepTile = terrain.tiles[connection.firstWaterStepLoc];
      var firstStepWater = firstStepTile.components.GetOnlyWaterTTCOrNull();
      firstStepTile.components.Remove(firstStepWater.AsITerrainTileComponent());
      firstStepWater.Destruct();
      // firstStepTile.components.Add(terrain.root.EffectGrassTTCCreate().AsITerrainTileComponent());
      // firstStepTile.components.Add(terrain.root.EffectFireTTCCreate().AsITerrainTileComponent());
      firstStepTile.elevation = thisRegionLocElevation + elevationDirection;
      
      var secondStepTile = terrain.tiles[connection.secondWaterStepLoc];
      var secondStepWater = secondStepTile.components.GetOnlyWaterTTCOrNull();
      secondStepTile.components.Remove(secondStepWater.AsITerrainTileComponent());
      secondStepWater.Destruct();
      // secondStepTile.components.Add(terrain.root.EffectGrassTTCCreate().AsITerrainTileComponent());
      // secondStepTile.components.Add(terrain.root.EffectFireTTCCreate().AsITerrainTileComponent());
      secondStepTile.elevation = thisRegionLocElevation + elevationDirection * 2;
      
      var thisRegionId = locToRegionIdMap[connection.thisRegionLoc];
      var thisRealmId = regionIdToRealmIdMap[thisRegionId];
      var otherRegionId = locToRegionIdMap[connection.otherRegionLoc];
      var otherRealmId = regionIdToRealmIdMap[otherRegionId];

      // Add a new region in a new realm
      var newRegionId = regionIdToLocsMap.Count;
      regionIdToLocsMap.Add(new SortedSet<Location>{connection.firstWaterStepLoc, connection.secondWaterStepLoc});
      locToRegionIdMap.Add(connection.firstWaterStepLoc, newRegionId);
      locToRegionIdMap.Add(connection.secondWaterStepLoc, newRegionId);
      var newRealmId = "realm" + newRegionId;
      regionIdToRealmIdMap.Add(newRegionId, newRealmId);

      // Join the two existing realms to be part of the new realm
      foreach (var regionId in new List<int>(regionIdToRealmIdMap.Keys)) {
        if (regionIdToRealmIdMap[regionId] == thisRealmId || regionIdToRealmIdMap[regionId] == otherRealmId) {
          regionIdToRealmIdMap[regionId] = newRealmId;
        }
      }
      
      // Remove all bridge possibilities that would overlap with this one.
      // (This also removes the current actual connection we're implementing)
      foreach (var overlappingLoc in connection.getAllLocations()) {
        var overlappingLocPossibleConnections = new List<RegionConnection>(locToPossibleConnectionMap[overlappingLoc]);
        foreach (var overlappingPossibleConnection in overlappingLocPossibleConnections) {
          // Now we remove this connection!
          foreach (var overlappingPossibleConnectionLoc in overlappingPossibleConnection.getAllLocations()) {
            locToPossibleConnectionMap[overlappingPossibleConnectionLoc].Remove(overlappingPossibleConnection);
          }
          var overlappingPossibleConnectionStartRegionId =
              locToRegionIdMap[overlappingPossibleConnection.thisRegionLoc];
          var overlappingPossibleConnectionEndRegionId =
              locToRegionIdMap[overlappingPossibleConnection.otherRegionLoc];
          regionIdToOtherRegionIdToPossibleConnectionMap[overlappingPossibleConnectionStartRegionId]
              [overlappingPossibleConnectionEndRegionId]
              .Remove(overlappingPossibleConnection);
        }
      }
    }

    static (List<SortedSet<Location>>, SortedDictionary<Location, int>) regionize(
        Terrain terrain,
        SortedSet<Location> pathLocs,
        SortedSet<Location> overlayLocs) {
      var unregionedPathLocs = new SortedSet<Location>(pathLocs);

      var regionIdToLocsMap = new List<SortedSet<Location>>();

      while (unregionedPathLocs.Count > 0) {
        var explorer =
            new AStarExplorer(
                new SortedSet<Location>{ SetUtils.GetFirst(unregionedPathLocs) },
                (to) => terrain.pattern.GetAdjacentLocations(to, false),
                (from, to, totalCost) => pathLocs.Contains(to),
                (to) => false, // dont stop early
                (to) => 0, // no goal
                (from, to) => 1); // all steps equal cost
        var connected = explorer.getClosedLocations();
        Asserts.Assert(connected.Count > 0);
        regionIdToLocsMap.Add(connected);
        SetUtils.RemoveAll(unregionedPathLocs, connected);
      }
      
      var unregionedOverlayLocs = new SortedSet<Location>(overlayLocs);
      
      while (unregionedOverlayLocs.Count > 0) {
        var explorer =
            new AStarExplorer(
                new SortedSet<Location>{ SetUtils.GetFirst(unregionedOverlayLocs) },
                (to) => terrain.pattern.GetAdjacentLocations(to, false),
                (from, to, totalCost) => overlayLocs.Contains(to),
                (to) => false, // dont stop early
                (to) => 0, // no goal
                (from, to) => 1); // all steps equal cost
        var connected = explorer.getClosedLocations();
        Asserts.Assert(connected.Count > 0);
        regionIdToLocsMap.Add(connected);
        SetUtils.RemoveAll(unregionedOverlayLocs, connected);
      }

      var locToRegionIdMap = new SortedDictionary<Location, int>();
      for (int regionId = 0; regionId < regionIdToLocsMap.Count; regionId++) {
        var regionLocs = regionIdToLocsMap[regionId];
        foreach (var loc in regionLocs) {
          locToRegionIdMap.Add(loc, regionId);
        }
      }

      return (regionIdToLocsMap, locToRegionIdMap);
    }

    static SortedSet<Location> removeCentersOfLargeIslands(
        Rand rand, Terrain terrain, SortedSet<Location> circleLocs, SortedSet<Location> overlayLocs) {
      var overlayLocsNew = new SortedSet<Location>(overlayLocs);
      
      var nonOverlayLocs = new SortedSet<Location>(circleLocs);
      SetUtils.RemoveAll(nonOverlayLocs, overlayLocs);
      var explorer =
          new AStarExplorer(
              nonOverlayLocs,
              (to) => terrain.pattern.GetAdjacentLocations(to, true),
              (a, b, totalCost) => overlayLocs.Contains(b),
              (b) => false, // Dont stop early
              (a) => 0, // No goal
              (a, b) => 1); // Each step costs 1
      foreach (var loc in explorer.getClosedLocations()) {
        var tile = terrain.tiles[loc];
        if (explorer.GetCostTo(loc) <= 1.5) {
          // do nothing
        } else if (explorer.GetCostTo(loc) <= 2.5) {
          // randomly put stalagmites in occasionally
          // if (rand.Next() % 5 == 0) {
          //   tile.components.Add(terrain.root.EffectTreeTTCCreate().AsITerrainTileComponent());
          // }
        } else {
          // foreach (var mud in terrain.tiles[loc].components.GetAllGrassTTC()) {
          //   tile.components.Remove(mud.AsITerrainTileComponent());
          //   mud.Destruct();
          // }
          tile.components.Add(terrain.root.EffectWaterTTCCreate().AsITerrainTileComponent());
          tile.elevation = WATER_HEIGHT;
          overlayLocsNew.Remove(loc);
        }
      }

      return overlayLocsNew;
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

    public static TerrainTile AddTile(Terrain terrain, Location location, int elevation, params ITerrainTileComponent[] components) {
      var tile =
        terrain.root.EffectTerrainTileCreate(
              NullITerrainTileEvent.Null,
              elevation,
              ITerrainTileComponentMutBunch.New(terrain.root));
      foreach (var component in components) {
        tile.components.Add(component);
      }
      terrain.tiles.Add(location, tile);
      return tile;
    }
  }
}
