using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class RetreatLevelControllerExtensions {
    public static Atharia.Model.Void Destruct(this RetreatLevelController self) {
      self.Delete();
      return new Atharia.Model.Void();
    }

    public static void LoadLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocationRet,
        out Location exitLocationRet,
        SSContext context,
        Game game) {
      var terrain =
        CircleTerrainGenerator.Generate(context, game.root, HexPattern.MakeHexPattern(), game.rand, 18.0f);
      TerrainUtils.randify(game.rand, terrain, 2);
      foreach (var locationAndTile in terrain.tiles) {
        locationAndTile.Value.components.Add(
          game.root.EffectMudTTCCreate().AsITerrainTileComponent());
      }

      level =
        game.root.EffectLevelCreate(
          new Vec3(0, -8, 16),
          terrain,
          game.root.EffectUnitMutSetCreate(),
          NullILevelController.Null,
          game.time);

      levelSuperstate = new LevelSuperstate(level);

      EmberDeepUnitsAndItems.PlaceRocks(game.rand, level, levelSuperstate);

      var entryLocation = new Location(-10, 0, 0);

      var exitLocation = new Location(15, 0, 0);
      level.terrain.tiles[exitLocation].components.Add(game.root.EffectDownStairsTTCCreate().AsITerrainTileComponent());

      var draxling = Draxling.Make(game.root);
      draxling.hp = 300;
      draxling.maxHp = 300;
      draxling.components.Add(game.root.EffectBaseOffenseUCCreate(10, 100 * 100 / 60).AsIUnitComponent());
      // Can see 1000 spaces away.
      draxling.components.Add(game.root.EffectBaseSightRangeUCCreate(1000, 100).AsIUnitComponent());
      level.EnterUnit(
        levelSuperstate,
        new Location(-15, 0, 0),
        level.time,
        draxling);

      level.terrain.tiles[new Location(1, 0, 0)].components.Add(
        game.root.EffectItemTTCCreate(
          game.root.EffectSlowRodCreate().AsIItem())
        .AsITerrainTileComponent());

      level.controller = game.root.EffectRetreatLevelControllerCreate(level).AsILevelController();

      game.levels.Add(level);
      entryLocationRet = entryLocation;
      exitLocationRet = exitLocation;
    }

    public static string GetName(this RetreatLevelController obj) {
      return "Retreat";
    }

    public static bool ConsiderCornersAdjacent(this RetreatLevelController obj) {
      return false;
    }

    public static Atharia.Model.Void SimpleTrigger(
        this RetreatLevelController obj,
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        string triggerName) {
      game.root.logger.Info("Got simple trigger: " + triggerName);

      if (triggerName == "levelStart") {
        game.EnterCinematic();
        game.player.hp = game.player.maxHp;
        var sorcerous = game.player.components.GetOnlySorcerousUCOrNull();
        if (sorcerous.Exists()) {
          sorcerous.mp = sorcerous.maxMp;
        }
        foreach (var item in game.player.components.GetAllIItem()) {
          game.player.components.Remove(item.AsIUnitComponent());
          item.Destruct();
        }

        game.ShowAside("Retreat Challenge");
        game.Wait(2000);
        game.ShowComm("There's a superpowered Draxling chasing you!\n\nIf you just run, it will catch you and kill you.\n\nDistract it with time clones!", "Uh oh!");
        game.FlyCameraTo(1000, new Location(15, 0, 0));
        game.FlyCameraTo(1500, new Location(-10, 0, 0));
        game.ExitCinematic();
        game.ShowComm("Keep in mind, enemies usually attack whoever is closest to them.\n\nYour past self should be close enough to distract the enemy, and buy you time to get away!\n\nHint: Try drawing the Draxling away and then reverting before it attacks.", "Got it!");
      }

      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this RetreatLevelController obj,
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate,
        Unit triggeringUnit,
        Location location,
        string triggerName) {
      game.root.logger.Info("Got simple unit trigger: " + triggerName);
      return new Atharia.Model.Void();
    }
  }
}
