using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class GenerationCommon {
    public static void GetMapBounds(
        out float mapMinX,
        out float mapMinY,
        out float mapMaxX,
        out float mapMaxY,
        Terrain terrain) {
      mapMinX = 0;
      mapMinY = 0;
      mapMaxX = 0;
      mapMaxY = 0;

      foreach (var entry in terrain.tiles) {
        var location = entry.Key;
        var center = terrain.pattern.GetTileCenter(location);
        mapMinX = Math.Min(mapMinX, center.x);
        mapMinY = Math.Min(mapMinY, center.y);
        mapMaxX = Math.Max(mapMaxX, center.x);
        mapMaxY = Math.Max(mapMaxY, center.y);
      }
    }

    public static void PlaceStaircase(
        Terrain terrain,
        Location location,
        bool down,
        int portalIndex,
        Level destinationLevel,
        int destinationLevelPortalIndex) {
      var upStaircaseTile = terrain.tiles[location];
      upStaircaseTile.components.Add(
          new StaircaseTTCAsITerrainTileComponent(
              terrain.root.EffectStaircaseTTCCreate(
                  portalIndex, destinationLevel, destinationLevelPortalIndex)));
      upStaircaseTile.components.Add(
          new DecorativeTTCAsITerrainTileComponent(
              terrain.root.EffectDecorativeTTCCreate(down ? "downstairs" : "upstairs")));
    }

    public static Location GetLocationClosestTo(
        Terrain terrain,
        Vec2 targetPos) {
      Location closestLocation = new Location(0, 0, 0);
      float closestLocationDistance =
          terrain.pattern.GetTileCenter(closestLocation)
          .distance(targetPos);

      foreach (var hay in terrain.tiles) {
        var hayLoc = hay.Key;
        var hayCenter = terrain.pattern.GetTileCenter(hayLoc);
        var hayDistance = hayCenter.distance(targetPos);
        if (hayDistance < closestLocationDistance) {
          closestLocation = hayLoc;
          closestLocationDistance = hayDistance;
        }
      }

      return closestLocation;
    }

    public static void PlaceItems(SSContext context, Rand rand, Level level, LevelSuperstate levelSuperstate) {
      List<Location> glaiveLocations =
          levelSuperstate.GetNRandomWalkableLocations(
              rand,
              levelSuperstate.NumWalkableLocations(false) / 20,
              new SortedSet<Location>(),
              false);

      foreach (var itemLocation in glaiveLocations) {
        var rockTile = level.terrain.tiles[itemLocation];
        rockTile.components.Add(
            context.root.EffectItemTTCCreate(
                context.root.EffectGlaiveCreate().AsIItem())
            .AsITerrainTileComponent());
      }

      List<Location> armorLocations =
          levelSuperstate.GetNRandomWalkableLocations(
              rand,
              levelSuperstate.NumWalkableLocations(false) / 20,
              new SortedSet<Location>(), 
              false);

      foreach (var itemLocation in armorLocations) {
        var rockTile = level.terrain.tiles[itemLocation];
        rockTile.components.Add(
            context.root.EffectItemTTCCreate(
                context.root.EffectArmorCreate().AsIItem())
            .AsITerrainTileComponent());
      }
    }

    public static void PlaceRocks(SSContext context, Rand rand, Level level, LevelSuperstate levelSuperstate) {
      List<Location> rockLocations =
          levelSuperstate.GetNRandomWalkableLocations(
              rand,
              levelSuperstate.NumWalkableLocations(false) / 20,
              new SortedSet<Location>(),
              false);

      foreach (var rockLocation in rockLocations) {
        var rockTile = level.terrain.tiles[rockLocation];
        rockTile.components.Add(
            context.root.EffectDecorativeTTCCreate("rocks")
            .AsITerrainTileComponent());
      }
    }

    //public static void PlaceStaircases(SSContext context, Rand rand, Level level, LevelSuperstate levelSuperstate) {
    //  List<Location> staircaseLocations =
    //      levelSuperstate.GetNRandomWalkableLocations(
    //          rand, 2, false);

    //  var upStaircaseLocation = staircaseLocations[0];
    //  var downStaircaseLocation = staircaseLocations[1];

    //  var upStaircaseTile = level.terrain.tiles[upStaircaseLocation];
    //  upStaircaseTile.components.Add(
    //      new UpStaircaseTTCAsITerrainTileComponent(
    //          context.root.EffectUpStaircaseTTCCreate()));

    //  var downStaircaseTile = level.terrain.tiles[downStaircaseLocation];
    //  downStaircaseTile.components.Add(
    //      new DownStaircaseTTCAsITerrainTileComponent(context.root.EffectDownStaircaseTTCCreate()));
    //}

    public static Location GetFurthestLocationInDirection(
        Pattern pattern,
        SortedSet<Location> locs,
        Vec2 direction) {
      Location furthestLoc = SetUtils.GetFirst(locs);
      float furthestDistance =
          pattern.GetTileCenter(furthestLoc)
          .dot(direction);
      foreach (var hayLoc in locs) {
        float hayDistance =
            pattern.GetTileCenter(hayLoc)
            .dot(direction);
        if (hayDistance > furthestDistance) {
          furthestDistance = hayDistance;
          furthestLoc = hayLoc;
        }
      }
      return furthestLoc;
    }

    public static void FillWithUnits(
        SSContext context,
        Game game,
        Level level,
        LevelSuperstate levelSuperstate,
        SortedSet<Location> forbiddenLocations,
        int numUnits) {
      var locations = levelSuperstate.GetNRandomWalkableLocations(game.rand, numUnits, forbiddenLocations, true);

      for (int i = 0; i < numUnits; i++) {
        var enemyLocation = locations[i];

        var components = IUnitComponentMutBunch.New(context.root);
        components.Add(context.root.EffectWanderAICapabilityUCCreate().AsIUnitComponent());
        components.Add(context.root.EffectAttackAICapabilityUCCreate().AsIUnitComponent());

        Unit enemy;
        if (game.rand.Next(0, 5) == 0) {
          components.Add(context.root.EffectBideAICapabilityUCCreate().AsIUnitComponent());
          enemy =
              context.root.EffectUnitCreate(
                  context.root.EffectIUnitEventMutListCreate(),
                  true,
                  0,
                  enemyLocation,
                  "irklingking",
                  20, 20,
                  40, 40,
                  600,
                  game.time + 10,
                  components,
                  IItemMutBunch.New(context.root),
                  false);
        } else {
          enemy =
              context.root.EffectUnitCreate(
                  context.root.EffectIUnitEventMutListCreate(),
                  true,
                  0,
                  enemyLocation,
                  "goblin",
                  3, 3,
                  0, 0,
                  600,
                  game.time + 10,
                  components,
                  IItemMutBunch.New(context.root),
                  false);
        }
        level.units.Add(enemy);
        levelSuperstate.Add(enemy);

        if (game.rand.Next(0, 3) == 0) {
          enemy.items.Add(new ArmorAsIItem(context.root.EffectArmorCreate()));
        }
        if (game.rand.Next(0, 3) == 0) {
          enemy.items.Add(new GlaiveAsIItem(context.root.EffectGlaiveCreate()));
        }
      }
    }

  }


}
