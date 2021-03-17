using Atharia.Model;
using IncendianFalls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Atharia.Model {
  static class RavaArcanaLevelLinkerTTCExtensions {
    public static Atharia.Model.Void Destruct(this RavaArcanaLevelLinkerTTC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static string Interact(
      this RavaArcanaLevelLinkerTTC linker,
      SSContext context,
      Game game,
      Superstate superstate,
      Unit interactingUnit,
      Location containingTileLocation) {

      Asserts.Assert(game.level.units.Contains(game.player));

      // Replace this linker with a regular LevelLink.
      var nextLevelDepth = linker.nextLevelDepth;
      game.level.terrain.tiles[containingTileLocation].components.Remove(linker.AsITerrainTileComponent());
      linker.Destruct();

      MakeNextLevel(
          out var nextLevel,
          out var nextLevelSuperstate,
          out var nextLevelEntryLocation,
          context,
          game,
          superstate,
          nextLevelDepth,
          game.squareLevelsOnly);

      // Link to the next level.
      var levelLink = game.root.EffectLevelLinkTTCCreate(true, nextLevel, nextLevelEntryLocation);
      game.level.terrain.tiles[containingTileLocation].components.Add(levelLink.AsITerrainTileComponent());

      //// Make the next level link back to here.
      //var nextLevelEntryTile = nextLevel.terrain.tiles[nextLevelEntryLocation];
      //nextLevelEntryTile.components.Add(
      //  game.root.EffectLevelLinkTTCCreate(false, game.level, containingTileLocation).AsITerrainTileComponent());

      if (nextLevelDepth == 0) {
        if (game.player.Exists()) {
          game.player.hp = game.player.maxHp;
          var sorcerous = game.player.components.GetOnlySorcerousUCOrNull();
          if (sorcerous.Exists()) {
            sorcerous.mp = sorcerous.maxMp;
          }
          foreach (var item in game.player.components.GetAllIItem()) {
            game.player.components.Remove(item.AsIUnitComponent());
            item.Destruct();
          }
        }
      }

      context.Flare(context.root.GetDeterministicHashCode().ToString());

      // Travel the level link, to switch levels.
      return levelLink.Interact(context, game, superstate, interactingUnit, containingTileLocation);
    }

    public static void MakeNextLevel(
        out Level level,
        out LevelSuperstate levelSuperstate,
        out Location entryLocation,
        SSContext context,
        Game game,
        Superstate superstate,
        int depth,
        bool squareLevelsOnly) {
      game.root.logger.Info("in MakeNextLevel! depth " + depth);
      context.Flare(context.root.GetDeterministicHashCode().ToString());
      if (depth == 0) {
        // Heal the player before they go onto the first level.
        var sorcerous = game.player.components.GetOnlySorcerousUCOrNull();
        if (sorcerous.Exists()) {
          sorcerous.mp = sorcerous.maxMp;
        }
        game.player.hp = game.player.maxHp;
      }
      context.Flare(game.root.GetDeterministicHashCode().ToString());
      switch (depth) {
        case 0:
        case 2:
        case 4:
        case 6:
        default:
          JumpingCaveLevelControllerExtensions.LoadLevel(
            out level,
            out levelSuperstate,
            out entryLocation,
            out SortedSet<Location> openLowerLocs,
            out SortedSet<Location> openUpperLocs,
            context,
            game,
            superstate,
            depth,
            squareLevelsOnly);
          context.Flare(game.root.GetDeterministicHashCode().ToString());

          if (depth == 0) {
            var blazeRodLoc = entryLocation; // The player jumps off the entry loc anyway
            var blazeRodTile = level.terrain.tiles[blazeRodLoc];
            blazeRodTile.components.Add(
                level.root.EffectItemTTCCreate(
                        level.root.EffectExplosionRodCreate().AsIItem())
                    .AsITerrainTileComponent());
            openLowerLocs.Remove(blazeRodLoc);

            var explosionRodLoc =
                levelSuperstate.GetNRandomWalkableLocations(
                    level.terrain,
                    game.rand,
                    1,
                    (a) => openUpperLocs.Contains(a),
                    true,
                    true)[0];
            var explosionRodTile = level.terrain.tiles[explosionRodLoc];
            explosionRodTile.components.Add(
                level.root.EffectItemTTCCreate(
                        level.root.EffectExplosionRodCreate().AsIItem())
                    .AsITerrainTileComponent());
            openUpperLocs.Remove(explosionRodLoc);
          }

          var openLocs = new SortedSet<Location>(openLowerLocs);
          SetUtils.AddAll(openLocs, openUpperLocs);
          foreach (var loc in openLocs) {
            var plantRand = game.rand.Next() % 100;
            if (plantRand < 20) {
              level.terrain.tiles[loc].components.Add(level.root.EffectLeafTTCCreate().AsITerrainTileComponent());
            } else if (plantRand < 25) {
              level.terrain.tiles[loc].components.Add(level.root.EffectFlowerTTCCreate().AsITerrainTileComponent());
            } else if (plantRand < 30) {
              level.terrain.tiles[loc].components.Add(level.root.EffectRoseTTCCreate().AsITerrainTileComponent());
            } else if (plantRand < 35) {
              level.terrain.tiles[loc].components.Add(level.root.EffectLotusTTCCreate().AsITerrainTileComponent());
            }
          }

          var viviantLocations = SetUtils.GetRandomN(openLocs, game.rand, 2, 20);
          foreach (var loc in viviantLocations) {
            level.EnterUnit(
                levelSuperstate,
                loc,
                level.time + 10,
                Viviant.Make(level.root));
          }

          break;
      }
    }
  }
}
