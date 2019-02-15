using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class RootIncarnation {
  public readonly int version;
  public int nextId;
  public readonly SortedDictionary<int, VersionAndIncarnation<GameIncarnation>> incarnationsGame;
  public readonly SortedDictionary<int, VersionAndIncarnation<RandIncarnation>> incarnationsRand;
  public readonly SortedDictionary<int, VersionAndIncarnation<LevelIncarnation>> incarnationsLevel;
  public readonly SortedDictionary<int, VersionAndIncarnation<TerrainIncarnation>> incarnationsTerrain;
  public readonly SortedDictionary<int, VersionAndIncarnation<TerrainTileIncarnation>> incarnationsTerrainTile;
  public readonly SortedDictionary<int, VersionAndIncarnation<UnitIncarnation>> incarnationsUnit;
  public readonly SortedDictionary<int, VersionAndIncarnation<MoveDirectiveIncarnation>> incarnationsMoveDirective;
  public readonly SortedDictionary<int, VersionAndIncarnation<AttackDirectiveIncarnation>> incarnationsAttackDirective;
  public readonly SortedDictionary<int, VersionAndIncarnation<DefendingDetailIncarnation>> incarnationsDefendingDetail;
  public readonly SortedDictionary<int, VersionAndIncarnation<ArmorIncarnation>> incarnationsArmor;
  public readonly SortedDictionary<int, VersionAndIncarnation<GlaiveIncarnation>> incarnationsGlaive;
  public readonly SortedDictionary<int, VersionAndIncarnation<ExecutionStateIncarnation>> incarnationsExecutionState;
  public readonly SortedDictionary<int, VersionAndIncarnation<LocationMutListIncarnation>> incarnationsLocationMutList;
  public readonly SortedDictionary<int, VersionAndIncarnation<IUnitEventMutListIncarnation>> incarnationsIUnitEventMutList;
  public readonly SortedDictionary<int, VersionAndIncarnation<IDetailMutListIncarnation>> incarnationsIDetailMutList;
  public readonly SortedDictionary<int, VersionAndIncarnation<LevelMutBunchIncarnation>> incarnationsLevelMutBunch;
  public readonly SortedDictionary<int, VersionAndIncarnation<UnitMutBunchIncarnation>> incarnationsUnitMutBunch;
  public readonly SortedDictionary<int, VersionAndIncarnation<IItemMutBunchIncarnation>> incarnationsIItemMutBunch;
  public readonly SortedDictionary<int, VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>> incarnationsTerrainTileByLocationMutMap;
  public RootIncarnation(int version) {
    this.version = version;
    this.nextId = 1;
    this.incarnationsGame = new SortedDictionary<int, VersionAndIncarnation<GameIncarnation>>();
    this.incarnationsRand = new SortedDictionary<int, VersionAndIncarnation<RandIncarnation>>();
    this.incarnationsLevel = new SortedDictionary<int, VersionAndIncarnation<LevelIncarnation>>();
    this.incarnationsTerrain = new SortedDictionary<int, VersionAndIncarnation<TerrainIncarnation>>();
    this.incarnationsTerrainTile = new SortedDictionary<int, VersionAndIncarnation<TerrainTileIncarnation>>();
    this.incarnationsUnit = new SortedDictionary<int, VersionAndIncarnation<UnitIncarnation>>();
    this.incarnationsMoveDirective = new SortedDictionary<int, VersionAndIncarnation<MoveDirectiveIncarnation>>();
    this.incarnationsAttackDirective = new SortedDictionary<int, VersionAndIncarnation<AttackDirectiveIncarnation>>();
    this.incarnationsDefendingDetail = new SortedDictionary<int, VersionAndIncarnation<DefendingDetailIncarnation>>();
    this.incarnationsArmor = new SortedDictionary<int, VersionAndIncarnation<ArmorIncarnation>>();
    this.incarnationsGlaive = new SortedDictionary<int, VersionAndIncarnation<GlaiveIncarnation>>();
    this.incarnationsExecutionState = new SortedDictionary<int, VersionAndIncarnation<ExecutionStateIncarnation>>();
    this.incarnationsLocationMutList = new SortedDictionary<int, VersionAndIncarnation<LocationMutListIncarnation>>();
    this.incarnationsIUnitEventMutList = new SortedDictionary<int, VersionAndIncarnation<IUnitEventMutListIncarnation>>();
    this.incarnationsIDetailMutList = new SortedDictionary<int, VersionAndIncarnation<IDetailMutListIncarnation>>();
    this.incarnationsLevelMutBunch = new SortedDictionary<int, VersionAndIncarnation<LevelMutBunchIncarnation>>();
    this.incarnationsUnitMutBunch = new SortedDictionary<int, VersionAndIncarnation<UnitMutBunchIncarnation>>();
    this.incarnationsIItemMutBunch = new SortedDictionary<int, VersionAndIncarnation<IItemMutBunchIncarnation>>();
    this.incarnationsTerrainTileByLocationMutMap = new SortedDictionary<int, VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>>();
  }
  public RootIncarnation(
      int version,
      int nextId,
      SortedDictionary<int, VersionAndIncarnation<GameIncarnation>> incarnationsGame,
      SortedDictionary<int, VersionAndIncarnation<RandIncarnation>> incarnationsRand,
      SortedDictionary<int, VersionAndIncarnation<LevelIncarnation>> incarnationsLevel,
      SortedDictionary<int, VersionAndIncarnation<TerrainIncarnation>> incarnationsTerrain,
      SortedDictionary<int, VersionAndIncarnation<TerrainTileIncarnation>> incarnationsTerrainTile,
      SortedDictionary<int, VersionAndIncarnation<UnitIncarnation>> incarnationsUnit,
      SortedDictionary<int, VersionAndIncarnation<MoveDirectiveIncarnation>> incarnationsMoveDirective,
      SortedDictionary<int, VersionAndIncarnation<AttackDirectiveIncarnation>> incarnationsAttackDirective,
      SortedDictionary<int, VersionAndIncarnation<DefendingDetailIncarnation>> incarnationsDefendingDetail,
      SortedDictionary<int, VersionAndIncarnation<ArmorIncarnation>> incarnationsArmor,
      SortedDictionary<int, VersionAndIncarnation<GlaiveIncarnation>> incarnationsGlaive,
      SortedDictionary<int, VersionAndIncarnation<ExecutionStateIncarnation>> incarnationsExecutionState,
      SortedDictionary<int, VersionAndIncarnation<LocationMutListIncarnation>> incarnationsLocationMutList,
      SortedDictionary<int, VersionAndIncarnation<IUnitEventMutListIncarnation>> incarnationsIUnitEventMutList,
      SortedDictionary<int, VersionAndIncarnation<IDetailMutListIncarnation>> incarnationsIDetailMutList,
      SortedDictionary<int, VersionAndIncarnation<LevelMutBunchIncarnation>> incarnationsLevelMutBunch,
      SortedDictionary<int, VersionAndIncarnation<UnitMutBunchIncarnation>> incarnationsUnitMutBunch,
      SortedDictionary<int, VersionAndIncarnation<IItemMutBunchIncarnation>> incarnationsIItemMutBunch,
      SortedDictionary<int, VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>> incarnationsTerrainTileByLocationMutMap  ) {
    this.version = version;
    this.nextId = nextId;
    this.incarnationsGame = incarnationsGame;
    this.incarnationsRand = incarnationsRand;
    this.incarnationsLevel = incarnationsLevel;
    this.incarnationsTerrain = incarnationsTerrain;
    this.incarnationsTerrainTile = incarnationsTerrainTile;
    this.incarnationsUnit = incarnationsUnit;
    this.incarnationsMoveDirective = incarnationsMoveDirective;
    this.incarnationsAttackDirective = incarnationsAttackDirective;
    this.incarnationsDefendingDetail = incarnationsDefendingDetail;
    this.incarnationsArmor = incarnationsArmor;
    this.incarnationsGlaive = incarnationsGlaive;
    this.incarnationsExecutionState = incarnationsExecutionState;
    this.incarnationsLocationMutList = incarnationsLocationMutList;
    this.incarnationsIUnitEventMutList = incarnationsIUnitEventMutList;
    this.incarnationsIDetailMutList = incarnationsIDetailMutList;
    this.incarnationsLevelMutBunch = incarnationsLevelMutBunch;
    this.incarnationsUnitMutBunch = incarnationsUnitMutBunch;
    this.incarnationsIItemMutBunch = incarnationsIItemMutBunch;
    this.incarnationsTerrainTileByLocationMutMap = incarnationsTerrainTileByLocationMutMap;
  }
}

}
