using Atharia.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

        foreach (var member in members) {
          switch (member) {
            case "mud":
              tile.components.Add(level.root.EffectMudTTCCreate().AsITerrainTileComponent());
              break;
            case "dirt":
              tile.components.Add(level.root.EffectDirtTTCCreate().AsITerrainTileComponent());
              break;
            case "cave":
              tile.components.Add(level.root.EffectCaveTTCCreate().AsITerrainTileComponent());
              break;
            case "rocks":
              tile.components.Add(level.root.EffectRocksTTCCreate().AsITerrainTileComponent());
              break;
            case "grass":
              tile.components.Add(level.root.EffectGrassTTCCreate().AsITerrainTileComponent());
              break;
            default:
              if (!unknownGeomancy.ContainsKey(location)) {
                unknownGeomancy.Add(location, new List<string>());
              }
              unknownGeomancy[location].Add(member);
              break;
          }
        }
      }
      return unknownGeomancy;
    }
  }
}
