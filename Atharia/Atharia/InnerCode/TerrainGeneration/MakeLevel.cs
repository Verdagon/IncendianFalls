﻿using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public static class MakeLevel {
    //  Level level;
    //  if (squareLevelsOnly || rand.Next() % 2 == 0) {
    //    level = MakeSquareLevel(context, rand, currentTime, name);
    //  } else {
    //    level = MakePentagonalLevel(context, rand, currentTime, name);
    //  }

    //  var walkableLocations = new WalkableLocations(level.terrain, level.units);

    //  SetupCommon.FillWithUnits(
    //      context,
    //      rand,
    //      currentTime,
    //      level.terrain,
    //      level.units,
    //      walkableLocations,
    //      walkableLocations.Count / 15);

    //  return level;
    //}

    //private static Level MakeSimpleLevel(SSContext context, string name) {
    //  var tiles = context.root.EffectTerrainTileByLocationMutMapCreate();
    //  tiles.Add(
    //      new Location(0, 0, 0),
    //      context.root.EffectTerrainTileCreate(
    //          1, true, "floor", ITerrainTileComponentMutBunch.New(context.root)));
    //  var terrain = context.root.EffectTerrainCreate(SquarePattern.MakeSquarePattern(), 1, tiles);
    //  return context.root.EffectLevelCreate(name, false, terrain, context.root.EffectUnitMutSetCreate(), NullILevelController.Null);
    //}

    //private static void PlaceStaircases(SSContext context, Rand rand, Terrain terrain, UnitMutSet units) {

    //  var walkableLocations = new WalkableLocations(terrain, units);

    //  List<Location> staircaseLocations = walkableLocations.GetRandomN(rand, 2);
    //  var upStaircaseLocation = staircaseLocations[0];
    //  var downStaircaseLocation = staircaseLocations[1];

    //  var upStaircaseTile = terrain.tiles[upStaircaseLocation];
    //  upStaircaseTile.components.Add(new UpStaircaseTTCAsITerrainTileComponent(context.root.EffectUpStaircaseTTCCreate()));

    //  var downStaircaseTile = terrain.tiles[downStaircaseLocation];
    //  downStaircaseTile.components.Add(new DownStaircaseTTCAsITerrainTileComponent(context.root.EffectDownStaircaseTTCCreate()));
    //}

    //private static Level MakePentagonalLevel(
    //    SSContext context,
    //    Rand rand,
    //    int currentTime,
    //    string name) {
    //  var terrain =
    //      ForestTerrainGenerator.Generate(
    //          context,
    //          rand,
    //          PentagonPattern9.makePentagon9Pattern());

    //  var units = context.root.EffectUnitMutSetCreate();

    //  PlaceRocks(context, rand, terrain);
    //  PlaceItems(context, rand, terrain);

    //  PlaceStaircases(context, rand, terrain, units);

    //  return context.root.EffectLevelCreate(name, false, terrain, units, NullILevelController.Null);
    //}

    //private static Level MakeSquareLevel(
    //    SSContext context,
    //    Rand rand,
    //    int currentTime, 
    //    string name) {
    //  context.root.GetDeterministicHashCode();
    //  var terrain = DungeonTerrainGenerator.Generate(context, 80, 20, rand);
    //  context.root.GetDeterministicHashCode();

    //  var units = context.root.EffectUnitMutSetCreate();

    //  context.root.GetDeterministicHashCode();

    //  PlaceRocks(context, rand, terrain);
    //  PlaceItems(context, rand, terrain);

    //  PlaceStaircases(context, rand, terrain, units);

    //  context.root.GetDeterministicHashCode();
    //  return context.root.EffectLevelCreate(name, true, terrain, units, NullILevelController.Null);
    //}

  }
}
