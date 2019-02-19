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
  private static readonly int VERSION_HASH_MULTIPLIER = 179424673;
  private static readonly int NEXT_ID_HASH_MULTIPLIER = 373587883;

  private void CheckRootsEqual(Root a, Root b) {
    if (a != b) {
      throw new System.Exception("Given objects aren't from the same root!");
    }
  }

  // This *always* points to a live RootIncarnation. When we snapshot, we eagerly
  // make a new one of these.
  private RootIncarnation rootIncarnation;

  bool locked = true;

  // 0 means everything
  readonly SortedDictionary<int, List<IGameEffectObserver>> observersGame;
  readonly SortedDictionary<int, List<IRandEffectObserver>> observersRand;
  readonly SortedDictionary<int, List<ILevelEffectObserver>> observersLevel;
  readonly SortedDictionary<int, List<ITerrainEffectObserver>> observersTerrain;
  readonly SortedDictionary<int, List<ITerrainTileEffectObserver>> observersTerrainTile;
  readonly SortedDictionary<int, List<IDownStaircaseFeatureEffectObserver>> observersDownStaircaseFeature;
  readonly SortedDictionary<int, List<IUpStaircaseFeatureEffectObserver>> observersUpStaircaseFeature;
  readonly SortedDictionary<int, List<IDecorativeFeatureEffectObserver>> observersDecorativeFeature;
  readonly SortedDictionary<int, List<IUnitEffectObserver>> observersUnit;
  readonly SortedDictionary<int, List<IMoveDirectiveEffectObserver>> observersMoveDirective;
  readonly SortedDictionary<int, List<IAttackDirectiveEffectObserver>> observersAttackDirective;
  readonly SortedDictionary<int, List<IDefendingDetailEffectObserver>> observersDefendingDetail;
  readonly SortedDictionary<int, List<IArmorEffectObserver>> observersArmor;
  readonly SortedDictionary<int, List<IGlaiveEffectObserver>> observersGlaive;
  readonly SortedDictionary<int, List<IExecutionStateEffectObserver>> observersExecutionState;
  readonly SortedDictionary<int, List<ILocationMutListEffectObserver>> observersLocationMutList;
  readonly SortedDictionary<int, List<IIFeatureMutListEffectObserver>> observersIFeatureMutList;
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
    this.observersDownStaircaseFeature = new SortedDictionary<int, List<IDownStaircaseFeatureEffectObserver>>();
    this.observersUpStaircaseFeature = new SortedDictionary<int, List<IUpStaircaseFeatureEffectObserver>>();
    this.observersDecorativeFeature = new SortedDictionary<int, List<IDecorativeFeatureEffectObserver>>();
    this.observersUnit = new SortedDictionary<int, List<IUnitEffectObserver>>();
    this.observersMoveDirective = new SortedDictionary<int, List<IMoveDirectiveEffectObserver>>();
    this.observersAttackDirective = new SortedDictionary<int, List<IAttackDirectiveEffectObserver>>();
    this.observersDefendingDetail = new SortedDictionary<int, List<IDefendingDetailEffectObserver>>();
    this.observersArmor = new SortedDictionary<int, List<IArmorEffectObserver>>();
    this.observersGlaive = new SortedDictionary<int, List<IGlaiveEffectObserver>>();
    this.observersExecutionState = new SortedDictionary<int, List<IExecutionStateEffectObserver>>();
    this.observersLocationMutList = new SortedDictionary<int, List<ILocationMutListEffectObserver>>();
    this.observersIFeatureMutList = new SortedDictionary<int, List<IIFeatureMutListEffectObserver>>();
    this.observersIUnitEventMutList = new SortedDictionary<int, List<IIUnitEventMutListEffectObserver>>();
    this.observersIDetailMutList = new SortedDictionary<int, List<IIDetailMutListEffectObserver>>();
    this.observersLevelMutBunch = new SortedDictionary<int, List<ILevelMutBunchEffectObserver>>();
    this.observersUnitMutBunch = new SortedDictionary<int, List<IUnitMutBunchEffectObserver>>();
    this.observersIItemMutBunch = new SortedDictionary<int, List<IIItemMutBunchEffectObserver>>();
    this.observersTerrainTileByLocationMutMap = new SortedDictionary<int, List<ITerrainTileByLocationMutMapEffectObserver>>();

    int initialVersion = 1;
    int initialNextId = 1;
    int initialHash = VERSION_HASH_MULTIPLIER * initialVersion + NEXT_ID_HASH_MULTIPLIER * initialNextId;
    rootIncarnation = new RootIncarnation(initialVersion, initialNextId, initialHash);
  }

  public RootIncarnation Snapshot() {
    CheckUnlocked();
    RootIncarnation oldIncarnation = rootIncarnation;
    int newHash = oldIncarnation.hash;
    int newVersion = oldIncarnation.version + 1;
    newHash -= VERSION_HASH_MULTIPLIER * oldIncarnation.version + NEXT_ID_HASH_MULTIPLIER * oldIncarnation.nextId;
    newHash += VERSION_HASH_MULTIPLIER * newVersion + NEXT_ID_HASH_MULTIPLIER * oldIncarnation.nextId;
    rootIncarnation =
        new RootIncarnation(
            newVersion, oldIncarnation.nextId, newHash, oldIncarnation);
    return oldIncarnation;
  }

  public void Unlock() {
    if (!locked) {
      throw new Exception("Can't unlock, not locked!");
    }
    locked = false;
  }

  public void Lock() {
    if (locked) {
      throw new Exception("Can't lock, already locked!");
    }
    locked = true;
  }
  public void CheckUnlocked() {
    if (locked) {
      throw new Exception("Can't proceed, superstructure is locked!");
    }
  }

  private int NewId() {
    this.UpdateHashOnNextIdChange(rootIncarnation.nextId, rootIncarnation.nextId + 1);
    return rootIncarnation.nextId++;
  }

  private void UpdateHashOnNextIdChange(int oldNextId, int newNextId) {
    int oldIdAndVersionHashContribution =
        VERSION_HASH_MULTIPLIER * rootIncarnation.version +
        NEXT_ID_HASH_MULTIPLIER * oldNextId;
    int newIdAndVersionHashContribution =
        VERSION_HASH_MULTIPLIER * rootIncarnation.version +
        NEXT_ID_HASH_MULTIPLIER * newNextId;
    rootIncarnation.hash =
        rootIncarnation.hash -
        oldIdAndVersionHashContribution +
        newIdAndVersionHashContribution;
  }

  private int RecalculateEntireHash() {
    int result =
        VERSION_HASH_MULTIPLIER * rootIncarnation.version +
        NEXT_ID_HASH_MULTIPLIER * rootIncarnation.nextId;

    foreach (var entry in this.rootIncarnation.incarnationsGame) {
      result += GetGameHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsRand) {
      result += GetRandHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsLevel) {
      result += GetLevelHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsTerrain) {
      result += GetTerrainHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsTerrainTile) {
      result += GetTerrainTileHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsDownStaircaseFeature) {
      result += GetDownStaircaseFeatureHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsUpStaircaseFeature) {
      result += GetUpStaircaseFeatureHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsDecorativeFeature) {
      result += GetDecorativeFeatureHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsUnit) {
      result += GetUnitHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsMoveDirective) {
      result += GetMoveDirectiveHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsAttackDirective) {
      result += GetAttackDirectiveHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsDefendingDetail) {
      result += GetDefendingDetailHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsArmor) {
      result += GetArmorHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsGlaive) {
      result += GetGlaiveHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsExecutionState) {
      result += GetExecutionStateHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsLocationMutList) {
      result += GetLocationMutListHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsIFeatureMutList) {
      result += GetIFeatureMutListHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsIUnitEventMutList) {
      result += GetIUnitEventMutListHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsIDetailMutList) {
      result += GetIDetailMutListHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsLevelMutBunch) {
      result += GetLevelMutBunchHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsUnitMutBunch) {
      result += GetUnitMutBunchHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsIItemMutBunch) {
      result += GetIItemMutBunchHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsTerrainTileByLocationMutMap) {
      result += GetTerrainTileByLocationMutMapHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    return result;
  }

  public int GetDeterministicHashCode() {
    // int doubleCheckHash = RecalculateEntireHash();
    // Asserts.Assert(doubleCheckHash == this.rootIncarnation.hash);
    return this.rootIncarnation.hash;
  }

  public void Revert(RootIncarnation sourceIncarnation) {
    CheckUnlocked();
    // We do all the adds first so that we don't violate any strong borrows.
    // Then we do all the changes, because those might be flipping things to point
    // at things that were just made.
    // Then we do all the removes.

    //this.rootIncarnation.hash -= VERSION_HASH_MULTIPLIER * rootIncarnation.version + NEXT_ID_HASH_MULTIPLIER * rootIncarnation.nextId;


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
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDownStaircaseFeature) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsDownStaircaseFeature.ContainsKey(sourceObjId)) {
        EffectInternalCreateDownStaircaseFeature(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsUpStaircaseFeature) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsUpStaircaseFeature.ContainsKey(sourceObjId)) {
        EffectInternalCreateUpStaircaseFeature(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDecorativeFeature) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsDecorativeFeature.ContainsKey(sourceObjId)) {
        EffectInternalCreateDecorativeFeature(sourceObjId, sourceObjIncarnation);
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
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIFeatureMutList) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsIFeatureMutList.ContainsKey(sourceObjId)) {
        EffectInternalCreateIFeatureMutList(sourceObjId, sourceObjIncarnation);
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
            rootIncarnation.hash -=
                GetLocationMutListHash(
                    objId,
                    rootIncarnation.incarnationsLocationMutList[objId].version,
                    rootIncarnation.incarnationsLocationMutList[objId].incarnation);
            rootIncarnation.incarnationsLocationMutList[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetLocationMutListHash(
                    objId,
                    rootIncarnation.incarnationsLocationMutList[objId].version,
                    rootIncarnation.incarnationsLocationMutList[objId].incarnation);
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIFeatureMutList) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsIFeatureMutList.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsIFeatureMutList[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            for (int i = currentObjIncarnation.list.Count - 1; i >= 0; i--) {
              EffectIFeatureMutListRemoveAt(objId, i);
            }
            foreach (var objIdInSourceObjIncarnation in sourceObjIncarnation.list) {
              EffectIFeatureMutListAdd(objId, objIdInSourceObjIncarnation);
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.hash -=
                GetIFeatureMutListHash(
                    objId,
                    rootIncarnation.incarnationsIFeatureMutList[objId].version,
                    rootIncarnation.incarnationsIFeatureMutList[objId].incarnation);
            rootIncarnation.incarnationsIFeatureMutList[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetIFeatureMutListHash(
                    objId,
                    rootIncarnation.incarnationsIFeatureMutList[objId].version,
                    rootIncarnation.incarnationsIFeatureMutList[objId].incarnation);
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
            rootIncarnation.hash -=
                GetIUnitEventMutListHash(
                    objId,
                    rootIncarnation.incarnationsIUnitEventMutList[objId].version,
                    rootIncarnation.incarnationsIUnitEventMutList[objId].incarnation);
            rootIncarnation.incarnationsIUnitEventMutList[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetIUnitEventMutListHash(
                    objId,
                    rootIncarnation.incarnationsIUnitEventMutList[objId].version,
                    rootIncarnation.incarnationsIUnitEventMutList[objId].incarnation);
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
            rootIncarnation.hash -=
                GetIDetailMutListHash(
                    objId,
                    rootIncarnation.incarnationsIDetailMutList[objId].version,
                    rootIncarnation.incarnationsIDetailMutList[objId].incarnation);
            rootIncarnation.incarnationsIDetailMutList[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetIDetailMutListHash(
                    objId,
                    rootIncarnation.incarnationsIDetailMutList[objId].version,
                    rootIncarnation.incarnationsIDetailMutList[objId].incarnation);
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
            rootIncarnation.hash -=
                GetLevelMutBunchHash(
                    objId,
                    rootIncarnation.incarnationsLevelMutBunch[objId].version,
                    rootIncarnation.incarnationsLevelMutBunch[objId].incarnation);
            rootIncarnation.incarnationsLevelMutBunch[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetLevelMutBunchHash(
                    objId,
                    rootIncarnation.incarnationsLevelMutBunch[objId].version,
                    rootIncarnation.incarnationsLevelMutBunch[objId].incarnation);
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
            rootIncarnation.hash -=
                GetUnitMutBunchHash(
                    objId,
                    rootIncarnation.incarnationsUnitMutBunch[objId].version,
                    rootIncarnation.incarnationsUnitMutBunch[objId].incarnation);
            rootIncarnation.incarnationsUnitMutBunch[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetUnitMutBunchHash(
                    objId,
                    rootIncarnation.incarnationsUnitMutBunch[objId].version,
                    rootIncarnation.incarnationsUnitMutBunch[objId].incarnation);
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
            rootIncarnation.hash -=
                GetIItemMutBunchHash(
                    objId,
                    rootIncarnation.incarnationsIItemMutBunch[objId].version,
                    rootIncarnation.incarnationsIItemMutBunch[objId].incarnation);
            rootIncarnation.incarnationsIItemMutBunch[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetIItemMutBunchHash(
                    objId,
                    rootIncarnation.incarnationsIItemMutBunch[objId].version,
                    rootIncarnation.incarnationsIItemMutBunch[objId].incarnation);
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
            rootIncarnation.hash -=
                GetGameHash(
                    objId,
                    rootIncarnation.incarnationsGame[objId].version,
                    rootIncarnation.incarnationsGame[objId].incarnation);
          rootIncarnation.incarnationsGame[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetGameHash(
                    objId,
                    rootIncarnation.incarnationsGame[objId].version,
                    rootIncarnation.incarnationsGame[objId].incarnation);
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
            rootIncarnation.hash -=
                GetRandHash(
                    objId,
                    rootIncarnation.incarnationsRand[objId].version,
                    rootIncarnation.incarnationsRand[objId].incarnation);
          rootIncarnation.incarnationsRand[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetRandHash(
                    objId,
                    rootIncarnation.incarnationsRand[objId].version,
                    rootIncarnation.incarnationsRand[objId].incarnation);
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
            rootIncarnation.hash -=
                GetLevelHash(
                    objId,
                    rootIncarnation.incarnationsLevel[objId].version,
                    rootIncarnation.incarnationsLevel[objId].incarnation);
          rootIncarnation.incarnationsLevel[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetLevelHash(
                    objId,
                    rootIncarnation.incarnationsLevel[objId].version,
                    rootIncarnation.incarnationsLevel[objId].incarnation);
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
            rootIncarnation.hash -=
                GetTerrainHash(
                    objId,
                    rootIncarnation.incarnationsTerrain[objId].version,
                    rootIncarnation.incarnationsTerrain[objId].incarnation);
          rootIncarnation.incarnationsTerrain[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetTerrainHash(
                    objId,
                    rootIncarnation.incarnationsTerrain[objId].version,
                    rootIncarnation.incarnationsTerrain[objId].incarnation);
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
            rootIncarnation.hash -=
                GetTerrainTileHash(
                    objId,
                    rootIncarnation.incarnationsTerrainTile[objId].version,
                    rootIncarnation.incarnationsTerrainTile[objId].incarnation);
          rootIncarnation.incarnationsTerrainTile[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetTerrainTileHash(
                    objId,
                    rootIncarnation.incarnationsTerrainTile[objId].version,
                    rootIncarnation.incarnationsTerrainTile[objId].incarnation);
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDownStaircaseFeature) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsDownStaircaseFeature.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsDownStaircaseFeature[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetDownStaircaseFeatureHash(
                    objId,
                    rootIncarnation.incarnationsDownStaircaseFeature[objId].version,
                    rootIncarnation.incarnationsDownStaircaseFeature[objId].incarnation);
          rootIncarnation.incarnationsDownStaircaseFeature[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetDownStaircaseFeatureHash(
                    objId,
                    rootIncarnation.incarnationsDownStaircaseFeature[objId].version,
                    rootIncarnation.incarnationsDownStaircaseFeature[objId].incarnation);
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsUpStaircaseFeature) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsUpStaircaseFeature.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsUpStaircaseFeature[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetUpStaircaseFeatureHash(
                    objId,
                    rootIncarnation.incarnationsUpStaircaseFeature[objId].version,
                    rootIncarnation.incarnationsUpStaircaseFeature[objId].incarnation);
          rootIncarnation.incarnationsUpStaircaseFeature[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetUpStaircaseFeatureHash(
                    objId,
                    rootIncarnation.incarnationsUpStaircaseFeature[objId].version,
                    rootIncarnation.incarnationsUpStaircaseFeature[objId].incarnation);
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDecorativeFeature) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsDecorativeFeature.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsDecorativeFeature[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetDecorativeFeatureHash(
                    objId,
                    rootIncarnation.incarnationsDecorativeFeature[objId].version,
                    rootIncarnation.incarnationsDecorativeFeature[objId].incarnation);
          rootIncarnation.incarnationsDecorativeFeature[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetDecorativeFeatureHash(
                    objId,
                    rootIncarnation.incarnationsDecorativeFeature[objId].version,
                    rootIncarnation.incarnationsDecorativeFeature[objId].incarnation);
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
            rootIncarnation.hash -=
                GetUnitHash(
                    objId,
                    rootIncarnation.incarnationsUnit[objId].version,
                    rootIncarnation.incarnationsUnit[objId].incarnation);
          rootIncarnation.incarnationsUnit[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetUnitHash(
                    objId,
                    rootIncarnation.incarnationsUnit[objId].version,
                    rootIncarnation.incarnationsUnit[objId].incarnation);
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
            rootIncarnation.hash -=
                GetMoveDirectiveHash(
                    objId,
                    rootIncarnation.incarnationsMoveDirective[objId].version,
                    rootIncarnation.incarnationsMoveDirective[objId].incarnation);
          rootIncarnation.incarnationsMoveDirective[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetMoveDirectiveHash(
                    objId,
                    rootIncarnation.incarnationsMoveDirective[objId].version,
                    rootIncarnation.incarnationsMoveDirective[objId].incarnation);
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
            rootIncarnation.hash -=
                GetAttackDirectiveHash(
                    objId,
                    rootIncarnation.incarnationsAttackDirective[objId].version,
                    rootIncarnation.incarnationsAttackDirective[objId].incarnation);
          rootIncarnation.incarnationsAttackDirective[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetAttackDirectiveHash(
                    objId,
                    rootIncarnation.incarnationsAttackDirective[objId].version,
                    rootIncarnation.incarnationsAttackDirective[objId].incarnation);
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
            rootIncarnation.hash -=
                GetDefendingDetailHash(
                    objId,
                    rootIncarnation.incarnationsDefendingDetail[objId].version,
                    rootIncarnation.incarnationsDefendingDetail[objId].incarnation);
          rootIncarnation.incarnationsDefendingDetail[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetDefendingDetailHash(
                    objId,
                    rootIncarnation.incarnationsDefendingDetail[objId].version,
                    rootIncarnation.incarnationsDefendingDetail[objId].incarnation);
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
            rootIncarnation.hash -=
                GetArmorHash(
                    objId,
                    rootIncarnation.incarnationsArmor[objId].version,
                    rootIncarnation.incarnationsArmor[objId].incarnation);
          rootIncarnation.incarnationsArmor[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetArmorHash(
                    objId,
                    rootIncarnation.incarnationsArmor[objId].version,
                    rootIncarnation.incarnationsArmor[objId].incarnation);
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
            rootIncarnation.hash -=
                GetGlaiveHash(
                    objId,
                    rootIncarnation.incarnationsGlaive[objId].version,
                    rootIncarnation.incarnationsGlaive[objId].incarnation);
          rootIncarnation.incarnationsGlaive[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetGlaiveHash(
                    objId,
                    rootIncarnation.incarnationsGlaive[objId].version,
                    rootIncarnation.incarnationsGlaive[objId].incarnation);
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
            rootIncarnation.hash -=
                GetExecutionStateHash(
                    objId,
                    rootIncarnation.incarnationsExecutionState[objId].version,
                    rootIncarnation.incarnationsExecutionState[objId].incarnation);
          rootIncarnation.incarnationsExecutionState[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetExecutionStateHash(
                    objId,
                    rootIncarnation.incarnationsExecutionState[objId].version,
                    rootIncarnation.incarnationsExecutionState[objId].incarnation);
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

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<DownStaircaseFeatureIncarnation>>(rootIncarnation.incarnationsDownStaircaseFeature)) {
      if (!sourceIncarnation.incarnationsDownStaircaseFeature.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectDownStaircaseFeatureDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<UpStaircaseFeatureIncarnation>>(rootIncarnation.incarnationsUpStaircaseFeature)) {
      if (!sourceIncarnation.incarnationsUpStaircaseFeature.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectUpStaircaseFeatureDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<DecorativeFeatureIncarnation>>(rootIncarnation.incarnationsDecorativeFeature)) {
      if (!sourceIncarnation.incarnationsDecorativeFeature.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectDecorativeFeatureDelete(id);
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

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<IFeatureMutListIncarnation>>(rootIncarnation.incarnationsIFeatureMutList)) {
      if (!sourceIncarnation.incarnationsIFeatureMutList.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectIFeatureMutListDelete(id);
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

    //this.rootIncarnation.hash += VERSION_HASH_MULTIPLIER * rootIncarnation.version + NEXT_ID_HASH_MULTIPLIER * rootIncarnation.nextId;
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
    CheckUnlocked();    CheckHasRand(rand);
    CheckHasLevelMutBunch(levels);
    CheckHasUnit(player);
    CheckHasLevel(level);
    CheckHasExecutionState(executionState);

    var id = NewId();
    var incarnation =
        new GameIncarnation(
            rand.id,
            squareLevelsOnly,
            levels.id,
            player.id,
            level.id,
            time,
            executionState.id
            );
    EffectInternalCreateGame(id, incarnation);
    return new Game(this, id);
  }
  public void EffectInternalCreateGame(
      int id,
      GameIncarnation incarnation) {
    CheckUnlocked();
    var effect = new GameCreateEffect(id, incarnation);
    rootIncarnation.incarnationsGame.Add(
        id,
        new VersionAndIncarnation<GameIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetGameHash(id, rootIncarnation.version, incarnation);
    BroadcastGameEffect(id, effect);
  }

  public void EffectGameDelete(int id) {
    CheckUnlocked();
    var effect = new GameDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsGame[id];
    this.rootIncarnation.hash -=
        GetGameHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastGameEffect(id, effect);
    rootIncarnation.incarnationsGame.Remove(id);
  }

     
  public int GetGameHash(int id, int version, GameIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.rand.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.squareLevelsOnly.GetDeterministicHashCode();
    result += id * version * 3 * incarnation.levels.GetDeterministicHashCode();
    result += id * version * 4 * incarnation.player.GetDeterministicHashCode();
    result += id * version * 5 * incarnation.level.GetDeterministicHashCode();
    result += id * version * 6 * incarnation.time.GetDeterministicHashCode();
    result += id * version * 7 * incarnation.executionState.GetDeterministicHashCode();
    return result;
  }
     
  public void EffectGameSetPlayer(int id, Unit newValue) {
    CheckUnlocked();
    CheckHasGame(id);
    var effect = new GameSetPlayerEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsGame[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldId = oldIncarnationAndVersion.incarnation.player;
      oldIncarnationAndVersion.incarnation.player = newValue.id;
      this.rootIncarnation.hash -= id * rootIncarnation.version * 4 * oldId.GetDeterministicHashCode();
      this.rootIncarnation.hash += id * rootIncarnation.version * 4 * newValue.id.GetDeterministicHashCode();
           
    } else {
      var newIncarnation =
          new GameIncarnation(
              oldIncarnationAndVersion.incarnation.rand,
              oldIncarnationAndVersion.incarnation.squareLevelsOnly,
              oldIncarnationAndVersion.incarnation.levels,
              newValue.id,
              oldIncarnationAndVersion.incarnation.level,
              oldIncarnationAndVersion.incarnation.time,
              oldIncarnationAndVersion.incarnation.executionState);
      rootIncarnation.incarnationsGame[id] =
          new VersionAndIncarnation<GameIncarnation>(
              rootIncarnation.version,
              newIncarnation);
      this.rootIncarnation.hash -= GetGameHash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
      this.rootIncarnation.hash += GetGameHash(id, rootIncarnation.version, newIncarnation);
    }

    BroadcastGameEffect(id, effect);
  }

  public void EffectGameSetLevel(int id, Level newValue) {
    CheckUnlocked();
    CheckHasGame(id);
    var effect = new GameSetLevelEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsGame[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldId = oldIncarnationAndVersion.incarnation.level;
      oldIncarnationAndVersion.incarnation.level = newValue.id;
      this.rootIncarnation.hash -= id * rootIncarnation.version * 5 * oldId.GetDeterministicHashCode();
      this.rootIncarnation.hash += id * rootIncarnation.version * 5 * newValue.id.GetDeterministicHashCode();
           
    } else {
      var newIncarnation =
          new GameIncarnation(
              oldIncarnationAndVersion.incarnation.rand,
              oldIncarnationAndVersion.incarnation.squareLevelsOnly,
              oldIncarnationAndVersion.incarnation.levels,
              oldIncarnationAndVersion.incarnation.player,
              newValue.id,
              oldIncarnationAndVersion.incarnation.time,
              oldIncarnationAndVersion.incarnation.executionState);
      rootIncarnation.incarnationsGame[id] =
          new VersionAndIncarnation<GameIncarnation>(
              rootIncarnation.version,
              newIncarnation);
      this.rootIncarnation.hash -= GetGameHash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
      this.rootIncarnation.hash += GetGameHash(id, rootIncarnation.version, newIncarnation);
    }

    BroadcastGameEffect(id, effect);
  }

  public void EffectGameSetTime(int id, int newValue) {
    CheckUnlocked();
    CheckHasGame(id);
    var effect = new GameSetTimeEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsGame[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.time;
      oldIncarnationAndVersion.incarnation.time = newValue;
      this.rootIncarnation.hash -= id * rootIncarnation.version * 6 * oldValue.GetDeterministicHashCode();
      this.rootIncarnation.hash += id * rootIncarnation.version * 6 * newValue.GetDeterministicHashCode();
           
    } else {
      var newIncarnation =
          new GameIncarnation(
              oldIncarnationAndVersion.incarnation.rand,
              oldIncarnationAndVersion.incarnation.squareLevelsOnly,
              oldIncarnationAndVersion.incarnation.levels,
              oldIncarnationAndVersion.incarnation.player,
              oldIncarnationAndVersion.incarnation.level,
              newValue,
              oldIncarnationAndVersion.incarnation.executionState);
      rootIncarnation.incarnationsGame[id] =
          new VersionAndIncarnation<GameIncarnation>(
              rootIncarnation.version,
              newIncarnation);
      this.rootIncarnation.hash -= GetGameHash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
      this.rootIncarnation.hash += GetGameHash(id, rootIncarnation.version, newIncarnation);
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
    CheckUnlocked();
    var id = NewId();
    var incarnation =
        new RandIncarnation(
            rand
            );
    EffectInternalCreateRand(id, incarnation);
    return new Rand(this, id);
  }
  public void EffectInternalCreateRand(
      int id,
      RandIncarnation incarnation) {
    CheckUnlocked();
    var effect = new RandCreateEffect(id, incarnation);
    rootIncarnation.incarnationsRand.Add(
        id,
        new VersionAndIncarnation<RandIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetRandHash(id, rootIncarnation.version, incarnation);
    BroadcastRandEffect(id, effect);
  }

  public void EffectRandDelete(int id) {
    CheckUnlocked();
    var effect = new RandDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsRand[id];
    this.rootIncarnation.hash -=
        GetRandHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastRandEffect(id, effect);
    rootIncarnation.incarnationsRand.Remove(id);
  }

     
  public int GetRandHash(int id, int version, RandIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.rand.GetDeterministicHashCode();
    return result;
  }
     
  public void EffectRandSetRand(int id, int newValue) {
    CheckUnlocked();
    CheckHasRand(id);
    var effect = new RandSetRandEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsRand[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.rand;
      oldIncarnationAndVersion.incarnation.rand = newValue;
      this.rootIncarnation.hash -= id * rootIncarnation.version * 1 * oldValue.GetDeterministicHashCode();
      this.rootIncarnation.hash += id * rootIncarnation.version * 1 * newValue.GetDeterministicHashCode();
           
    } else {
      var newIncarnation =
          new RandIncarnation(
              newValue);
      rootIncarnation.incarnationsRand[id] =
          new VersionAndIncarnation<RandIncarnation>(
              rootIncarnation.version,
              newIncarnation);
      this.rootIncarnation.hash -= GetRandHash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
      this.rootIncarnation.hash += GetRandHash(id, rootIncarnation.version, newIncarnation);
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
    CheckUnlocked();    CheckHasTerrain(terrain);
    CheckHasUnitMutBunch(units);

    var id = NewId();
    var incarnation =
        new LevelIncarnation(
            name,
            considerCornersAdjacent,
            terrain.id,
            units.id
            );
    EffectInternalCreateLevel(id, incarnation);
    return new Level(this, id);
  }
  public void EffectInternalCreateLevel(
      int id,
      LevelIncarnation incarnation) {
    CheckUnlocked();
    var effect = new LevelCreateEffect(id, incarnation);
    rootIncarnation.incarnationsLevel.Add(
        id,
        new VersionAndIncarnation<LevelIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetLevelHash(id, rootIncarnation.version, incarnation);
    BroadcastLevelEffect(id, effect);
  }

  public void EffectLevelDelete(int id) {
    CheckUnlocked();
    var effect = new LevelDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsLevel[id];
    this.rootIncarnation.hash -=
        GetLevelHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastLevelEffect(id, effect);
    rootIncarnation.incarnationsLevel.Remove(id);
  }

     
  public int GetLevelHash(int id, int version, LevelIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.name.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.considerCornersAdjacent.GetDeterministicHashCode();
    result += id * version * 3 * incarnation.terrain.GetDeterministicHashCode();
    result += id * version * 4 * incarnation.units.GetDeterministicHashCode();
    return result;
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
    CheckUnlocked();    CheckHasTerrainTileByLocationMutMap(tiles);

    var id = NewId();
    var incarnation =
        new TerrainIncarnation(
            pattern,
            elevationStepHeight,
            tiles.id
            );
    EffectInternalCreateTerrain(id, incarnation);
    return new Terrain(this, id);
  }
  public void EffectInternalCreateTerrain(
      int id,
      TerrainIncarnation incarnation) {
    CheckUnlocked();
    var effect = new TerrainCreateEffect(id, incarnation);
    rootIncarnation.incarnationsTerrain.Add(
        id,
        new VersionAndIncarnation<TerrainIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetTerrainHash(id, rootIncarnation.version, incarnation);
    BroadcastTerrainEffect(id, effect);
  }

  public void EffectTerrainDelete(int id) {
    CheckUnlocked();
    var effect = new TerrainDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsTerrain[id];
    this.rootIncarnation.hash -=
        GetTerrainHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastTerrainEffect(id, effect);
    rootIncarnation.incarnationsTerrain.Remove(id);
  }

     
  public int GetTerrainHash(int id, int version, TerrainIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.pattern.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.elevationStepHeight.GetDeterministicHashCode();
    result += id * version * 3 * incarnation.tiles.GetDeterministicHashCode();
    return result;
  }
     
  public void EffectTerrainSetPattern(int id, Pattern newValue) {
    CheckUnlocked();
    CheckHasTerrain(id);
    var effect = new TerrainSetPatternEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsTerrain[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.pattern;
      oldIncarnationAndVersion.incarnation.pattern = newValue;
      this.rootIncarnation.hash -= id * rootIncarnation.version * 1 * oldValue.GetDeterministicHashCode();
      this.rootIncarnation.hash += id * rootIncarnation.version * 1 * newValue.GetDeterministicHashCode();
           
    } else {
      var newIncarnation =
          new TerrainIncarnation(
              newValue,
              oldIncarnationAndVersion.incarnation.elevationStepHeight,
              oldIncarnationAndVersion.incarnation.tiles);
      rootIncarnation.incarnationsTerrain[id] =
          new VersionAndIncarnation<TerrainIncarnation>(
              rootIncarnation.version,
              newIncarnation);
      this.rootIncarnation.hash -= GetTerrainHash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
      this.rootIncarnation.hash += GetTerrainHash(id, rootIncarnation.version, newIncarnation);
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
      string classId,
      IFeatureMutList features) {
    CheckUnlocked();    CheckHasIFeatureMutList(features);

    var id = NewId();
    var incarnation =
        new TerrainTileIncarnation(
            elevation,
            walkable,
            classId,
            features.id
            );
    EffectInternalCreateTerrainTile(id, incarnation);
    return new TerrainTile(this, id);
  }
  public void EffectInternalCreateTerrainTile(
      int id,
      TerrainTileIncarnation incarnation) {
    CheckUnlocked();
    var effect = new TerrainTileCreateEffect(id, incarnation);
    rootIncarnation.incarnationsTerrainTile.Add(
        id,
        new VersionAndIncarnation<TerrainTileIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetTerrainTileHash(id, rootIncarnation.version, incarnation);
    BroadcastTerrainTileEffect(id, effect);
  }

  public void EffectTerrainTileDelete(int id) {
    CheckUnlocked();
    var effect = new TerrainTileDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsTerrainTile[id];
    this.rootIncarnation.hash -=
        GetTerrainTileHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastTerrainTileEffect(id, effect);
    rootIncarnation.incarnationsTerrainTile.Remove(id);
  }

     
  public int GetTerrainTileHash(int id, int version, TerrainTileIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.elevation.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.walkable.GetDeterministicHashCode();
    result += id * version * 3 * incarnation.classId.GetDeterministicHashCode();
    result += id * version * 4 * incarnation.features.GetDeterministicHashCode();
    return result;
  }
     
  public void EffectTerrainTileSetElevation(int id, int newValue) {
    CheckUnlocked();
    CheckHasTerrainTile(id);
    var effect = new TerrainTileSetElevationEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsTerrainTile[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.elevation;
      oldIncarnationAndVersion.incarnation.elevation = newValue;
      this.rootIncarnation.hash -= id * rootIncarnation.version * 1 * oldValue.GetDeterministicHashCode();
      this.rootIncarnation.hash += id * rootIncarnation.version * 1 * newValue.GetDeterministicHashCode();
           
    } else {
      var newIncarnation =
          new TerrainTileIncarnation(
              newValue,
              oldIncarnationAndVersion.incarnation.walkable,
              oldIncarnationAndVersion.incarnation.classId,
              oldIncarnationAndVersion.incarnation.features);
      rootIncarnation.incarnationsTerrainTile[id] =
          new VersionAndIncarnation<TerrainTileIncarnation>(
              rootIncarnation.version,
              newIncarnation);
      this.rootIncarnation.hash -= GetTerrainTileHash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
      this.rootIncarnation.hash += GetTerrainTileHash(id, rootIncarnation.version, newIncarnation);
    }

    BroadcastTerrainTileEffect(id, effect);
  }
  public DownStaircaseFeatureIncarnation GetDownStaircaseFeatureIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsDownStaircaseFeature[id].incarnation;
  }
  public bool DownStaircaseFeatureExists(int id) {
    return rootIncarnation.incarnationsDownStaircaseFeature.ContainsKey(id);
  }
  public DownStaircaseFeature GetDownStaircaseFeature(int id) {
    return new DownStaircaseFeature(this, id);
  }
  public List<DownStaircaseFeature> AllDownStaircaseFeature() {
    List<DownStaircaseFeature> result = new List<DownStaircaseFeature>(rootIncarnation.incarnationsDownStaircaseFeature.Count);
    foreach (var gameId in rootIncarnation.incarnationsDownStaircaseFeature.Keys) {
      result.Add(new DownStaircaseFeature(this, gameId));
    }
    return result;
  }
  public IEnumerator<DownStaircaseFeature> EnumAllDownStaircaseFeature() {
    foreach (var gameId in rootIncarnation.incarnationsDownStaircaseFeature.Keys) {
      yield return GetDownStaircaseFeature(gameId);
    }
  }
  public void CheckHasDownStaircaseFeature(DownStaircaseFeature thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasDownStaircaseFeature(thing.id);
  }
  public void CheckHasDownStaircaseFeature(int id) {
    if (!rootIncarnation.incarnationsDownStaircaseFeature.ContainsKey(id)) {
      throw new System.Exception("Invalid DownStaircaseFeature!");
    }
  }
  public void AddDownStaircaseFeatureObserver(int id, IDownStaircaseFeatureEffectObserver observer) {
    List<IDownStaircaseFeatureEffectObserver> obsies;
    if (!observersDownStaircaseFeature.TryGetValue(id, out obsies)) {
      obsies = new List<IDownStaircaseFeatureEffectObserver>();
    }
    obsies.Add(observer);
    observersDownStaircaseFeature[id] = obsies;
  }

  public void RemoveDownStaircaseFeatureObserver(int id, IDownStaircaseFeatureEffectObserver observer) {
    if (observersDownStaircaseFeature.ContainsKey(id)) {
      var list = observersDownStaircaseFeature[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersDownStaircaseFeature.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastDownStaircaseFeatureEffect(int id, IDownStaircaseFeatureEffect effect) {
    if (observersDownStaircaseFeature.ContainsKey(0)) {
      foreach (var observer in new List<IDownStaircaseFeatureEffectObserver>(observersDownStaircaseFeature[0])) {
        observer.OnDownStaircaseFeatureEffect(effect);
      }
    }
    if (observersDownStaircaseFeature.ContainsKey(id)) {
      foreach (var observer in new List<IDownStaircaseFeatureEffectObserver>(observersDownStaircaseFeature[id])) {
        observer.OnDownStaircaseFeatureEffect(effect);
      }
    }
  }

  public DownStaircaseFeature EffectDownStaircaseFeatureCreate(
) {
    CheckUnlocked();
    var id = NewId();
    var incarnation =
        new DownStaircaseFeatureIncarnation(

            );
    EffectInternalCreateDownStaircaseFeature(id, incarnation);
    return new DownStaircaseFeature(this, id);
  }
  public void EffectInternalCreateDownStaircaseFeature(
      int id,
      DownStaircaseFeatureIncarnation incarnation) {
    CheckUnlocked();
    var effect = new DownStaircaseFeatureCreateEffect(id, incarnation);
    rootIncarnation.incarnationsDownStaircaseFeature.Add(
        id,
        new VersionAndIncarnation<DownStaircaseFeatureIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetDownStaircaseFeatureHash(id, rootIncarnation.version, incarnation);
    BroadcastDownStaircaseFeatureEffect(id, effect);
  }

  public void EffectDownStaircaseFeatureDelete(int id) {
    CheckUnlocked();
    var effect = new DownStaircaseFeatureDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsDownStaircaseFeature[id];
    this.rootIncarnation.hash -=
        GetDownStaircaseFeatureHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastDownStaircaseFeatureEffect(id, effect);
    rootIncarnation.incarnationsDownStaircaseFeature.Remove(id);
  }

     
  public int GetDownStaircaseFeatureHash(int id, int version, DownStaircaseFeatureIncarnation incarnation) {
    int result = id * version;
    return result;
  }
       public UpStaircaseFeatureIncarnation GetUpStaircaseFeatureIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsUpStaircaseFeature[id].incarnation;
  }
  public bool UpStaircaseFeatureExists(int id) {
    return rootIncarnation.incarnationsUpStaircaseFeature.ContainsKey(id);
  }
  public UpStaircaseFeature GetUpStaircaseFeature(int id) {
    return new UpStaircaseFeature(this, id);
  }
  public List<UpStaircaseFeature> AllUpStaircaseFeature() {
    List<UpStaircaseFeature> result = new List<UpStaircaseFeature>(rootIncarnation.incarnationsUpStaircaseFeature.Count);
    foreach (var gameId in rootIncarnation.incarnationsUpStaircaseFeature.Keys) {
      result.Add(new UpStaircaseFeature(this, gameId));
    }
    return result;
  }
  public IEnumerator<UpStaircaseFeature> EnumAllUpStaircaseFeature() {
    foreach (var gameId in rootIncarnation.incarnationsUpStaircaseFeature.Keys) {
      yield return GetUpStaircaseFeature(gameId);
    }
  }
  public void CheckHasUpStaircaseFeature(UpStaircaseFeature thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasUpStaircaseFeature(thing.id);
  }
  public void CheckHasUpStaircaseFeature(int id) {
    if (!rootIncarnation.incarnationsUpStaircaseFeature.ContainsKey(id)) {
      throw new System.Exception("Invalid UpStaircaseFeature!");
    }
  }
  public void AddUpStaircaseFeatureObserver(int id, IUpStaircaseFeatureEffectObserver observer) {
    List<IUpStaircaseFeatureEffectObserver> obsies;
    if (!observersUpStaircaseFeature.TryGetValue(id, out obsies)) {
      obsies = new List<IUpStaircaseFeatureEffectObserver>();
    }
    obsies.Add(observer);
    observersUpStaircaseFeature[id] = obsies;
  }

  public void RemoveUpStaircaseFeatureObserver(int id, IUpStaircaseFeatureEffectObserver observer) {
    if (observersUpStaircaseFeature.ContainsKey(id)) {
      var list = observersUpStaircaseFeature[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersUpStaircaseFeature.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastUpStaircaseFeatureEffect(int id, IUpStaircaseFeatureEffect effect) {
    if (observersUpStaircaseFeature.ContainsKey(0)) {
      foreach (var observer in new List<IUpStaircaseFeatureEffectObserver>(observersUpStaircaseFeature[0])) {
        observer.OnUpStaircaseFeatureEffect(effect);
      }
    }
    if (observersUpStaircaseFeature.ContainsKey(id)) {
      foreach (var observer in new List<IUpStaircaseFeatureEffectObserver>(observersUpStaircaseFeature[id])) {
        observer.OnUpStaircaseFeatureEffect(effect);
      }
    }
  }

  public UpStaircaseFeature EffectUpStaircaseFeatureCreate(
) {
    CheckUnlocked();
    var id = NewId();
    var incarnation =
        new UpStaircaseFeatureIncarnation(

            );
    EffectInternalCreateUpStaircaseFeature(id, incarnation);
    return new UpStaircaseFeature(this, id);
  }
  public void EffectInternalCreateUpStaircaseFeature(
      int id,
      UpStaircaseFeatureIncarnation incarnation) {
    CheckUnlocked();
    var effect = new UpStaircaseFeatureCreateEffect(id, incarnation);
    rootIncarnation.incarnationsUpStaircaseFeature.Add(
        id,
        new VersionAndIncarnation<UpStaircaseFeatureIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetUpStaircaseFeatureHash(id, rootIncarnation.version, incarnation);
    BroadcastUpStaircaseFeatureEffect(id, effect);
  }

  public void EffectUpStaircaseFeatureDelete(int id) {
    CheckUnlocked();
    var effect = new UpStaircaseFeatureDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsUpStaircaseFeature[id];
    this.rootIncarnation.hash -=
        GetUpStaircaseFeatureHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastUpStaircaseFeatureEffect(id, effect);
    rootIncarnation.incarnationsUpStaircaseFeature.Remove(id);
  }

     
  public int GetUpStaircaseFeatureHash(int id, int version, UpStaircaseFeatureIncarnation incarnation) {
    int result = id * version;
    return result;
  }
       public DecorativeFeatureIncarnation GetDecorativeFeatureIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsDecorativeFeature[id].incarnation;
  }
  public bool DecorativeFeatureExists(int id) {
    return rootIncarnation.incarnationsDecorativeFeature.ContainsKey(id);
  }
  public DecorativeFeature GetDecorativeFeature(int id) {
    return new DecorativeFeature(this, id);
  }
  public List<DecorativeFeature> AllDecorativeFeature() {
    List<DecorativeFeature> result = new List<DecorativeFeature>(rootIncarnation.incarnationsDecorativeFeature.Count);
    foreach (var gameId in rootIncarnation.incarnationsDecorativeFeature.Keys) {
      result.Add(new DecorativeFeature(this, gameId));
    }
    return result;
  }
  public IEnumerator<DecorativeFeature> EnumAllDecorativeFeature() {
    foreach (var gameId in rootIncarnation.incarnationsDecorativeFeature.Keys) {
      yield return GetDecorativeFeature(gameId);
    }
  }
  public void CheckHasDecorativeFeature(DecorativeFeature thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasDecorativeFeature(thing.id);
  }
  public void CheckHasDecorativeFeature(int id) {
    if (!rootIncarnation.incarnationsDecorativeFeature.ContainsKey(id)) {
      throw new System.Exception("Invalid DecorativeFeature!");
    }
  }
  public void AddDecorativeFeatureObserver(int id, IDecorativeFeatureEffectObserver observer) {
    List<IDecorativeFeatureEffectObserver> obsies;
    if (!observersDecorativeFeature.TryGetValue(id, out obsies)) {
      obsies = new List<IDecorativeFeatureEffectObserver>();
    }
    obsies.Add(observer);
    observersDecorativeFeature[id] = obsies;
  }

  public void RemoveDecorativeFeatureObserver(int id, IDecorativeFeatureEffectObserver observer) {
    if (observersDecorativeFeature.ContainsKey(id)) {
      var list = observersDecorativeFeature[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersDecorativeFeature.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastDecorativeFeatureEffect(int id, IDecorativeFeatureEffect effect) {
    if (observersDecorativeFeature.ContainsKey(0)) {
      foreach (var observer in new List<IDecorativeFeatureEffectObserver>(observersDecorativeFeature[0])) {
        observer.OnDecorativeFeatureEffect(effect);
      }
    }
    if (observersDecorativeFeature.ContainsKey(id)) {
      foreach (var observer in new List<IDecorativeFeatureEffectObserver>(observersDecorativeFeature[id])) {
        observer.OnDecorativeFeatureEffect(effect);
      }
    }
  }

  public DecorativeFeature EffectDecorativeFeatureCreate(
      string symbolId) {
    CheckUnlocked();
    var id = NewId();
    var incarnation =
        new DecorativeFeatureIncarnation(
            symbolId
            );
    EffectInternalCreateDecorativeFeature(id, incarnation);
    return new DecorativeFeature(this, id);
  }
  public void EffectInternalCreateDecorativeFeature(
      int id,
      DecorativeFeatureIncarnation incarnation) {
    CheckUnlocked();
    var effect = new DecorativeFeatureCreateEffect(id, incarnation);
    rootIncarnation.incarnationsDecorativeFeature.Add(
        id,
        new VersionAndIncarnation<DecorativeFeatureIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetDecorativeFeatureHash(id, rootIncarnation.version, incarnation);
    BroadcastDecorativeFeatureEffect(id, effect);
  }

  public void EffectDecorativeFeatureDelete(int id) {
    CheckUnlocked();
    var effect = new DecorativeFeatureDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsDecorativeFeature[id];
    this.rootIncarnation.hash -=
        GetDecorativeFeatureHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastDecorativeFeatureEffect(id, effect);
    rootIncarnation.incarnationsDecorativeFeature.Remove(id);
  }

     
  public int GetDecorativeFeatureHash(int id, int version, DecorativeFeatureIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.symbolId.GetDeterministicHashCode();
    return result;
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
    CheckUnlocked();    CheckHasIUnitEventMutList(events);
    CheckHasIDetailMutList(details);
    CheckHasIItemMutBunch(items);

    var id = NewId();
    var incarnation =
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
            items.id
            );
    EffectInternalCreateUnit(id, incarnation);
    return new Unit(this, id);
  }
  public void EffectInternalCreateUnit(
      int id,
      UnitIncarnation incarnation) {
    CheckUnlocked();
    var effect = new UnitCreateEffect(id, incarnation);
    rootIncarnation.incarnationsUnit.Add(
        id,
        new VersionAndIncarnation<UnitIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetUnitHash(id, rootIncarnation.version, incarnation);
    BroadcastUnitEffect(id, effect);
  }

  public void EffectUnitDelete(int id) {
    CheckUnlocked();
    var effect = new UnitDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsUnit[id];
    this.rootIncarnation.hash -=
        GetUnitHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastUnitEffect(id, effect);
    rootIncarnation.incarnationsUnit.Remove(id);
  }

     
  public int GetUnitHash(int id, int version, UnitIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.events.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.alive.GetDeterministicHashCode();
    result += id * version * 3 * incarnation.lifeEndTime.GetDeterministicHashCode();
    result += id * version * 4 * incarnation.location.GetDeterministicHashCode();
    result += id * version * 5 * incarnation.classId.GetDeterministicHashCode();
    result += id * version * 6 * incarnation.hp.GetDeterministicHashCode();
    result += id * version * 7 * incarnation.maxHp.GetDeterministicHashCode();
    result += id * version * 8 * incarnation.mp.GetDeterministicHashCode();
    result += id * version * 9 * incarnation.maxMp.GetDeterministicHashCode();
    result += id * version * 10 * incarnation.inertia.GetDeterministicHashCode();
    result += id * version * 11 * incarnation.nextActionTime.GetDeterministicHashCode();
    result += id * version * 12 * incarnation.directive.GetDeterministicHashCode();
    result += id * version * 13 * incarnation.details.GetDeterministicHashCode();
    result += id * version * 14 * incarnation.items.GetDeterministicHashCode();
    return result;
  }
     
  public void EffectUnitSetAlive(int id, bool newValue) {
    CheckUnlocked();
    CheckHasUnit(id);
    var effect = new UnitSetAliveEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsUnit[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.alive;
      oldIncarnationAndVersion.incarnation.alive = newValue;
      this.rootIncarnation.hash -= id * rootIncarnation.version * 2 * oldValue.GetDeterministicHashCode();
      this.rootIncarnation.hash += id * rootIncarnation.version * 2 * newValue.GetDeterministicHashCode();
           
    } else {
      var newIncarnation =
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
              oldIncarnationAndVersion.incarnation.items);
      rootIncarnation.incarnationsUnit[id] =
          new VersionAndIncarnation<UnitIncarnation>(
              rootIncarnation.version,
              newIncarnation);
      this.rootIncarnation.hash -= GetUnitHash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
      this.rootIncarnation.hash += GetUnitHash(id, rootIncarnation.version, newIncarnation);
    }

    BroadcastUnitEffect(id, effect);
  }

  public void EffectUnitSetLifeEndTime(int id, int newValue) {
    CheckUnlocked();
    CheckHasUnit(id);
    var effect = new UnitSetLifeEndTimeEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsUnit[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.lifeEndTime;
      oldIncarnationAndVersion.incarnation.lifeEndTime = newValue;
      this.rootIncarnation.hash -= id * rootIncarnation.version * 3 * oldValue.GetDeterministicHashCode();
      this.rootIncarnation.hash += id * rootIncarnation.version * 3 * newValue.GetDeterministicHashCode();
           
    } else {
      var newIncarnation =
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
              oldIncarnationAndVersion.incarnation.items);
      rootIncarnation.incarnationsUnit[id] =
          new VersionAndIncarnation<UnitIncarnation>(
              rootIncarnation.version,
              newIncarnation);
      this.rootIncarnation.hash -= GetUnitHash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
      this.rootIncarnation.hash += GetUnitHash(id, rootIncarnation.version, newIncarnation);
    }

    BroadcastUnitEffect(id, effect);
  }

  public void EffectUnitSetLocation(int id, Location newValue) {
    CheckUnlocked();
    CheckHasUnit(id);
    var effect = new UnitSetLocationEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsUnit[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.location;
      oldIncarnationAndVersion.incarnation.location = newValue;
      this.rootIncarnation.hash -= id * rootIncarnation.version * 4 * oldValue.GetDeterministicHashCode();
      this.rootIncarnation.hash += id * rootIncarnation.version * 4 * newValue.GetDeterministicHashCode();
           
    } else {
      var newIncarnation =
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
              oldIncarnationAndVersion.incarnation.items);
      rootIncarnation.incarnationsUnit[id] =
          new VersionAndIncarnation<UnitIncarnation>(
              rootIncarnation.version,
              newIncarnation);
      this.rootIncarnation.hash -= GetUnitHash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
      this.rootIncarnation.hash += GetUnitHash(id, rootIncarnation.version, newIncarnation);
    }

    BroadcastUnitEffect(id, effect);
  }

  public void EffectUnitSetHp(int id, int newValue) {
    CheckUnlocked();
    CheckHasUnit(id);
    var effect = new UnitSetHpEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsUnit[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.hp;
      oldIncarnationAndVersion.incarnation.hp = newValue;
      this.rootIncarnation.hash -= id * rootIncarnation.version * 6 * oldValue.GetDeterministicHashCode();
      this.rootIncarnation.hash += id * rootIncarnation.version * 6 * newValue.GetDeterministicHashCode();
           
    } else {
      var newIncarnation =
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
              oldIncarnationAndVersion.incarnation.items);
      rootIncarnation.incarnationsUnit[id] =
          new VersionAndIncarnation<UnitIncarnation>(
              rootIncarnation.version,
              newIncarnation);
      this.rootIncarnation.hash -= GetUnitHash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
      this.rootIncarnation.hash += GetUnitHash(id, rootIncarnation.version, newIncarnation);
    }

    BroadcastUnitEffect(id, effect);
  }

  public void EffectUnitSetMp(int id, int newValue) {
    CheckUnlocked();
    CheckHasUnit(id);
    var effect = new UnitSetMpEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsUnit[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.mp;
      oldIncarnationAndVersion.incarnation.mp = newValue;
      this.rootIncarnation.hash -= id * rootIncarnation.version * 8 * oldValue.GetDeterministicHashCode();
      this.rootIncarnation.hash += id * rootIncarnation.version * 8 * newValue.GetDeterministicHashCode();
           
    } else {
      var newIncarnation =
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
              oldIncarnationAndVersion.incarnation.items);
      rootIncarnation.incarnationsUnit[id] =
          new VersionAndIncarnation<UnitIncarnation>(
              rootIncarnation.version,
              newIncarnation);
      this.rootIncarnation.hash -= GetUnitHash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
      this.rootIncarnation.hash += GetUnitHash(id, rootIncarnation.version, newIncarnation);
    }

    BroadcastUnitEffect(id, effect);
  }

  public void EffectUnitSetNextActionTime(int id, int newValue) {
    CheckUnlocked();
    CheckHasUnit(id);
    var effect = new UnitSetNextActionTimeEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsUnit[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.nextActionTime;
      oldIncarnationAndVersion.incarnation.nextActionTime = newValue;
      this.rootIncarnation.hash -= id * rootIncarnation.version * 11 * oldValue.GetDeterministicHashCode();
      this.rootIncarnation.hash += id * rootIncarnation.version * 11 * newValue.GetDeterministicHashCode();
           
    } else {
      var newIncarnation =
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
              oldIncarnationAndVersion.incarnation.items);
      rootIncarnation.incarnationsUnit[id] =
          new VersionAndIncarnation<UnitIncarnation>(
              rootIncarnation.version,
              newIncarnation);
      this.rootIncarnation.hash -= GetUnitHash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
      this.rootIncarnation.hash += GetUnitHash(id, rootIncarnation.version, newIncarnation);
    }

    BroadcastUnitEffect(id, effect);
  }

  public void EffectUnitSetDirective(int id, IDirective newValue) {
    CheckUnlocked();
    CheckHasUnit(id);
    var effect = new UnitSetDirectiveEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsUnit[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldId = oldIncarnationAndVersion.incarnation.directive;
      oldIncarnationAndVersion.incarnation.directive = newValue.id;
      this.rootIncarnation.hash -= id * rootIncarnation.version * 12 * oldId.GetDeterministicHashCode();
      this.rootIncarnation.hash += id * rootIncarnation.version * 12 * newValue.id.GetDeterministicHashCode();
           
    } else {
      var newIncarnation =
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
              oldIncarnationAndVersion.incarnation.items);
      rootIncarnation.incarnationsUnit[id] =
          new VersionAndIncarnation<UnitIncarnation>(
              rootIncarnation.version,
              newIncarnation);
      this.rootIncarnation.hash -= GetUnitHash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
      this.rootIncarnation.hash += GetUnitHash(id, rootIncarnation.version, newIncarnation);
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
    CheckUnlocked();    CheckHasLocationMutList(path);

    var id = NewId();
    var incarnation =
        new MoveDirectiveIncarnation(
            path.id
            );
    EffectInternalCreateMoveDirective(id, incarnation);
    return new MoveDirective(this, id);
  }
  public void EffectInternalCreateMoveDirective(
      int id,
      MoveDirectiveIncarnation incarnation) {
    CheckUnlocked();
    var effect = new MoveDirectiveCreateEffect(id, incarnation);
    rootIncarnation.incarnationsMoveDirective.Add(
        id,
        new VersionAndIncarnation<MoveDirectiveIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetMoveDirectiveHash(id, rootIncarnation.version, incarnation);
    BroadcastMoveDirectiveEffect(id, effect);
  }

  public void EffectMoveDirectiveDelete(int id) {
    CheckUnlocked();
    var effect = new MoveDirectiveDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsMoveDirective[id];
    this.rootIncarnation.hash -=
        GetMoveDirectiveHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastMoveDirectiveEffect(id, effect);
    rootIncarnation.incarnationsMoveDirective.Remove(id);
  }

     
  public int GetMoveDirectiveHash(int id, int version, MoveDirectiveIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.path.GetDeterministicHashCode();
    return result;
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
    CheckUnlocked();    CheckHasUnit(targetUnit);
    CheckHasLocationMutList(pathToLastSeenLocation);

    var id = NewId();
    var incarnation =
        new AttackDirectiveIncarnation(
            targetUnit.id,
            pathToLastSeenLocation.id
            );
    EffectInternalCreateAttackDirective(id, incarnation);
    return new AttackDirective(this, id);
  }
  public void EffectInternalCreateAttackDirective(
      int id,
      AttackDirectiveIncarnation incarnation) {
    CheckUnlocked();
    var effect = new AttackDirectiveCreateEffect(id, incarnation);
    rootIncarnation.incarnationsAttackDirective.Add(
        id,
        new VersionAndIncarnation<AttackDirectiveIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetAttackDirectiveHash(id, rootIncarnation.version, incarnation);
    BroadcastAttackDirectiveEffect(id, effect);
  }

  public void EffectAttackDirectiveDelete(int id) {
    CheckUnlocked();
    var effect = new AttackDirectiveDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsAttackDirective[id];
    this.rootIncarnation.hash -=
        GetAttackDirectiveHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastAttackDirectiveEffect(id, effect);
    rootIncarnation.incarnationsAttackDirective.Remove(id);
  }

     
  public int GetAttackDirectiveHash(int id, int version, AttackDirectiveIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.targetUnit.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.pathToLastSeenLocation.GetDeterministicHashCode();
    return result;
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
    CheckUnlocked();
    var id = NewId();
    var incarnation =
        new DefendingDetailIncarnation(
            power
            );
    EffectInternalCreateDefendingDetail(id, incarnation);
    return new DefendingDetail(this, id);
  }
  public void EffectInternalCreateDefendingDetail(
      int id,
      DefendingDetailIncarnation incarnation) {
    CheckUnlocked();
    var effect = new DefendingDetailCreateEffect(id, incarnation);
    rootIncarnation.incarnationsDefendingDetail.Add(
        id,
        new VersionAndIncarnation<DefendingDetailIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetDefendingDetailHash(id, rootIncarnation.version, incarnation);
    BroadcastDefendingDetailEffect(id, effect);
  }

  public void EffectDefendingDetailDelete(int id) {
    CheckUnlocked();
    var effect = new DefendingDetailDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsDefendingDetail[id];
    this.rootIncarnation.hash -=
        GetDefendingDetailHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastDefendingDetailEffect(id, effect);
    rootIncarnation.incarnationsDefendingDetail.Remove(id);
  }

     
  public int GetDefendingDetailHash(int id, int version, DefendingDetailIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.power.GetDeterministicHashCode();
    return result;
  }
     
  public void EffectDefendingDetailSetPower(int id, int newValue) {
    CheckUnlocked();
    CheckHasDefendingDetail(id);
    var effect = new DefendingDetailSetPowerEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsDefendingDetail[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.power;
      oldIncarnationAndVersion.incarnation.power = newValue;
      this.rootIncarnation.hash -= id * rootIncarnation.version * 1 * oldValue.GetDeterministicHashCode();
      this.rootIncarnation.hash += id * rootIncarnation.version * 1 * newValue.GetDeterministicHashCode();
           
    } else {
      var newIncarnation =
          new DefendingDetailIncarnation(
              newValue);
      rootIncarnation.incarnationsDefendingDetail[id] =
          new VersionAndIncarnation<DefendingDetailIncarnation>(
              rootIncarnation.version,
              newIncarnation);
      this.rootIncarnation.hash -= GetDefendingDetailHash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
      this.rootIncarnation.hash += GetDefendingDetailHash(id, rootIncarnation.version, newIncarnation);
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
    CheckUnlocked();
    var id = NewId();
    var incarnation =
        new ArmorIncarnation(

            );
    EffectInternalCreateArmor(id, incarnation);
    return new Armor(this, id);
  }
  public void EffectInternalCreateArmor(
      int id,
      ArmorIncarnation incarnation) {
    CheckUnlocked();
    var effect = new ArmorCreateEffect(id, incarnation);
    rootIncarnation.incarnationsArmor.Add(
        id,
        new VersionAndIncarnation<ArmorIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetArmorHash(id, rootIncarnation.version, incarnation);
    BroadcastArmorEffect(id, effect);
  }

  public void EffectArmorDelete(int id) {
    CheckUnlocked();
    var effect = new ArmorDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsArmor[id];
    this.rootIncarnation.hash -=
        GetArmorHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastArmorEffect(id, effect);
    rootIncarnation.incarnationsArmor.Remove(id);
  }

     
  public int GetArmorHash(int id, int version, ArmorIncarnation incarnation) {
    int result = id * version;
    return result;
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
    CheckUnlocked();
    var id = NewId();
    var incarnation =
        new GlaiveIncarnation(

            );
    EffectInternalCreateGlaive(id, incarnation);
    return new Glaive(this, id);
  }
  public void EffectInternalCreateGlaive(
      int id,
      GlaiveIncarnation incarnation) {
    CheckUnlocked();
    var effect = new GlaiveCreateEffect(id, incarnation);
    rootIncarnation.incarnationsGlaive.Add(
        id,
        new VersionAndIncarnation<GlaiveIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetGlaiveHash(id, rootIncarnation.version, incarnation);
    BroadcastGlaiveEffect(id, effect);
  }

  public void EffectGlaiveDelete(int id) {
    CheckUnlocked();
    var effect = new GlaiveDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsGlaive[id];
    this.rootIncarnation.hash -=
        GetGlaiveHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastGlaiveEffect(id, effect);
    rootIncarnation.incarnationsGlaive.Remove(id);
  }

     
  public int GetGlaiveHash(int id, int version, GlaiveIncarnation incarnation) {
    int result = id * version;
    return result;
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
    CheckUnlocked();
    var id = NewId();
    var incarnation =
        new ExecutionStateIncarnation(
            actingUnit.id,
            actingUnitDidAction,
            remainingPreActingDetails.id,
            remainingPostActingDetails.id
            );
    EffectInternalCreateExecutionState(id, incarnation);
    return new ExecutionState(this, id);
  }
  public void EffectInternalCreateExecutionState(
      int id,
      ExecutionStateIncarnation incarnation) {
    CheckUnlocked();
    var effect = new ExecutionStateCreateEffect(id, incarnation);
    rootIncarnation.incarnationsExecutionState.Add(
        id,
        new VersionAndIncarnation<ExecutionStateIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetExecutionStateHash(id, rootIncarnation.version, incarnation);
    BroadcastExecutionStateEffect(id, effect);
  }

  public void EffectExecutionStateDelete(int id) {
    CheckUnlocked();
    var effect = new ExecutionStateDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsExecutionState[id];
    this.rootIncarnation.hash -=
        GetExecutionStateHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastExecutionStateEffect(id, effect);
    rootIncarnation.incarnationsExecutionState.Remove(id);
  }

     
  public int GetExecutionStateHash(int id, int version, ExecutionStateIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.actingUnit.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.actingUnitDidAction.GetDeterministicHashCode();
    result += id * version * 3 * incarnation.remainingPreActingDetails.GetDeterministicHashCode();
    result += id * version * 4 * incarnation.remainingPostActingDetails.GetDeterministicHashCode();
    return result;
  }
     
  public void EffectExecutionStateSetActingUnit(int id, Unit newValue) {
    CheckUnlocked();
    CheckHasExecutionState(id);
    var effect = new ExecutionStateSetActingUnitEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsExecutionState[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldId = oldIncarnationAndVersion.incarnation.actingUnit;
      oldIncarnationAndVersion.incarnation.actingUnit = newValue.id;
      this.rootIncarnation.hash -= id * rootIncarnation.version * 1 * oldId.GetDeterministicHashCode();
      this.rootIncarnation.hash += id * rootIncarnation.version * 1 * newValue.id.GetDeterministicHashCode();
           
    } else {
      var newIncarnation =
          new ExecutionStateIncarnation(
              newValue.id,
              oldIncarnationAndVersion.incarnation.actingUnitDidAction,
              oldIncarnationAndVersion.incarnation.remainingPreActingDetails,
              oldIncarnationAndVersion.incarnation.remainingPostActingDetails);
      rootIncarnation.incarnationsExecutionState[id] =
          new VersionAndIncarnation<ExecutionStateIncarnation>(
              rootIncarnation.version,
              newIncarnation);
      this.rootIncarnation.hash -= GetExecutionStateHash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
      this.rootIncarnation.hash += GetExecutionStateHash(id, rootIncarnation.version, newIncarnation);
    }

    BroadcastExecutionStateEffect(id, effect);
  }

  public void EffectExecutionStateSetActingUnitDidAction(int id, bool newValue) {
    CheckUnlocked();
    CheckHasExecutionState(id);
    var effect = new ExecutionStateSetActingUnitDidActionEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsExecutionState[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.actingUnitDidAction;
      oldIncarnationAndVersion.incarnation.actingUnitDidAction = newValue;
      this.rootIncarnation.hash -= id * rootIncarnation.version * 2 * oldValue.GetDeterministicHashCode();
      this.rootIncarnation.hash += id * rootIncarnation.version * 2 * newValue.GetDeterministicHashCode();
           
    } else {
      var newIncarnation =
          new ExecutionStateIncarnation(
              oldIncarnationAndVersion.incarnation.actingUnit,
              newValue,
              oldIncarnationAndVersion.incarnation.remainingPreActingDetails,
              oldIncarnationAndVersion.incarnation.remainingPostActingDetails);
      rootIncarnation.incarnationsExecutionState[id] =
          new VersionAndIncarnation<ExecutionStateIncarnation>(
              rootIncarnation.version,
              newIncarnation);
      this.rootIncarnation.hash -= GetExecutionStateHash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
      this.rootIncarnation.hash += GetExecutionStateHash(id, rootIncarnation.version, newIncarnation);
    }

    BroadcastExecutionStateEffect(id, effect);
  }

  public void EffectExecutionStateSetRemainingPreActingDetails(int id, IDetailMutList newValue) {
    CheckUnlocked();
    CheckHasExecutionState(id);
    var effect = new ExecutionStateSetRemainingPreActingDetailsEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsExecutionState[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldId = oldIncarnationAndVersion.incarnation.remainingPreActingDetails;
      oldIncarnationAndVersion.incarnation.remainingPreActingDetails = newValue.id;
      this.rootIncarnation.hash -= id * rootIncarnation.version * 3 * oldId.GetDeterministicHashCode();
      this.rootIncarnation.hash += id * rootIncarnation.version * 3 * newValue.id.GetDeterministicHashCode();
           
    } else {
      var newIncarnation =
          new ExecutionStateIncarnation(
              oldIncarnationAndVersion.incarnation.actingUnit,
              oldIncarnationAndVersion.incarnation.actingUnitDidAction,
              newValue.id,
              oldIncarnationAndVersion.incarnation.remainingPostActingDetails);
      rootIncarnation.incarnationsExecutionState[id] =
          new VersionAndIncarnation<ExecutionStateIncarnation>(
              rootIncarnation.version,
              newIncarnation);
      this.rootIncarnation.hash -= GetExecutionStateHash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
      this.rootIncarnation.hash += GetExecutionStateHash(id, rootIncarnation.version, newIncarnation);
    }

    BroadcastExecutionStateEffect(id, effect);
  }

  public void EffectExecutionStateSetRemainingPostActingDetails(int id, IDetailMutList newValue) {
    CheckUnlocked();
    CheckHasExecutionState(id);
    var effect = new ExecutionStateSetRemainingPostActingDetailsEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsExecutionState[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldId = oldIncarnationAndVersion.incarnation.remainingPostActingDetails;
      oldIncarnationAndVersion.incarnation.remainingPostActingDetails = newValue.id;
      this.rootIncarnation.hash -= id * rootIncarnation.version * 4 * oldId.GetDeterministicHashCode();
      this.rootIncarnation.hash += id * rootIncarnation.version * 4 * newValue.id.GetDeterministicHashCode();
           
    } else {
      var newIncarnation =
          new ExecutionStateIncarnation(
              oldIncarnationAndVersion.incarnation.actingUnit,
              oldIncarnationAndVersion.incarnation.actingUnitDidAction,
              oldIncarnationAndVersion.incarnation.remainingPreActingDetails,
              newValue.id);
      rootIncarnation.incarnationsExecutionState[id] =
          new VersionAndIncarnation<ExecutionStateIncarnation>(
              rootIncarnation.version,
              newIncarnation);
      this.rootIncarnation.hash -= GetExecutionStateHash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
      this.rootIncarnation.hash += GetExecutionStateHash(id, rootIncarnation.version, newIncarnation);
    }

    BroadcastExecutionStateEffect(id, effect);
  }

  public IFeature GetIFeature(int id) {

    if (rootIncarnation.incarnationsDownStaircaseFeature.ContainsKey(id)) {
      return new DownStaircaseFeatureAsIFeature(new DownStaircaseFeature(this, id));
    }

    if (rootIncarnation.incarnationsUpStaircaseFeature.ContainsKey(id)) {
      return new UpStaircaseFeatureAsIFeature(new UpStaircaseFeature(this, id));
    }

    if (rootIncarnation.incarnationsDecorativeFeature.ContainsKey(id)) {
      return new DecorativeFeatureAsIFeature(new DecorativeFeature(this, id));
    }

    return NullIFeature.Null;
  }
  public void CheckHasIFeature(IFeature thing) {
    GetIFeature(thing.id);
  }
  public void CheckHasIFeature(int id) {
    GetIFeature(id);
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

    public int GetLevelMutBunchHash(int id, int version, LevelMutBunchIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
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
      CheckUnlocked();
      var id = NewId();
      var incarnation = new LevelMutBunchIncarnation(new SortedSet<int>());
      EffectInternalCreateLevelMutBunch(id, incarnation);
      this.rootIncarnation.hash += GetLevelMutBunchHash(id, rootIncarnation.version, incarnation);
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
      CheckUnlocked();
      var effect = new LevelMutBunchDeleteEffect(id);
      BroadcastLevelMutBunchEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsLevelMutBunch[id];
      this.rootIncarnation.hash -=
          GetLevelMutBunchHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsLevelMutBunch.Remove(id);
    }

       
    public void EffectLevelMutBunchAdd(int bunchId, int elementId) {
      CheckUnlocked();
      CheckHasLevelMutBunch(bunchId);
      CheckHasLevel(elementId);

      var effect = new LevelMutBunchAddEffect(bunchId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsLevelMutBunch[bunchId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
        this.rootIncarnation.hash += bunchId * rootIncarnation.version * elementId.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new LevelMutBunchIncarnation(newMap);
        rootIncarnation.incarnationsLevelMutBunch[bunchId] =
            new VersionAndIncarnation<LevelMutBunchIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetLevelMutBunchHash(bunchId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetLevelMutBunchHash(bunchId, rootIncarnation.version, newIncarnation);
      }
      BroadcastLevelMutBunchEffect(bunchId, effect);
    }
    public void EffectLevelMutBunchRemove(int bunchId, int elementId) {
      CheckUnlocked();
      CheckHasLevelMutBunch(bunchId);
      CheckHasLevel(elementId);

      var effect = new LevelMutBunchRemoveEffect(bunchId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsLevelMutBunch[bunchId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        this.rootIncarnation.hash -= bunchId * rootIncarnation.version * elementId.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new LevelMutBunchIncarnation(newMap);
        rootIncarnation.incarnationsLevelMutBunch[bunchId] =
            new VersionAndIncarnation<LevelMutBunchIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetLevelMutBunchHash(bunchId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetLevelMutBunchHash(bunchId, rootIncarnation.version, newIncarnation);
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

    public int GetUnitMutBunchHash(int id, int version, UnitMutBunchIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
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
      CheckUnlocked();
      var id = NewId();
      var incarnation = new UnitMutBunchIncarnation(new SortedSet<int>());
      EffectInternalCreateUnitMutBunch(id, incarnation);
      this.rootIncarnation.hash += GetUnitMutBunchHash(id, rootIncarnation.version, incarnation);
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
      CheckUnlocked();
      var effect = new UnitMutBunchDeleteEffect(id);
      BroadcastUnitMutBunchEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsUnitMutBunch[id];
      this.rootIncarnation.hash -=
          GetUnitMutBunchHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsUnitMutBunch.Remove(id);
    }

       
    public void EffectUnitMutBunchAdd(int bunchId, int elementId) {
      CheckUnlocked();
      CheckHasUnitMutBunch(bunchId);
      CheckHasUnit(elementId);

      var effect = new UnitMutBunchAddEffect(bunchId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsUnitMutBunch[bunchId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
        this.rootIncarnation.hash += bunchId * rootIncarnation.version * elementId.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new UnitMutBunchIncarnation(newMap);
        rootIncarnation.incarnationsUnitMutBunch[bunchId] =
            new VersionAndIncarnation<UnitMutBunchIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetUnitMutBunchHash(bunchId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetUnitMutBunchHash(bunchId, rootIncarnation.version, newIncarnation);
      }
      BroadcastUnitMutBunchEffect(bunchId, effect);
    }
    public void EffectUnitMutBunchRemove(int bunchId, int elementId) {
      CheckUnlocked();
      CheckHasUnitMutBunch(bunchId);
      CheckHasUnit(elementId);

      var effect = new UnitMutBunchRemoveEffect(bunchId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsUnitMutBunch[bunchId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        this.rootIncarnation.hash -= bunchId * rootIncarnation.version * elementId.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new UnitMutBunchIncarnation(newMap);
        rootIncarnation.incarnationsUnitMutBunch[bunchId] =
            new VersionAndIncarnation<UnitMutBunchIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetUnitMutBunchHash(bunchId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetUnitMutBunchHash(bunchId, rootIncarnation.version, newIncarnation);
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

    public int GetIItemMutBunchHash(int id, int version, IItemMutBunchIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
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
      CheckUnlocked();
      var id = NewId();
      var incarnation = new IItemMutBunchIncarnation(new SortedSet<int>());
      EffectInternalCreateIItemMutBunch(id, incarnation);
      this.rootIncarnation.hash += GetIItemMutBunchHash(id, rootIncarnation.version, incarnation);
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
      CheckUnlocked();
      var effect = new IItemMutBunchDeleteEffect(id);
      BroadcastIItemMutBunchEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsIItemMutBunch[id];
      this.rootIncarnation.hash -=
          GetIItemMutBunchHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsIItemMutBunch.Remove(id);
    }

       
    public void EffectIItemMutBunchAdd(int bunchId, int elementId) {
      CheckUnlocked();
      CheckHasIItemMutBunch(bunchId);
      CheckHasIItem(elementId);

      var effect = new IItemMutBunchAddEffect(bunchId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsIItemMutBunch[bunchId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
        this.rootIncarnation.hash += bunchId * rootIncarnation.version * elementId.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new IItemMutBunchIncarnation(newMap);
        rootIncarnation.incarnationsIItemMutBunch[bunchId] =
            new VersionAndIncarnation<IItemMutBunchIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetIItemMutBunchHash(bunchId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetIItemMutBunchHash(bunchId, rootIncarnation.version, newIncarnation);
      }
      BroadcastIItemMutBunchEffect(bunchId, effect);
    }
    public void EffectIItemMutBunchRemove(int bunchId, int elementId) {
      CheckUnlocked();
      CheckHasIItemMutBunch(bunchId);
      CheckHasIItem(elementId);

      var effect = new IItemMutBunchRemoveEffect(bunchId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsIItemMutBunch[bunchId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        this.rootIncarnation.hash -= bunchId * rootIncarnation.version * elementId.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new IItemMutBunchIncarnation(newMap);
        rootIncarnation.incarnationsIItemMutBunch[bunchId] =
            new VersionAndIncarnation<IItemMutBunchIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetIItemMutBunchHash(bunchId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetIItemMutBunchHash(bunchId, rootIncarnation.version, newIncarnation);
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

    public int GetLocationMutListHash(int id, int version, LocationMutListIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.list) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
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
      CheckUnlocked();
      var id = NewId();
      EffectInternalCreateLocationMutList(id, new LocationMutListIncarnation(new List<Location>()));
      return new LocationMutList(this, id);
    }
    public LocationMutList EffectLocationMutListCreate(List<Location> elements) {
      var id = NewId();
      var incarnation = new LocationMutListIncarnation(new List<Location>(elements));
      EffectInternalCreateLocationMutList(id, incarnation);
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
      this.rootIncarnation.hash += GetLocationMutListHash(id, rootIncarnation.version, incarnation);
      BroadcastLocationMutListEffect(id, effect);
    }
    public void EffectLocationMutListDelete(int id) {
      CheckUnlocked();
      var effect = new LocationMutListDeleteEffect(id);
      BroadcastLocationMutListEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsLocationMutList[id];
      this.rootIncarnation.hash -=
          GetLocationMutListHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsLocationMutList.Remove(id);
    }
    public void EffectLocationMutListAdd(int listId, Location element) {
      CheckUnlocked();
      CheckHasLocationMutList(listId);

    
      var effect = new LocationMutListAddEffect(listId, element);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsLocationMutList[listId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.list.Add(element);
        this.rootIncarnation.hash += listId * rootIncarnation.version * element.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.list;
        var newMap = new List<Location>(oldMap);
        newMap.Add(element);
        var newIncarnation = new LocationMutListIncarnation(newMap);
        rootIncarnation.incarnationsLocationMutList[listId] =
            new VersionAndIncarnation<LocationMutListIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetLocationMutListHash(listId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetLocationMutListHash(listId, rootIncarnation.version, newIncarnation);
      }
      BroadcastLocationMutListEffect(listId, effect);
    }
    public void EffectLocationMutListRemoveAt(int listId, int index) {
      CheckUnlocked();
      CheckHasLocationMutList(listId);

      var effect = new LocationMutListRemoveEffect(listId, index);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsLocationMutList[listId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        var oldElement = oldIncarnationAndVersion.incarnation.list[index];
        this.rootIncarnation.hash -= listId * rootIncarnation.version * oldElement.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.list.RemoveAt(index);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.list;
        var newMap = new List<Location>(oldMap);
        newMap.RemoveAt(index);
        var newIncarnation = new LocationMutListIncarnation(newMap);
        rootIncarnation.incarnationsLocationMutList[listId] =
            new VersionAndIncarnation<LocationMutListIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetLocationMutListHash(listId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetLocationMutListHash(listId, rootIncarnation.version, newIncarnation);
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

    public int GetIFeatureMutListHash(int id, int version, IFeatureMutListIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.list) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public IFeatureMutListIncarnation GetIFeatureMutListIncarnation(int id) {
      return rootIncarnation.incarnationsIFeatureMutList[id].incarnation;
    }
    public IFeatureMutList GetIFeatureMutList(int id) {
      return new IFeatureMutList(this, id);
    }
    public bool IFeatureMutListExists(int id) {
      return rootIncarnation.incarnationsIFeatureMutList.ContainsKey(id);
    }
    public void CheckHasIFeatureMutList(IFeatureMutList thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasIFeatureMutList(thing.id);
    }
    public void CheckHasIFeatureMutList(int id) {
      if (!rootIncarnation.incarnationsIFeatureMutList.ContainsKey(id)) {
        throw new System.Exception("Invalid IFeatureMutList}!");
      }
    }
    public IFeatureMutList EffectIFeatureMutListCreate() {
      CheckUnlocked();
      var id = NewId();
      EffectInternalCreateIFeatureMutList(id, new IFeatureMutListIncarnation(new List<int>()));
      return new IFeatureMutList(this, id);
    }
    public IFeatureMutList EffectIFeatureMutListCreate(List<IFeature> elements) {
      var id = NewId();
      var elementsIds = new List<int>();
      foreach (var element in elements) {
        elementsIds.Add(element.id);
      }
      var incarnation = new IFeatureMutListIncarnation(elementsIds);
      EffectInternalCreateIFeatureMutList(id, incarnation);
      return new IFeatureMutList(this, id);
    }
    public void EffectInternalCreateIFeatureMutList(int id, IFeatureMutListIncarnation incarnation) {
      var effect = new IFeatureMutListCreateEffect(id, incarnation);
      rootIncarnation.incarnationsIFeatureMutList
          .Add(
              id,
              new VersionAndIncarnation<IFeatureMutListIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      this.rootIncarnation.hash += GetIFeatureMutListHash(id, rootIncarnation.version, incarnation);
      BroadcastIFeatureMutListEffect(id, effect);
    }
    public void EffectIFeatureMutListDelete(int id) {
      CheckUnlocked();
      var effect = new IFeatureMutListDeleteEffect(id);
      BroadcastIFeatureMutListEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsIFeatureMutList[id];
      this.rootIncarnation.hash -=
          GetIFeatureMutListHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsIFeatureMutList.Remove(id);
    }
    public void EffectIFeatureMutListAdd(int listId, int element) {
      CheckUnlocked();
      CheckHasIFeatureMutList(listId);

          CheckHasIFeature(element);
      var effect = new IFeatureMutListAddEffect(listId, element);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsIFeatureMutList[listId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.list.Add(element);
        this.rootIncarnation.hash += listId * rootIncarnation.version * element.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.list;
        var newMap = new List<int>(oldMap);
        newMap.Add(element);
        var newIncarnation = new IFeatureMutListIncarnation(newMap);
        rootIncarnation.incarnationsIFeatureMutList[listId] =
            new VersionAndIncarnation<IFeatureMutListIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetIFeatureMutListHash(listId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetIFeatureMutListHash(listId, rootIncarnation.version, newIncarnation);
      }
      BroadcastIFeatureMutListEffect(listId, effect);
    }
    public void EffectIFeatureMutListRemoveAt(int listId, int index) {
      CheckUnlocked();
      CheckHasIFeatureMutList(listId);

      var effect = new IFeatureMutListRemoveEffect(listId, index);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsIFeatureMutList[listId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        var oldElement = oldIncarnationAndVersion.incarnation.list[index];
        this.rootIncarnation.hash -= listId * rootIncarnation.version * oldElement.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.list.RemoveAt(index);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.list;
        var newMap = new List<int>(oldMap);
        newMap.RemoveAt(index);
        var newIncarnation = new IFeatureMutListIncarnation(newMap);
        rootIncarnation.incarnationsIFeatureMutList[listId] =
            new VersionAndIncarnation<IFeatureMutListIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetIFeatureMutListHash(listId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetIFeatureMutListHash(listId, rootIncarnation.version, newIncarnation);
      }
      BroadcastIFeatureMutListEffect(listId, effect);
    }
       
    public void AddIFeatureMutListObserver(int id, IIFeatureMutListEffectObserver observer) {
      List<IIFeatureMutListEffectObserver> obsies;
      if (!observersIFeatureMutList.TryGetValue(id, out obsies)) {
        obsies = new List<IIFeatureMutListEffectObserver>();
      }
      obsies.Add(observer);
      observersIFeatureMutList[id] = obsies;
    }

    public void RemoveIFeatureMutListObserver(int id, IIFeatureMutListEffectObserver observer) {
      if (observersIFeatureMutList.ContainsKey(id)) {
        var list = observersIFeatureMutList[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersIFeatureMutList.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void BroadcastIFeatureMutListEffect(int id, IIFeatureMutListEffect effect) {
      if (observersIFeatureMutList.ContainsKey(0)) {
        foreach (var observer in new List<IIFeatureMutListEffectObserver>(observersIFeatureMutList[0])) {
          observer.OnIFeatureMutListEffect(effect);
        }
      }
      if (observersIFeatureMutList.ContainsKey(id)) {
        foreach (var observer in new List<IIFeatureMutListEffectObserver>(observersIFeatureMutList[id])) {
          observer.OnIFeatureMutListEffect(effect);
        }
      }
    }

    public int GetIUnitEventMutListHash(int id, int version, IUnitEventMutListIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.list) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
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
      CheckUnlocked();
      var id = NewId();
      EffectInternalCreateIUnitEventMutList(id, new IUnitEventMutListIncarnation(new List<IUnitEvent>()));
      return new IUnitEventMutList(this, id);
    }
    public IUnitEventMutList EffectIUnitEventMutListCreate(List<IUnitEvent> elements) {
      var id = NewId();
      var incarnation = new IUnitEventMutListIncarnation(new List<IUnitEvent>(elements));
      EffectInternalCreateIUnitEventMutList(id, incarnation);
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
      this.rootIncarnation.hash += GetIUnitEventMutListHash(id, rootIncarnation.version, incarnation);
      BroadcastIUnitEventMutListEffect(id, effect);
    }
    public void EffectIUnitEventMutListDelete(int id) {
      CheckUnlocked();
      var effect = new IUnitEventMutListDeleteEffect(id);
      BroadcastIUnitEventMutListEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsIUnitEventMutList[id];
      this.rootIncarnation.hash -=
          GetIUnitEventMutListHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsIUnitEventMutList.Remove(id);
    }
    public void EffectIUnitEventMutListAdd(int listId, IUnitEvent element) {
      CheckUnlocked();
      CheckHasIUnitEventMutList(listId);

    
      var effect = new IUnitEventMutListAddEffect(listId, element);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsIUnitEventMutList[listId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.list.Add(element);
        this.rootIncarnation.hash += listId * rootIncarnation.version * element.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.list;
        var newMap = new List<IUnitEvent>(oldMap);
        newMap.Add(element);
        var newIncarnation = new IUnitEventMutListIncarnation(newMap);
        rootIncarnation.incarnationsIUnitEventMutList[listId] =
            new VersionAndIncarnation<IUnitEventMutListIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetIUnitEventMutListHash(listId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetIUnitEventMutListHash(listId, rootIncarnation.version, newIncarnation);
      }
      BroadcastIUnitEventMutListEffect(listId, effect);
    }
    public void EffectIUnitEventMutListRemoveAt(int listId, int index) {
      CheckUnlocked();
      CheckHasIUnitEventMutList(listId);

      var effect = new IUnitEventMutListRemoveEffect(listId, index);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsIUnitEventMutList[listId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        var oldElement = oldIncarnationAndVersion.incarnation.list[index];
        this.rootIncarnation.hash -= listId * rootIncarnation.version * oldElement.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.list.RemoveAt(index);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.list;
        var newMap = new List<IUnitEvent>(oldMap);
        newMap.RemoveAt(index);
        var newIncarnation = new IUnitEventMutListIncarnation(newMap);
        rootIncarnation.incarnationsIUnitEventMutList[listId] =
            new VersionAndIncarnation<IUnitEventMutListIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetIUnitEventMutListHash(listId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetIUnitEventMutListHash(listId, rootIncarnation.version, newIncarnation);
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

    public int GetIDetailMutListHash(int id, int version, IDetailMutListIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.list) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
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
      CheckUnlocked();
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
      var incarnation = new IDetailMutListIncarnation(elementsIds);
      EffectInternalCreateIDetailMutList(id, incarnation);
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
      this.rootIncarnation.hash += GetIDetailMutListHash(id, rootIncarnation.version, incarnation);
      BroadcastIDetailMutListEffect(id, effect);
    }
    public void EffectIDetailMutListDelete(int id) {
      CheckUnlocked();
      var effect = new IDetailMutListDeleteEffect(id);
      BroadcastIDetailMutListEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsIDetailMutList[id];
      this.rootIncarnation.hash -=
          GetIDetailMutListHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsIDetailMutList.Remove(id);
    }
    public void EffectIDetailMutListAdd(int listId, int element) {
      CheckUnlocked();
      CheckHasIDetailMutList(listId);

          CheckHasIDetail(element);
      var effect = new IDetailMutListAddEffect(listId, element);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsIDetailMutList[listId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.list.Add(element);
        this.rootIncarnation.hash += listId * rootIncarnation.version * element.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.list;
        var newMap = new List<int>(oldMap);
        newMap.Add(element);
        var newIncarnation = new IDetailMutListIncarnation(newMap);
        rootIncarnation.incarnationsIDetailMutList[listId] =
            new VersionAndIncarnation<IDetailMutListIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetIDetailMutListHash(listId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetIDetailMutListHash(listId, rootIncarnation.version, newIncarnation);
      }
      BroadcastIDetailMutListEffect(listId, effect);
    }
    public void EffectIDetailMutListRemoveAt(int listId, int index) {
      CheckUnlocked();
      CheckHasIDetailMutList(listId);

      var effect = new IDetailMutListRemoveEffect(listId, index);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsIDetailMutList[listId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        var oldElement = oldIncarnationAndVersion.incarnation.list[index];
        this.rootIncarnation.hash -= listId * rootIncarnation.version * oldElement.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.list.RemoveAt(index);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.list;
        var newMap = new List<int>(oldMap);
        newMap.RemoveAt(index);
        var newIncarnation = new IDetailMutListIncarnation(newMap);
        rootIncarnation.incarnationsIDetailMutList[listId] =
            new VersionAndIncarnation<IDetailMutListIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetIDetailMutListHash(listId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetIDetailMutListHash(listId, rootIncarnation.version, newIncarnation);
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

    public int GetTerrainTileByLocationMutMapHash(int id, int version, TerrainTileByLocationMutMapIncarnation incarnation) {
      int result = id * version;
      foreach (var entry in incarnation.map) {
        result += id * version * entry.Key.GetDeterministicHashCode() * entry.Value.GetDeterministicHashCode();
      }
      return result;
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
      CheckUnlocked();
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
      this.rootIncarnation.hash += GetTerrainTileByLocationMutMapHash(id, rootIncarnation.version, incarnation);
      BroadcastTerrainTileByLocationMutMapEffect(id, effect);
    }
    public void EffectTerrainTileByLocationMutMapDelete(int id) {
      CheckUnlocked();
      var effect = new TerrainTileByLocationMutMapDeleteEffect(id);
      BroadcastTerrainTileByLocationMutMapEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsTerrainTileByLocationMutMap[id];
      this.rootIncarnation.hash -=
          GetTerrainTileByLocationMutMapHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsTerrainTileByLocationMutMap.Remove(id);
    }
    public void EffectTerrainTileByLocationMutMapAdd(int mapId, Location key, int value) {
      CheckUnlocked();
      CheckHasTerrainTileByLocationMutMap(mapId);
      CheckHasTerrainTile(value);

      var effect = new TerrainTileByLocationMutMapAddEffect(mapId, key, value);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsTerrainTileByLocationMutMap[mapId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.map.ContainsKey(key));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.map.Add(key, value);
        this.rootIncarnation.hash += mapId * rootIncarnation.version * key.GetDeterministicHashCode() * value.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.map;
        var newMap = new SortedDictionary<Location, int>(oldMap);
        newMap.Add(key, value);
        var newIncarnation = new TerrainTileByLocationMutMapIncarnation(newMap);
        rootIncarnation.incarnationsTerrainTileByLocationMutMap[mapId] =
            new VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetTerrainTileByLocationMutMapHash(mapId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetTerrainTileByLocationMutMapHash(mapId, rootIncarnation.version, newIncarnation);
      }
      BroadcastTerrainTileByLocationMutMapEffect(mapId, effect);
    }
       
    public void EffectTerrainTileByLocationMutMapRemove(int mapId, Location key) {
      CheckUnlocked();
      CheckHasTerrainTileByLocationMutMap(mapId);

      var effect = new TerrainTileByLocationMutMapRemoveEffect(mapId, key);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsTerrainTileByLocationMutMap[mapId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.map.ContainsKey(key));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        var oldValue = oldIncarnationAndVersion.incarnation.map[key];
        oldIncarnationAndVersion.incarnation.map.Remove(key);
        this.rootIncarnation.hash -= mapId * rootIncarnation.version * key.GetDeterministicHashCode() * oldValue.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.map;
        var newMap = new SortedDictionary<Location, int>(oldMap);
        newMap.Remove(key);
        var newIncarnation = new TerrainTileByLocationMutMapIncarnation(newMap);
        rootIncarnation.incarnationsTerrainTileByLocationMutMap[mapId] =
            new VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetTerrainTileByLocationMutMapHash(mapId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetTerrainTileByLocationMutMapHash(mapId, rootIncarnation.version, newIncarnation);
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
