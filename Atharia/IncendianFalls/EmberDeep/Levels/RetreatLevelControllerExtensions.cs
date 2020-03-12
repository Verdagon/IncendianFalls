using System;
using System.Collections.Generic;
using IncendianFalls;

namespace Atharia.Model {
  public static class RetreatLevelControllerExtensions {
    public static void LoadLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocationRet,
        out Location exitLocationRet,
        Game game) {
      var terrain =
        CircleTerrainGenerator.Generate(game.root, HexPattern.MakeHexPattern(), game.rand, 18.0f);
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
        Game game,
        Superstate superstate,
        string triggerName) {
      game.root.logger.Info("Got simple trigger: " + triggerName);

      if (triggerName == "levelStart") {
        game.player.hp = game.player.maxHp;
        var sorcerous = game.player.components.GetOnlySorcerousUCOrNull();
        if (sorcerous.Exists()) {
          sorcerous.mp = sorcerous.maxMp;
        }
        foreach (var item in game.player.components.GetAllIItem()) {
          game.player.components.Remove(item.AsIUnitComponent());
          item.Destruct();
        }

        game.AddEvent(
          new ShowOverlayEvent(
            30, // sizePercent
            new Color(0, 0, 0, 224), // backgroundColor
            500, // fadeInEnd
            2000, // fadeOutStart
            2500, // fadeOutEnd,
            "startTalking",

            "Retreat Challenge",
            new Color(255, 255, 255, 255), // textColor
            500, // textFadeInStartS
            1000, // textFadeInEndS
            2000, // textFadeOutStartS
            2500, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new List<Button>()))
          .AsIGameEvent());
      }

      if (triggerName == "startTalking") {
        game.AddEvent(
          new ShowOverlayEvent(
            70, // sizePercent
            new Color(16, 16, 16, 224), // backgroundColor
            300,// fadeInEnd
            0, // fadeOutStart
            0, // fadeOutEnd,
            "",

            "There's a superpowered Draxling chasing you!\n\nIf you just run, it will catch you and kill you.\n\nDistract it with time clones!",
            new Color(255, 255, 255, 255), // textColor
            300, // textFadeInStartS
            600, // textFadeInEndS
            0, // textFadeOutStartS
            0, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new Button[] { new Button("Uh oh!", new Color(32, 32, 32, 255), "startCamera") }))
          .AsIGameEvent());
      }
      if (triggerName == "startCamera") {
        game.AddEvent(
          new FlyCameraEvent(
            new Location(15, 0, 0),
            new Vec3(0, 8, 8),
            1000,
            "cameraReachedPanTo")
          .AsIGameEvent());
      }
      if (triggerName == "cameraReachedPanTo") {
        game.AddEvent(
          new WaitEvent(500, "cameraWaitDone").AsIGameEvent());
      }
      if (triggerName == "cameraWaitDone") {
        game.AddEvent(
          new FlyCameraEvent(
            new Location(-10, 0, 0),
            new Vec3(0, 8, 8),
            1500,
            "showHints")
          .AsIGameEvent());
      }
      if (triggerName == "showHints") {
        game.AddEvent(
          new ShowOverlayEvent(
            70, // sizePercent
            new Color(16, 16, 16, 224), // backgroundColor
            300,// fadeInEnd
            0, // fadeOutStart
            0, // fadeOutEnd,
            "",

            "Keep in mind, enemies usually attack whoever is closest to them.\n\nYour past self should be close enough to distract the enemy, and buy you time to get away!\n\nHint: Try drawing the Draxling away and then reverting before it attacks.",
            new Color(255, 255, 255, 255), // textColor
            300, // textFadeInStartS
            600, // textFadeInEndS
            0, // textFadeOutStartS
            0, // textFadeOutEndS
            true, // topAligned
            true, // leftAligned

            new ButtonImmList(new Button[] { new Button("Got it!", new Color(32, 32, 32, 255), "") }))
          .AsIGameEvent());
      }

      return new Atharia.Model.Void();
    }

    public static Atharia.Model.Void SimpleUnitTrigger(
        this RetreatLevelController obj,
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
