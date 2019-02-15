using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct VersionAndIncarnation<T> {
  public int version;
  public T incarnation;
  public VersionAndIncarnation(int version, T incarnation) {
    this.version = version;
    this.incarnation = incarnation;
  }
}

public class Root {
  private void CheckRootsEqual(Root a, Root b) {
    if (a != b) {
      throw new System.Exception("Given objects aren't from the same root!");
    }
  }

  // This *always* points to a live RootIncarnation. When we snapshot, we eagerly
  // make a new one of these.
  private RootIncarnation rootIncarnation;

  // 0 means everything
  readonly SortedDictionary<int, List<IGameEffectObserver>> observersGame;
  readonly SortedDictionary<int, List<IRandEffectObserver>> observersRand;
  readonly SortedDictionary<int, List<ILevelEffectObserver>> observersLevel;
  readonly SortedDictionary<int, List<ITerrainEffectObserver>> observersTerrain;
  readonly SortedDictionary<int, List<ITerrainTileEffectObserver>> observersTerrainTile;
  readonly SortedDictionary<int, List<IUnitEffectObserver>> observersUnit;
  readonly SortedDictionary<int, List<IMoveDirectiveEffectObserver>> observersMoveDirective;
  readonly SortedDictionary<int, List<IAttackDirectiveEffectObserver>> observersAttackDirective;
  readonly SortedDictionary<int, List<IDefendingDetailEffectObserver>> observersDefendingDetail;
  readonly SortedDictionary<int, List<IArmorEffectObserver>> observersArmor;
  readonly SortedDictionary<int, List<IGlaiveEffectObserver>> observersGlaive;
  readonly SortedDictionary<int, List<IExecutionStateEffectObserver>> observersExecutionState;
  readonly SortedDictionary<int, List<ILocationMutListEffectObserver>> observersLocationMutList;
  readonly SortedDictionary<int, List<IIUnitEventMutListEffectObserver>> observersIUnitEventMutList;
  readonly SortedDictionary<int, List<IIDetailMutListEffectObserver>> observersIDetailMutList;
  readonly SortedDictionary<int, List<ILevelMutBunchEffectObserver>> observersLevelMutBunch;
  readonly SortedDictionary<int, List<IUnitMutBunchEffectObserver>> observersUnitMutBunch;
  readonly SortedDictionary<int, List<IIItemMutBunchEffectObserver>> observersIItemMutBunch;
  readonly SortedDictionary<int, List<ITerrainTileByLocationMutMapEffectObserver>> observersTerrainTileByLocationMutMap;

  public Root() {
    this.observersGame = new SortedDictionary<int, List<IGameEffectObserver>>();
    this.observersRand = new SortedDictionary<int, List<IRandEffectObserver>>();
    this.observersLevel = new SortedDictionary<int, List<ILevelEffectObserver>>();
    this.observersTerrain = new SortedDictionary<int, List<ITerrainEffectObserver>>();
    this.observersTerrainTile = new SortedDictionary<int, List<ITerrainTileEffectObserver>>();
    this.observersUnit = new SortedDictionary<int, List<IUnitEffectObserver>>();
    this.observersMoveDirective = new SortedDictionary<int, List<IMoveDirectiveEffectObserver>>();
    this.observersAttackDirective = new SortedDictionary<int, List<IAttackDirectiveEffectObserver>>();
    this.observersDefendingDetail = new SortedDictionary<int, List<IDefendingDetailEffectObserver>>();
    this.observersArmor = new SortedDictionary<int, List<IArmorEffectObserver>>();
    this.observersGlaive = new SortedDictionary<int, List<IGlaiveEffectObserver>>();
    this.observersExecutionState = new SortedDictionary<int, List<IExecutionStateEffectObserver>>();
    this.observersLocationMutList = new SortedDictionary<int, List<ILocationMutListEffectObserver>>();
    this.observersIUnitEventMutList = new SortedDictionary<int, List<IIUnitEventMutListEffectObserver>>();
    this.observersIDetailMutList = new SortedDictionary<int, List<IIDetailMutListEffectObserver>>();
    this.observersLevelMutBunch = new SortedDictionary<int, List<ILevelMutBunchEffectObserver>>();
    this.observersUnitMutBunch = new SortedDictionary<int, List<IUnitMutBunchEffectObserver>>();
    this.observersIItemMutBunch = new SortedDictionary<int, List<IIItemMutBunchEffectObserver>>();
    this.observersTerrainTileByLocationMutMap = new SortedDictionary<int, List<ITerrainTileByLocationMutMapEffectObserver>>();

    rootIncarnation = new RootIncarnation(1);
  }

  private int NewId() {
    return rootIncarnation.nextId++;
  }

  public RootIncarnation Snapshot() {
    RootIncarnation oldIncarnation = rootIncarnation;
    rootIncarnation =
        new RootIncarnation(
            oldIncarnation.version + 1,
            oldIncarnation.nextId,
            new SortedDictionary<int, VersionAndIncarnation<GameIncarnation>>(oldIncarnation.incarnationsGame),
            new SortedDictionary<int, VersionAndIncarnation<RandIncarnation>>(oldIncarnation.incarnationsRand),
            new SortedDictionary<int, VersionAndIncarnation<LevelIncarnation>>(oldIncarnation.incarnationsLevel),
            new SortedDictionary<int, VersionAndIncarnation<TerrainIncarnation>>(oldIncarnation.incarnationsTerrain),
            new SortedDictionary<int, VersionAndIncarnation<TerrainTileIncarnation>>(oldIncarnation.incarnationsTerrainTile),
            new SortedDictionary<int, VersionAndIncarnation<UnitIncarnation>>(oldIncarnation.incarnationsUnit),
            new SortedDictionary<int, VersionAndIncarnation<MoveDirectiveIncarnation>>(oldIncarnation.incarnationsMoveDirective),
            new SortedDictionary<int, VersionAndIncarnation<AttackDirectiveIncarnation>>(oldIncarnation.incarnationsAttackDirective),
            new SortedDictionary<int, VersionAndIncarnation<DefendingDetailIncarnation>>(oldIncarnation.incarnationsDefendingDetail),
            new SortedDictionary<int, VersionAndIncarnation<ArmorIncarnation>>(oldIncarnation.incarnationsArmor),
            new SortedDictionary<int, VersionAndIncarnation<GlaiveIncarnation>>(oldIncarnation.incarnationsGlaive),
            new SortedDictionary<int, VersionAndIncarnation<ExecutionStateIncarnation>>(oldIncarnation.incarnationsExecutionState),
            new SortedDictionary<int, VersionAndIncarnation<LocationMutListIncarnation>>(oldIncarnation.incarnationsLocationMutList),
            new SortedDictionary<int, VersionAndIncarnation<IUnitEventMutListIncarnation>>(oldIncarnation.incarnationsIUnitEventMutList),
            new SortedDictionary<int, VersionAndIncarnation<IDetailMutListIncarnation>>(oldIncarnation.incarnationsIDetailMutList),
            new SortedDictionary<int, VersionAndIncarnation<LevelMutBunchIncarnation>>(oldIncarnation.incarnationsLevelMutBunch),
            new SortedDictionary<int, VersionAndIncarnation<UnitMutBunchIncarnation>>(oldIncarnation.incarnationsUnitMutBunch),
            new SortedDictionary<int, VersionAndIncarnation<IItemMutBunchIncarnation>>(oldIncarnation.incarnationsIItemMutBunch),
            new SortedDictionary<int, VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>>(oldIncarnation.incarnationsTerrainTileByLocationMutMap));
    return oldIncarnation;
  }
     
