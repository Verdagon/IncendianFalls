using System;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class EvolvifyImpulseExtensions {
    public static Atharia.Model.Void Destruct(
        this EvolvifyImpulse obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static int GetWeight(this EvolvifyImpulse obj) {
      return obj.weight;
    }

    private static Unit Evolvify(Game game, TerrainTile tile) {
      var plantOrNull = tile.components.GetOnlyIPlantTTCOrNull();
      Asserts.Assert(plantOrNull.Exists());
      if (plantOrNull is LeafTTCAsIPlantTTC) {
        tile.components.Remove(plantOrNull.AsITerrainTileComponent());
        return Leaf.Make(game.root);
      } else if (plantOrNull is LotusTTCAsIPlantTTC) {
        tile.components.Remove(plantOrNull.AsITerrainTileComponent());
        return Lotus.Make(game.root);
      } else if (plantOrNull is FlowerTTCAsIPlantTTC) {
        tile.components.Remove(plantOrNull.AsITerrainTileComponent());
        return Flower.Make(game.root);
      } else if (plantOrNull is RoseTTCAsIPlantTTC) {
        tile.components.Remove(plantOrNull.AsITerrainTileComponent());
        return Rose.Make(game.root);
      }
      Asserts.Assert(false);
      return Unit.Null;
    }

    public static Atharia.Model.Void Enact(
        this EvolvifyImpulse obj,
        Game game,
        Superstate superstate,
        Unit actingUnit) {
      var evolvifyLoc = actingUnit.location;
      var evolvifyTile = game.level.terrain.tiles[evolvifyLoc];
      
      var moveToLocation = obj.moveToLocation;
      Actions.Hop(game, superstate, actingUnit, moveToLocation, true);
      
      var newUnit = Evolvify(game, evolvifyTile);
      game.level.EnterUnit(superstate.levelSuperstate, evolvifyLoc, actingUnit.nextActionTime, newUnit);

      return new Atharia.Model.Void();
    }
  }
}
