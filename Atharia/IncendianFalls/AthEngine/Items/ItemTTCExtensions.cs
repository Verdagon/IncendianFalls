using Atharia.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Atharia.Model {
  public static class ItemTTCExtensions {
    public static Atharia.Model.Void Destruct(this ItemTTC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static string Interact(
        this ItemTTC itemTTC,
        Game game,
        Superstate superstate,
        Unit interactingUnit,
        Location containingTileLocation) {
      var item = itemTTC.item;
      var tile = game.level.terrain.tiles[containingTileLocation];

      tile.components.Remove(itemTTC.AsITerrainTileComponent());
      itemTTC.Destruct();

      if (item is ArmorAsIItem && interactingUnit.components.GetOnlyArmorOrNull().Exists()) {
        return "You already have armor!";
      } else if (item is GlaiveAsIItem && interactingUnit.components.GetOnlyGlaiveOrNull().Exists()) {
        return "You already have a weapon!";
      } else if (item is SpeedRingAsIItem && interactingUnit.components.GetOnlySpeedRingOrNull().Exists()) {
        return "You already have a speed ring!";
      } else {
        // Give it to the unit.
        interactingUnit.components.Add(item.AsIUnitComponent());

        // After here, item is a borrow reference.

        game.root.logger.Info(item.ToString());

        // Figure out if it was an immediate-use item.
        var itemBunch = IItemStrongMutBunch.New(game.root);
        itemBunch.Add(item);
        var immediatelyUseItem = itemBunch.GetOnlyIImmediatelyUseItemOrNull();
        game.root.logger.Info("immediate? " + immediatelyUseItem.ToString());
        itemBunch.Remove(item);
        itemBunch.Destruct();

        // If the item wants to be immediately used, use it.
        if (immediatelyUseItem.Exists()) {
          game.root.logger.Info("immediately use");
          immediatelyUseItem.Use(game, superstate, interactingUnit);
        }
        game.root.logger.Info("done");
      }

      return "";
    }
  }
}
