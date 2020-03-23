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
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        Unit interactingUnit,
        Location containingTileLocation) {
      var item = itemTTC.item;
      var tile = game.level.terrain.tiles[containingTileLocation];

      tile.components.Remove(itemTTC.AsITerrainTileComponent());
      itemTTC.Destruct();

      if (item is ArmorAsIItem && interactingUnit.components.GetOnlyArmorOrNull().Exists()) {
        item.Destruct();
        return "You already have armor!";
      } else if (item is GlaiveAsIItem && interactingUnit.components.GetOnlyGlaiveOrNull().Exists()) {
        item.Destruct();
        return "You already have a weapon!";
      } else if (item is SpeedRingAsIItem && interactingUnit.components.GetOnlySpeedRingOrNull().Exists()) {
        item.Destruct();
        return "You already have a speed ring!";
      }

      // Give it to the unit.
      interactingUnit.components.Add(item.AsIUnitComponent());

      // After here, item is a borrow reference.

      // Figure out if it was an immediate-use item.
      var itemBunch = IItemStrongMutBunch.New(game.root);
      itemBunch.Add(item);
      var immediatelyUseItem = itemBunch.GetOnlyIImmediatelyUseItemOrNull();
      var pickUpReactorItem = itemBunch.GetOnlyIPickUpReactorItemOrNull();
      game.root.logger.Info("immediate? " + immediatelyUseItem.ToString());
      itemBunch.Remove(item);
      itemBunch.Destruct();

      // If the item wants to react on pickup, react.
      if (pickUpReactorItem.Exists()) {
        pickUpReactorItem.ReactToPickUp(game, superstate, interactingUnit);
      }

      // If the item wants to be immediately used, use it.
      if (immediatelyUseItem.Exists()) {
        immediatelyUseItem.Use(game, superstate, interactingUnit);
      }
      game.root.logger.Info("done");

      return "";
    }
  }
}