  public void Revert(RootIncarnation sourceIncarnation) {
    // We do all the adds first so that we don't violate any strong borrows.
    // Then we do all the changes, because those might be flipping things to point
    // at things that were just made.
    // Then we do all the removes.


    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsGame) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsGame.ContainsKey(sourceObjId)) {
        EffectInternalCreateGame(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsRand) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsRand.ContainsKey(sourceObjId)) {
        EffectInternalCreateRand(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsLevel) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsLevel.ContainsKey(sourceObjId)) {
        EffectInternalCreateLevel(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTerrain) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsTerrain.ContainsKey(sourceObjId)) {
        EffectInternalCreateTerrain(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTerrainTile) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsTerrainTile.ContainsKey(sourceObjId)) {
        EffectInternalCreateTerrainTile(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsUnit) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsUnit.ContainsKey(sourceObjId)) {
        EffectInternalCreateUnit(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsMoveDirective) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsMoveDirective.ContainsKey(sourceObjId)) {
        EffectInternalCreateMoveDirective(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsAttackDirective) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsAttackDirective.ContainsKey(sourceObjId)) {
        EffectInternalCreateAttackDirective(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDefendingDetail) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsDefendingDetail.ContainsKey(sourceObjId)) {
        EffectInternalCreateDefendingDetail(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsArmor) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsArmor.ContainsKey(sourceObjId)) {
        EffectInternalCreateArmor(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsGlaive) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsGlaive.ContainsKey(sourceObjId)) {
        EffectInternalCreateGlaive(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsExecutionState) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsExecutionState.ContainsKey(sourceObjId)) {
        EffectInternalCreateExecutionState(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsLocationMutList) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsLocationMutList.ContainsKey(sourceObjId)) {
        EffectInternalCreateLocationMutList(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIUnitEventMutList) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsIUnitEventMutList.ContainsKey(sourceObjId)) {
        EffectInternalCreateIUnitEventMutList(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIDetailMutList) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsIDetailMutList.ContainsKey(sourceObjId)) {
        EffectInternalCreateIDetailMutList(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsLevelMutBunch) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsLevelMutBunch.ContainsKey(sourceObjId)) {
        EffectInternalCreateLevelMutBunch(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsUnitMutBunch) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsUnitMutBunch.ContainsKey(sourceObjId)) {
        EffectInternalCreateUnitMutBunch(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIItemMutBunch) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsIItemMutBunch.ContainsKey(sourceObjId)) {
        EffectInternalCreateIItemMutBunch(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTerrainTileByLocationMutMap) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsTerrainTileByLocationMutMap.ContainsKey(sourceObjId)) {
        EffectInternalCreateTerrainTileByLocationMutMap(sourceObjId, sourceObjIncarnation);
      }
    }
         
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsLocationMutList) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsLocationMutList.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsLocationMutList[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            for (int i = currentObjIncarnation.list.Count - 1; i >= 0; i--) {
              EffectLocationMutListRemoveAt(objId, i);
            }
            foreach (var objIdInSourceObjIncarnation in sourceObjIncarnation.list) {
              EffectLocationMutListAdd(objId, objIdInSourceObjIncarnation);
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.incarnationsLocationMutList[objId] = sourceVersionAndObjIncarnation;
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIUnitEventMutList) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsIUnitEventMutList.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsIUnitEventMutList[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            for (int i = currentObjIncarnation.list.Count - 1; i >= 0; i--) {
              EffectIUnitEventMutListRemoveAt(objId, i);
            }
            foreach (var objIdInSourceObjIncarnation in sourceObjIncarnation.list) {
              EffectIUnitEventMutListAdd(objId, objIdInSourceObjIncarnation);
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.incarnationsIUnitEventMutList[objId] = sourceVersionAndObjIncarnation;
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIDetailMutList) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsIDetailMutList.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsIDetailMutList[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            for (int i = currentObjIncarnation.list.Count - 1; i >= 0; i--) {
              EffectIDetailMutListRemoveAt(objId, i);
            }
            foreach (var objIdInSourceObjIncarnation in sourceObjIncarnation.list) {
              EffectIDetailMutListAdd(objId, objIdInSourceObjIncarnation);
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.incarnationsIDetailMutList[objId] = sourceVersionAndObjIncarnation;
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsLevelMutBunch) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsLevelMutBunch.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsLevelMutBunch[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in currentObjIncarnation.set) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectLevelMutBunchRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectLevelMutBunchAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.incarnationsLevelMutBunch[objId] = sourceVersionAndObjIncarnation;
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsUnitMutBunch) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsUnitMutBunch.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsUnitMutBunch[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in currentObjIncarnation.set) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectUnitMutBunchRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectUnitMutBunchAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.incarnationsUnitMutBunch[objId] = sourceVersionAndObjIncarnation;
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIItemMutBunch) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsIItemMutBunch.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsIItemMutBunch[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in currentObjIncarnation.set) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectIItemMutBunchRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectIItemMutBunchAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.incarnationsIItemMutBunch[objId] = sourceVersionAndObjIncarnation;
          }
        }
      }
             
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsGame) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsGame.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsGame[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          if (sourceObjIncarnation.player != currentObjIncarnation.player) {
            EffectGameSetPlayer(objId, new Unit(this, sourceObjIncarnation.player));
          }

          if (sourceObjIncarnation.level != currentObjIncarnation.level) {
            EffectGameSetLevel(objId, new Level(this, sourceObjIncarnation.level));
          }

          if (sourceObjIncarnation.time != currentObjIncarnation.time) {
            EffectGameSetTime(objId, sourceObjIncarnation.time);
          }

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          rootIncarnation.incarnationsGame[objId] = sourceVersionAndObjIncarnation;
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsRand) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsRand.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsRand[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          if (sourceObjIncarnation.rand != currentObjIncarnation.rand) {
            EffectRandSetRand(objId, sourceObjIncarnation.rand);
          }

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          rootIncarnation.incarnationsRand[objId] = sourceVersionAndObjIncarnation;
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsLevel) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsLevel.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsLevel[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          rootIncarnation.incarnationsLevel[objId] = sourceVersionAndObjIncarnation;
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTerrain) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsTerrain.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsTerrain[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          if (sourceObjIncarnation.pattern != currentObjIncarnation.pattern) {
            EffectTerrainSetPattern(objId, sourceObjIncarnation.pattern);
          }

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          rootIncarnation.incarnationsTerrain[objId] = sourceVersionAndObjIncarnation;
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTerrainTile) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsTerrainTile.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsTerrainTile[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          if (sourceObjIncarnation.elevation != currentObjIncarnation.elevation) {
            EffectTerrainTileSetElevation(objId, sourceObjIncarnation.elevation);
          }

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          rootIncarnation.incarnationsTerrainTile[objId] = sourceVersionAndObjIncarnation;
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsUnit) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsUnit.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsUnit[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          if (sourceObjIncarnation.alive != currentObjIncarnation.alive) {
            EffectUnitSetAlive(objId, sourceObjIncarnation.alive);
          }

          if (sourceObjIncarnation.lifeEndTime != currentObjIncarnation.lifeEndTime) {
            EffectUnitSetLifeEndTime(objId, sourceObjIncarnation.lifeEndTime);
          }

          if (sourceObjIncarnation.location != currentObjIncarnation.location) {
            EffectUnitSetLocation(objId, sourceObjIncarnation.location);
          }

          if (sourceObjIncarnation.hp != currentObjIncarnation.hp) {
            EffectUnitSetHp(objId, sourceObjIncarnation.hp);
          }

          if (sourceObjIncarnation.mp != currentObjIncarnation.mp) {
            EffectUnitSetMp(objId, sourceObjIncarnation.mp);
          }

          if (sourceObjIncarnation.nextActionTime != currentObjIncarnation.nextActionTime) {
            EffectUnitSetNextActionTime(objId, sourceObjIncarnation.nextActionTime);
          }

          if (sourceObjIncarnation.directive != currentObjIncarnation.directive) {
            EffectUnitSetDirective(objId, GetIDirective(sourceObjIncarnation.directive));
          }

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          rootIncarnation.incarnationsUnit[objId] = sourceVersionAndObjIncarnation;
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsMoveDirective) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsMoveDirective.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsMoveDirective[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          rootIncarnation.incarnationsMoveDirective[objId] = sourceVersionAndObjIncarnation;
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsAttackDirective) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsAttackDirective.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsAttackDirective[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          rootIncarnation.incarnationsAttackDirective[objId] = sourceVersionAndObjIncarnation;
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDefendingDetail) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsDefendingDetail.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsDefendingDetail[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          if (sourceObjIncarnation.power != currentObjIncarnation.power) {
            EffectDefendingDetailSetPower(objId, sourceObjIncarnation.power);
          }

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          rootIncarnation.incarnationsDefendingDetail[objId] = sourceVersionAndObjIncarnation;
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsArmor) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsArmor.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsArmor[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          rootIncarnation.incarnationsArmor[objId] = sourceVersionAndObjIncarnation;
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsGlaive) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsGlaive.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsGlaive[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          rootIncarnation.incarnationsGlaive[objId] = sourceVersionAndObjIncarnation;
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsExecutionState) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsExecutionState.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsExecutionState[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          if (sourceObjIncarnation.actingUnit != currentObjIncarnation.actingUnit) {
            EffectExecutionStateSetActingUnit(objId, new Unit(this, sourceObjIncarnation.actingUnit));
          }

          if (sourceObjIncarnation.actingUnitDidAction != currentObjIncarnation.actingUnitDidAction) {
            EffectExecutionStateSetActingUnitDidAction(objId, sourceObjIncarnation.actingUnitDidAction);
          }

          if (sourceObjIncarnation.remainingPreActingDetails != currentObjIncarnation.remainingPreActingDetails) {
            EffectExecutionStateSetRemainingPreActingDetails(objId, new IDetailMutList(this, sourceObjIncarnation.remainingPreActingDetails));
          }

          if (sourceObjIncarnation.remainingPostActingDetails != currentObjIncarnation.remainingPostActingDetails) {
            EffectExecutionStateSetRemainingPostActingDetails(objId, new IDetailMutList(this, sourceObjIncarnation.remainingPostActingDetails));
          }

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          rootIncarnation.incarnationsExecutionState[objId] = sourceVersionAndObjIncarnation;
        }
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<GameIncarnation>>(rootIncarnation.incarnationsGame)) {
      if (!sourceIncarnation.incarnationsGame.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectGameDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<RandIncarnation>>(rootIncarnation.incarnationsRand)) {
      if (!sourceIncarnation.incarnationsRand.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectRandDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<LevelIncarnation>>(rootIncarnation.incarnationsLevel)) {
      if (!sourceIncarnation.incarnationsLevel.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectLevelDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<TerrainIncarnation>>(rootIncarnation.incarnationsTerrain)) {
      if (!sourceIncarnation.incarnationsTerrain.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectTerrainDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<TerrainTileIncarnation>>(rootIncarnation.incarnationsTerrainTile)) {
      if (!sourceIncarnation.incarnationsTerrainTile.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectTerrainTileDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<UnitIncarnation>>(rootIncarnation.incarnationsUnit)) {
      if (!sourceIncarnation.incarnationsUnit.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectUnitDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<MoveDirectiveIncarnation>>(rootIncarnation.incarnationsMoveDirective)) {
      if (!sourceIncarnation.incarnationsMoveDirective.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectMoveDirectiveDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<AttackDirectiveIncarnation>>(rootIncarnation.incarnationsAttackDirective)) {
      if (!sourceIncarnation.incarnationsAttackDirective.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectAttackDirectiveDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<DefendingDetailIncarnation>>(rootIncarnation.incarnationsDefendingDetail)) {
      if (!sourceIncarnation.incarnationsDefendingDetail.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectDefendingDetailDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<ArmorIncarnation>>(rootIncarnation.incarnationsArmor)) {
      if (!sourceIncarnation.incarnationsArmor.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectArmorDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<GlaiveIncarnation>>(rootIncarnation.incarnationsGlaive)) {
      if (!sourceIncarnation.incarnationsGlaive.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectGlaiveDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<ExecutionStateIncarnation>>(rootIncarnation.incarnationsExecutionState)) {
      if (!sourceIncarnation.incarnationsExecutionState.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectExecutionStateDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<LocationMutListIncarnation>>(rootIncarnation.incarnationsLocationMutList)) {
      if (!sourceIncarnation.incarnationsLocationMutList.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectLocationMutListDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<IUnitEventMutListIncarnation>>(rootIncarnation.incarnationsIUnitEventMutList)) {
      if (!sourceIncarnation.incarnationsIUnitEventMutList.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectIUnitEventMutListDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<IDetailMutListIncarnation>>(rootIncarnation.incarnationsIDetailMutList)) {
      if (!sourceIncarnation.incarnationsIDetailMutList.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectIDetailMutListDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<LevelMutBunchIncarnation>>(rootIncarnation.incarnationsLevelMutBunch)) {
      if (!sourceIncarnation.incarnationsLevelMutBunch.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectLevelMutBunchDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<UnitMutBunchIncarnation>>(rootIncarnation.incarnationsUnitMutBunch)) {
      if (!sourceIncarnation.incarnationsUnitMutBunch.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectUnitMutBunchDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<IItemMutBunchIncarnation>>(rootIncarnation.incarnationsIItemMutBunch)) {
      if (!sourceIncarnation.incarnationsIItemMutBunch.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectIItemMutBunchDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>>(rootIncarnation.incarnationsTerrainTileByLocationMutMap)) {
      if (!sourceIncarnation.incarnationsTerrainTileByLocationMutMap.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectTerrainTileByLocationMutMapDelete(id);
      }
    }

  }
     
  public GameIncarnation GetGameIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsGame[id].incarnation;
  }
  public bool GameExists(int id) {
    return rootIncarnation.incarnationsGame.ContainsKey(id);
  }
  public Game GetGame(int id) {
    return new Game(this, id);
  }
  public List<Game> AllGame() {
    List<Game> result = new List<Game>(rootIncarnation.incarnationsGame.Count);
    foreach (var gameId in rootIncarnation.incarnationsGame.Keys) {
      result.Add(new Game(this, gameId));
    }
    return result;
  }
  public IEnumerator<Game> EnumAllGame() {
    foreach (var gameId in rootIncarnation.incarnationsGame.Keys) {
      yield return GetGame(gameId);
    }
  }
  public void CheckHasGame(Game thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasGame(thing.id);
  }
  public void CheckHasGame(int id) {
    if (!rootIncarnation.incarnationsGame.ContainsKey(id)) {
      throw new System.Exception("Invalid Game!");
    }
  }
  public void AddGameObserver(int id, IGameEffectObserver observer) {
    List<IGameEffectObserver> obsies;
    if (!observersGame.TryGetValue(id, out obsies)) {
      obsies = new List<IGameEffectObserver>();
    }
    obsies.Add(observer);
    observersGame[id] = obsies;
  }

  public void RemoveGameObserver(int id, IGameEffectObserver observer) {
    if (observersGame.ContainsKey(id)) {
      var list = observersGame[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersGame.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastGameEffect(int id, IGameEffect effect) {
    if (observersGame.ContainsKey(0)) {
      foreach (var observer in new List<IGameEffectObserver>(observersGame[0])) {
        observer.OnGameEffect(effect);
      }
    }
    if (observersGame.ContainsKey(id)) {
      foreach (var observer in new List<IGameEffectObserver>(observersGame[id])) {
        observer.OnGameEffect(effect);
      }
    }
  }

  public Game EffectGameCreate(
      Rand rand,
      bool squareLevelsOnly,
      LevelMutBunch levels,
      Unit player,
      Level level,
      int time,
      ExecutionState executionState) {
    CheckHasRand(rand);
    CheckHasLevelMutBunch(levels);
    CheckHasUnit(player);
    CheckHasLevel(level);
    CheckHasExecutionState(executionState);
    var id = NewId();
    EffectInternalCreateGame(
        id,
        new GameIncarnation(
            rand.id,
            squareLevelsOnly,
            levels.id,
            player.id,
            level.id,
            time,
            executionState.id));
    return new Game(this, id);
  }

  public void EffectInternalCreateGame(
      int id,
      GameIncarnation incarnation) {
    var effect = new GameCreateEffect(id, incarnation);
    rootIncarnation.incarnationsGame.Add(
        id,
        new VersionAndIncarnation<GameIncarnation>(
            rootIncarnation.version,
            incarnation));
    BroadcastGameEffect(id, effect);
  }
     
public void EffectGameDelete(int id) {
  var effect = new GameDeleteEffect(id);
  BroadcastGameEffect(id, effect);
  rootIncarnation.incarnationsGame.Remove(id);
}
     
  public void EffectGameSetPlayer(int id, Unit newValue) {
    CheckHasGame(id);
    var effect = new GameSetPlayerEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsGame[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      oldIncarnationAndVersion.incarnation.player = newValue.id;
    } else {
      rootIncarnation.incarnationsGame[id] =
        new VersionAndIncarnation<GameIncarnation>(
          rootIncarnation.version,
          new GameIncarnation(
              oldIncarnationAndVersion.incarnation.rand,
              oldIncarnationAndVersion.incarnation.squareLevelsOnly,
              oldIncarnationAndVersion.incarnation.levels,
              newValue.id,
              oldIncarnationAndVersion.incarnation.level,
              oldIncarnationAndVersion.incarnation.time,
              oldIncarnationAndVersion.incarnation.executionState));
    }
    BroadcastGameEffect(id, effect);
  }

  public void EffectGameSetLevel(int id, Level newValue) {
    CheckHasGame(id);
    var effect = new GameSetLevelEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsGame[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      oldIncarnationAndVersion.incarnation.level = newValue.id;
    } else {
      rootIncarnation.incarnationsGame[id] =
        new VersionAndIncarnation<GameIncarnation>(
          rootIncarnation.version,
          new GameIncarnation(
              oldIncarnationAndVersion.incarnation.rand,
              oldIncarnationAndVersion.incarnation.squareLevelsOnly,
              oldIncarnationAndVersion.incarnation.levels,
              oldIncarnationAndVersion.incarnation.player,
              newValue.id,
              oldIncarnationAndVersion.incarnation.time,
              oldIncarnationAndVersion.incarnation.executionState));
    }
    BroadcastGameEffect(id, effect);
  }

  public void EffectGameSetTime(int id, int newValue) {
    CheckHasGame(id);
    var effect = new GameSetTimeEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsGame[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      oldIncarnationAndVersion.incarnation.time = newValue;
    } else {
      rootIncarnation.incarnationsGame[id] =
        new VersionAndIncarnation<GameIncarnation>(
          rootIncarnation.version,
          new GameIncarnation(
              oldIncarnationAndVersion.incarnation.rand,
              oldIncarnationAndVersion.incarnation.squareLevelsOnly,
              oldIncarnationAndVersion.incarnation.levels,
              oldIncarnationAndVersion.incarnation.player,
              oldIncarnationAndVersion.incarnation.level,
              newValue,
              oldIncarnationAndVersion.incarnation.executionState));
    }
    BroadcastGameEffect(id, effect);
  }

  public RandIncarnation GetRandIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsRand[id].incarnation;
  }
  public bool RandExists(int id) {
    return rootIncarnation.incarnationsRand.ContainsKey(id);
  }
  public Rand GetRand(int id) {
    return new Rand(this, id);
  }
  public List<Rand> AllRand() {
    List<Rand> result = new List<Rand>(rootIncarnation.incarnationsRand.Count);
    foreach (var gameId in rootIncarnation.incarnationsRand.Keys) {
      result.Add(new Rand(this, gameId));
    }
    return result;
  }
  public IEnumerator<Rand> EnumAllRand() {
    foreach (var gameId in rootIncarnation.incarnationsRand.Keys) {
      yield return GetRand(gameId);
    }
  }
  public void CheckHasRand(Rand thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasRand(thing.id);
  }
  public void CheckHasRand(int id) {
    if (!rootIncarnation.incarnationsRand.ContainsKey(id)) {
      throw new System.Exception("Invalid Rand!");
    }
  }
  public void AddRandObserver(int id, IRandEffectObserver observer) {
    List<IRandEffectObserver> obsies;
    if (!observersRand.TryGetValue(id, out obsies)) {
      obsies = new List<IRandEffectObserver>();
    }
    obsies.Add(observer);
    observersRand[id] = obsies;
  }

  public void RemoveRandObserver(int id, IRandEffectObserver observer) {
    if (observersRand.ContainsKey(id)) {
      var list = observersRand[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersRand.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastRandEffect(int id, IRandEffect effect) {
    if (observersRand.ContainsKey(0)) {
      foreach (var observer in new List<IRandEffectObserver>(observersRand[0])) {
        observer.OnRandEffect(effect);
      }
    }
    if (observersRand.ContainsKey(id)) {
      foreach (var observer in new List<IRandEffectObserver>(observersRand[id])) {
        observer.OnRandEffect(effect);
      }
    }
  }

  public Rand EffectRandCreate(
      int rand) {
    var id = NewId();
    EffectInternalCreateRand(
        id,
        new RandIncarnation(
            rand));
    return new Rand(this, id);
  }

  public void EffectInternalCreateRand(
      int id,
      RandIncarnation incarnation) {
    var effect = new RandCreateEffect(id, incarnation);
    rootIncarnation.incarnationsRand.Add(
        id,
        new VersionAndIncarnation<RandIncarnation>(
            rootIncarnation.version,
            incarnation));
    BroadcastRandEffect(id, effect);
  }
     
public void EffectRandDelete(int id) {
  var effect = new RandDeleteEffect(id);
  BroadcastRandEffect(id, effect);
  rootIncarnation.incarnationsRand.Remove(id);
}
     
  public void EffectRandSetRand(int id, int newValue) {
    CheckHasRand(id);
    var effect = new RandSetRandEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsRand[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      oldIncarnationAndVersion.incarnation.rand = newValue;
    } else {
      rootIncarnation.incarnationsRand[id] =
        new VersionAndIncarnation<RandIncarnation>(
          rootIncarnation.version,
          new RandIncarnation(
              newValue));
    }
    BroadcastRandEffect(id, effect);
  }

  public LevelIncarnation GetLevelIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsLevel[id].incarnation;
  }
  public bool LevelExists(int id) {
    return rootIncarnation.incarnationsLevel.ContainsKey(id);
  }
  public Level GetLevel(int id) {
    return new Level(this, id);
  }
  public List<Level> AllLevel() {
    List<Level> result = new List<Level>(rootIncarnation.incarnationsLevel.Count);
    foreach (var gameId in rootIncarnation.incarnationsLevel.Keys) {
      result.Add(new Level(this, gameId));
    }
    return result;
  }
  public IEnumerator<Level> EnumAllLevel() {
    foreach (var gameId in rootIncarnation.incarnationsLevel.Keys) {
      yield return GetLevel(gameId);
    }
  }
  public void CheckHasLevel(Level thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasLevel(thing.id);
  }
  public void CheckHasLevel(int id) {
    if (!rootIncarnation.incarnationsLevel.ContainsKey(id)) {
      throw new System.Exception("Invalid Level!");
    }
  }
  public void AddLevelObserver(int id, ILevelEffectObserver observer) {
    List<ILevelEffectObserver> obsies;
    if (!observersLevel.TryGetValue(id, out obsies)) {
      obsies = new List<ILevelEffectObserver>();
    }
    obsies.Add(observer);
    observersLevel[id] = obsies;
  }

  public void RemoveLevelObserver(int id, ILevelEffectObserver observer) {
    if (observersLevel.ContainsKey(id)) {
      var list = observersLevel[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersLevel.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastLevelEffect(int id, ILevelEffect effect) {
    if (observersLevel.ContainsKey(0)) {
      foreach (var observer in new List<ILevelEffectObserver>(observersLevel[0])) {
        observer.OnLevelEffect(effect);
      }
    }
    if (observersLevel.ContainsKey(id)) {
      foreach (var observer in new List<ILevelEffectObserver>(observersLevel[id])) {
        observer.OnLevelEffect(effect);
      }
    }
  }

  public Level EffectLevelCreate(
      string name,
      bool considerCornersAdjacent,
      Terrain terrain,
      UnitMutBunch units) {
    CheckHasTerrain(terrain);
    CheckHasUnitMutBunch(units);
    var id = NewId();
    EffectInternalCreateLevel(
        id,
        new LevelIncarnation(
            name,
            considerCornersAdjacent,
            terrain.id,
            units.id));
    return new Level(this, id);
  }

  public void EffectInternalCreateLevel(
      int id,
      LevelIncarnation incarnation) {
    var effect = new LevelCreateEffect(id, incarnation);
    rootIncarnation.incarnationsLevel.Add(
        id,
        new VersionAndIncarnation<LevelIncarnation>(
            rootIncarnation.version,
            incarnation));
    BroadcastLevelEffect(id, effect);
  }
     
public void EffectLevelDelete(int id) {
  var effect = new LevelDeleteEffect(id);
  BroadcastLevelEffect(id, effect);
  rootIncarnation.incarnationsLevel.Remove(id);
}
     
  public TerrainIncarnation GetTerrainIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsTerrain[id].incarnation;
  }
  public bool TerrainExists(int id) {
    return rootIncarnation.incarnationsTerrain.ContainsKey(id);
  }
  public Terrain GetTerrain(int id) {
    return new Terrain(this, id);
  }
  public List<Terrain> AllTerrain() {
    List<Terrain> result = new List<Terrain>(rootIncarnation.incarnationsTerrain.Count);
    foreach (var gameId in rootIncarnation.incarnationsTerrain.Keys) {
      result.Add(new Terrain(this, gameId));
    }
    return result;
  }
  public IEnumerator<Terrain> EnumAllTerrain() {
    foreach (var gameId in rootIncarnation.incarnationsTerrain.Keys) {
      yield return GetTerrain(gameId);
    }
  }
  public void CheckHasTerrain(Terrain thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasTerrain(thing.id);
  }
  public void CheckHasTerrain(int id) {
    if (!rootIncarnation.incarnationsTerrain.ContainsKey(id)) {
      throw new System.Exception("Invalid Terrain!");
    }
  }
  public void AddTerrainObserver(int id, ITerrainEffectObserver observer) {
    List<ITerrainEffectObserver> obsies;
    if (!observersTerrain.TryGetValue(id, out obsies)) {
      obsies = new List<ITerrainEffectObserver>();
    }
    obsies.Add(observer);
    observersTerrain[id] = obsies;
  }

  public void RemoveTerrainObserver(int id, ITerrainEffectObserver observer) {
    if (observersTerrain.ContainsKey(id)) {
      var list = observersTerrain[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersTerrain.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastTerrainEffect(int id, ITerrainEffect effect) {
    if (observersTerrain.ContainsKey(0)) {
      foreach (var observer in new List<ITerrainEffectObserver>(observersTerrain[0])) {
        observer.OnTerrainEffect(effect);
      }
    }
    if (observersTerrain.ContainsKey(id)) {
      foreach (var observer in new List<ITerrainEffectObserver>(observersTerrain[id])) {
        observer.OnTerrainEffect(effect);
      }
    }
  }

  public Terrain EffectTerrainCreate(
      Pattern pattern,
      float elevationStepHeight,
      TerrainTileByLocationMutMap tiles) {
    CheckHasTerrainTileByLocationMutMap(tiles);
    var id = NewId();
    EffectInternalCreateTerrain(
        id,
        new TerrainIncarnation(
            pattern,
            elevationStepHeight,
            tiles.id));
    return new Terrain(this, id);
  }

  public void EffectInternalCreateTerrain(
      int id,
      TerrainIncarnation incarnation) {
    var effect = new TerrainCreateEffect(id, incarnation);
    rootIncarnation.incarnationsTerrain.Add(
        id,
        new VersionAndIncarnation<TerrainIncarnation>(
            rootIncarnation.version,
            incarnation));
    BroadcastTerrainEffect(id, effect);
  }
     
public void EffectTerrainDelete(int id) {
  var effect = new TerrainDeleteEffect(id);
  BroadcastTerrainEffect(id, effect);
  rootIncarnation.incarnationsTerrain.Remove(id);
}
     
  public void EffectTerrainSetPattern(int id, Pattern newValue) {
    CheckHasTerrain(id);
    var effect = new TerrainSetPatternEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsTerrain[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      oldIncarnationAndVersion.incarnation.pattern = newValue;
    } else {
      rootIncarnation.incarnationsTerrain[id] =
        new VersionAndIncarnation<TerrainIncarnation>(
          rootIncarnation.version,
          new TerrainIncarnation(
              newValue,
              oldIncarnationAndVersion.incarnation.elevationStepHeight,
              oldIncarnationAndVersion.incarnation.tiles));
    }
    BroadcastTerrainEffect(id, effect);
  }

  public TerrainTileIncarnation GetTerrainTileIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsTerrainTile[id].incarnation;
  }
  public bool TerrainTileExists(int id) {
    return rootIncarnation.incarnationsTerrainTile.ContainsKey(id);
  }
  public TerrainTile GetTerrainTile(int id) {
    return new TerrainTile(this, id);
  }
  public List<TerrainTile> AllTerrainTile() {
    List<TerrainTile> result = new List<TerrainTile>(rootIncarnation.incarnationsTerrainTile.Count);
    foreach (var gameId in rootIncarnation.incarnationsTerrainTile.Keys) {
      result.Add(new TerrainTile(this, gameId));
    }
    return result;
  }
  public IEnumerator<TerrainTile> EnumAllTerrainTile() {
    foreach (var gameId in rootIncarnation.incarnationsTerrainTile.Keys) {
      yield return GetTerrainTile(gameId);
    }
  }
  public void CheckHasTerrainTile(TerrainTile thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasTerrainTile(thing.id);
  }
  public void CheckHasTerrainTile(int id) {
    if (!rootIncarnation.incarnationsTerrainTile.ContainsKey(id)) {
      throw new System.Exception("Invalid TerrainTile!");
    }
  }
  public void AddTerrainTileObserver(int id, ITerrainTileEffectObserver observer) {
    List<ITerrainTileEffectObserver> obsies;
    if (!observersTerrainTile.TryGetValue(id, out obsies)) {
      obsies = new List<ITerrainTileEffectObserver>();
    }
    obsies.Add(observer);
    observersTerrainTile[id] = obsies;
  }

  public void RemoveTerrainTileObserver(int id, ITerrainTileEffectObserver observer) {
    if (observersTerrainTile.ContainsKey(id)) {
      var list = observersTerrainTile[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersTerrainTile.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastTerrainTileEffect(int id, ITerrainTileEffect effect) {
    if (observersTerrainTile.ContainsKey(0)) {
      foreach (var observer in new List<ITerrainTileEffectObserver>(observersTerrainTile[0])) {
        observer.OnTerrainTileEffect(effect);
      }
    }
    if (observersTerrainTile.ContainsKey(id)) {
      foreach (var observer in new List<ITerrainTileEffectObserver>(observersTerrainTile[id])) {
        observer.OnTerrainTileEffect(effect);
      }
    }
  }

  public TerrainTile EffectTerrainTileCreate(
      int elevation,
      bool walkable,
      string classId) {
    var id = NewId();
    EffectInternalCreateTerrainTile(
        id,
        new TerrainTileIncarnation(
            elevation,
            walkable,
            classId));
    return new TerrainTile(this, id);
  }

  public void EffectInternalCreateTerrainTile(
      int id,
      TerrainTileIncarnation incarnation) {
    var effect = new TerrainTileCreateEffect(id, incarnation);
    rootIncarnation.incarnationsTerrainTile.Add(
        id,
        new VersionAndIncarnation<TerrainTileIncarnation>(
            rootIncarnation.version,
            incarnation));
    BroadcastTerrainTileEffect(id, effect);
  }
     
public void EffectTerrainTileDelete(int id) {
  var effect = new TerrainTileDeleteEffect(id);
  BroadcastTerrainTileEffect(id, effect);
  rootIncarnation.incarnationsTerrainTile.Remove(id);
}
     
  public void EffectTerrainTileSetElevation(int id, int newValue) {
    CheckHasTerrainTile(id);
    var effect = new TerrainTileSetElevationEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsTerrainTile[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      oldIncarnationAndVersion.incarnation.elevation = newValue;
    } else {
      rootIncarnation.incarnationsTerrainTile[id] =
        new VersionAndIncarnation<TerrainTileIncarnation>(
          rootIncarnation.version,
          new TerrainTileIncarnation(
              newValue,
              oldIncarnationAndVersion.incarnation.walkable,
              oldIncarnationAndVersion.incarnation.classId));
    }
    BroadcastTerrainTileEffect(id, effect);
  }

  public UnitIncarnation GetUnitIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsUnit[id].incarnation;
  }
  public bool UnitExists(int id) {
    return rootIncarnation.incarnationsUnit.ContainsKey(id);
  }
  public Unit GetUnit(int id) {
    return new Unit(this, id);
  }
  public List<Unit> AllUnit() {
    List<Unit> result = new List<Unit>(rootIncarnation.incarnationsUnit.Count);
    foreach (var gameId in rootIncarnation.incarnationsUnit.Keys) {
      result.Add(new Unit(this, gameId));
    }
    return result;
  }
  public IEnumerator<Unit> EnumAllUnit() {
    foreach (var gameId in rootIncarnation.incarnationsUnit.Keys) {
      yield return GetUnit(gameId);
    }
  }
  public void CheckHasUnit(Unit thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasUnit(thing.id);
  }
  public void CheckHasUnit(int id) {
    if (!rootIncarnation.incarnationsUnit.ContainsKey(id)) {
      throw new System.Exception("Invalid Unit!");
    }
  }
  public void AddUnitObserver(int id, IUnitEffectObserver observer) {
    List<IUnitEffectObserver> obsies;
    if (!observersUnit.TryGetValue(id, out obsies)) {
      obsies = new List<IUnitEffectObserver>();
    }
    obsies.Add(observer);
    observersUnit[id] = obsies;
  }

  public void RemoveUnitObserver(int id, IUnitEffectObserver observer) {
    if (observersUnit.ContainsKey(id)) {
      var list = observersUnit[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersUnit.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastUnitEffect(int id, IUnitEffect effect) {
    if (observersUnit.ContainsKey(0)) {
      foreach (var observer in new List<IUnitEffectObserver>(observersUnit[0])) {
        observer.OnUnitEffect(effect);
      }
    }
    if (observersUnit.ContainsKey(id)) {
      foreach (var observer in new List<IUnitEffectObserver>(observersUnit[id])) {
        observer.OnUnitEffect(effect);
      }
    }
  }

  public Unit EffectUnitCreate(
      IUnitEventMutList events,
      bool alive,
      int lifeEndTime,
      Location location,
      string classId,
      int hp,
      int maxHp,
      int mp,
      int maxMp,
      int inertia,
      int nextActionTime,
      IDirective directive,
      IDetailMutList details,
      IItemMutBunch items) {
    CheckHasIUnitEventMutList(events);
    CheckHasIDetailMutList(details);
    CheckHasIItemMutBunch(items);
    var id = NewId();
    EffectInternalCreateUnit(
        id,
        new UnitIncarnation(
            events.id,
            alive,
            lifeEndTime,
            location,
            classId,
            hp,
            maxHp,
            mp,
            maxMp,
            inertia,
            nextActionTime,
            directive.id,
            details.id,
            items.id));
    return new Unit(this, id);
  }

  public void EffectInternalCreateUnit(
      int id,
      UnitIncarnation incarnation) {
    var effect = new UnitCreateEffect(id, incarnation);
    rootIncarnation.incarnationsUnit.Add(
        id,
        new VersionAndIncarnation<UnitIncarnation>(
            rootIncarnation.version,
            incarnation));
    BroadcastUnitEffect(id, effect);
  }
     
public void EffectUnitDelete(int id) {
  var effect = new UnitDeleteEffect(id);
  BroadcastUnitEffect(id, effect);
  rootIncarnation.incarnationsUnit.Remove(id);
}
     
  public void EffectUnitSetAlive(int id, bool newValue) {
    CheckHasUnit(id);
    var effect = new UnitSetAliveEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsUnit[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      oldIncarnationAndVersion.incarnation.alive = newValue;
    } else {
      rootIncarnation.incarnationsUnit[id] =
        new VersionAndIncarnation<UnitIncarnation>(
          rootIncarnation.version,
          new UnitIncarnation(
              oldIncarnationAndVersion.incarnation.events,
              newValue,
              oldIncarnationAndVersion.incarnation.lifeEndTime,
              oldIncarnationAndVersion.incarnation.location,
              oldIncarnationAndVersion.incarnation.classId,
              oldIncarnationAndVersion.incarnation.hp,
              oldIncarnationAndVersion.incarnation.maxHp,
              oldIncarnationAndVersion.incarnation.mp,
              oldIncarnationAndVersion.incarnation.maxMp,
              oldIncarnationAndVersion.incarnation.inertia,
              oldIncarnationAndVersion.incarnation.nextActionTime,
              oldIncarnationAndVersion.incarnation.directive,
              oldIncarnationAndVersion.incarnation.details,
              oldIncarnationAndVersion.incarnation.items));
    }
    BroadcastUnitEffect(id, effect);
  }

  public void EffectUnitSetLifeEndTime(int id, int newValue) {
    CheckHasUnit(id);
    var effect = new UnitSetLifeEndTimeEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsUnit[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      oldIncarnationAndVersion.incarnation.lifeEndTime = newValue;
    } else {
      rootIncarnation.incarnationsUnit[id] =
        new VersionAndIncarnation<UnitIncarnation>(
          rootIncarnation.version,
          new UnitIncarnation(
              oldIncarnationAndVersion.incarnation.events,
              oldIncarnationAndVersion.incarnation.alive,
              newValue,
              oldIncarnationAndVersion.incarnation.location,
              oldIncarnationAndVersion.incarnation.classId,
              oldIncarnationAndVersion.incarnation.hp,
              oldIncarnationAndVersion.incarnation.maxHp,
              oldIncarnationAndVersion.incarnation.mp,
              oldIncarnationAndVersion.incarnation.maxMp,
              oldIncarnationAndVersion.incarnation.inertia,
              oldIncarnationAndVersion.incarnation.nextActionTime,
              oldIncarnationAndVersion.incarnation.directive,
              oldIncarnationAndVersion.incarnation.details,
              oldIncarnationAndVersion.incarnation.items));
    }
    BroadcastUnitEffect(id, effect);
  }

  public void EffectUnitSetLocation(int id, Location newValue) {
    CheckHasUnit(id);
    var effect = new UnitSetLocationEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsUnit[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      oldIncarnationAndVersion.incarnation.location = newValue;
    } else {
      rootIncarnation.incarnationsUnit[id] =
        new VersionAndIncarnation<UnitIncarnation>(
          rootIncarnation.version,
          new UnitIncarnation(
              oldIncarnationAndVersion.incarnation.events,
              oldIncarnationAndVersion.incarnation.alive,
              oldIncarnationAndVersion.incarnation.lifeEndTime,
              newValue,
              oldIncarnationAndVersion.incarnation.classId,
              oldIncarnationAndVersion.incarnation.hp,
              oldIncarnationAndVersion.incarnation.maxHp,
              oldIncarnationAndVersion.incarnation.mp,
              oldIncarnationAndVersion.incarnation.maxMp,
              oldIncarnationAndVersion.incarnation.inertia,
              oldIncarnationAndVersion.incarnation.nextActionTime,
              oldIncarnationAndVersion.incarnation.directive,
              oldIncarnationAndVersion.incarnation.details,
              oldIncarnationAndVersion.incarnation.items));
    }
    BroadcastUnitEffect(id, effect);
  }

  public void EffectUnitSetHp(int id, int newValue) {
    CheckHasUnit(id);
    var effect = new UnitSetHpEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsUnit[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      oldIncarnationAndVersion.incarnation.hp = newValue;
    } else {
      rootIncarnation.incarnationsUnit[id] =
        new VersionAndIncarnation<UnitIncarnation>(
          rootIncarnation.version,
          new UnitIncarnation(
              oldIncarnationAndVersion.incarnation.events,
              oldIncarnationAndVersion.incarnation.alive,
              oldIncarnationAndVersion.incarnation.lifeEndTime,
              oldIncarnationAndVersion.incarnation.location,
              oldIncarnationAndVersion.incarnation.classId,
              newValue,
              oldIncarnationAndVersion.incarnation.maxHp,
              oldIncarnationAndVersion.incarnation.mp,
              oldIncarnationAndVersion.incarnation.maxMp,
              oldIncarnationAndVersion.incarnation.inertia,
              oldIncarnationAndVersion.incarnation.nextActionTime,
              oldIncarnationAndVersion.incarnation.directive,
              oldIncarnationAndVersion.incarnation.details,
              oldIncarnationAndVersion.incarnation.items));
    }
    BroadcastUnitEffect(id, effect);
  }

  public void EffectUnitSetMp(int id, int newValue) {
    CheckHasUnit(id);
    var effect = new UnitSetMpEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsUnit[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      oldIncarnationAndVersion.incarnation.mp = newValue;
    } else {
      rootIncarnation.incarnationsUnit[id] =
        new VersionAndIncarnation<UnitIncarnation>(
          rootIncarnation.version,
          new UnitIncarnation(
              oldIncarnationAndVersion.incarnation.events,
              oldIncarnationAndVersion.incarnation.alive,
              oldIncarnationAndVersion.incarnation.lifeEndTime,
              oldIncarnationAndVersion.incarnation.location,
              oldIncarnationAndVersion.incarnation.classId,
              oldIncarnationAndVersion.incarnation.hp,
              oldIncarnationAndVersion.incarnation.maxHp,
              newValue,
              oldIncarnationAndVersion.incarnation.maxMp,
              oldIncarnationAndVersion.incarnation.inertia,
              oldIncarnationAndVersion.incarnation.nextActionTime,
              oldIncarnationAndVersion.incarnation.directive,
              oldIncarnationAndVersion.incarnation.details,
              oldIncarnationAndVersion.incarnation.items));
    }
    BroadcastUnitEffect(id, effect);
  }

  public void EffectUnitSetNextActionTime(int id, int newValue) {
    CheckHasUnit(id);
    var effect = new UnitSetNextActionTimeEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsUnit[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      oldIncarnationAndVersion.incarnation.nextActionTime = newValue;
    } else {
      rootIncarnation.incarnationsUnit[id] =
        new VersionAndIncarnation<UnitIncarnation>(
          rootIncarnation.version,
          new UnitIncarnation(
              oldIncarnationAndVersion.incarnation.events,
              oldIncarnationAndVersion.incarnation.alive,
              oldIncarnationAndVersion.incarnation.lifeEndTime,
              oldIncarnationAndVersion.incarnation.location,
              oldIncarnationAndVersion.incarnation.classId,
              oldIncarnationAndVersion.incarnation.hp,
              oldIncarnationAndVersion.incarnation.maxHp,
              oldIncarnationAndVersion.incarnation.mp,
              oldIncarnationAndVersion.incarnation.maxMp,
              oldIncarnationAndVersion.incarnation.inertia,
              newValue,
              oldIncarnationAndVersion.incarnation.directive,
              oldIncarnationAndVersion.incarnation.details,
              oldIncarnationAndVersion.incarnation.items));
    }
    BroadcastUnitEffect(id, effect);
  }

  public void EffectUnitSetDirective(int id, IDirective newValue) {
    CheckHasUnit(id);
    var effect = new UnitSetDirectiveEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsUnit[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      oldIncarnationAndVersion.incarnation.directive = newValue.id;
    } else {
      rootIncarnation.incarnationsUnit[id] =
        new VersionAndIncarnation<UnitIncarnation>(
          rootIncarnation.version,
          new UnitIncarnation(
              oldIncarnationAndVersion.incarnation.events,
              oldIncarnationAndVersion.incarnation.alive,
              oldIncarnationAndVersion.incarnation.lifeEndTime,
              oldIncarnationAndVersion.incarnation.location,
              oldIncarnationAndVersion.incarnation.classId,
              oldIncarnationAndVersion.incarnation.hp,
              oldIncarnationAndVersion.incarnation.maxHp,
              oldIncarnationAndVersion.incarnation.mp,
              oldIncarnationAndVersion.incarnation.maxMp,
              oldIncarnationAndVersion.incarnation.inertia,
              oldIncarnationAndVersion.incarnation.nextActionTime,
              newValue.id,
              oldIncarnationAndVersion.incarnation.details,
              oldIncarnationAndVersion.incarnation.items));
    }
    BroadcastUnitEffect(id, effect);
  }

  public MoveDirectiveIncarnation GetMoveDirectiveIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsMoveDirective[id].incarnation;
  }
  public bool MoveDirectiveExists(int id) {
    return rootIncarnation.incarnationsMoveDirective.ContainsKey(id);
  }
  public MoveDirective GetMoveDirective(int id) {
    return new MoveDirective(this, id);
  }
  public List<MoveDirective> AllMoveDirective() {
    List<MoveDirective> result = new List<MoveDirective>(rootIncarnation.incarnationsMoveDirective.Count);
    foreach (var gameId in rootIncarnation.incarnationsMoveDirective.Keys) {
      result.Add(new MoveDirective(this, gameId));
    }
    return result;
  }
  public IEnumerator<MoveDirective> EnumAllMoveDirective() {
    foreach (var gameId in rootIncarnation.incarnationsMoveDirective.Keys) {
      yield return GetMoveDirective(gameId);
    }
  }
  public void CheckHasMoveDirective(MoveDirective thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasMoveDirective(thing.id);
  }
  public void CheckHasMoveDirective(int id) {
    if (!rootIncarnation.incarnationsMoveDirective.ContainsKey(id)) {
      throw new System.Exception("Invalid MoveDirective!");
    }
  }
  public void AddMoveDirectiveObserver(int id, IMoveDirectiveEffectObserver observer) {
    List<IMoveDirectiveEffectObserver> obsies;
    if (!observersMoveDirective.TryGetValue(id, out obsies)) {
      obsies = new List<IMoveDirectiveEffectObserver>();
    }
    obsies.Add(observer);
    observersMoveDirective[id] = obsies;
  }

  public void RemoveMoveDirectiveObserver(int id, IMoveDirectiveEffectObserver observer) {
    if (observersMoveDirective.ContainsKey(id)) {
      var list = observersMoveDirective[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersMoveDirective.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastMoveDirectiveEffect(int id, IMoveDirectiveEffect effect) {
    if (observersMoveDirective.ContainsKey(0)) {
      foreach (var observer in new List<IMoveDirectiveEffectObserver>(observersMoveDirective[0])) {
        observer.OnMoveDirectiveEffect(effect);
      }
    }
    if (observersMoveDirective.ContainsKey(id)) {
      foreach (var observer in new List<IMoveDirectiveEffectObserver>(observersMoveDirective[id])) {
        observer.OnMoveDirectiveEffect(effect);
      }
    }
  }

  public MoveDirective EffectMoveDirectiveCreate(
      LocationMutList path) {
    CheckHasLocationMutList(path);
    var id = NewId();
    EffectInternalCreateMoveDirective(
        id,
        new MoveDirectiveIncarnation(
            path.id));
    return new MoveDirective(this, id);
  }

  public void EffectInternalCreateMoveDirective(
      int id,
      MoveDirectiveIncarnation incarnation) {
    var effect = new MoveDirectiveCreateEffect(id, incarnation);
    rootIncarnation.incarnationsMoveDirective.Add(
        id,
        new VersionAndIncarnation<MoveDirectiveIncarnation>(
            rootIncarnation.version,
            incarnation));
    BroadcastMoveDirectiveEffect(id, effect);
  }
     
public void EffectMoveDirectiveDelete(int id) {
  var effect = new MoveDirectiveDeleteEffect(id);
  BroadcastMoveDirectiveEffect(id, effect);
  rootIncarnation.incarnationsMoveDirective.Remove(id);
}
     
  public AttackDirectiveIncarnation GetAttackDirectiveIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsAttackDirective[id].incarnation;
  }
  public bool AttackDirectiveExists(int id) {
    return rootIncarnation.incarnationsAttackDirective.ContainsKey(id);
  }
  public AttackDirective GetAttackDirective(int id) {
    return new AttackDirective(this, id);
  }
  public List<AttackDirective> AllAttackDirective() {
    List<AttackDirective> result = new List<AttackDirective>(rootIncarnation.incarnationsAttackDirective.Count);
    foreach (var gameId in rootIncarnation.incarnationsAttackDirective.Keys) {
      result.Add(new AttackDirective(this, gameId));
    }
    return result;
  }
  public IEnumerator<AttackDirective> EnumAllAttackDirective() {
    foreach (var gameId in rootIncarnation.incarnationsAttackDirective.Keys) {
      yield return GetAttackDirective(gameId);
    }
  }
  public void CheckHasAttackDirective(AttackDirective thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasAttackDirective(thing.id);
  }
  public void CheckHasAttackDirective(int id) {
    if (!rootIncarnation.incarnationsAttackDirective.ContainsKey(id)) {
      throw new System.Exception("Invalid AttackDirective!");
    }
  }
  public void AddAttackDirectiveObserver(int id, IAttackDirectiveEffectObserver observer) {
    List<IAttackDirectiveEffectObserver> obsies;
    if (!observersAttackDirective.TryGetValue(id, out obsies)) {
      obsies = new List<IAttackDirectiveEffectObserver>();
    }
    obsies.Add(observer);
    observersAttackDirective[id] = obsies;
  }

  public void RemoveAttackDirectiveObserver(int id, IAttackDirectiveEffectObserver observer) {
    if (observersAttackDirective.ContainsKey(id)) {
      var list = observersAttackDirective[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersAttackDirective.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastAttackDirectiveEffect(int id, IAttackDirectiveEffect effect) {
    if (observersAttackDirective.ContainsKey(0)) {
      foreach (var observer in new List<IAttackDirectiveEffectObserver>(observersAttackDirective[0])) {
        observer.OnAttackDirectiveEffect(effect);
      }
    }
    if (observersAttackDirective.ContainsKey(id)) {
      foreach (var observer in new List<IAttackDirectiveEffectObserver>(observersAttackDirective[id])) {
        observer.OnAttackDirectiveEffect(effect);
      }
    }
  }

  public AttackDirective EffectAttackDirectiveCreate(
      Unit targetUnit,
      LocationMutList pathToLastSeenLocation) {
    CheckHasUnit(targetUnit);
    CheckHasLocationMutList(pathToLastSeenLocation);
    var id = NewId();
    EffectInternalCreateAttackDirective(
        id,
        new AttackDirectiveIncarnation(
            targetUnit.id,
            pathToLastSeenLocation.id));
    return new AttackDirective(this, id);
  }

  public void EffectInternalCreateAttackDirective(
      int id,
      AttackDirectiveIncarnation incarnation) {
    var effect = new AttackDirectiveCreateEffect(id, incarnation);
    rootIncarnation.incarnationsAttackDirective.Add(
        id,
        new VersionAndIncarnation<AttackDirectiveIncarnation>(
            rootIncarnation.version,
            incarnation));
    BroadcastAttackDirectiveEffect(id, effect);
  }
     
public void EffectAttackDirectiveDelete(int id) {
  var effect = new AttackDirectiveDeleteEffect(id);
  BroadcastAttackDirectiveEffect(id, effect);
  rootIncarnation.incarnationsAttackDirective.Remove(id);
}
     
  public DefendingDetailIncarnation GetDefendingDetailIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsDefendingDetail[id].incarnation;
  }
  public bool DefendingDetailExists(int id) {
    return rootIncarnation.incarnationsDefendingDetail.ContainsKey(id);
  }
  public DefendingDetail GetDefendingDetail(int id) {
    return new DefendingDetail(this, id);
  }
  public List<DefendingDetail> AllDefendingDetail() {
    List<DefendingDetail> result = new List<DefendingDetail>(rootIncarnation.incarnationsDefendingDetail.Count);
    foreach (var gameId in rootIncarnation.incarnationsDefendingDetail.Keys) {
      result.Add(new DefendingDetail(this, gameId));
    }
    return result;
  }
  public IEnumerator<DefendingDetail> EnumAllDefendingDetail() {
    foreach (var gameId in rootIncarnation.incarnationsDefendingDetail.Keys) {
      yield return GetDefendingDetail(gameId);
    }
  }
  public void CheckHasDefendingDetail(DefendingDetail thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasDefendingDetail(thing.id);
  }
  public void CheckHasDefendingDetail(int id) {
    if (!rootIncarnation.incarnationsDefendingDetail.ContainsKey(id)) {
      throw new System.Exception("Invalid DefendingDetail!");
    }
  }
  public void AddDefendingDetailObserver(int id, IDefendingDetailEffectObserver observer) {
    List<IDefendingDetailEffectObserver> obsies;
    if (!observersDefendingDetail.TryGetValue(id, out obsies)) {
      obsies = new List<IDefendingDetailEffectObserver>();
    }
    obsies.Add(observer);
    observersDefendingDetail[id] = obsies;
  }

  public void RemoveDefendingDetailObserver(int id, IDefendingDetailEffectObserver observer) {
    if (observersDefendingDetail.ContainsKey(id)) {
      var list = observersDefendingDetail[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersDefendingDetail.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastDefendingDetailEffect(int id, IDefendingDetailEffect effect) {
    if (observersDefendingDetail.ContainsKey(0)) {
      foreach (var observer in new List<IDefendingDetailEffectObserver>(observersDefendingDetail[0])) {
        observer.OnDefendingDetailEffect(effect);
      }
    }
    if (observersDefendingDetail.ContainsKey(id)) {
      foreach (var observer in new List<IDefendingDetailEffectObserver>(observersDefendingDetail[id])) {
        observer.OnDefendingDetailEffect(effect);
      }
    }
  }

  public DefendingDetail EffectDefendingDetailCreate(
      int power) {
    var id = NewId();
    EffectInternalCreateDefendingDetail(
        id,
        new DefendingDetailIncarnation(
            power));
    return new DefendingDetail(this, id);
  }

  public void EffectInternalCreateDefendingDetail(
      int id,
      DefendingDetailIncarnation incarnation) {
    var effect = new DefendingDetailCreateEffect(id, incarnation);
    rootIncarnation.incarnationsDefendingDetail.Add(
        id,
        new VersionAndIncarnation<DefendingDetailIncarnation>(
            rootIncarnation.version,
            incarnation));
    BroadcastDefendingDetailEffect(id, effect);
  }
     
public void EffectDefendingDetailDelete(int id) {
  var effect = new DefendingDetailDeleteEffect(id);
  BroadcastDefendingDetailEffect(id, effect);
  rootIncarnation.incarnationsDefendingDetail.Remove(id);
}
     
  public void EffectDefendingDetailSetPower(int id, int newValue) {
    CheckHasDefendingDetail(id);
    var effect = new DefendingDetailSetPowerEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsDefendingDetail[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      oldIncarnationAndVersion.incarnation.power = newValue;
    } else {
      rootIncarnation.incarnationsDefendingDetail[id] =
        new VersionAndIncarnation<DefendingDetailIncarnation>(
          rootIncarnation.version,
          new DefendingDetailIncarnation(
              newValue));
    }
    BroadcastDefendingDetailEffect(id, effect);
  }

  public ArmorIncarnation GetArmorIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsArmor[id].incarnation;
  }
  public bool ArmorExists(int id) {
    return rootIncarnation.incarnationsArmor.ContainsKey(id);
  }
  public Armor GetArmor(int id) {
    return new Armor(this, id);
  }
  public List<Armor> AllArmor() {
    List<Armor> result = new List<Armor>(rootIncarnation.incarnationsArmor.Count);
    foreach (var gameId in rootIncarnation.incarnationsArmor.Keys) {
      result.Add(new Armor(this, gameId));
    }
    return result;
  }
  public IEnumerator<Armor> EnumAllArmor() {
    foreach (var gameId in rootIncarnation.incarnationsArmor.Keys) {
      yield return GetArmor(gameId);
    }
  }
  public void CheckHasArmor(Armor thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasArmor(thing.id);
  }
  public void CheckHasArmor(int id) {
    if (!rootIncarnation.incarnationsArmor.ContainsKey(id)) {
      throw new System.Exception("Invalid Armor!");
    }
  }
  public void AddArmorObserver(int id, IArmorEffectObserver observer) {
    List<IArmorEffectObserver> obsies;
    if (!observersArmor.TryGetValue(id, out obsies)) {
      obsies = new List<IArmorEffectObserver>();
    }
    obsies.Add(observer);
    observersArmor[id] = obsies;
  }

  public void RemoveArmorObserver(int id, IArmorEffectObserver observer) {
    if (observersArmor.ContainsKey(id)) {
      var list = observersArmor[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersArmor.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastArmorEffect(int id, IArmorEffect effect) {
    if (observersArmor.ContainsKey(0)) {
      foreach (var observer in new List<IArmorEffectObserver>(observersArmor[0])) {
        observer.OnArmorEffect(effect);
      }
    }
    if (observersArmor.ContainsKey(id)) {
      foreach (var observer in new List<IArmorEffectObserver>(observersArmor[id])) {
        observer.OnArmorEffect(effect);
      }
    }
  }

  public Armor EffectArmorCreate(
) {
    var id = NewId();
    EffectInternalCreateArmor(
        id,
        new ArmorIncarnation(
));
    return new Armor(this, id);
  }

  public void EffectInternalCreateArmor(
      int id,
      ArmorIncarnation incarnation) {
    var effect = new ArmorCreateEffect(id, incarnation);
    rootIncarnation.incarnationsArmor.Add(
        id,
        new VersionAndIncarnation<ArmorIncarnation>(
            rootIncarnation.version,
            incarnation));
    BroadcastArmorEffect(id, effect);
  }
     
public void EffectArmorDelete(int id) {
  var effect = new ArmorDeleteEffect(id);
  BroadcastArmorEffect(id, effect);
  rootIncarnation.incarnationsArmor.Remove(id);
}
     
  public GlaiveIncarnation GetGlaiveIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsGlaive[id].incarnation;
  }
  public bool GlaiveExists(int id) {
    return rootIncarnation.incarnationsGlaive.ContainsKey(id);
  }
  public Glaive GetGlaive(int id) {
    return new Glaive(this, id);
  }
  public List<Glaive> AllGlaive() {
    List<Glaive> result = new List<Glaive>(rootIncarnation.incarnationsGlaive.Count);
    foreach (var gameId in rootIncarnation.incarnationsGlaive.Keys) {
      result.Add(new Glaive(this, gameId));
    }
    return result;
  }
  public IEnumerator<Glaive> EnumAllGlaive() {
    foreach (var gameId in rootIncarnation.incarnationsGlaive.Keys) {
      yield return GetGlaive(gameId);
    }
  }
  public void CheckHasGlaive(Glaive thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasGlaive(thing.id);
  }
  public void CheckHasGlaive(int id) {
    if (!rootIncarnation.incarnationsGlaive.ContainsKey(id)) {
      throw new System.Exception("Invalid Glaive!");
    }
  }
  public void AddGlaiveObserver(int id, IGlaiveEffectObserver observer) {
    List<IGlaiveEffectObserver> obsies;
    if (!observersGlaive.TryGetValue(id, out obsies)) {
      obsies = new List<IGlaiveEffectObserver>();
    }
    obsies.Add(observer);
    observersGlaive[id] = obsies;
  }

  public void RemoveGlaiveObserver(int id, IGlaiveEffectObserver observer) {
    if (observersGlaive.ContainsKey(id)) {
      var list = observersGlaive[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersGlaive.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastGlaiveEffect(int id, IGlaiveEffect effect) {
    if (observersGlaive.ContainsKey(0)) {
      foreach (var observer in new List<IGlaiveEffectObserver>(observersGlaive[0])) {
        observer.OnGlaiveEffect(effect);
      }
    }
    if (observersGlaive.ContainsKey(id)) {
      foreach (var observer in new List<IGlaiveEffectObserver>(observersGlaive[id])) {
        observer.OnGlaiveEffect(effect);
      }
    }
  }

  public Glaive EffectGlaiveCreate(
) {
    var id = NewId();
    EffectInternalCreateGlaive(
        id,
        new GlaiveIncarnation(
));
    return new Glaive(this, id);
  }

  public void EffectInternalCreateGlaive(
      int id,
      GlaiveIncarnation incarnation) {
    var effect = new GlaiveCreateEffect(id, incarnation);
    rootIncarnation.incarnationsGlaive.Add(
        id,
        new VersionAndIncarnation<GlaiveIncarnation>(
            rootIncarnation.version,
            incarnation));
    BroadcastGlaiveEffect(id, effect);
  }
     
public void EffectGlaiveDelete(int id) {
  var effect = new GlaiveDeleteEffect(id);
  BroadcastGlaiveEffect(id, effect);
  rootIncarnation.incarnationsGlaive.Remove(id);
}
     
  public ExecutionStateIncarnation GetExecutionStateIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsExecutionState[id].incarnation;
  }
  public bool ExecutionStateExists(int id) {
    return rootIncarnation.incarnationsExecutionState.ContainsKey(id);
  }
  public ExecutionState GetExecutionState(int id) {
    return new ExecutionState(this, id);
  }
  public List<ExecutionState> AllExecutionState() {
    List<ExecutionState> result = new List<ExecutionState>(rootIncarnation.incarnationsExecutionState.Count);
    foreach (var gameId in rootIncarnation.incarnationsExecutionState.Keys) {
      result.Add(new ExecutionState(this, gameId));
    }
    return result;
  }
  public IEnumerator<ExecutionState> EnumAllExecutionState() {
    foreach (var gameId in rootIncarnation.incarnationsExecutionState.Keys) {
      yield return GetExecutionState(gameId);
    }
  }
  public void CheckHasExecutionState(ExecutionState thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasExecutionState(thing.id);
  }
  public void CheckHasExecutionState(int id) {
    if (!rootIncarnation.incarnationsExecutionState.ContainsKey(id)) {
      throw new System.Exception("Invalid ExecutionState!");
    }
  }
  public void AddExecutionStateObserver(int id, IExecutionStateEffectObserver observer) {
    List<IExecutionStateEffectObserver> obsies;
    if (!observersExecutionState.TryGetValue(id, out obsies)) {
      obsies = new List<IExecutionStateEffectObserver>();
    }
    obsies.Add(observer);
    observersExecutionState[id] = obsies;
  }

  public void RemoveExecutionStateObserver(int id, IExecutionStateEffectObserver observer) {
    if (observersExecutionState.ContainsKey(id)) {
      var list = observersExecutionState[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersExecutionState.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastExecutionStateEffect(int id, IExecutionStateEffect effect) {
    if (observersExecutionState.ContainsKey(0)) {
      foreach (var observer in new List<IExecutionStateEffectObserver>(observersExecutionState[0])) {
        observer.OnExecutionStateEffect(effect);
      }
    }
    if (observersExecutionState.ContainsKey(id)) {
      foreach (var observer in new List<IExecutionStateEffectObserver>(observersExecutionState[id])) {
        observer.OnExecutionStateEffect(effect);
      }
    }
  }

  public ExecutionState EffectExecutionStateCreate(
      Unit actingUnit,
      bool actingUnitDidAction,
      IDetailMutList remainingPreActingDetails,
      IDetailMutList remainingPostActingDetails) {
    var id = NewId();
    EffectInternalCreateExecutionState(
        id,
        new ExecutionStateIncarnation(
            actingUnit.id,
            actingUnitDidAction,
            remainingPreActingDetails.id,
            remainingPostActingDetails.id));
    return new ExecutionState(this, id);
  }

  public void EffectInternalCreateExecutionState(
      int id,
      ExecutionStateIncarnation incarnation) {
    var effect = new ExecutionStateCreateEffect(id, incarnation);
    rootIncarnation.incarnationsExecutionState.Add(
        id,
        new VersionAndIncarnation<ExecutionStateIncarnation>(
            rootIncarnation.version,
            incarnation));
    BroadcastExecutionStateEffect(id, effect);
  }
     
public void EffectExecutionStateDelete(int id) {
  var effect = new ExecutionStateDeleteEffect(id);
  BroadcastExecutionStateEffect(id, effect);
  rootIncarnation.incarnationsExecutionState.Remove(id);
}
     
  public void EffectExecutionStateSetActingUnit(int id, Unit newValue) {
    CheckHasExecutionState(id);
    var effect = new ExecutionStateSetActingUnitEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsExecutionState[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      oldIncarnationAndVersion.incarnation.actingUnit = newValue.id;
    } else {
      rootIncarnation.incarnationsExecutionState[id] =
        new VersionAndIncarnation<ExecutionStateIncarnation>(
          rootIncarnation.version,
          new ExecutionStateIncarnation(
              newValue.id,
              oldIncarnationAndVersion.incarnation.actingUnitDidAction,
              oldIncarnationAndVersion.incarnation.remainingPreActingDetails,
              oldIncarnationAndVersion.incarnation.remainingPostActingDetails));
    }
    BroadcastExecutionStateEffect(id, effect);
  }

  public void EffectExecutionStateSetActingUnitDidAction(int id, bool newValue) {
    CheckHasExecutionState(id);
    var effect = new ExecutionStateSetActingUnitDidActionEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsExecutionState[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      oldIncarnationAndVersion.incarnation.actingUnitDidAction = newValue;
    } else {
      rootIncarnation.incarnationsExecutionState[id] =
        new VersionAndIncarnation<ExecutionStateIncarnation>(
          rootIncarnation.version,
          new ExecutionStateIncarnation(
              oldIncarnationAndVersion.incarnation.actingUnit,
              newValue,
              oldIncarnationAndVersion.incarnation.remainingPreActingDetails,
              oldIncarnationAndVersion.incarnation.remainingPostActingDetails));
    }
    BroadcastExecutionStateEffect(id, effect);
  }

  public void EffectExecutionStateSetRemainingPreActingDetails(int id, IDetailMutList newValue) {
    CheckHasExecutionState(id);
    var effect = new ExecutionStateSetRemainingPreActingDetailsEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsExecutionState[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      oldIncarnationAndVersion.incarnation.remainingPreActingDetails = newValue.id;
    } else {
      rootIncarnation.incarnationsExecutionState[id] =
        new VersionAndIncarnation<ExecutionStateIncarnation>(
          rootIncarnation.version,
          new ExecutionStateIncarnation(
              oldIncarnationAndVersion.incarnation.actingUnit,
              oldIncarnationAndVersion.incarnation.actingUnitDidAction,
              newValue.id,
              oldIncarnationAndVersion.incarnation.remainingPostActingDetails));
    }
    BroadcastExecutionStateEffect(id, effect);
  }

  public void EffectExecutionStateSetRemainingPostActingDetails(int id, IDetailMutList newValue) {
    CheckHasExecutionState(id);
    var effect = new ExecutionStateSetRemainingPostActingDetailsEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsExecutionState[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      oldIncarnationAndVersion.incarnation.remainingPostActingDetails = newValue.id;
    } else {
      rootIncarnation.incarnationsExecutionState[id] =
        new VersionAndIncarnation<ExecutionStateIncarnation>(
          rootIncarnation.version,
          new ExecutionStateIncarnation(
              oldIncarnationAndVersion.incarnation.actingUnit,
              oldIncarnationAndVersion.incarnation.actingUnitDidAction,
              oldIncarnationAndVersion.incarnation.remainingPreActingDetails,
              newValue.id));
    }
    BroadcastExecutionStateEffect(id, effect);
  }

  public IDirective GetIDirective(int id) {

    if (rootIncarnation.incarnationsMoveDirective.ContainsKey(id)) {
      return new MoveDirectiveAsIDirective(new MoveDirective(this, id));
    }

    if (rootIncarnation.incarnationsAttackDirective.ContainsKey(id)) {
      return new AttackDirectiveAsIDirective(new AttackDirective(this, id));
    }

    return NullIDirective.Null;
  }
  public void CheckHasIDirective(IDirective thing) {
    GetIDirective(thing.id);
  }
  public void CheckHasIDirective(int id) {
    GetIDirective(id);
  }

  public IDetail GetIDetail(int id) {

    if (rootIncarnation.incarnationsDefendingDetail.ContainsKey(id)) {
      return new DefendingDetailAsIDetail(new DefendingDetail(this, id));
    }

    return NullIDetail.Null;
  }
  public void CheckHasIDetail(IDetail thing) {
    GetIDetail(thing.id);
  }
  public void CheckHasIDetail(int id) {
    GetIDetail(id);
  }

  public IItem GetIItem(int id) {

    if (rootIncarnation.incarnationsArmor.ContainsKey(id)) {
      return new ArmorAsIItem(new Armor(this, id));
    }

    if (rootIncarnation.incarnationsGlaive.ContainsKey(id)) {
      return new GlaiveAsIItem(new Glaive(this, id));
    }

    return NullIItem.Null;
  }
  public void CheckHasIItem(IItem thing) {
    GetIItem(thing.id);
  }
  public void CheckHasIItem(int id) {
    GetIItem(id);
  }

    public LevelMutBunchIncarnation GetLevelMutBunchIncarnation(int id) {
      return rootIncarnation.incarnationsLevelMutBunch[id].incarnation;
    }
    public LevelMutBunch GetLevelMutBunch(int id) {
      return new LevelMutBunch(this, id);
    }
    public void CheckHasLevelMutBunch(LevelMutBunch thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasLevelMutBunch(thing.id);
    }
    public void CheckHasLevelMutBunch(int id) {
      if (!rootIncarnation.incarnationsLevelMutBunch.ContainsKey(id)) {
        throw new System.Exception("Invalid LevelMutBunch}!");
      }
    }
    public LevelMutBunch EffectLevelMutBunchCreate() {
      var id = NewId();
      EffectInternalCreateLevelMutBunch(id, new LevelMutBunchIncarnation(new SortedSet<int>()));
      return new LevelMutBunch(this, id);
    }
    public void EffectInternalCreateLevelMutBunch(int id, LevelMutBunchIncarnation incarnation) {
      var effect = new LevelMutBunchCreateEffect(id, incarnation);
      rootIncarnation.incarnationsLevelMutBunch
          .Add(
              id,
              new VersionAndIncarnation<LevelMutBunchIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastLevelMutBunchEffect(id, effect);
    }
    public void EffectLevelMutBunchDelete(int id) {
      var effect = new LevelMutBunchDeleteEffect(id);
      BroadcastLevelMutBunchEffect(id, effect);
      rootIncarnation.incarnationsLevelMutBunch.Remove(id);
    }
    public void EffectLevelMutBunchAdd(int bunchId, int elementId) {
      CheckHasLevelMutBunch(bunchId);
      CheckHasLevel(elementId);

      var effect = new LevelMutBunchAddEffect(bunchId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsLevelMutBunch[bunchId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new LevelMutBunchIncarnation(newMap);
        rootIncarnation.incarnationsLevelMutBunch[bunchId] =
            new VersionAndIncarnation<LevelMutBunchIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      BroadcastLevelMutBunchEffect(bunchId, effect);
    }
    public void EffectLevelMutBunchRemove(int bunchId, int elementId) {
      CheckHasLevelMutBunch(bunchId);
      CheckHasLevel(elementId);

      var effect = new LevelMutBunchRemoveEffect(bunchId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsLevelMutBunch[bunchId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        rootIncarnation.incarnationsLevelMutBunch[bunchId] =
            new VersionAndIncarnation<LevelMutBunchIncarnation>(
                rootIncarnation.version,
                new LevelMutBunchIncarnation(newMap));
      }
      BroadcastLevelMutBunchEffect(bunchId, effect);
    }
    public void AddLevelMutBunchObserver(int id, ILevelMutBunchEffectObserver observer) {
      List<ILevelMutBunchEffectObserver> obsies;
      if (!observersLevelMutBunch.TryGetValue(id, out obsies)) {
        obsies = new List<ILevelMutBunchEffectObserver>();
      }
      obsies.Add(observer);
      observersLevelMutBunch[id] = obsies;
    }

    public void RemoveLevelMutBunchObserver(int id, ILevelMutBunchEffectObserver observer) {
      if (observersLevelMutBunch.ContainsKey(id)) {
        var list = observersLevelMutBunch[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersLevelMutBunch.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
    public void BroadcastLevelMutBunchEffect(int id, ILevelMutBunchEffect effect) {
      if (observersLevelMutBunch.ContainsKey(0)) {
        foreach (var observer in new List<ILevelMutBunchEffectObserver>(observersLevelMutBunch[0])) {
          observer.OnLevelMutBunchEffect(effect);
        }
      }
      if (observersLevelMutBunch.ContainsKey(id)) {
        foreach (var observer in new List<ILevelMutBunchEffectObserver>(observersLevelMutBunch[id])) {
          observer.OnLevelMutBunchEffect(effect);
        }
      }
    }

    public UnitMutBunchIncarnation GetUnitMutBunchIncarnation(int id) {
      return rootIncarnation.incarnationsUnitMutBunch[id].incarnation;
    }
    public UnitMutBunch GetUnitMutBunch(int id) {
      return new UnitMutBunch(this, id);
    }
    public void CheckHasUnitMutBunch(UnitMutBunch thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasUnitMutBunch(thing.id);
    }
    public void CheckHasUnitMutBunch(int id) {
      if (!rootIncarnation.incarnationsUnitMutBunch.ContainsKey(id)) {
        throw new System.Exception("Invalid UnitMutBunch}!");
      }
    }
    public UnitMutBunch EffectUnitMutBunchCreate() {
      var id = NewId();
      EffectInternalCreateUnitMutBunch(id, new UnitMutBunchIncarnation(new SortedSet<int>()));
      return new UnitMutBunch(this, id);
    }
    public void EffectInternalCreateUnitMutBunch(int id, UnitMutBunchIncarnation incarnation) {
      var effect = new UnitMutBunchCreateEffect(id, incarnation);
      rootIncarnation.incarnationsUnitMutBunch
          .Add(
              id,
              new VersionAndIncarnation<UnitMutBunchIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastUnitMutBunchEffect(id, effect);
    }
    public void EffectUnitMutBunchDelete(int id) {
      var effect = new UnitMutBunchDeleteEffect(id);
      BroadcastUnitMutBunchEffect(id, effect);
      rootIncarnation.incarnationsUnitMutBunch.Remove(id);
    }
    public void EffectUnitMutBunchAdd(int bunchId, int elementId) {
      CheckHasUnitMutBunch(bunchId);
      CheckHasUnit(elementId);

      var effect = new UnitMutBunchAddEffect(bunchId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsUnitMutBunch[bunchId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new UnitMutBunchIncarnation(newMap);
        rootIncarnation.incarnationsUnitMutBunch[bunchId] =
            new VersionAndIncarnation<UnitMutBunchIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      BroadcastUnitMutBunchEffect(bunchId, effect);
    }
    public void EffectUnitMutBunchRemove(int bunchId, int elementId) {
      CheckHasUnitMutBunch(bunchId);
      CheckHasUnit(elementId);

      var effect = new UnitMutBunchRemoveEffect(bunchId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsUnitMutBunch[bunchId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        rootIncarnation.incarnationsUnitMutBunch[bunchId] =
            new VersionAndIncarnation<UnitMutBunchIncarnation>(
                rootIncarnation.version,
                new UnitMutBunchIncarnation(newMap));
      }
      BroadcastUnitMutBunchEffect(bunchId, effect);
    }
    public void AddUnitMutBunchObserver(int id, IUnitMutBunchEffectObserver observer) {
      List<IUnitMutBunchEffectObserver> obsies;
      if (!observersUnitMutBunch.TryGetValue(id, out obsies)) {
        obsies = new List<IUnitMutBunchEffectObserver>();
      }
      obsies.Add(observer);
      observersUnitMutBunch[id] = obsies;
    }

    public void RemoveUnitMutBunchObserver(int id, IUnitMutBunchEffectObserver observer) {
      if (observersUnitMutBunch.ContainsKey(id)) {
        var list = observersUnitMutBunch[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersUnitMutBunch.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
    public void BroadcastUnitMutBunchEffect(int id, IUnitMutBunchEffect effect) {
      if (observersUnitMutBunch.ContainsKey(0)) {
        foreach (var observer in new List<IUnitMutBunchEffectObserver>(observersUnitMutBunch[0])) {
          observer.OnUnitMutBunchEffect(effect);
        }
      }
      if (observersUnitMutBunch.ContainsKey(id)) {
        foreach (var observer in new List<IUnitMutBunchEffectObserver>(observersUnitMutBunch[id])) {
          observer.OnUnitMutBunchEffect(effect);
        }
      }
    }

    public IItemMutBunchIncarnation GetIItemMutBunchIncarnation(int id) {
      return rootIncarnation.incarnationsIItemMutBunch[id].incarnation;
    }
    public IItemMutBunch GetIItemMutBunch(int id) {
      return new IItemMutBunch(this, id);
    }
    public void CheckHasIItemMutBunch(IItemMutBunch thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasIItemMutBunch(thing.id);
    }
    public void CheckHasIItemMutBunch(int id) {
      if (!rootIncarnation.incarnationsIItemMutBunch.ContainsKey(id)) {
        throw new System.Exception("Invalid IItemMutBunch}!");
      }
    }
    public IItemMutBunch EffectIItemMutBunchCreate() {
      var id = NewId();
      EffectInternalCreateIItemMutBunch(id, new IItemMutBunchIncarnation(new SortedSet<int>()));
      return new IItemMutBunch(this, id);
    }
    public void EffectInternalCreateIItemMutBunch(int id, IItemMutBunchIncarnation incarnation) {
      var effect = new IItemMutBunchCreateEffect(id, incarnation);
      rootIncarnation.incarnationsIItemMutBunch
          .Add(
              id,
              new VersionAndIncarnation<IItemMutBunchIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastIItemMutBunchEffect(id, effect);
    }
    public void EffectIItemMutBunchDelete(int id) {
      var effect = new IItemMutBunchDeleteEffect(id);
      BroadcastIItemMutBunchEffect(id, effect);
      rootIncarnation.incarnationsIItemMutBunch.Remove(id);
    }
    public void EffectIItemMutBunchAdd(int bunchId, int elementId) {
      CheckHasIItemMutBunch(bunchId);
      CheckHasIItem(elementId);

      var effect = new IItemMutBunchAddEffect(bunchId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsIItemMutBunch[bunchId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new IItemMutBunchIncarnation(newMap);
        rootIncarnation.incarnationsIItemMutBunch[bunchId] =
            new VersionAndIncarnation<IItemMutBunchIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      BroadcastIItemMutBunchEffect(bunchId, effect);
    }
    public void EffectIItemMutBunchRemove(int bunchId, int elementId) {
      CheckHasIItemMutBunch(bunchId);
      CheckHasIItem(elementId);

      var effect = new IItemMutBunchRemoveEffect(bunchId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsIItemMutBunch[bunchId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        rootIncarnation.incarnationsIItemMutBunch[bunchId] =
            new VersionAndIncarnation<IItemMutBunchIncarnation>(
                rootIncarnation.version,
                new IItemMutBunchIncarnation(newMap));
      }
      BroadcastIItemMutBunchEffect(bunchId, effect);
    }
    public void AddIItemMutBunchObserver(int id, IIItemMutBunchEffectObserver observer) {
      List<IIItemMutBunchEffectObserver> obsies;
      if (!observersIItemMutBunch.TryGetValue(id, out obsies)) {
        obsies = new List<IIItemMutBunchEffectObserver>();
      }
      obsies.Add(observer);
      observersIItemMutBunch[id] = obsies;
    }

    public void RemoveIItemMutBunchObserver(int id, IIItemMutBunchEffectObserver observer) {
      if (observersIItemMutBunch.ContainsKey(id)) {
        var list = observersIItemMutBunch[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersIItemMutBunch.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
    public void BroadcastIItemMutBunchEffect(int id, IIItemMutBunchEffect effect) {
      if (observersIItemMutBunch.ContainsKey(0)) {
        foreach (var observer in new List<IIItemMutBunchEffectObserver>(observersIItemMutBunch[0])) {
          observer.OnIItemMutBunchEffect(effect);
        }
      }
      if (observersIItemMutBunch.ContainsKey(id)) {
        foreach (var observer in new List<IIItemMutBunchEffectObserver>(observersIItemMutBunch[id])) {
          observer.OnIItemMutBunchEffect(effect);
        }
      }
    }

    public LocationMutListIncarnation GetLocationMutListIncarnation(int id) {
      return rootIncarnation.incarnationsLocationMutList[id].incarnation;
    }
    public LocationMutList GetLocationMutList(int id) {
      return new LocationMutList(this, id);
    }
    public bool LocationMutListExists(int id) {
      return rootIncarnation.incarnationsLocationMutList.ContainsKey(id);
    }
    public void CheckHasLocationMutList(LocationMutList thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasLocationMutList(thing.id);
    }
    public void CheckHasLocationMutList(int id) {
      if (!rootIncarnation.incarnationsLocationMutList.ContainsKey(id)) {
        throw new System.Exception("Invalid LocationMutList}!");
      }
    }
    public LocationMutList EffectLocationMutListCreate() {
      var id = NewId();
      EffectInternalCreateLocationMutList(id, new LocationMutListIncarnation(new List<Location>()));
      return new LocationMutList(this, id);
    }
    public LocationMutList EffectLocationMutListCreate(List<Location> elements) {
      var id = NewId();

      EffectInternalCreateLocationMutList(id, new LocationMutListIncarnation(new List<Location>(elements)));

      return new LocationMutList(this, id);
    }
    public void EffectInternalCreateLocationMutList(int id, LocationMutListIncarnation incarnation) {
      var effect = new LocationMutListCreateEffect(id, incarnation);
      rootIncarnation.incarnationsLocationMutList
          .Add(
              id,
              new VersionAndIncarnation<LocationMutListIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastLocationMutListEffect(id, effect);
    }
    public void EffectLocationMutListDelete(int id) {
      var effect = new LocationMutListDeleteEffect(id);
      BroadcastLocationMutListEffect(id, effect);
      rootIncarnation.incarnationsLocationMutList.Remove(id);
    }
    public void EffectLocationMutListAdd(int listId, Location element) {
      CheckHasLocationMutList(listId);

    
      var effect = new LocationMutListAddEffect(listId, element);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsLocationMutList[listId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.list.Add(element);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.list;
        var newMap = new List<Location>(oldMap);
        newMap.Add(element);
        var newIncarnation = new LocationMutListIncarnation(newMap);
        rootIncarnation.incarnationsLocationMutList[listId] =
            new VersionAndIncarnation<LocationMutListIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      BroadcastLocationMutListEffect(listId, effect);
    }
    public void EffectLocationMutListRemoveAt(int listId, int index) {
      CheckHasLocationMutList(listId);

      var effect = new LocationMutListRemoveEffect(listId, index);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsLocationMutList[listId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.list.RemoveAt(index);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.list;
        var newMap = new List<Location>(oldMap);
        newMap.RemoveAt(index);
        rootIncarnation.incarnationsLocationMutList[listId] =
            new VersionAndIncarnation<LocationMutListIncarnation>(
                rootIncarnation.version,
                new LocationMutListIncarnation(newMap));
      }
      BroadcastLocationMutListEffect(listId, effect);
    }
    public void AddLocationMutListObserver(int id, ILocationMutListEffectObserver observer) {
      List<ILocationMutListEffectObserver> obsies;
      if (!observersLocationMutList.TryGetValue(id, out obsies)) {
        obsies = new List<ILocationMutListEffectObserver>();
      }
      obsies.Add(observer);
      observersLocationMutList[id] = obsies;
    }

    public void RemoveLocationMutListObserver(int id, ILocationMutListEffectObserver observer) {
      if (observersLocationMutList.ContainsKey(id)) {
        var list = observersLocationMutList[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersLocationMutList.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void BroadcastLocationMutListEffect(int id, ILocationMutListEffect effect) {
      if (observersLocationMutList.ContainsKey(0)) {
        foreach (var observer in new List<ILocationMutListEffectObserver>(observersLocationMutList[0])) {
          observer.OnLocationMutListEffect(effect);
        }
      }
      if (observersLocationMutList.ContainsKey(id)) {
        foreach (var observer in new List<ILocationMutListEffectObserver>(observersLocationMutList[id])) {
          observer.OnLocationMutListEffect(effect);
        }
      }
    }

    public IUnitEventMutListIncarnation GetIUnitEventMutListIncarnation(int id) {
      return rootIncarnation.incarnationsIUnitEventMutList[id].incarnation;
    }
    public IUnitEventMutList GetIUnitEventMutList(int id) {
      return new IUnitEventMutList(this, id);
    }
    public bool IUnitEventMutListExists(int id) {
      return rootIncarnation.incarnationsIUnitEventMutList.ContainsKey(id);
    }
    public void CheckHasIUnitEventMutList(IUnitEventMutList thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasIUnitEventMutList(thing.id);
    }
    public void CheckHasIUnitEventMutList(int id) {
      if (!rootIncarnation.incarnationsIUnitEventMutList.ContainsKey(id)) {
        throw new System.Exception("Invalid IUnitEventMutList}!");
      }
    }
    public IUnitEventMutList EffectIUnitEventMutListCreate() {
      var id = NewId();
      EffectInternalCreateIUnitEventMutList(id, new IUnitEventMutListIncarnation(new List<IUnitEvent>()));
      return new IUnitEventMutList(this, id);
    }
    public IUnitEventMutList EffectIUnitEventMutListCreate(List<IUnitEvent> elements) {
      var id = NewId();

      EffectInternalCreateIUnitEventMutList(id, new IUnitEventMutListIncarnation(new List<IUnitEvent>(elements)));

      return new IUnitEventMutList(this, id);
    }
    public void EffectInternalCreateIUnitEventMutList(int id, IUnitEventMutListIncarnation incarnation) {
      var effect = new IUnitEventMutListCreateEffect(id, incarnation);
      rootIncarnation.incarnationsIUnitEventMutList
          .Add(
              id,
              new VersionAndIncarnation<IUnitEventMutListIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastIUnitEventMutListEffect(id, effect);
    }
    public void EffectIUnitEventMutListDelete(int id) {
      var effect = new IUnitEventMutListDeleteEffect(id);
      BroadcastIUnitEventMutListEffect(id, effect);
      rootIncarnation.incarnationsIUnitEventMutList.Remove(id);
    }
    public void EffectIUnitEventMutListAdd(int listId, IUnitEvent element) {
      CheckHasIUnitEventMutList(listId);

    
      var effect = new IUnitEventMutListAddEffect(listId, element);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsIUnitEventMutList[listId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.list.Add(element);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.list;
        var newMap = new List<IUnitEvent>(oldMap);
        newMap.Add(element);
        var newIncarnation = new IUnitEventMutListIncarnation(newMap);
        rootIncarnation.incarnationsIUnitEventMutList[listId] =
            new VersionAndIncarnation<IUnitEventMutListIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      BroadcastIUnitEventMutListEffect(listId, effect);
    }
    public void EffectIUnitEventMutListRemoveAt(int listId, int index) {
      CheckHasIUnitEventMutList(listId);

      var effect = new IUnitEventMutListRemoveEffect(listId, index);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsIUnitEventMutList[listId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.list.RemoveAt(index);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.list;
        var newMap = new List<IUnitEvent>(oldMap);
        newMap.RemoveAt(index);
        rootIncarnation.incarnationsIUnitEventMutList[listId] =
            new VersionAndIncarnation<IUnitEventMutListIncarnation>(
                rootIncarnation.version,
                new IUnitEventMutListIncarnation(newMap));
      }
      BroadcastIUnitEventMutListEffect(listId, effect);
    }
    public void AddIUnitEventMutListObserver(int id, IIUnitEventMutListEffectObserver observer) {
      List<IIUnitEventMutListEffectObserver> obsies;
      if (!observersIUnitEventMutList.TryGetValue(id, out obsies)) {
        obsies = new List<IIUnitEventMutListEffectObserver>();
      }
      obsies.Add(observer);
      observersIUnitEventMutList[id] = obsies;
    }

    public void RemoveIUnitEventMutListObserver(int id, IIUnitEventMutListEffectObserver observer) {
      if (observersIUnitEventMutList.ContainsKey(id)) {
        var list = observersIUnitEventMutList[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersIUnitEventMutList.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void BroadcastIUnitEventMutListEffect(int id, IIUnitEventMutListEffect effect) {
      if (observersIUnitEventMutList.ContainsKey(0)) {
        foreach (var observer in new List<IIUnitEventMutListEffectObserver>(observersIUnitEventMutList[0])) {
          observer.OnIUnitEventMutListEffect(effect);
        }
      }
      if (observersIUnitEventMutList.ContainsKey(id)) {
        foreach (var observer in new List<IIUnitEventMutListEffectObserver>(observersIUnitEventMutList[id])) {
          observer.OnIUnitEventMutListEffect(effect);
        }
      }
    }

    public IDetailMutListIncarnation GetIDetailMutListIncarnation(int id) {
      return rootIncarnation.incarnationsIDetailMutList[id].incarnation;
    }
    public IDetailMutList GetIDetailMutList(int id) {
      return new IDetailMutList(this, id);
    }
    public bool IDetailMutListExists(int id) {
      return rootIncarnation.incarnationsIDetailMutList.ContainsKey(id);
    }
    public void CheckHasIDetailMutList(IDetailMutList thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasIDetailMutList(thing.id);
    }
    public void CheckHasIDetailMutList(int id) {
      if (!rootIncarnation.incarnationsIDetailMutList.ContainsKey(id)) {
        throw new System.Exception("Invalid IDetailMutList}!");
      }
    }
    public IDetailMutList EffectIDetailMutListCreate() {
      var id = NewId();
      EffectInternalCreateIDetailMutList(id, new IDetailMutListIncarnation(new List<int>()));
      return new IDetailMutList(this, id);
    }
    public IDetailMutList EffectIDetailMutListCreate(List<IDetail> elements) {
      var id = NewId();

      var elementsIds = new List<int>();
      foreach (var element in elements) {
        elementsIds.Add(element.id);
      }
      EffectInternalCreateIDetailMutList(id, new IDetailMutListIncarnation(elementsIds));

      return new IDetailMutList(this, id);
    }
    public void EffectInternalCreateIDetailMutList(int id, IDetailMutListIncarnation incarnation) {
      var effect = new IDetailMutListCreateEffect(id, incarnation);
      rootIncarnation.incarnationsIDetailMutList
          .Add(
              id,
              new VersionAndIncarnation<IDetailMutListIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastIDetailMutListEffect(id, effect);
    }
    public void EffectIDetailMutListDelete(int id) {
      var effect = new IDetailMutListDeleteEffect(id);
      BroadcastIDetailMutListEffect(id, effect);
      rootIncarnation.incarnationsIDetailMutList.Remove(id);
    }
    public void EffectIDetailMutListAdd(int listId, int element) {
      CheckHasIDetailMutList(listId);

          CheckHasIDetail(element);
      var effect = new IDetailMutListAddEffect(listId, element);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsIDetailMutList[listId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.list.Add(element);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.list;
        var newMap = new List<int>(oldMap);
        newMap.Add(element);
        var newIncarnation = new IDetailMutListIncarnation(newMap);
        rootIncarnation.incarnationsIDetailMutList[listId] =
            new VersionAndIncarnation<IDetailMutListIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      BroadcastIDetailMutListEffect(listId, effect);
    }
    public void EffectIDetailMutListRemoveAt(int listId, int index) {
      CheckHasIDetailMutList(listId);

      var effect = new IDetailMutListRemoveEffect(listId, index);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsIDetailMutList[listId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.list.RemoveAt(index);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.list;
        var newMap = new List<int>(oldMap);
        newMap.RemoveAt(index);
        rootIncarnation.incarnationsIDetailMutList[listId] =
            new VersionAndIncarnation<IDetailMutListIncarnation>(
                rootIncarnation.version,
                new IDetailMutListIncarnation(newMap));
      }
      BroadcastIDetailMutListEffect(listId, effect);
    }
    public void AddIDetailMutListObserver(int id, IIDetailMutListEffectObserver observer) {
      List<IIDetailMutListEffectObserver> obsies;
      if (!observersIDetailMutList.TryGetValue(id, out obsies)) {
        obsies = new List<IIDetailMutListEffectObserver>();
      }
      obsies.Add(observer);
      observersIDetailMutList[id] = obsies;
    }

    public void RemoveIDetailMutListObserver(int id, IIDetailMutListEffectObserver observer) {
      if (observersIDetailMutList.ContainsKey(id)) {
        var list = observersIDetailMutList[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersIDetailMutList.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void BroadcastIDetailMutListEffect(int id, IIDetailMutListEffect effect) {
      if (observersIDetailMutList.ContainsKey(0)) {
        foreach (var observer in new List<IIDetailMutListEffectObserver>(observersIDetailMutList[0])) {
          observer.OnIDetailMutListEffect(effect);
        }
      }
      if (observersIDetailMutList.ContainsKey(id)) {
        foreach (var observer in new List<IIDetailMutListEffectObserver>(observersIDetailMutList[id])) {
          observer.OnIDetailMutListEffect(effect);
        }
      }
    }

    public TerrainTileByLocationMutMapIncarnation GetTerrainTileByLocationMutMapIncarnation(int id) {
      return rootIncarnation.incarnationsTerrainTileByLocationMutMap[id].incarnation;
    }
    public TerrainTileByLocationMutMap GetTerrainTileByLocationMutMap(int id) {
      return new TerrainTileByLocationMutMap(this, id);
    }
    public void CheckHasTerrainTileByLocationMutMap(TerrainTileByLocationMutMap thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasTerrainTileByLocationMutMap(thing.id);
    }
    public void CheckHasTerrainTileByLocationMutMap(int id) {
      if (!rootIncarnation.incarnationsTerrainTileByLocationMutMap.ContainsKey(id)) {
        throw new System.Exception("Invalid TerrainTileByLocationMutMap}!");
      }
    }
    public TerrainTileByLocationMutMap EffectTerrainTileByLocationMutMapCreate() {
      var id = NewId();
      EffectInternalCreateTerrainTileByLocationMutMap(
          id,
          new TerrainTileByLocationMutMapIncarnation(
              new SortedDictionary<Location, int>()));
      return new TerrainTileByLocationMutMap(this, id);
    }
    public void EffectInternalCreateTerrainTileByLocationMutMap(int id, TerrainTileByLocationMutMapIncarnation incarnation) {
      var effect = new TerrainTileByLocationMutMapCreateEffect(id, incarnation);
      rootIncarnation.incarnationsTerrainTileByLocationMutMap
          .Add(
              id,
              new VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastTerrainTileByLocationMutMapEffect(id, effect);
    }
    public void EffectTerrainTileByLocationMutMapDelete(int id) {
      var effect = new TerrainTileByLocationMutMapDeleteEffect(id);
      BroadcastTerrainTileByLocationMutMapEffect(id, effect);
      rootIncarnation.incarnationsTerrainTileByLocationMutMap.Remove(id);
    }
    public void EffectTerrainTileByLocationMutMapAdd(int mapId, Location key, int value) {
      CheckHasTerrainTileByLocationMutMap(mapId);
      CheckHasTerrainTile(value);

      var effect = new TerrainTileByLocationMutMapAddEffect(mapId, key, value);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsTerrainTileByLocationMutMap[mapId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.map.Add(key, value);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.map;
        var newMap = new SortedDictionary<Location, int>(oldMap);
        newMap.Add(key, value);
        var newIncarnation = new TerrainTileByLocationMutMapIncarnation(newMap);
        rootIncarnation.incarnationsTerrainTileByLocationMutMap[mapId] =
            new VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      BroadcastTerrainTileByLocationMutMapEffect(mapId, effect);
    }
       
    public void EffectTerrainTileByLocationMutMapRemove(int mapId, Location key) {
      CheckHasTerrainTileByLocationMutMap(mapId);

      var effect = new TerrainTileByLocationMutMapRemoveEffect(mapId, key);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsTerrainTileByLocationMutMap[mapId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.map.Remove(key);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.map;
        var newMap = new SortedDictionary<Location, int>(oldMap);
        newMap.Remove(key);
        rootIncarnation.incarnationsTerrainTileByLocationMutMap[mapId] =
            new VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>(
                rootIncarnation.version,
                new TerrainTileByLocationMutMapIncarnation(newMap));
      }
      BroadcastTerrainTileByLocationMutMapEffect(mapId, effect);
    }
    public void AddTerrainTileByLocationMutMapObserver(int id, ITerrainTileByLocationMutMapEffectObserver observer) {
      List<ITerrainTileByLocationMutMapEffectObserver> obsies;
      if (!observersTerrainTileByLocationMutMap.TryGetValue(id, out obsies)) {
        obsies = new List<ITerrainTileByLocationMutMapEffectObserver>();
      }
      obsies.Add(observer);
      observersTerrainTileByLocationMutMap[id] = obsies;
    }

    public void RemoveTerrainTileByLocationMutMapObserver(int id, ITerrainTileByLocationMutMapEffectObserver observer) {
      if (observersTerrainTileByLocationMutMap.ContainsKey(id)) {
        var map = observersTerrainTileByLocationMutMap[id];
        map.Remove(observer);
        if (map.Count == 0) {
          observersTerrainTileByLocationMutMap.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void BroadcastTerrainTileByLocationMutMapEffect(int id, ITerrainTileByLocationMutMapEffect effect) {
      if (observersTerrainTileByLocationMutMap.ContainsKey(0)) {
        foreach (var observer in new List<ITerrainTileByLocationMutMapEffectObserver>(observersTerrainTileByLocationMutMap[0])) {
          observer.OnTerrainTileByLocationMutMapEffect(effect);
        }
      }
      if (observersTerrainTileByLocationMutMap.ContainsKey(id)) {
        foreach (var observer in new List<ITerrainTileByLocationMutMapEffectObserver>(observersTerrainTileByLocationMutMap[id])) {
          observer.OnTerrainTileByLocationMutMapEffect(effect);
        }
      }
    }
}

}
