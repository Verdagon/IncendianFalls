using System;
using System.Collections.Generic;
using System.Text;
using Atharia.Model;

namespace IncendianFalls {
  // AKA the "Vivimap"
  public class MemberToComponentMapper {
    public TerrainTile VivifyTile(Root root, List<String> members) {
      var tile =
          root.EffectTerrainTileCreate(
              NullITerrainTileEvent.Null,
              1, ITerrainTileComponentMutBunch.New(root));

      foreach (var member in members) {
        switch (member) {
          case "Grass":
            tile.components.Add(root.EffectGrassTTCCreate().AsITerrainTileComponent());
            break;
          case "CaveWall":
            tile.components.Add(root.EffectCaveWallTTCCreate().AsITerrainTileComponent());
            break;
          case "Stone":
            tile.components.Add(root.EffectStoneTTCCreate().AsITerrainTileComponent());
            break;
          case "CliffLanding":
            tile.components.Add(root.EffectCliffLandingTTCCreate().AsITerrainTileComponent());
            break;
          case "RavaNest":
            tile.components.Add(root.EffectRavaNestTTCCreate().AsITerrainTileComponent());
            break;
          case "Cliff":
            tile.components.Add(root.EffectCliffTTCCreate().AsITerrainTileComponent());
            break;
          case "Magma":
            tile.components.Add(root.EffectMagmaTTCCreate().AsITerrainTileComponent());
            break;
          case "Falls":
            tile.components.Add(root.EffectFallsTTCCreate().AsITerrainTileComponent());
            break;
          case "ObsidianFloor":
            tile.components.Add(root.EffectObsidianFloorTTCCreate().AsITerrainTileComponent());
            break;
          default:
            throw new Exception("Unknown member: " + member);
        }
      }

      return tile;
    }
  }
}
