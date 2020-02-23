using System;
using System.Collections.Generic;
using System.Text;
using Atharia.Model;

namespace IncendianFalls {
  // AKA the "Vivimap"
  public class MemberToComponentMapper {
    public Atharia.Model.TerrainTile VivifyTile(Root root, List<String> members) {
      var tile =
          root.EffectTerrainTileCreate(
              1, ITerrainTileComponentMutBunch.New(root));

      foreach (var member in members) {
        switch (member) {
          case "grass":
            tile.components.Add(root.EffectGrassTTCCreate().AsITerrainTileComponent());
            break;
          case "stone":
            tile.components.Add(root.EffectStoneTTCCreate().AsITerrainTileComponent());
            break;
          case "clifflanding":
            tile.components.Add(root.EffectCliffLandingTTCCreate().AsITerrainTileComponent());
            break;
          case "ravanest":
            tile.components.Add(root.EffectRavaNestTTCCreate().AsITerrainTileComponent());
            break;
          case "cliff":
            tile.components.Add(root.EffectCliffTTCCreate().AsITerrainTileComponent());
            break;
          case "magma":
            tile.components.Add(root.EffectMagmaTTCCreate().AsITerrainTileComponent());
            break;
          case "falls":
            tile.components.Add(root.EffectFallsTTCCreate().AsITerrainTileComponent());
            break;
          default:
            throw new Exception("Unknown member: " + member);
            break;
        }
      }

      return tile;
    }
  }
}
