using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class RootIncarnation {
  public readonly int version;
  public int nextId;
  public int hash;
  public readonly SortedDictionary<int, VersionAndIncarnation<GameIncarnation>> incarnationsGame;
  public readonly SortedDictionary<int, VersionAndIncarnation<RandIncarnation>> incarnationsRand;
  public readonly SortedDictionary<int, VersionAndIncarnation<LevelIncarnation>> incarnationsLevel;
  public readonly SortedDictionary<int, VersionAndIncarnation<TerrainIncarnation>> incarnationsTerrain;
  public readonly SortedDictionary<int, VersionAndIncarnation<TerrainTileIncarnation>> incarnationsTerrainTile;
  public readonly SortedDictionary<int, VersionAndIncarnation<DownStaircaseFeatureIncarnation>> incarnationsDownStaircaseFeature;
  public readonly SortedDictionary<int, VersionAndIncarnation<UpStaircaseFeatureIncarnation>> incarnationsUpStaircaseFeature;
  public readonly SortedDictionary<int, VersionAndIncarnation<DecorativeFeatureIncarnation>> incarnationsDecorativeFeature;
  public readonly SortedDictionary<int, VersionAndIncarnation<UnitIncarnation>> incarnationsUnit;
  public readonly SortedDictionary<int, VersionAndIncarnation<MoveDirectiveIncarnation>> incarnationsMoveDirective;
  public readonly SortedDictionary<int, VersionAndIncarnation<AttackDirectiveIncarnation>> incarnationsAttackDirective;
  public readonly SortedDictionary<int, VersionAndIncarnation<DefendingDetailIncarnation>> incarnationsDefendingDetail;
  public readonly SortedDictionary<int, VersionAndIncarnation<ArmorIncarnation>> incarnationsArmor;
  public readonly SortedDictionary<int, VersionAndIncarnation<GlaiveIncarnation>> incarnationsGlaive;
  public readonly SortedDictionary<int, VersionAndIncarnation<ExecutionStateIncarnation>> incarnationsExecutionState;
  public readonly SortedDictionary<int, VersionAndIncarnation<LocationMutListIncarnation>> incarnationsLocationMutList;
  public readonly SortedDictionary<int, VersionAndIncarnation<IFeatureMutListIncarnation>> incarnationsIFeatureMutList;
  public readonly SortedDictionary<int, VersionAndIncarnation<IUnitEventMutListIncarnation>> incarnationsIUnitEventMutList;
  public readonly SortedDictionary<int, VersionAndIncarnation<IDetailMutListIncarnation>> incarnationsIDetailMutList;
  public readonly SortedDictionary<int, VersionAndIncarnation<LevelMutBunchIncarnation>> incarnationsLevelMutBunch;
  public readonly SortedDictionary<int, VersionAndIncarnation<UnitMutBunchIncarnation>> incarnationsUnitMutBunch;
  public readonly SortedDictionary<int, VersionAndIncarnation<IItemMutBunchIncarnation>> incarnationsIItemMutBunch;
  public readonly SortedDictionary<int, VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>> incarnationsTerrainTileByLocationMutMap;
  public RootIncarnation(int version, int nextId, int hash) {
    this.version = version;
    this.nextId = nextId;
    this.hash = hash;
    this.incarnationsGame = new SortedDictionary<int, VersionAndIncarnation<GameIncarnation>>();
    this.incarnationsRand = new SortedDictionary<int, VersionAndIncarnation<RandIncarnation>>();
    this.incarnationsLevel = new SortedDictionary<int, VersionAndIncarnation<LevelIncarnation>>();
    this.incarnationsTerrain = new SortedDictionary<int, VersionAndIncarnation<TerrainIncarnation>>();
    this.incarnationsTerrainTile = new SortedDictionary<int, VersionAndIncarnation<TerrainTileIncarnation>>();
    this.incarnationsDownStaircaseFeature = new SortedDictionary<int, VersionAndIncarnation<DownStaircaseFeatureIncarnation>>();
    this.incarnationsUpStaircaseFeature = new SortedDictionary<int, VersionAndIncarnation<UpStaircaseFeatureIncarnation>>();
    this.incarnationsDecorativeFeature = new SortedDictionary<int, VersionAndIncarnation<DecorativeFeatureIncarnation>>();
    this.incarnationsUnit = new SortedDictionary<int, VersionAndIncarnation<UnitIncarnation>>();
    this.incarnationsMoveDirective = new SortedDictionary<int, VersionAndIncarnation<MoveDirectiveIncarnation>>();
    this.incarnationsAttackDirective = new SortedDictionary<int, VersionAndIncarnation<AttackDirectiveIncarnation>>();
    this.incarnationsDefendingDetail = new SortedDictionary<int, VersionAndIncarnation<DefendingDetailIncarnation>>();
    this.incarnationsArmor = new SortedDictionary<int, VersionAndIncarnation<ArmorIncarnation>>();
    this.incarnationsGlaive = new SortedDictionary<int, VersionAndIncarnation<GlaiveIncarnation>>();
    this.incarnationsExecutionState = new SortedDictionary<int, VersionAndIncarnation<ExecutionStateIncarnation>>();
    this.incarnationsLocationMutList = new SortedDictionary<int, VersionAndIncarnation<LocationMutListIncarnation>>();
    this.incarnationsIFeatureMutList = new SortedDictionary<int, VersionAndIncarnation<IFeatureMutListIncarnation>>();
    this.incarnationsIUnitEventMutList = new SortedDictionary<int, VersionAndIncarnation<IUnitEventMutListIncarnation>>();
    this.incarnationsIDetailMutList = new SortedDictionary<int, VersionAndIncarnation<IDetailMutListIncarnation>>();
    this.incarnationsLevelMutBunch = new SortedDictionary<int, VersionAndIncarnation<LevelMutBunchIncarnation>>();
    this.incarnationsUnitMutBunch = new SortedDictionary<int, VersionAndIncarnation<UnitMutBunchIncarnation>>();
    this.incarnationsIItemMutBunch = new SortedDictionary<int, VersionAndIncarnation<IItemMutBunchIncarnation>>();
    this.incarnationsTerrainTileByLocationMutMap = new SortedDictionary<int, VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>>();
  }
  public RootIncarnation(
      int newVersion,
      int newNextId,
      int newHash,
      RootIncarnation that) {
    this.version = newVersion;
    this.nextId = newNextId;
    this.hash = newHash;
    this.incarnationsGame = new SortedDictionary<int, VersionAndIncarnation<GameIncarnation>>(that.incarnationsGame);
    this.incarnationsRand = new SortedDictionary<int, VersionAndIncarnation<RandIncarnation>>(that.incarnationsRand);
    this.incarnationsLevel = new SortedDictionary<int, VersionAndIncarnation<LevelIncarnation>>(that.incarnationsLevel);
    this.incarnationsTerrain = new SortedDictionary<int, VersionAndIncarnation<TerrainIncarnation>>(that.incarnationsTerrain);
    this.incarnationsTerrainTile = new SortedDictionary<int, VersionAndIncarnation<TerrainTileIncarnation>>(that.incarnationsTerrainTile);
    this.incarnationsDownStaircaseFeature = new SortedDictionary<int, VersionAndIncarnation<DownStaircaseFeatureIncarnation>>(that.incarnationsDownStaircaseFeature);
    this.incarnationsUpStaircaseFeature = new SortedDictionary<int, VersionAndIncarnation<UpStaircaseFeatureIncarnation>>(that.incarnationsUpStaircaseFeature);
    this.incarnationsDecorativeFeature = new SortedDictionary<int, VersionAndIncarnation<DecorativeFeatureIncarnation>>(that.incarnationsDecorativeFeature);
    this.incarnationsUnit = new SortedDictionary<int, VersionAndIncarnation<UnitIncarnation>>(that.incarnationsUnit);
    this.incarnationsMoveDirective = new SortedDictionary<int, VersionAndIncarnation<MoveDirectiveIncarnation>>(that.incarnationsMoveDirective);
    this.incarnationsAttackDirective = new SortedDictionary<int, VersionAndIncarnation<AttackDirectiveIncarnation>>(that.incarnationsAttackDirective);
    this.incarnationsDefendingDetail = new SortedDictionary<int, VersionAndIncarnation<DefendingDetailIncarnation>>(that.incarnationsDefendingDetail);
    this.incarnationsArmor = new SortedDictionary<int, VersionAndIncarnation<ArmorIncarnation>>(that.incarnationsArmor);
    this.incarnationsGlaive = new SortedDictionary<int, VersionAndIncarnation<GlaiveIncarnation>>(that.incarnationsGlaive);
    this.incarnationsExecutionState = new SortedDictionary<int, VersionAndIncarnation<ExecutionStateIncarnation>>(that.incarnationsExecutionState);
    this.incarnationsLocationMutList = new SortedDictionary<int, VersionAndIncarnation<LocationMutListIncarnation>>(that.incarnationsLocationMutList);
    this.incarnationsIFeatureMutList = new SortedDictionary<int, VersionAndIncarnation<IFeatureMutListIncarnation>>(that.incarnationsIFeatureMutList);
    this.incarnationsIUnitEventMutList = new SortedDictionary<int, VersionAndIncarnation<IUnitEventMutListIncarnation>>(that.incarnationsIUnitEventMutList);
    this.incarnationsIDetailMutList = new SortedDictionary<int, VersionAndIncarnation<IDetailMutListIncarnation>>(that.incarnationsIDetailMutList);
    this.incarnationsLevelMutBunch = new SortedDictionary<int, VersionAndIncarnation<LevelMutBunchIncarnation>>(that.incarnationsLevelMutBunch);
    this.incarnationsUnitMutBunch = new SortedDictionary<int, VersionAndIncarnation<UnitMutBunchIncarnation>>(that.incarnationsUnitMutBunch);
    this.incarnationsIItemMutBunch = new SortedDictionary<int, VersionAndIncarnation<IItemMutBunchIncarnation>>(that.incarnationsIItemMutBunch);
    this.incarnationsTerrainTileByLocationMutMap = new SortedDictionary<int, VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>>(that.incarnationsTerrainTileByLocationMutMap);
  }
}

}
