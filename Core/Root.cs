using System;
using System.Collections;

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

  public readonly ILogger logger;

  // This *always* points to a live RootIncarnation. When we snapshot, we eagerly
  // make a new one of these.
  private RootIncarnation rootIncarnation;

  bool locked = true;

  // 0 means everything
  readonly SortedDictionary<int, List<ILevelEffectObserver>> observersLevel;
  readonly SortedDictionary<int, List<IDecorativeTerrainTileComponentEffectObserver>> observersDecorativeTerrainTileComponent;
  readonly SortedDictionary<int, List<IUpStaircaseTerrainTileComponentEffectObserver>> observersUpStaircaseTerrainTileComponent;
  readonly SortedDictionary<int, List<IDownStaircaseTerrainTileComponentEffectObserver>> observersDownStaircaseTerrainTileComponent;
  readonly SortedDictionary<int, List<ITerrainTileEffectObserver>> observersTerrainTile;
  readonly SortedDictionary<int, List<IITerrainTileComponentMutBunchEffectObserver>> observersITerrainTileComponentMutBunch;
  readonly SortedDictionary<int, List<ITerrainEffectObserver>> observersTerrain;
  readonly SortedDictionary<int, List<IGlaiveEffectObserver>> observersGlaive;
  readonly SortedDictionary<int, List<IArmorEffectObserver>> observersArmor;
  readonly SortedDictionary<int, List<IRandEffectObserver>> observersRand;
  readonly SortedDictionary<int, List<IWanderAICapabilityUCEffectObserver>> observersWanderAICapabilityUC;
  readonly SortedDictionary<int, List<IShieldingUCEffectObserver>> observersShieldingUC;
  readonly SortedDictionary<int, List<IBidingOperationUCEffectObserver>> observersBidingOperationUC;
  readonly SortedDictionary<int, List<IUnleashBideImpulseEffectObserver>> observersUnleashBideImpulse;
  readonly SortedDictionary<int, List<IStartBidingImpulseEffectObserver>> observersStartBidingImpulse;
  readonly SortedDictionary<int, List<IBideAICapabilityUCEffectObserver>> observersBideAICapabilityUC;
  readonly SortedDictionary<int, List<IAttackImpulseEffectObserver>> observersAttackImpulse;
  readonly SortedDictionary<int, List<IPursueImpulseEffectObserver>> observersPursueImpulse;
  readonly SortedDictionary<int, List<IKillDirectiveUCEffectObserver>> observersKillDirectiveUC;
  readonly SortedDictionary<int, List<IAttackAICapabilityUCEffectObserver>> observersAttackAICapabilityUC;
  readonly SortedDictionary<int, List<IMoveImpulseEffectObserver>> observersMoveImpulse;
  readonly SortedDictionary<int, List<IMoveDirectiveUCEffectObserver>> observersMoveDirectiveUC;
  readonly SortedDictionary<int, List<IUnitEffectObserver>> observersUnit;
  readonly SortedDictionary<int, List<IIItemMutBunchEffectObserver>> observersIItemMutBunch;
  readonly SortedDictionary<int, List<IIUnitComponentMutBunchEffectObserver>> observersIUnitComponentMutBunch;
  readonly SortedDictionary<int, List<INoImpulseEffectObserver>> observersNoImpulse;
  readonly SortedDictionary<int, List<IExecutionStateEffectObserver>> observersExecutionState;
  readonly SortedDictionary<int, List<IIPostActingUCWeakMutBunchEffectObserver>> observersIPostActingUCWeakMutBunch;
  readonly SortedDictionary<int, List<IIPreActingUCWeakMutBunchEffectObserver>> observersIPreActingUCWeakMutBunch;
  readonly SortedDictionary<int, List<IGameEffectObserver>> observersGame;
  readonly SortedDictionary<int, List<IIUnitEventMutListEffectObserver>> observersIUnitEventMutList;
  readonly SortedDictionary<int, List<ILocationMutListEffectObserver>> observersLocationMutList;
  readonly SortedDictionary<int, List<ILevelMutSetEffectObserver>> observersLevelMutSet;
  readonly SortedDictionary<int, List<IShieldingUCWeakMutSetEffectObserver>> observersShieldingUCWeakMutSet;
  readonly SortedDictionary<int, List<IAttackAICapabilityUCWeakMutSetEffectObserver>> observersAttackAICapabilityUCWeakMutSet;
  readonly SortedDictionary<int, List<IKillDirectiveUCMutSetEffectObserver>> observersKillDirectiveUCMutSet;
  readonly SortedDictionary<int, List<IMoveDirectiveUCMutSetEffectObserver>> observersMoveDirectiveUCMutSet;
  readonly SortedDictionary<int, List<IWanderAICapabilityUCMutSetEffectObserver>> observersWanderAICapabilityUCMutSet;
  readonly SortedDictionary<int, List<IBideAICapabilityUCMutSetEffectObserver>> observersBideAICapabilityUCMutSet;
  readonly SortedDictionary<int, List<IAttackAICapabilityUCMutSetEffectObserver>> observersAttackAICapabilityUCMutSet;
  readonly SortedDictionary<int, List<IShieldingUCMutSetEffectObserver>> observersShieldingUCMutSet;
  readonly SortedDictionary<int, List<IBidingOperationUCMutSetEffectObserver>> observersBidingOperationUCMutSet;
  readonly SortedDictionary<int, List<IGlaiveMutSetEffectObserver>> observersGlaiveMutSet;
  readonly SortedDictionary<int, List<IArmorMutSetEffectObserver>> observersArmorMutSet;
  readonly SortedDictionary<int, List<IDecorativeTerrainTileComponentMutSetEffectObserver>> observersDecorativeTerrainTileComponentMutSet;
  readonly SortedDictionary<int, List<IUpStaircaseTerrainTileComponentMutSetEffectObserver>> observersUpStaircaseTerrainTileComponentMutSet;
  readonly SortedDictionary<int, List<IDownStaircaseTerrainTileComponentMutSetEffectObserver>> observersDownStaircaseTerrainTileComponentMutSet;
  readonly SortedDictionary<int, List<IUnitMutSetEffectObserver>> observersUnitMutSet;
  readonly SortedDictionary<int, List<ITerrainTileByLocationMutMapEffectObserver>> observersTerrainTileByLocationMutMap;

  public Root(ILogger logger) {
    this.logger = logger;
    this.observersLevel = new SortedDictionary<int, List<ILevelEffectObserver>>();
    this.observersDecorativeTerrainTileComponent = new SortedDictionary<int, List<IDecorativeTerrainTileComponentEffectObserver>>();
    this.observersUpStaircaseTerrainTileComponent = new SortedDictionary<int, List<IUpStaircaseTerrainTileComponentEffectObserver>>();
    this.observersDownStaircaseTerrainTileComponent = new SortedDictionary<int, List<IDownStaircaseTerrainTileComponentEffectObserver>>();
    this.observersTerrainTile = new SortedDictionary<int, List<ITerrainTileEffectObserver>>();
    this.observersITerrainTileComponentMutBunch = new SortedDictionary<int, List<IITerrainTileComponentMutBunchEffectObserver>>();
    this.observersTerrain = new SortedDictionary<int, List<ITerrainEffectObserver>>();
    this.observersGlaive = new SortedDictionary<int, List<IGlaiveEffectObserver>>();
    this.observersArmor = new SortedDictionary<int, List<IArmorEffectObserver>>();
    this.observersRand = new SortedDictionary<int, List<IRandEffectObserver>>();
    this.observersWanderAICapabilityUC = new SortedDictionary<int, List<IWanderAICapabilityUCEffectObserver>>();
    this.observersShieldingUC = new SortedDictionary<int, List<IShieldingUCEffectObserver>>();
    this.observersBidingOperationUC = new SortedDictionary<int, List<IBidingOperationUCEffectObserver>>();
    this.observersUnleashBideImpulse = new SortedDictionary<int, List<IUnleashBideImpulseEffectObserver>>();
    this.observersStartBidingImpulse = new SortedDictionary<int, List<IStartBidingImpulseEffectObserver>>();
    this.observersBideAICapabilityUC = new SortedDictionary<int, List<IBideAICapabilityUCEffectObserver>>();
    this.observersAttackImpulse = new SortedDictionary<int, List<IAttackImpulseEffectObserver>>();
    this.observersPursueImpulse = new SortedDictionary<int, List<IPursueImpulseEffectObserver>>();
    this.observersKillDirectiveUC = new SortedDictionary<int, List<IKillDirectiveUCEffectObserver>>();
    this.observersAttackAICapabilityUC = new SortedDictionary<int, List<IAttackAICapabilityUCEffectObserver>>();
    this.observersMoveImpulse = new SortedDictionary<int, List<IMoveImpulseEffectObserver>>();
    this.observersMoveDirectiveUC = new SortedDictionary<int, List<IMoveDirectiveUCEffectObserver>>();
    this.observersUnit = new SortedDictionary<int, List<IUnitEffectObserver>>();
    this.observersIItemMutBunch = new SortedDictionary<int, List<IIItemMutBunchEffectObserver>>();
    this.observersIUnitComponentMutBunch = new SortedDictionary<int, List<IIUnitComponentMutBunchEffectObserver>>();
    this.observersNoImpulse = new SortedDictionary<int, List<INoImpulseEffectObserver>>();
    this.observersExecutionState = new SortedDictionary<int, List<IExecutionStateEffectObserver>>();
    this.observersIPostActingUCWeakMutBunch = new SortedDictionary<int, List<IIPostActingUCWeakMutBunchEffectObserver>>();
    this.observersIPreActingUCWeakMutBunch = new SortedDictionary<int, List<IIPreActingUCWeakMutBunchEffectObserver>>();
    this.observersGame = new SortedDictionary<int, List<IGameEffectObserver>>();
    this.observersIUnitEventMutList = new SortedDictionary<int, List<IIUnitEventMutListEffectObserver>>();
    this.observersLocationMutList = new SortedDictionary<int, List<ILocationMutListEffectObserver>>();
    this.observersLevelMutSet = new SortedDictionary<int, List<ILevelMutSetEffectObserver>>();
    this.observersShieldingUCWeakMutSet = new SortedDictionary<int, List<IShieldingUCWeakMutSetEffectObserver>>();
    this.observersAttackAICapabilityUCWeakMutSet = new SortedDictionary<int, List<IAttackAICapabilityUCWeakMutSetEffectObserver>>();
    this.observersKillDirectiveUCMutSet = new SortedDictionary<int, List<IKillDirectiveUCMutSetEffectObserver>>();
    this.observersMoveDirectiveUCMutSet = new SortedDictionary<int, List<IMoveDirectiveUCMutSetEffectObserver>>();
    this.observersWanderAICapabilityUCMutSet = new SortedDictionary<int, List<IWanderAICapabilityUCMutSetEffectObserver>>();
    this.observersBideAICapabilityUCMutSet = new SortedDictionary<int, List<IBideAICapabilityUCMutSetEffectObserver>>();
    this.observersAttackAICapabilityUCMutSet = new SortedDictionary<int, List<IAttackAICapabilityUCMutSetEffectObserver>>();
    this.observersShieldingUCMutSet = new SortedDictionary<int, List<IShieldingUCMutSetEffectObserver>>();
    this.observersBidingOperationUCMutSet = new SortedDictionary<int, List<IBidingOperationUCMutSetEffectObserver>>();
    this.observersGlaiveMutSet = new SortedDictionary<int, List<IGlaiveMutSetEffectObserver>>();
    this.observersArmorMutSet = new SortedDictionary<int, List<IArmorMutSetEffectObserver>>();
    this.observersDecorativeTerrainTileComponentMutSet = new SortedDictionary<int, List<IDecorativeTerrainTileComponentMutSetEffectObserver>>();
    this.observersUpStaircaseTerrainTileComponentMutSet = new SortedDictionary<int, List<IUpStaircaseTerrainTileComponentMutSetEffectObserver>>();
    this.observersDownStaircaseTerrainTileComponentMutSet = new SortedDictionary<int, List<IDownStaircaseTerrainTileComponentMutSetEffectObserver>>();
    this.observersUnitMutSet = new SortedDictionary<int, List<IUnitMutSetEffectObserver>>();
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

    foreach (var entry in this.rootIncarnation.incarnationsLevel) {
      result += GetLevelHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsDecorativeTerrainTileComponent) {
      result += GetDecorativeTerrainTileComponentHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsUpStaircaseTerrainTileComponent) {
      result += GetUpStaircaseTerrainTileComponentHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsDownStaircaseTerrainTileComponent) {
      result += GetDownStaircaseTerrainTileComponentHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsTerrainTile) {
      result += GetTerrainTileHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsITerrainTileComponentMutBunch) {
      result += GetITerrainTileComponentMutBunchHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsTerrain) {
      result += GetTerrainHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsGlaive) {
      result += GetGlaiveHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsArmor) {
      result += GetArmorHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsRand) {
      result += GetRandHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsWanderAICapabilityUC) {
      result += GetWanderAICapabilityUCHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsShieldingUC) {
      result += GetShieldingUCHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsBidingOperationUC) {
      result += GetBidingOperationUCHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsUnleashBideImpulse) {
      result += GetUnleashBideImpulseHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsStartBidingImpulse) {
      result += GetStartBidingImpulseHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsBideAICapabilityUC) {
      result += GetBideAICapabilityUCHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsAttackImpulse) {
      result += GetAttackImpulseHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsPursueImpulse) {
      result += GetPursueImpulseHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsKillDirectiveUC) {
      result += GetKillDirectiveUCHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsAttackAICapabilityUC) {
      result += GetAttackAICapabilityUCHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsMoveImpulse) {
      result += GetMoveImpulseHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsMoveDirectiveUC) {
      result += GetMoveDirectiveUCHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsUnit) {
      result += GetUnitHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsIItemMutBunch) {
      result += GetIItemMutBunchHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsIUnitComponentMutBunch) {
      result += GetIUnitComponentMutBunchHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsNoImpulse) {
      result += GetNoImpulseHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsExecutionState) {
      result += GetExecutionStateHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsIPostActingUCWeakMutBunch) {
      result += GetIPostActingUCWeakMutBunchHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsIPreActingUCWeakMutBunch) {
      result += GetIPreActingUCWeakMutBunchHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsGame) {
      result += GetGameHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsIUnitEventMutList) {
      result += GetIUnitEventMutListHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsLocationMutList) {
      result += GetLocationMutListHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsLevelMutSet) {
      result += GetLevelMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsShieldingUCWeakMutSet) {
      result += GetShieldingUCWeakMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet) {
      result += GetAttackAICapabilityUCWeakMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsKillDirectiveUCMutSet) {
      result += GetKillDirectiveUCMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsMoveDirectiveUCMutSet) {
      result += GetMoveDirectiveUCMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsWanderAICapabilityUCMutSet) {
      result += GetWanderAICapabilityUCMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsBideAICapabilityUCMutSet) {
      result += GetBideAICapabilityUCMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsAttackAICapabilityUCMutSet) {
      result += GetAttackAICapabilityUCMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsShieldingUCMutSet) {
      result += GetShieldingUCMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsBidingOperationUCMutSet) {
      result += GetBidingOperationUCMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsGlaiveMutSet) {
      result += GetGlaiveMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsArmorMutSet) {
      result += GetArmorMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet) {
      result += GetDecorativeTerrainTileComponentMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet) {
      result += GetUpStaircaseTerrainTileComponentMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet) {
      result += GetDownStaircaseTerrainTileComponentMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsUnitMutSet) {
      result += GetUnitMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsTerrainTileByLocationMutMap) {
      result += GetTerrainTileByLocationMutMapHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    return result;
  }

  public void CheckForViolations() {
    List<string> violations = new List<string>();

    foreach (var obj in this.AllLevel()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllDecorativeTerrainTileComponent()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllUpStaircaseTerrainTileComponent()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllDownStaircaseTerrainTileComponent()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllTerrainTile()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllITerrainTileComponentMutBunch()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllTerrain()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllGlaive()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllArmor()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllRand()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllWanderAICapabilityUC()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllShieldingUC()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllBidingOperationUC()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllUnleashBideImpulse()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllStartBidingImpulse()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllBideAICapabilityUC()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllAttackImpulse()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllPursueImpulse()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllKillDirectiveUC()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllAttackAICapabilityUC()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllMoveImpulse()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllMoveDirectiveUC()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllUnit()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllIItemMutBunch()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllIUnitComponentMutBunch()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllNoImpulse()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllExecutionState()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllIPostActingUCWeakMutBunch()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllIPreActingUCWeakMutBunch()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllGame()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllIUnitEventMutList()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllLocationMutList()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllLevelMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllShieldingUCWeakMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllAttackAICapabilityUCWeakMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllKillDirectiveUCMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllMoveDirectiveUCMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllWanderAICapabilityUCMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllBideAICapabilityUCMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllAttackAICapabilityUCMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllShieldingUCMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllBidingOperationUCMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllGlaiveMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllArmorMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllDecorativeTerrainTileComponentMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllUpStaircaseTerrainTileComponentMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllDownStaircaseTerrainTileComponentMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllUnitMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllTerrainTileByLocationMutMap()) {
      obj.CheckForNullViolations(violations);
    }

    SortedSet<int> reachableIds = new SortedSet<int>();
    foreach (var rootStruct in this.AllGame()) {
      rootStruct.FindReachableObjects(reachableIds);
    }
    foreach (var obj in this.AllLevel()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllDecorativeTerrainTileComponent()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllUpStaircaseTerrainTileComponent()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllDownStaircaseTerrainTileComponent()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllTerrainTile()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllITerrainTileComponentMutBunch()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllTerrain()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllGlaive()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllArmor()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllRand()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllWanderAICapabilityUC()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllShieldingUC()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllBidingOperationUC()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllUnleashBideImpulse()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllStartBidingImpulse()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllBideAICapabilityUC()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllAttackImpulse()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllPursueImpulse()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllKillDirectiveUC()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllAttackAICapabilityUC()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllMoveImpulse()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllMoveDirectiveUC()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllUnit()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllIItemMutBunch()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllIUnitComponentMutBunch()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllNoImpulse()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllExecutionState()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllIPostActingUCWeakMutBunch()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllIPreActingUCWeakMutBunch()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllGame()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllIUnitEventMutList()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllLocationMutList()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllLevelMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllShieldingUCWeakMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllAttackAICapabilityUCWeakMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllKillDirectiveUCMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllMoveDirectiveUCMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllWanderAICapabilityUCMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllBideAICapabilityUCMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllAttackAICapabilityUCMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllShieldingUCMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllBidingOperationUCMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllGlaiveMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllArmorMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllDecorativeTerrainTileComponentMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllUpStaircaseTerrainTileComponentMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllDownStaircaseTerrainTileComponentMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllUnitMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllTerrainTileByLocationMutMap()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }

    if (violations.Count > 0) {
      string message = "Found violations!\n";
      foreach (var violation in violations) {
        message += violation + "\n";
      }
      throw new Exception(message);
    }
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


    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsLevel) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsLevel.ContainsKey(sourceObjId)) {
        EffectInternalCreateLevel(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDecorativeTerrainTileComponent) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsDecorativeTerrainTileComponent.ContainsKey(sourceObjId)) {
        EffectInternalCreateDecorativeTerrainTileComponent(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsUpStaircaseTerrainTileComponent) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsUpStaircaseTerrainTileComponent.ContainsKey(sourceObjId)) {
        EffectInternalCreateUpStaircaseTerrainTileComponent(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDownStaircaseTerrainTileComponent) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsDownStaircaseTerrainTileComponent.ContainsKey(sourceObjId)) {
        EffectInternalCreateDownStaircaseTerrainTileComponent(sourceObjId, sourceObjIncarnation);
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
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsITerrainTileComponentMutBunch) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsITerrainTileComponentMutBunch.ContainsKey(sourceObjId)) {
        EffectInternalCreateITerrainTileComponentMutBunch(sourceObjId, sourceObjIncarnation);
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
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsGlaive) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsGlaive.ContainsKey(sourceObjId)) {
        EffectInternalCreateGlaive(sourceObjId, sourceObjIncarnation);
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
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsRand) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsRand.ContainsKey(sourceObjId)) {
        EffectInternalCreateRand(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsWanderAICapabilityUC) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsWanderAICapabilityUC.ContainsKey(sourceObjId)) {
        EffectInternalCreateWanderAICapabilityUC(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsShieldingUC) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsShieldingUC.ContainsKey(sourceObjId)) {
        EffectInternalCreateShieldingUC(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsBidingOperationUC) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsBidingOperationUC.ContainsKey(sourceObjId)) {
        EffectInternalCreateBidingOperationUC(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsUnleashBideImpulse) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsUnleashBideImpulse.ContainsKey(sourceObjId)) {
        EffectInternalCreateUnleashBideImpulse(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsStartBidingImpulse) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsStartBidingImpulse.ContainsKey(sourceObjId)) {
        EffectInternalCreateStartBidingImpulse(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsBideAICapabilityUC) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsBideAICapabilityUC.ContainsKey(sourceObjId)) {
        EffectInternalCreateBideAICapabilityUC(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsAttackImpulse) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsAttackImpulse.ContainsKey(sourceObjId)) {
        EffectInternalCreateAttackImpulse(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsPursueImpulse) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsPursueImpulse.ContainsKey(sourceObjId)) {
        EffectInternalCreatePursueImpulse(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsKillDirectiveUC) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsKillDirectiveUC.ContainsKey(sourceObjId)) {
        EffectInternalCreateKillDirectiveUC(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsAttackAICapabilityUC) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsAttackAICapabilityUC.ContainsKey(sourceObjId)) {
        EffectInternalCreateAttackAICapabilityUC(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsMoveImpulse) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsMoveImpulse.ContainsKey(sourceObjId)) {
        EffectInternalCreateMoveImpulse(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsMoveDirectiveUC) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsMoveDirectiveUC.ContainsKey(sourceObjId)) {
        EffectInternalCreateMoveDirectiveUC(sourceObjId, sourceObjIncarnation);
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
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIItemMutBunch) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsIItemMutBunch.ContainsKey(sourceObjId)) {
        EffectInternalCreateIItemMutBunch(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIUnitComponentMutBunch) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsIUnitComponentMutBunch.ContainsKey(sourceObjId)) {
        EffectInternalCreateIUnitComponentMutBunch(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsNoImpulse) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsNoImpulse.ContainsKey(sourceObjId)) {
        EffectInternalCreateNoImpulse(sourceObjId, sourceObjIncarnation);
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
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIPostActingUCWeakMutBunch) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsIPostActingUCWeakMutBunch.ContainsKey(sourceObjId)) {
        EffectInternalCreateIPostActingUCWeakMutBunch(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIPreActingUCWeakMutBunch) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsIPreActingUCWeakMutBunch.ContainsKey(sourceObjId)) {
        EffectInternalCreateIPreActingUCWeakMutBunch(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsGame) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsGame.ContainsKey(sourceObjId)) {
        EffectInternalCreateGame(sourceObjId, sourceObjIncarnation);
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
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsLocationMutList) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsLocationMutList.ContainsKey(sourceObjId)) {
        EffectInternalCreateLocationMutList(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsLevelMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsLevelMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateLevelMutSet(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsShieldingUCWeakMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsShieldingUCWeakMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateShieldingUCWeakMutSet(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsAttackAICapabilityUCWeakMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateAttackAICapabilityUCWeakMutSet(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsKillDirectiveUCMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsKillDirectiveUCMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateKillDirectiveUCMutSet(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsMoveDirectiveUCMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsMoveDirectiveUCMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateMoveDirectiveUCMutSet(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsWanderAICapabilityUCMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsWanderAICapabilityUCMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateWanderAICapabilityUCMutSet(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsBideAICapabilityUCMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsBideAICapabilityUCMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateBideAICapabilityUCMutSet(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsAttackAICapabilityUCMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsAttackAICapabilityUCMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateAttackAICapabilityUCMutSet(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsShieldingUCMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsShieldingUCMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateShieldingUCMutSet(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsBidingOperationUCMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsBidingOperationUCMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateBidingOperationUCMutSet(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsGlaiveMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsGlaiveMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateGlaiveMutSet(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsArmorMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsArmorMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateArmorMutSet(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDecorativeTerrainTileComponentMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateDecorativeTerrainTileComponentMutSet(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateUpStaircaseTerrainTileComponentMutSet(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateDownStaircaseTerrainTileComponentMutSet(sourceObjId, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsUnitMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsUnitMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateUnitMutSet(sourceObjId, sourceObjIncarnation);
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
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsLevelMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsLevelMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsLevelMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in currentObjIncarnation.set) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectLevelMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectLevelMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.hash -=
                GetLevelMutSetHash(
                    objId,
                    rootIncarnation.incarnationsLevelMutSet[objId].version,
                    rootIncarnation.incarnationsLevelMutSet[objId].incarnation);
            rootIncarnation.incarnationsLevelMutSet[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetLevelMutSetHash(
                    objId,
                    rootIncarnation.incarnationsLevelMutSet[objId].version,
                    rootIncarnation.incarnationsLevelMutSet[objId].incarnation);
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsShieldingUCWeakMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsShieldingUCWeakMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsShieldingUCWeakMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in currentObjIncarnation.set) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectShieldingUCWeakMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectShieldingUCWeakMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.hash -=
                GetShieldingUCWeakMutSetHash(
                    objId,
                    rootIncarnation.incarnationsShieldingUCWeakMutSet[objId].version,
                    rootIncarnation.incarnationsShieldingUCWeakMutSet[objId].incarnation);
            rootIncarnation.incarnationsShieldingUCWeakMutSet[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetShieldingUCWeakMutSetHash(
                    objId,
                    rootIncarnation.incarnationsShieldingUCWeakMutSet[objId].version,
                    rootIncarnation.incarnationsShieldingUCWeakMutSet[objId].incarnation);
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsAttackAICapabilityUCWeakMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in currentObjIncarnation.set) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectAttackAICapabilityUCWeakMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectAttackAICapabilityUCWeakMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.hash -=
                GetAttackAICapabilityUCWeakMutSetHash(
                    objId,
                    rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet[objId].version,
                    rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet[objId].incarnation);
            rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetAttackAICapabilityUCWeakMutSetHash(
                    objId,
                    rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet[objId].version,
                    rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet[objId].incarnation);
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsKillDirectiveUCMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsKillDirectiveUCMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsKillDirectiveUCMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in currentObjIncarnation.set) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectKillDirectiveUCMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectKillDirectiveUCMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.hash -=
                GetKillDirectiveUCMutSetHash(
                    objId,
                    rootIncarnation.incarnationsKillDirectiveUCMutSet[objId].version,
                    rootIncarnation.incarnationsKillDirectiveUCMutSet[objId].incarnation);
            rootIncarnation.incarnationsKillDirectiveUCMutSet[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetKillDirectiveUCMutSetHash(
                    objId,
                    rootIncarnation.incarnationsKillDirectiveUCMutSet[objId].version,
                    rootIncarnation.incarnationsKillDirectiveUCMutSet[objId].incarnation);
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsMoveDirectiveUCMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsMoveDirectiveUCMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsMoveDirectiveUCMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in currentObjIncarnation.set) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectMoveDirectiveUCMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectMoveDirectiveUCMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.hash -=
                GetMoveDirectiveUCMutSetHash(
                    objId,
                    rootIncarnation.incarnationsMoveDirectiveUCMutSet[objId].version,
                    rootIncarnation.incarnationsMoveDirectiveUCMutSet[objId].incarnation);
            rootIncarnation.incarnationsMoveDirectiveUCMutSet[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetMoveDirectiveUCMutSetHash(
                    objId,
                    rootIncarnation.incarnationsMoveDirectiveUCMutSet[objId].version,
                    rootIncarnation.incarnationsMoveDirectiveUCMutSet[objId].incarnation);
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsWanderAICapabilityUCMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsWanderAICapabilityUCMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsWanderAICapabilityUCMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in currentObjIncarnation.set) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectWanderAICapabilityUCMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectWanderAICapabilityUCMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.hash -=
                GetWanderAICapabilityUCMutSetHash(
                    objId,
                    rootIncarnation.incarnationsWanderAICapabilityUCMutSet[objId].version,
                    rootIncarnation.incarnationsWanderAICapabilityUCMutSet[objId].incarnation);
            rootIncarnation.incarnationsWanderAICapabilityUCMutSet[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetWanderAICapabilityUCMutSetHash(
                    objId,
                    rootIncarnation.incarnationsWanderAICapabilityUCMutSet[objId].version,
                    rootIncarnation.incarnationsWanderAICapabilityUCMutSet[objId].incarnation);
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsBideAICapabilityUCMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsBideAICapabilityUCMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsBideAICapabilityUCMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in currentObjIncarnation.set) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectBideAICapabilityUCMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectBideAICapabilityUCMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.hash -=
                GetBideAICapabilityUCMutSetHash(
                    objId,
                    rootIncarnation.incarnationsBideAICapabilityUCMutSet[objId].version,
                    rootIncarnation.incarnationsBideAICapabilityUCMutSet[objId].incarnation);
            rootIncarnation.incarnationsBideAICapabilityUCMutSet[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetBideAICapabilityUCMutSetHash(
                    objId,
                    rootIncarnation.incarnationsBideAICapabilityUCMutSet[objId].version,
                    rootIncarnation.incarnationsBideAICapabilityUCMutSet[objId].incarnation);
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsAttackAICapabilityUCMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsAttackAICapabilityUCMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsAttackAICapabilityUCMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in currentObjIncarnation.set) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectAttackAICapabilityUCMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectAttackAICapabilityUCMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.hash -=
                GetAttackAICapabilityUCMutSetHash(
                    objId,
                    rootIncarnation.incarnationsAttackAICapabilityUCMutSet[objId].version,
                    rootIncarnation.incarnationsAttackAICapabilityUCMutSet[objId].incarnation);
            rootIncarnation.incarnationsAttackAICapabilityUCMutSet[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetAttackAICapabilityUCMutSetHash(
                    objId,
                    rootIncarnation.incarnationsAttackAICapabilityUCMutSet[objId].version,
                    rootIncarnation.incarnationsAttackAICapabilityUCMutSet[objId].incarnation);
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsShieldingUCMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsShieldingUCMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsShieldingUCMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in currentObjIncarnation.set) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectShieldingUCMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectShieldingUCMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.hash -=
                GetShieldingUCMutSetHash(
                    objId,
                    rootIncarnation.incarnationsShieldingUCMutSet[objId].version,
                    rootIncarnation.incarnationsShieldingUCMutSet[objId].incarnation);
            rootIncarnation.incarnationsShieldingUCMutSet[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetShieldingUCMutSetHash(
                    objId,
                    rootIncarnation.incarnationsShieldingUCMutSet[objId].version,
                    rootIncarnation.incarnationsShieldingUCMutSet[objId].incarnation);
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsBidingOperationUCMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsBidingOperationUCMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsBidingOperationUCMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in currentObjIncarnation.set) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectBidingOperationUCMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectBidingOperationUCMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.hash -=
                GetBidingOperationUCMutSetHash(
                    objId,
                    rootIncarnation.incarnationsBidingOperationUCMutSet[objId].version,
                    rootIncarnation.incarnationsBidingOperationUCMutSet[objId].incarnation);
            rootIncarnation.incarnationsBidingOperationUCMutSet[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetBidingOperationUCMutSetHash(
                    objId,
                    rootIncarnation.incarnationsBidingOperationUCMutSet[objId].version,
                    rootIncarnation.incarnationsBidingOperationUCMutSet[objId].incarnation);
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsGlaiveMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsGlaiveMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsGlaiveMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in currentObjIncarnation.set) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectGlaiveMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectGlaiveMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.hash -=
                GetGlaiveMutSetHash(
                    objId,
                    rootIncarnation.incarnationsGlaiveMutSet[objId].version,
                    rootIncarnation.incarnationsGlaiveMutSet[objId].incarnation);
            rootIncarnation.incarnationsGlaiveMutSet[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetGlaiveMutSetHash(
                    objId,
                    rootIncarnation.incarnationsGlaiveMutSet[objId].version,
                    rootIncarnation.incarnationsGlaiveMutSet[objId].incarnation);
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsArmorMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsArmorMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsArmorMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in currentObjIncarnation.set) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectArmorMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectArmorMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.hash -=
                GetArmorMutSetHash(
                    objId,
                    rootIncarnation.incarnationsArmorMutSet[objId].version,
                    rootIncarnation.incarnationsArmorMutSet[objId].incarnation);
            rootIncarnation.incarnationsArmorMutSet[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetArmorMutSetHash(
                    objId,
                    rootIncarnation.incarnationsArmorMutSet[objId].version,
                    rootIncarnation.incarnationsArmorMutSet[objId].incarnation);
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDecorativeTerrainTileComponentMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in currentObjIncarnation.set) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectDecorativeTerrainTileComponentMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectDecorativeTerrainTileComponentMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.hash -=
                GetDecorativeTerrainTileComponentMutSetHash(
                    objId,
                    rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet[objId].version,
                    rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet[objId].incarnation);
            rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetDecorativeTerrainTileComponentMutSetHash(
                    objId,
                    rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet[objId].version,
                    rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet[objId].incarnation);
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in currentObjIncarnation.set) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectUpStaircaseTerrainTileComponentMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectUpStaircaseTerrainTileComponentMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.hash -=
                GetUpStaircaseTerrainTileComponentMutSetHash(
                    objId,
                    rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet[objId].version,
                    rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet[objId].incarnation);
            rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetUpStaircaseTerrainTileComponentMutSetHash(
                    objId,
                    rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet[objId].version,
                    rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet[objId].incarnation);
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in currentObjIncarnation.set) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectDownStaircaseTerrainTileComponentMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectDownStaircaseTerrainTileComponentMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.hash -=
                GetDownStaircaseTerrainTileComponentMutSetHash(
                    objId,
                    rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet[objId].version,
                    rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet[objId].incarnation);
            rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetDownStaircaseTerrainTileComponentMutSetHash(
                    objId,
                    rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet[objId].version,
                    rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet[objId].incarnation);
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsUnitMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsUnitMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsUnitMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in currentObjIncarnation.set) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectUnitMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectUnitMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.hash -=
                GetUnitMutSetHash(
                    objId,
                    rootIncarnation.incarnationsUnitMutSet[objId].version,
                    rootIncarnation.incarnationsUnitMutSet[objId].incarnation);
            rootIncarnation.incarnationsUnitMutSet[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetUnitMutSetHash(
                    objId,
                    rootIncarnation.incarnationsUnitMutSet[objId].version,
                    rootIncarnation.incarnationsUnitMutSet[objId].incarnation);
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

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDecorativeTerrainTileComponent) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsDecorativeTerrainTileComponent.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsDecorativeTerrainTileComponent[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetDecorativeTerrainTileComponentHash(
                    objId,
                    rootIncarnation.incarnationsDecorativeTerrainTileComponent[objId].version,
                    rootIncarnation.incarnationsDecorativeTerrainTileComponent[objId].incarnation);
          rootIncarnation.incarnationsDecorativeTerrainTileComponent[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetDecorativeTerrainTileComponentHash(
                    objId,
                    rootIncarnation.incarnationsDecorativeTerrainTileComponent[objId].version,
                    rootIncarnation.incarnationsDecorativeTerrainTileComponent[objId].incarnation);
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsUpStaircaseTerrainTileComponent) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsUpStaircaseTerrainTileComponent.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsUpStaircaseTerrainTileComponent[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetUpStaircaseTerrainTileComponentHash(
                    objId,
                    rootIncarnation.incarnationsUpStaircaseTerrainTileComponent[objId].version,
                    rootIncarnation.incarnationsUpStaircaseTerrainTileComponent[objId].incarnation);
          rootIncarnation.incarnationsUpStaircaseTerrainTileComponent[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetUpStaircaseTerrainTileComponentHash(
                    objId,
                    rootIncarnation.incarnationsUpStaircaseTerrainTileComponent[objId].version,
                    rootIncarnation.incarnationsUpStaircaseTerrainTileComponent[objId].incarnation);
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDownStaircaseTerrainTileComponent) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsDownStaircaseTerrainTileComponent.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsDownStaircaseTerrainTileComponent[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetDownStaircaseTerrainTileComponentHash(
                    objId,
                    rootIncarnation.incarnationsDownStaircaseTerrainTileComponent[objId].version,
                    rootIncarnation.incarnationsDownStaircaseTerrainTileComponent[objId].incarnation);
          rootIncarnation.incarnationsDownStaircaseTerrainTileComponent[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetDownStaircaseTerrainTileComponentHash(
                    objId,
                    rootIncarnation.incarnationsDownStaircaseTerrainTileComponent[objId].version,
                    rootIncarnation.incarnationsDownStaircaseTerrainTileComponent[objId].incarnation);
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

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsITerrainTileComponentMutBunch) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsITerrainTileComponentMutBunch.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsITerrainTileComponentMutBunch[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetITerrainTileComponentMutBunchHash(
                    objId,
                    rootIncarnation.incarnationsITerrainTileComponentMutBunch[objId].version,
                    rootIncarnation.incarnationsITerrainTileComponentMutBunch[objId].incarnation);
          rootIncarnation.incarnationsITerrainTileComponentMutBunch[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetITerrainTileComponentMutBunchHash(
                    objId,
                    rootIncarnation.incarnationsITerrainTileComponentMutBunch[objId].version,
                    rootIncarnation.incarnationsITerrainTileComponentMutBunch[objId].incarnation);
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

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsWanderAICapabilityUC) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsWanderAICapabilityUC.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsWanderAICapabilityUC[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetWanderAICapabilityUCHash(
                    objId,
                    rootIncarnation.incarnationsWanderAICapabilityUC[objId].version,
                    rootIncarnation.incarnationsWanderAICapabilityUC[objId].incarnation);
          rootIncarnation.incarnationsWanderAICapabilityUC[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetWanderAICapabilityUCHash(
                    objId,
                    rootIncarnation.incarnationsWanderAICapabilityUC[objId].version,
                    rootIncarnation.incarnationsWanderAICapabilityUC[objId].incarnation);
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsShieldingUC) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsShieldingUC.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsShieldingUC[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetShieldingUCHash(
                    objId,
                    rootIncarnation.incarnationsShieldingUC[objId].version,
                    rootIncarnation.incarnationsShieldingUC[objId].incarnation);
          rootIncarnation.incarnationsShieldingUC[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetShieldingUCHash(
                    objId,
                    rootIncarnation.incarnationsShieldingUC[objId].version,
                    rootIncarnation.incarnationsShieldingUC[objId].incarnation);
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsBidingOperationUC) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsBidingOperationUC.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsBidingOperationUC[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetBidingOperationUCHash(
                    objId,
                    rootIncarnation.incarnationsBidingOperationUC[objId].version,
                    rootIncarnation.incarnationsBidingOperationUC[objId].incarnation);
          rootIncarnation.incarnationsBidingOperationUC[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetBidingOperationUCHash(
                    objId,
                    rootIncarnation.incarnationsBidingOperationUC[objId].version,
                    rootIncarnation.incarnationsBidingOperationUC[objId].incarnation);
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsUnleashBideImpulse) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsUnleashBideImpulse.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsUnleashBideImpulse[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetUnleashBideImpulseHash(
                    objId,
                    rootIncarnation.incarnationsUnleashBideImpulse[objId].version,
                    rootIncarnation.incarnationsUnleashBideImpulse[objId].incarnation);
          rootIncarnation.incarnationsUnleashBideImpulse[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetUnleashBideImpulseHash(
                    objId,
                    rootIncarnation.incarnationsUnleashBideImpulse[objId].version,
                    rootIncarnation.incarnationsUnleashBideImpulse[objId].incarnation);
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsStartBidingImpulse) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsStartBidingImpulse.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsStartBidingImpulse[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetStartBidingImpulseHash(
                    objId,
                    rootIncarnation.incarnationsStartBidingImpulse[objId].version,
                    rootIncarnation.incarnationsStartBidingImpulse[objId].incarnation);
          rootIncarnation.incarnationsStartBidingImpulse[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetStartBidingImpulseHash(
                    objId,
                    rootIncarnation.incarnationsStartBidingImpulse[objId].version,
                    rootIncarnation.incarnationsStartBidingImpulse[objId].incarnation);
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsBideAICapabilityUC) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsBideAICapabilityUC.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsBideAICapabilityUC[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetBideAICapabilityUCHash(
                    objId,
                    rootIncarnation.incarnationsBideAICapabilityUC[objId].version,
                    rootIncarnation.incarnationsBideAICapabilityUC[objId].incarnation);
          rootIncarnation.incarnationsBideAICapabilityUC[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetBideAICapabilityUCHash(
                    objId,
                    rootIncarnation.incarnationsBideAICapabilityUC[objId].version,
                    rootIncarnation.incarnationsBideAICapabilityUC[objId].incarnation);
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsAttackImpulse) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsAttackImpulse.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsAttackImpulse[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetAttackImpulseHash(
                    objId,
                    rootIncarnation.incarnationsAttackImpulse[objId].version,
                    rootIncarnation.incarnationsAttackImpulse[objId].incarnation);
          rootIncarnation.incarnationsAttackImpulse[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetAttackImpulseHash(
                    objId,
                    rootIncarnation.incarnationsAttackImpulse[objId].version,
                    rootIncarnation.incarnationsAttackImpulse[objId].incarnation);
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsPursueImpulse) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsPursueImpulse.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsPursueImpulse[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetPursueImpulseHash(
                    objId,
                    rootIncarnation.incarnationsPursueImpulse[objId].version,
                    rootIncarnation.incarnationsPursueImpulse[objId].incarnation);
          rootIncarnation.incarnationsPursueImpulse[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetPursueImpulseHash(
                    objId,
                    rootIncarnation.incarnationsPursueImpulse[objId].version,
                    rootIncarnation.incarnationsPursueImpulse[objId].incarnation);
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsKillDirectiveUC) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsKillDirectiveUC.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsKillDirectiveUC[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetKillDirectiveUCHash(
                    objId,
                    rootIncarnation.incarnationsKillDirectiveUC[objId].version,
                    rootIncarnation.incarnationsKillDirectiveUC[objId].incarnation);
          rootIncarnation.incarnationsKillDirectiveUC[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetKillDirectiveUCHash(
                    objId,
                    rootIncarnation.incarnationsKillDirectiveUC[objId].version,
                    rootIncarnation.incarnationsKillDirectiveUC[objId].incarnation);
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsAttackAICapabilityUC) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsAttackAICapabilityUC.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsAttackAICapabilityUC[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetAttackAICapabilityUCHash(
                    objId,
                    rootIncarnation.incarnationsAttackAICapabilityUC[objId].version,
                    rootIncarnation.incarnationsAttackAICapabilityUC[objId].incarnation);
          rootIncarnation.incarnationsAttackAICapabilityUC[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetAttackAICapabilityUCHash(
                    objId,
                    rootIncarnation.incarnationsAttackAICapabilityUC[objId].version,
                    rootIncarnation.incarnationsAttackAICapabilityUC[objId].incarnation);
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsMoveImpulse) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsMoveImpulse.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsMoveImpulse[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetMoveImpulseHash(
                    objId,
                    rootIncarnation.incarnationsMoveImpulse[objId].version,
                    rootIncarnation.incarnationsMoveImpulse[objId].incarnation);
          rootIncarnation.incarnationsMoveImpulse[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetMoveImpulseHash(
                    objId,
                    rootIncarnation.incarnationsMoveImpulse[objId].version,
                    rootIncarnation.incarnationsMoveImpulse[objId].incarnation);
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsMoveDirectiveUC) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsMoveDirectiveUC.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsMoveDirectiveUC[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetMoveDirectiveUCHash(
                    objId,
                    rootIncarnation.incarnationsMoveDirectiveUC[objId].version,
                    rootIncarnation.incarnationsMoveDirectiveUC[objId].incarnation);
          rootIncarnation.incarnationsMoveDirectiveUC[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetMoveDirectiveUCHash(
                    objId,
                    rootIncarnation.incarnationsMoveDirectiveUC[objId].version,
                    rootIncarnation.incarnationsMoveDirectiveUC[objId].incarnation);
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

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIUnitComponentMutBunch) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsIUnitComponentMutBunch.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsIUnitComponentMutBunch[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetIUnitComponentMutBunchHash(
                    objId,
                    rootIncarnation.incarnationsIUnitComponentMutBunch[objId].version,
                    rootIncarnation.incarnationsIUnitComponentMutBunch[objId].incarnation);
          rootIncarnation.incarnationsIUnitComponentMutBunch[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetIUnitComponentMutBunchHash(
                    objId,
                    rootIncarnation.incarnationsIUnitComponentMutBunch[objId].version,
                    rootIncarnation.incarnationsIUnitComponentMutBunch[objId].incarnation);
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsNoImpulse) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsNoImpulse.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsNoImpulse[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetNoImpulseHash(
                    objId,
                    rootIncarnation.incarnationsNoImpulse[objId].version,
                    rootIncarnation.incarnationsNoImpulse[objId].incarnation);
          rootIncarnation.incarnationsNoImpulse[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetNoImpulseHash(
                    objId,
                    rootIncarnation.incarnationsNoImpulse[objId].version,
                    rootIncarnation.incarnationsNoImpulse[objId].incarnation);
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

          if (sourceObjIncarnation.remainingPreActingUnitComponents != currentObjIncarnation.remainingPreActingUnitComponents) {
            EffectExecutionStateSetRemainingPreActingUnitComponents(objId, new IPreActingUCWeakMutBunch(this, sourceObjIncarnation.remainingPreActingUnitComponents));
          }

          if (sourceObjIncarnation.remainingPostActingUnitComponents != currentObjIncarnation.remainingPostActingUnitComponents) {
            EffectExecutionStateSetRemainingPostActingUnitComponents(objId, new IPostActingUCWeakMutBunch(this, sourceObjIncarnation.remainingPostActingUnitComponents));
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

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIPostActingUCWeakMutBunch) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsIPostActingUCWeakMutBunch.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsIPostActingUCWeakMutBunch[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetIPostActingUCWeakMutBunchHash(
                    objId,
                    rootIncarnation.incarnationsIPostActingUCWeakMutBunch[objId].version,
                    rootIncarnation.incarnationsIPostActingUCWeakMutBunch[objId].incarnation);
          rootIncarnation.incarnationsIPostActingUCWeakMutBunch[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetIPostActingUCWeakMutBunchHash(
                    objId,
                    rootIncarnation.incarnationsIPostActingUCWeakMutBunch[objId].version,
                    rootIncarnation.incarnationsIPostActingUCWeakMutBunch[objId].incarnation);
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIPreActingUCWeakMutBunch) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsIPreActingUCWeakMutBunch.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsIPreActingUCWeakMutBunch[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetIPreActingUCWeakMutBunchHash(
                    objId,
                    rootIncarnation.incarnationsIPreActingUCWeakMutBunch[objId].version,
                    rootIncarnation.incarnationsIPreActingUCWeakMutBunch[objId].incarnation);
          rootIncarnation.incarnationsIPreActingUCWeakMutBunch[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetIPreActingUCWeakMutBunchHash(
                    objId,
                    rootIncarnation.incarnationsIPreActingUCWeakMutBunch[objId].version,
                    rootIncarnation.incarnationsIPreActingUCWeakMutBunch[objId].incarnation);
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

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<LevelIncarnation>>(rootIncarnation.incarnationsLevel)) {
      if (!sourceIncarnation.incarnationsLevel.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectLevelDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<DecorativeTerrainTileComponentIncarnation>>(rootIncarnation.incarnationsDecorativeTerrainTileComponent)) {
      if (!sourceIncarnation.incarnationsDecorativeTerrainTileComponent.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectDecorativeTerrainTileComponentDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<UpStaircaseTerrainTileComponentIncarnation>>(rootIncarnation.incarnationsUpStaircaseTerrainTileComponent)) {
      if (!sourceIncarnation.incarnationsUpStaircaseTerrainTileComponent.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectUpStaircaseTerrainTileComponentDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<DownStaircaseTerrainTileComponentIncarnation>>(rootIncarnation.incarnationsDownStaircaseTerrainTileComponent)) {
      if (!sourceIncarnation.incarnationsDownStaircaseTerrainTileComponent.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectDownStaircaseTerrainTileComponentDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<TerrainTileIncarnation>>(rootIncarnation.incarnationsTerrainTile)) {
      if (!sourceIncarnation.incarnationsTerrainTile.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectTerrainTileDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<ITerrainTileComponentMutBunchIncarnation>>(rootIncarnation.incarnationsITerrainTileComponentMutBunch)) {
      if (!sourceIncarnation.incarnationsITerrainTileComponentMutBunch.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectITerrainTileComponentMutBunchDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<TerrainIncarnation>>(rootIncarnation.incarnationsTerrain)) {
      if (!sourceIncarnation.incarnationsTerrain.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectTerrainDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<GlaiveIncarnation>>(rootIncarnation.incarnationsGlaive)) {
      if (!sourceIncarnation.incarnationsGlaive.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectGlaiveDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<ArmorIncarnation>>(rootIncarnation.incarnationsArmor)) {
      if (!sourceIncarnation.incarnationsArmor.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectArmorDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<RandIncarnation>>(rootIncarnation.incarnationsRand)) {
      if (!sourceIncarnation.incarnationsRand.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectRandDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<WanderAICapabilityUCIncarnation>>(rootIncarnation.incarnationsWanderAICapabilityUC)) {
      if (!sourceIncarnation.incarnationsWanderAICapabilityUC.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectWanderAICapabilityUCDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<ShieldingUCIncarnation>>(rootIncarnation.incarnationsShieldingUC)) {
      if (!sourceIncarnation.incarnationsShieldingUC.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectShieldingUCDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<BidingOperationUCIncarnation>>(rootIncarnation.incarnationsBidingOperationUC)) {
      if (!sourceIncarnation.incarnationsBidingOperationUC.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectBidingOperationUCDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<UnleashBideImpulseIncarnation>>(rootIncarnation.incarnationsUnleashBideImpulse)) {
      if (!sourceIncarnation.incarnationsUnleashBideImpulse.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectUnleashBideImpulseDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<StartBidingImpulseIncarnation>>(rootIncarnation.incarnationsStartBidingImpulse)) {
      if (!sourceIncarnation.incarnationsStartBidingImpulse.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectStartBidingImpulseDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<BideAICapabilityUCIncarnation>>(rootIncarnation.incarnationsBideAICapabilityUC)) {
      if (!sourceIncarnation.incarnationsBideAICapabilityUC.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectBideAICapabilityUCDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<AttackImpulseIncarnation>>(rootIncarnation.incarnationsAttackImpulse)) {
      if (!sourceIncarnation.incarnationsAttackImpulse.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectAttackImpulseDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<PursueImpulseIncarnation>>(rootIncarnation.incarnationsPursueImpulse)) {
      if (!sourceIncarnation.incarnationsPursueImpulse.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectPursueImpulseDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<KillDirectiveUCIncarnation>>(rootIncarnation.incarnationsKillDirectiveUC)) {
      if (!sourceIncarnation.incarnationsKillDirectiveUC.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectKillDirectiveUCDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<AttackAICapabilityUCIncarnation>>(rootIncarnation.incarnationsAttackAICapabilityUC)) {
      if (!sourceIncarnation.incarnationsAttackAICapabilityUC.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectAttackAICapabilityUCDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<MoveImpulseIncarnation>>(rootIncarnation.incarnationsMoveImpulse)) {
      if (!sourceIncarnation.incarnationsMoveImpulse.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectMoveImpulseDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<MoveDirectiveUCIncarnation>>(rootIncarnation.incarnationsMoveDirectiveUC)) {
      if (!sourceIncarnation.incarnationsMoveDirectiveUC.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectMoveDirectiveUCDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<UnitIncarnation>>(rootIncarnation.incarnationsUnit)) {
      if (!sourceIncarnation.incarnationsUnit.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectUnitDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<IItemMutBunchIncarnation>>(rootIncarnation.incarnationsIItemMutBunch)) {
      if (!sourceIncarnation.incarnationsIItemMutBunch.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectIItemMutBunchDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<IUnitComponentMutBunchIncarnation>>(rootIncarnation.incarnationsIUnitComponentMutBunch)) {
      if (!sourceIncarnation.incarnationsIUnitComponentMutBunch.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectIUnitComponentMutBunchDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<NoImpulseIncarnation>>(rootIncarnation.incarnationsNoImpulse)) {
      if (!sourceIncarnation.incarnationsNoImpulse.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectNoImpulseDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<ExecutionStateIncarnation>>(rootIncarnation.incarnationsExecutionState)) {
      if (!sourceIncarnation.incarnationsExecutionState.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectExecutionStateDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<IPostActingUCWeakMutBunchIncarnation>>(rootIncarnation.incarnationsIPostActingUCWeakMutBunch)) {
      if (!sourceIncarnation.incarnationsIPostActingUCWeakMutBunch.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectIPostActingUCWeakMutBunchDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<IPreActingUCWeakMutBunchIncarnation>>(rootIncarnation.incarnationsIPreActingUCWeakMutBunch)) {
      if (!sourceIncarnation.incarnationsIPreActingUCWeakMutBunch.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectIPreActingUCWeakMutBunchDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<GameIncarnation>>(rootIncarnation.incarnationsGame)) {
      if (!sourceIncarnation.incarnationsGame.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectGameDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<IUnitEventMutListIncarnation>>(rootIncarnation.incarnationsIUnitEventMutList)) {
      if (!sourceIncarnation.incarnationsIUnitEventMutList.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectIUnitEventMutListDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<LocationMutListIncarnation>>(rootIncarnation.incarnationsLocationMutList)) {
      if (!sourceIncarnation.incarnationsLocationMutList.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectLocationMutListDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<LevelMutSetIncarnation>>(rootIncarnation.incarnationsLevelMutSet)) {
      if (!sourceIncarnation.incarnationsLevelMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectLevelMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<ShieldingUCWeakMutSetIncarnation>>(rootIncarnation.incarnationsShieldingUCWeakMutSet)) {
      if (!sourceIncarnation.incarnationsShieldingUCWeakMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectShieldingUCWeakMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<AttackAICapabilityUCWeakMutSetIncarnation>>(rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet)) {
      if (!sourceIncarnation.incarnationsAttackAICapabilityUCWeakMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectAttackAICapabilityUCWeakMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<KillDirectiveUCMutSetIncarnation>>(rootIncarnation.incarnationsKillDirectiveUCMutSet)) {
      if (!sourceIncarnation.incarnationsKillDirectiveUCMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectKillDirectiveUCMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<MoveDirectiveUCMutSetIncarnation>>(rootIncarnation.incarnationsMoveDirectiveUCMutSet)) {
      if (!sourceIncarnation.incarnationsMoveDirectiveUCMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectMoveDirectiveUCMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<WanderAICapabilityUCMutSetIncarnation>>(rootIncarnation.incarnationsWanderAICapabilityUCMutSet)) {
      if (!sourceIncarnation.incarnationsWanderAICapabilityUCMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectWanderAICapabilityUCMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<BideAICapabilityUCMutSetIncarnation>>(rootIncarnation.incarnationsBideAICapabilityUCMutSet)) {
      if (!sourceIncarnation.incarnationsBideAICapabilityUCMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectBideAICapabilityUCMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<AttackAICapabilityUCMutSetIncarnation>>(rootIncarnation.incarnationsAttackAICapabilityUCMutSet)) {
      if (!sourceIncarnation.incarnationsAttackAICapabilityUCMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectAttackAICapabilityUCMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<ShieldingUCMutSetIncarnation>>(rootIncarnation.incarnationsShieldingUCMutSet)) {
      if (!sourceIncarnation.incarnationsShieldingUCMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectShieldingUCMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<BidingOperationUCMutSetIncarnation>>(rootIncarnation.incarnationsBidingOperationUCMutSet)) {
      if (!sourceIncarnation.incarnationsBidingOperationUCMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectBidingOperationUCMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<GlaiveMutSetIncarnation>>(rootIncarnation.incarnationsGlaiveMutSet)) {
      if (!sourceIncarnation.incarnationsGlaiveMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectGlaiveMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<ArmorMutSetIncarnation>>(rootIncarnation.incarnationsArmorMutSet)) {
      if (!sourceIncarnation.incarnationsArmorMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectArmorMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<DecorativeTerrainTileComponentMutSetIncarnation>>(rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet)) {
      if (!sourceIncarnation.incarnationsDecorativeTerrainTileComponentMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectDecorativeTerrainTileComponentMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<UpStaircaseTerrainTileComponentMutSetIncarnation>>(rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet)) {
      if (!sourceIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectUpStaircaseTerrainTileComponentMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<DownStaircaseTerrainTileComponentMutSetIncarnation>>(rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet)) {
      if (!sourceIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectDownStaircaseTerrainTileComponentMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<UnitMutSetIncarnation>>(rootIncarnation.incarnationsUnitMutSet)) {
      if (!sourceIncarnation.incarnationsUnitMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectUnitMutSetDelete(id);
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
    foreach (var id in rootIncarnation.incarnationsLevel.Keys) {
      result.Add(new Level(this, id));
    }
    return result;
  }
  public IEnumerator<Level> EnumAllLevel() {
    foreach (var id in rootIncarnation.incarnationsLevel.Keys) {
      yield return GetLevel(id);
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
      UnitMutSet units) {
    CheckUnlocked();
    CheckHasTerrain(terrain);
    CheckHasUnitMutSet(units);

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
       public DecorativeTerrainTileComponentIncarnation GetDecorativeTerrainTileComponentIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsDecorativeTerrainTileComponent[id].incarnation;
  }
  public bool DecorativeTerrainTileComponentExists(int id) {
    return rootIncarnation.incarnationsDecorativeTerrainTileComponent.ContainsKey(id);
  }
  public DecorativeTerrainTileComponent GetDecorativeTerrainTileComponent(int id) {
    return new DecorativeTerrainTileComponent(this, id);
  }
  public List<DecorativeTerrainTileComponent> AllDecorativeTerrainTileComponent() {
    List<DecorativeTerrainTileComponent> result = new List<DecorativeTerrainTileComponent>(rootIncarnation.incarnationsDecorativeTerrainTileComponent.Count);
    foreach (var id in rootIncarnation.incarnationsDecorativeTerrainTileComponent.Keys) {
      result.Add(new DecorativeTerrainTileComponent(this, id));
    }
    return result;
  }
  public IEnumerator<DecorativeTerrainTileComponent> EnumAllDecorativeTerrainTileComponent() {
    foreach (var id in rootIncarnation.incarnationsDecorativeTerrainTileComponent.Keys) {
      yield return GetDecorativeTerrainTileComponent(id);
    }
  }
  public void CheckHasDecorativeTerrainTileComponent(DecorativeTerrainTileComponent thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasDecorativeTerrainTileComponent(thing.id);
  }
  public void CheckHasDecorativeTerrainTileComponent(int id) {
    if (!rootIncarnation.incarnationsDecorativeTerrainTileComponent.ContainsKey(id)) {
      throw new System.Exception("Invalid DecorativeTerrainTileComponent!");
    }
  }
  public void AddDecorativeTerrainTileComponentObserver(int id, IDecorativeTerrainTileComponentEffectObserver observer) {
    List<IDecorativeTerrainTileComponentEffectObserver> obsies;
    if (!observersDecorativeTerrainTileComponent.TryGetValue(id, out obsies)) {
      obsies = new List<IDecorativeTerrainTileComponentEffectObserver>();
    }
    obsies.Add(observer);
    observersDecorativeTerrainTileComponent[id] = obsies;
  }

  public void RemoveDecorativeTerrainTileComponentObserver(int id, IDecorativeTerrainTileComponentEffectObserver observer) {
    if (observersDecorativeTerrainTileComponent.ContainsKey(id)) {
      var list = observersDecorativeTerrainTileComponent[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersDecorativeTerrainTileComponent.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastDecorativeTerrainTileComponentEffect(int id, IDecorativeTerrainTileComponentEffect effect) {
    if (observersDecorativeTerrainTileComponent.ContainsKey(0)) {
      foreach (var observer in new List<IDecorativeTerrainTileComponentEffectObserver>(observersDecorativeTerrainTileComponent[0])) {
        observer.OnDecorativeTerrainTileComponentEffect(effect);
      }
    }
    if (observersDecorativeTerrainTileComponent.ContainsKey(id)) {
      foreach (var observer in new List<IDecorativeTerrainTileComponentEffectObserver>(observersDecorativeTerrainTileComponent[id])) {
        observer.OnDecorativeTerrainTileComponentEffect(effect);
      }
    }
  }

  public DecorativeTerrainTileComponent EffectDecorativeTerrainTileComponentCreate(
      string symbolId) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new DecorativeTerrainTileComponentIncarnation(
            symbolId
            );
    EffectInternalCreateDecorativeTerrainTileComponent(id, incarnation);
    return new DecorativeTerrainTileComponent(this, id);
  }
  public void EffectInternalCreateDecorativeTerrainTileComponent(
      int id,
      DecorativeTerrainTileComponentIncarnation incarnation) {
    CheckUnlocked();
    var effect = new DecorativeTerrainTileComponentCreateEffect(id, incarnation);
    rootIncarnation.incarnationsDecorativeTerrainTileComponent.Add(
        id,
        new VersionAndIncarnation<DecorativeTerrainTileComponentIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetDecorativeTerrainTileComponentHash(id, rootIncarnation.version, incarnation);
    BroadcastDecorativeTerrainTileComponentEffect(id, effect);
  }

  public void EffectDecorativeTerrainTileComponentDelete(int id) {
    CheckUnlocked();
    var effect = new DecorativeTerrainTileComponentDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsDecorativeTerrainTileComponent[id];
    this.rootIncarnation.hash -=
        GetDecorativeTerrainTileComponentHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastDecorativeTerrainTileComponentEffect(id, effect);
    rootIncarnation.incarnationsDecorativeTerrainTileComponent.Remove(id);
  }

     
  public int GetDecorativeTerrainTileComponentHash(int id, int version, DecorativeTerrainTileComponentIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.symbolId.GetDeterministicHashCode();
    return result;
  }
       public UpStaircaseTerrainTileComponentIncarnation GetUpStaircaseTerrainTileComponentIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsUpStaircaseTerrainTileComponent[id].incarnation;
  }
  public bool UpStaircaseTerrainTileComponentExists(int id) {
    return rootIncarnation.incarnationsUpStaircaseTerrainTileComponent.ContainsKey(id);
  }
  public UpStaircaseTerrainTileComponent GetUpStaircaseTerrainTileComponent(int id) {
    return new UpStaircaseTerrainTileComponent(this, id);
  }
  public List<UpStaircaseTerrainTileComponent> AllUpStaircaseTerrainTileComponent() {
    List<UpStaircaseTerrainTileComponent> result = new List<UpStaircaseTerrainTileComponent>(rootIncarnation.incarnationsUpStaircaseTerrainTileComponent.Count);
    foreach (var id in rootIncarnation.incarnationsUpStaircaseTerrainTileComponent.Keys) {
      result.Add(new UpStaircaseTerrainTileComponent(this, id));
    }
    return result;
  }
  public IEnumerator<UpStaircaseTerrainTileComponent> EnumAllUpStaircaseTerrainTileComponent() {
    foreach (var id in rootIncarnation.incarnationsUpStaircaseTerrainTileComponent.Keys) {
      yield return GetUpStaircaseTerrainTileComponent(id);
    }
  }
  public void CheckHasUpStaircaseTerrainTileComponent(UpStaircaseTerrainTileComponent thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasUpStaircaseTerrainTileComponent(thing.id);
  }
  public void CheckHasUpStaircaseTerrainTileComponent(int id) {
    if (!rootIncarnation.incarnationsUpStaircaseTerrainTileComponent.ContainsKey(id)) {
      throw new System.Exception("Invalid UpStaircaseTerrainTileComponent!");
    }
  }
  public void AddUpStaircaseTerrainTileComponentObserver(int id, IUpStaircaseTerrainTileComponentEffectObserver observer) {
    List<IUpStaircaseTerrainTileComponentEffectObserver> obsies;
    if (!observersUpStaircaseTerrainTileComponent.TryGetValue(id, out obsies)) {
      obsies = new List<IUpStaircaseTerrainTileComponentEffectObserver>();
    }
    obsies.Add(observer);
    observersUpStaircaseTerrainTileComponent[id] = obsies;
  }

  public void RemoveUpStaircaseTerrainTileComponentObserver(int id, IUpStaircaseTerrainTileComponentEffectObserver observer) {
    if (observersUpStaircaseTerrainTileComponent.ContainsKey(id)) {
      var list = observersUpStaircaseTerrainTileComponent[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersUpStaircaseTerrainTileComponent.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastUpStaircaseTerrainTileComponentEffect(int id, IUpStaircaseTerrainTileComponentEffect effect) {
    if (observersUpStaircaseTerrainTileComponent.ContainsKey(0)) {
      foreach (var observer in new List<IUpStaircaseTerrainTileComponentEffectObserver>(observersUpStaircaseTerrainTileComponent[0])) {
        observer.OnUpStaircaseTerrainTileComponentEffect(effect);
      }
    }
    if (observersUpStaircaseTerrainTileComponent.ContainsKey(id)) {
      foreach (var observer in new List<IUpStaircaseTerrainTileComponentEffectObserver>(observersUpStaircaseTerrainTileComponent[id])) {
        observer.OnUpStaircaseTerrainTileComponentEffect(effect);
      }
    }
  }

  public UpStaircaseTerrainTileComponent EffectUpStaircaseTerrainTileComponentCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new UpStaircaseTerrainTileComponentIncarnation(

            );
    EffectInternalCreateUpStaircaseTerrainTileComponent(id, incarnation);
    return new UpStaircaseTerrainTileComponent(this, id);
  }
  public void EffectInternalCreateUpStaircaseTerrainTileComponent(
      int id,
      UpStaircaseTerrainTileComponentIncarnation incarnation) {
    CheckUnlocked();
    var effect = new UpStaircaseTerrainTileComponentCreateEffect(id, incarnation);
    rootIncarnation.incarnationsUpStaircaseTerrainTileComponent.Add(
        id,
        new VersionAndIncarnation<UpStaircaseTerrainTileComponentIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetUpStaircaseTerrainTileComponentHash(id, rootIncarnation.version, incarnation);
    BroadcastUpStaircaseTerrainTileComponentEffect(id, effect);
  }

  public void EffectUpStaircaseTerrainTileComponentDelete(int id) {
    CheckUnlocked();
    var effect = new UpStaircaseTerrainTileComponentDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsUpStaircaseTerrainTileComponent[id];
    this.rootIncarnation.hash -=
        GetUpStaircaseTerrainTileComponentHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastUpStaircaseTerrainTileComponentEffect(id, effect);
    rootIncarnation.incarnationsUpStaircaseTerrainTileComponent.Remove(id);
  }

     
  public int GetUpStaircaseTerrainTileComponentHash(int id, int version, UpStaircaseTerrainTileComponentIncarnation incarnation) {
    int result = id * version;
    return result;
  }
       public DownStaircaseTerrainTileComponentIncarnation GetDownStaircaseTerrainTileComponentIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsDownStaircaseTerrainTileComponent[id].incarnation;
  }
  public bool DownStaircaseTerrainTileComponentExists(int id) {
    return rootIncarnation.incarnationsDownStaircaseTerrainTileComponent.ContainsKey(id);
  }
  public DownStaircaseTerrainTileComponent GetDownStaircaseTerrainTileComponent(int id) {
    return new DownStaircaseTerrainTileComponent(this, id);
  }
  public List<DownStaircaseTerrainTileComponent> AllDownStaircaseTerrainTileComponent() {
    List<DownStaircaseTerrainTileComponent> result = new List<DownStaircaseTerrainTileComponent>(rootIncarnation.incarnationsDownStaircaseTerrainTileComponent.Count);
    foreach (var id in rootIncarnation.incarnationsDownStaircaseTerrainTileComponent.Keys) {
      result.Add(new DownStaircaseTerrainTileComponent(this, id));
    }
    return result;
  }
  public IEnumerator<DownStaircaseTerrainTileComponent> EnumAllDownStaircaseTerrainTileComponent() {
    foreach (var id in rootIncarnation.incarnationsDownStaircaseTerrainTileComponent.Keys) {
      yield return GetDownStaircaseTerrainTileComponent(id);
    }
  }
  public void CheckHasDownStaircaseTerrainTileComponent(DownStaircaseTerrainTileComponent thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasDownStaircaseTerrainTileComponent(thing.id);
  }
  public void CheckHasDownStaircaseTerrainTileComponent(int id) {
    if (!rootIncarnation.incarnationsDownStaircaseTerrainTileComponent.ContainsKey(id)) {
      throw new System.Exception("Invalid DownStaircaseTerrainTileComponent!");
    }
  }
  public void AddDownStaircaseTerrainTileComponentObserver(int id, IDownStaircaseTerrainTileComponentEffectObserver observer) {
    List<IDownStaircaseTerrainTileComponentEffectObserver> obsies;
    if (!observersDownStaircaseTerrainTileComponent.TryGetValue(id, out obsies)) {
      obsies = new List<IDownStaircaseTerrainTileComponentEffectObserver>();
    }
    obsies.Add(observer);
    observersDownStaircaseTerrainTileComponent[id] = obsies;
  }

  public void RemoveDownStaircaseTerrainTileComponentObserver(int id, IDownStaircaseTerrainTileComponentEffectObserver observer) {
    if (observersDownStaircaseTerrainTileComponent.ContainsKey(id)) {
      var list = observersDownStaircaseTerrainTileComponent[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersDownStaircaseTerrainTileComponent.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastDownStaircaseTerrainTileComponentEffect(int id, IDownStaircaseTerrainTileComponentEffect effect) {
    if (observersDownStaircaseTerrainTileComponent.ContainsKey(0)) {
      foreach (var observer in new List<IDownStaircaseTerrainTileComponentEffectObserver>(observersDownStaircaseTerrainTileComponent[0])) {
        observer.OnDownStaircaseTerrainTileComponentEffect(effect);
      }
    }
    if (observersDownStaircaseTerrainTileComponent.ContainsKey(id)) {
      foreach (var observer in new List<IDownStaircaseTerrainTileComponentEffectObserver>(observersDownStaircaseTerrainTileComponent[id])) {
        observer.OnDownStaircaseTerrainTileComponentEffect(effect);
      }
    }
  }

  public DownStaircaseTerrainTileComponent EffectDownStaircaseTerrainTileComponentCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new DownStaircaseTerrainTileComponentIncarnation(

            );
    EffectInternalCreateDownStaircaseTerrainTileComponent(id, incarnation);
    return new DownStaircaseTerrainTileComponent(this, id);
  }
  public void EffectInternalCreateDownStaircaseTerrainTileComponent(
      int id,
      DownStaircaseTerrainTileComponentIncarnation incarnation) {
    CheckUnlocked();
    var effect = new DownStaircaseTerrainTileComponentCreateEffect(id, incarnation);
    rootIncarnation.incarnationsDownStaircaseTerrainTileComponent.Add(
        id,
        new VersionAndIncarnation<DownStaircaseTerrainTileComponentIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetDownStaircaseTerrainTileComponentHash(id, rootIncarnation.version, incarnation);
    BroadcastDownStaircaseTerrainTileComponentEffect(id, effect);
  }

  public void EffectDownStaircaseTerrainTileComponentDelete(int id) {
    CheckUnlocked();
    var effect = new DownStaircaseTerrainTileComponentDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsDownStaircaseTerrainTileComponent[id];
    this.rootIncarnation.hash -=
        GetDownStaircaseTerrainTileComponentHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastDownStaircaseTerrainTileComponentEffect(id, effect);
    rootIncarnation.incarnationsDownStaircaseTerrainTileComponent.Remove(id);
  }

     
  public int GetDownStaircaseTerrainTileComponentHash(int id, int version, DownStaircaseTerrainTileComponentIncarnation incarnation) {
    int result = id * version;
    return result;
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
    foreach (var id in rootIncarnation.incarnationsTerrainTile.Keys) {
      result.Add(new TerrainTile(this, id));
    }
    return result;
  }
  public IEnumerator<TerrainTile> EnumAllTerrainTile() {
    foreach (var id in rootIncarnation.incarnationsTerrainTile.Keys) {
      yield return GetTerrainTile(id);
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
      ITerrainTileComponentMutBunch components) {
    CheckUnlocked();
    CheckHasITerrainTileComponentMutBunch(components);

    var id = NewId();
    var incarnation =
        new TerrainTileIncarnation(
            elevation,
            walkable,
            classId,
            components.id
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
    result += id * version * 4 * incarnation.components.GetDeterministicHashCode();
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
              oldIncarnationAndVersion.incarnation.components);
      rootIncarnation.incarnationsTerrainTile[id] =
          new VersionAndIncarnation<TerrainTileIncarnation>(
              rootIncarnation.version,
              newIncarnation);
      this.rootIncarnation.hash -= GetTerrainTileHash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
      this.rootIncarnation.hash += GetTerrainTileHash(id, rootIncarnation.version, newIncarnation);
    }

    BroadcastTerrainTileEffect(id, effect);
  }
  public ITerrainTileComponentMutBunchIncarnation GetITerrainTileComponentMutBunchIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsITerrainTileComponentMutBunch[id].incarnation;
  }
  public bool ITerrainTileComponentMutBunchExists(int id) {
    return rootIncarnation.incarnationsITerrainTileComponentMutBunch.ContainsKey(id);
  }
  public ITerrainTileComponentMutBunch GetITerrainTileComponentMutBunch(int id) {
    return new ITerrainTileComponentMutBunch(this, id);
  }
  public List<ITerrainTileComponentMutBunch> AllITerrainTileComponentMutBunch() {
    List<ITerrainTileComponentMutBunch> result = new List<ITerrainTileComponentMutBunch>(rootIncarnation.incarnationsITerrainTileComponentMutBunch.Count);
    foreach (var id in rootIncarnation.incarnationsITerrainTileComponentMutBunch.Keys) {
      result.Add(new ITerrainTileComponentMutBunch(this, id));
    }
    return result;
  }
  public IEnumerator<ITerrainTileComponentMutBunch> EnumAllITerrainTileComponentMutBunch() {
    foreach (var id in rootIncarnation.incarnationsITerrainTileComponentMutBunch.Keys) {
      yield return GetITerrainTileComponentMutBunch(id);
    }
  }
  public void CheckHasITerrainTileComponentMutBunch(ITerrainTileComponentMutBunch thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasITerrainTileComponentMutBunch(thing.id);
  }
  public void CheckHasITerrainTileComponentMutBunch(int id) {
    if (!rootIncarnation.incarnationsITerrainTileComponentMutBunch.ContainsKey(id)) {
      throw new System.Exception("Invalid ITerrainTileComponentMutBunch!");
    }
  }
  public void AddITerrainTileComponentMutBunchObserver(int id, IITerrainTileComponentMutBunchEffectObserver observer) {
    List<IITerrainTileComponentMutBunchEffectObserver> obsies;
    if (!observersITerrainTileComponentMutBunch.TryGetValue(id, out obsies)) {
      obsies = new List<IITerrainTileComponentMutBunchEffectObserver>();
    }
    obsies.Add(observer);
    observersITerrainTileComponentMutBunch[id] = obsies;
  }

  public void RemoveITerrainTileComponentMutBunchObserver(int id, IITerrainTileComponentMutBunchEffectObserver observer) {
    if (observersITerrainTileComponentMutBunch.ContainsKey(id)) {
      var list = observersITerrainTileComponentMutBunch[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersITerrainTileComponentMutBunch.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastITerrainTileComponentMutBunchEffect(int id, IITerrainTileComponentMutBunchEffect effect) {
    if (observersITerrainTileComponentMutBunch.ContainsKey(0)) {
      foreach (var observer in new List<IITerrainTileComponentMutBunchEffectObserver>(observersITerrainTileComponentMutBunch[0])) {
        observer.OnITerrainTileComponentMutBunchEffect(effect);
      }
    }
    if (observersITerrainTileComponentMutBunch.ContainsKey(id)) {
      foreach (var observer in new List<IITerrainTileComponentMutBunchEffectObserver>(observersITerrainTileComponentMutBunch[id])) {
        observer.OnITerrainTileComponentMutBunchEffect(effect);
      }
    }
  }

  public ITerrainTileComponentMutBunch EffectITerrainTileComponentMutBunchCreate(
      DecorativeTerrainTileComponentMutSet membersDecorativeTerrainTileComponentMutSet,
      UpStaircaseTerrainTileComponentMutSet membersUpStaircaseTerrainTileComponentMutSet,
      DownStaircaseTerrainTileComponentMutSet membersDownStaircaseTerrainTileComponentMutSet) {
    CheckUnlocked();
    CheckHasDecorativeTerrainTileComponentMutSet(membersDecorativeTerrainTileComponentMutSet);
    CheckHasUpStaircaseTerrainTileComponentMutSet(membersUpStaircaseTerrainTileComponentMutSet);
    CheckHasDownStaircaseTerrainTileComponentMutSet(membersDownStaircaseTerrainTileComponentMutSet);

    var id = NewId();
    var incarnation =
        new ITerrainTileComponentMutBunchIncarnation(
            membersDecorativeTerrainTileComponentMutSet.id,
            membersUpStaircaseTerrainTileComponentMutSet.id,
            membersDownStaircaseTerrainTileComponentMutSet.id
            );
    EffectInternalCreateITerrainTileComponentMutBunch(id, incarnation);
    return new ITerrainTileComponentMutBunch(this, id);
  }
  public void EffectInternalCreateITerrainTileComponentMutBunch(
      int id,
      ITerrainTileComponentMutBunchIncarnation incarnation) {
    CheckUnlocked();
    var effect = new ITerrainTileComponentMutBunchCreateEffect(id, incarnation);
    rootIncarnation.incarnationsITerrainTileComponentMutBunch.Add(
        id,
        new VersionAndIncarnation<ITerrainTileComponentMutBunchIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetITerrainTileComponentMutBunchHash(id, rootIncarnation.version, incarnation);
    BroadcastITerrainTileComponentMutBunchEffect(id, effect);
  }

  public void EffectITerrainTileComponentMutBunchDelete(int id) {
    CheckUnlocked();
    var effect = new ITerrainTileComponentMutBunchDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsITerrainTileComponentMutBunch[id];
    this.rootIncarnation.hash -=
        GetITerrainTileComponentMutBunchHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastITerrainTileComponentMutBunchEffect(id, effect);
    rootIncarnation.incarnationsITerrainTileComponentMutBunch.Remove(id);
  }

     
  public int GetITerrainTileComponentMutBunchHash(int id, int version, ITerrainTileComponentMutBunchIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.membersDecorativeTerrainTileComponentMutSet.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.membersUpStaircaseTerrainTileComponentMutSet.GetDeterministicHashCode();
    result += id * version * 3 * incarnation.membersDownStaircaseTerrainTileComponentMutSet.GetDeterministicHashCode();
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
    foreach (var id in rootIncarnation.incarnationsTerrain.Keys) {
      result.Add(new Terrain(this, id));
    }
    return result;
  }
  public IEnumerator<Terrain> EnumAllTerrain() {
    foreach (var id in rootIncarnation.incarnationsTerrain.Keys) {
      yield return GetTerrain(id);
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
    CheckUnlocked();
    CheckHasTerrainTileByLocationMutMap(tiles);

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
    foreach (var id in rootIncarnation.incarnationsGlaive.Keys) {
      result.Add(new Glaive(this, id));
    }
    return result;
  }
  public IEnumerator<Glaive> EnumAllGlaive() {
    foreach (var id in rootIncarnation.incarnationsGlaive.Keys) {
      yield return GetGlaive(id);
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
    foreach (var id in rootIncarnation.incarnationsArmor.Keys) {
      result.Add(new Armor(this, id));
    }
    return result;
  }
  public IEnumerator<Armor> EnumAllArmor() {
    foreach (var id in rootIncarnation.incarnationsArmor.Keys) {
      yield return GetArmor(id);
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
    foreach (var id in rootIncarnation.incarnationsRand.Keys) {
      result.Add(new Rand(this, id));
    }
    return result;
  }
  public IEnumerator<Rand> EnumAllRand() {
    foreach (var id in rootIncarnation.incarnationsRand.Keys) {
      yield return GetRand(id);
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
  public WanderAICapabilityUCIncarnation GetWanderAICapabilityUCIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsWanderAICapabilityUC[id].incarnation;
  }
  public bool WanderAICapabilityUCExists(int id) {
    return rootIncarnation.incarnationsWanderAICapabilityUC.ContainsKey(id);
  }
  public WanderAICapabilityUC GetWanderAICapabilityUC(int id) {
    return new WanderAICapabilityUC(this, id);
  }
  public List<WanderAICapabilityUC> AllWanderAICapabilityUC() {
    List<WanderAICapabilityUC> result = new List<WanderAICapabilityUC>(rootIncarnation.incarnationsWanderAICapabilityUC.Count);
    foreach (var id in rootIncarnation.incarnationsWanderAICapabilityUC.Keys) {
      result.Add(new WanderAICapabilityUC(this, id));
    }
    return result;
  }
  public IEnumerator<WanderAICapabilityUC> EnumAllWanderAICapabilityUC() {
    foreach (var id in rootIncarnation.incarnationsWanderAICapabilityUC.Keys) {
      yield return GetWanderAICapabilityUC(id);
    }
  }
  public void CheckHasWanderAICapabilityUC(WanderAICapabilityUC thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasWanderAICapabilityUC(thing.id);
  }
  public void CheckHasWanderAICapabilityUC(int id) {
    if (!rootIncarnation.incarnationsWanderAICapabilityUC.ContainsKey(id)) {
      throw new System.Exception("Invalid WanderAICapabilityUC!");
    }
  }
  public void AddWanderAICapabilityUCObserver(int id, IWanderAICapabilityUCEffectObserver observer) {
    List<IWanderAICapabilityUCEffectObserver> obsies;
    if (!observersWanderAICapabilityUC.TryGetValue(id, out obsies)) {
      obsies = new List<IWanderAICapabilityUCEffectObserver>();
    }
    obsies.Add(observer);
    observersWanderAICapabilityUC[id] = obsies;
  }

  public void RemoveWanderAICapabilityUCObserver(int id, IWanderAICapabilityUCEffectObserver observer) {
    if (observersWanderAICapabilityUC.ContainsKey(id)) {
      var list = observersWanderAICapabilityUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersWanderAICapabilityUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastWanderAICapabilityUCEffect(int id, IWanderAICapabilityUCEffect effect) {
    if (observersWanderAICapabilityUC.ContainsKey(0)) {
      foreach (var observer in new List<IWanderAICapabilityUCEffectObserver>(observersWanderAICapabilityUC[0])) {
        observer.OnWanderAICapabilityUCEffect(effect);
      }
    }
    if (observersWanderAICapabilityUC.ContainsKey(id)) {
      foreach (var observer in new List<IWanderAICapabilityUCEffectObserver>(observersWanderAICapabilityUC[id])) {
        observer.OnWanderAICapabilityUCEffect(effect);
      }
    }
  }

  public WanderAICapabilityUC EffectWanderAICapabilityUCCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new WanderAICapabilityUCIncarnation(

            );
    EffectInternalCreateWanderAICapabilityUC(id, incarnation);
    return new WanderAICapabilityUC(this, id);
  }
  public void EffectInternalCreateWanderAICapabilityUC(
      int id,
      WanderAICapabilityUCIncarnation incarnation) {
    CheckUnlocked();
    var effect = new WanderAICapabilityUCCreateEffect(id, incarnation);
    rootIncarnation.incarnationsWanderAICapabilityUC.Add(
        id,
        new VersionAndIncarnation<WanderAICapabilityUCIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetWanderAICapabilityUCHash(id, rootIncarnation.version, incarnation);
    BroadcastWanderAICapabilityUCEffect(id, effect);
  }

  public void EffectWanderAICapabilityUCDelete(int id) {
    CheckUnlocked();
    var effect = new WanderAICapabilityUCDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsWanderAICapabilityUC[id];
    this.rootIncarnation.hash -=
        GetWanderAICapabilityUCHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastWanderAICapabilityUCEffect(id, effect);
    rootIncarnation.incarnationsWanderAICapabilityUC.Remove(id);
  }

     
  public int GetWanderAICapabilityUCHash(int id, int version, WanderAICapabilityUCIncarnation incarnation) {
    int result = id * version;
    return result;
  }
       public ShieldingUCIncarnation GetShieldingUCIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsShieldingUC[id].incarnation;
  }
  public bool ShieldingUCExists(int id) {
    return rootIncarnation.incarnationsShieldingUC.ContainsKey(id);
  }
  public ShieldingUC GetShieldingUC(int id) {
    return new ShieldingUC(this, id);
  }
  public List<ShieldingUC> AllShieldingUC() {
    List<ShieldingUC> result = new List<ShieldingUC>(rootIncarnation.incarnationsShieldingUC.Count);
    foreach (var id in rootIncarnation.incarnationsShieldingUC.Keys) {
      result.Add(new ShieldingUC(this, id));
    }
    return result;
  }
  public IEnumerator<ShieldingUC> EnumAllShieldingUC() {
    foreach (var id in rootIncarnation.incarnationsShieldingUC.Keys) {
      yield return GetShieldingUC(id);
    }
  }
  public void CheckHasShieldingUC(ShieldingUC thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasShieldingUC(thing.id);
  }
  public void CheckHasShieldingUC(int id) {
    if (!rootIncarnation.incarnationsShieldingUC.ContainsKey(id)) {
      throw new System.Exception("Invalid ShieldingUC!");
    }
  }
  public void AddShieldingUCObserver(int id, IShieldingUCEffectObserver observer) {
    List<IShieldingUCEffectObserver> obsies;
    if (!observersShieldingUC.TryGetValue(id, out obsies)) {
      obsies = new List<IShieldingUCEffectObserver>();
    }
    obsies.Add(observer);
    observersShieldingUC[id] = obsies;
  }

  public void RemoveShieldingUCObserver(int id, IShieldingUCEffectObserver observer) {
    if (observersShieldingUC.ContainsKey(id)) {
      var list = observersShieldingUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersShieldingUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastShieldingUCEffect(int id, IShieldingUCEffect effect) {
    if (observersShieldingUC.ContainsKey(0)) {
      foreach (var observer in new List<IShieldingUCEffectObserver>(observersShieldingUC[0])) {
        observer.OnShieldingUCEffect(effect);
      }
    }
    if (observersShieldingUC.ContainsKey(id)) {
      foreach (var observer in new List<IShieldingUCEffectObserver>(observersShieldingUC[id])) {
        observer.OnShieldingUCEffect(effect);
      }
    }
  }

  public ShieldingUC EffectShieldingUCCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new ShieldingUCIncarnation(

            );
    EffectInternalCreateShieldingUC(id, incarnation);
    return new ShieldingUC(this, id);
  }
  public void EffectInternalCreateShieldingUC(
      int id,
      ShieldingUCIncarnation incarnation) {
    CheckUnlocked();
    var effect = new ShieldingUCCreateEffect(id, incarnation);
    rootIncarnation.incarnationsShieldingUC.Add(
        id,
        new VersionAndIncarnation<ShieldingUCIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetShieldingUCHash(id, rootIncarnation.version, incarnation);
    BroadcastShieldingUCEffect(id, effect);
  }

  public void EffectShieldingUCDelete(int id) {
    CheckUnlocked();
    var effect = new ShieldingUCDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsShieldingUC[id];
    this.rootIncarnation.hash -=
        GetShieldingUCHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastShieldingUCEffect(id, effect);
    rootIncarnation.incarnationsShieldingUC.Remove(id);
  }

     
  public int GetShieldingUCHash(int id, int version, ShieldingUCIncarnation incarnation) {
    int result = id * version;
    return result;
  }
       public BidingOperationUCIncarnation GetBidingOperationUCIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsBidingOperationUC[id].incarnation;
  }
  public bool BidingOperationUCExists(int id) {
    return rootIncarnation.incarnationsBidingOperationUC.ContainsKey(id);
  }
  public BidingOperationUC GetBidingOperationUC(int id) {
    return new BidingOperationUC(this, id);
  }
  public List<BidingOperationUC> AllBidingOperationUC() {
    List<BidingOperationUC> result = new List<BidingOperationUC>(rootIncarnation.incarnationsBidingOperationUC.Count);
    foreach (var id in rootIncarnation.incarnationsBidingOperationUC.Keys) {
      result.Add(new BidingOperationUC(this, id));
    }
    return result;
  }
  public IEnumerator<BidingOperationUC> EnumAllBidingOperationUC() {
    foreach (var id in rootIncarnation.incarnationsBidingOperationUC.Keys) {
      yield return GetBidingOperationUC(id);
    }
  }
  public void CheckHasBidingOperationUC(BidingOperationUC thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasBidingOperationUC(thing.id);
  }
  public void CheckHasBidingOperationUC(int id) {
    if (!rootIncarnation.incarnationsBidingOperationUC.ContainsKey(id)) {
      throw new System.Exception("Invalid BidingOperationUC!");
    }
  }
  public void AddBidingOperationUCObserver(int id, IBidingOperationUCEffectObserver observer) {
    List<IBidingOperationUCEffectObserver> obsies;
    if (!observersBidingOperationUC.TryGetValue(id, out obsies)) {
      obsies = new List<IBidingOperationUCEffectObserver>();
    }
    obsies.Add(observer);
    observersBidingOperationUC[id] = obsies;
  }

  public void RemoveBidingOperationUCObserver(int id, IBidingOperationUCEffectObserver observer) {
    if (observersBidingOperationUC.ContainsKey(id)) {
      var list = observersBidingOperationUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersBidingOperationUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastBidingOperationUCEffect(int id, IBidingOperationUCEffect effect) {
    if (observersBidingOperationUC.ContainsKey(0)) {
      foreach (var observer in new List<IBidingOperationUCEffectObserver>(observersBidingOperationUC[0])) {
        observer.OnBidingOperationUCEffect(effect);
      }
    }
    if (observersBidingOperationUC.ContainsKey(id)) {
      foreach (var observer in new List<IBidingOperationUCEffectObserver>(observersBidingOperationUC[id])) {
        observer.OnBidingOperationUCEffect(effect);
      }
    }
  }

  public BidingOperationUC EffectBidingOperationUCCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new BidingOperationUCIncarnation(

            );
    EffectInternalCreateBidingOperationUC(id, incarnation);
    return new BidingOperationUC(this, id);
  }
  public void EffectInternalCreateBidingOperationUC(
      int id,
      BidingOperationUCIncarnation incarnation) {
    CheckUnlocked();
    var effect = new BidingOperationUCCreateEffect(id, incarnation);
    rootIncarnation.incarnationsBidingOperationUC.Add(
        id,
        new VersionAndIncarnation<BidingOperationUCIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetBidingOperationUCHash(id, rootIncarnation.version, incarnation);
    BroadcastBidingOperationUCEffect(id, effect);
  }

  public void EffectBidingOperationUCDelete(int id) {
    CheckUnlocked();
    var effect = new BidingOperationUCDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsBidingOperationUC[id];
    this.rootIncarnation.hash -=
        GetBidingOperationUCHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastBidingOperationUCEffect(id, effect);
    rootIncarnation.incarnationsBidingOperationUC.Remove(id);
  }

     
  public int GetBidingOperationUCHash(int id, int version, BidingOperationUCIncarnation incarnation) {
    int result = id * version;
    return result;
  }
       public UnleashBideImpulseIncarnation GetUnleashBideImpulseIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsUnleashBideImpulse[id].incarnation;
  }
  public bool UnleashBideImpulseExists(int id) {
    return rootIncarnation.incarnationsUnleashBideImpulse.ContainsKey(id);
  }
  public UnleashBideImpulse GetUnleashBideImpulse(int id) {
    return new UnleashBideImpulse(this, id);
  }
  public List<UnleashBideImpulse> AllUnleashBideImpulse() {
    List<UnleashBideImpulse> result = new List<UnleashBideImpulse>(rootIncarnation.incarnationsUnleashBideImpulse.Count);
    foreach (var id in rootIncarnation.incarnationsUnleashBideImpulse.Keys) {
      result.Add(new UnleashBideImpulse(this, id));
    }
    return result;
  }
  public IEnumerator<UnleashBideImpulse> EnumAllUnleashBideImpulse() {
    foreach (var id in rootIncarnation.incarnationsUnleashBideImpulse.Keys) {
      yield return GetUnleashBideImpulse(id);
    }
  }
  public void CheckHasUnleashBideImpulse(UnleashBideImpulse thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasUnleashBideImpulse(thing.id);
  }
  public void CheckHasUnleashBideImpulse(int id) {
    if (!rootIncarnation.incarnationsUnleashBideImpulse.ContainsKey(id)) {
      throw new System.Exception("Invalid UnleashBideImpulse!");
    }
  }
  public void AddUnleashBideImpulseObserver(int id, IUnleashBideImpulseEffectObserver observer) {
    List<IUnleashBideImpulseEffectObserver> obsies;
    if (!observersUnleashBideImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<IUnleashBideImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersUnleashBideImpulse[id] = obsies;
  }

  public void RemoveUnleashBideImpulseObserver(int id, IUnleashBideImpulseEffectObserver observer) {
    if (observersUnleashBideImpulse.ContainsKey(id)) {
      var list = observersUnleashBideImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersUnleashBideImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastUnleashBideImpulseEffect(int id, IUnleashBideImpulseEffect effect) {
    if (observersUnleashBideImpulse.ContainsKey(0)) {
      foreach (var observer in new List<IUnleashBideImpulseEffectObserver>(observersUnleashBideImpulse[0])) {
        observer.OnUnleashBideImpulseEffect(effect);
      }
    }
    if (observersUnleashBideImpulse.ContainsKey(id)) {
      foreach (var observer in new List<IUnleashBideImpulseEffectObserver>(observersUnleashBideImpulse[id])) {
        observer.OnUnleashBideImpulseEffect(effect);
      }
    }
  }

  public UnleashBideImpulse EffectUnleashBideImpulseCreate(
      int weight) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new UnleashBideImpulseIncarnation(
            weight
            );
    EffectInternalCreateUnleashBideImpulse(id, incarnation);
    return new UnleashBideImpulse(this, id);
  }
  public void EffectInternalCreateUnleashBideImpulse(
      int id,
      UnleashBideImpulseIncarnation incarnation) {
    CheckUnlocked();
    var effect = new UnleashBideImpulseCreateEffect(id, incarnation);
    rootIncarnation.incarnationsUnleashBideImpulse.Add(
        id,
        new VersionAndIncarnation<UnleashBideImpulseIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetUnleashBideImpulseHash(id, rootIncarnation.version, incarnation);
    BroadcastUnleashBideImpulseEffect(id, effect);
  }

  public void EffectUnleashBideImpulseDelete(int id) {
    CheckUnlocked();
    var effect = new UnleashBideImpulseDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsUnleashBideImpulse[id];
    this.rootIncarnation.hash -=
        GetUnleashBideImpulseHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastUnleashBideImpulseEffect(id, effect);
    rootIncarnation.incarnationsUnleashBideImpulse.Remove(id);
  }

     
  public int GetUnleashBideImpulseHash(int id, int version, UnleashBideImpulseIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.weight.GetDeterministicHashCode();
    return result;
  }
       public StartBidingImpulseIncarnation GetStartBidingImpulseIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsStartBidingImpulse[id].incarnation;
  }
  public bool StartBidingImpulseExists(int id) {
    return rootIncarnation.incarnationsStartBidingImpulse.ContainsKey(id);
  }
  public StartBidingImpulse GetStartBidingImpulse(int id) {
    return new StartBidingImpulse(this, id);
  }
  public List<StartBidingImpulse> AllStartBidingImpulse() {
    List<StartBidingImpulse> result = new List<StartBidingImpulse>(rootIncarnation.incarnationsStartBidingImpulse.Count);
    foreach (var id in rootIncarnation.incarnationsStartBidingImpulse.Keys) {
      result.Add(new StartBidingImpulse(this, id));
    }
    return result;
  }
  public IEnumerator<StartBidingImpulse> EnumAllStartBidingImpulse() {
    foreach (var id in rootIncarnation.incarnationsStartBidingImpulse.Keys) {
      yield return GetStartBidingImpulse(id);
    }
  }
  public void CheckHasStartBidingImpulse(StartBidingImpulse thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasStartBidingImpulse(thing.id);
  }
  public void CheckHasStartBidingImpulse(int id) {
    if (!rootIncarnation.incarnationsStartBidingImpulse.ContainsKey(id)) {
      throw new System.Exception("Invalid StartBidingImpulse!");
    }
  }
  public void AddStartBidingImpulseObserver(int id, IStartBidingImpulseEffectObserver observer) {
    List<IStartBidingImpulseEffectObserver> obsies;
    if (!observersStartBidingImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<IStartBidingImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersStartBidingImpulse[id] = obsies;
  }

  public void RemoveStartBidingImpulseObserver(int id, IStartBidingImpulseEffectObserver observer) {
    if (observersStartBidingImpulse.ContainsKey(id)) {
      var list = observersStartBidingImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersStartBidingImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastStartBidingImpulseEffect(int id, IStartBidingImpulseEffect effect) {
    if (observersStartBidingImpulse.ContainsKey(0)) {
      foreach (var observer in new List<IStartBidingImpulseEffectObserver>(observersStartBidingImpulse[0])) {
        observer.OnStartBidingImpulseEffect(effect);
      }
    }
    if (observersStartBidingImpulse.ContainsKey(id)) {
      foreach (var observer in new List<IStartBidingImpulseEffectObserver>(observersStartBidingImpulse[id])) {
        observer.OnStartBidingImpulseEffect(effect);
      }
    }
  }

  public StartBidingImpulse EffectStartBidingImpulseCreate(
      int weight) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new StartBidingImpulseIncarnation(
            weight
            );
    EffectInternalCreateStartBidingImpulse(id, incarnation);
    return new StartBidingImpulse(this, id);
  }
  public void EffectInternalCreateStartBidingImpulse(
      int id,
      StartBidingImpulseIncarnation incarnation) {
    CheckUnlocked();
    var effect = new StartBidingImpulseCreateEffect(id, incarnation);
    rootIncarnation.incarnationsStartBidingImpulse.Add(
        id,
        new VersionAndIncarnation<StartBidingImpulseIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetStartBidingImpulseHash(id, rootIncarnation.version, incarnation);
    BroadcastStartBidingImpulseEffect(id, effect);
  }

  public void EffectStartBidingImpulseDelete(int id) {
    CheckUnlocked();
    var effect = new StartBidingImpulseDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsStartBidingImpulse[id];
    this.rootIncarnation.hash -=
        GetStartBidingImpulseHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastStartBidingImpulseEffect(id, effect);
    rootIncarnation.incarnationsStartBidingImpulse.Remove(id);
  }

     
  public int GetStartBidingImpulseHash(int id, int version, StartBidingImpulseIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.weight.GetDeterministicHashCode();
    return result;
  }
       public BideAICapabilityUCIncarnation GetBideAICapabilityUCIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsBideAICapabilityUC[id].incarnation;
  }
  public bool BideAICapabilityUCExists(int id) {
    return rootIncarnation.incarnationsBideAICapabilityUC.ContainsKey(id);
  }
  public BideAICapabilityUC GetBideAICapabilityUC(int id) {
    return new BideAICapabilityUC(this, id);
  }
  public List<BideAICapabilityUC> AllBideAICapabilityUC() {
    List<BideAICapabilityUC> result = new List<BideAICapabilityUC>(rootIncarnation.incarnationsBideAICapabilityUC.Count);
    foreach (var id in rootIncarnation.incarnationsBideAICapabilityUC.Keys) {
      result.Add(new BideAICapabilityUC(this, id));
    }
    return result;
  }
  public IEnumerator<BideAICapabilityUC> EnumAllBideAICapabilityUC() {
    foreach (var id in rootIncarnation.incarnationsBideAICapabilityUC.Keys) {
      yield return GetBideAICapabilityUC(id);
    }
  }
  public void CheckHasBideAICapabilityUC(BideAICapabilityUC thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasBideAICapabilityUC(thing.id);
  }
  public void CheckHasBideAICapabilityUC(int id) {
    if (!rootIncarnation.incarnationsBideAICapabilityUC.ContainsKey(id)) {
      throw new System.Exception("Invalid BideAICapabilityUC!");
    }
  }
  public void AddBideAICapabilityUCObserver(int id, IBideAICapabilityUCEffectObserver observer) {
    List<IBideAICapabilityUCEffectObserver> obsies;
    if (!observersBideAICapabilityUC.TryGetValue(id, out obsies)) {
      obsies = new List<IBideAICapabilityUCEffectObserver>();
    }
    obsies.Add(observer);
    observersBideAICapabilityUC[id] = obsies;
  }

  public void RemoveBideAICapabilityUCObserver(int id, IBideAICapabilityUCEffectObserver observer) {
    if (observersBideAICapabilityUC.ContainsKey(id)) {
      var list = observersBideAICapabilityUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersBideAICapabilityUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastBideAICapabilityUCEffect(int id, IBideAICapabilityUCEffect effect) {
    if (observersBideAICapabilityUC.ContainsKey(0)) {
      foreach (var observer in new List<IBideAICapabilityUCEffectObserver>(observersBideAICapabilityUC[0])) {
        observer.OnBideAICapabilityUCEffect(effect);
      }
    }
    if (observersBideAICapabilityUC.ContainsKey(id)) {
      foreach (var observer in new List<IBideAICapabilityUCEffectObserver>(observersBideAICapabilityUC[id])) {
        observer.OnBideAICapabilityUCEffect(effect);
      }
    }
  }

  public BideAICapabilityUC EffectBideAICapabilityUCCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new BideAICapabilityUCIncarnation(

            );
    EffectInternalCreateBideAICapabilityUC(id, incarnation);
    return new BideAICapabilityUC(this, id);
  }
  public void EffectInternalCreateBideAICapabilityUC(
      int id,
      BideAICapabilityUCIncarnation incarnation) {
    CheckUnlocked();
    var effect = new BideAICapabilityUCCreateEffect(id, incarnation);
    rootIncarnation.incarnationsBideAICapabilityUC.Add(
        id,
        new VersionAndIncarnation<BideAICapabilityUCIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetBideAICapabilityUCHash(id, rootIncarnation.version, incarnation);
    BroadcastBideAICapabilityUCEffect(id, effect);
  }

  public void EffectBideAICapabilityUCDelete(int id) {
    CheckUnlocked();
    var effect = new BideAICapabilityUCDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsBideAICapabilityUC[id];
    this.rootIncarnation.hash -=
        GetBideAICapabilityUCHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastBideAICapabilityUCEffect(id, effect);
    rootIncarnation.incarnationsBideAICapabilityUC.Remove(id);
  }

     
  public int GetBideAICapabilityUCHash(int id, int version, BideAICapabilityUCIncarnation incarnation) {
    int result = id * version;
    return result;
  }
       public AttackImpulseIncarnation GetAttackImpulseIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsAttackImpulse[id].incarnation;
  }
  public bool AttackImpulseExists(int id) {
    return rootIncarnation.incarnationsAttackImpulse.ContainsKey(id);
  }
  public AttackImpulse GetAttackImpulse(int id) {
    return new AttackImpulse(this, id);
  }
  public List<AttackImpulse> AllAttackImpulse() {
    List<AttackImpulse> result = new List<AttackImpulse>(rootIncarnation.incarnationsAttackImpulse.Count);
    foreach (var id in rootIncarnation.incarnationsAttackImpulse.Keys) {
      result.Add(new AttackImpulse(this, id));
    }
    return result;
  }
  public IEnumerator<AttackImpulse> EnumAllAttackImpulse() {
    foreach (var id in rootIncarnation.incarnationsAttackImpulse.Keys) {
      yield return GetAttackImpulse(id);
    }
  }
  public void CheckHasAttackImpulse(AttackImpulse thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasAttackImpulse(thing.id);
  }
  public void CheckHasAttackImpulse(int id) {
    if (!rootIncarnation.incarnationsAttackImpulse.ContainsKey(id)) {
      throw new System.Exception("Invalid AttackImpulse!");
    }
  }
  public void AddAttackImpulseObserver(int id, IAttackImpulseEffectObserver observer) {
    List<IAttackImpulseEffectObserver> obsies;
    if (!observersAttackImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<IAttackImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersAttackImpulse[id] = obsies;
  }

  public void RemoveAttackImpulseObserver(int id, IAttackImpulseEffectObserver observer) {
    if (observersAttackImpulse.ContainsKey(id)) {
      var list = observersAttackImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersAttackImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastAttackImpulseEffect(int id, IAttackImpulseEffect effect) {
    if (observersAttackImpulse.ContainsKey(0)) {
      foreach (var observer in new List<IAttackImpulseEffectObserver>(observersAttackImpulse[0])) {
        observer.OnAttackImpulseEffect(effect);
      }
    }
    if (observersAttackImpulse.ContainsKey(id)) {
      foreach (var observer in new List<IAttackImpulseEffectObserver>(observersAttackImpulse[id])) {
        observer.OnAttackImpulseEffect(effect);
      }
    }
  }

  public AttackImpulse EffectAttackImpulseCreate(
      int weight) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new AttackImpulseIncarnation(
            weight
            );
    EffectInternalCreateAttackImpulse(id, incarnation);
    return new AttackImpulse(this, id);
  }
  public void EffectInternalCreateAttackImpulse(
      int id,
      AttackImpulseIncarnation incarnation) {
    CheckUnlocked();
    var effect = new AttackImpulseCreateEffect(id, incarnation);
    rootIncarnation.incarnationsAttackImpulse.Add(
        id,
        new VersionAndIncarnation<AttackImpulseIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetAttackImpulseHash(id, rootIncarnation.version, incarnation);
    BroadcastAttackImpulseEffect(id, effect);
  }

  public void EffectAttackImpulseDelete(int id) {
    CheckUnlocked();
    var effect = new AttackImpulseDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsAttackImpulse[id];
    this.rootIncarnation.hash -=
        GetAttackImpulseHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastAttackImpulseEffect(id, effect);
    rootIncarnation.incarnationsAttackImpulse.Remove(id);
  }

     
  public int GetAttackImpulseHash(int id, int version, AttackImpulseIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.weight.GetDeterministicHashCode();
    return result;
  }
       public PursueImpulseIncarnation GetPursueImpulseIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsPursueImpulse[id].incarnation;
  }
  public bool PursueImpulseExists(int id) {
    return rootIncarnation.incarnationsPursueImpulse.ContainsKey(id);
  }
  public PursueImpulse GetPursueImpulse(int id) {
    return new PursueImpulse(this, id);
  }
  public List<PursueImpulse> AllPursueImpulse() {
    List<PursueImpulse> result = new List<PursueImpulse>(rootIncarnation.incarnationsPursueImpulse.Count);
    foreach (var id in rootIncarnation.incarnationsPursueImpulse.Keys) {
      result.Add(new PursueImpulse(this, id));
    }
    return result;
  }
  public IEnumerator<PursueImpulse> EnumAllPursueImpulse() {
    foreach (var id in rootIncarnation.incarnationsPursueImpulse.Keys) {
      yield return GetPursueImpulse(id);
    }
  }
  public void CheckHasPursueImpulse(PursueImpulse thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasPursueImpulse(thing.id);
  }
  public void CheckHasPursueImpulse(int id) {
    if (!rootIncarnation.incarnationsPursueImpulse.ContainsKey(id)) {
      throw new System.Exception("Invalid PursueImpulse!");
    }
  }
  public void AddPursueImpulseObserver(int id, IPursueImpulseEffectObserver observer) {
    List<IPursueImpulseEffectObserver> obsies;
    if (!observersPursueImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<IPursueImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersPursueImpulse[id] = obsies;
  }

  public void RemovePursueImpulseObserver(int id, IPursueImpulseEffectObserver observer) {
    if (observersPursueImpulse.ContainsKey(id)) {
      var list = observersPursueImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersPursueImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastPursueImpulseEffect(int id, IPursueImpulseEffect effect) {
    if (observersPursueImpulse.ContainsKey(0)) {
      foreach (var observer in new List<IPursueImpulseEffectObserver>(observersPursueImpulse[0])) {
        observer.OnPursueImpulseEffect(effect);
      }
    }
    if (observersPursueImpulse.ContainsKey(id)) {
      foreach (var observer in new List<IPursueImpulseEffectObserver>(observersPursueImpulse[id])) {
        observer.OnPursueImpulseEffect(effect);
      }
    }
  }

  public PursueImpulse EffectPursueImpulseCreate(
      int weight,
      Location stepLocation) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new PursueImpulseIncarnation(
            weight,
            stepLocation
            );
    EffectInternalCreatePursueImpulse(id, incarnation);
    return new PursueImpulse(this, id);
  }
  public void EffectInternalCreatePursueImpulse(
      int id,
      PursueImpulseIncarnation incarnation) {
    CheckUnlocked();
    var effect = new PursueImpulseCreateEffect(id, incarnation);
    rootIncarnation.incarnationsPursueImpulse.Add(
        id,
        new VersionAndIncarnation<PursueImpulseIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetPursueImpulseHash(id, rootIncarnation.version, incarnation);
    BroadcastPursueImpulseEffect(id, effect);
  }

  public void EffectPursueImpulseDelete(int id) {
    CheckUnlocked();
    var effect = new PursueImpulseDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsPursueImpulse[id];
    this.rootIncarnation.hash -=
        GetPursueImpulseHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastPursueImpulseEffect(id, effect);
    rootIncarnation.incarnationsPursueImpulse.Remove(id);
  }

     
  public int GetPursueImpulseHash(int id, int version, PursueImpulseIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.weight.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.stepLocation.GetDeterministicHashCode();
    return result;
  }
       public KillDirectiveUCIncarnation GetKillDirectiveUCIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsKillDirectiveUC[id].incarnation;
  }
  public bool KillDirectiveUCExists(int id) {
    return rootIncarnation.incarnationsKillDirectiveUC.ContainsKey(id);
  }
  public KillDirectiveUC GetKillDirectiveUC(int id) {
    return new KillDirectiveUC(this, id);
  }
  public List<KillDirectiveUC> AllKillDirectiveUC() {
    List<KillDirectiveUC> result = new List<KillDirectiveUC>(rootIncarnation.incarnationsKillDirectiveUC.Count);
    foreach (var id in rootIncarnation.incarnationsKillDirectiveUC.Keys) {
      result.Add(new KillDirectiveUC(this, id));
    }
    return result;
  }
  public IEnumerator<KillDirectiveUC> EnumAllKillDirectiveUC() {
    foreach (var id in rootIncarnation.incarnationsKillDirectiveUC.Keys) {
      yield return GetKillDirectiveUC(id);
    }
  }
  public void CheckHasKillDirectiveUC(KillDirectiveUC thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasKillDirectiveUC(thing.id);
  }
  public void CheckHasKillDirectiveUC(int id) {
    if (!rootIncarnation.incarnationsKillDirectiveUC.ContainsKey(id)) {
      throw new System.Exception("Invalid KillDirectiveUC!");
    }
  }
  public void AddKillDirectiveUCObserver(int id, IKillDirectiveUCEffectObserver observer) {
    List<IKillDirectiveUCEffectObserver> obsies;
    if (!observersKillDirectiveUC.TryGetValue(id, out obsies)) {
      obsies = new List<IKillDirectiveUCEffectObserver>();
    }
    obsies.Add(observer);
    observersKillDirectiveUC[id] = obsies;
  }

  public void RemoveKillDirectiveUCObserver(int id, IKillDirectiveUCEffectObserver observer) {
    if (observersKillDirectiveUC.ContainsKey(id)) {
      var list = observersKillDirectiveUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersKillDirectiveUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastKillDirectiveUCEffect(int id, IKillDirectiveUCEffect effect) {
    if (observersKillDirectiveUC.ContainsKey(0)) {
      foreach (var observer in new List<IKillDirectiveUCEffectObserver>(observersKillDirectiveUC[0])) {
        observer.OnKillDirectiveUCEffect(effect);
      }
    }
    if (observersKillDirectiveUC.ContainsKey(id)) {
      foreach (var observer in new List<IKillDirectiveUCEffectObserver>(observersKillDirectiveUC[id])) {
        observer.OnKillDirectiveUCEffect(effect);
      }
    }
  }

  public KillDirectiveUC EffectKillDirectiveUCCreate(
      Unit targetUnit,
      LocationMutList pathToLastSeenLocation) {
    CheckUnlocked();
    CheckHasUnit(targetUnit);
    CheckHasLocationMutList(pathToLastSeenLocation);

    var id = NewId();
    var incarnation =
        new KillDirectiveUCIncarnation(
            targetUnit.id,
            pathToLastSeenLocation.id
            );
    EffectInternalCreateKillDirectiveUC(id, incarnation);
    return new KillDirectiveUC(this, id);
  }
  public void EffectInternalCreateKillDirectiveUC(
      int id,
      KillDirectiveUCIncarnation incarnation) {
    CheckUnlocked();
    var effect = new KillDirectiveUCCreateEffect(id, incarnation);
    rootIncarnation.incarnationsKillDirectiveUC.Add(
        id,
        new VersionAndIncarnation<KillDirectiveUCIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetKillDirectiveUCHash(id, rootIncarnation.version, incarnation);
    BroadcastKillDirectiveUCEffect(id, effect);
  }

  public void EffectKillDirectiveUCDelete(int id) {
    CheckUnlocked();
    var effect = new KillDirectiveUCDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsKillDirectiveUC[id];
    this.rootIncarnation.hash -=
        GetKillDirectiveUCHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastKillDirectiveUCEffect(id, effect);
    rootIncarnation.incarnationsKillDirectiveUC.Remove(id);
  }

     
  public int GetKillDirectiveUCHash(int id, int version, KillDirectiveUCIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.targetUnit.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.pathToLastSeenLocation.GetDeterministicHashCode();
    return result;
  }
       public AttackAICapabilityUCIncarnation GetAttackAICapabilityUCIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsAttackAICapabilityUC[id].incarnation;
  }
  public bool AttackAICapabilityUCExists(int id) {
    return rootIncarnation.incarnationsAttackAICapabilityUC.ContainsKey(id);
  }
  public AttackAICapabilityUC GetAttackAICapabilityUC(int id) {
    return new AttackAICapabilityUC(this, id);
  }
  public List<AttackAICapabilityUC> AllAttackAICapabilityUC() {
    List<AttackAICapabilityUC> result = new List<AttackAICapabilityUC>(rootIncarnation.incarnationsAttackAICapabilityUC.Count);
    foreach (var id in rootIncarnation.incarnationsAttackAICapabilityUC.Keys) {
      result.Add(new AttackAICapabilityUC(this, id));
    }
    return result;
  }
  public IEnumerator<AttackAICapabilityUC> EnumAllAttackAICapabilityUC() {
    foreach (var id in rootIncarnation.incarnationsAttackAICapabilityUC.Keys) {
      yield return GetAttackAICapabilityUC(id);
    }
  }
  public void CheckHasAttackAICapabilityUC(AttackAICapabilityUC thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasAttackAICapabilityUC(thing.id);
  }
  public void CheckHasAttackAICapabilityUC(int id) {
    if (!rootIncarnation.incarnationsAttackAICapabilityUC.ContainsKey(id)) {
      throw new System.Exception("Invalid AttackAICapabilityUC!");
    }
  }
  public void AddAttackAICapabilityUCObserver(int id, IAttackAICapabilityUCEffectObserver observer) {
    List<IAttackAICapabilityUCEffectObserver> obsies;
    if (!observersAttackAICapabilityUC.TryGetValue(id, out obsies)) {
      obsies = new List<IAttackAICapabilityUCEffectObserver>();
    }
    obsies.Add(observer);
    observersAttackAICapabilityUC[id] = obsies;
  }

  public void RemoveAttackAICapabilityUCObserver(int id, IAttackAICapabilityUCEffectObserver observer) {
    if (observersAttackAICapabilityUC.ContainsKey(id)) {
      var list = observersAttackAICapabilityUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersAttackAICapabilityUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastAttackAICapabilityUCEffect(int id, IAttackAICapabilityUCEffect effect) {
    if (observersAttackAICapabilityUC.ContainsKey(0)) {
      foreach (var observer in new List<IAttackAICapabilityUCEffectObserver>(observersAttackAICapabilityUC[0])) {
        observer.OnAttackAICapabilityUCEffect(effect);
      }
    }
    if (observersAttackAICapabilityUC.ContainsKey(id)) {
      foreach (var observer in new List<IAttackAICapabilityUCEffectObserver>(observersAttackAICapabilityUC[id])) {
        observer.OnAttackAICapabilityUCEffect(effect);
      }
    }
  }

  public AttackAICapabilityUC EffectAttackAICapabilityUCCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new AttackAICapabilityUCIncarnation(

            );
    EffectInternalCreateAttackAICapabilityUC(id, incarnation);
    return new AttackAICapabilityUC(this, id);
  }
  public void EffectInternalCreateAttackAICapabilityUC(
      int id,
      AttackAICapabilityUCIncarnation incarnation) {
    CheckUnlocked();
    var effect = new AttackAICapabilityUCCreateEffect(id, incarnation);
    rootIncarnation.incarnationsAttackAICapabilityUC.Add(
        id,
        new VersionAndIncarnation<AttackAICapabilityUCIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetAttackAICapabilityUCHash(id, rootIncarnation.version, incarnation);
    BroadcastAttackAICapabilityUCEffect(id, effect);
  }

  public void EffectAttackAICapabilityUCDelete(int id) {
    CheckUnlocked();
    var effect = new AttackAICapabilityUCDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsAttackAICapabilityUC[id];
    this.rootIncarnation.hash -=
        GetAttackAICapabilityUCHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastAttackAICapabilityUCEffect(id, effect);
    rootIncarnation.incarnationsAttackAICapabilityUC.Remove(id);
  }

     
  public int GetAttackAICapabilityUCHash(int id, int version, AttackAICapabilityUCIncarnation incarnation) {
    int result = id * version;
    return result;
  }
       public MoveImpulseIncarnation GetMoveImpulseIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsMoveImpulse[id].incarnation;
  }
  public bool MoveImpulseExists(int id) {
    return rootIncarnation.incarnationsMoveImpulse.ContainsKey(id);
  }
  public MoveImpulse GetMoveImpulse(int id) {
    return new MoveImpulse(this, id);
  }
  public List<MoveImpulse> AllMoveImpulse() {
    List<MoveImpulse> result = new List<MoveImpulse>(rootIncarnation.incarnationsMoveImpulse.Count);
    foreach (var id in rootIncarnation.incarnationsMoveImpulse.Keys) {
      result.Add(new MoveImpulse(this, id));
    }
    return result;
  }
  public IEnumerator<MoveImpulse> EnumAllMoveImpulse() {
    foreach (var id in rootIncarnation.incarnationsMoveImpulse.Keys) {
      yield return GetMoveImpulse(id);
    }
  }
  public void CheckHasMoveImpulse(MoveImpulse thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasMoveImpulse(thing.id);
  }
  public void CheckHasMoveImpulse(int id) {
    if (!rootIncarnation.incarnationsMoveImpulse.ContainsKey(id)) {
      throw new System.Exception("Invalid MoveImpulse!");
    }
  }
  public void AddMoveImpulseObserver(int id, IMoveImpulseEffectObserver observer) {
    List<IMoveImpulseEffectObserver> obsies;
    if (!observersMoveImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<IMoveImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersMoveImpulse[id] = obsies;
  }

  public void RemoveMoveImpulseObserver(int id, IMoveImpulseEffectObserver observer) {
    if (observersMoveImpulse.ContainsKey(id)) {
      var list = observersMoveImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersMoveImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastMoveImpulseEffect(int id, IMoveImpulseEffect effect) {
    if (observersMoveImpulse.ContainsKey(0)) {
      foreach (var observer in new List<IMoveImpulseEffectObserver>(observersMoveImpulse[0])) {
        observer.OnMoveImpulseEffect(effect);
      }
    }
    if (observersMoveImpulse.ContainsKey(id)) {
      foreach (var observer in new List<IMoveImpulseEffectObserver>(observersMoveImpulse[id])) {
        observer.OnMoveImpulseEffect(effect);
      }
    }
  }

  public MoveImpulse EffectMoveImpulseCreate(
      int weight,
      Location stepLocation) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new MoveImpulseIncarnation(
            weight,
            stepLocation
            );
    EffectInternalCreateMoveImpulse(id, incarnation);
    return new MoveImpulse(this, id);
  }
  public void EffectInternalCreateMoveImpulse(
      int id,
      MoveImpulseIncarnation incarnation) {
    CheckUnlocked();
    var effect = new MoveImpulseCreateEffect(id, incarnation);
    rootIncarnation.incarnationsMoveImpulse.Add(
        id,
        new VersionAndIncarnation<MoveImpulseIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetMoveImpulseHash(id, rootIncarnation.version, incarnation);
    BroadcastMoveImpulseEffect(id, effect);
  }

  public void EffectMoveImpulseDelete(int id) {
    CheckUnlocked();
    var effect = new MoveImpulseDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsMoveImpulse[id];
    this.rootIncarnation.hash -=
        GetMoveImpulseHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastMoveImpulseEffect(id, effect);
    rootIncarnation.incarnationsMoveImpulse.Remove(id);
  }

     
  public int GetMoveImpulseHash(int id, int version, MoveImpulseIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.weight.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.stepLocation.GetDeterministicHashCode();
    return result;
  }
       public MoveDirectiveUCIncarnation GetMoveDirectiveUCIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsMoveDirectiveUC[id].incarnation;
  }
  public bool MoveDirectiveUCExists(int id) {
    return rootIncarnation.incarnationsMoveDirectiveUC.ContainsKey(id);
  }
  public MoveDirectiveUC GetMoveDirectiveUC(int id) {
    return new MoveDirectiveUC(this, id);
  }
  public List<MoveDirectiveUC> AllMoveDirectiveUC() {
    List<MoveDirectiveUC> result = new List<MoveDirectiveUC>(rootIncarnation.incarnationsMoveDirectiveUC.Count);
    foreach (var id in rootIncarnation.incarnationsMoveDirectiveUC.Keys) {
      result.Add(new MoveDirectiveUC(this, id));
    }
    return result;
  }
  public IEnumerator<MoveDirectiveUC> EnumAllMoveDirectiveUC() {
    foreach (var id in rootIncarnation.incarnationsMoveDirectiveUC.Keys) {
      yield return GetMoveDirectiveUC(id);
    }
  }
  public void CheckHasMoveDirectiveUC(MoveDirectiveUC thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasMoveDirectiveUC(thing.id);
  }
  public void CheckHasMoveDirectiveUC(int id) {
    if (!rootIncarnation.incarnationsMoveDirectiveUC.ContainsKey(id)) {
      throw new System.Exception("Invalid MoveDirectiveUC!");
    }
  }
  public void AddMoveDirectiveUCObserver(int id, IMoveDirectiveUCEffectObserver observer) {
    List<IMoveDirectiveUCEffectObserver> obsies;
    if (!observersMoveDirectiveUC.TryGetValue(id, out obsies)) {
      obsies = new List<IMoveDirectiveUCEffectObserver>();
    }
    obsies.Add(observer);
    observersMoveDirectiveUC[id] = obsies;
  }

  public void RemoveMoveDirectiveUCObserver(int id, IMoveDirectiveUCEffectObserver observer) {
    if (observersMoveDirectiveUC.ContainsKey(id)) {
      var list = observersMoveDirectiveUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersMoveDirectiveUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastMoveDirectiveUCEffect(int id, IMoveDirectiveUCEffect effect) {
    if (observersMoveDirectiveUC.ContainsKey(0)) {
      foreach (var observer in new List<IMoveDirectiveUCEffectObserver>(observersMoveDirectiveUC[0])) {
        observer.OnMoveDirectiveUCEffect(effect);
      }
    }
    if (observersMoveDirectiveUC.ContainsKey(id)) {
      foreach (var observer in new List<IMoveDirectiveUCEffectObserver>(observersMoveDirectiveUC[id])) {
        observer.OnMoveDirectiveUCEffect(effect);
      }
    }
  }

  public MoveDirectiveUC EffectMoveDirectiveUCCreate(
      LocationMutList path) {
    CheckUnlocked();
    CheckHasLocationMutList(path);

    var id = NewId();
    var incarnation =
        new MoveDirectiveUCIncarnation(
            path.id
            );
    EffectInternalCreateMoveDirectiveUC(id, incarnation);
    return new MoveDirectiveUC(this, id);
  }
  public void EffectInternalCreateMoveDirectiveUC(
      int id,
      MoveDirectiveUCIncarnation incarnation) {
    CheckUnlocked();
    var effect = new MoveDirectiveUCCreateEffect(id, incarnation);
    rootIncarnation.incarnationsMoveDirectiveUC.Add(
        id,
        new VersionAndIncarnation<MoveDirectiveUCIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetMoveDirectiveUCHash(id, rootIncarnation.version, incarnation);
    BroadcastMoveDirectiveUCEffect(id, effect);
  }

  public void EffectMoveDirectiveUCDelete(int id) {
    CheckUnlocked();
    var effect = new MoveDirectiveUCDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsMoveDirectiveUC[id];
    this.rootIncarnation.hash -=
        GetMoveDirectiveUCHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastMoveDirectiveUCEffect(id, effect);
    rootIncarnation.incarnationsMoveDirectiveUC.Remove(id);
  }

     
  public int GetMoveDirectiveUCHash(int id, int version, MoveDirectiveUCIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.path.GetDeterministicHashCode();
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
    foreach (var id in rootIncarnation.incarnationsUnit.Keys) {
      result.Add(new Unit(this, id));
    }
    return result;
  }
  public IEnumerator<Unit> EnumAllUnit() {
    foreach (var id in rootIncarnation.incarnationsUnit.Keys) {
      yield return GetUnit(id);
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
      IUnitComponentMutBunch components,
      IItemMutBunch items) {
    CheckUnlocked();
    CheckHasIUnitEventMutList(events);
    CheckHasIUnitComponentMutBunch(components);
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
            components.id,
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
    result += id * version * 12 * incarnation.components.GetDeterministicHashCode();
    result += id * version * 13 * incarnation.items.GetDeterministicHashCode();
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
              oldIncarnationAndVersion.incarnation.components,
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
              oldIncarnationAndVersion.incarnation.components,
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
              oldIncarnationAndVersion.incarnation.components,
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
              oldIncarnationAndVersion.incarnation.components,
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
              oldIncarnationAndVersion.incarnation.components,
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
              oldIncarnationAndVersion.incarnation.components,
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
  public IItemMutBunchIncarnation GetIItemMutBunchIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsIItemMutBunch[id].incarnation;
  }
  public bool IItemMutBunchExists(int id) {
    return rootIncarnation.incarnationsIItemMutBunch.ContainsKey(id);
  }
  public IItemMutBunch GetIItemMutBunch(int id) {
    return new IItemMutBunch(this, id);
  }
  public List<IItemMutBunch> AllIItemMutBunch() {
    List<IItemMutBunch> result = new List<IItemMutBunch>(rootIncarnation.incarnationsIItemMutBunch.Count);
    foreach (var id in rootIncarnation.incarnationsIItemMutBunch.Keys) {
      result.Add(new IItemMutBunch(this, id));
    }
    return result;
  }
  public IEnumerator<IItemMutBunch> EnumAllIItemMutBunch() {
    foreach (var id in rootIncarnation.incarnationsIItemMutBunch.Keys) {
      yield return GetIItemMutBunch(id);
    }
  }
  public void CheckHasIItemMutBunch(IItemMutBunch thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasIItemMutBunch(thing.id);
  }
  public void CheckHasIItemMutBunch(int id) {
    if (!rootIncarnation.incarnationsIItemMutBunch.ContainsKey(id)) {
      throw new System.Exception("Invalid IItemMutBunch!");
    }
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

  public IItemMutBunch EffectIItemMutBunchCreate(
      GlaiveMutSet membersGlaiveMutSet,
      ArmorMutSet membersArmorMutSet) {
    CheckUnlocked();
    CheckHasGlaiveMutSet(membersGlaiveMutSet);
    CheckHasArmorMutSet(membersArmorMutSet);

    var id = NewId();
    var incarnation =
        new IItemMutBunchIncarnation(
            membersGlaiveMutSet.id,
            membersArmorMutSet.id
            );
    EffectInternalCreateIItemMutBunch(id, incarnation);
    return new IItemMutBunch(this, id);
  }
  public void EffectInternalCreateIItemMutBunch(
      int id,
      IItemMutBunchIncarnation incarnation) {
    CheckUnlocked();
    var effect = new IItemMutBunchCreateEffect(id, incarnation);
    rootIncarnation.incarnationsIItemMutBunch.Add(
        id,
        new VersionAndIncarnation<IItemMutBunchIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetIItemMutBunchHash(id, rootIncarnation.version, incarnation);
    BroadcastIItemMutBunchEffect(id, effect);
  }

  public void EffectIItemMutBunchDelete(int id) {
    CheckUnlocked();
    var effect = new IItemMutBunchDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsIItemMutBunch[id];
    this.rootIncarnation.hash -=
        GetIItemMutBunchHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastIItemMutBunchEffect(id, effect);
    rootIncarnation.incarnationsIItemMutBunch.Remove(id);
  }

     
  public int GetIItemMutBunchHash(int id, int version, IItemMutBunchIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.membersGlaiveMutSet.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.membersArmorMutSet.GetDeterministicHashCode();
    return result;
  }
       public IUnitComponentMutBunchIncarnation GetIUnitComponentMutBunchIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsIUnitComponentMutBunch[id].incarnation;
  }
  public bool IUnitComponentMutBunchExists(int id) {
    return rootIncarnation.incarnationsIUnitComponentMutBunch.ContainsKey(id);
  }
  public IUnitComponentMutBunch GetIUnitComponentMutBunch(int id) {
    return new IUnitComponentMutBunch(this, id);
  }
  public List<IUnitComponentMutBunch> AllIUnitComponentMutBunch() {
    List<IUnitComponentMutBunch> result = new List<IUnitComponentMutBunch>(rootIncarnation.incarnationsIUnitComponentMutBunch.Count);
    foreach (var id in rootIncarnation.incarnationsIUnitComponentMutBunch.Keys) {
      result.Add(new IUnitComponentMutBunch(this, id));
    }
    return result;
  }
  public IEnumerator<IUnitComponentMutBunch> EnumAllIUnitComponentMutBunch() {
    foreach (var id in rootIncarnation.incarnationsIUnitComponentMutBunch.Keys) {
      yield return GetIUnitComponentMutBunch(id);
    }
  }
  public void CheckHasIUnitComponentMutBunch(IUnitComponentMutBunch thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasIUnitComponentMutBunch(thing.id);
  }
  public void CheckHasIUnitComponentMutBunch(int id) {
    if (!rootIncarnation.incarnationsIUnitComponentMutBunch.ContainsKey(id)) {
      throw new System.Exception("Invalid IUnitComponentMutBunch!");
    }
  }
  public void AddIUnitComponentMutBunchObserver(int id, IIUnitComponentMutBunchEffectObserver observer) {
    List<IIUnitComponentMutBunchEffectObserver> obsies;
    if (!observersIUnitComponentMutBunch.TryGetValue(id, out obsies)) {
      obsies = new List<IIUnitComponentMutBunchEffectObserver>();
    }
    obsies.Add(observer);
    observersIUnitComponentMutBunch[id] = obsies;
  }

  public void RemoveIUnitComponentMutBunchObserver(int id, IIUnitComponentMutBunchEffectObserver observer) {
    if (observersIUnitComponentMutBunch.ContainsKey(id)) {
      var list = observersIUnitComponentMutBunch[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersIUnitComponentMutBunch.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastIUnitComponentMutBunchEffect(int id, IIUnitComponentMutBunchEffect effect) {
    if (observersIUnitComponentMutBunch.ContainsKey(0)) {
      foreach (var observer in new List<IIUnitComponentMutBunchEffectObserver>(observersIUnitComponentMutBunch[0])) {
        observer.OnIUnitComponentMutBunchEffect(effect);
      }
    }
    if (observersIUnitComponentMutBunch.ContainsKey(id)) {
      foreach (var observer in new List<IIUnitComponentMutBunchEffectObserver>(observersIUnitComponentMutBunch[id])) {
        observer.OnIUnitComponentMutBunchEffect(effect);
      }
    }
  }

  public IUnitComponentMutBunch EffectIUnitComponentMutBunchCreate(
      KillDirectiveUCMutSet membersKillDirectiveUCMutSet,
      MoveDirectiveUCMutSet membersMoveDirectiveUCMutSet,
      WanderAICapabilityUCMutSet membersWanderAICapabilityUCMutSet,
      BideAICapabilityUCMutSet membersBideAICapabilityUCMutSet,
      AttackAICapabilityUCMutSet membersAttackAICapabilityUCMutSet,
      ShieldingUCMutSet membersShieldingUCMutSet,
      BidingOperationUCMutSet membersBidingOperationUCMutSet) {
    CheckUnlocked();
    CheckHasKillDirectiveUCMutSet(membersKillDirectiveUCMutSet);
    CheckHasMoveDirectiveUCMutSet(membersMoveDirectiveUCMutSet);
    CheckHasWanderAICapabilityUCMutSet(membersWanderAICapabilityUCMutSet);
    CheckHasBideAICapabilityUCMutSet(membersBideAICapabilityUCMutSet);
    CheckHasAttackAICapabilityUCMutSet(membersAttackAICapabilityUCMutSet);
    CheckHasShieldingUCMutSet(membersShieldingUCMutSet);
    CheckHasBidingOperationUCMutSet(membersBidingOperationUCMutSet);

    var id = NewId();
    var incarnation =
        new IUnitComponentMutBunchIncarnation(
            membersKillDirectiveUCMutSet.id,
            membersMoveDirectiveUCMutSet.id,
            membersWanderAICapabilityUCMutSet.id,
            membersBideAICapabilityUCMutSet.id,
            membersAttackAICapabilityUCMutSet.id,
            membersShieldingUCMutSet.id,
            membersBidingOperationUCMutSet.id
            );
    EffectInternalCreateIUnitComponentMutBunch(id, incarnation);
    return new IUnitComponentMutBunch(this, id);
  }
  public void EffectInternalCreateIUnitComponentMutBunch(
      int id,
      IUnitComponentMutBunchIncarnation incarnation) {
    CheckUnlocked();
    var effect = new IUnitComponentMutBunchCreateEffect(id, incarnation);
    rootIncarnation.incarnationsIUnitComponentMutBunch.Add(
        id,
        new VersionAndIncarnation<IUnitComponentMutBunchIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetIUnitComponentMutBunchHash(id, rootIncarnation.version, incarnation);
    BroadcastIUnitComponentMutBunchEffect(id, effect);
  }

  public void EffectIUnitComponentMutBunchDelete(int id) {
    CheckUnlocked();
    var effect = new IUnitComponentMutBunchDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsIUnitComponentMutBunch[id];
    this.rootIncarnation.hash -=
        GetIUnitComponentMutBunchHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastIUnitComponentMutBunchEffect(id, effect);
    rootIncarnation.incarnationsIUnitComponentMutBunch.Remove(id);
  }

     
  public int GetIUnitComponentMutBunchHash(int id, int version, IUnitComponentMutBunchIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.membersKillDirectiveUCMutSet.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.membersMoveDirectiveUCMutSet.GetDeterministicHashCode();
    result += id * version * 3 * incarnation.membersWanderAICapabilityUCMutSet.GetDeterministicHashCode();
    result += id * version * 4 * incarnation.membersBideAICapabilityUCMutSet.GetDeterministicHashCode();
    result += id * version * 5 * incarnation.membersAttackAICapabilityUCMutSet.GetDeterministicHashCode();
    result += id * version * 6 * incarnation.membersShieldingUCMutSet.GetDeterministicHashCode();
    result += id * version * 7 * incarnation.membersBidingOperationUCMutSet.GetDeterministicHashCode();
    return result;
  }
       public NoImpulseIncarnation GetNoImpulseIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsNoImpulse[id].incarnation;
  }
  public bool NoImpulseExists(int id) {
    return rootIncarnation.incarnationsNoImpulse.ContainsKey(id);
  }
  public NoImpulse GetNoImpulse(int id) {
    return new NoImpulse(this, id);
  }
  public List<NoImpulse> AllNoImpulse() {
    List<NoImpulse> result = new List<NoImpulse>(rootIncarnation.incarnationsNoImpulse.Count);
    foreach (var id in rootIncarnation.incarnationsNoImpulse.Keys) {
      result.Add(new NoImpulse(this, id));
    }
    return result;
  }
  public IEnumerator<NoImpulse> EnumAllNoImpulse() {
    foreach (var id in rootIncarnation.incarnationsNoImpulse.Keys) {
      yield return GetNoImpulse(id);
    }
  }
  public void CheckHasNoImpulse(NoImpulse thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasNoImpulse(thing.id);
  }
  public void CheckHasNoImpulse(int id) {
    if (!rootIncarnation.incarnationsNoImpulse.ContainsKey(id)) {
      throw new System.Exception("Invalid NoImpulse!");
    }
  }
  public void AddNoImpulseObserver(int id, INoImpulseEffectObserver observer) {
    List<INoImpulseEffectObserver> obsies;
    if (!observersNoImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<INoImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersNoImpulse[id] = obsies;
  }

  public void RemoveNoImpulseObserver(int id, INoImpulseEffectObserver observer) {
    if (observersNoImpulse.ContainsKey(id)) {
      var list = observersNoImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersNoImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastNoImpulseEffect(int id, INoImpulseEffect effect) {
    if (observersNoImpulse.ContainsKey(0)) {
      foreach (var observer in new List<INoImpulseEffectObserver>(observersNoImpulse[0])) {
        observer.OnNoImpulseEffect(effect);
      }
    }
    if (observersNoImpulse.ContainsKey(id)) {
      foreach (var observer in new List<INoImpulseEffectObserver>(observersNoImpulse[id])) {
        observer.OnNoImpulseEffect(effect);
      }
    }
  }

  public NoImpulse EffectNoImpulseCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new NoImpulseIncarnation(

            );
    EffectInternalCreateNoImpulse(id, incarnation);
    return new NoImpulse(this, id);
  }
  public void EffectInternalCreateNoImpulse(
      int id,
      NoImpulseIncarnation incarnation) {
    CheckUnlocked();
    var effect = new NoImpulseCreateEffect(id, incarnation);
    rootIncarnation.incarnationsNoImpulse.Add(
        id,
        new VersionAndIncarnation<NoImpulseIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetNoImpulseHash(id, rootIncarnation.version, incarnation);
    BroadcastNoImpulseEffect(id, effect);
  }

  public void EffectNoImpulseDelete(int id) {
    CheckUnlocked();
    var effect = new NoImpulseDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsNoImpulse[id];
    this.rootIncarnation.hash -=
        GetNoImpulseHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastNoImpulseEffect(id, effect);
    rootIncarnation.incarnationsNoImpulse.Remove(id);
  }

     
  public int GetNoImpulseHash(int id, int version, NoImpulseIncarnation incarnation) {
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
    foreach (var id in rootIncarnation.incarnationsExecutionState.Keys) {
      result.Add(new ExecutionState(this, id));
    }
    return result;
  }
  public IEnumerator<ExecutionState> EnumAllExecutionState() {
    foreach (var id in rootIncarnation.incarnationsExecutionState.Keys) {
      yield return GetExecutionState(id);
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
      IPreActingUCWeakMutBunch remainingPreActingUnitComponents,
      IPostActingUCWeakMutBunch remainingPostActingUnitComponents) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new ExecutionStateIncarnation(
            actingUnit.id,
            actingUnitDidAction,
            remainingPreActingUnitComponents.id,
            remainingPostActingUnitComponents.id
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
    result += id * version * 3 * incarnation.remainingPreActingUnitComponents.GetDeterministicHashCode();
    result += id * version * 4 * incarnation.remainingPostActingUnitComponents.GetDeterministicHashCode();
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
              oldIncarnationAndVersion.incarnation.remainingPreActingUnitComponents,
              oldIncarnationAndVersion.incarnation.remainingPostActingUnitComponents);
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
              oldIncarnationAndVersion.incarnation.remainingPreActingUnitComponents,
              oldIncarnationAndVersion.incarnation.remainingPostActingUnitComponents);
      rootIncarnation.incarnationsExecutionState[id] =
          new VersionAndIncarnation<ExecutionStateIncarnation>(
              rootIncarnation.version,
              newIncarnation);
      this.rootIncarnation.hash -= GetExecutionStateHash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
      this.rootIncarnation.hash += GetExecutionStateHash(id, rootIncarnation.version, newIncarnation);
    }

    BroadcastExecutionStateEffect(id, effect);
  }

  public void EffectExecutionStateSetRemainingPreActingUnitComponents(int id, IPreActingUCWeakMutBunch newValue) {
    CheckUnlocked();
    CheckHasExecutionState(id);
    var effect = new ExecutionStateSetRemainingPreActingUnitComponentsEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsExecutionState[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldId = oldIncarnationAndVersion.incarnation.remainingPreActingUnitComponents;
      oldIncarnationAndVersion.incarnation.remainingPreActingUnitComponents = newValue.id;
      this.rootIncarnation.hash -= id * rootIncarnation.version * 3 * oldId.GetDeterministicHashCode();
      this.rootIncarnation.hash += id * rootIncarnation.version * 3 * newValue.id.GetDeterministicHashCode();
           
    } else {
      var newIncarnation =
          new ExecutionStateIncarnation(
              oldIncarnationAndVersion.incarnation.actingUnit,
              oldIncarnationAndVersion.incarnation.actingUnitDidAction,
              newValue.id,
              oldIncarnationAndVersion.incarnation.remainingPostActingUnitComponents);
      rootIncarnation.incarnationsExecutionState[id] =
          new VersionAndIncarnation<ExecutionStateIncarnation>(
              rootIncarnation.version,
              newIncarnation);
      this.rootIncarnation.hash -= GetExecutionStateHash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
      this.rootIncarnation.hash += GetExecutionStateHash(id, rootIncarnation.version, newIncarnation);
    }

    BroadcastExecutionStateEffect(id, effect);
  }

  public void EffectExecutionStateSetRemainingPostActingUnitComponents(int id, IPostActingUCWeakMutBunch newValue) {
    CheckUnlocked();
    CheckHasExecutionState(id);
    var effect = new ExecutionStateSetRemainingPostActingUnitComponentsEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsExecutionState[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldId = oldIncarnationAndVersion.incarnation.remainingPostActingUnitComponents;
      oldIncarnationAndVersion.incarnation.remainingPostActingUnitComponents = newValue.id;
      this.rootIncarnation.hash -= id * rootIncarnation.version * 4 * oldId.GetDeterministicHashCode();
      this.rootIncarnation.hash += id * rootIncarnation.version * 4 * newValue.id.GetDeterministicHashCode();
           
    } else {
      var newIncarnation =
          new ExecutionStateIncarnation(
              oldIncarnationAndVersion.incarnation.actingUnit,
              oldIncarnationAndVersion.incarnation.actingUnitDidAction,
              oldIncarnationAndVersion.incarnation.remainingPreActingUnitComponents,
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
  public IPostActingUCWeakMutBunchIncarnation GetIPostActingUCWeakMutBunchIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsIPostActingUCWeakMutBunch[id].incarnation;
  }
  public bool IPostActingUCWeakMutBunchExists(int id) {
    return rootIncarnation.incarnationsIPostActingUCWeakMutBunch.ContainsKey(id);
  }
  public IPostActingUCWeakMutBunch GetIPostActingUCWeakMutBunch(int id) {
    return new IPostActingUCWeakMutBunch(this, id);
  }
  public List<IPostActingUCWeakMutBunch> AllIPostActingUCWeakMutBunch() {
    List<IPostActingUCWeakMutBunch> result = new List<IPostActingUCWeakMutBunch>(rootIncarnation.incarnationsIPostActingUCWeakMutBunch.Count);
    foreach (var id in rootIncarnation.incarnationsIPostActingUCWeakMutBunch.Keys) {
      result.Add(new IPostActingUCWeakMutBunch(this, id));
    }
    return result;
  }
  public IEnumerator<IPostActingUCWeakMutBunch> EnumAllIPostActingUCWeakMutBunch() {
    foreach (var id in rootIncarnation.incarnationsIPostActingUCWeakMutBunch.Keys) {
      yield return GetIPostActingUCWeakMutBunch(id);
    }
  }
  public void CheckHasIPostActingUCWeakMutBunch(IPostActingUCWeakMutBunch thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasIPostActingUCWeakMutBunch(thing.id);
  }
  public void CheckHasIPostActingUCWeakMutBunch(int id) {
    if (!rootIncarnation.incarnationsIPostActingUCWeakMutBunch.ContainsKey(id)) {
      throw new System.Exception("Invalid IPostActingUCWeakMutBunch!");
    }
  }
  public void AddIPostActingUCWeakMutBunchObserver(int id, IIPostActingUCWeakMutBunchEffectObserver observer) {
    List<IIPostActingUCWeakMutBunchEffectObserver> obsies;
    if (!observersIPostActingUCWeakMutBunch.TryGetValue(id, out obsies)) {
      obsies = new List<IIPostActingUCWeakMutBunchEffectObserver>();
    }
    obsies.Add(observer);
    observersIPostActingUCWeakMutBunch[id] = obsies;
  }

  public void RemoveIPostActingUCWeakMutBunchObserver(int id, IIPostActingUCWeakMutBunchEffectObserver observer) {
    if (observersIPostActingUCWeakMutBunch.ContainsKey(id)) {
      var list = observersIPostActingUCWeakMutBunch[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersIPostActingUCWeakMutBunch.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastIPostActingUCWeakMutBunchEffect(int id, IIPostActingUCWeakMutBunchEffect effect) {
    if (observersIPostActingUCWeakMutBunch.ContainsKey(0)) {
      foreach (var observer in new List<IIPostActingUCWeakMutBunchEffectObserver>(observersIPostActingUCWeakMutBunch[0])) {
        observer.OnIPostActingUCWeakMutBunchEffect(effect);
      }
    }
    if (observersIPostActingUCWeakMutBunch.ContainsKey(id)) {
      foreach (var observer in new List<IIPostActingUCWeakMutBunchEffectObserver>(observersIPostActingUCWeakMutBunch[id])) {
        observer.OnIPostActingUCWeakMutBunchEffect(effect);
      }
    }
  }

  public IPostActingUCWeakMutBunch EffectIPostActingUCWeakMutBunchCreate(
      ShieldingUCWeakMutSet membersShieldingUCWeakMutSet) {
    CheckUnlocked();
    CheckHasShieldingUCWeakMutSet(membersShieldingUCWeakMutSet);

    var id = NewId();
    var incarnation =
        new IPostActingUCWeakMutBunchIncarnation(
            membersShieldingUCWeakMutSet.id
            );
    EffectInternalCreateIPostActingUCWeakMutBunch(id, incarnation);
    return new IPostActingUCWeakMutBunch(this, id);
  }
  public void EffectInternalCreateIPostActingUCWeakMutBunch(
      int id,
      IPostActingUCWeakMutBunchIncarnation incarnation) {
    CheckUnlocked();
    var effect = new IPostActingUCWeakMutBunchCreateEffect(id, incarnation);
    rootIncarnation.incarnationsIPostActingUCWeakMutBunch.Add(
        id,
        new VersionAndIncarnation<IPostActingUCWeakMutBunchIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetIPostActingUCWeakMutBunchHash(id, rootIncarnation.version, incarnation);
    BroadcastIPostActingUCWeakMutBunchEffect(id, effect);
  }

  public void EffectIPostActingUCWeakMutBunchDelete(int id) {
    CheckUnlocked();
    var effect = new IPostActingUCWeakMutBunchDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsIPostActingUCWeakMutBunch[id];
    this.rootIncarnation.hash -=
        GetIPostActingUCWeakMutBunchHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastIPostActingUCWeakMutBunchEffect(id, effect);
    rootIncarnation.incarnationsIPostActingUCWeakMutBunch.Remove(id);
  }

     
  public int GetIPostActingUCWeakMutBunchHash(int id, int version, IPostActingUCWeakMutBunchIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.membersShieldingUCWeakMutSet.GetDeterministicHashCode();
    return result;
  }
       public IPreActingUCWeakMutBunchIncarnation GetIPreActingUCWeakMutBunchIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsIPreActingUCWeakMutBunch[id].incarnation;
  }
  public bool IPreActingUCWeakMutBunchExists(int id) {
    return rootIncarnation.incarnationsIPreActingUCWeakMutBunch.ContainsKey(id);
  }
  public IPreActingUCWeakMutBunch GetIPreActingUCWeakMutBunch(int id) {
    return new IPreActingUCWeakMutBunch(this, id);
  }
  public List<IPreActingUCWeakMutBunch> AllIPreActingUCWeakMutBunch() {
    List<IPreActingUCWeakMutBunch> result = new List<IPreActingUCWeakMutBunch>(rootIncarnation.incarnationsIPreActingUCWeakMutBunch.Count);
    foreach (var id in rootIncarnation.incarnationsIPreActingUCWeakMutBunch.Keys) {
      result.Add(new IPreActingUCWeakMutBunch(this, id));
    }
    return result;
  }
  public IEnumerator<IPreActingUCWeakMutBunch> EnumAllIPreActingUCWeakMutBunch() {
    foreach (var id in rootIncarnation.incarnationsIPreActingUCWeakMutBunch.Keys) {
      yield return GetIPreActingUCWeakMutBunch(id);
    }
  }
  public void CheckHasIPreActingUCWeakMutBunch(IPreActingUCWeakMutBunch thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasIPreActingUCWeakMutBunch(thing.id);
  }
  public void CheckHasIPreActingUCWeakMutBunch(int id) {
    if (!rootIncarnation.incarnationsIPreActingUCWeakMutBunch.ContainsKey(id)) {
      throw new System.Exception("Invalid IPreActingUCWeakMutBunch!");
    }
  }
  public void AddIPreActingUCWeakMutBunchObserver(int id, IIPreActingUCWeakMutBunchEffectObserver observer) {
    List<IIPreActingUCWeakMutBunchEffectObserver> obsies;
    if (!observersIPreActingUCWeakMutBunch.TryGetValue(id, out obsies)) {
      obsies = new List<IIPreActingUCWeakMutBunchEffectObserver>();
    }
    obsies.Add(observer);
    observersIPreActingUCWeakMutBunch[id] = obsies;
  }

  public void RemoveIPreActingUCWeakMutBunchObserver(int id, IIPreActingUCWeakMutBunchEffectObserver observer) {
    if (observersIPreActingUCWeakMutBunch.ContainsKey(id)) {
      var list = observersIPreActingUCWeakMutBunch[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersIPreActingUCWeakMutBunch.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void BroadcastIPreActingUCWeakMutBunchEffect(int id, IIPreActingUCWeakMutBunchEffect effect) {
    if (observersIPreActingUCWeakMutBunch.ContainsKey(0)) {
      foreach (var observer in new List<IIPreActingUCWeakMutBunchEffectObserver>(observersIPreActingUCWeakMutBunch[0])) {
        observer.OnIPreActingUCWeakMutBunchEffect(effect);
      }
    }
    if (observersIPreActingUCWeakMutBunch.ContainsKey(id)) {
      foreach (var observer in new List<IIPreActingUCWeakMutBunchEffectObserver>(observersIPreActingUCWeakMutBunch[id])) {
        observer.OnIPreActingUCWeakMutBunchEffect(effect);
      }
    }
  }

  public IPreActingUCWeakMutBunch EffectIPreActingUCWeakMutBunchCreate(
      ShieldingUCWeakMutSet membersShieldingUCWeakMutSet,
      AttackAICapabilityUCWeakMutSet membersAttackAICapabilityUCWeakMutSet) {
    CheckUnlocked();
    CheckHasShieldingUCWeakMutSet(membersShieldingUCWeakMutSet);
    CheckHasAttackAICapabilityUCWeakMutSet(membersAttackAICapabilityUCWeakMutSet);

    var id = NewId();
    var incarnation =
        new IPreActingUCWeakMutBunchIncarnation(
            membersShieldingUCWeakMutSet.id,
            membersAttackAICapabilityUCWeakMutSet.id
            );
    EffectInternalCreateIPreActingUCWeakMutBunch(id, incarnation);
    return new IPreActingUCWeakMutBunch(this, id);
  }
  public void EffectInternalCreateIPreActingUCWeakMutBunch(
      int id,
      IPreActingUCWeakMutBunchIncarnation incarnation) {
    CheckUnlocked();
    var effect = new IPreActingUCWeakMutBunchCreateEffect(id, incarnation);
    rootIncarnation.incarnationsIPreActingUCWeakMutBunch.Add(
        id,
        new VersionAndIncarnation<IPreActingUCWeakMutBunchIncarnation>(
            rootIncarnation.version,
            incarnation));
    this.rootIncarnation.hash += GetIPreActingUCWeakMutBunchHash(id, rootIncarnation.version, incarnation);
    BroadcastIPreActingUCWeakMutBunchEffect(id, effect);
  }

  public void EffectIPreActingUCWeakMutBunchDelete(int id) {
    CheckUnlocked();
    var effect = new IPreActingUCWeakMutBunchDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsIPreActingUCWeakMutBunch[id];
    this.rootIncarnation.hash -=
        GetIPreActingUCWeakMutBunchHash(
            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);

    BroadcastIPreActingUCWeakMutBunchEffect(id, effect);
    rootIncarnation.incarnationsIPreActingUCWeakMutBunch.Remove(id);
  }

     
  public int GetIPreActingUCWeakMutBunchHash(int id, int version, IPreActingUCWeakMutBunchIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.membersShieldingUCWeakMutSet.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.membersAttackAICapabilityUCWeakMutSet.GetDeterministicHashCode();
    return result;
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
    foreach (var id in rootIncarnation.incarnationsGame.Keys) {
      result.Add(new Game(this, id));
    }
    return result;
  }
  public IEnumerator<Game> EnumAllGame() {
    foreach (var id in rootIncarnation.incarnationsGame.Keys) {
      yield return GetGame(id);
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
      LevelMutSet levels,
      Unit player,
      Level level,
      int time,
      ExecutionState executionState) {
    CheckUnlocked();
    CheckHasRand(rand);
    CheckHasLevelMutSet(levels);
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

  public ITerrainTileComponent GetITerrainTileComponent(int id) {
    if (rootIncarnation.incarnationsDecorativeTerrainTileComponent.ContainsKey(id)) {
      return new DecorativeTerrainTileComponentAsITerrainTileComponent(new DecorativeTerrainTileComponent(this, id));
    }
    if (rootIncarnation.incarnationsUpStaircaseTerrainTileComponent.ContainsKey(id)) {
      return new UpStaircaseTerrainTileComponentAsITerrainTileComponent(new UpStaircaseTerrainTileComponent(this, id));
    }
    if (rootIncarnation.incarnationsDownStaircaseTerrainTileComponent.ContainsKey(id)) {
      return new DownStaircaseTerrainTileComponentAsITerrainTileComponent(new DownStaircaseTerrainTileComponent(this, id));
    }
    throw new Exception("Unknown ITerrainTileComponent: " + id);
  }
  public ITerrainTileComponent GetITerrainTileComponentOrNull(int id) {
    if (rootIncarnation.incarnationsDecorativeTerrainTileComponent.ContainsKey(id)) {
      return new DecorativeTerrainTileComponentAsITerrainTileComponent(new DecorativeTerrainTileComponent(this, id));
    }
    if (rootIncarnation.incarnationsUpStaircaseTerrainTileComponent.ContainsKey(id)) {
      return new UpStaircaseTerrainTileComponentAsITerrainTileComponent(new UpStaircaseTerrainTileComponent(this, id));
    }
    if (rootIncarnation.incarnationsDownStaircaseTerrainTileComponent.ContainsKey(id)) {
      return new DownStaircaseTerrainTileComponentAsITerrainTileComponent(new DownStaircaseTerrainTileComponent(this, id));
    }
    return NullITerrainTileComponent.Null;
  }
  public void CheckHasITerrainTileComponent(ITerrainTileComponent thing) {
    GetITerrainTileComponent(thing.id);
  }
  public void CheckHasITerrainTileComponent(int id) {
    GetITerrainTileComponent(id);
  }

  public IItem GetIItem(int id) {
    if (rootIncarnation.incarnationsGlaive.ContainsKey(id)) {
      return new GlaiveAsIItem(new Glaive(this, id));
    }
    if (rootIncarnation.incarnationsArmor.ContainsKey(id)) {
      return new ArmorAsIItem(new Armor(this, id));
    }
    throw new Exception("Unknown IItem: " + id);
  }
  public IItem GetIItemOrNull(int id) {
    if (rootIncarnation.incarnationsGlaive.ContainsKey(id)) {
      return new GlaiveAsIItem(new Glaive(this, id));
    }
    if (rootIncarnation.incarnationsArmor.ContainsKey(id)) {
      return new ArmorAsIItem(new Armor(this, id));
    }
    return NullIItem.Null;
  }
  public void CheckHasIItem(IItem thing) {
    GetIItem(thing.id);
  }
  public void CheckHasIItem(int id) {
    GetIItem(id);
  }

  public IOperationUC GetIOperationUC(int id) {
    if (rootIncarnation.incarnationsBidingOperationUC.ContainsKey(id)) {
      return new BidingOperationUCAsIOperationUC(new BidingOperationUC(this, id));
    }
    throw new Exception("Unknown IOperationUC: " + id);
  }
  public IOperationUC GetIOperationUCOrNull(int id) {
    if (rootIncarnation.incarnationsBidingOperationUC.ContainsKey(id)) {
      return new BidingOperationUCAsIOperationUC(new BidingOperationUC(this, id));
    }
    return NullIOperationUC.Null;
  }
  public void CheckHasIOperationUC(IOperationUC thing) {
    GetIOperationUC(thing.id);
  }
  public void CheckHasIOperationUC(int id) {
    GetIOperationUC(id);
  }

  public IDirectiveUC GetIDirectiveUC(int id) {
    if (rootIncarnation.incarnationsKillDirectiveUC.ContainsKey(id)) {
      return new KillDirectiveUCAsIDirectiveUC(new KillDirectiveUC(this, id));
    }
    if (rootIncarnation.incarnationsMoveDirectiveUC.ContainsKey(id)) {
      return new MoveDirectiveUCAsIDirectiveUC(new MoveDirectiveUC(this, id));
    }
    throw new Exception("Unknown IDirectiveUC: " + id);
  }
  public IDirectiveUC GetIDirectiveUCOrNull(int id) {
    if (rootIncarnation.incarnationsKillDirectiveUC.ContainsKey(id)) {
      return new KillDirectiveUCAsIDirectiveUC(new KillDirectiveUC(this, id));
    }
    if (rootIncarnation.incarnationsMoveDirectiveUC.ContainsKey(id)) {
      return new MoveDirectiveUCAsIDirectiveUC(new MoveDirectiveUC(this, id));
    }
    return NullIDirectiveUC.Null;
  }
  public void CheckHasIDirectiveUC(IDirectiveUC thing) {
    GetIDirectiveUC(thing.id);
  }
  public void CheckHasIDirectiveUC(int id) {
    GetIDirectiveUC(id);
  }

  public IAICapabilityUC GetIAICapabilityUC(int id) {
    if (rootIncarnation.incarnationsWanderAICapabilityUC.ContainsKey(id)) {
      return new WanderAICapabilityUCAsIAICapabilityUC(new WanderAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsBideAICapabilityUC.ContainsKey(id)) {
      return new BideAICapabilityUCAsIAICapabilityUC(new BideAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsAttackAICapabilityUC.ContainsKey(id)) {
      return new AttackAICapabilityUCAsIAICapabilityUC(new AttackAICapabilityUC(this, id));
    }
    throw new Exception("Unknown IAICapabilityUC: " + id);
  }
  public IAICapabilityUC GetIAICapabilityUCOrNull(int id) {
    if (rootIncarnation.incarnationsWanderAICapabilityUC.ContainsKey(id)) {
      return new WanderAICapabilityUCAsIAICapabilityUC(new WanderAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsBideAICapabilityUC.ContainsKey(id)) {
      return new BideAICapabilityUCAsIAICapabilityUC(new BideAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsAttackAICapabilityUC.ContainsKey(id)) {
      return new AttackAICapabilityUCAsIAICapabilityUC(new AttackAICapabilityUC(this, id));
    }
    return NullIAICapabilityUC.Null;
  }
  public void CheckHasIAICapabilityUC(IAICapabilityUC thing) {
    GetIAICapabilityUC(thing.id);
  }
  public void CheckHasIAICapabilityUC(int id) {
    GetIAICapabilityUC(id);
  }

  public IPostActingUC GetIPostActingUC(int id) {
    if (rootIncarnation.incarnationsShieldingUC.ContainsKey(id)) {
      return new ShieldingUCAsIPostActingUC(new ShieldingUC(this, id));
    }
    throw new Exception("Unknown IPostActingUC: " + id);
  }
  public IPostActingUC GetIPostActingUCOrNull(int id) {
    if (rootIncarnation.incarnationsShieldingUC.ContainsKey(id)) {
      return new ShieldingUCAsIPostActingUC(new ShieldingUC(this, id));
    }
    return NullIPostActingUC.Null;
  }
  public void CheckHasIPostActingUC(IPostActingUC thing) {
    GetIPostActingUC(thing.id);
  }
  public void CheckHasIPostActingUC(int id) {
    GetIPostActingUC(id);
  }

  public IPreActingUC GetIPreActingUC(int id) {
    if (rootIncarnation.incarnationsShieldingUC.ContainsKey(id)) {
      return new ShieldingUCAsIPreActingUC(new ShieldingUC(this, id));
    }
    if (rootIncarnation.incarnationsAttackAICapabilityUC.ContainsKey(id)) {
      return new AttackAICapabilityUCAsIPreActingUC(new AttackAICapabilityUC(this, id));
    }
    throw new Exception("Unknown IPreActingUC: " + id);
  }
  public IPreActingUC GetIPreActingUCOrNull(int id) {
    if (rootIncarnation.incarnationsShieldingUC.ContainsKey(id)) {
      return new ShieldingUCAsIPreActingUC(new ShieldingUC(this, id));
    }
    if (rootIncarnation.incarnationsAttackAICapabilityUC.ContainsKey(id)) {
      return new AttackAICapabilityUCAsIPreActingUC(new AttackAICapabilityUC(this, id));
    }
    return NullIPreActingUC.Null;
  }
  public void CheckHasIPreActingUC(IPreActingUC thing) {
    GetIPreActingUC(thing.id);
  }
  public void CheckHasIPreActingUC(int id) {
    GetIPreActingUC(id);
  }

  public IDefenseUC GetIDefenseUC(int id) {
    if (rootIncarnation.incarnationsShieldingUC.ContainsKey(id)) {
      return new ShieldingUCAsIDefenseUC(new ShieldingUC(this, id));
    }
    if (rootIncarnation.incarnationsBidingOperationUC.ContainsKey(id)) {
      return new BidingOperationUCAsIDefenseUC(new BidingOperationUC(this, id));
    }
    throw new Exception("Unknown IDefenseUC: " + id);
  }
  public IDefenseUC GetIDefenseUCOrNull(int id) {
    if (rootIncarnation.incarnationsShieldingUC.ContainsKey(id)) {
      return new ShieldingUCAsIDefenseUC(new ShieldingUC(this, id));
    }
    if (rootIncarnation.incarnationsBidingOperationUC.ContainsKey(id)) {
      return new BidingOperationUCAsIDefenseUC(new BidingOperationUC(this, id));
    }
    return NullIDefenseUC.Null;
  }
  public void CheckHasIDefenseUC(IDefenseUC thing) {
    GetIDefenseUC(thing.id);
  }
  public void CheckHasIDefenseUC(int id) {
    GetIDefenseUC(id);
  }

  public IUnitComponent GetIUnitComponent(int id) {
    if (rootIncarnation.incarnationsKillDirectiveUC.ContainsKey(id)) {
      return new KillDirectiveUCAsIUnitComponent(new KillDirectiveUC(this, id));
    }
    if (rootIncarnation.incarnationsMoveDirectiveUC.ContainsKey(id)) {
      return new MoveDirectiveUCAsIUnitComponent(new MoveDirectiveUC(this, id));
    }
    if (rootIncarnation.incarnationsWanderAICapabilityUC.ContainsKey(id)) {
      return new WanderAICapabilityUCAsIUnitComponent(new WanderAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsBideAICapabilityUC.ContainsKey(id)) {
      return new BideAICapabilityUCAsIUnitComponent(new BideAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsAttackAICapabilityUC.ContainsKey(id)) {
      return new AttackAICapabilityUCAsIUnitComponent(new AttackAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsShieldingUC.ContainsKey(id)) {
      return new ShieldingUCAsIUnitComponent(new ShieldingUC(this, id));
    }
    if (rootIncarnation.incarnationsBidingOperationUC.ContainsKey(id)) {
      return new BidingOperationUCAsIUnitComponent(new BidingOperationUC(this, id));
    }
    throw new Exception("Unknown IUnitComponent: " + id);
  }
  public IUnitComponent GetIUnitComponentOrNull(int id) {
    if (rootIncarnation.incarnationsKillDirectiveUC.ContainsKey(id)) {
      return new KillDirectiveUCAsIUnitComponent(new KillDirectiveUC(this, id));
    }
    if (rootIncarnation.incarnationsMoveDirectiveUC.ContainsKey(id)) {
      return new MoveDirectiveUCAsIUnitComponent(new MoveDirectiveUC(this, id));
    }
    if (rootIncarnation.incarnationsWanderAICapabilityUC.ContainsKey(id)) {
      return new WanderAICapabilityUCAsIUnitComponent(new WanderAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsBideAICapabilityUC.ContainsKey(id)) {
      return new BideAICapabilityUCAsIUnitComponent(new BideAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsAttackAICapabilityUC.ContainsKey(id)) {
      return new AttackAICapabilityUCAsIUnitComponent(new AttackAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsShieldingUC.ContainsKey(id)) {
      return new ShieldingUCAsIUnitComponent(new ShieldingUC(this, id));
    }
    if (rootIncarnation.incarnationsBidingOperationUC.ContainsKey(id)) {
      return new BidingOperationUCAsIUnitComponent(new BidingOperationUC(this, id));
    }
    return NullIUnitComponent.Null;
  }
  public void CheckHasIUnitComponent(IUnitComponent thing) {
    GetIUnitComponent(thing.id);
  }
  public void CheckHasIUnitComponent(int id) {
    GetIUnitComponent(id);
  }

  public IImpulse GetIImpulse(int id) {
    if (rootIncarnation.incarnationsUnleashBideImpulse.ContainsKey(id)) {
      return new UnleashBideImpulseAsIImpulse(new UnleashBideImpulse(this, id));
    }
    if (rootIncarnation.incarnationsStartBidingImpulse.ContainsKey(id)) {
      return new StartBidingImpulseAsIImpulse(new StartBidingImpulse(this, id));
    }
    if (rootIncarnation.incarnationsAttackImpulse.ContainsKey(id)) {
      return new AttackImpulseAsIImpulse(new AttackImpulse(this, id));
    }
    if (rootIncarnation.incarnationsPursueImpulse.ContainsKey(id)) {
      return new PursueImpulseAsIImpulse(new PursueImpulse(this, id));
    }
    if (rootIncarnation.incarnationsMoveImpulse.ContainsKey(id)) {
      return new MoveImpulseAsIImpulse(new MoveImpulse(this, id));
    }
    if (rootIncarnation.incarnationsNoImpulse.ContainsKey(id)) {
      return new NoImpulseAsIImpulse(new NoImpulse(this, id));
    }
    throw new Exception("Unknown IImpulse: " + id);
  }
  public IImpulse GetIImpulseOrNull(int id) {
    if (rootIncarnation.incarnationsUnleashBideImpulse.ContainsKey(id)) {
      return new UnleashBideImpulseAsIImpulse(new UnleashBideImpulse(this, id));
    }
    if (rootIncarnation.incarnationsStartBidingImpulse.ContainsKey(id)) {
      return new StartBidingImpulseAsIImpulse(new StartBidingImpulse(this, id));
    }
    if (rootIncarnation.incarnationsAttackImpulse.ContainsKey(id)) {
      return new AttackImpulseAsIImpulse(new AttackImpulse(this, id));
    }
    if (rootIncarnation.incarnationsPursueImpulse.ContainsKey(id)) {
      return new PursueImpulseAsIImpulse(new PursueImpulse(this, id));
    }
    if (rootIncarnation.incarnationsMoveImpulse.ContainsKey(id)) {
      return new MoveImpulseAsIImpulse(new MoveImpulse(this, id));
    }
    if (rootIncarnation.incarnationsNoImpulse.ContainsKey(id)) {
      return new NoImpulseAsIImpulse(new NoImpulse(this, id));
    }
    return NullIImpulse.Null;
  }
  public void CheckHasIImpulse(IImpulse thing) {
    GetIImpulse(thing.id);
  }
  public void CheckHasIImpulse(int id) {
    GetIImpulse(id);
  }

  public IDestructible GetIDestructible(int id) {
    if (rootIncarnation.incarnationsKillDirectiveUC.ContainsKey(id)) {
      return new KillDirectiveUCAsIDestructible(new KillDirectiveUC(this, id));
    }
    if (rootIncarnation.incarnationsMoveDirectiveUC.ContainsKey(id)) {
      return new MoveDirectiveUCAsIDestructible(new MoveDirectiveUC(this, id));
    }
    if (rootIncarnation.incarnationsWanderAICapabilityUC.ContainsKey(id)) {
      return new WanderAICapabilityUCAsIDestructible(new WanderAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsBideAICapabilityUC.ContainsKey(id)) {
      return new BideAICapabilityUCAsIDestructible(new BideAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsAttackAICapabilityUC.ContainsKey(id)) {
      return new AttackAICapabilityUCAsIDestructible(new AttackAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsShieldingUC.ContainsKey(id)) {
      return new ShieldingUCAsIDestructible(new ShieldingUC(this, id));
    }
    if (rootIncarnation.incarnationsBidingOperationUC.ContainsKey(id)) {
      return new BidingOperationUCAsIDestructible(new BidingOperationUC(this, id));
    }
    if (rootIncarnation.incarnationsUnleashBideImpulse.ContainsKey(id)) {
      return new UnleashBideImpulseAsIDestructible(new UnleashBideImpulse(this, id));
    }
    if (rootIncarnation.incarnationsStartBidingImpulse.ContainsKey(id)) {
      return new StartBidingImpulseAsIDestructible(new StartBidingImpulse(this, id));
    }
    if (rootIncarnation.incarnationsAttackImpulse.ContainsKey(id)) {
      return new AttackImpulseAsIDestructible(new AttackImpulse(this, id));
    }
    if (rootIncarnation.incarnationsPursueImpulse.ContainsKey(id)) {
      return new PursueImpulseAsIDestructible(new PursueImpulse(this, id));
    }
    if (rootIncarnation.incarnationsMoveImpulse.ContainsKey(id)) {
      return new MoveImpulseAsIDestructible(new MoveImpulse(this, id));
    }
    if (rootIncarnation.incarnationsNoImpulse.ContainsKey(id)) {
      return new NoImpulseAsIDestructible(new NoImpulse(this, id));
    }
    if (rootIncarnation.incarnationsUnit.ContainsKey(id)) {
      return new UnitAsIDestructible(new Unit(this, id));
    }
    throw new Exception("Unknown IDestructible: " + id);
  }
  public IDestructible GetIDestructibleOrNull(int id) {
    if (rootIncarnation.incarnationsKillDirectiveUC.ContainsKey(id)) {
      return new KillDirectiveUCAsIDestructible(new KillDirectiveUC(this, id));
    }
    if (rootIncarnation.incarnationsMoveDirectiveUC.ContainsKey(id)) {
      return new MoveDirectiveUCAsIDestructible(new MoveDirectiveUC(this, id));
    }
    if (rootIncarnation.incarnationsWanderAICapabilityUC.ContainsKey(id)) {
      return new WanderAICapabilityUCAsIDestructible(new WanderAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsBideAICapabilityUC.ContainsKey(id)) {
      return new BideAICapabilityUCAsIDestructible(new BideAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsAttackAICapabilityUC.ContainsKey(id)) {
      return new AttackAICapabilityUCAsIDestructible(new AttackAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsShieldingUC.ContainsKey(id)) {
      return new ShieldingUCAsIDestructible(new ShieldingUC(this, id));
    }
    if (rootIncarnation.incarnationsBidingOperationUC.ContainsKey(id)) {
      return new BidingOperationUCAsIDestructible(new BidingOperationUC(this, id));
    }
    if (rootIncarnation.incarnationsUnleashBideImpulse.ContainsKey(id)) {
      return new UnleashBideImpulseAsIDestructible(new UnleashBideImpulse(this, id));
    }
    if (rootIncarnation.incarnationsStartBidingImpulse.ContainsKey(id)) {
      return new StartBidingImpulseAsIDestructible(new StartBidingImpulse(this, id));
    }
    if (rootIncarnation.incarnationsAttackImpulse.ContainsKey(id)) {
      return new AttackImpulseAsIDestructible(new AttackImpulse(this, id));
    }
    if (rootIncarnation.incarnationsPursueImpulse.ContainsKey(id)) {
      return new PursueImpulseAsIDestructible(new PursueImpulse(this, id));
    }
    if (rootIncarnation.incarnationsMoveImpulse.ContainsKey(id)) {
      return new MoveImpulseAsIDestructible(new MoveImpulse(this, id));
    }
    if (rootIncarnation.incarnationsNoImpulse.ContainsKey(id)) {
      return new NoImpulseAsIDestructible(new NoImpulse(this, id));
    }
    if (rootIncarnation.incarnationsUnit.ContainsKey(id)) {
      return new UnitAsIDestructible(new Unit(this, id));
    }
    return NullIDestructible.Null;
  }
  public void CheckHasIDestructible(IDestructible thing) {
    GetIDestructible(thing.id);
  }
  public void CheckHasIDestructible(int id) {
    GetIDestructible(id);
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
    public List<IUnitEventMutList> AllIUnitEventMutList() {
      List<IUnitEventMutList> result = new List<IUnitEventMutList>(rootIncarnation.incarnationsIUnitEventMutList.Count);
      foreach (var id in rootIncarnation.incarnationsIUnitEventMutList.Keys) {
        result.Add(new IUnitEventMutList(this, id));
      }
      return result;
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
    public IUnitEventMutList EffectIUnitEventMutListCreate(IEnumerable<IUnitEvent> elements) {
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
    public List<LocationMutList> AllLocationMutList() {
      List<LocationMutList> result = new List<LocationMutList>(rootIncarnation.incarnationsLocationMutList.Count);
      foreach (var id in rootIncarnation.incarnationsLocationMutList.Keys) {
        result.Add(new LocationMutList(this, id));
      }
      return result;
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
    public LocationMutList EffectLocationMutListCreate(IEnumerable<Location> elements) {
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

    public int GetLevelMutSetHash(int id, int version, LevelMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public LevelMutSetIncarnation GetLevelMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsLevelMutSet[id].incarnation;
    }
    public LevelMutSet GetLevelMutSet(int id) {
      return new LevelMutSet(this, id);
    }
    public List<LevelMutSet> AllLevelMutSet() {
      List<LevelMutSet> result = new List<LevelMutSet>(rootIncarnation.incarnationsLevelMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsLevelMutSet.Keys) {
        result.Add(new LevelMutSet(this, id));
      }
      return result;
    }
    public bool LevelMutSetExists(int id) {
      return rootIncarnation.incarnationsLevelMutSet.ContainsKey(id);
    }
    public void CheckHasLevelMutSet(LevelMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasLevelMutSet(thing.id);
    }
    public void CheckHasLevelMutSet(int id) {
      if (!rootIncarnation.incarnationsLevelMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid LevelMutSet}!");
      }
    }
    public LevelMutSet EffectLevelMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new LevelMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateLevelMutSet(id, incarnation);
      this.rootIncarnation.hash += GetLevelMutSetHash(id, rootIncarnation.version, incarnation);
      return new LevelMutSet(this, id);
    }
    public void EffectInternalCreateLevelMutSet(int id, LevelMutSetIncarnation incarnation) {
      var effect = new LevelMutSetCreateEffect(id, incarnation);
      rootIncarnation.incarnationsLevelMutSet
          .Add(
              id,
              new VersionAndIncarnation<LevelMutSetIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastLevelMutSetEffect(id, effect);
    }
    public void EffectLevelMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new LevelMutSetDeleteEffect(id);
      BroadcastLevelMutSetEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsLevelMutSet[id];
      this.rootIncarnation.hash -=
          GetLevelMutSetHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsLevelMutSet.Remove(id);
    }

       
    public void EffectLevelMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasLevelMutSet(setId);
      CheckHasLevel(elementId);

      var effect = new LevelMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsLevelMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
        this.rootIncarnation.hash += setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new LevelMutSetIncarnation(newMap);
        rootIncarnation.incarnationsLevelMutSet[setId] =
            new VersionAndIncarnation<LevelMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetLevelMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetLevelMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastLevelMutSetEffect(setId, effect);
    }
    public void EffectLevelMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasLevelMutSet(setId);
      CheckHasLevel(elementId);

      var effect = new LevelMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsLevelMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        this.rootIncarnation.hash -= setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new LevelMutSetIncarnation(newMap);
        rootIncarnation.incarnationsLevelMutSet[setId] =
            new VersionAndIncarnation<LevelMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetLevelMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetLevelMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastLevelMutSetEffect(setId, effect);
    }

       
    public void AddLevelMutSetObserver(int id, ILevelMutSetEffectObserver observer) {
      List<ILevelMutSetEffectObserver> obsies;
      if (!observersLevelMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ILevelMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersLevelMutSet[id] = obsies;
    }

    public void RemoveLevelMutSetObserver(int id, ILevelMutSetEffectObserver observer) {
      if (observersLevelMutSet.ContainsKey(id)) {
        var list = observersLevelMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersLevelMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
    public void BroadcastLevelMutSetEffect(int id, ILevelMutSetEffect effect) {
      if (observersLevelMutSet.ContainsKey(0)) {
        foreach (var observer in new List<ILevelMutSetEffectObserver>(observersLevelMutSet[0])) {
          observer.OnLevelMutSetEffect(effect);
        }
      }
      if (observersLevelMutSet.ContainsKey(id)) {
        foreach (var observer in new List<ILevelMutSetEffectObserver>(observersLevelMutSet[id])) {
          observer.OnLevelMutSetEffect(effect);
        }
      }
    }

    public int GetShieldingUCWeakMutSetHash(int id, int version, ShieldingUCWeakMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public ShieldingUCWeakMutSetIncarnation GetShieldingUCWeakMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsShieldingUCWeakMutSet[id].incarnation;
    }
    public ShieldingUCWeakMutSet GetShieldingUCWeakMutSet(int id) {
      return new ShieldingUCWeakMutSet(this, id);
    }
    public List<ShieldingUCWeakMutSet> AllShieldingUCWeakMutSet() {
      List<ShieldingUCWeakMutSet> result = new List<ShieldingUCWeakMutSet>(rootIncarnation.incarnationsShieldingUCWeakMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsShieldingUCWeakMutSet.Keys) {
        result.Add(new ShieldingUCWeakMutSet(this, id));
      }
      return result;
    }
    public bool ShieldingUCWeakMutSetExists(int id) {
      return rootIncarnation.incarnationsShieldingUCWeakMutSet.ContainsKey(id);
    }
    public void CheckHasShieldingUCWeakMutSet(ShieldingUCWeakMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasShieldingUCWeakMutSet(thing.id);
    }
    public void CheckHasShieldingUCWeakMutSet(int id) {
      if (!rootIncarnation.incarnationsShieldingUCWeakMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid ShieldingUCWeakMutSet}!");
      }
    }
    public ShieldingUCWeakMutSet EffectShieldingUCWeakMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new ShieldingUCWeakMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateShieldingUCWeakMutSet(id, incarnation);
      this.rootIncarnation.hash += GetShieldingUCWeakMutSetHash(id, rootIncarnation.version, incarnation);
      return new ShieldingUCWeakMutSet(this, id);
    }
    public void EffectInternalCreateShieldingUCWeakMutSet(int id, ShieldingUCWeakMutSetIncarnation incarnation) {
      var effect = new ShieldingUCWeakMutSetCreateEffect(id, incarnation);
      rootIncarnation.incarnationsShieldingUCWeakMutSet
          .Add(
              id,
              new VersionAndIncarnation<ShieldingUCWeakMutSetIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastShieldingUCWeakMutSetEffect(id, effect);
    }
    public void EffectShieldingUCWeakMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new ShieldingUCWeakMutSetDeleteEffect(id);
      BroadcastShieldingUCWeakMutSetEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsShieldingUCWeakMutSet[id];
      this.rootIncarnation.hash -=
          GetShieldingUCWeakMutSetHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsShieldingUCWeakMutSet.Remove(id);
    }

       
    public void EffectShieldingUCWeakMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasShieldingUCWeakMutSet(setId);
      CheckHasShieldingUC(elementId);

      var effect = new ShieldingUCWeakMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsShieldingUCWeakMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
        this.rootIncarnation.hash += setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new ShieldingUCWeakMutSetIncarnation(newMap);
        rootIncarnation.incarnationsShieldingUCWeakMutSet[setId] =
            new VersionAndIncarnation<ShieldingUCWeakMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetShieldingUCWeakMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetShieldingUCWeakMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastShieldingUCWeakMutSetEffect(setId, effect);
    }
    public void EffectShieldingUCWeakMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasShieldingUCWeakMutSet(setId);
      CheckHasShieldingUC(elementId);

      var effect = new ShieldingUCWeakMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsShieldingUCWeakMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        this.rootIncarnation.hash -= setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new ShieldingUCWeakMutSetIncarnation(newMap);
        rootIncarnation.incarnationsShieldingUCWeakMutSet[setId] =
            new VersionAndIncarnation<ShieldingUCWeakMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetShieldingUCWeakMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetShieldingUCWeakMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastShieldingUCWeakMutSetEffect(setId, effect);
    }

       
    public void AddShieldingUCWeakMutSetObserver(int id, IShieldingUCWeakMutSetEffectObserver observer) {
      List<IShieldingUCWeakMutSetEffectObserver> obsies;
      if (!observersShieldingUCWeakMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IShieldingUCWeakMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersShieldingUCWeakMutSet[id] = obsies;
    }

    public void RemoveShieldingUCWeakMutSetObserver(int id, IShieldingUCWeakMutSetEffectObserver observer) {
      if (observersShieldingUCWeakMutSet.ContainsKey(id)) {
        var list = observersShieldingUCWeakMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersShieldingUCWeakMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
    public void BroadcastShieldingUCWeakMutSetEffect(int id, IShieldingUCWeakMutSetEffect effect) {
      if (observersShieldingUCWeakMutSet.ContainsKey(0)) {
        foreach (var observer in new List<IShieldingUCWeakMutSetEffectObserver>(observersShieldingUCWeakMutSet[0])) {
          observer.OnShieldingUCWeakMutSetEffect(effect);
        }
      }
      if (observersShieldingUCWeakMutSet.ContainsKey(id)) {
        foreach (var observer in new List<IShieldingUCWeakMutSetEffectObserver>(observersShieldingUCWeakMutSet[id])) {
          observer.OnShieldingUCWeakMutSetEffect(effect);
        }
      }
    }

    public int GetAttackAICapabilityUCWeakMutSetHash(int id, int version, AttackAICapabilityUCWeakMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public AttackAICapabilityUCWeakMutSetIncarnation GetAttackAICapabilityUCWeakMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet[id].incarnation;
    }
    public AttackAICapabilityUCWeakMutSet GetAttackAICapabilityUCWeakMutSet(int id) {
      return new AttackAICapabilityUCWeakMutSet(this, id);
    }
    public List<AttackAICapabilityUCWeakMutSet> AllAttackAICapabilityUCWeakMutSet() {
      List<AttackAICapabilityUCWeakMutSet> result = new List<AttackAICapabilityUCWeakMutSet>(rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet.Keys) {
        result.Add(new AttackAICapabilityUCWeakMutSet(this, id));
      }
      return result;
    }
    public bool AttackAICapabilityUCWeakMutSetExists(int id) {
      return rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet.ContainsKey(id);
    }
    public void CheckHasAttackAICapabilityUCWeakMutSet(AttackAICapabilityUCWeakMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasAttackAICapabilityUCWeakMutSet(thing.id);
    }
    public void CheckHasAttackAICapabilityUCWeakMutSet(int id) {
      if (!rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid AttackAICapabilityUCWeakMutSet}!");
      }
    }
    public AttackAICapabilityUCWeakMutSet EffectAttackAICapabilityUCWeakMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new AttackAICapabilityUCWeakMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateAttackAICapabilityUCWeakMutSet(id, incarnation);
      this.rootIncarnation.hash += GetAttackAICapabilityUCWeakMutSetHash(id, rootIncarnation.version, incarnation);
      return new AttackAICapabilityUCWeakMutSet(this, id);
    }
    public void EffectInternalCreateAttackAICapabilityUCWeakMutSet(int id, AttackAICapabilityUCWeakMutSetIncarnation incarnation) {
      var effect = new AttackAICapabilityUCWeakMutSetCreateEffect(id, incarnation);
      rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet
          .Add(
              id,
              new VersionAndIncarnation<AttackAICapabilityUCWeakMutSetIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastAttackAICapabilityUCWeakMutSetEffect(id, effect);
    }
    public void EffectAttackAICapabilityUCWeakMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new AttackAICapabilityUCWeakMutSetDeleteEffect(id);
      BroadcastAttackAICapabilityUCWeakMutSetEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet[id];
      this.rootIncarnation.hash -=
          GetAttackAICapabilityUCWeakMutSetHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet.Remove(id);
    }

       
    public void EffectAttackAICapabilityUCWeakMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasAttackAICapabilityUCWeakMutSet(setId);
      CheckHasAttackAICapabilityUC(elementId);

      var effect = new AttackAICapabilityUCWeakMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
        this.rootIncarnation.hash += setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new AttackAICapabilityUCWeakMutSetIncarnation(newMap);
        rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet[setId] =
            new VersionAndIncarnation<AttackAICapabilityUCWeakMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetAttackAICapabilityUCWeakMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetAttackAICapabilityUCWeakMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastAttackAICapabilityUCWeakMutSetEffect(setId, effect);
    }
    public void EffectAttackAICapabilityUCWeakMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasAttackAICapabilityUCWeakMutSet(setId);
      CheckHasAttackAICapabilityUC(elementId);

      var effect = new AttackAICapabilityUCWeakMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        this.rootIncarnation.hash -= setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new AttackAICapabilityUCWeakMutSetIncarnation(newMap);
        rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet[setId] =
            new VersionAndIncarnation<AttackAICapabilityUCWeakMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetAttackAICapabilityUCWeakMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetAttackAICapabilityUCWeakMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastAttackAICapabilityUCWeakMutSetEffect(setId, effect);
    }

       
    public void AddAttackAICapabilityUCWeakMutSetObserver(int id, IAttackAICapabilityUCWeakMutSetEffectObserver observer) {
      List<IAttackAICapabilityUCWeakMutSetEffectObserver> obsies;
      if (!observersAttackAICapabilityUCWeakMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IAttackAICapabilityUCWeakMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersAttackAICapabilityUCWeakMutSet[id] = obsies;
    }

    public void RemoveAttackAICapabilityUCWeakMutSetObserver(int id, IAttackAICapabilityUCWeakMutSetEffectObserver observer) {
      if (observersAttackAICapabilityUCWeakMutSet.ContainsKey(id)) {
        var list = observersAttackAICapabilityUCWeakMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersAttackAICapabilityUCWeakMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
    public void BroadcastAttackAICapabilityUCWeakMutSetEffect(int id, IAttackAICapabilityUCWeakMutSetEffect effect) {
      if (observersAttackAICapabilityUCWeakMutSet.ContainsKey(0)) {
        foreach (var observer in new List<IAttackAICapabilityUCWeakMutSetEffectObserver>(observersAttackAICapabilityUCWeakMutSet[0])) {
          observer.OnAttackAICapabilityUCWeakMutSetEffect(effect);
        }
      }
      if (observersAttackAICapabilityUCWeakMutSet.ContainsKey(id)) {
        foreach (var observer in new List<IAttackAICapabilityUCWeakMutSetEffectObserver>(observersAttackAICapabilityUCWeakMutSet[id])) {
          observer.OnAttackAICapabilityUCWeakMutSetEffect(effect);
        }
      }
    }

    public int GetKillDirectiveUCMutSetHash(int id, int version, KillDirectiveUCMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public KillDirectiveUCMutSetIncarnation GetKillDirectiveUCMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsKillDirectiveUCMutSet[id].incarnation;
    }
    public KillDirectiveUCMutSet GetKillDirectiveUCMutSet(int id) {
      return new KillDirectiveUCMutSet(this, id);
    }
    public List<KillDirectiveUCMutSet> AllKillDirectiveUCMutSet() {
      List<KillDirectiveUCMutSet> result = new List<KillDirectiveUCMutSet>(rootIncarnation.incarnationsKillDirectiveUCMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsKillDirectiveUCMutSet.Keys) {
        result.Add(new KillDirectiveUCMutSet(this, id));
      }
      return result;
    }
    public bool KillDirectiveUCMutSetExists(int id) {
      return rootIncarnation.incarnationsKillDirectiveUCMutSet.ContainsKey(id);
    }
    public void CheckHasKillDirectiveUCMutSet(KillDirectiveUCMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasKillDirectiveUCMutSet(thing.id);
    }
    public void CheckHasKillDirectiveUCMutSet(int id) {
      if (!rootIncarnation.incarnationsKillDirectiveUCMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid KillDirectiveUCMutSet}!");
      }
    }
    public KillDirectiveUCMutSet EffectKillDirectiveUCMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new KillDirectiveUCMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateKillDirectiveUCMutSet(id, incarnation);
      this.rootIncarnation.hash += GetKillDirectiveUCMutSetHash(id, rootIncarnation.version, incarnation);
      return new KillDirectiveUCMutSet(this, id);
    }
    public void EffectInternalCreateKillDirectiveUCMutSet(int id, KillDirectiveUCMutSetIncarnation incarnation) {
      var effect = new KillDirectiveUCMutSetCreateEffect(id, incarnation);
      rootIncarnation.incarnationsKillDirectiveUCMutSet
          .Add(
              id,
              new VersionAndIncarnation<KillDirectiveUCMutSetIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastKillDirectiveUCMutSetEffect(id, effect);
    }
    public void EffectKillDirectiveUCMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new KillDirectiveUCMutSetDeleteEffect(id);
      BroadcastKillDirectiveUCMutSetEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsKillDirectiveUCMutSet[id];
      this.rootIncarnation.hash -=
          GetKillDirectiveUCMutSetHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsKillDirectiveUCMutSet.Remove(id);
    }

       
    public void EffectKillDirectiveUCMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasKillDirectiveUCMutSet(setId);
      CheckHasKillDirectiveUC(elementId);

      var effect = new KillDirectiveUCMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsKillDirectiveUCMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
        this.rootIncarnation.hash += setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new KillDirectiveUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsKillDirectiveUCMutSet[setId] =
            new VersionAndIncarnation<KillDirectiveUCMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetKillDirectiveUCMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetKillDirectiveUCMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastKillDirectiveUCMutSetEffect(setId, effect);
    }
    public void EffectKillDirectiveUCMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasKillDirectiveUCMutSet(setId);
      CheckHasKillDirectiveUC(elementId);

      var effect = new KillDirectiveUCMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsKillDirectiveUCMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        this.rootIncarnation.hash -= setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new KillDirectiveUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsKillDirectiveUCMutSet[setId] =
            new VersionAndIncarnation<KillDirectiveUCMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetKillDirectiveUCMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetKillDirectiveUCMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastKillDirectiveUCMutSetEffect(setId, effect);
    }

       
    public void AddKillDirectiveUCMutSetObserver(int id, IKillDirectiveUCMutSetEffectObserver observer) {
      List<IKillDirectiveUCMutSetEffectObserver> obsies;
      if (!observersKillDirectiveUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IKillDirectiveUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersKillDirectiveUCMutSet[id] = obsies;
    }

    public void RemoveKillDirectiveUCMutSetObserver(int id, IKillDirectiveUCMutSetEffectObserver observer) {
      if (observersKillDirectiveUCMutSet.ContainsKey(id)) {
        var list = observersKillDirectiveUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersKillDirectiveUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
    public void BroadcastKillDirectiveUCMutSetEffect(int id, IKillDirectiveUCMutSetEffect effect) {
      if (observersKillDirectiveUCMutSet.ContainsKey(0)) {
        foreach (var observer in new List<IKillDirectiveUCMutSetEffectObserver>(observersKillDirectiveUCMutSet[0])) {
          observer.OnKillDirectiveUCMutSetEffect(effect);
        }
      }
      if (observersKillDirectiveUCMutSet.ContainsKey(id)) {
        foreach (var observer in new List<IKillDirectiveUCMutSetEffectObserver>(observersKillDirectiveUCMutSet[id])) {
          observer.OnKillDirectiveUCMutSetEffect(effect);
        }
      }
    }

    public int GetMoveDirectiveUCMutSetHash(int id, int version, MoveDirectiveUCMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public MoveDirectiveUCMutSetIncarnation GetMoveDirectiveUCMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsMoveDirectiveUCMutSet[id].incarnation;
    }
    public MoveDirectiveUCMutSet GetMoveDirectiveUCMutSet(int id) {
      return new MoveDirectiveUCMutSet(this, id);
    }
    public List<MoveDirectiveUCMutSet> AllMoveDirectiveUCMutSet() {
      List<MoveDirectiveUCMutSet> result = new List<MoveDirectiveUCMutSet>(rootIncarnation.incarnationsMoveDirectiveUCMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsMoveDirectiveUCMutSet.Keys) {
        result.Add(new MoveDirectiveUCMutSet(this, id));
      }
      return result;
    }
    public bool MoveDirectiveUCMutSetExists(int id) {
      return rootIncarnation.incarnationsMoveDirectiveUCMutSet.ContainsKey(id);
    }
    public void CheckHasMoveDirectiveUCMutSet(MoveDirectiveUCMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasMoveDirectiveUCMutSet(thing.id);
    }
    public void CheckHasMoveDirectiveUCMutSet(int id) {
      if (!rootIncarnation.incarnationsMoveDirectiveUCMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid MoveDirectiveUCMutSet}!");
      }
    }
    public MoveDirectiveUCMutSet EffectMoveDirectiveUCMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new MoveDirectiveUCMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateMoveDirectiveUCMutSet(id, incarnation);
      this.rootIncarnation.hash += GetMoveDirectiveUCMutSetHash(id, rootIncarnation.version, incarnation);
      return new MoveDirectiveUCMutSet(this, id);
    }
    public void EffectInternalCreateMoveDirectiveUCMutSet(int id, MoveDirectiveUCMutSetIncarnation incarnation) {
      var effect = new MoveDirectiveUCMutSetCreateEffect(id, incarnation);
      rootIncarnation.incarnationsMoveDirectiveUCMutSet
          .Add(
              id,
              new VersionAndIncarnation<MoveDirectiveUCMutSetIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastMoveDirectiveUCMutSetEffect(id, effect);
    }
    public void EffectMoveDirectiveUCMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new MoveDirectiveUCMutSetDeleteEffect(id);
      BroadcastMoveDirectiveUCMutSetEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsMoveDirectiveUCMutSet[id];
      this.rootIncarnation.hash -=
          GetMoveDirectiveUCMutSetHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsMoveDirectiveUCMutSet.Remove(id);
    }

       
    public void EffectMoveDirectiveUCMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasMoveDirectiveUCMutSet(setId);
      CheckHasMoveDirectiveUC(elementId);

      var effect = new MoveDirectiveUCMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsMoveDirectiveUCMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
        this.rootIncarnation.hash += setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new MoveDirectiveUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsMoveDirectiveUCMutSet[setId] =
            new VersionAndIncarnation<MoveDirectiveUCMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetMoveDirectiveUCMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetMoveDirectiveUCMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastMoveDirectiveUCMutSetEffect(setId, effect);
    }
    public void EffectMoveDirectiveUCMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasMoveDirectiveUCMutSet(setId);
      CheckHasMoveDirectiveUC(elementId);

      var effect = new MoveDirectiveUCMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsMoveDirectiveUCMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        this.rootIncarnation.hash -= setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new MoveDirectiveUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsMoveDirectiveUCMutSet[setId] =
            new VersionAndIncarnation<MoveDirectiveUCMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetMoveDirectiveUCMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetMoveDirectiveUCMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastMoveDirectiveUCMutSetEffect(setId, effect);
    }

       
    public void AddMoveDirectiveUCMutSetObserver(int id, IMoveDirectiveUCMutSetEffectObserver observer) {
      List<IMoveDirectiveUCMutSetEffectObserver> obsies;
      if (!observersMoveDirectiveUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IMoveDirectiveUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersMoveDirectiveUCMutSet[id] = obsies;
    }

    public void RemoveMoveDirectiveUCMutSetObserver(int id, IMoveDirectiveUCMutSetEffectObserver observer) {
      if (observersMoveDirectiveUCMutSet.ContainsKey(id)) {
        var list = observersMoveDirectiveUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersMoveDirectiveUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
    public void BroadcastMoveDirectiveUCMutSetEffect(int id, IMoveDirectiveUCMutSetEffect effect) {
      if (observersMoveDirectiveUCMutSet.ContainsKey(0)) {
        foreach (var observer in new List<IMoveDirectiveUCMutSetEffectObserver>(observersMoveDirectiveUCMutSet[0])) {
          observer.OnMoveDirectiveUCMutSetEffect(effect);
        }
      }
      if (observersMoveDirectiveUCMutSet.ContainsKey(id)) {
        foreach (var observer in new List<IMoveDirectiveUCMutSetEffectObserver>(observersMoveDirectiveUCMutSet[id])) {
          observer.OnMoveDirectiveUCMutSetEffect(effect);
        }
      }
    }

    public int GetWanderAICapabilityUCMutSetHash(int id, int version, WanderAICapabilityUCMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public WanderAICapabilityUCMutSetIncarnation GetWanderAICapabilityUCMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsWanderAICapabilityUCMutSet[id].incarnation;
    }
    public WanderAICapabilityUCMutSet GetWanderAICapabilityUCMutSet(int id) {
      return new WanderAICapabilityUCMutSet(this, id);
    }
    public List<WanderAICapabilityUCMutSet> AllWanderAICapabilityUCMutSet() {
      List<WanderAICapabilityUCMutSet> result = new List<WanderAICapabilityUCMutSet>(rootIncarnation.incarnationsWanderAICapabilityUCMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsWanderAICapabilityUCMutSet.Keys) {
        result.Add(new WanderAICapabilityUCMutSet(this, id));
      }
      return result;
    }
    public bool WanderAICapabilityUCMutSetExists(int id) {
      return rootIncarnation.incarnationsWanderAICapabilityUCMutSet.ContainsKey(id);
    }
    public void CheckHasWanderAICapabilityUCMutSet(WanderAICapabilityUCMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasWanderAICapabilityUCMutSet(thing.id);
    }
    public void CheckHasWanderAICapabilityUCMutSet(int id) {
      if (!rootIncarnation.incarnationsWanderAICapabilityUCMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid WanderAICapabilityUCMutSet}!");
      }
    }
    public WanderAICapabilityUCMutSet EffectWanderAICapabilityUCMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new WanderAICapabilityUCMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateWanderAICapabilityUCMutSet(id, incarnation);
      this.rootIncarnation.hash += GetWanderAICapabilityUCMutSetHash(id, rootIncarnation.version, incarnation);
      return new WanderAICapabilityUCMutSet(this, id);
    }
    public void EffectInternalCreateWanderAICapabilityUCMutSet(int id, WanderAICapabilityUCMutSetIncarnation incarnation) {
      var effect = new WanderAICapabilityUCMutSetCreateEffect(id, incarnation);
      rootIncarnation.incarnationsWanderAICapabilityUCMutSet
          .Add(
              id,
              new VersionAndIncarnation<WanderAICapabilityUCMutSetIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastWanderAICapabilityUCMutSetEffect(id, effect);
    }
    public void EffectWanderAICapabilityUCMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new WanderAICapabilityUCMutSetDeleteEffect(id);
      BroadcastWanderAICapabilityUCMutSetEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsWanderAICapabilityUCMutSet[id];
      this.rootIncarnation.hash -=
          GetWanderAICapabilityUCMutSetHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsWanderAICapabilityUCMutSet.Remove(id);
    }

       
    public void EffectWanderAICapabilityUCMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasWanderAICapabilityUCMutSet(setId);
      CheckHasWanderAICapabilityUC(elementId);

      var effect = new WanderAICapabilityUCMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsWanderAICapabilityUCMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
        this.rootIncarnation.hash += setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new WanderAICapabilityUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsWanderAICapabilityUCMutSet[setId] =
            new VersionAndIncarnation<WanderAICapabilityUCMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetWanderAICapabilityUCMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetWanderAICapabilityUCMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastWanderAICapabilityUCMutSetEffect(setId, effect);
    }
    public void EffectWanderAICapabilityUCMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasWanderAICapabilityUCMutSet(setId);
      CheckHasWanderAICapabilityUC(elementId);

      var effect = new WanderAICapabilityUCMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsWanderAICapabilityUCMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        this.rootIncarnation.hash -= setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new WanderAICapabilityUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsWanderAICapabilityUCMutSet[setId] =
            new VersionAndIncarnation<WanderAICapabilityUCMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetWanderAICapabilityUCMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetWanderAICapabilityUCMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastWanderAICapabilityUCMutSetEffect(setId, effect);
    }

       
    public void AddWanderAICapabilityUCMutSetObserver(int id, IWanderAICapabilityUCMutSetEffectObserver observer) {
      List<IWanderAICapabilityUCMutSetEffectObserver> obsies;
      if (!observersWanderAICapabilityUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IWanderAICapabilityUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersWanderAICapabilityUCMutSet[id] = obsies;
    }

    public void RemoveWanderAICapabilityUCMutSetObserver(int id, IWanderAICapabilityUCMutSetEffectObserver observer) {
      if (observersWanderAICapabilityUCMutSet.ContainsKey(id)) {
        var list = observersWanderAICapabilityUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersWanderAICapabilityUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
    public void BroadcastWanderAICapabilityUCMutSetEffect(int id, IWanderAICapabilityUCMutSetEffect effect) {
      if (observersWanderAICapabilityUCMutSet.ContainsKey(0)) {
        foreach (var observer in new List<IWanderAICapabilityUCMutSetEffectObserver>(observersWanderAICapabilityUCMutSet[0])) {
          observer.OnWanderAICapabilityUCMutSetEffect(effect);
        }
      }
      if (observersWanderAICapabilityUCMutSet.ContainsKey(id)) {
        foreach (var observer in new List<IWanderAICapabilityUCMutSetEffectObserver>(observersWanderAICapabilityUCMutSet[id])) {
          observer.OnWanderAICapabilityUCMutSetEffect(effect);
        }
      }
    }

    public int GetBideAICapabilityUCMutSetHash(int id, int version, BideAICapabilityUCMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public BideAICapabilityUCMutSetIncarnation GetBideAICapabilityUCMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsBideAICapabilityUCMutSet[id].incarnation;
    }
    public BideAICapabilityUCMutSet GetBideAICapabilityUCMutSet(int id) {
      return new BideAICapabilityUCMutSet(this, id);
    }
    public List<BideAICapabilityUCMutSet> AllBideAICapabilityUCMutSet() {
      List<BideAICapabilityUCMutSet> result = new List<BideAICapabilityUCMutSet>(rootIncarnation.incarnationsBideAICapabilityUCMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsBideAICapabilityUCMutSet.Keys) {
        result.Add(new BideAICapabilityUCMutSet(this, id));
      }
      return result;
    }
    public bool BideAICapabilityUCMutSetExists(int id) {
      return rootIncarnation.incarnationsBideAICapabilityUCMutSet.ContainsKey(id);
    }
    public void CheckHasBideAICapabilityUCMutSet(BideAICapabilityUCMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasBideAICapabilityUCMutSet(thing.id);
    }
    public void CheckHasBideAICapabilityUCMutSet(int id) {
      if (!rootIncarnation.incarnationsBideAICapabilityUCMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid BideAICapabilityUCMutSet}!");
      }
    }
    public BideAICapabilityUCMutSet EffectBideAICapabilityUCMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new BideAICapabilityUCMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateBideAICapabilityUCMutSet(id, incarnation);
      this.rootIncarnation.hash += GetBideAICapabilityUCMutSetHash(id, rootIncarnation.version, incarnation);
      return new BideAICapabilityUCMutSet(this, id);
    }
    public void EffectInternalCreateBideAICapabilityUCMutSet(int id, BideAICapabilityUCMutSetIncarnation incarnation) {
      var effect = new BideAICapabilityUCMutSetCreateEffect(id, incarnation);
      rootIncarnation.incarnationsBideAICapabilityUCMutSet
          .Add(
              id,
              new VersionAndIncarnation<BideAICapabilityUCMutSetIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastBideAICapabilityUCMutSetEffect(id, effect);
    }
    public void EffectBideAICapabilityUCMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new BideAICapabilityUCMutSetDeleteEffect(id);
      BroadcastBideAICapabilityUCMutSetEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsBideAICapabilityUCMutSet[id];
      this.rootIncarnation.hash -=
          GetBideAICapabilityUCMutSetHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsBideAICapabilityUCMutSet.Remove(id);
    }

       
    public void EffectBideAICapabilityUCMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasBideAICapabilityUCMutSet(setId);
      CheckHasBideAICapabilityUC(elementId);

      var effect = new BideAICapabilityUCMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsBideAICapabilityUCMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
        this.rootIncarnation.hash += setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new BideAICapabilityUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsBideAICapabilityUCMutSet[setId] =
            new VersionAndIncarnation<BideAICapabilityUCMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetBideAICapabilityUCMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetBideAICapabilityUCMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastBideAICapabilityUCMutSetEffect(setId, effect);
    }
    public void EffectBideAICapabilityUCMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasBideAICapabilityUCMutSet(setId);
      CheckHasBideAICapabilityUC(elementId);

      var effect = new BideAICapabilityUCMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsBideAICapabilityUCMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        this.rootIncarnation.hash -= setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new BideAICapabilityUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsBideAICapabilityUCMutSet[setId] =
            new VersionAndIncarnation<BideAICapabilityUCMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetBideAICapabilityUCMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetBideAICapabilityUCMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastBideAICapabilityUCMutSetEffect(setId, effect);
    }

       
    public void AddBideAICapabilityUCMutSetObserver(int id, IBideAICapabilityUCMutSetEffectObserver observer) {
      List<IBideAICapabilityUCMutSetEffectObserver> obsies;
      if (!observersBideAICapabilityUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IBideAICapabilityUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersBideAICapabilityUCMutSet[id] = obsies;
    }

    public void RemoveBideAICapabilityUCMutSetObserver(int id, IBideAICapabilityUCMutSetEffectObserver observer) {
      if (observersBideAICapabilityUCMutSet.ContainsKey(id)) {
        var list = observersBideAICapabilityUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersBideAICapabilityUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
    public void BroadcastBideAICapabilityUCMutSetEffect(int id, IBideAICapabilityUCMutSetEffect effect) {
      if (observersBideAICapabilityUCMutSet.ContainsKey(0)) {
        foreach (var observer in new List<IBideAICapabilityUCMutSetEffectObserver>(observersBideAICapabilityUCMutSet[0])) {
          observer.OnBideAICapabilityUCMutSetEffect(effect);
        }
      }
      if (observersBideAICapabilityUCMutSet.ContainsKey(id)) {
        foreach (var observer in new List<IBideAICapabilityUCMutSetEffectObserver>(observersBideAICapabilityUCMutSet[id])) {
          observer.OnBideAICapabilityUCMutSetEffect(effect);
        }
      }
    }

    public int GetAttackAICapabilityUCMutSetHash(int id, int version, AttackAICapabilityUCMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public AttackAICapabilityUCMutSetIncarnation GetAttackAICapabilityUCMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsAttackAICapabilityUCMutSet[id].incarnation;
    }
    public AttackAICapabilityUCMutSet GetAttackAICapabilityUCMutSet(int id) {
      return new AttackAICapabilityUCMutSet(this, id);
    }
    public List<AttackAICapabilityUCMutSet> AllAttackAICapabilityUCMutSet() {
      List<AttackAICapabilityUCMutSet> result = new List<AttackAICapabilityUCMutSet>(rootIncarnation.incarnationsAttackAICapabilityUCMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsAttackAICapabilityUCMutSet.Keys) {
        result.Add(new AttackAICapabilityUCMutSet(this, id));
      }
      return result;
    }
    public bool AttackAICapabilityUCMutSetExists(int id) {
      return rootIncarnation.incarnationsAttackAICapabilityUCMutSet.ContainsKey(id);
    }
    public void CheckHasAttackAICapabilityUCMutSet(AttackAICapabilityUCMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasAttackAICapabilityUCMutSet(thing.id);
    }
    public void CheckHasAttackAICapabilityUCMutSet(int id) {
      if (!rootIncarnation.incarnationsAttackAICapabilityUCMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid AttackAICapabilityUCMutSet}!");
      }
    }
    public AttackAICapabilityUCMutSet EffectAttackAICapabilityUCMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new AttackAICapabilityUCMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateAttackAICapabilityUCMutSet(id, incarnation);
      this.rootIncarnation.hash += GetAttackAICapabilityUCMutSetHash(id, rootIncarnation.version, incarnation);
      return new AttackAICapabilityUCMutSet(this, id);
    }
    public void EffectInternalCreateAttackAICapabilityUCMutSet(int id, AttackAICapabilityUCMutSetIncarnation incarnation) {
      var effect = new AttackAICapabilityUCMutSetCreateEffect(id, incarnation);
      rootIncarnation.incarnationsAttackAICapabilityUCMutSet
          .Add(
              id,
              new VersionAndIncarnation<AttackAICapabilityUCMutSetIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastAttackAICapabilityUCMutSetEffect(id, effect);
    }
    public void EffectAttackAICapabilityUCMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new AttackAICapabilityUCMutSetDeleteEffect(id);
      BroadcastAttackAICapabilityUCMutSetEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsAttackAICapabilityUCMutSet[id];
      this.rootIncarnation.hash -=
          GetAttackAICapabilityUCMutSetHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsAttackAICapabilityUCMutSet.Remove(id);
    }

       
    public void EffectAttackAICapabilityUCMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasAttackAICapabilityUCMutSet(setId);
      CheckHasAttackAICapabilityUC(elementId);

      var effect = new AttackAICapabilityUCMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsAttackAICapabilityUCMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
        this.rootIncarnation.hash += setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new AttackAICapabilityUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsAttackAICapabilityUCMutSet[setId] =
            new VersionAndIncarnation<AttackAICapabilityUCMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetAttackAICapabilityUCMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetAttackAICapabilityUCMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastAttackAICapabilityUCMutSetEffect(setId, effect);
    }
    public void EffectAttackAICapabilityUCMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasAttackAICapabilityUCMutSet(setId);
      CheckHasAttackAICapabilityUC(elementId);

      var effect = new AttackAICapabilityUCMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsAttackAICapabilityUCMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        this.rootIncarnation.hash -= setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new AttackAICapabilityUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsAttackAICapabilityUCMutSet[setId] =
            new VersionAndIncarnation<AttackAICapabilityUCMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetAttackAICapabilityUCMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetAttackAICapabilityUCMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastAttackAICapabilityUCMutSetEffect(setId, effect);
    }

       
    public void AddAttackAICapabilityUCMutSetObserver(int id, IAttackAICapabilityUCMutSetEffectObserver observer) {
      List<IAttackAICapabilityUCMutSetEffectObserver> obsies;
      if (!observersAttackAICapabilityUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IAttackAICapabilityUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersAttackAICapabilityUCMutSet[id] = obsies;
    }

    public void RemoveAttackAICapabilityUCMutSetObserver(int id, IAttackAICapabilityUCMutSetEffectObserver observer) {
      if (observersAttackAICapabilityUCMutSet.ContainsKey(id)) {
        var list = observersAttackAICapabilityUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersAttackAICapabilityUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
    public void BroadcastAttackAICapabilityUCMutSetEffect(int id, IAttackAICapabilityUCMutSetEffect effect) {
      if (observersAttackAICapabilityUCMutSet.ContainsKey(0)) {
        foreach (var observer in new List<IAttackAICapabilityUCMutSetEffectObserver>(observersAttackAICapabilityUCMutSet[0])) {
          observer.OnAttackAICapabilityUCMutSetEffect(effect);
        }
      }
      if (observersAttackAICapabilityUCMutSet.ContainsKey(id)) {
        foreach (var observer in new List<IAttackAICapabilityUCMutSetEffectObserver>(observersAttackAICapabilityUCMutSet[id])) {
          observer.OnAttackAICapabilityUCMutSetEffect(effect);
        }
      }
    }

    public int GetShieldingUCMutSetHash(int id, int version, ShieldingUCMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public ShieldingUCMutSetIncarnation GetShieldingUCMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsShieldingUCMutSet[id].incarnation;
    }
    public ShieldingUCMutSet GetShieldingUCMutSet(int id) {
      return new ShieldingUCMutSet(this, id);
    }
    public List<ShieldingUCMutSet> AllShieldingUCMutSet() {
      List<ShieldingUCMutSet> result = new List<ShieldingUCMutSet>(rootIncarnation.incarnationsShieldingUCMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsShieldingUCMutSet.Keys) {
        result.Add(new ShieldingUCMutSet(this, id));
      }
      return result;
    }
    public bool ShieldingUCMutSetExists(int id) {
      return rootIncarnation.incarnationsShieldingUCMutSet.ContainsKey(id);
    }
    public void CheckHasShieldingUCMutSet(ShieldingUCMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasShieldingUCMutSet(thing.id);
    }
    public void CheckHasShieldingUCMutSet(int id) {
      if (!rootIncarnation.incarnationsShieldingUCMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid ShieldingUCMutSet}!");
      }
    }
    public ShieldingUCMutSet EffectShieldingUCMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new ShieldingUCMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateShieldingUCMutSet(id, incarnation);
      this.rootIncarnation.hash += GetShieldingUCMutSetHash(id, rootIncarnation.version, incarnation);
      return new ShieldingUCMutSet(this, id);
    }
    public void EffectInternalCreateShieldingUCMutSet(int id, ShieldingUCMutSetIncarnation incarnation) {
      var effect = new ShieldingUCMutSetCreateEffect(id, incarnation);
      rootIncarnation.incarnationsShieldingUCMutSet
          .Add(
              id,
              new VersionAndIncarnation<ShieldingUCMutSetIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastShieldingUCMutSetEffect(id, effect);
    }
    public void EffectShieldingUCMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new ShieldingUCMutSetDeleteEffect(id);
      BroadcastShieldingUCMutSetEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsShieldingUCMutSet[id];
      this.rootIncarnation.hash -=
          GetShieldingUCMutSetHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsShieldingUCMutSet.Remove(id);
    }

       
    public void EffectShieldingUCMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasShieldingUCMutSet(setId);
      CheckHasShieldingUC(elementId);

      var effect = new ShieldingUCMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsShieldingUCMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
        this.rootIncarnation.hash += setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new ShieldingUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsShieldingUCMutSet[setId] =
            new VersionAndIncarnation<ShieldingUCMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetShieldingUCMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetShieldingUCMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastShieldingUCMutSetEffect(setId, effect);
    }
    public void EffectShieldingUCMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasShieldingUCMutSet(setId);
      CheckHasShieldingUC(elementId);

      var effect = new ShieldingUCMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsShieldingUCMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        this.rootIncarnation.hash -= setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new ShieldingUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsShieldingUCMutSet[setId] =
            new VersionAndIncarnation<ShieldingUCMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetShieldingUCMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetShieldingUCMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastShieldingUCMutSetEffect(setId, effect);
    }

       
    public void AddShieldingUCMutSetObserver(int id, IShieldingUCMutSetEffectObserver observer) {
      List<IShieldingUCMutSetEffectObserver> obsies;
      if (!observersShieldingUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IShieldingUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersShieldingUCMutSet[id] = obsies;
    }

    public void RemoveShieldingUCMutSetObserver(int id, IShieldingUCMutSetEffectObserver observer) {
      if (observersShieldingUCMutSet.ContainsKey(id)) {
        var list = observersShieldingUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersShieldingUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
    public void BroadcastShieldingUCMutSetEffect(int id, IShieldingUCMutSetEffect effect) {
      if (observersShieldingUCMutSet.ContainsKey(0)) {
        foreach (var observer in new List<IShieldingUCMutSetEffectObserver>(observersShieldingUCMutSet[0])) {
          observer.OnShieldingUCMutSetEffect(effect);
        }
      }
      if (observersShieldingUCMutSet.ContainsKey(id)) {
        foreach (var observer in new List<IShieldingUCMutSetEffectObserver>(observersShieldingUCMutSet[id])) {
          observer.OnShieldingUCMutSetEffect(effect);
        }
      }
    }

    public int GetBidingOperationUCMutSetHash(int id, int version, BidingOperationUCMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public BidingOperationUCMutSetIncarnation GetBidingOperationUCMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsBidingOperationUCMutSet[id].incarnation;
    }
    public BidingOperationUCMutSet GetBidingOperationUCMutSet(int id) {
      return new BidingOperationUCMutSet(this, id);
    }
    public List<BidingOperationUCMutSet> AllBidingOperationUCMutSet() {
      List<BidingOperationUCMutSet> result = new List<BidingOperationUCMutSet>(rootIncarnation.incarnationsBidingOperationUCMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsBidingOperationUCMutSet.Keys) {
        result.Add(new BidingOperationUCMutSet(this, id));
      }
      return result;
    }
    public bool BidingOperationUCMutSetExists(int id) {
      return rootIncarnation.incarnationsBidingOperationUCMutSet.ContainsKey(id);
    }
    public void CheckHasBidingOperationUCMutSet(BidingOperationUCMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasBidingOperationUCMutSet(thing.id);
    }
    public void CheckHasBidingOperationUCMutSet(int id) {
      if (!rootIncarnation.incarnationsBidingOperationUCMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid BidingOperationUCMutSet}!");
      }
    }
    public BidingOperationUCMutSet EffectBidingOperationUCMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new BidingOperationUCMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateBidingOperationUCMutSet(id, incarnation);
      this.rootIncarnation.hash += GetBidingOperationUCMutSetHash(id, rootIncarnation.version, incarnation);
      return new BidingOperationUCMutSet(this, id);
    }
    public void EffectInternalCreateBidingOperationUCMutSet(int id, BidingOperationUCMutSetIncarnation incarnation) {
      var effect = new BidingOperationUCMutSetCreateEffect(id, incarnation);
      rootIncarnation.incarnationsBidingOperationUCMutSet
          .Add(
              id,
              new VersionAndIncarnation<BidingOperationUCMutSetIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastBidingOperationUCMutSetEffect(id, effect);
    }
    public void EffectBidingOperationUCMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new BidingOperationUCMutSetDeleteEffect(id);
      BroadcastBidingOperationUCMutSetEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsBidingOperationUCMutSet[id];
      this.rootIncarnation.hash -=
          GetBidingOperationUCMutSetHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsBidingOperationUCMutSet.Remove(id);
    }

       
    public void EffectBidingOperationUCMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasBidingOperationUCMutSet(setId);
      CheckHasBidingOperationUC(elementId);

      var effect = new BidingOperationUCMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsBidingOperationUCMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
        this.rootIncarnation.hash += setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new BidingOperationUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsBidingOperationUCMutSet[setId] =
            new VersionAndIncarnation<BidingOperationUCMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetBidingOperationUCMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetBidingOperationUCMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastBidingOperationUCMutSetEffect(setId, effect);
    }
    public void EffectBidingOperationUCMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasBidingOperationUCMutSet(setId);
      CheckHasBidingOperationUC(elementId);

      var effect = new BidingOperationUCMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsBidingOperationUCMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        this.rootIncarnation.hash -= setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new BidingOperationUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsBidingOperationUCMutSet[setId] =
            new VersionAndIncarnation<BidingOperationUCMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetBidingOperationUCMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetBidingOperationUCMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastBidingOperationUCMutSetEffect(setId, effect);
    }

       
    public void AddBidingOperationUCMutSetObserver(int id, IBidingOperationUCMutSetEffectObserver observer) {
      List<IBidingOperationUCMutSetEffectObserver> obsies;
      if (!observersBidingOperationUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IBidingOperationUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersBidingOperationUCMutSet[id] = obsies;
    }

    public void RemoveBidingOperationUCMutSetObserver(int id, IBidingOperationUCMutSetEffectObserver observer) {
      if (observersBidingOperationUCMutSet.ContainsKey(id)) {
        var list = observersBidingOperationUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersBidingOperationUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
    public void BroadcastBidingOperationUCMutSetEffect(int id, IBidingOperationUCMutSetEffect effect) {
      if (observersBidingOperationUCMutSet.ContainsKey(0)) {
        foreach (var observer in new List<IBidingOperationUCMutSetEffectObserver>(observersBidingOperationUCMutSet[0])) {
          observer.OnBidingOperationUCMutSetEffect(effect);
        }
      }
      if (observersBidingOperationUCMutSet.ContainsKey(id)) {
        foreach (var observer in new List<IBidingOperationUCMutSetEffectObserver>(observersBidingOperationUCMutSet[id])) {
          observer.OnBidingOperationUCMutSetEffect(effect);
        }
      }
    }

    public int GetGlaiveMutSetHash(int id, int version, GlaiveMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public GlaiveMutSetIncarnation GetGlaiveMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsGlaiveMutSet[id].incarnation;
    }
    public GlaiveMutSet GetGlaiveMutSet(int id) {
      return new GlaiveMutSet(this, id);
    }
    public List<GlaiveMutSet> AllGlaiveMutSet() {
      List<GlaiveMutSet> result = new List<GlaiveMutSet>(rootIncarnation.incarnationsGlaiveMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsGlaiveMutSet.Keys) {
        result.Add(new GlaiveMutSet(this, id));
      }
      return result;
    }
    public bool GlaiveMutSetExists(int id) {
      return rootIncarnation.incarnationsGlaiveMutSet.ContainsKey(id);
    }
    public void CheckHasGlaiveMutSet(GlaiveMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasGlaiveMutSet(thing.id);
    }
    public void CheckHasGlaiveMutSet(int id) {
      if (!rootIncarnation.incarnationsGlaiveMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid GlaiveMutSet}!");
      }
    }
    public GlaiveMutSet EffectGlaiveMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new GlaiveMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateGlaiveMutSet(id, incarnation);
      this.rootIncarnation.hash += GetGlaiveMutSetHash(id, rootIncarnation.version, incarnation);
      return new GlaiveMutSet(this, id);
    }
    public void EffectInternalCreateGlaiveMutSet(int id, GlaiveMutSetIncarnation incarnation) {
      var effect = new GlaiveMutSetCreateEffect(id, incarnation);
      rootIncarnation.incarnationsGlaiveMutSet
          .Add(
              id,
              new VersionAndIncarnation<GlaiveMutSetIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastGlaiveMutSetEffect(id, effect);
    }
    public void EffectGlaiveMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new GlaiveMutSetDeleteEffect(id);
      BroadcastGlaiveMutSetEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsGlaiveMutSet[id];
      this.rootIncarnation.hash -=
          GetGlaiveMutSetHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsGlaiveMutSet.Remove(id);
    }

       
    public void EffectGlaiveMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasGlaiveMutSet(setId);
      CheckHasGlaive(elementId);

      var effect = new GlaiveMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsGlaiveMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
        this.rootIncarnation.hash += setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new GlaiveMutSetIncarnation(newMap);
        rootIncarnation.incarnationsGlaiveMutSet[setId] =
            new VersionAndIncarnation<GlaiveMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetGlaiveMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetGlaiveMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastGlaiveMutSetEffect(setId, effect);
    }
    public void EffectGlaiveMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasGlaiveMutSet(setId);
      CheckHasGlaive(elementId);

      var effect = new GlaiveMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsGlaiveMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        this.rootIncarnation.hash -= setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new GlaiveMutSetIncarnation(newMap);
        rootIncarnation.incarnationsGlaiveMutSet[setId] =
            new VersionAndIncarnation<GlaiveMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetGlaiveMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetGlaiveMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastGlaiveMutSetEffect(setId, effect);
    }

       
    public void AddGlaiveMutSetObserver(int id, IGlaiveMutSetEffectObserver observer) {
      List<IGlaiveMutSetEffectObserver> obsies;
      if (!observersGlaiveMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IGlaiveMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersGlaiveMutSet[id] = obsies;
    }

    public void RemoveGlaiveMutSetObserver(int id, IGlaiveMutSetEffectObserver observer) {
      if (observersGlaiveMutSet.ContainsKey(id)) {
        var list = observersGlaiveMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersGlaiveMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
    public void BroadcastGlaiveMutSetEffect(int id, IGlaiveMutSetEffect effect) {
      if (observersGlaiveMutSet.ContainsKey(0)) {
        foreach (var observer in new List<IGlaiveMutSetEffectObserver>(observersGlaiveMutSet[0])) {
          observer.OnGlaiveMutSetEffect(effect);
        }
      }
      if (observersGlaiveMutSet.ContainsKey(id)) {
        foreach (var observer in new List<IGlaiveMutSetEffectObserver>(observersGlaiveMutSet[id])) {
          observer.OnGlaiveMutSetEffect(effect);
        }
      }
    }

    public int GetArmorMutSetHash(int id, int version, ArmorMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public ArmorMutSetIncarnation GetArmorMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsArmorMutSet[id].incarnation;
    }
    public ArmorMutSet GetArmorMutSet(int id) {
      return new ArmorMutSet(this, id);
    }
    public List<ArmorMutSet> AllArmorMutSet() {
      List<ArmorMutSet> result = new List<ArmorMutSet>(rootIncarnation.incarnationsArmorMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsArmorMutSet.Keys) {
        result.Add(new ArmorMutSet(this, id));
      }
      return result;
    }
    public bool ArmorMutSetExists(int id) {
      return rootIncarnation.incarnationsArmorMutSet.ContainsKey(id);
    }
    public void CheckHasArmorMutSet(ArmorMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasArmorMutSet(thing.id);
    }
    public void CheckHasArmorMutSet(int id) {
      if (!rootIncarnation.incarnationsArmorMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid ArmorMutSet}!");
      }
    }
    public ArmorMutSet EffectArmorMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new ArmorMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateArmorMutSet(id, incarnation);
      this.rootIncarnation.hash += GetArmorMutSetHash(id, rootIncarnation.version, incarnation);
      return new ArmorMutSet(this, id);
    }
    public void EffectInternalCreateArmorMutSet(int id, ArmorMutSetIncarnation incarnation) {
      var effect = new ArmorMutSetCreateEffect(id, incarnation);
      rootIncarnation.incarnationsArmorMutSet
          .Add(
              id,
              new VersionAndIncarnation<ArmorMutSetIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastArmorMutSetEffect(id, effect);
    }
    public void EffectArmorMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new ArmorMutSetDeleteEffect(id);
      BroadcastArmorMutSetEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsArmorMutSet[id];
      this.rootIncarnation.hash -=
          GetArmorMutSetHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsArmorMutSet.Remove(id);
    }

       
    public void EffectArmorMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasArmorMutSet(setId);
      CheckHasArmor(elementId);

      var effect = new ArmorMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsArmorMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
        this.rootIncarnation.hash += setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new ArmorMutSetIncarnation(newMap);
        rootIncarnation.incarnationsArmorMutSet[setId] =
            new VersionAndIncarnation<ArmorMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetArmorMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetArmorMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastArmorMutSetEffect(setId, effect);
    }
    public void EffectArmorMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasArmorMutSet(setId);
      CheckHasArmor(elementId);

      var effect = new ArmorMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsArmorMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        this.rootIncarnation.hash -= setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new ArmorMutSetIncarnation(newMap);
        rootIncarnation.incarnationsArmorMutSet[setId] =
            new VersionAndIncarnation<ArmorMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetArmorMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetArmorMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastArmorMutSetEffect(setId, effect);
    }

       
    public void AddArmorMutSetObserver(int id, IArmorMutSetEffectObserver observer) {
      List<IArmorMutSetEffectObserver> obsies;
      if (!observersArmorMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IArmorMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersArmorMutSet[id] = obsies;
    }

    public void RemoveArmorMutSetObserver(int id, IArmorMutSetEffectObserver observer) {
      if (observersArmorMutSet.ContainsKey(id)) {
        var list = observersArmorMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersArmorMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
    public void BroadcastArmorMutSetEffect(int id, IArmorMutSetEffect effect) {
      if (observersArmorMutSet.ContainsKey(0)) {
        foreach (var observer in new List<IArmorMutSetEffectObserver>(observersArmorMutSet[0])) {
          observer.OnArmorMutSetEffect(effect);
        }
      }
      if (observersArmorMutSet.ContainsKey(id)) {
        foreach (var observer in new List<IArmorMutSetEffectObserver>(observersArmorMutSet[id])) {
          observer.OnArmorMutSetEffect(effect);
        }
      }
    }

    public int GetDecorativeTerrainTileComponentMutSetHash(int id, int version, DecorativeTerrainTileComponentMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public DecorativeTerrainTileComponentMutSetIncarnation GetDecorativeTerrainTileComponentMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet[id].incarnation;
    }
    public DecorativeTerrainTileComponentMutSet GetDecorativeTerrainTileComponentMutSet(int id) {
      return new DecorativeTerrainTileComponentMutSet(this, id);
    }
    public List<DecorativeTerrainTileComponentMutSet> AllDecorativeTerrainTileComponentMutSet() {
      List<DecorativeTerrainTileComponentMutSet> result = new List<DecorativeTerrainTileComponentMutSet>(rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet.Keys) {
        result.Add(new DecorativeTerrainTileComponentMutSet(this, id));
      }
      return result;
    }
    public bool DecorativeTerrainTileComponentMutSetExists(int id) {
      return rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet.ContainsKey(id);
    }
    public void CheckHasDecorativeTerrainTileComponentMutSet(DecorativeTerrainTileComponentMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasDecorativeTerrainTileComponentMutSet(thing.id);
    }
    public void CheckHasDecorativeTerrainTileComponentMutSet(int id) {
      if (!rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid DecorativeTerrainTileComponentMutSet}!");
      }
    }
    public DecorativeTerrainTileComponentMutSet EffectDecorativeTerrainTileComponentMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new DecorativeTerrainTileComponentMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateDecorativeTerrainTileComponentMutSet(id, incarnation);
      this.rootIncarnation.hash += GetDecorativeTerrainTileComponentMutSetHash(id, rootIncarnation.version, incarnation);
      return new DecorativeTerrainTileComponentMutSet(this, id);
    }
    public void EffectInternalCreateDecorativeTerrainTileComponentMutSet(int id, DecorativeTerrainTileComponentMutSetIncarnation incarnation) {
      var effect = new DecorativeTerrainTileComponentMutSetCreateEffect(id, incarnation);
      rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet
          .Add(
              id,
              new VersionAndIncarnation<DecorativeTerrainTileComponentMutSetIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastDecorativeTerrainTileComponentMutSetEffect(id, effect);
    }
    public void EffectDecorativeTerrainTileComponentMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new DecorativeTerrainTileComponentMutSetDeleteEffect(id);
      BroadcastDecorativeTerrainTileComponentMutSetEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet[id];
      this.rootIncarnation.hash -=
          GetDecorativeTerrainTileComponentMutSetHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet.Remove(id);
    }

       
    public void EffectDecorativeTerrainTileComponentMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasDecorativeTerrainTileComponentMutSet(setId);
      CheckHasDecorativeTerrainTileComponent(elementId);

      var effect = new DecorativeTerrainTileComponentMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
        this.rootIncarnation.hash += setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new DecorativeTerrainTileComponentMutSetIncarnation(newMap);
        rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet[setId] =
            new VersionAndIncarnation<DecorativeTerrainTileComponentMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetDecorativeTerrainTileComponentMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetDecorativeTerrainTileComponentMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastDecorativeTerrainTileComponentMutSetEffect(setId, effect);
    }
    public void EffectDecorativeTerrainTileComponentMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasDecorativeTerrainTileComponentMutSet(setId);
      CheckHasDecorativeTerrainTileComponent(elementId);

      var effect = new DecorativeTerrainTileComponentMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        this.rootIncarnation.hash -= setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new DecorativeTerrainTileComponentMutSetIncarnation(newMap);
        rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet[setId] =
            new VersionAndIncarnation<DecorativeTerrainTileComponentMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetDecorativeTerrainTileComponentMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetDecorativeTerrainTileComponentMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastDecorativeTerrainTileComponentMutSetEffect(setId, effect);
    }

       
    public void AddDecorativeTerrainTileComponentMutSetObserver(int id, IDecorativeTerrainTileComponentMutSetEffectObserver observer) {
      List<IDecorativeTerrainTileComponentMutSetEffectObserver> obsies;
      if (!observersDecorativeTerrainTileComponentMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IDecorativeTerrainTileComponentMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersDecorativeTerrainTileComponentMutSet[id] = obsies;
    }

    public void RemoveDecorativeTerrainTileComponentMutSetObserver(int id, IDecorativeTerrainTileComponentMutSetEffectObserver observer) {
      if (observersDecorativeTerrainTileComponentMutSet.ContainsKey(id)) {
        var list = observersDecorativeTerrainTileComponentMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersDecorativeTerrainTileComponentMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
    public void BroadcastDecorativeTerrainTileComponentMutSetEffect(int id, IDecorativeTerrainTileComponentMutSetEffect effect) {
      if (observersDecorativeTerrainTileComponentMutSet.ContainsKey(0)) {
        foreach (var observer in new List<IDecorativeTerrainTileComponentMutSetEffectObserver>(observersDecorativeTerrainTileComponentMutSet[0])) {
          observer.OnDecorativeTerrainTileComponentMutSetEffect(effect);
        }
      }
      if (observersDecorativeTerrainTileComponentMutSet.ContainsKey(id)) {
        foreach (var observer in new List<IDecorativeTerrainTileComponentMutSetEffectObserver>(observersDecorativeTerrainTileComponentMutSet[id])) {
          observer.OnDecorativeTerrainTileComponentMutSetEffect(effect);
        }
      }
    }

    public int GetUpStaircaseTerrainTileComponentMutSetHash(int id, int version, UpStaircaseTerrainTileComponentMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public UpStaircaseTerrainTileComponentMutSetIncarnation GetUpStaircaseTerrainTileComponentMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet[id].incarnation;
    }
    public UpStaircaseTerrainTileComponentMutSet GetUpStaircaseTerrainTileComponentMutSet(int id) {
      return new UpStaircaseTerrainTileComponentMutSet(this, id);
    }
    public List<UpStaircaseTerrainTileComponentMutSet> AllUpStaircaseTerrainTileComponentMutSet() {
      List<UpStaircaseTerrainTileComponentMutSet> result = new List<UpStaircaseTerrainTileComponentMutSet>(rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet.Keys) {
        result.Add(new UpStaircaseTerrainTileComponentMutSet(this, id));
      }
      return result;
    }
    public bool UpStaircaseTerrainTileComponentMutSetExists(int id) {
      return rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet.ContainsKey(id);
    }
    public void CheckHasUpStaircaseTerrainTileComponentMutSet(UpStaircaseTerrainTileComponentMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasUpStaircaseTerrainTileComponentMutSet(thing.id);
    }
    public void CheckHasUpStaircaseTerrainTileComponentMutSet(int id) {
      if (!rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid UpStaircaseTerrainTileComponentMutSet}!");
      }
    }
    public UpStaircaseTerrainTileComponentMutSet EffectUpStaircaseTerrainTileComponentMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new UpStaircaseTerrainTileComponentMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateUpStaircaseTerrainTileComponentMutSet(id, incarnation);
      this.rootIncarnation.hash += GetUpStaircaseTerrainTileComponentMutSetHash(id, rootIncarnation.version, incarnation);
      return new UpStaircaseTerrainTileComponentMutSet(this, id);
    }
    public void EffectInternalCreateUpStaircaseTerrainTileComponentMutSet(int id, UpStaircaseTerrainTileComponentMutSetIncarnation incarnation) {
      var effect = new UpStaircaseTerrainTileComponentMutSetCreateEffect(id, incarnation);
      rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet
          .Add(
              id,
              new VersionAndIncarnation<UpStaircaseTerrainTileComponentMutSetIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastUpStaircaseTerrainTileComponentMutSetEffect(id, effect);
    }
    public void EffectUpStaircaseTerrainTileComponentMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new UpStaircaseTerrainTileComponentMutSetDeleteEffect(id);
      BroadcastUpStaircaseTerrainTileComponentMutSetEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet[id];
      this.rootIncarnation.hash -=
          GetUpStaircaseTerrainTileComponentMutSetHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet.Remove(id);
    }

       
    public void EffectUpStaircaseTerrainTileComponentMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasUpStaircaseTerrainTileComponentMutSet(setId);
      CheckHasUpStaircaseTerrainTileComponent(elementId);

      var effect = new UpStaircaseTerrainTileComponentMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
        this.rootIncarnation.hash += setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new UpStaircaseTerrainTileComponentMutSetIncarnation(newMap);
        rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet[setId] =
            new VersionAndIncarnation<UpStaircaseTerrainTileComponentMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetUpStaircaseTerrainTileComponentMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetUpStaircaseTerrainTileComponentMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastUpStaircaseTerrainTileComponentMutSetEffect(setId, effect);
    }
    public void EffectUpStaircaseTerrainTileComponentMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasUpStaircaseTerrainTileComponentMutSet(setId);
      CheckHasUpStaircaseTerrainTileComponent(elementId);

      var effect = new UpStaircaseTerrainTileComponentMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        this.rootIncarnation.hash -= setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new UpStaircaseTerrainTileComponentMutSetIncarnation(newMap);
        rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet[setId] =
            new VersionAndIncarnation<UpStaircaseTerrainTileComponentMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetUpStaircaseTerrainTileComponentMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetUpStaircaseTerrainTileComponentMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastUpStaircaseTerrainTileComponentMutSetEffect(setId, effect);
    }

       
    public void AddUpStaircaseTerrainTileComponentMutSetObserver(int id, IUpStaircaseTerrainTileComponentMutSetEffectObserver observer) {
      List<IUpStaircaseTerrainTileComponentMutSetEffectObserver> obsies;
      if (!observersUpStaircaseTerrainTileComponentMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IUpStaircaseTerrainTileComponentMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersUpStaircaseTerrainTileComponentMutSet[id] = obsies;
    }

    public void RemoveUpStaircaseTerrainTileComponentMutSetObserver(int id, IUpStaircaseTerrainTileComponentMutSetEffectObserver observer) {
      if (observersUpStaircaseTerrainTileComponentMutSet.ContainsKey(id)) {
        var list = observersUpStaircaseTerrainTileComponentMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersUpStaircaseTerrainTileComponentMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
    public void BroadcastUpStaircaseTerrainTileComponentMutSetEffect(int id, IUpStaircaseTerrainTileComponentMutSetEffect effect) {
      if (observersUpStaircaseTerrainTileComponentMutSet.ContainsKey(0)) {
        foreach (var observer in new List<IUpStaircaseTerrainTileComponentMutSetEffectObserver>(observersUpStaircaseTerrainTileComponentMutSet[0])) {
          observer.OnUpStaircaseTerrainTileComponentMutSetEffect(effect);
        }
      }
      if (observersUpStaircaseTerrainTileComponentMutSet.ContainsKey(id)) {
        foreach (var observer in new List<IUpStaircaseTerrainTileComponentMutSetEffectObserver>(observersUpStaircaseTerrainTileComponentMutSet[id])) {
          observer.OnUpStaircaseTerrainTileComponentMutSetEffect(effect);
        }
      }
    }

    public int GetDownStaircaseTerrainTileComponentMutSetHash(int id, int version, DownStaircaseTerrainTileComponentMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public DownStaircaseTerrainTileComponentMutSetIncarnation GetDownStaircaseTerrainTileComponentMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet[id].incarnation;
    }
    public DownStaircaseTerrainTileComponentMutSet GetDownStaircaseTerrainTileComponentMutSet(int id) {
      return new DownStaircaseTerrainTileComponentMutSet(this, id);
    }
    public List<DownStaircaseTerrainTileComponentMutSet> AllDownStaircaseTerrainTileComponentMutSet() {
      List<DownStaircaseTerrainTileComponentMutSet> result = new List<DownStaircaseTerrainTileComponentMutSet>(rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet.Keys) {
        result.Add(new DownStaircaseTerrainTileComponentMutSet(this, id));
      }
      return result;
    }
    public bool DownStaircaseTerrainTileComponentMutSetExists(int id) {
      return rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet.ContainsKey(id);
    }
    public void CheckHasDownStaircaseTerrainTileComponentMutSet(DownStaircaseTerrainTileComponentMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasDownStaircaseTerrainTileComponentMutSet(thing.id);
    }
    public void CheckHasDownStaircaseTerrainTileComponentMutSet(int id) {
      if (!rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid DownStaircaseTerrainTileComponentMutSet}!");
      }
    }
    public DownStaircaseTerrainTileComponentMutSet EffectDownStaircaseTerrainTileComponentMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new DownStaircaseTerrainTileComponentMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateDownStaircaseTerrainTileComponentMutSet(id, incarnation);
      this.rootIncarnation.hash += GetDownStaircaseTerrainTileComponentMutSetHash(id, rootIncarnation.version, incarnation);
      return new DownStaircaseTerrainTileComponentMutSet(this, id);
    }
    public void EffectInternalCreateDownStaircaseTerrainTileComponentMutSet(int id, DownStaircaseTerrainTileComponentMutSetIncarnation incarnation) {
      var effect = new DownStaircaseTerrainTileComponentMutSetCreateEffect(id, incarnation);
      rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet
          .Add(
              id,
              new VersionAndIncarnation<DownStaircaseTerrainTileComponentMutSetIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastDownStaircaseTerrainTileComponentMutSetEffect(id, effect);
    }
    public void EffectDownStaircaseTerrainTileComponentMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new DownStaircaseTerrainTileComponentMutSetDeleteEffect(id);
      BroadcastDownStaircaseTerrainTileComponentMutSetEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet[id];
      this.rootIncarnation.hash -=
          GetDownStaircaseTerrainTileComponentMutSetHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet.Remove(id);
    }

       
    public void EffectDownStaircaseTerrainTileComponentMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasDownStaircaseTerrainTileComponentMutSet(setId);
      CheckHasDownStaircaseTerrainTileComponent(elementId);

      var effect = new DownStaircaseTerrainTileComponentMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
        this.rootIncarnation.hash += setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new DownStaircaseTerrainTileComponentMutSetIncarnation(newMap);
        rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet[setId] =
            new VersionAndIncarnation<DownStaircaseTerrainTileComponentMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetDownStaircaseTerrainTileComponentMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetDownStaircaseTerrainTileComponentMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastDownStaircaseTerrainTileComponentMutSetEffect(setId, effect);
    }
    public void EffectDownStaircaseTerrainTileComponentMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasDownStaircaseTerrainTileComponentMutSet(setId);
      CheckHasDownStaircaseTerrainTileComponent(elementId);

      var effect = new DownStaircaseTerrainTileComponentMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        this.rootIncarnation.hash -= setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new DownStaircaseTerrainTileComponentMutSetIncarnation(newMap);
        rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet[setId] =
            new VersionAndIncarnation<DownStaircaseTerrainTileComponentMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetDownStaircaseTerrainTileComponentMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetDownStaircaseTerrainTileComponentMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastDownStaircaseTerrainTileComponentMutSetEffect(setId, effect);
    }

       
    public void AddDownStaircaseTerrainTileComponentMutSetObserver(int id, IDownStaircaseTerrainTileComponentMutSetEffectObserver observer) {
      List<IDownStaircaseTerrainTileComponentMutSetEffectObserver> obsies;
      if (!observersDownStaircaseTerrainTileComponentMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IDownStaircaseTerrainTileComponentMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersDownStaircaseTerrainTileComponentMutSet[id] = obsies;
    }

    public void RemoveDownStaircaseTerrainTileComponentMutSetObserver(int id, IDownStaircaseTerrainTileComponentMutSetEffectObserver observer) {
      if (observersDownStaircaseTerrainTileComponentMutSet.ContainsKey(id)) {
        var list = observersDownStaircaseTerrainTileComponentMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersDownStaircaseTerrainTileComponentMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
    public void BroadcastDownStaircaseTerrainTileComponentMutSetEffect(int id, IDownStaircaseTerrainTileComponentMutSetEffect effect) {
      if (observersDownStaircaseTerrainTileComponentMutSet.ContainsKey(0)) {
        foreach (var observer in new List<IDownStaircaseTerrainTileComponentMutSetEffectObserver>(observersDownStaircaseTerrainTileComponentMutSet[0])) {
          observer.OnDownStaircaseTerrainTileComponentMutSetEffect(effect);
        }
      }
      if (observersDownStaircaseTerrainTileComponentMutSet.ContainsKey(id)) {
        foreach (var observer in new List<IDownStaircaseTerrainTileComponentMutSetEffectObserver>(observersDownStaircaseTerrainTileComponentMutSet[id])) {
          observer.OnDownStaircaseTerrainTileComponentMutSetEffect(effect);
        }
      }
    }

    public int GetUnitMutSetHash(int id, int version, UnitMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public UnitMutSetIncarnation GetUnitMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsUnitMutSet[id].incarnation;
    }
    public UnitMutSet GetUnitMutSet(int id) {
      return new UnitMutSet(this, id);
    }
    public List<UnitMutSet> AllUnitMutSet() {
      List<UnitMutSet> result = new List<UnitMutSet>(rootIncarnation.incarnationsUnitMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsUnitMutSet.Keys) {
        result.Add(new UnitMutSet(this, id));
      }
      return result;
    }
    public bool UnitMutSetExists(int id) {
      return rootIncarnation.incarnationsUnitMutSet.ContainsKey(id);
    }
    public void CheckHasUnitMutSet(UnitMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasUnitMutSet(thing.id);
    }
    public void CheckHasUnitMutSet(int id) {
      if (!rootIncarnation.incarnationsUnitMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid UnitMutSet}!");
      }
    }
    public UnitMutSet EffectUnitMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new UnitMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateUnitMutSet(id, incarnation);
      this.rootIncarnation.hash += GetUnitMutSetHash(id, rootIncarnation.version, incarnation);
      return new UnitMutSet(this, id);
    }
    public void EffectInternalCreateUnitMutSet(int id, UnitMutSetIncarnation incarnation) {
      var effect = new UnitMutSetCreateEffect(id, incarnation);
      rootIncarnation.incarnationsUnitMutSet
          .Add(
              id,
              new VersionAndIncarnation<UnitMutSetIncarnation>(
                  rootIncarnation.version,
                  incarnation));
      BroadcastUnitMutSetEffect(id, effect);
    }
    public void EffectUnitMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new UnitMutSetDeleteEffect(id);
      BroadcastUnitMutSetEffect(id, effect);
      var versionAndIncarnation = rootIncarnation.incarnationsUnitMutSet[id];
      this.rootIncarnation.hash -=
          GetUnitMutSetHash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
      rootIncarnation.incarnationsUnitMutSet.Remove(id);
    }

       
    public void EffectUnitMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasUnitMutSet(setId);
      CheckHasUnit(elementId);

      var effect = new UnitMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsUnitMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
        this.rootIncarnation.hash += setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new UnitMutSetIncarnation(newMap);
        rootIncarnation.incarnationsUnitMutSet[setId] =
            new VersionAndIncarnation<UnitMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
        this.rootIncarnation.hash -= GetUnitMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetUnitMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastUnitMutSetEffect(setId, effect);
    }
    public void EffectUnitMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasUnitMutSet(setId);
      CheckHasUnit(elementId);

      var effect = new UnitMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsUnitMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        this.rootIncarnation.hash -= setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new UnitMutSetIncarnation(newMap);
        rootIncarnation.incarnationsUnitMutSet[setId] =
            new VersionAndIncarnation<UnitMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
        this.rootIncarnation.hash -= GetUnitMutSetHash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
        this.rootIncarnation.hash += GetUnitMutSetHash(setId, rootIncarnation.version, newIncarnation);
      }
      BroadcastUnitMutSetEffect(setId, effect);
    }

       
    public void AddUnitMutSetObserver(int id, IUnitMutSetEffectObserver observer) {
      List<IUnitMutSetEffectObserver> obsies;
      if (!observersUnitMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IUnitMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersUnitMutSet[id] = obsies;
    }

    public void RemoveUnitMutSetObserver(int id, IUnitMutSetEffectObserver observer) {
      if (observersUnitMutSet.ContainsKey(id)) {
        var list = observersUnitMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersUnitMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
    public void BroadcastUnitMutSetEffect(int id, IUnitMutSetEffect effect) {
      if (observersUnitMutSet.ContainsKey(0)) {
        foreach (var observer in new List<IUnitMutSetEffectObserver>(observersUnitMutSet[0])) {
          observer.OnUnitMutSetEffect(effect);
        }
      }
      if (observersUnitMutSet.ContainsKey(id)) {
        foreach (var observer in new List<IUnitMutSetEffectObserver>(observersUnitMutSet[id])) {
          observer.OnUnitMutSetEffect(effect);
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
    public List<TerrainTileByLocationMutMap> AllTerrainTileByLocationMutMap() {
      List<TerrainTileByLocationMutMap> result = new List<TerrainTileByLocationMutMap>(rootIncarnation.incarnationsTerrainTileByLocationMutMap.Count);
      foreach (var id in rootIncarnation.incarnationsTerrainTileByLocationMutMap.Keys) {
        result.Add(new TerrainTileByLocationMutMap(this, id));
      }
      return result;
    }
    public bool TerrainTileByLocationMutMapExists(int id) {
      return rootIncarnation.incarnationsTerrainTileByLocationMutMap.ContainsKey(id);
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
