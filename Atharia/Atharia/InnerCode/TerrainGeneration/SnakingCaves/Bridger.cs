using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class Bridger {
    public const int PATH_BRIDGE_HEIGHT = 5;
    public const int OVERLAY_HEIGHT = 2;
    public const int OVERLAY_BRIDGE_HEIGHT = 2;

    // Returns overlay locs and new bridges
    public static (SortedSet<Location>, List<Bridge>) addBridgesAndOverlay(
        Terrain terrain, bool considerCornersAdjacent, SortedSet<Location> circleLocs, SortedSet<Location> mainLocs, SortedSet<Location> sideLocs) {
      var untestedPossibleBridgeEndLocs = terrain.pattern.GetAdjacentLocations(mainLocs, false, considerCornersAdjacent);
      SetUtils.RemoveAll(untestedPossibleBridgeEndLocs, sideLocs);
      SetUtils.RetainAll(untestedPossibleBridgeEndLocs, circleLocs);

      var locToPossibleBridgeEndingThereMap = new SortedDictionary<Location, List<Bridge>>();
      var locToPossibleBridgesTouchingThereMap = new SortedDictionary<Location, List<Bridge>>();
      foreach (var untestedPossibleBridgeEndLoc in untestedPossibleBridgeEndLocs) {
        var discoveredPossibleBridgesEndingHere =
            BridgeDFS.getBridges(terrain.pattern, circleLocs, mainLocs, sideLocs, untestedPossibleBridgeEndLoc);
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

      var implementedBridges = new List<Bridge>();
      
      var nonEmptyLocs = new SortedSet<Location>(terrain.tiles.Keys);
      var nonEmptyAndAdjacentLocs = terrain.pattern.GetAdjacentLocations(nonEmptyLocs, true, true);
      var overlayLocsPreCulling = new SortedSet<Location>(circleLocs);
      SetUtils.RemoveAll(overlayLocsPreCulling, nonEmptyAndAdjacentLocs);

      var overlayLocs = new SortedSet<Location>();
      
      // Remove any tiny islands we just made (<5 locs)
      var unexploredOverlayLocs = new SortedSet<Location>(overlayLocsPreCulling);
      while (unexploredOverlayLocs.Count > 0) {
        var explorer =
            new AStarExplorer(
                new SortedSet<Location>{ SetUtils.GetFirst(unexploredOverlayLocs) },
                (to) => terrain.pattern.GetAdjacentLocations(to, false),
                (from, to, totalCost) => unexploredOverlayLocs.Contains(to),
                (to) => false, // dont stop early
                (to) => 0, // no goal
                (from, to) => 1); // all steps equal cost
        var connected = explorer.getClosedLocations();
        SetUtils.RemoveAll(unexploredOverlayLocs, connected);
        Asserts.Assert(connected.Count > 0);
        if (connected.Count >= 5) {
          SetUtils.AddAll(overlayLocs, connected);
        }
      }

      
      foreach (var overlayLoc in overlayLocs) {
        if (locToPossibleBridgeEndingThereMap.TryGetValue(overlayLoc, out List<Bridge> bridgesEndingHere)) {
          var newBridge = bridgesEndingHere[0];
          implementBridge(terrain, newBridge);
          implementedBridges.Add(newBridge);
          
          // If any bridge ends or paths in any of these locations, get rid of them.
          var otherBridgeBlastZone = new SortedSet<Location>(newBridge.getAllLocations());
          otherBridgeBlastZone = terrain.pattern.GetAdjacentLocations(otherBridgeBlastZone, true, false);
      
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
            IntertwiningCaveTerrainGenerator.AddTile(terrain, overlayLoc, OVERLAY_HEIGHT);
          }
        }
      }
      return (overlayLocs, implementedBridges);
    }
 
    static void implementBridge(Terrain terrain, Bridge bridge) {
      foreach (var bridgeLoc in bridge.getAllLocations()) {
        if (!terrain.TileExists(bridgeLoc)) {
          IntertwiningCaveTerrainGenerator.AddTile(terrain, bridgeLoc, OVERLAY_BRIDGE_HEIGHT);
        }
      }

      terrain.tiles[bridge.aLoc].elevation = OVERLAY_BRIDGE_HEIGHT;
      terrain.tiles[bridge.dLoc].elevation = OVERLAY_BRIDGE_HEIGHT;
      terrain.tiles[bridge.eLoc].elevation = OVERLAY_BRIDGE_HEIGHT;
      terrain.tiles[bridge.hLoc].elevation = OVERLAY_BRIDGE_HEIGHT;
      
      terrain.tiles[bridge.iLoc].elevation = OVERLAY_BRIDGE_HEIGHT;
      terrain.tiles[bridge.jLoc].elevation = OVERLAY_BRIDGE_HEIGHT;
      terrain.tiles[bridge.kLoc].elevation = OVERLAY_BRIDGE_HEIGHT;
      terrain.tiles[bridge.lLoc].elevation = OVERLAY_BRIDGE_HEIGHT;
      
      terrain.tiles[bridge.mLoc].elevation = PATH_BRIDGE_HEIGHT;
      terrain.tiles[bridge.nLoc].elevation = PATH_BRIDGE_HEIGHT;
      terrain.tiles[bridge.oLoc].elevation = PATH_BRIDGE_HEIGHT;
      terrain.tiles[bridge.pLoc].elevation = PATH_BRIDGE_HEIGHT;
      
      terrain.tiles[bridge.bLoc].elevation = Math.Min(PATH_BRIDGE_HEIGHT, OVERLAY_BRIDGE_HEIGHT);
      terrain.tiles[bridge.cLoc].elevation = Math.Min(PATH_BRIDGE_HEIGHT, OVERLAY_BRIDGE_HEIGHT);
      terrain.tiles[bridge.fLoc].elevation = Math.Min(PATH_BRIDGE_HEIGHT, OVERLAY_BRIDGE_HEIGHT);
      terrain.tiles[bridge.gLoc].elevation = Math.Min(PATH_BRIDGE_HEIGHT, OVERLAY_BRIDGE_HEIGHT);
      terrain.tiles[bridge.bLoc].components.Add(terrain.root.EffectRocksTTCCreate().AsITerrainTileComponent());
      terrain.tiles[bridge.cLoc].components.Add(terrain.root.EffectRocksTTCCreate().AsITerrainTileComponent());
      terrain.tiles[bridge.fLoc].components.Add(terrain.root.EffectRocksTTCCreate().AsITerrainTileComponent());
      terrain.tiles[bridge.gLoc].components.Add(terrain.root.EffectRocksTTCCreate().AsITerrainTileComponent());

    }

  }
}