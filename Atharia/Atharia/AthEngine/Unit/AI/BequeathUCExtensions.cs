using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class BequeathUCExtensions {
    public static Atharia.Model.Void Destruct(
        this BequeathUC self) {
      self.Delete();
      return new Atharia.Model.Void();
    }

    private static IItem blueprintNameToItem(Root root, string blueprintName) {
      switch (blueprintName) {
        case "ManaPotion":
          return root.EffectManaPotionCreate().AsIItem();
        case "HealthPotion":
          return root.EffectHealthPotionCreate().AsIItem();
        default:
          Asserts.Assert(false);
          return null;
      }
    }

    public static Atharia.Model.Void BeforeDeath(
        BequeathUC self,
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        Unit unit) {
      var item = blueprintNameToItem(game.root, self.blueprintName);
      game.level.terrain.tiles[unit.location].components.Add(
          game.root.EffectItemTTCCreate(item).AsITerrainTileComponent());
      return new Atharia.Model.Void();
    }
  }
}
