using Atharia.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace IncendianFalls {
  class Vivifier {
    public static Dictionary<ElevatedLocation, List<string>> ParseGeomancy(string levelContents) {
      var membersByLocation = new Dictionary<ElevatedLocation, List<string>>();
      foreach (var untrimmedLine in levelContents.Split('\n')) {
        var line = untrimmedLine.Trim();
        if (line == "") {
          continue;
        }

        var parts = line.Split(' ');
        int groupX = int.Parse(parts[0]);
        int groupY = int.Parse(parts[1]);
        int indexInGroup = int.Parse(parts[2]);
        int elevation = int.Parse(parts[3]);

        var location = new Location(groupX, groupY, indexInGroup);
        var elevatedLocation = new ElevatedLocation(location, elevation);

        var members = new List<string>();
        for (int i = 4; i < parts.Length; i++) {
          members.Add(parts[i]);
        }
        membersByLocation.Add(elevatedLocation, members);
      }
      return membersByLocation;
    }

    //public static (Dictionary<ElevatedLocation, List<string>>, List<ElevatedLocation>)
    //    FindLocationsWithMember(Dictionary<ElevatedLocation, List<string>> geomancy, string needle) {
    //  var remainingGeomancy = new Dictionary<ElevatedLocation, List<string>>();
    //  var foundLocations = new List<ElevatedLocation>();
    //  foreach (var elevatedLocationAndMembers in geomancy) {
    //    var elevatedLocation = elevatedLocationAndMembers.Key;
    //    var location = elevatedLocation.location;
    //    var elevation = elevatedLocation.elevation;
    //    var members = elevatedLocationAndMembers.Value;

    //    var remainingMembers = members;
    //    foreach (var member in members) {
    //      if (member == needle) {

    //      }
    //    }
    //  }
    //}

    public static Location ExtractLocation(
        Dictionary<Location, List<string>> geomancy,
        string needle) {
      var locations = ExtractLocations(geomancy, needle);
      if (locations.Count > 1) {
        throw new Exception("Too many '" + needle + "' (" + locations.Count + ") in geomancy!");
      }
      if (locations.Count == 0) {
        throw new Exception("Couldnt find '" + needle + "' in geomancy!");
      }
      return locations[0];
    }

    public static List<Location> ExtractLocations(
        Dictionary<Location, List<string>> geomancy,
        string needle) {
      var locations = new List<Location>();

      foreach (var location in new List<Location>(geomancy.Keys)) {
        var members = geomancy[location];
        var remainingMembers = new List<string>();
        foreach (var member in members) {
          if (member == needle) {
            locations.Add(location);
          } else {
            remainingMembers.Add(member);
          }
        }
        if (remainingMembers.Count == 0) {
          geomancy.Remove(location);
        } else {
          geomancy[location] = remainingMembers;
        }
      }

      return locations;
    }

    public static string PrintMembers(
        Dictionary<Location, List<string>> geomancy) {
      string output = "";
      foreach (var locationAndMembers in geomancy) {
        var location = locationAndMembers.Key;
        var members = locationAndMembers.Value;
        Asserts.Assert(members.Count > 0);
        output += location.groupX + ", " + location.groupY + ", " + location.indexInGroup + ":";
        foreach (var member in members) {
          output += " " + member;
        }
        output += "\n";
      }
      return output;
    }

    public static Dictionary<Location, List<string>>
    Vivify(
        Level level,
        LevelSuperstate levelSuperstate,
        Dictionary<ElevatedLocation, List<string>> geomancy) {
      var unknownGeomancy = new Dictionary<Location, List<string>>();
      foreach (var elevatedLocationAndMembers in geomancy) {
        var elevatedLocation = elevatedLocationAndMembers.Key;
        var location = elevatedLocation.location;
        var elevation = elevatedLocation.elevation;
        var members = elevatedLocationAndMembers.Value;

        var tile =
          level.root.EffectTerrainTileCreate(
            elevation, ITerrainTileComponentMutBunch.New(level.root));
        level.terrain.tiles.Add(location, tile);

        foreach (var memberString in members) {
          var recognized = DoMemberStringThing(level, levelSuperstate, location, tile, memberString);
          if (!recognized) {
            if (!unknownGeomancy.ContainsKey(location)) {
              unknownGeomancy.Add(location, new List<string>());
            }
            unknownGeomancy[location].Add(memberString);
          }
        }
      }
      return unknownGeomancy;
    }

    private static bool DoMemberStringThing(Level level, LevelSuperstate levelSuperstate, Location location, TerrainTile tile, string memberString) {
      switch (memberString) {
        case "Mud":
          tile.components.Add(level.root.EffectMudTTCCreate().AsITerrainTileComponent());
          return true;
        case "Dirt":
          tile.components.Add(level.root.EffectDirtTTCCreate().AsITerrainTileComponent());
          return true;
        case "Cave":
          tile.components.Add(level.root.EffectCaveTTCCreate().AsITerrainTileComponent());
          return true;
        case "Rocks":
          tile.components.Add(level.root.EffectRocksTTCCreate().AsITerrainTileComponent());
          return true;
        case "Grass":
          tile.components.Add(level.root.EffectGrassTTCCreate().AsITerrainTileComponent());
          return true;
        case "Floor":
          tile.components.Add(level.root.EffectFloorTTCCreate().AsITerrainTileComponent());
          return true;
        case "Tree":
          tile.components.Add(level.root.EffectTreeTTCCreate().AsITerrainTileComponent());
          return true;
        case "Water":
          tile.components.Add(level.root.EffectWaterTTCCreate().AsITerrainTileComponent());
          return true;
        case "Ravashrike":
          AddRavashrike(level, levelSuperstate, location);
          return true;
      }

      // Put quotes back in once we have a nice way to load levels from files, so we dont have
      // to hardcode them in strings, which quotes dont do well in.
      //Match triggerMatch = Regex.Match(memberString, "^Trigger\\(\"([A-Za-z0-9_]+)\"\\)$", RegexOptions.IgnoreCase);
      Match triggerMatch = Regex.Match(memberString, "^Trigger\\(([A-Za-z0-9_]+)\\)$", RegexOptions.IgnoreCase);
      if (triggerMatch.Success) {
        string name = triggerMatch.Groups[1].Value;
        tile.components.Add(level.root.EffectSimplePresenceTriggerTTCCreate(name).AsITerrainTileComponent());
        return true;
      }

      // Put quotes back in once we have a nice way to load levels from files, so we dont have
      // to hardcode them in strings, which quotes dont do well in.
      //Match markerMatch = Regex.Match(memberString, "^Marker\\(\"([A-Za-z0-9_]+)\"\\)$", RegexOptions.IgnoreCase);
      Match markerMatch = Regex.Match(memberString, "^Marker\\(([A-Za-z0-9_]+)\\)$", RegexOptions.IgnoreCase);
      if (markerMatch.Success) {
        string name = markerMatch.Groups[1].Value;
        tile.components.Add(level.root.EffectMarkerTTCCreate(name).AsITerrainTileComponent());
        return true;
      }

      return false;
    }

    public static void AddRavashrike(Level level, LevelSuperstate levelSuperstate, Location location) {
      var components = IUnitComponentMutBunch.New(level.root);
      components.Add(level.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(level.root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      components.Add(level.root.EffectBideAICapabilityUCCreate(0).AsIUnitComponent());
      Unit enemy =
          level.root.EffectUnitCreate(
              level.root.EffectIUnitEventMutListCreate(),
              true,
              0,
              location,
              "Ravashrike",
              600, 600,
              100, 100,
              250,
              level.time,
              components,
              false,
              14);
      level.EnterUnit(levelSuperstate, enemy, location);
    }
    public static void AddIrkling(Level level, LevelSuperstate levelSuperstate, Location location) {
      var components = IUnitComponentMutBunch.New(level.root);
      components.Add(level.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
      components.Add(level.root.EffectAttackAICapabilityUCCreate(KillDirective.Null).AsIUnitComponent());
      Unit enemy =
          level.root.EffectUnitCreate(
              level.root.EffectIUnitEventMutListCreate(),
              true,
              0,
              location,
              "Irkling",
              4, 4,
              0, 0,
              600,
              level.time,
              components,
              false,
              4);
      level.EnterUnit(levelSuperstate, enemy, location);
    }
  }
}
