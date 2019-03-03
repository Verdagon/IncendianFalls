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

  readonly SortedDictionary<int, List<ILevelEffectObserver>> observersForLevel =
      new SortedDictionary<int, List<ILevelEffectObserver>>();
  readonly List<LevelCreateEffect> effectsLevelCreateEffect =
      new List<LevelCreateEffect>();
  readonly List<LevelDeleteEffect> effectsLevelDeleteEffect =
      new List<LevelDeleteEffect>();

  readonly SortedDictionary<int, List<IItemTerrainTileComponentEffectObserver>> observersForItemTerrainTileComponent =
      new SortedDictionary<int, List<IItemTerrainTileComponentEffectObserver>>();
  readonly List<ItemTerrainTileComponentCreateEffect> effectsItemTerrainTileComponentCreateEffect =
      new List<ItemTerrainTileComponentCreateEffect>();
  readonly List<ItemTerrainTileComponentDeleteEffect> effectsItemTerrainTileComponentDeleteEffect =
      new List<ItemTerrainTileComponentDeleteEffect>();

  readonly SortedDictionary<int, List<IDecorativeTerrainTileComponentEffectObserver>> observersForDecorativeTerrainTileComponent =
      new SortedDictionary<int, List<IDecorativeTerrainTileComponentEffectObserver>>();
  readonly List<DecorativeTerrainTileComponentCreateEffect> effectsDecorativeTerrainTileComponentCreateEffect =
      new List<DecorativeTerrainTileComponentCreateEffect>();
  readonly List<DecorativeTerrainTileComponentDeleteEffect> effectsDecorativeTerrainTileComponentDeleteEffect =
      new List<DecorativeTerrainTileComponentDeleteEffect>();

  readonly SortedDictionary<int, List<IUpStaircaseTerrainTileComponentEffectObserver>> observersForUpStaircaseTerrainTileComponent =
      new SortedDictionary<int, List<IUpStaircaseTerrainTileComponentEffectObserver>>();
  readonly List<UpStaircaseTerrainTileComponentCreateEffect> effectsUpStaircaseTerrainTileComponentCreateEffect =
      new List<UpStaircaseTerrainTileComponentCreateEffect>();
  readonly List<UpStaircaseTerrainTileComponentDeleteEffect> effectsUpStaircaseTerrainTileComponentDeleteEffect =
      new List<UpStaircaseTerrainTileComponentDeleteEffect>();

  readonly SortedDictionary<int, List<IDownStaircaseTerrainTileComponentEffectObserver>> observersForDownStaircaseTerrainTileComponent =
      new SortedDictionary<int, List<IDownStaircaseTerrainTileComponentEffectObserver>>();
  readonly List<DownStaircaseTerrainTileComponentCreateEffect> effectsDownStaircaseTerrainTileComponentCreateEffect =
      new List<DownStaircaseTerrainTileComponentCreateEffect>();
  readonly List<DownStaircaseTerrainTileComponentDeleteEffect> effectsDownStaircaseTerrainTileComponentDeleteEffect =
      new List<DownStaircaseTerrainTileComponentDeleteEffect>();

  readonly SortedDictionary<int, List<ITerrainTileEffectObserver>> observersForTerrainTile =
      new SortedDictionary<int, List<ITerrainTileEffectObserver>>();
  readonly List<TerrainTileCreateEffect> effectsTerrainTileCreateEffect =
      new List<TerrainTileCreateEffect>();
  readonly List<TerrainTileDeleteEffect> effectsTerrainTileDeleteEffect =
      new List<TerrainTileDeleteEffect>();
  readonly List<TerrainTileSetElevationEffect> effectsTerrainTileSetElevationEffect =
      new List<TerrainTileSetElevationEffect>();

  readonly SortedDictionary<int, List<IITerrainTileComponentMutBunchEffectObserver>> observersForITerrainTileComponentMutBunch =
      new SortedDictionary<int, List<IITerrainTileComponentMutBunchEffectObserver>>();
  readonly List<ITerrainTileComponentMutBunchCreateEffect> effectsITerrainTileComponentMutBunchCreateEffect =
      new List<ITerrainTileComponentMutBunchCreateEffect>();
  readonly List<ITerrainTileComponentMutBunchDeleteEffect> effectsITerrainTileComponentMutBunchDeleteEffect =
      new List<ITerrainTileComponentMutBunchDeleteEffect>();

  readonly SortedDictionary<int, List<ITerrainEffectObserver>> observersForTerrain =
      new SortedDictionary<int, List<ITerrainEffectObserver>>();
  readonly List<TerrainCreateEffect> effectsTerrainCreateEffect =
      new List<TerrainCreateEffect>();
  readonly List<TerrainDeleteEffect> effectsTerrainDeleteEffect =
      new List<TerrainDeleteEffect>();
  readonly List<TerrainSetPatternEffect> effectsTerrainSetPatternEffect =
      new List<TerrainSetPatternEffect>();

  readonly SortedDictionary<int, List<IGlaiveEffectObserver>> observersForGlaive =
      new SortedDictionary<int, List<IGlaiveEffectObserver>>();
  readonly List<GlaiveCreateEffect> effectsGlaiveCreateEffect =
      new List<GlaiveCreateEffect>();
  readonly List<GlaiveDeleteEffect> effectsGlaiveDeleteEffect =
      new List<GlaiveDeleteEffect>();

  readonly SortedDictionary<int, List<IArmorEffectObserver>> observersForArmor =
      new SortedDictionary<int, List<IArmorEffectObserver>>();
  readonly List<ArmorCreateEffect> effectsArmorCreateEffect =
      new List<ArmorCreateEffect>();
  readonly List<ArmorDeleteEffect> effectsArmorDeleteEffect =
      new List<ArmorDeleteEffect>();

  readonly SortedDictionary<int, List<IRandEffectObserver>> observersForRand =
      new SortedDictionary<int, List<IRandEffectObserver>>();
  readonly List<RandCreateEffect> effectsRandCreateEffect =
      new List<RandCreateEffect>();
  readonly List<RandDeleteEffect> effectsRandDeleteEffect =
      new List<RandDeleteEffect>();
  readonly List<RandSetRandEffect> effectsRandSetRandEffect =
      new List<RandSetRandEffect>();

  readonly SortedDictionary<int, List<IWanderAICapabilityUCEffectObserver>> observersForWanderAICapabilityUC =
      new SortedDictionary<int, List<IWanderAICapabilityUCEffectObserver>>();
  readonly List<WanderAICapabilityUCCreateEffect> effectsWanderAICapabilityUCCreateEffect =
      new List<WanderAICapabilityUCCreateEffect>();
  readonly List<WanderAICapabilityUCDeleteEffect> effectsWanderAICapabilityUCDeleteEffect =
      new List<WanderAICapabilityUCDeleteEffect>();

  readonly SortedDictionary<int, List<IShieldingUCEffectObserver>> observersForShieldingUC =
      new SortedDictionary<int, List<IShieldingUCEffectObserver>>();
  readonly List<ShieldingUCCreateEffect> effectsShieldingUCCreateEffect =
      new List<ShieldingUCCreateEffect>();
  readonly List<ShieldingUCDeleteEffect> effectsShieldingUCDeleteEffect =
      new List<ShieldingUCDeleteEffect>();

  readonly SortedDictionary<int, List<IBidingOperationUCEffectObserver>> observersForBidingOperationUC =
      new SortedDictionary<int, List<IBidingOperationUCEffectObserver>>();
  readonly List<BidingOperationUCCreateEffect> effectsBidingOperationUCCreateEffect =
      new List<BidingOperationUCCreateEffect>();
  readonly List<BidingOperationUCDeleteEffect> effectsBidingOperationUCDeleteEffect =
      new List<BidingOperationUCDeleteEffect>();

  readonly SortedDictionary<int, List<IUnleashBideImpulseEffectObserver>> observersForUnleashBideImpulse =
      new SortedDictionary<int, List<IUnleashBideImpulseEffectObserver>>();
  readonly List<UnleashBideImpulseCreateEffect> effectsUnleashBideImpulseCreateEffect =
      new List<UnleashBideImpulseCreateEffect>();
  readonly List<UnleashBideImpulseDeleteEffect> effectsUnleashBideImpulseDeleteEffect =
      new List<UnleashBideImpulseDeleteEffect>();

  readonly SortedDictionary<int, List<IStartBidingImpulseEffectObserver>> observersForStartBidingImpulse =
      new SortedDictionary<int, List<IStartBidingImpulseEffectObserver>>();
  readonly List<StartBidingImpulseCreateEffect> effectsStartBidingImpulseCreateEffect =
      new List<StartBidingImpulseCreateEffect>();
  readonly List<StartBidingImpulseDeleteEffect> effectsStartBidingImpulseDeleteEffect =
      new List<StartBidingImpulseDeleteEffect>();

  readonly SortedDictionary<int, List<IBideAICapabilityUCEffectObserver>> observersForBideAICapabilityUC =
      new SortedDictionary<int, List<IBideAICapabilityUCEffectObserver>>();
  readonly List<BideAICapabilityUCCreateEffect> effectsBideAICapabilityUCCreateEffect =
      new List<BideAICapabilityUCCreateEffect>();
  readonly List<BideAICapabilityUCDeleteEffect> effectsBideAICapabilityUCDeleteEffect =
      new List<BideAICapabilityUCDeleteEffect>();

  readonly SortedDictionary<int, List<IAttackImpulseEffectObserver>> observersForAttackImpulse =
      new SortedDictionary<int, List<IAttackImpulseEffectObserver>>();
  readonly List<AttackImpulseCreateEffect> effectsAttackImpulseCreateEffect =
      new List<AttackImpulseCreateEffect>();
  readonly List<AttackImpulseDeleteEffect> effectsAttackImpulseDeleteEffect =
      new List<AttackImpulseDeleteEffect>();

  readonly SortedDictionary<int, List<IPursueImpulseEffectObserver>> observersForPursueImpulse =
      new SortedDictionary<int, List<IPursueImpulseEffectObserver>>();
  readonly List<PursueImpulseCreateEffect> effectsPursueImpulseCreateEffect =
      new List<PursueImpulseCreateEffect>();
  readonly List<PursueImpulseDeleteEffect> effectsPursueImpulseDeleteEffect =
      new List<PursueImpulseDeleteEffect>();

  readonly SortedDictionary<int, List<IKillDirectiveUCEffectObserver>> observersForKillDirectiveUC =
      new SortedDictionary<int, List<IKillDirectiveUCEffectObserver>>();
  readonly List<KillDirectiveUCCreateEffect> effectsKillDirectiveUCCreateEffect =
      new List<KillDirectiveUCCreateEffect>();
  readonly List<KillDirectiveUCDeleteEffect> effectsKillDirectiveUCDeleteEffect =
      new List<KillDirectiveUCDeleteEffect>();

  readonly SortedDictionary<int, List<IAttackAICapabilityUCEffectObserver>> observersForAttackAICapabilityUC =
      new SortedDictionary<int, List<IAttackAICapabilityUCEffectObserver>>();
  readonly List<AttackAICapabilityUCCreateEffect> effectsAttackAICapabilityUCCreateEffect =
      new List<AttackAICapabilityUCCreateEffect>();
  readonly List<AttackAICapabilityUCDeleteEffect> effectsAttackAICapabilityUCDeleteEffect =
      new List<AttackAICapabilityUCDeleteEffect>();

  readonly SortedDictionary<int, List<IMoveImpulseEffectObserver>> observersForMoveImpulse =
      new SortedDictionary<int, List<IMoveImpulseEffectObserver>>();
  readonly List<MoveImpulseCreateEffect> effectsMoveImpulseCreateEffect =
      new List<MoveImpulseCreateEffect>();
  readonly List<MoveImpulseDeleteEffect> effectsMoveImpulseDeleteEffect =
      new List<MoveImpulseDeleteEffect>();

  readonly SortedDictionary<int, List<IMoveDirectiveUCEffectObserver>> observersForMoveDirectiveUC =
      new SortedDictionary<int, List<IMoveDirectiveUCEffectObserver>>();
  readonly List<MoveDirectiveUCCreateEffect> effectsMoveDirectiveUCCreateEffect =
      new List<MoveDirectiveUCCreateEffect>();
  readonly List<MoveDirectiveUCDeleteEffect> effectsMoveDirectiveUCDeleteEffect =
      new List<MoveDirectiveUCDeleteEffect>();

  readonly SortedDictionary<int, List<IUnitEffectObserver>> observersForUnit =
      new SortedDictionary<int, List<IUnitEffectObserver>>();
  readonly List<UnitCreateEffect> effectsUnitCreateEffect =
      new List<UnitCreateEffect>();
  readonly List<UnitDeleteEffect> effectsUnitDeleteEffect =
      new List<UnitDeleteEffect>();
  readonly List<UnitSetAliveEffect> effectsUnitSetAliveEffect =
      new List<UnitSetAliveEffect>();
  readonly List<UnitSetLifeEndTimeEffect> effectsUnitSetLifeEndTimeEffect =
      new List<UnitSetLifeEndTimeEffect>();
  readonly List<UnitSetLocationEffect> effectsUnitSetLocationEffect =
      new List<UnitSetLocationEffect>();
  readonly List<UnitSetHpEffect> effectsUnitSetHpEffect =
      new List<UnitSetHpEffect>();
  readonly List<UnitSetMpEffect> effectsUnitSetMpEffect =
      new List<UnitSetMpEffect>();
  readonly List<UnitSetNextActionTimeEffect> effectsUnitSetNextActionTimeEffect =
      new List<UnitSetNextActionTimeEffect>();

  readonly SortedDictionary<int, List<IIItemMutBunchEffectObserver>> observersForIItemMutBunch =
      new SortedDictionary<int, List<IIItemMutBunchEffectObserver>>();
  readonly List<IItemMutBunchCreateEffect> effectsIItemMutBunchCreateEffect =
      new List<IItemMutBunchCreateEffect>();
  readonly List<IItemMutBunchDeleteEffect> effectsIItemMutBunchDeleteEffect =
      new List<IItemMutBunchDeleteEffect>();

  readonly SortedDictionary<int, List<IIUnitComponentMutBunchEffectObserver>> observersForIUnitComponentMutBunch =
      new SortedDictionary<int, List<IIUnitComponentMutBunchEffectObserver>>();
  readonly List<IUnitComponentMutBunchCreateEffect> effectsIUnitComponentMutBunchCreateEffect =
      new List<IUnitComponentMutBunchCreateEffect>();
  readonly List<IUnitComponentMutBunchDeleteEffect> effectsIUnitComponentMutBunchDeleteEffect =
      new List<IUnitComponentMutBunchDeleteEffect>();

  readonly SortedDictionary<int, List<INoImpulseEffectObserver>> observersForNoImpulse =
      new SortedDictionary<int, List<INoImpulseEffectObserver>>();
  readonly List<NoImpulseCreateEffect> effectsNoImpulseCreateEffect =
      new List<NoImpulseCreateEffect>();
  readonly List<NoImpulseDeleteEffect> effectsNoImpulseDeleteEffect =
      new List<NoImpulseDeleteEffect>();

  readonly SortedDictionary<int, List<IExecutionStateEffectObserver>> observersForExecutionState =
      new SortedDictionary<int, List<IExecutionStateEffectObserver>>();
  readonly List<ExecutionStateCreateEffect> effectsExecutionStateCreateEffect =
      new List<ExecutionStateCreateEffect>();
  readonly List<ExecutionStateDeleteEffect> effectsExecutionStateDeleteEffect =
      new List<ExecutionStateDeleteEffect>();
  readonly List<ExecutionStateSetActingUnitEffect> effectsExecutionStateSetActingUnitEffect =
      new List<ExecutionStateSetActingUnitEffect>();
  readonly List<ExecutionStateSetActingUnitDidActionEffect> effectsExecutionStateSetActingUnitDidActionEffect =
      new List<ExecutionStateSetActingUnitDidActionEffect>();
  readonly List<ExecutionStateSetRemainingPreActingUnitComponentsEffect> effectsExecutionStateSetRemainingPreActingUnitComponentsEffect =
      new List<ExecutionStateSetRemainingPreActingUnitComponentsEffect>();
  readonly List<ExecutionStateSetRemainingPostActingUnitComponentsEffect> effectsExecutionStateSetRemainingPostActingUnitComponentsEffect =
      new List<ExecutionStateSetRemainingPostActingUnitComponentsEffect>();

  readonly SortedDictionary<int, List<IIPostActingUCWeakMutBunchEffectObserver>> observersForIPostActingUCWeakMutBunch =
      new SortedDictionary<int, List<IIPostActingUCWeakMutBunchEffectObserver>>();
  readonly List<IPostActingUCWeakMutBunchCreateEffect> effectsIPostActingUCWeakMutBunchCreateEffect =
      new List<IPostActingUCWeakMutBunchCreateEffect>();
  readonly List<IPostActingUCWeakMutBunchDeleteEffect> effectsIPostActingUCWeakMutBunchDeleteEffect =
      new List<IPostActingUCWeakMutBunchDeleteEffect>();

  readonly SortedDictionary<int, List<IIPreActingUCWeakMutBunchEffectObserver>> observersForIPreActingUCWeakMutBunch =
      new SortedDictionary<int, List<IIPreActingUCWeakMutBunchEffectObserver>>();
  readonly List<IPreActingUCWeakMutBunchCreateEffect> effectsIPreActingUCWeakMutBunchCreateEffect =
      new List<IPreActingUCWeakMutBunchCreateEffect>();
  readonly List<IPreActingUCWeakMutBunchDeleteEffect> effectsIPreActingUCWeakMutBunchDeleteEffect =
      new List<IPreActingUCWeakMutBunchDeleteEffect>();

  readonly SortedDictionary<int, List<IGameEffectObserver>> observersForGame =
      new SortedDictionary<int, List<IGameEffectObserver>>();
  readonly List<GameCreateEffect> effectsGameCreateEffect =
      new List<GameCreateEffect>();
  readonly List<GameDeleteEffect> effectsGameDeleteEffect =
      new List<GameDeleteEffect>();
  readonly List<GameSetPlayerEffect> effectsGameSetPlayerEffect =
      new List<GameSetPlayerEffect>();
  readonly List<GameSetLevelEffect> effectsGameSetLevelEffect =
      new List<GameSetLevelEffect>();
  readonly List<GameSetTimeEffect> effectsGameSetTimeEffect =
      new List<GameSetTimeEffect>();

  readonly SortedDictionary<int, List<IIUnitEventMutListEffectObserver>> observersForIUnitEventMutList =
      new SortedDictionary<int, List<IIUnitEventMutListEffectObserver>>();
  readonly List<IUnitEventMutListCreateEffect> effectsIUnitEventMutListCreateEffect =
      new List<IUnitEventMutListCreateEffect>();
  readonly List<IUnitEventMutListDeleteEffect> effectsIUnitEventMutListDeleteEffect =
      new List<IUnitEventMutListDeleteEffect>();
  readonly List<IUnitEventMutListAddEffect> effectsIUnitEventMutListAddEffect =
      new List<IUnitEventMutListAddEffect>();
  readonly List<IUnitEventMutListRemoveEffect> effectsIUnitEventMutListRemoveEffect =
      new List<IUnitEventMutListRemoveEffect>();

  readonly SortedDictionary<int, List<ILocationMutListEffectObserver>> observersForLocationMutList =
      new SortedDictionary<int, List<ILocationMutListEffectObserver>>();
  readonly List<LocationMutListCreateEffect> effectsLocationMutListCreateEffect =
      new List<LocationMutListCreateEffect>();
  readonly List<LocationMutListDeleteEffect> effectsLocationMutListDeleteEffect =
      new List<LocationMutListDeleteEffect>();
  readonly List<LocationMutListAddEffect> effectsLocationMutListAddEffect =
      new List<LocationMutListAddEffect>();
  readonly List<LocationMutListRemoveEffect> effectsLocationMutListRemoveEffect =
      new List<LocationMutListRemoveEffect>();

  readonly SortedDictionary<int, List<ILevelMutSetEffectObserver>> observersForLevelMutSet =
      new SortedDictionary<int, List<ILevelMutSetEffectObserver>>();
  readonly List<LevelMutSetCreateEffect> effectsLevelMutSetCreateEffect =
      new List<LevelMutSetCreateEffect>();
  readonly List<LevelMutSetDeleteEffect> effectsLevelMutSetDeleteEffect =
      new List<LevelMutSetDeleteEffect>();
  readonly List<LevelMutSetAddEffect> effectsLevelMutSetAddEffect =
      new List<LevelMutSetAddEffect>();
  readonly List<LevelMutSetRemoveEffect> effectsLevelMutSetRemoveEffect =
      new List<LevelMutSetRemoveEffect>();

  readonly SortedDictionary<int, List<IShieldingUCWeakMutSetEffectObserver>> observersForShieldingUCWeakMutSet =
      new SortedDictionary<int, List<IShieldingUCWeakMutSetEffectObserver>>();
  readonly List<ShieldingUCWeakMutSetCreateEffect> effectsShieldingUCWeakMutSetCreateEffect =
      new List<ShieldingUCWeakMutSetCreateEffect>();
  readonly List<ShieldingUCWeakMutSetDeleteEffect> effectsShieldingUCWeakMutSetDeleteEffect =
      new List<ShieldingUCWeakMutSetDeleteEffect>();
  readonly List<ShieldingUCWeakMutSetAddEffect> effectsShieldingUCWeakMutSetAddEffect =
      new List<ShieldingUCWeakMutSetAddEffect>();
  readonly List<ShieldingUCWeakMutSetRemoveEffect> effectsShieldingUCWeakMutSetRemoveEffect =
      new List<ShieldingUCWeakMutSetRemoveEffect>();

  readonly SortedDictionary<int, List<IAttackAICapabilityUCWeakMutSetEffectObserver>> observersForAttackAICapabilityUCWeakMutSet =
      new SortedDictionary<int, List<IAttackAICapabilityUCWeakMutSetEffectObserver>>();
  readonly List<AttackAICapabilityUCWeakMutSetCreateEffect> effectsAttackAICapabilityUCWeakMutSetCreateEffect =
      new List<AttackAICapabilityUCWeakMutSetCreateEffect>();
  readonly List<AttackAICapabilityUCWeakMutSetDeleteEffect> effectsAttackAICapabilityUCWeakMutSetDeleteEffect =
      new List<AttackAICapabilityUCWeakMutSetDeleteEffect>();
  readonly List<AttackAICapabilityUCWeakMutSetAddEffect> effectsAttackAICapabilityUCWeakMutSetAddEffect =
      new List<AttackAICapabilityUCWeakMutSetAddEffect>();
  readonly List<AttackAICapabilityUCWeakMutSetRemoveEffect> effectsAttackAICapabilityUCWeakMutSetRemoveEffect =
      new List<AttackAICapabilityUCWeakMutSetRemoveEffect>();

  readonly SortedDictionary<int, List<IKillDirectiveUCMutSetEffectObserver>> observersForKillDirectiveUCMutSet =
      new SortedDictionary<int, List<IKillDirectiveUCMutSetEffectObserver>>();
  readonly List<KillDirectiveUCMutSetCreateEffect> effectsKillDirectiveUCMutSetCreateEffect =
      new List<KillDirectiveUCMutSetCreateEffect>();
  readonly List<KillDirectiveUCMutSetDeleteEffect> effectsKillDirectiveUCMutSetDeleteEffect =
      new List<KillDirectiveUCMutSetDeleteEffect>();
  readonly List<KillDirectiveUCMutSetAddEffect> effectsKillDirectiveUCMutSetAddEffect =
      new List<KillDirectiveUCMutSetAddEffect>();
  readonly List<KillDirectiveUCMutSetRemoveEffect> effectsKillDirectiveUCMutSetRemoveEffect =
      new List<KillDirectiveUCMutSetRemoveEffect>();

  readonly SortedDictionary<int, List<IMoveDirectiveUCMutSetEffectObserver>> observersForMoveDirectiveUCMutSet =
      new SortedDictionary<int, List<IMoveDirectiveUCMutSetEffectObserver>>();
  readonly List<MoveDirectiveUCMutSetCreateEffect> effectsMoveDirectiveUCMutSetCreateEffect =
      new List<MoveDirectiveUCMutSetCreateEffect>();
  readonly List<MoveDirectiveUCMutSetDeleteEffect> effectsMoveDirectiveUCMutSetDeleteEffect =
      new List<MoveDirectiveUCMutSetDeleteEffect>();
  readonly List<MoveDirectiveUCMutSetAddEffect> effectsMoveDirectiveUCMutSetAddEffect =
      new List<MoveDirectiveUCMutSetAddEffect>();
  readonly List<MoveDirectiveUCMutSetRemoveEffect> effectsMoveDirectiveUCMutSetRemoveEffect =
      new List<MoveDirectiveUCMutSetRemoveEffect>();

  readonly SortedDictionary<int, List<IWanderAICapabilityUCMutSetEffectObserver>> observersForWanderAICapabilityUCMutSet =
      new SortedDictionary<int, List<IWanderAICapabilityUCMutSetEffectObserver>>();
  readonly List<WanderAICapabilityUCMutSetCreateEffect> effectsWanderAICapabilityUCMutSetCreateEffect =
      new List<WanderAICapabilityUCMutSetCreateEffect>();
  readonly List<WanderAICapabilityUCMutSetDeleteEffect> effectsWanderAICapabilityUCMutSetDeleteEffect =
      new List<WanderAICapabilityUCMutSetDeleteEffect>();
  readonly List<WanderAICapabilityUCMutSetAddEffect> effectsWanderAICapabilityUCMutSetAddEffect =
      new List<WanderAICapabilityUCMutSetAddEffect>();
  readonly List<WanderAICapabilityUCMutSetRemoveEffect> effectsWanderAICapabilityUCMutSetRemoveEffect =
      new List<WanderAICapabilityUCMutSetRemoveEffect>();

  readonly SortedDictionary<int, List<IBideAICapabilityUCMutSetEffectObserver>> observersForBideAICapabilityUCMutSet =
      new SortedDictionary<int, List<IBideAICapabilityUCMutSetEffectObserver>>();
  readonly List<BideAICapabilityUCMutSetCreateEffect> effectsBideAICapabilityUCMutSetCreateEffect =
      new List<BideAICapabilityUCMutSetCreateEffect>();
  readonly List<BideAICapabilityUCMutSetDeleteEffect> effectsBideAICapabilityUCMutSetDeleteEffect =
      new List<BideAICapabilityUCMutSetDeleteEffect>();
  readonly List<BideAICapabilityUCMutSetAddEffect> effectsBideAICapabilityUCMutSetAddEffect =
      new List<BideAICapabilityUCMutSetAddEffect>();
  readonly List<BideAICapabilityUCMutSetRemoveEffect> effectsBideAICapabilityUCMutSetRemoveEffect =
      new List<BideAICapabilityUCMutSetRemoveEffect>();

  readonly SortedDictionary<int, List<IAttackAICapabilityUCMutSetEffectObserver>> observersForAttackAICapabilityUCMutSet =
      new SortedDictionary<int, List<IAttackAICapabilityUCMutSetEffectObserver>>();
  readonly List<AttackAICapabilityUCMutSetCreateEffect> effectsAttackAICapabilityUCMutSetCreateEffect =
      new List<AttackAICapabilityUCMutSetCreateEffect>();
  readonly List<AttackAICapabilityUCMutSetDeleteEffect> effectsAttackAICapabilityUCMutSetDeleteEffect =
      new List<AttackAICapabilityUCMutSetDeleteEffect>();
  readonly List<AttackAICapabilityUCMutSetAddEffect> effectsAttackAICapabilityUCMutSetAddEffect =
      new List<AttackAICapabilityUCMutSetAddEffect>();
  readonly List<AttackAICapabilityUCMutSetRemoveEffect> effectsAttackAICapabilityUCMutSetRemoveEffect =
      new List<AttackAICapabilityUCMutSetRemoveEffect>();

  readonly SortedDictionary<int, List<IShieldingUCMutSetEffectObserver>> observersForShieldingUCMutSet =
      new SortedDictionary<int, List<IShieldingUCMutSetEffectObserver>>();
  readonly List<ShieldingUCMutSetCreateEffect> effectsShieldingUCMutSetCreateEffect =
      new List<ShieldingUCMutSetCreateEffect>();
  readonly List<ShieldingUCMutSetDeleteEffect> effectsShieldingUCMutSetDeleteEffect =
      new List<ShieldingUCMutSetDeleteEffect>();
  readonly List<ShieldingUCMutSetAddEffect> effectsShieldingUCMutSetAddEffect =
      new List<ShieldingUCMutSetAddEffect>();
  readonly List<ShieldingUCMutSetRemoveEffect> effectsShieldingUCMutSetRemoveEffect =
      new List<ShieldingUCMutSetRemoveEffect>();

  readonly SortedDictionary<int, List<IBidingOperationUCMutSetEffectObserver>> observersForBidingOperationUCMutSet =
      new SortedDictionary<int, List<IBidingOperationUCMutSetEffectObserver>>();
  readonly List<BidingOperationUCMutSetCreateEffect> effectsBidingOperationUCMutSetCreateEffect =
      new List<BidingOperationUCMutSetCreateEffect>();
  readonly List<BidingOperationUCMutSetDeleteEffect> effectsBidingOperationUCMutSetDeleteEffect =
      new List<BidingOperationUCMutSetDeleteEffect>();
  readonly List<BidingOperationUCMutSetAddEffect> effectsBidingOperationUCMutSetAddEffect =
      new List<BidingOperationUCMutSetAddEffect>();
  readonly List<BidingOperationUCMutSetRemoveEffect> effectsBidingOperationUCMutSetRemoveEffect =
      new List<BidingOperationUCMutSetRemoveEffect>();

  readonly SortedDictionary<int, List<IGlaiveMutSetEffectObserver>> observersForGlaiveMutSet =
      new SortedDictionary<int, List<IGlaiveMutSetEffectObserver>>();
  readonly List<GlaiveMutSetCreateEffect> effectsGlaiveMutSetCreateEffect =
      new List<GlaiveMutSetCreateEffect>();
  readonly List<GlaiveMutSetDeleteEffect> effectsGlaiveMutSetDeleteEffect =
      new List<GlaiveMutSetDeleteEffect>();
  readonly List<GlaiveMutSetAddEffect> effectsGlaiveMutSetAddEffect =
      new List<GlaiveMutSetAddEffect>();
  readonly List<GlaiveMutSetRemoveEffect> effectsGlaiveMutSetRemoveEffect =
      new List<GlaiveMutSetRemoveEffect>();

  readonly SortedDictionary<int, List<IArmorMutSetEffectObserver>> observersForArmorMutSet =
      new SortedDictionary<int, List<IArmorMutSetEffectObserver>>();
  readonly List<ArmorMutSetCreateEffect> effectsArmorMutSetCreateEffect =
      new List<ArmorMutSetCreateEffect>();
  readonly List<ArmorMutSetDeleteEffect> effectsArmorMutSetDeleteEffect =
      new List<ArmorMutSetDeleteEffect>();
  readonly List<ArmorMutSetAddEffect> effectsArmorMutSetAddEffect =
      new List<ArmorMutSetAddEffect>();
  readonly List<ArmorMutSetRemoveEffect> effectsArmorMutSetRemoveEffect =
      new List<ArmorMutSetRemoveEffect>();

  readonly SortedDictionary<int, List<IItemTerrainTileComponentMutSetEffectObserver>> observersForItemTerrainTileComponentMutSet =
      new SortedDictionary<int, List<IItemTerrainTileComponentMutSetEffectObserver>>();
  readonly List<ItemTerrainTileComponentMutSetCreateEffect> effectsItemTerrainTileComponentMutSetCreateEffect =
      new List<ItemTerrainTileComponentMutSetCreateEffect>();
  readonly List<ItemTerrainTileComponentMutSetDeleteEffect> effectsItemTerrainTileComponentMutSetDeleteEffect =
      new List<ItemTerrainTileComponentMutSetDeleteEffect>();
  readonly List<ItemTerrainTileComponentMutSetAddEffect> effectsItemTerrainTileComponentMutSetAddEffect =
      new List<ItemTerrainTileComponentMutSetAddEffect>();
  readonly List<ItemTerrainTileComponentMutSetRemoveEffect> effectsItemTerrainTileComponentMutSetRemoveEffect =
      new List<ItemTerrainTileComponentMutSetRemoveEffect>();

  readonly SortedDictionary<int, List<IDecorativeTerrainTileComponentMutSetEffectObserver>> observersForDecorativeTerrainTileComponentMutSet =
      new SortedDictionary<int, List<IDecorativeTerrainTileComponentMutSetEffectObserver>>();
  readonly List<DecorativeTerrainTileComponentMutSetCreateEffect> effectsDecorativeTerrainTileComponentMutSetCreateEffect =
      new List<DecorativeTerrainTileComponentMutSetCreateEffect>();
  readonly List<DecorativeTerrainTileComponentMutSetDeleteEffect> effectsDecorativeTerrainTileComponentMutSetDeleteEffect =
      new List<DecorativeTerrainTileComponentMutSetDeleteEffect>();
  readonly List<DecorativeTerrainTileComponentMutSetAddEffect> effectsDecorativeTerrainTileComponentMutSetAddEffect =
      new List<DecorativeTerrainTileComponentMutSetAddEffect>();
  readonly List<DecorativeTerrainTileComponentMutSetRemoveEffect> effectsDecorativeTerrainTileComponentMutSetRemoveEffect =
      new List<DecorativeTerrainTileComponentMutSetRemoveEffect>();

  readonly SortedDictionary<int, List<IUpStaircaseTerrainTileComponentMutSetEffectObserver>> observersForUpStaircaseTerrainTileComponentMutSet =
      new SortedDictionary<int, List<IUpStaircaseTerrainTileComponentMutSetEffectObserver>>();
  readonly List<UpStaircaseTerrainTileComponentMutSetCreateEffect> effectsUpStaircaseTerrainTileComponentMutSetCreateEffect =
      new List<UpStaircaseTerrainTileComponentMutSetCreateEffect>();
  readonly List<UpStaircaseTerrainTileComponentMutSetDeleteEffect> effectsUpStaircaseTerrainTileComponentMutSetDeleteEffect =
      new List<UpStaircaseTerrainTileComponentMutSetDeleteEffect>();
  readonly List<UpStaircaseTerrainTileComponentMutSetAddEffect> effectsUpStaircaseTerrainTileComponentMutSetAddEffect =
      new List<UpStaircaseTerrainTileComponentMutSetAddEffect>();
  readonly List<UpStaircaseTerrainTileComponentMutSetRemoveEffect> effectsUpStaircaseTerrainTileComponentMutSetRemoveEffect =
      new List<UpStaircaseTerrainTileComponentMutSetRemoveEffect>();

  readonly SortedDictionary<int, List<IDownStaircaseTerrainTileComponentMutSetEffectObserver>> observersForDownStaircaseTerrainTileComponentMutSet =
      new SortedDictionary<int, List<IDownStaircaseTerrainTileComponentMutSetEffectObserver>>();
  readonly List<DownStaircaseTerrainTileComponentMutSetCreateEffect> effectsDownStaircaseTerrainTileComponentMutSetCreateEffect =
      new List<DownStaircaseTerrainTileComponentMutSetCreateEffect>();
  readonly List<DownStaircaseTerrainTileComponentMutSetDeleteEffect> effectsDownStaircaseTerrainTileComponentMutSetDeleteEffect =
      new List<DownStaircaseTerrainTileComponentMutSetDeleteEffect>();
  readonly List<DownStaircaseTerrainTileComponentMutSetAddEffect> effectsDownStaircaseTerrainTileComponentMutSetAddEffect =
      new List<DownStaircaseTerrainTileComponentMutSetAddEffect>();
  readonly List<DownStaircaseTerrainTileComponentMutSetRemoveEffect> effectsDownStaircaseTerrainTileComponentMutSetRemoveEffect =
      new List<DownStaircaseTerrainTileComponentMutSetRemoveEffect>();

  readonly SortedDictionary<int, List<IUnitMutSetEffectObserver>> observersForUnitMutSet =
      new SortedDictionary<int, List<IUnitMutSetEffectObserver>>();
  readonly List<UnitMutSetCreateEffect> effectsUnitMutSetCreateEffect =
      new List<UnitMutSetCreateEffect>();
  readonly List<UnitMutSetDeleteEffect> effectsUnitMutSetDeleteEffect =
      new List<UnitMutSetDeleteEffect>();
  readonly List<UnitMutSetAddEffect> effectsUnitMutSetAddEffect =
      new List<UnitMutSetAddEffect>();
  readonly List<UnitMutSetRemoveEffect> effectsUnitMutSetRemoveEffect =
      new List<UnitMutSetRemoveEffect>();

  readonly SortedDictionary<int, List<ITerrainTileByLocationMutMapEffectObserver>> observersForTerrainTileByLocationMutMap =
      new SortedDictionary<int, List<ITerrainTileByLocationMutMapEffectObserver>>();
  readonly List<TerrainTileByLocationMutMapCreateEffect> effectsTerrainTileByLocationMutMapCreateEffect =
      new List<TerrainTileByLocationMutMapCreateEffect>();
  readonly List<TerrainTileByLocationMutMapDeleteEffect> effectsTerrainTileByLocationMutMapDeleteEffect =
      new List<TerrainTileByLocationMutMapDeleteEffect>();
  readonly List<TerrainTileByLocationMutMapAddEffect> effectsTerrainTileByLocationMutMapAddEffect =
      new List<TerrainTileByLocationMutMapAddEffect>();
  readonly List<TerrainTileByLocationMutMapRemoveEffect> effectsTerrainTileByLocationMutMapRemoveEffect =
      new List<TerrainTileByLocationMutMapRemoveEffect>();

  public Root(ILogger logger) {
    this.logger = logger;
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
    foreach (var entry in this.rootIncarnation.incarnationsItemTerrainTileComponent) {
      result += GetItemTerrainTileComponentHash(entry.Key, entry.Value.version, entry.Value.incarnation);
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
    foreach (var entry in this.rootIncarnation.incarnationsItemTerrainTileComponentMutSet) {
      result += GetItemTerrainTileComponentMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
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
    foreach (var obj in this.AllItemTerrainTileComponent()) {
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
    foreach (var obj in this.AllItemTerrainTileComponentMutSet()) {
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
    foreach (var obj in this.AllItemTerrainTileComponent()) {
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
    foreach (var obj in this.AllItemTerrainTileComponentMutSet()) {
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

  public void FlushEvents() {



    var copyOfObserversForLevel =
        new SortedDictionary<int, List<ILevelEffectObserver>>();
    foreach (var entry in observersForLevel) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForLevel.Add(
          objectId,
          new List<ILevelEffectObserver>(
              observers));
    }

    var copyOfObserversForItemTerrainTileComponent =
        new SortedDictionary<int, List<IItemTerrainTileComponentEffectObserver>>();
    foreach (var entry in observersForItemTerrainTileComponent) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForItemTerrainTileComponent.Add(
          objectId,
          new List<IItemTerrainTileComponentEffectObserver>(
              observers));
    }

    var copyOfObserversForDecorativeTerrainTileComponent =
        new SortedDictionary<int, List<IDecorativeTerrainTileComponentEffectObserver>>();
    foreach (var entry in observersForDecorativeTerrainTileComponent) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForDecorativeTerrainTileComponent.Add(
          objectId,
          new List<IDecorativeTerrainTileComponentEffectObserver>(
              observers));
    }

    var copyOfObserversForUpStaircaseTerrainTileComponent =
        new SortedDictionary<int, List<IUpStaircaseTerrainTileComponentEffectObserver>>();
    foreach (var entry in observersForUpStaircaseTerrainTileComponent) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForUpStaircaseTerrainTileComponent.Add(
          objectId,
          new List<IUpStaircaseTerrainTileComponentEffectObserver>(
              observers));
    }

    var copyOfObserversForDownStaircaseTerrainTileComponent =
        new SortedDictionary<int, List<IDownStaircaseTerrainTileComponentEffectObserver>>();
    foreach (var entry in observersForDownStaircaseTerrainTileComponent) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForDownStaircaseTerrainTileComponent.Add(
          objectId,
          new List<IDownStaircaseTerrainTileComponentEffectObserver>(
              observers));
    }

    var copyOfObserversForTerrainTile =
        new SortedDictionary<int, List<ITerrainTileEffectObserver>>();
    foreach (var entry in observersForTerrainTile) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForTerrainTile.Add(
          objectId,
          new List<ITerrainTileEffectObserver>(
              observers));
    }

    var copyOfObserversForITerrainTileComponentMutBunch =
        new SortedDictionary<int, List<IITerrainTileComponentMutBunchEffectObserver>>();
    foreach (var entry in observersForITerrainTileComponentMutBunch) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForITerrainTileComponentMutBunch.Add(
          objectId,
          new List<IITerrainTileComponentMutBunchEffectObserver>(
              observers));
    }

    var copyOfObserversForTerrain =
        new SortedDictionary<int, List<ITerrainEffectObserver>>();
    foreach (var entry in observersForTerrain) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForTerrain.Add(
          objectId,
          new List<ITerrainEffectObserver>(
              observers));
    }

    var copyOfObserversForGlaive =
        new SortedDictionary<int, List<IGlaiveEffectObserver>>();
    foreach (var entry in observersForGlaive) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForGlaive.Add(
          objectId,
          new List<IGlaiveEffectObserver>(
              observers));
    }

    var copyOfObserversForArmor =
        new SortedDictionary<int, List<IArmorEffectObserver>>();
    foreach (var entry in observersForArmor) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForArmor.Add(
          objectId,
          new List<IArmorEffectObserver>(
              observers));
    }

    var copyOfObserversForRand =
        new SortedDictionary<int, List<IRandEffectObserver>>();
    foreach (var entry in observersForRand) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForRand.Add(
          objectId,
          new List<IRandEffectObserver>(
              observers));
    }

    var copyOfObserversForWanderAICapabilityUC =
        new SortedDictionary<int, List<IWanderAICapabilityUCEffectObserver>>();
    foreach (var entry in observersForWanderAICapabilityUC) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForWanderAICapabilityUC.Add(
          objectId,
          new List<IWanderAICapabilityUCEffectObserver>(
              observers));
    }

    var copyOfObserversForShieldingUC =
        new SortedDictionary<int, List<IShieldingUCEffectObserver>>();
    foreach (var entry in observersForShieldingUC) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForShieldingUC.Add(
          objectId,
          new List<IShieldingUCEffectObserver>(
              observers));
    }

    var copyOfObserversForBidingOperationUC =
        new SortedDictionary<int, List<IBidingOperationUCEffectObserver>>();
    foreach (var entry in observersForBidingOperationUC) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForBidingOperationUC.Add(
          objectId,
          new List<IBidingOperationUCEffectObserver>(
              observers));
    }

    var copyOfObserversForUnleashBideImpulse =
        new SortedDictionary<int, List<IUnleashBideImpulseEffectObserver>>();
    foreach (var entry in observersForUnleashBideImpulse) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForUnleashBideImpulse.Add(
          objectId,
          new List<IUnleashBideImpulseEffectObserver>(
              observers));
    }

    var copyOfObserversForStartBidingImpulse =
        new SortedDictionary<int, List<IStartBidingImpulseEffectObserver>>();
    foreach (var entry in observersForStartBidingImpulse) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForStartBidingImpulse.Add(
          objectId,
          new List<IStartBidingImpulseEffectObserver>(
              observers));
    }

    var copyOfObserversForBideAICapabilityUC =
        new SortedDictionary<int, List<IBideAICapabilityUCEffectObserver>>();
    foreach (var entry in observersForBideAICapabilityUC) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForBideAICapabilityUC.Add(
          objectId,
          new List<IBideAICapabilityUCEffectObserver>(
              observers));
    }

    var copyOfObserversForAttackImpulse =
        new SortedDictionary<int, List<IAttackImpulseEffectObserver>>();
    foreach (var entry in observersForAttackImpulse) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForAttackImpulse.Add(
          objectId,
          new List<IAttackImpulseEffectObserver>(
              observers));
    }

    var copyOfObserversForPursueImpulse =
        new SortedDictionary<int, List<IPursueImpulseEffectObserver>>();
    foreach (var entry in observersForPursueImpulse) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForPursueImpulse.Add(
          objectId,
          new List<IPursueImpulseEffectObserver>(
              observers));
    }

    var copyOfObserversForKillDirectiveUC =
        new SortedDictionary<int, List<IKillDirectiveUCEffectObserver>>();
    foreach (var entry in observersForKillDirectiveUC) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForKillDirectiveUC.Add(
          objectId,
          new List<IKillDirectiveUCEffectObserver>(
              observers));
    }

    var copyOfObserversForAttackAICapabilityUC =
        new SortedDictionary<int, List<IAttackAICapabilityUCEffectObserver>>();
    foreach (var entry in observersForAttackAICapabilityUC) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForAttackAICapabilityUC.Add(
          objectId,
          new List<IAttackAICapabilityUCEffectObserver>(
              observers));
    }

    var copyOfObserversForMoveImpulse =
        new SortedDictionary<int, List<IMoveImpulseEffectObserver>>();
    foreach (var entry in observersForMoveImpulse) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForMoveImpulse.Add(
          objectId,
          new List<IMoveImpulseEffectObserver>(
              observers));
    }

    var copyOfObserversForMoveDirectiveUC =
        new SortedDictionary<int, List<IMoveDirectiveUCEffectObserver>>();
    foreach (var entry in observersForMoveDirectiveUC) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForMoveDirectiveUC.Add(
          objectId,
          new List<IMoveDirectiveUCEffectObserver>(
              observers));
    }

    var copyOfObserversForUnit =
        new SortedDictionary<int, List<IUnitEffectObserver>>();
    foreach (var entry in observersForUnit) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForUnit.Add(
          objectId,
          new List<IUnitEffectObserver>(
              observers));
    }

    var copyOfObserversForIItemMutBunch =
        new SortedDictionary<int, List<IIItemMutBunchEffectObserver>>();
    foreach (var entry in observersForIItemMutBunch) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForIItemMutBunch.Add(
          objectId,
          new List<IIItemMutBunchEffectObserver>(
              observers));
    }

    var copyOfObserversForIUnitComponentMutBunch =
        new SortedDictionary<int, List<IIUnitComponentMutBunchEffectObserver>>();
    foreach (var entry in observersForIUnitComponentMutBunch) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForIUnitComponentMutBunch.Add(
          objectId,
          new List<IIUnitComponentMutBunchEffectObserver>(
              observers));
    }

    var copyOfObserversForNoImpulse =
        new SortedDictionary<int, List<INoImpulseEffectObserver>>();
    foreach (var entry in observersForNoImpulse) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForNoImpulse.Add(
          objectId,
          new List<INoImpulseEffectObserver>(
              observers));
    }

    var copyOfObserversForExecutionState =
        new SortedDictionary<int, List<IExecutionStateEffectObserver>>();
    foreach (var entry in observersForExecutionState) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForExecutionState.Add(
          objectId,
          new List<IExecutionStateEffectObserver>(
              observers));
    }

    var copyOfObserversForIPostActingUCWeakMutBunch =
        new SortedDictionary<int, List<IIPostActingUCWeakMutBunchEffectObserver>>();
    foreach (var entry in observersForIPostActingUCWeakMutBunch) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForIPostActingUCWeakMutBunch.Add(
          objectId,
          new List<IIPostActingUCWeakMutBunchEffectObserver>(
              observers));
    }

    var copyOfObserversForIPreActingUCWeakMutBunch =
        new SortedDictionary<int, List<IIPreActingUCWeakMutBunchEffectObserver>>();
    foreach (var entry in observersForIPreActingUCWeakMutBunch) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForIPreActingUCWeakMutBunch.Add(
          objectId,
          new List<IIPreActingUCWeakMutBunchEffectObserver>(
              observers));
    }

    var copyOfObserversForGame =
        new SortedDictionary<int, List<IGameEffectObserver>>();
    foreach (var entry in observersForGame) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForGame.Add(
          objectId,
          new List<IGameEffectObserver>(
              observers));
    }

    var copyOfObserversForIUnitEventMutList =
        new SortedDictionary<int, List<IIUnitEventMutListEffectObserver>>();
    foreach (var entry in observersForIUnitEventMutList) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForIUnitEventMutList.Add(
          objectId,
          new List<IIUnitEventMutListEffectObserver>(
              observers));
    }

    var copyOfObserversForLocationMutList =
        new SortedDictionary<int, List<ILocationMutListEffectObserver>>();
    foreach (var entry in observersForLocationMutList) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForLocationMutList.Add(
          objectId,
          new List<ILocationMutListEffectObserver>(
              observers));
    }

    var copyOfObserversForLevelMutSet =
        new SortedDictionary<int, List<ILevelMutSetEffectObserver>>();
    foreach (var entry in observersForLevelMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForLevelMutSet.Add(
          objectId,
          new List<ILevelMutSetEffectObserver>(
              observers));
    }

    var copyOfObserversForShieldingUCWeakMutSet =
        new SortedDictionary<int, List<IShieldingUCWeakMutSetEffectObserver>>();
    foreach (var entry in observersForShieldingUCWeakMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForShieldingUCWeakMutSet.Add(
          objectId,
          new List<IShieldingUCWeakMutSetEffectObserver>(
              observers));
    }

    var copyOfObserversForAttackAICapabilityUCWeakMutSet =
        new SortedDictionary<int, List<IAttackAICapabilityUCWeakMutSetEffectObserver>>();
    foreach (var entry in observersForAttackAICapabilityUCWeakMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForAttackAICapabilityUCWeakMutSet.Add(
          objectId,
          new List<IAttackAICapabilityUCWeakMutSetEffectObserver>(
              observers));
    }

    var copyOfObserversForKillDirectiveUCMutSet =
        new SortedDictionary<int, List<IKillDirectiveUCMutSetEffectObserver>>();
    foreach (var entry in observersForKillDirectiveUCMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForKillDirectiveUCMutSet.Add(
          objectId,
          new List<IKillDirectiveUCMutSetEffectObserver>(
              observers));
    }

    var copyOfObserversForMoveDirectiveUCMutSet =
        new SortedDictionary<int, List<IMoveDirectiveUCMutSetEffectObserver>>();
    foreach (var entry in observersForMoveDirectiveUCMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForMoveDirectiveUCMutSet.Add(
          objectId,
          new List<IMoveDirectiveUCMutSetEffectObserver>(
              observers));
    }

    var copyOfObserversForWanderAICapabilityUCMutSet =
        new SortedDictionary<int, List<IWanderAICapabilityUCMutSetEffectObserver>>();
    foreach (var entry in observersForWanderAICapabilityUCMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForWanderAICapabilityUCMutSet.Add(
          objectId,
          new List<IWanderAICapabilityUCMutSetEffectObserver>(
              observers));
    }

    var copyOfObserversForBideAICapabilityUCMutSet =
        new SortedDictionary<int, List<IBideAICapabilityUCMutSetEffectObserver>>();
    foreach (var entry in observersForBideAICapabilityUCMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForBideAICapabilityUCMutSet.Add(
          objectId,
          new List<IBideAICapabilityUCMutSetEffectObserver>(
              observers));
    }

    var copyOfObserversForAttackAICapabilityUCMutSet =
        new SortedDictionary<int, List<IAttackAICapabilityUCMutSetEffectObserver>>();
    foreach (var entry in observersForAttackAICapabilityUCMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForAttackAICapabilityUCMutSet.Add(
          objectId,
          new List<IAttackAICapabilityUCMutSetEffectObserver>(
              observers));
    }

    var copyOfObserversForShieldingUCMutSet =
        new SortedDictionary<int, List<IShieldingUCMutSetEffectObserver>>();
    foreach (var entry in observersForShieldingUCMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForShieldingUCMutSet.Add(
          objectId,
          new List<IShieldingUCMutSetEffectObserver>(
              observers));
    }

    var copyOfObserversForBidingOperationUCMutSet =
        new SortedDictionary<int, List<IBidingOperationUCMutSetEffectObserver>>();
    foreach (var entry in observersForBidingOperationUCMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForBidingOperationUCMutSet.Add(
          objectId,
          new List<IBidingOperationUCMutSetEffectObserver>(
              observers));
    }

    var copyOfObserversForGlaiveMutSet =
        new SortedDictionary<int, List<IGlaiveMutSetEffectObserver>>();
    foreach (var entry in observersForGlaiveMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForGlaiveMutSet.Add(
          objectId,
          new List<IGlaiveMutSetEffectObserver>(
              observers));
    }

    var copyOfObserversForArmorMutSet =
        new SortedDictionary<int, List<IArmorMutSetEffectObserver>>();
    foreach (var entry in observersForArmorMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForArmorMutSet.Add(
          objectId,
          new List<IArmorMutSetEffectObserver>(
              observers));
    }

    var copyOfObserversForItemTerrainTileComponentMutSet =
        new SortedDictionary<int, List<IItemTerrainTileComponentMutSetEffectObserver>>();
    foreach (var entry in observersForItemTerrainTileComponentMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForItemTerrainTileComponentMutSet.Add(
          objectId,
          new List<IItemTerrainTileComponentMutSetEffectObserver>(
              observers));
    }

    var copyOfObserversForDecorativeTerrainTileComponentMutSet =
        new SortedDictionary<int, List<IDecorativeTerrainTileComponentMutSetEffectObserver>>();
    foreach (var entry in observersForDecorativeTerrainTileComponentMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForDecorativeTerrainTileComponentMutSet.Add(
          objectId,
          new List<IDecorativeTerrainTileComponentMutSetEffectObserver>(
              observers));
    }

    var copyOfObserversForUpStaircaseTerrainTileComponentMutSet =
        new SortedDictionary<int, List<IUpStaircaseTerrainTileComponentMutSetEffectObserver>>();
    foreach (var entry in observersForUpStaircaseTerrainTileComponentMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForUpStaircaseTerrainTileComponentMutSet.Add(
          objectId,
          new List<IUpStaircaseTerrainTileComponentMutSetEffectObserver>(
              observers));
    }

    var copyOfObserversForDownStaircaseTerrainTileComponentMutSet =
        new SortedDictionary<int, List<IDownStaircaseTerrainTileComponentMutSetEffectObserver>>();
    foreach (var entry in observersForDownStaircaseTerrainTileComponentMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForDownStaircaseTerrainTileComponentMutSet.Add(
          objectId,
          new List<IDownStaircaseTerrainTileComponentMutSetEffectObserver>(
              observers));
    }

    var copyOfObserversForUnitMutSet =
        new SortedDictionary<int, List<IUnitMutSetEffectObserver>>();
    foreach (var entry in observersForUnitMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForUnitMutSet.Add(
          objectId,
          new List<IUnitMutSetEffectObserver>(
              observers));
    }

    var copyOfObserversForTerrainTileByLocationMutMap =
        new SortedDictionary<int, List<ITerrainTileByLocationMutMapEffectObserver>>();
    foreach (var entry in observersForTerrainTileByLocationMutMap) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForTerrainTileByLocationMutMap.Add(
          objectId,
          new List<ITerrainTileByLocationMutMapEffectObserver>(
              observers));
    }

    BroadcastLevelEffects(
        copyOfObserversForLevel);
           
    BroadcastItemTerrainTileComponentEffects(
        copyOfObserversForItemTerrainTileComponent);
           
    BroadcastDecorativeTerrainTileComponentEffects(
        copyOfObserversForDecorativeTerrainTileComponent);
           
    BroadcastUpStaircaseTerrainTileComponentEffects(
        copyOfObserversForUpStaircaseTerrainTileComponent);
           
    BroadcastDownStaircaseTerrainTileComponentEffects(
        copyOfObserversForDownStaircaseTerrainTileComponent);
           
    BroadcastTerrainTileEffects(
        copyOfObserversForTerrainTile);
           
    BroadcastITerrainTileComponentMutBunchEffects(
        copyOfObserversForITerrainTileComponentMutBunch);
           
    BroadcastTerrainEffects(
        copyOfObserversForTerrain);
           
    BroadcastGlaiveEffects(
        copyOfObserversForGlaive);
           
    BroadcastArmorEffects(
        copyOfObserversForArmor);
           
    BroadcastRandEffects(
        copyOfObserversForRand);
           
    BroadcastWanderAICapabilityUCEffects(
        copyOfObserversForWanderAICapabilityUC);
           
    BroadcastShieldingUCEffects(
        copyOfObserversForShieldingUC);
           
    BroadcastBidingOperationUCEffects(
        copyOfObserversForBidingOperationUC);
           
    BroadcastUnleashBideImpulseEffects(
        copyOfObserversForUnleashBideImpulse);
           
    BroadcastStartBidingImpulseEffects(
        copyOfObserversForStartBidingImpulse);
           
    BroadcastBideAICapabilityUCEffects(
        copyOfObserversForBideAICapabilityUC);
           
    BroadcastAttackImpulseEffects(
        copyOfObserversForAttackImpulse);
           
    BroadcastPursueImpulseEffects(
        copyOfObserversForPursueImpulse);
           
    BroadcastKillDirectiveUCEffects(
        copyOfObserversForKillDirectiveUC);
           
    BroadcastAttackAICapabilityUCEffects(
        copyOfObserversForAttackAICapabilityUC);
           
    BroadcastMoveImpulseEffects(
        copyOfObserversForMoveImpulse);
           
    BroadcastMoveDirectiveUCEffects(
        copyOfObserversForMoveDirectiveUC);
           
    BroadcastUnitEffects(
        copyOfObserversForUnit);
           
    BroadcastIItemMutBunchEffects(
        copyOfObserversForIItemMutBunch);
           
    BroadcastIUnitComponentMutBunchEffects(
        copyOfObserversForIUnitComponentMutBunch);
           
    BroadcastNoImpulseEffects(
        copyOfObserversForNoImpulse);
           
    BroadcastExecutionStateEffects(
        copyOfObserversForExecutionState);
           
    BroadcastIPostActingUCWeakMutBunchEffects(
        copyOfObserversForIPostActingUCWeakMutBunch);
           
    BroadcastIPreActingUCWeakMutBunchEffects(
        copyOfObserversForIPreActingUCWeakMutBunch);
           
    BroadcastGameEffects(
        copyOfObserversForGame);
           
    BroadcastIUnitEventMutListEffects(
        copyOfObserversForIUnitEventMutList);
           
    BroadcastLocationMutListEffects(
        copyOfObserversForLocationMutList);
           
    BroadcastLevelMutSetEffects(
        copyOfObserversForLevelMutSet);
           
    BroadcastShieldingUCWeakMutSetEffects(
        copyOfObserversForShieldingUCWeakMutSet);
           
    BroadcastAttackAICapabilityUCWeakMutSetEffects(
        copyOfObserversForAttackAICapabilityUCWeakMutSet);
           
    BroadcastKillDirectiveUCMutSetEffects(
        copyOfObserversForKillDirectiveUCMutSet);
           
    BroadcastMoveDirectiveUCMutSetEffects(
        copyOfObserversForMoveDirectiveUCMutSet);
           
    BroadcastWanderAICapabilityUCMutSetEffects(
        copyOfObserversForWanderAICapabilityUCMutSet);
           
    BroadcastBideAICapabilityUCMutSetEffects(
        copyOfObserversForBideAICapabilityUCMutSet);
           
    BroadcastAttackAICapabilityUCMutSetEffects(
        copyOfObserversForAttackAICapabilityUCMutSet);
           
    BroadcastShieldingUCMutSetEffects(
        copyOfObserversForShieldingUCMutSet);
           
    BroadcastBidingOperationUCMutSetEffects(
        copyOfObserversForBidingOperationUCMutSet);
           
    BroadcastGlaiveMutSetEffects(
        copyOfObserversForGlaiveMutSet);
           
    BroadcastArmorMutSetEffects(
        copyOfObserversForArmorMutSet);
           
    BroadcastItemTerrainTileComponentMutSetEffects(
        copyOfObserversForItemTerrainTileComponentMutSet);
           
    BroadcastDecorativeTerrainTileComponentMutSetEffects(
        copyOfObserversForDecorativeTerrainTileComponentMutSet);
           
    BroadcastUpStaircaseTerrainTileComponentMutSetEffects(
        copyOfObserversForUpStaircaseTerrainTileComponentMutSet);
           
    BroadcastDownStaircaseTerrainTileComponentMutSetEffects(
        copyOfObserversForDownStaircaseTerrainTileComponentMutSet);
           
    BroadcastUnitMutSetEffects(
        copyOfObserversForUnitMutSet);
           
    BroadcastTerrainTileByLocationMutMapEffects(
        copyOfObserversForTerrainTileByLocationMutMap);
           
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


    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsLevel) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsLevel.ContainsKey(sourceObjId)) {
        EffectInternalCreateLevel(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsItemTerrainTileComponent) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsItemTerrainTileComponent.ContainsKey(sourceObjId)) {
        EffectInternalCreateItemTerrainTileComponent(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDecorativeTerrainTileComponent) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsDecorativeTerrainTileComponent.ContainsKey(sourceObjId)) {
        EffectInternalCreateDecorativeTerrainTileComponent(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsUpStaircaseTerrainTileComponent) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsUpStaircaseTerrainTileComponent.ContainsKey(sourceObjId)) {
        EffectInternalCreateUpStaircaseTerrainTileComponent(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDownStaircaseTerrainTileComponent) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsDownStaircaseTerrainTileComponent.ContainsKey(sourceObjId)) {
        EffectInternalCreateDownStaircaseTerrainTileComponent(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTerrainTile) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsTerrainTile.ContainsKey(sourceObjId)) {
        EffectInternalCreateTerrainTile(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsITerrainTileComponentMutBunch) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsITerrainTileComponentMutBunch.ContainsKey(sourceObjId)) {
        EffectInternalCreateITerrainTileComponentMutBunch(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTerrain) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsTerrain.ContainsKey(sourceObjId)) {
        EffectInternalCreateTerrain(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsGlaive) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsGlaive.ContainsKey(sourceObjId)) {
        EffectInternalCreateGlaive(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsArmor) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsArmor.ContainsKey(sourceObjId)) {
        EffectInternalCreateArmor(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsRand) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsRand.ContainsKey(sourceObjId)) {
        EffectInternalCreateRand(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsWanderAICapabilityUC) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsWanderAICapabilityUC.ContainsKey(sourceObjId)) {
        EffectInternalCreateWanderAICapabilityUC(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsShieldingUC) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsShieldingUC.ContainsKey(sourceObjId)) {
        EffectInternalCreateShieldingUC(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsBidingOperationUC) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsBidingOperationUC.ContainsKey(sourceObjId)) {
        EffectInternalCreateBidingOperationUC(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsUnleashBideImpulse) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsUnleashBideImpulse.ContainsKey(sourceObjId)) {
        EffectInternalCreateUnleashBideImpulse(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsStartBidingImpulse) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsStartBidingImpulse.ContainsKey(sourceObjId)) {
        EffectInternalCreateStartBidingImpulse(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsBideAICapabilityUC) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsBideAICapabilityUC.ContainsKey(sourceObjId)) {
        EffectInternalCreateBideAICapabilityUC(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsAttackImpulse) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsAttackImpulse.ContainsKey(sourceObjId)) {
        EffectInternalCreateAttackImpulse(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsPursueImpulse) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsPursueImpulse.ContainsKey(sourceObjId)) {
        EffectInternalCreatePursueImpulse(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsKillDirectiveUC) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsKillDirectiveUC.ContainsKey(sourceObjId)) {
        EffectInternalCreateKillDirectiveUC(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsAttackAICapabilityUC) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsAttackAICapabilityUC.ContainsKey(sourceObjId)) {
        EffectInternalCreateAttackAICapabilityUC(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsMoveImpulse) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsMoveImpulse.ContainsKey(sourceObjId)) {
        EffectInternalCreateMoveImpulse(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsMoveDirectiveUC) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsMoveDirectiveUC.ContainsKey(sourceObjId)) {
        EffectInternalCreateMoveDirectiveUC(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsUnit) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsUnit.ContainsKey(sourceObjId)) {
        EffectInternalCreateUnit(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIItemMutBunch) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsIItemMutBunch.ContainsKey(sourceObjId)) {
        EffectInternalCreateIItemMutBunch(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIUnitComponentMutBunch) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsIUnitComponentMutBunch.ContainsKey(sourceObjId)) {
        EffectInternalCreateIUnitComponentMutBunch(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsNoImpulse) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsNoImpulse.ContainsKey(sourceObjId)) {
        EffectInternalCreateNoImpulse(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsExecutionState) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsExecutionState.ContainsKey(sourceObjId)) {
        EffectInternalCreateExecutionState(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIPostActingUCWeakMutBunch) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsIPostActingUCWeakMutBunch.ContainsKey(sourceObjId)) {
        EffectInternalCreateIPostActingUCWeakMutBunch(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIPreActingUCWeakMutBunch) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsIPreActingUCWeakMutBunch.ContainsKey(sourceObjId)) {
        EffectInternalCreateIPreActingUCWeakMutBunch(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsGame) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsGame.ContainsKey(sourceObjId)) {
        EffectInternalCreateGame(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIUnitEventMutList) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsIUnitEventMutList.ContainsKey(sourceObjId)) {
        EffectInternalCreateIUnitEventMutList(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsLocationMutList) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsLocationMutList.ContainsKey(sourceObjId)) {
        EffectInternalCreateLocationMutList(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsLevelMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsLevelMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateLevelMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsShieldingUCWeakMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsShieldingUCWeakMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateShieldingUCWeakMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsAttackAICapabilityUCWeakMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateAttackAICapabilityUCWeakMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsKillDirectiveUCMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsKillDirectiveUCMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateKillDirectiveUCMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsMoveDirectiveUCMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsMoveDirectiveUCMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateMoveDirectiveUCMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsWanderAICapabilityUCMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsWanderAICapabilityUCMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateWanderAICapabilityUCMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsBideAICapabilityUCMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsBideAICapabilityUCMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateBideAICapabilityUCMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsAttackAICapabilityUCMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsAttackAICapabilityUCMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateAttackAICapabilityUCMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsShieldingUCMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsShieldingUCMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateShieldingUCMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsBidingOperationUCMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsBidingOperationUCMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateBidingOperationUCMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsGlaiveMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsGlaiveMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateGlaiveMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsArmorMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsArmorMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateArmorMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsItemTerrainTileComponentMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsItemTerrainTileComponentMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateItemTerrainTileComponentMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDecorativeTerrainTileComponentMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateDecorativeTerrainTileComponentMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateUpStaircaseTerrainTileComponentMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateDownStaircaseTerrainTileComponentMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsUnitMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsUnitMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateUnitMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTerrainTileByLocationMutMap) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsTerrainTileByLocationMutMap.ContainsKey(sourceObjId)) {
        EffectInternalCreateTerrainTileByLocationMutMap(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
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
            rootIncarnation.incarnationsLevelMutSet[objId] = sourceVersionAndObjIncarnation;
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
            rootIncarnation.incarnationsShieldingUCWeakMutSet[objId] = sourceVersionAndObjIncarnation;
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
            rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet[objId] = sourceVersionAndObjIncarnation;
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
            rootIncarnation.incarnationsKillDirectiveUCMutSet[objId] = sourceVersionAndObjIncarnation;
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
            rootIncarnation.incarnationsMoveDirectiveUCMutSet[objId] = sourceVersionAndObjIncarnation;
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
            rootIncarnation.incarnationsWanderAICapabilityUCMutSet[objId] = sourceVersionAndObjIncarnation;
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
            rootIncarnation.incarnationsBideAICapabilityUCMutSet[objId] = sourceVersionAndObjIncarnation;
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
            rootIncarnation.incarnationsAttackAICapabilityUCMutSet[objId] = sourceVersionAndObjIncarnation;
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
            rootIncarnation.incarnationsShieldingUCMutSet[objId] = sourceVersionAndObjIncarnation;
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
            rootIncarnation.incarnationsBidingOperationUCMutSet[objId] = sourceVersionAndObjIncarnation;
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
            rootIncarnation.incarnationsGlaiveMutSet[objId] = sourceVersionAndObjIncarnation;
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
            rootIncarnation.incarnationsArmorMutSet[objId] = sourceVersionAndObjIncarnation;
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsItemTerrainTileComponentMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsItemTerrainTileComponentMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsItemTerrainTileComponentMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in currentObjIncarnation.set) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectItemTerrainTileComponentMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectItemTerrainTileComponentMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.incarnationsItemTerrainTileComponentMutSet[objId] = sourceVersionAndObjIncarnation;
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
            rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet[objId] = sourceVersionAndObjIncarnation;
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
            rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet[objId] = sourceVersionAndObjIncarnation;
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
            rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet[objId] = sourceVersionAndObjIncarnation;
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
            rootIncarnation.incarnationsUnitMutSet[objId] = sourceVersionAndObjIncarnation;
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

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsItemTerrainTileComponent) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsItemTerrainTileComponent.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsItemTerrainTileComponent[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
            rootIncarnation.hash -=
                GetItemTerrainTileComponentHash(
                    objId,
                    rootIncarnation.incarnationsItemTerrainTileComponent[objId].version,
                    rootIncarnation.incarnationsItemTerrainTileComponent[objId].incarnation);
          rootIncarnation.incarnationsItemTerrainTileComponent[objId] = sourceVersionAndObjIncarnation;
            rootIncarnation.hash +=
                GetItemTerrainTileComponentHash(
                    objId,
                    rootIncarnation.incarnationsItemTerrainTileComponent[objId].version,
                    rootIncarnation.incarnationsItemTerrainTileComponent[objId].incarnation);
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

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<ItemTerrainTileComponentIncarnation>>(rootIncarnation.incarnationsItemTerrainTileComponent)) {
      if (!sourceIncarnation.incarnationsItemTerrainTileComponent.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectItemTerrainTileComponentDelete(id);
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

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<ItemTerrainTileComponentMutSetIncarnation>>(rootIncarnation.incarnationsItemTerrainTileComponentMutSet)) {
      if (!sourceIncarnation.incarnationsItemTerrainTileComponentMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectItemTerrainTileComponentMutSetDelete(id);
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
      throw new System.Exception("Invalid Level: " + id);
    }
  }
  public void AddLevelObserver(int id, ILevelEffectObserver observer) {
    List<ILevelEffectObserver> obsies;
    if (!observersForLevel.TryGetValue(id, out obsies)) {
      obsies = new List<ILevelEffectObserver>();
    }
    obsies.Add(observer);
    observersForLevel[id] = obsies;
  }

  public void RemoveLevelObserver(int id, ILevelEffectObserver observer) {
    if (observersForLevel.ContainsKey(id)) {
      var list = observersForLevel[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForLevel.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
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
    EffectInternalCreateLevel(id, rootIncarnation.version, incarnation);
    return new Level(this, id);
  }
  public void EffectInternalCreateLevel(
      int id,
      int incarnationVersion,
      LevelIncarnation incarnation) {
    CheckUnlocked();
    var effect = new LevelCreateEffect(id);
    rootIncarnation.incarnationsLevel.Add(
        id,
        new VersionAndIncarnation<LevelIncarnation>(
            incarnationVersion,
            incarnation));
    effectsLevelCreateEffect.Add(effect);
  }

  public void EffectLevelDelete(int id) {
    CheckUnlocked();
    var effect = new LevelDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsLevel[id];

    rootIncarnation.incarnationsLevel.Remove(id);
    effectsLevelDeleteEffect.Add(effect);
  }

     
  public int GetLevelHash(int id, int version, LevelIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.name.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.considerCornersAdjacent.GetDeterministicHashCode();
    result += id * version * 3 * incarnation.terrain.GetDeterministicHashCode();
    result += id * version * 4 * incarnation.units.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastLevelEffects(
      SortedDictionary<int, List<ILevelEffectObserver>> observers) {
    foreach (var effect in effectsLevelDeleteEffect) {
      if (observers.TryGetValue(0, out List<ILevelEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnLevelEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ILevelEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnLevelEffect(effect);
        }
        observersForLevel.Remove(effect.id);
      }
    }
    effectsLevelDeleteEffect.Clear();


    foreach (var effect in effectsLevelCreateEffect) {
      if (observers.TryGetValue(0, out List<ILevelEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnLevelEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ILevelEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnLevelEffect(effect);
        }
      }
    }
    effectsLevelCreateEffect.Clear();
  }
  public ItemTerrainTileComponentIncarnation GetItemTerrainTileComponentIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsItemTerrainTileComponent[id].incarnation;
  }
  public bool ItemTerrainTileComponentExists(int id) {
    return rootIncarnation.incarnationsItemTerrainTileComponent.ContainsKey(id);
  }
  public ItemTerrainTileComponent GetItemTerrainTileComponent(int id) {
    return new ItemTerrainTileComponent(this, id);
  }
  public List<ItemTerrainTileComponent> AllItemTerrainTileComponent() {
    List<ItemTerrainTileComponent> result = new List<ItemTerrainTileComponent>(rootIncarnation.incarnationsItemTerrainTileComponent.Count);
    foreach (var id in rootIncarnation.incarnationsItemTerrainTileComponent.Keys) {
      result.Add(new ItemTerrainTileComponent(this, id));
    }
    return result;
  }
  public IEnumerator<ItemTerrainTileComponent> EnumAllItemTerrainTileComponent() {
    foreach (var id in rootIncarnation.incarnationsItemTerrainTileComponent.Keys) {
      yield return GetItemTerrainTileComponent(id);
    }
  }
  public void CheckHasItemTerrainTileComponent(ItemTerrainTileComponent thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasItemTerrainTileComponent(thing.id);
  }
  public void CheckHasItemTerrainTileComponent(int id) {
    if (!rootIncarnation.incarnationsItemTerrainTileComponent.ContainsKey(id)) {
      throw new System.Exception("Invalid ItemTerrainTileComponent: " + id);
    }
  }
  public void AddItemTerrainTileComponentObserver(int id, IItemTerrainTileComponentEffectObserver observer) {
    List<IItemTerrainTileComponentEffectObserver> obsies;
    if (!observersForItemTerrainTileComponent.TryGetValue(id, out obsies)) {
      obsies = new List<IItemTerrainTileComponentEffectObserver>();
    }
    obsies.Add(observer);
    observersForItemTerrainTileComponent[id] = obsies;
  }

  public void RemoveItemTerrainTileComponentObserver(int id, IItemTerrainTileComponentEffectObserver observer) {
    if (observersForItemTerrainTileComponent.ContainsKey(id)) {
      var list = observersForItemTerrainTileComponent[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForItemTerrainTileComponent.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public ItemTerrainTileComponent EffectItemTerrainTileComponentCreate(
      IItem item) {
    CheckUnlocked();
    CheckHasIItem(item);

    var id = NewId();
    var incarnation =
        new ItemTerrainTileComponentIncarnation(
            item.id
            );
    EffectInternalCreateItemTerrainTileComponent(id, rootIncarnation.version, incarnation);
    return new ItemTerrainTileComponent(this, id);
  }
  public void EffectInternalCreateItemTerrainTileComponent(
      int id,
      int incarnationVersion,
      ItemTerrainTileComponentIncarnation incarnation) {
    CheckUnlocked();
    var effect = new ItemTerrainTileComponentCreateEffect(id);
    rootIncarnation.incarnationsItemTerrainTileComponent.Add(
        id,
        new VersionAndIncarnation<ItemTerrainTileComponentIncarnation>(
            incarnationVersion,
            incarnation));
    effectsItemTerrainTileComponentCreateEffect.Add(effect);
  }

  public void EffectItemTerrainTileComponentDelete(int id) {
    CheckUnlocked();
    var effect = new ItemTerrainTileComponentDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsItemTerrainTileComponent[id];

    rootIncarnation.incarnationsItemTerrainTileComponent.Remove(id);
    effectsItemTerrainTileComponentDeleteEffect.Add(effect);
  }

     
  public int GetItemTerrainTileComponentHash(int id, int version, ItemTerrainTileComponentIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.item.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastItemTerrainTileComponentEffects(
      SortedDictionary<int, List<IItemTerrainTileComponentEffectObserver>> observers) {
    foreach (var effect in effectsItemTerrainTileComponentDeleteEffect) {
      if (observers.TryGetValue(0, out List<IItemTerrainTileComponentEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnItemTerrainTileComponentEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IItemTerrainTileComponentEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnItemTerrainTileComponentEffect(effect);
        }
        observersForItemTerrainTileComponent.Remove(effect.id);
      }
    }
    effectsItemTerrainTileComponentDeleteEffect.Clear();


    foreach (var effect in effectsItemTerrainTileComponentCreateEffect) {
      if (observers.TryGetValue(0, out List<IItemTerrainTileComponentEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnItemTerrainTileComponentEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IItemTerrainTileComponentEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnItemTerrainTileComponentEffect(effect);
        }
      }
    }
    effectsItemTerrainTileComponentCreateEffect.Clear();
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
      throw new System.Exception("Invalid DecorativeTerrainTileComponent: " + id);
    }
  }
  public void AddDecorativeTerrainTileComponentObserver(int id, IDecorativeTerrainTileComponentEffectObserver observer) {
    List<IDecorativeTerrainTileComponentEffectObserver> obsies;
    if (!observersForDecorativeTerrainTileComponent.TryGetValue(id, out obsies)) {
      obsies = new List<IDecorativeTerrainTileComponentEffectObserver>();
    }
    obsies.Add(observer);
    observersForDecorativeTerrainTileComponent[id] = obsies;
  }

  public void RemoveDecorativeTerrainTileComponentObserver(int id, IDecorativeTerrainTileComponentEffectObserver observer) {
    if (observersForDecorativeTerrainTileComponent.ContainsKey(id)) {
      var list = observersForDecorativeTerrainTileComponent[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForDecorativeTerrainTileComponent.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
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
    EffectInternalCreateDecorativeTerrainTileComponent(id, rootIncarnation.version, incarnation);
    return new DecorativeTerrainTileComponent(this, id);
  }
  public void EffectInternalCreateDecorativeTerrainTileComponent(
      int id,
      int incarnationVersion,
      DecorativeTerrainTileComponentIncarnation incarnation) {
    CheckUnlocked();
    var effect = new DecorativeTerrainTileComponentCreateEffect(id);
    rootIncarnation.incarnationsDecorativeTerrainTileComponent.Add(
        id,
        new VersionAndIncarnation<DecorativeTerrainTileComponentIncarnation>(
            incarnationVersion,
            incarnation));
    effectsDecorativeTerrainTileComponentCreateEffect.Add(effect);
  }

  public void EffectDecorativeTerrainTileComponentDelete(int id) {
    CheckUnlocked();
    var effect = new DecorativeTerrainTileComponentDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsDecorativeTerrainTileComponent[id];

    rootIncarnation.incarnationsDecorativeTerrainTileComponent.Remove(id);
    effectsDecorativeTerrainTileComponentDeleteEffect.Add(effect);
  }

     
  public int GetDecorativeTerrainTileComponentHash(int id, int version, DecorativeTerrainTileComponentIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.symbolId.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastDecorativeTerrainTileComponentEffects(
      SortedDictionary<int, List<IDecorativeTerrainTileComponentEffectObserver>> observers) {
    foreach (var effect in effectsDecorativeTerrainTileComponentDeleteEffect) {
      if (observers.TryGetValue(0, out List<IDecorativeTerrainTileComponentEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnDecorativeTerrainTileComponentEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IDecorativeTerrainTileComponentEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnDecorativeTerrainTileComponentEffect(effect);
        }
        observersForDecorativeTerrainTileComponent.Remove(effect.id);
      }
    }
    effectsDecorativeTerrainTileComponentDeleteEffect.Clear();


    foreach (var effect in effectsDecorativeTerrainTileComponentCreateEffect) {
      if (observers.TryGetValue(0, out List<IDecorativeTerrainTileComponentEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnDecorativeTerrainTileComponentEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IDecorativeTerrainTileComponentEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnDecorativeTerrainTileComponentEffect(effect);
        }
      }
    }
    effectsDecorativeTerrainTileComponentCreateEffect.Clear();
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
      throw new System.Exception("Invalid UpStaircaseTerrainTileComponent: " + id);
    }
  }
  public void AddUpStaircaseTerrainTileComponentObserver(int id, IUpStaircaseTerrainTileComponentEffectObserver observer) {
    List<IUpStaircaseTerrainTileComponentEffectObserver> obsies;
    if (!observersForUpStaircaseTerrainTileComponent.TryGetValue(id, out obsies)) {
      obsies = new List<IUpStaircaseTerrainTileComponentEffectObserver>();
    }
    obsies.Add(observer);
    observersForUpStaircaseTerrainTileComponent[id] = obsies;
  }

  public void RemoveUpStaircaseTerrainTileComponentObserver(int id, IUpStaircaseTerrainTileComponentEffectObserver observer) {
    if (observersForUpStaircaseTerrainTileComponent.ContainsKey(id)) {
      var list = observersForUpStaircaseTerrainTileComponent[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForUpStaircaseTerrainTileComponent.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public UpStaircaseTerrainTileComponent EffectUpStaircaseTerrainTileComponentCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new UpStaircaseTerrainTileComponentIncarnation(

            );
    EffectInternalCreateUpStaircaseTerrainTileComponent(id, rootIncarnation.version, incarnation);
    return new UpStaircaseTerrainTileComponent(this, id);
  }
  public void EffectInternalCreateUpStaircaseTerrainTileComponent(
      int id,
      int incarnationVersion,
      UpStaircaseTerrainTileComponentIncarnation incarnation) {
    CheckUnlocked();
    var effect = new UpStaircaseTerrainTileComponentCreateEffect(id);
    rootIncarnation.incarnationsUpStaircaseTerrainTileComponent.Add(
        id,
        new VersionAndIncarnation<UpStaircaseTerrainTileComponentIncarnation>(
            incarnationVersion,
            incarnation));
    effectsUpStaircaseTerrainTileComponentCreateEffect.Add(effect);
  }

  public void EffectUpStaircaseTerrainTileComponentDelete(int id) {
    CheckUnlocked();
    var effect = new UpStaircaseTerrainTileComponentDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsUpStaircaseTerrainTileComponent[id];

    rootIncarnation.incarnationsUpStaircaseTerrainTileComponent.Remove(id);
    effectsUpStaircaseTerrainTileComponentDeleteEffect.Add(effect);
  }

     
  public int GetUpStaircaseTerrainTileComponentHash(int id, int version, UpStaircaseTerrainTileComponentIncarnation incarnation) {
    int result = id * version;
    return result;
  }
     
  public void BroadcastUpStaircaseTerrainTileComponentEffects(
      SortedDictionary<int, List<IUpStaircaseTerrainTileComponentEffectObserver>> observers) {
    foreach (var effect in effectsUpStaircaseTerrainTileComponentDeleteEffect) {
      if (observers.TryGetValue(0, out List<IUpStaircaseTerrainTileComponentEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnUpStaircaseTerrainTileComponentEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IUpStaircaseTerrainTileComponentEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnUpStaircaseTerrainTileComponentEffect(effect);
        }
        observersForUpStaircaseTerrainTileComponent.Remove(effect.id);
      }
    }
    effectsUpStaircaseTerrainTileComponentDeleteEffect.Clear();


    foreach (var effect in effectsUpStaircaseTerrainTileComponentCreateEffect) {
      if (observers.TryGetValue(0, out List<IUpStaircaseTerrainTileComponentEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnUpStaircaseTerrainTileComponentEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IUpStaircaseTerrainTileComponentEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnUpStaircaseTerrainTileComponentEffect(effect);
        }
      }
    }
    effectsUpStaircaseTerrainTileComponentCreateEffect.Clear();
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
      throw new System.Exception("Invalid DownStaircaseTerrainTileComponent: " + id);
    }
  }
  public void AddDownStaircaseTerrainTileComponentObserver(int id, IDownStaircaseTerrainTileComponentEffectObserver observer) {
    List<IDownStaircaseTerrainTileComponentEffectObserver> obsies;
    if (!observersForDownStaircaseTerrainTileComponent.TryGetValue(id, out obsies)) {
      obsies = new List<IDownStaircaseTerrainTileComponentEffectObserver>();
    }
    obsies.Add(observer);
    observersForDownStaircaseTerrainTileComponent[id] = obsies;
  }

  public void RemoveDownStaircaseTerrainTileComponentObserver(int id, IDownStaircaseTerrainTileComponentEffectObserver observer) {
    if (observersForDownStaircaseTerrainTileComponent.ContainsKey(id)) {
      var list = observersForDownStaircaseTerrainTileComponent[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForDownStaircaseTerrainTileComponent.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public DownStaircaseTerrainTileComponent EffectDownStaircaseTerrainTileComponentCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new DownStaircaseTerrainTileComponentIncarnation(

            );
    EffectInternalCreateDownStaircaseTerrainTileComponent(id, rootIncarnation.version, incarnation);
    return new DownStaircaseTerrainTileComponent(this, id);
  }
  public void EffectInternalCreateDownStaircaseTerrainTileComponent(
      int id,
      int incarnationVersion,
      DownStaircaseTerrainTileComponentIncarnation incarnation) {
    CheckUnlocked();
    var effect = new DownStaircaseTerrainTileComponentCreateEffect(id);
    rootIncarnation.incarnationsDownStaircaseTerrainTileComponent.Add(
        id,
        new VersionAndIncarnation<DownStaircaseTerrainTileComponentIncarnation>(
            incarnationVersion,
            incarnation));
    effectsDownStaircaseTerrainTileComponentCreateEffect.Add(effect);
  }

  public void EffectDownStaircaseTerrainTileComponentDelete(int id) {
    CheckUnlocked();
    var effect = new DownStaircaseTerrainTileComponentDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsDownStaircaseTerrainTileComponent[id];

    rootIncarnation.incarnationsDownStaircaseTerrainTileComponent.Remove(id);
    effectsDownStaircaseTerrainTileComponentDeleteEffect.Add(effect);
  }

     
  public int GetDownStaircaseTerrainTileComponentHash(int id, int version, DownStaircaseTerrainTileComponentIncarnation incarnation) {
    int result = id * version;
    return result;
  }
     
  public void BroadcastDownStaircaseTerrainTileComponentEffects(
      SortedDictionary<int, List<IDownStaircaseTerrainTileComponentEffectObserver>> observers) {
    foreach (var effect in effectsDownStaircaseTerrainTileComponentDeleteEffect) {
      if (observers.TryGetValue(0, out List<IDownStaircaseTerrainTileComponentEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnDownStaircaseTerrainTileComponentEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IDownStaircaseTerrainTileComponentEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnDownStaircaseTerrainTileComponentEffect(effect);
        }
        observersForDownStaircaseTerrainTileComponent.Remove(effect.id);
      }
    }
    effectsDownStaircaseTerrainTileComponentDeleteEffect.Clear();


    foreach (var effect in effectsDownStaircaseTerrainTileComponentCreateEffect) {
      if (observers.TryGetValue(0, out List<IDownStaircaseTerrainTileComponentEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnDownStaircaseTerrainTileComponentEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IDownStaircaseTerrainTileComponentEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnDownStaircaseTerrainTileComponentEffect(effect);
        }
      }
    }
    effectsDownStaircaseTerrainTileComponentCreateEffect.Clear();
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
      throw new System.Exception("Invalid TerrainTile: " + id);
    }
  }
  public void AddTerrainTileObserver(int id, ITerrainTileEffectObserver observer) {
    List<ITerrainTileEffectObserver> obsies;
    if (!observersForTerrainTile.TryGetValue(id, out obsies)) {
      obsies = new List<ITerrainTileEffectObserver>();
    }
    obsies.Add(observer);
    observersForTerrainTile[id] = obsies;
  }

  public void RemoveTerrainTileObserver(int id, ITerrainTileEffectObserver observer) {
    if (observersForTerrainTile.ContainsKey(id)) {
      var list = observersForTerrainTile[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForTerrainTile.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
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
    EffectInternalCreateTerrainTile(id, rootIncarnation.version, incarnation);
    return new TerrainTile(this, id);
  }
  public void EffectInternalCreateTerrainTile(
      int id,
      int incarnationVersion,
      TerrainTileIncarnation incarnation) {
    CheckUnlocked();
    var effect = new TerrainTileCreateEffect(id);
    rootIncarnation.incarnationsTerrainTile.Add(
        id,
        new VersionAndIncarnation<TerrainTileIncarnation>(
            incarnationVersion,
            incarnation));
    effectsTerrainTileCreateEffect.Add(effect);
  }

  public void EffectTerrainTileDelete(int id) {
    CheckUnlocked();
    var effect = new TerrainTileDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsTerrainTile[id];

    rootIncarnation.incarnationsTerrainTile.Remove(id);
    effectsTerrainTileDeleteEffect.Add(effect);
  }

     
  public int GetTerrainTileHash(int id, int version, TerrainTileIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.elevation.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.walkable.GetDeterministicHashCode();
    result += id * version * 3 * incarnation.classId.GetDeterministicHashCode();
    result += id * version * 4 * incarnation.components.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastTerrainTileEffects(
      SortedDictionary<int, List<ITerrainTileEffectObserver>> observers) {
    foreach (var effect in effectsTerrainTileDeleteEffect) {
      if (observers.TryGetValue(0, out List<ITerrainTileEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTerrainTileEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITerrainTileEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTerrainTileEffect(effect);
        }
        observersForTerrainTile.Remove(effect.id);
      }
    }
    effectsTerrainTileDeleteEffect.Clear();


    foreach (var effect in effectsTerrainTileSetElevationEffect) {
      if (observers.TryGetValue(0, out List<ITerrainTileEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTerrainTileEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITerrainTileEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTerrainTileEffect(effect);
        }
      }
    }
    effectsTerrainTileSetElevationEffect.Clear();

    foreach (var effect in effectsTerrainTileCreateEffect) {
      if (observers.TryGetValue(0, out List<ITerrainTileEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTerrainTileEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITerrainTileEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTerrainTileEffect(effect);
        }
      }
    }
    effectsTerrainTileCreateEffect.Clear();
  }

  public void EffectTerrainTileSetElevation(int id, int newValue) {
    CheckUnlocked();
    CheckHasTerrainTile(id);
    var effect = new TerrainTileSetElevationEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsTerrainTile[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.elevation;
      oldIncarnationAndVersion.incarnation.elevation = newValue;

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
    }

    effectsTerrainTileSetElevationEffect.Add(effect);
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
      throw new System.Exception("Invalid ITerrainTileComponentMutBunch: " + id);
    }
  }
  public void AddITerrainTileComponentMutBunchObserver(int id, IITerrainTileComponentMutBunchEffectObserver observer) {
    List<IITerrainTileComponentMutBunchEffectObserver> obsies;
    if (!observersForITerrainTileComponentMutBunch.TryGetValue(id, out obsies)) {
      obsies = new List<IITerrainTileComponentMutBunchEffectObserver>();
    }
    obsies.Add(observer);
    observersForITerrainTileComponentMutBunch[id] = obsies;
  }

  public void RemoveITerrainTileComponentMutBunchObserver(int id, IITerrainTileComponentMutBunchEffectObserver observer) {
    if (observersForITerrainTileComponentMutBunch.ContainsKey(id)) {
      var list = observersForITerrainTileComponentMutBunch[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForITerrainTileComponentMutBunch.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public ITerrainTileComponentMutBunch EffectITerrainTileComponentMutBunchCreate(
      ItemTerrainTileComponentMutSet membersItemTerrainTileComponentMutSet,
      DecorativeTerrainTileComponentMutSet membersDecorativeTerrainTileComponentMutSet,
      UpStaircaseTerrainTileComponentMutSet membersUpStaircaseTerrainTileComponentMutSet,
      DownStaircaseTerrainTileComponentMutSet membersDownStaircaseTerrainTileComponentMutSet) {
    CheckUnlocked();
    CheckHasItemTerrainTileComponentMutSet(membersItemTerrainTileComponentMutSet);
    CheckHasDecorativeTerrainTileComponentMutSet(membersDecorativeTerrainTileComponentMutSet);
    CheckHasUpStaircaseTerrainTileComponentMutSet(membersUpStaircaseTerrainTileComponentMutSet);
    CheckHasDownStaircaseTerrainTileComponentMutSet(membersDownStaircaseTerrainTileComponentMutSet);

    var id = NewId();
    var incarnation =
        new ITerrainTileComponentMutBunchIncarnation(
            membersItemTerrainTileComponentMutSet.id,
            membersDecorativeTerrainTileComponentMutSet.id,
            membersUpStaircaseTerrainTileComponentMutSet.id,
            membersDownStaircaseTerrainTileComponentMutSet.id
            );
    EffectInternalCreateITerrainTileComponentMutBunch(id, rootIncarnation.version, incarnation);
    return new ITerrainTileComponentMutBunch(this, id);
  }
  public void EffectInternalCreateITerrainTileComponentMutBunch(
      int id,
      int incarnationVersion,
      ITerrainTileComponentMutBunchIncarnation incarnation) {
    CheckUnlocked();
    var effect = new ITerrainTileComponentMutBunchCreateEffect(id);
    rootIncarnation.incarnationsITerrainTileComponentMutBunch.Add(
        id,
        new VersionAndIncarnation<ITerrainTileComponentMutBunchIncarnation>(
            incarnationVersion,
            incarnation));
    effectsITerrainTileComponentMutBunchCreateEffect.Add(effect);
  }

  public void EffectITerrainTileComponentMutBunchDelete(int id) {
    CheckUnlocked();
    var effect = new ITerrainTileComponentMutBunchDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsITerrainTileComponentMutBunch[id];

    rootIncarnation.incarnationsITerrainTileComponentMutBunch.Remove(id);
    effectsITerrainTileComponentMutBunchDeleteEffect.Add(effect);
  }

     
  public int GetITerrainTileComponentMutBunchHash(int id, int version, ITerrainTileComponentMutBunchIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.membersItemTerrainTileComponentMutSet.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.membersDecorativeTerrainTileComponentMutSet.GetDeterministicHashCode();
    result += id * version * 3 * incarnation.membersUpStaircaseTerrainTileComponentMutSet.GetDeterministicHashCode();
    result += id * version * 4 * incarnation.membersDownStaircaseTerrainTileComponentMutSet.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastITerrainTileComponentMutBunchEffects(
      SortedDictionary<int, List<IITerrainTileComponentMutBunchEffectObserver>> observers) {
    foreach (var effect in effectsITerrainTileComponentMutBunchDeleteEffect) {
      if (observers.TryGetValue(0, out List<IITerrainTileComponentMutBunchEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnITerrainTileComponentMutBunchEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IITerrainTileComponentMutBunchEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnITerrainTileComponentMutBunchEffect(effect);
        }
        observersForITerrainTileComponentMutBunch.Remove(effect.id);
      }
    }
    effectsITerrainTileComponentMutBunchDeleteEffect.Clear();


    foreach (var effect in effectsITerrainTileComponentMutBunchCreateEffect) {
      if (observers.TryGetValue(0, out List<IITerrainTileComponentMutBunchEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnITerrainTileComponentMutBunchEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IITerrainTileComponentMutBunchEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnITerrainTileComponentMutBunchEffect(effect);
        }
      }
    }
    effectsITerrainTileComponentMutBunchCreateEffect.Clear();
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
      throw new System.Exception("Invalid Terrain: " + id);
    }
  }
  public void AddTerrainObserver(int id, ITerrainEffectObserver observer) {
    List<ITerrainEffectObserver> obsies;
    if (!observersForTerrain.TryGetValue(id, out obsies)) {
      obsies = new List<ITerrainEffectObserver>();
    }
    obsies.Add(observer);
    observersForTerrain[id] = obsies;
  }

  public void RemoveTerrainObserver(int id, ITerrainEffectObserver observer) {
    if (observersForTerrain.ContainsKey(id)) {
      var list = observersForTerrain[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForTerrain.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
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
    EffectInternalCreateTerrain(id, rootIncarnation.version, incarnation);
    return new Terrain(this, id);
  }
  public void EffectInternalCreateTerrain(
      int id,
      int incarnationVersion,
      TerrainIncarnation incarnation) {
    CheckUnlocked();
    var effect = new TerrainCreateEffect(id);
    rootIncarnation.incarnationsTerrain.Add(
        id,
        new VersionAndIncarnation<TerrainIncarnation>(
            incarnationVersion,
            incarnation));
    effectsTerrainCreateEffect.Add(effect);
  }

  public void EffectTerrainDelete(int id) {
    CheckUnlocked();
    var effect = new TerrainDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsTerrain[id];

    rootIncarnation.incarnationsTerrain.Remove(id);
    effectsTerrainDeleteEffect.Add(effect);
  }

     
  public int GetTerrainHash(int id, int version, TerrainIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.pattern.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.elevationStepHeight.GetDeterministicHashCode();
    result += id * version * 3 * incarnation.tiles.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastTerrainEffects(
      SortedDictionary<int, List<ITerrainEffectObserver>> observers) {
    foreach (var effect in effectsTerrainDeleteEffect) {
      if (observers.TryGetValue(0, out List<ITerrainEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTerrainEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITerrainEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTerrainEffect(effect);
        }
        observersForTerrain.Remove(effect.id);
      }
    }
    effectsTerrainDeleteEffect.Clear();


    foreach (var effect in effectsTerrainSetPatternEffect) {
      if (observers.TryGetValue(0, out List<ITerrainEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTerrainEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITerrainEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTerrainEffect(effect);
        }
      }
    }
    effectsTerrainSetPatternEffect.Clear();

    foreach (var effect in effectsTerrainCreateEffect) {
      if (observers.TryGetValue(0, out List<ITerrainEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTerrainEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITerrainEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTerrainEffect(effect);
        }
      }
    }
    effectsTerrainCreateEffect.Clear();
  }

  public void EffectTerrainSetPattern(int id, Pattern newValue) {
    CheckUnlocked();
    CheckHasTerrain(id);
    var effect = new TerrainSetPatternEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsTerrain[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.pattern;
      oldIncarnationAndVersion.incarnation.pattern = newValue;

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
    }

    effectsTerrainSetPatternEffect.Add(effect);
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
      throw new System.Exception("Invalid Glaive: " + id);
    }
  }
  public void AddGlaiveObserver(int id, IGlaiveEffectObserver observer) {
    List<IGlaiveEffectObserver> obsies;
    if (!observersForGlaive.TryGetValue(id, out obsies)) {
      obsies = new List<IGlaiveEffectObserver>();
    }
    obsies.Add(observer);
    observersForGlaive[id] = obsies;
  }

  public void RemoveGlaiveObserver(int id, IGlaiveEffectObserver observer) {
    if (observersForGlaive.ContainsKey(id)) {
      var list = observersForGlaive[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForGlaive.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public Glaive EffectGlaiveCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new GlaiveIncarnation(

            );
    EffectInternalCreateGlaive(id, rootIncarnation.version, incarnation);
    return new Glaive(this, id);
  }
  public void EffectInternalCreateGlaive(
      int id,
      int incarnationVersion,
      GlaiveIncarnation incarnation) {
    CheckUnlocked();
    var effect = new GlaiveCreateEffect(id);
    rootIncarnation.incarnationsGlaive.Add(
        id,
        new VersionAndIncarnation<GlaiveIncarnation>(
            incarnationVersion,
            incarnation));
    effectsGlaiveCreateEffect.Add(effect);
  }

  public void EffectGlaiveDelete(int id) {
    CheckUnlocked();
    var effect = new GlaiveDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsGlaive[id];

    rootIncarnation.incarnationsGlaive.Remove(id);
    effectsGlaiveDeleteEffect.Add(effect);
  }

     
  public int GetGlaiveHash(int id, int version, GlaiveIncarnation incarnation) {
    int result = id * version;
    return result;
  }
     
  public void BroadcastGlaiveEffects(
      SortedDictionary<int, List<IGlaiveEffectObserver>> observers) {
    foreach (var effect in effectsGlaiveDeleteEffect) {
      if (observers.TryGetValue(0, out List<IGlaiveEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnGlaiveEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IGlaiveEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnGlaiveEffect(effect);
        }
        observersForGlaive.Remove(effect.id);
      }
    }
    effectsGlaiveDeleteEffect.Clear();


    foreach (var effect in effectsGlaiveCreateEffect) {
      if (observers.TryGetValue(0, out List<IGlaiveEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnGlaiveEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IGlaiveEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnGlaiveEffect(effect);
        }
      }
    }
    effectsGlaiveCreateEffect.Clear();
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
      throw new System.Exception("Invalid Armor: " + id);
    }
  }
  public void AddArmorObserver(int id, IArmorEffectObserver observer) {
    List<IArmorEffectObserver> obsies;
    if (!observersForArmor.TryGetValue(id, out obsies)) {
      obsies = new List<IArmorEffectObserver>();
    }
    obsies.Add(observer);
    observersForArmor[id] = obsies;
  }

  public void RemoveArmorObserver(int id, IArmorEffectObserver observer) {
    if (observersForArmor.ContainsKey(id)) {
      var list = observersForArmor[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForArmor.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public Armor EffectArmorCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new ArmorIncarnation(

            );
    EffectInternalCreateArmor(id, rootIncarnation.version, incarnation);
    return new Armor(this, id);
  }
  public void EffectInternalCreateArmor(
      int id,
      int incarnationVersion,
      ArmorIncarnation incarnation) {
    CheckUnlocked();
    var effect = new ArmorCreateEffect(id);
    rootIncarnation.incarnationsArmor.Add(
        id,
        new VersionAndIncarnation<ArmorIncarnation>(
            incarnationVersion,
            incarnation));
    effectsArmorCreateEffect.Add(effect);
  }

  public void EffectArmorDelete(int id) {
    CheckUnlocked();
    var effect = new ArmorDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsArmor[id];

    rootIncarnation.incarnationsArmor.Remove(id);
    effectsArmorDeleteEffect.Add(effect);
  }

     
  public int GetArmorHash(int id, int version, ArmorIncarnation incarnation) {
    int result = id * version;
    return result;
  }
     
  public void BroadcastArmorEffects(
      SortedDictionary<int, List<IArmorEffectObserver>> observers) {
    foreach (var effect in effectsArmorDeleteEffect) {
      if (observers.TryGetValue(0, out List<IArmorEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnArmorEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IArmorEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnArmorEffect(effect);
        }
        observersForArmor.Remove(effect.id);
      }
    }
    effectsArmorDeleteEffect.Clear();


    foreach (var effect in effectsArmorCreateEffect) {
      if (observers.TryGetValue(0, out List<IArmorEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnArmorEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IArmorEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnArmorEffect(effect);
        }
      }
    }
    effectsArmorCreateEffect.Clear();
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
      throw new System.Exception("Invalid Rand: " + id);
    }
  }
  public void AddRandObserver(int id, IRandEffectObserver observer) {
    List<IRandEffectObserver> obsies;
    if (!observersForRand.TryGetValue(id, out obsies)) {
      obsies = new List<IRandEffectObserver>();
    }
    obsies.Add(observer);
    observersForRand[id] = obsies;
  }

  public void RemoveRandObserver(int id, IRandEffectObserver observer) {
    if (observersForRand.ContainsKey(id)) {
      var list = observersForRand[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForRand.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
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
    EffectInternalCreateRand(id, rootIncarnation.version, incarnation);
    return new Rand(this, id);
  }
  public void EffectInternalCreateRand(
      int id,
      int incarnationVersion,
      RandIncarnation incarnation) {
    CheckUnlocked();
    var effect = new RandCreateEffect(id);
    rootIncarnation.incarnationsRand.Add(
        id,
        new VersionAndIncarnation<RandIncarnation>(
            incarnationVersion,
            incarnation));
    effectsRandCreateEffect.Add(effect);
  }

  public void EffectRandDelete(int id) {
    CheckUnlocked();
    var effect = new RandDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsRand[id];

    rootIncarnation.incarnationsRand.Remove(id);
    effectsRandDeleteEffect.Add(effect);
  }

     
  public int GetRandHash(int id, int version, RandIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.rand.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastRandEffects(
      SortedDictionary<int, List<IRandEffectObserver>> observers) {
    foreach (var effect in effectsRandDeleteEffect) {
      if (observers.TryGetValue(0, out List<IRandEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnRandEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IRandEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnRandEffect(effect);
        }
        observersForRand.Remove(effect.id);
      }
    }
    effectsRandDeleteEffect.Clear();


    foreach (var effect in effectsRandSetRandEffect) {
      if (observers.TryGetValue(0, out List<IRandEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnRandEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IRandEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnRandEffect(effect);
        }
      }
    }
    effectsRandSetRandEffect.Clear();

    foreach (var effect in effectsRandCreateEffect) {
      if (observers.TryGetValue(0, out List<IRandEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnRandEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IRandEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnRandEffect(effect);
        }
      }
    }
    effectsRandCreateEffect.Clear();
  }

  public void EffectRandSetRand(int id, int newValue) {
    CheckUnlocked();
    CheckHasRand(id);
    var effect = new RandSetRandEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsRand[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.rand;
      oldIncarnationAndVersion.incarnation.rand = newValue;

    } else {
      var newIncarnation =
          new RandIncarnation(
              newValue);
      rootIncarnation.incarnationsRand[id] =
          new VersionAndIncarnation<RandIncarnation>(
              rootIncarnation.version,
              newIncarnation);
    }

    effectsRandSetRandEffect.Add(effect);
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
      throw new System.Exception("Invalid WanderAICapabilityUC: " + id);
    }
  }
  public void AddWanderAICapabilityUCObserver(int id, IWanderAICapabilityUCEffectObserver observer) {
    List<IWanderAICapabilityUCEffectObserver> obsies;
    if (!observersForWanderAICapabilityUC.TryGetValue(id, out obsies)) {
      obsies = new List<IWanderAICapabilityUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForWanderAICapabilityUC[id] = obsies;
  }

  public void RemoveWanderAICapabilityUCObserver(int id, IWanderAICapabilityUCEffectObserver observer) {
    if (observersForWanderAICapabilityUC.ContainsKey(id)) {
      var list = observersForWanderAICapabilityUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForWanderAICapabilityUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public WanderAICapabilityUC EffectWanderAICapabilityUCCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new WanderAICapabilityUCIncarnation(

            );
    EffectInternalCreateWanderAICapabilityUC(id, rootIncarnation.version, incarnation);
    return new WanderAICapabilityUC(this, id);
  }
  public void EffectInternalCreateWanderAICapabilityUC(
      int id,
      int incarnationVersion,
      WanderAICapabilityUCIncarnation incarnation) {
    CheckUnlocked();
    var effect = new WanderAICapabilityUCCreateEffect(id);
    rootIncarnation.incarnationsWanderAICapabilityUC.Add(
        id,
        new VersionAndIncarnation<WanderAICapabilityUCIncarnation>(
            incarnationVersion,
            incarnation));
    effectsWanderAICapabilityUCCreateEffect.Add(effect);
  }

  public void EffectWanderAICapabilityUCDelete(int id) {
    CheckUnlocked();
    var effect = new WanderAICapabilityUCDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsWanderAICapabilityUC[id];

    rootIncarnation.incarnationsWanderAICapabilityUC.Remove(id);
    effectsWanderAICapabilityUCDeleteEffect.Add(effect);
  }

     
  public int GetWanderAICapabilityUCHash(int id, int version, WanderAICapabilityUCIncarnation incarnation) {
    int result = id * version;
    return result;
  }
     
  public void BroadcastWanderAICapabilityUCEffects(
      SortedDictionary<int, List<IWanderAICapabilityUCEffectObserver>> observers) {
    foreach (var effect in effectsWanderAICapabilityUCDeleteEffect) {
      if (observers.TryGetValue(0, out List<IWanderAICapabilityUCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnWanderAICapabilityUCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IWanderAICapabilityUCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnWanderAICapabilityUCEffect(effect);
        }
        observersForWanderAICapabilityUC.Remove(effect.id);
      }
    }
    effectsWanderAICapabilityUCDeleteEffect.Clear();


    foreach (var effect in effectsWanderAICapabilityUCCreateEffect) {
      if (observers.TryGetValue(0, out List<IWanderAICapabilityUCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnWanderAICapabilityUCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IWanderAICapabilityUCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnWanderAICapabilityUCEffect(effect);
        }
      }
    }
    effectsWanderAICapabilityUCCreateEffect.Clear();
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
      throw new System.Exception("Invalid ShieldingUC: " + id);
    }
  }
  public void AddShieldingUCObserver(int id, IShieldingUCEffectObserver observer) {
    List<IShieldingUCEffectObserver> obsies;
    if (!observersForShieldingUC.TryGetValue(id, out obsies)) {
      obsies = new List<IShieldingUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForShieldingUC[id] = obsies;
  }

  public void RemoveShieldingUCObserver(int id, IShieldingUCEffectObserver observer) {
    if (observersForShieldingUC.ContainsKey(id)) {
      var list = observersForShieldingUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForShieldingUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public ShieldingUC EffectShieldingUCCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new ShieldingUCIncarnation(

            );
    EffectInternalCreateShieldingUC(id, rootIncarnation.version, incarnation);
    return new ShieldingUC(this, id);
  }
  public void EffectInternalCreateShieldingUC(
      int id,
      int incarnationVersion,
      ShieldingUCIncarnation incarnation) {
    CheckUnlocked();
    var effect = new ShieldingUCCreateEffect(id);
    rootIncarnation.incarnationsShieldingUC.Add(
        id,
        new VersionAndIncarnation<ShieldingUCIncarnation>(
            incarnationVersion,
            incarnation));
    effectsShieldingUCCreateEffect.Add(effect);
  }

  public void EffectShieldingUCDelete(int id) {
    CheckUnlocked();
    var effect = new ShieldingUCDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsShieldingUC[id];

    rootIncarnation.incarnationsShieldingUC.Remove(id);
    effectsShieldingUCDeleteEffect.Add(effect);
  }

     
  public int GetShieldingUCHash(int id, int version, ShieldingUCIncarnation incarnation) {
    int result = id * version;
    return result;
  }
     
  public void BroadcastShieldingUCEffects(
      SortedDictionary<int, List<IShieldingUCEffectObserver>> observers) {
    foreach (var effect in effectsShieldingUCDeleteEffect) {
      if (observers.TryGetValue(0, out List<IShieldingUCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnShieldingUCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IShieldingUCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnShieldingUCEffect(effect);
        }
        observersForShieldingUC.Remove(effect.id);
      }
    }
    effectsShieldingUCDeleteEffect.Clear();


    foreach (var effect in effectsShieldingUCCreateEffect) {
      if (observers.TryGetValue(0, out List<IShieldingUCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnShieldingUCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IShieldingUCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnShieldingUCEffect(effect);
        }
      }
    }
    effectsShieldingUCCreateEffect.Clear();
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
      throw new System.Exception("Invalid BidingOperationUC: " + id);
    }
  }
  public void AddBidingOperationUCObserver(int id, IBidingOperationUCEffectObserver observer) {
    List<IBidingOperationUCEffectObserver> obsies;
    if (!observersForBidingOperationUC.TryGetValue(id, out obsies)) {
      obsies = new List<IBidingOperationUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForBidingOperationUC[id] = obsies;
  }

  public void RemoveBidingOperationUCObserver(int id, IBidingOperationUCEffectObserver observer) {
    if (observersForBidingOperationUC.ContainsKey(id)) {
      var list = observersForBidingOperationUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForBidingOperationUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public BidingOperationUC EffectBidingOperationUCCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new BidingOperationUCIncarnation(

            );
    EffectInternalCreateBidingOperationUC(id, rootIncarnation.version, incarnation);
    return new BidingOperationUC(this, id);
  }
  public void EffectInternalCreateBidingOperationUC(
      int id,
      int incarnationVersion,
      BidingOperationUCIncarnation incarnation) {
    CheckUnlocked();
    var effect = new BidingOperationUCCreateEffect(id);
    rootIncarnation.incarnationsBidingOperationUC.Add(
        id,
        new VersionAndIncarnation<BidingOperationUCIncarnation>(
            incarnationVersion,
            incarnation));
    effectsBidingOperationUCCreateEffect.Add(effect);
  }

  public void EffectBidingOperationUCDelete(int id) {
    CheckUnlocked();
    var effect = new BidingOperationUCDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsBidingOperationUC[id];

    rootIncarnation.incarnationsBidingOperationUC.Remove(id);
    effectsBidingOperationUCDeleteEffect.Add(effect);
  }

     
  public int GetBidingOperationUCHash(int id, int version, BidingOperationUCIncarnation incarnation) {
    int result = id * version;
    return result;
  }
     
  public void BroadcastBidingOperationUCEffects(
      SortedDictionary<int, List<IBidingOperationUCEffectObserver>> observers) {
    foreach (var effect in effectsBidingOperationUCDeleteEffect) {
      if (observers.TryGetValue(0, out List<IBidingOperationUCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnBidingOperationUCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IBidingOperationUCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnBidingOperationUCEffect(effect);
        }
        observersForBidingOperationUC.Remove(effect.id);
      }
    }
    effectsBidingOperationUCDeleteEffect.Clear();


    foreach (var effect in effectsBidingOperationUCCreateEffect) {
      if (observers.TryGetValue(0, out List<IBidingOperationUCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnBidingOperationUCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IBidingOperationUCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnBidingOperationUCEffect(effect);
        }
      }
    }
    effectsBidingOperationUCCreateEffect.Clear();
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
      throw new System.Exception("Invalid UnleashBideImpulse: " + id);
    }
  }
  public void AddUnleashBideImpulseObserver(int id, IUnleashBideImpulseEffectObserver observer) {
    List<IUnleashBideImpulseEffectObserver> obsies;
    if (!observersForUnleashBideImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<IUnleashBideImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersForUnleashBideImpulse[id] = obsies;
  }

  public void RemoveUnleashBideImpulseObserver(int id, IUnleashBideImpulseEffectObserver observer) {
    if (observersForUnleashBideImpulse.ContainsKey(id)) {
      var list = observersForUnleashBideImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForUnleashBideImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
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
    EffectInternalCreateUnleashBideImpulse(id, rootIncarnation.version, incarnation);
    return new UnleashBideImpulse(this, id);
  }
  public void EffectInternalCreateUnleashBideImpulse(
      int id,
      int incarnationVersion,
      UnleashBideImpulseIncarnation incarnation) {
    CheckUnlocked();
    var effect = new UnleashBideImpulseCreateEffect(id);
    rootIncarnation.incarnationsUnleashBideImpulse.Add(
        id,
        new VersionAndIncarnation<UnleashBideImpulseIncarnation>(
            incarnationVersion,
            incarnation));
    effectsUnleashBideImpulseCreateEffect.Add(effect);
  }

  public void EffectUnleashBideImpulseDelete(int id) {
    CheckUnlocked();
    var effect = new UnleashBideImpulseDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsUnleashBideImpulse[id];

    rootIncarnation.incarnationsUnleashBideImpulse.Remove(id);
    effectsUnleashBideImpulseDeleteEffect.Add(effect);
  }

     
  public int GetUnleashBideImpulseHash(int id, int version, UnleashBideImpulseIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.weight.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastUnleashBideImpulseEffects(
      SortedDictionary<int, List<IUnleashBideImpulseEffectObserver>> observers) {
    foreach (var effect in effectsUnleashBideImpulseDeleteEffect) {
      if (observers.TryGetValue(0, out List<IUnleashBideImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnUnleashBideImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IUnleashBideImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnUnleashBideImpulseEffect(effect);
        }
        observersForUnleashBideImpulse.Remove(effect.id);
      }
    }
    effectsUnleashBideImpulseDeleteEffect.Clear();


    foreach (var effect in effectsUnleashBideImpulseCreateEffect) {
      if (observers.TryGetValue(0, out List<IUnleashBideImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnUnleashBideImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IUnleashBideImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnUnleashBideImpulseEffect(effect);
        }
      }
    }
    effectsUnleashBideImpulseCreateEffect.Clear();
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
      throw new System.Exception("Invalid StartBidingImpulse: " + id);
    }
  }
  public void AddStartBidingImpulseObserver(int id, IStartBidingImpulseEffectObserver observer) {
    List<IStartBidingImpulseEffectObserver> obsies;
    if (!observersForStartBidingImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<IStartBidingImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersForStartBidingImpulse[id] = obsies;
  }

  public void RemoveStartBidingImpulseObserver(int id, IStartBidingImpulseEffectObserver observer) {
    if (observersForStartBidingImpulse.ContainsKey(id)) {
      var list = observersForStartBidingImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForStartBidingImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
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
    EffectInternalCreateStartBidingImpulse(id, rootIncarnation.version, incarnation);
    return new StartBidingImpulse(this, id);
  }
  public void EffectInternalCreateStartBidingImpulse(
      int id,
      int incarnationVersion,
      StartBidingImpulseIncarnation incarnation) {
    CheckUnlocked();
    var effect = new StartBidingImpulseCreateEffect(id);
    rootIncarnation.incarnationsStartBidingImpulse.Add(
        id,
        new VersionAndIncarnation<StartBidingImpulseIncarnation>(
            incarnationVersion,
            incarnation));
    effectsStartBidingImpulseCreateEffect.Add(effect);
  }

  public void EffectStartBidingImpulseDelete(int id) {
    CheckUnlocked();
    var effect = new StartBidingImpulseDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsStartBidingImpulse[id];

    rootIncarnation.incarnationsStartBidingImpulse.Remove(id);
    effectsStartBidingImpulseDeleteEffect.Add(effect);
  }

     
  public int GetStartBidingImpulseHash(int id, int version, StartBidingImpulseIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.weight.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastStartBidingImpulseEffects(
      SortedDictionary<int, List<IStartBidingImpulseEffectObserver>> observers) {
    foreach (var effect in effectsStartBidingImpulseDeleteEffect) {
      if (observers.TryGetValue(0, out List<IStartBidingImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnStartBidingImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IStartBidingImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnStartBidingImpulseEffect(effect);
        }
        observersForStartBidingImpulse.Remove(effect.id);
      }
    }
    effectsStartBidingImpulseDeleteEffect.Clear();


    foreach (var effect in effectsStartBidingImpulseCreateEffect) {
      if (observers.TryGetValue(0, out List<IStartBidingImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnStartBidingImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IStartBidingImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnStartBidingImpulseEffect(effect);
        }
      }
    }
    effectsStartBidingImpulseCreateEffect.Clear();
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
      throw new System.Exception("Invalid BideAICapabilityUC: " + id);
    }
  }
  public void AddBideAICapabilityUCObserver(int id, IBideAICapabilityUCEffectObserver observer) {
    List<IBideAICapabilityUCEffectObserver> obsies;
    if (!observersForBideAICapabilityUC.TryGetValue(id, out obsies)) {
      obsies = new List<IBideAICapabilityUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForBideAICapabilityUC[id] = obsies;
  }

  public void RemoveBideAICapabilityUCObserver(int id, IBideAICapabilityUCEffectObserver observer) {
    if (observersForBideAICapabilityUC.ContainsKey(id)) {
      var list = observersForBideAICapabilityUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForBideAICapabilityUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public BideAICapabilityUC EffectBideAICapabilityUCCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new BideAICapabilityUCIncarnation(

            );
    EffectInternalCreateBideAICapabilityUC(id, rootIncarnation.version, incarnation);
    return new BideAICapabilityUC(this, id);
  }
  public void EffectInternalCreateBideAICapabilityUC(
      int id,
      int incarnationVersion,
      BideAICapabilityUCIncarnation incarnation) {
    CheckUnlocked();
    var effect = new BideAICapabilityUCCreateEffect(id);
    rootIncarnation.incarnationsBideAICapabilityUC.Add(
        id,
        new VersionAndIncarnation<BideAICapabilityUCIncarnation>(
            incarnationVersion,
            incarnation));
    effectsBideAICapabilityUCCreateEffect.Add(effect);
  }

  public void EffectBideAICapabilityUCDelete(int id) {
    CheckUnlocked();
    var effect = new BideAICapabilityUCDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsBideAICapabilityUC[id];

    rootIncarnation.incarnationsBideAICapabilityUC.Remove(id);
    effectsBideAICapabilityUCDeleteEffect.Add(effect);
  }

     
  public int GetBideAICapabilityUCHash(int id, int version, BideAICapabilityUCIncarnation incarnation) {
    int result = id * version;
    return result;
  }
     
  public void BroadcastBideAICapabilityUCEffects(
      SortedDictionary<int, List<IBideAICapabilityUCEffectObserver>> observers) {
    foreach (var effect in effectsBideAICapabilityUCDeleteEffect) {
      if (observers.TryGetValue(0, out List<IBideAICapabilityUCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnBideAICapabilityUCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IBideAICapabilityUCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnBideAICapabilityUCEffect(effect);
        }
        observersForBideAICapabilityUC.Remove(effect.id);
      }
    }
    effectsBideAICapabilityUCDeleteEffect.Clear();


    foreach (var effect in effectsBideAICapabilityUCCreateEffect) {
      if (observers.TryGetValue(0, out List<IBideAICapabilityUCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnBideAICapabilityUCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IBideAICapabilityUCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnBideAICapabilityUCEffect(effect);
        }
      }
    }
    effectsBideAICapabilityUCCreateEffect.Clear();
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
      throw new System.Exception("Invalid AttackImpulse: " + id);
    }
  }
  public void AddAttackImpulseObserver(int id, IAttackImpulseEffectObserver observer) {
    List<IAttackImpulseEffectObserver> obsies;
    if (!observersForAttackImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<IAttackImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersForAttackImpulse[id] = obsies;
  }

  public void RemoveAttackImpulseObserver(int id, IAttackImpulseEffectObserver observer) {
    if (observersForAttackImpulse.ContainsKey(id)) {
      var list = observersForAttackImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForAttackImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
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
    EffectInternalCreateAttackImpulse(id, rootIncarnation.version, incarnation);
    return new AttackImpulse(this, id);
  }
  public void EffectInternalCreateAttackImpulse(
      int id,
      int incarnationVersion,
      AttackImpulseIncarnation incarnation) {
    CheckUnlocked();
    var effect = new AttackImpulseCreateEffect(id);
    rootIncarnation.incarnationsAttackImpulse.Add(
        id,
        new VersionAndIncarnation<AttackImpulseIncarnation>(
            incarnationVersion,
            incarnation));
    effectsAttackImpulseCreateEffect.Add(effect);
  }

  public void EffectAttackImpulseDelete(int id) {
    CheckUnlocked();
    var effect = new AttackImpulseDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsAttackImpulse[id];

    rootIncarnation.incarnationsAttackImpulse.Remove(id);
    effectsAttackImpulseDeleteEffect.Add(effect);
  }

     
  public int GetAttackImpulseHash(int id, int version, AttackImpulseIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.weight.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastAttackImpulseEffects(
      SortedDictionary<int, List<IAttackImpulseEffectObserver>> observers) {
    foreach (var effect in effectsAttackImpulseDeleteEffect) {
      if (observers.TryGetValue(0, out List<IAttackImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnAttackImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IAttackImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnAttackImpulseEffect(effect);
        }
        observersForAttackImpulse.Remove(effect.id);
      }
    }
    effectsAttackImpulseDeleteEffect.Clear();


    foreach (var effect in effectsAttackImpulseCreateEffect) {
      if (observers.TryGetValue(0, out List<IAttackImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnAttackImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IAttackImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnAttackImpulseEffect(effect);
        }
      }
    }
    effectsAttackImpulseCreateEffect.Clear();
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
      throw new System.Exception("Invalid PursueImpulse: " + id);
    }
  }
  public void AddPursueImpulseObserver(int id, IPursueImpulseEffectObserver observer) {
    List<IPursueImpulseEffectObserver> obsies;
    if (!observersForPursueImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<IPursueImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersForPursueImpulse[id] = obsies;
  }

  public void RemovePursueImpulseObserver(int id, IPursueImpulseEffectObserver observer) {
    if (observersForPursueImpulse.ContainsKey(id)) {
      var list = observersForPursueImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForPursueImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public PursueImpulse EffectPursueImpulseCreate(
      int weight) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new PursueImpulseIncarnation(
            weight
            );
    EffectInternalCreatePursueImpulse(id, rootIncarnation.version, incarnation);
    return new PursueImpulse(this, id);
  }
  public void EffectInternalCreatePursueImpulse(
      int id,
      int incarnationVersion,
      PursueImpulseIncarnation incarnation) {
    CheckUnlocked();
    var effect = new PursueImpulseCreateEffect(id);
    rootIncarnation.incarnationsPursueImpulse.Add(
        id,
        new VersionAndIncarnation<PursueImpulseIncarnation>(
            incarnationVersion,
            incarnation));
    effectsPursueImpulseCreateEffect.Add(effect);
  }

  public void EffectPursueImpulseDelete(int id) {
    CheckUnlocked();
    var effect = new PursueImpulseDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsPursueImpulse[id];

    rootIncarnation.incarnationsPursueImpulse.Remove(id);
    effectsPursueImpulseDeleteEffect.Add(effect);
  }

     
  public int GetPursueImpulseHash(int id, int version, PursueImpulseIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.weight.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastPursueImpulseEffects(
      SortedDictionary<int, List<IPursueImpulseEffectObserver>> observers) {
    foreach (var effect in effectsPursueImpulseDeleteEffect) {
      if (observers.TryGetValue(0, out List<IPursueImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnPursueImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IPursueImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnPursueImpulseEffect(effect);
        }
        observersForPursueImpulse.Remove(effect.id);
      }
    }
    effectsPursueImpulseDeleteEffect.Clear();


    foreach (var effect in effectsPursueImpulseCreateEffect) {
      if (observers.TryGetValue(0, out List<IPursueImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnPursueImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IPursueImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnPursueImpulseEffect(effect);
        }
      }
    }
    effectsPursueImpulseCreateEffect.Clear();
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
      throw new System.Exception("Invalid KillDirectiveUC: " + id);
    }
  }
  public void AddKillDirectiveUCObserver(int id, IKillDirectiveUCEffectObserver observer) {
    List<IKillDirectiveUCEffectObserver> obsies;
    if (!observersForKillDirectiveUC.TryGetValue(id, out obsies)) {
      obsies = new List<IKillDirectiveUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForKillDirectiveUC[id] = obsies;
  }

  public void RemoveKillDirectiveUCObserver(int id, IKillDirectiveUCEffectObserver observer) {
    if (observersForKillDirectiveUC.ContainsKey(id)) {
      var list = observersForKillDirectiveUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForKillDirectiveUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
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
    EffectInternalCreateKillDirectiveUC(id, rootIncarnation.version, incarnation);
    return new KillDirectiveUC(this, id);
  }
  public void EffectInternalCreateKillDirectiveUC(
      int id,
      int incarnationVersion,
      KillDirectiveUCIncarnation incarnation) {
    CheckUnlocked();
    var effect = new KillDirectiveUCCreateEffect(id);
    rootIncarnation.incarnationsKillDirectiveUC.Add(
        id,
        new VersionAndIncarnation<KillDirectiveUCIncarnation>(
            incarnationVersion,
            incarnation));
    effectsKillDirectiveUCCreateEffect.Add(effect);
  }

  public void EffectKillDirectiveUCDelete(int id) {
    CheckUnlocked();
    var effect = new KillDirectiveUCDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsKillDirectiveUC[id];

    rootIncarnation.incarnationsKillDirectiveUC.Remove(id);
    effectsKillDirectiveUCDeleteEffect.Add(effect);
  }

     
  public int GetKillDirectiveUCHash(int id, int version, KillDirectiveUCIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.targetUnit.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.pathToLastSeenLocation.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastKillDirectiveUCEffects(
      SortedDictionary<int, List<IKillDirectiveUCEffectObserver>> observers) {
    foreach (var effect in effectsKillDirectiveUCDeleteEffect) {
      if (observers.TryGetValue(0, out List<IKillDirectiveUCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnKillDirectiveUCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IKillDirectiveUCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnKillDirectiveUCEffect(effect);
        }
        observersForKillDirectiveUC.Remove(effect.id);
      }
    }
    effectsKillDirectiveUCDeleteEffect.Clear();


    foreach (var effect in effectsKillDirectiveUCCreateEffect) {
      if (observers.TryGetValue(0, out List<IKillDirectiveUCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnKillDirectiveUCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IKillDirectiveUCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnKillDirectiveUCEffect(effect);
        }
      }
    }
    effectsKillDirectiveUCCreateEffect.Clear();
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
      throw new System.Exception("Invalid AttackAICapabilityUC: " + id);
    }
  }
  public void AddAttackAICapabilityUCObserver(int id, IAttackAICapabilityUCEffectObserver observer) {
    List<IAttackAICapabilityUCEffectObserver> obsies;
    if (!observersForAttackAICapabilityUC.TryGetValue(id, out obsies)) {
      obsies = new List<IAttackAICapabilityUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForAttackAICapabilityUC[id] = obsies;
  }

  public void RemoveAttackAICapabilityUCObserver(int id, IAttackAICapabilityUCEffectObserver observer) {
    if (observersForAttackAICapabilityUC.ContainsKey(id)) {
      var list = observersForAttackAICapabilityUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForAttackAICapabilityUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public AttackAICapabilityUC EffectAttackAICapabilityUCCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new AttackAICapabilityUCIncarnation(

            );
    EffectInternalCreateAttackAICapabilityUC(id, rootIncarnation.version, incarnation);
    return new AttackAICapabilityUC(this, id);
  }
  public void EffectInternalCreateAttackAICapabilityUC(
      int id,
      int incarnationVersion,
      AttackAICapabilityUCIncarnation incarnation) {
    CheckUnlocked();
    var effect = new AttackAICapabilityUCCreateEffect(id);
    rootIncarnation.incarnationsAttackAICapabilityUC.Add(
        id,
        new VersionAndIncarnation<AttackAICapabilityUCIncarnation>(
            incarnationVersion,
            incarnation));
    effectsAttackAICapabilityUCCreateEffect.Add(effect);
  }

  public void EffectAttackAICapabilityUCDelete(int id) {
    CheckUnlocked();
    var effect = new AttackAICapabilityUCDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsAttackAICapabilityUC[id];

    rootIncarnation.incarnationsAttackAICapabilityUC.Remove(id);
    effectsAttackAICapabilityUCDeleteEffect.Add(effect);
  }

     
  public int GetAttackAICapabilityUCHash(int id, int version, AttackAICapabilityUCIncarnation incarnation) {
    int result = id * version;
    return result;
  }
     
  public void BroadcastAttackAICapabilityUCEffects(
      SortedDictionary<int, List<IAttackAICapabilityUCEffectObserver>> observers) {
    foreach (var effect in effectsAttackAICapabilityUCDeleteEffect) {
      if (observers.TryGetValue(0, out List<IAttackAICapabilityUCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnAttackAICapabilityUCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IAttackAICapabilityUCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnAttackAICapabilityUCEffect(effect);
        }
        observersForAttackAICapabilityUC.Remove(effect.id);
      }
    }
    effectsAttackAICapabilityUCDeleteEffect.Clear();


    foreach (var effect in effectsAttackAICapabilityUCCreateEffect) {
      if (observers.TryGetValue(0, out List<IAttackAICapabilityUCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnAttackAICapabilityUCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IAttackAICapabilityUCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnAttackAICapabilityUCEffect(effect);
        }
      }
    }
    effectsAttackAICapabilityUCCreateEffect.Clear();
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
      throw new System.Exception("Invalid MoveImpulse: " + id);
    }
  }
  public void AddMoveImpulseObserver(int id, IMoveImpulseEffectObserver observer) {
    List<IMoveImpulseEffectObserver> obsies;
    if (!observersForMoveImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<IMoveImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersForMoveImpulse[id] = obsies;
  }

  public void RemoveMoveImpulseObserver(int id, IMoveImpulseEffectObserver observer) {
    if (observersForMoveImpulse.ContainsKey(id)) {
      var list = observersForMoveImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForMoveImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
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
    EffectInternalCreateMoveImpulse(id, rootIncarnation.version, incarnation);
    return new MoveImpulse(this, id);
  }
  public void EffectInternalCreateMoveImpulse(
      int id,
      int incarnationVersion,
      MoveImpulseIncarnation incarnation) {
    CheckUnlocked();
    var effect = new MoveImpulseCreateEffect(id);
    rootIncarnation.incarnationsMoveImpulse.Add(
        id,
        new VersionAndIncarnation<MoveImpulseIncarnation>(
            incarnationVersion,
            incarnation));
    effectsMoveImpulseCreateEffect.Add(effect);
  }

  public void EffectMoveImpulseDelete(int id) {
    CheckUnlocked();
    var effect = new MoveImpulseDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsMoveImpulse[id];

    rootIncarnation.incarnationsMoveImpulse.Remove(id);
    effectsMoveImpulseDeleteEffect.Add(effect);
  }

     
  public int GetMoveImpulseHash(int id, int version, MoveImpulseIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.weight.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.stepLocation.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastMoveImpulseEffects(
      SortedDictionary<int, List<IMoveImpulseEffectObserver>> observers) {
    foreach (var effect in effectsMoveImpulseDeleteEffect) {
      if (observers.TryGetValue(0, out List<IMoveImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnMoveImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IMoveImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnMoveImpulseEffect(effect);
        }
        observersForMoveImpulse.Remove(effect.id);
      }
    }
    effectsMoveImpulseDeleteEffect.Clear();


    foreach (var effect in effectsMoveImpulseCreateEffect) {
      if (observers.TryGetValue(0, out List<IMoveImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnMoveImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IMoveImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnMoveImpulseEffect(effect);
        }
      }
    }
    effectsMoveImpulseCreateEffect.Clear();
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
      throw new System.Exception("Invalid MoveDirectiveUC: " + id);
    }
  }
  public void AddMoveDirectiveUCObserver(int id, IMoveDirectiveUCEffectObserver observer) {
    List<IMoveDirectiveUCEffectObserver> obsies;
    if (!observersForMoveDirectiveUC.TryGetValue(id, out obsies)) {
      obsies = new List<IMoveDirectiveUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForMoveDirectiveUC[id] = obsies;
  }

  public void RemoveMoveDirectiveUCObserver(int id, IMoveDirectiveUCEffectObserver observer) {
    if (observersForMoveDirectiveUC.ContainsKey(id)) {
      var list = observersForMoveDirectiveUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForMoveDirectiveUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
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
    EffectInternalCreateMoveDirectiveUC(id, rootIncarnation.version, incarnation);
    return new MoveDirectiveUC(this, id);
  }
  public void EffectInternalCreateMoveDirectiveUC(
      int id,
      int incarnationVersion,
      MoveDirectiveUCIncarnation incarnation) {
    CheckUnlocked();
    var effect = new MoveDirectiveUCCreateEffect(id);
    rootIncarnation.incarnationsMoveDirectiveUC.Add(
        id,
        new VersionAndIncarnation<MoveDirectiveUCIncarnation>(
            incarnationVersion,
            incarnation));
    effectsMoveDirectiveUCCreateEffect.Add(effect);
  }

  public void EffectMoveDirectiveUCDelete(int id) {
    CheckUnlocked();
    var effect = new MoveDirectiveUCDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsMoveDirectiveUC[id];

    rootIncarnation.incarnationsMoveDirectiveUC.Remove(id);
    effectsMoveDirectiveUCDeleteEffect.Add(effect);
  }

     
  public int GetMoveDirectiveUCHash(int id, int version, MoveDirectiveUCIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.path.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastMoveDirectiveUCEffects(
      SortedDictionary<int, List<IMoveDirectiveUCEffectObserver>> observers) {
    foreach (var effect in effectsMoveDirectiveUCDeleteEffect) {
      if (observers.TryGetValue(0, out List<IMoveDirectiveUCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnMoveDirectiveUCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IMoveDirectiveUCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnMoveDirectiveUCEffect(effect);
        }
        observersForMoveDirectiveUC.Remove(effect.id);
      }
    }
    effectsMoveDirectiveUCDeleteEffect.Clear();


    foreach (var effect in effectsMoveDirectiveUCCreateEffect) {
      if (observers.TryGetValue(0, out List<IMoveDirectiveUCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnMoveDirectiveUCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IMoveDirectiveUCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnMoveDirectiveUCEffect(effect);
        }
      }
    }
    effectsMoveDirectiveUCCreateEffect.Clear();
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
      throw new System.Exception("Invalid Unit: " + id);
    }
  }
  public void AddUnitObserver(int id, IUnitEffectObserver observer) {
    List<IUnitEffectObserver> obsies;
    if (!observersForUnit.TryGetValue(id, out obsies)) {
      obsies = new List<IUnitEffectObserver>();
    }
    obsies.Add(observer);
    observersForUnit[id] = obsies;
  }

  public void RemoveUnitObserver(int id, IUnitEffectObserver observer) {
    if (observersForUnit.ContainsKey(id)) {
      var list = observersForUnit[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForUnit.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
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
    EffectInternalCreateUnit(id, rootIncarnation.version, incarnation);
    return new Unit(this, id);
  }
  public void EffectInternalCreateUnit(
      int id,
      int incarnationVersion,
      UnitIncarnation incarnation) {
    CheckUnlocked();
    var effect = new UnitCreateEffect(id);
    rootIncarnation.incarnationsUnit.Add(
        id,
        new VersionAndIncarnation<UnitIncarnation>(
            incarnationVersion,
            incarnation));
    effectsUnitCreateEffect.Add(effect);
  }

  public void EffectUnitDelete(int id) {
    CheckUnlocked();
    var effect = new UnitDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsUnit[id];

    rootIncarnation.incarnationsUnit.Remove(id);
    effectsUnitDeleteEffect.Add(effect);
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
     
  public void BroadcastUnitEffects(
      SortedDictionary<int, List<IUnitEffectObserver>> observers) {
    foreach (var effect in effectsUnitDeleteEffect) {
      if (observers.TryGetValue(0, out List<IUnitEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnUnitEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IUnitEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnUnitEffect(effect);
        }
        observersForUnit.Remove(effect.id);
      }
    }
    effectsUnitDeleteEffect.Clear();


    foreach (var effect in effectsUnitSetAliveEffect) {
      if (observers.TryGetValue(0, out List<IUnitEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnUnitEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IUnitEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnUnitEffect(effect);
        }
      }
    }
    effectsUnitSetAliveEffect.Clear();

    foreach (var effect in effectsUnitSetLifeEndTimeEffect) {
      if (observers.TryGetValue(0, out List<IUnitEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnUnitEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IUnitEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnUnitEffect(effect);
        }
      }
    }
    effectsUnitSetLifeEndTimeEffect.Clear();

    foreach (var effect in effectsUnitSetLocationEffect) {
      if (observers.TryGetValue(0, out List<IUnitEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnUnitEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IUnitEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnUnitEffect(effect);
        }
      }
    }
    effectsUnitSetLocationEffect.Clear();

    foreach (var effect in effectsUnitSetHpEffect) {
      if (observers.TryGetValue(0, out List<IUnitEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnUnitEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IUnitEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnUnitEffect(effect);
        }
      }
    }
    effectsUnitSetHpEffect.Clear();

    foreach (var effect in effectsUnitSetMpEffect) {
      if (observers.TryGetValue(0, out List<IUnitEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnUnitEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IUnitEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnUnitEffect(effect);
        }
      }
    }
    effectsUnitSetMpEffect.Clear();

    foreach (var effect in effectsUnitSetNextActionTimeEffect) {
      if (observers.TryGetValue(0, out List<IUnitEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnUnitEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IUnitEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnUnitEffect(effect);
        }
      }
    }
    effectsUnitSetNextActionTimeEffect.Clear();

    foreach (var effect in effectsUnitCreateEffect) {
      if (observers.TryGetValue(0, out List<IUnitEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnUnitEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IUnitEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnUnitEffect(effect);
        }
      }
    }
    effectsUnitCreateEffect.Clear();
  }

  public void EffectUnitSetAlive(int id, bool newValue) {
    CheckUnlocked();
    CheckHasUnit(id);
    var effect = new UnitSetAliveEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsUnit[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.alive;
      oldIncarnationAndVersion.incarnation.alive = newValue;

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
    }

    effectsUnitSetAliveEffect.Add(effect);
  }

  public void EffectUnitSetLifeEndTime(int id, int newValue) {
    CheckUnlocked();
    CheckHasUnit(id);
    var effect = new UnitSetLifeEndTimeEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsUnit[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.lifeEndTime;
      oldIncarnationAndVersion.incarnation.lifeEndTime = newValue;

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
    }

    effectsUnitSetLifeEndTimeEffect.Add(effect);
  }

  public void EffectUnitSetLocation(int id, Location newValue) {
    CheckUnlocked();
    CheckHasUnit(id);
    var effect = new UnitSetLocationEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsUnit[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.location;
      oldIncarnationAndVersion.incarnation.location = newValue;

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
    }

    effectsUnitSetLocationEffect.Add(effect);
  }

  public void EffectUnitSetHp(int id, int newValue) {
    CheckUnlocked();
    CheckHasUnit(id);
    var effect = new UnitSetHpEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsUnit[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.hp;
      oldIncarnationAndVersion.incarnation.hp = newValue;

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
    }

    effectsUnitSetHpEffect.Add(effect);
  }

  public void EffectUnitSetMp(int id, int newValue) {
    CheckUnlocked();
    CheckHasUnit(id);
    var effect = new UnitSetMpEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsUnit[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.mp;
      oldIncarnationAndVersion.incarnation.mp = newValue;

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
    }

    effectsUnitSetMpEffect.Add(effect);
  }

  public void EffectUnitSetNextActionTime(int id, int newValue) {
    CheckUnlocked();
    CheckHasUnit(id);
    var effect = new UnitSetNextActionTimeEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsUnit[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.nextActionTime;
      oldIncarnationAndVersion.incarnation.nextActionTime = newValue;

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
    }

    effectsUnitSetNextActionTimeEffect.Add(effect);
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
      throw new System.Exception("Invalid IItemMutBunch: " + id);
    }
  }
  public void AddIItemMutBunchObserver(int id, IIItemMutBunchEffectObserver observer) {
    List<IIItemMutBunchEffectObserver> obsies;
    if (!observersForIItemMutBunch.TryGetValue(id, out obsies)) {
      obsies = new List<IIItemMutBunchEffectObserver>();
    }
    obsies.Add(observer);
    observersForIItemMutBunch[id] = obsies;
  }

  public void RemoveIItemMutBunchObserver(int id, IIItemMutBunchEffectObserver observer) {
    if (observersForIItemMutBunch.ContainsKey(id)) {
      var list = observersForIItemMutBunch[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForIItemMutBunch.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
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
    EffectInternalCreateIItemMutBunch(id, rootIncarnation.version, incarnation);
    return new IItemMutBunch(this, id);
  }
  public void EffectInternalCreateIItemMutBunch(
      int id,
      int incarnationVersion,
      IItemMutBunchIncarnation incarnation) {
    CheckUnlocked();
    var effect = new IItemMutBunchCreateEffect(id);
    rootIncarnation.incarnationsIItemMutBunch.Add(
        id,
        new VersionAndIncarnation<IItemMutBunchIncarnation>(
            incarnationVersion,
            incarnation));
    effectsIItemMutBunchCreateEffect.Add(effect);
  }

  public void EffectIItemMutBunchDelete(int id) {
    CheckUnlocked();
    var effect = new IItemMutBunchDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsIItemMutBunch[id];

    rootIncarnation.incarnationsIItemMutBunch.Remove(id);
    effectsIItemMutBunchDeleteEffect.Add(effect);
  }

     
  public int GetIItemMutBunchHash(int id, int version, IItemMutBunchIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.membersGlaiveMutSet.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.membersArmorMutSet.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastIItemMutBunchEffects(
      SortedDictionary<int, List<IIItemMutBunchEffectObserver>> observers) {
    foreach (var effect in effectsIItemMutBunchDeleteEffect) {
      if (observers.TryGetValue(0, out List<IIItemMutBunchEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnIItemMutBunchEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IIItemMutBunchEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnIItemMutBunchEffect(effect);
        }
        observersForIItemMutBunch.Remove(effect.id);
      }
    }
    effectsIItemMutBunchDeleteEffect.Clear();


    foreach (var effect in effectsIItemMutBunchCreateEffect) {
      if (observers.TryGetValue(0, out List<IIItemMutBunchEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnIItemMutBunchEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IIItemMutBunchEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnIItemMutBunchEffect(effect);
        }
      }
    }
    effectsIItemMutBunchCreateEffect.Clear();
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
      throw new System.Exception("Invalid IUnitComponentMutBunch: " + id);
    }
  }
  public void AddIUnitComponentMutBunchObserver(int id, IIUnitComponentMutBunchEffectObserver observer) {
    List<IIUnitComponentMutBunchEffectObserver> obsies;
    if (!observersForIUnitComponentMutBunch.TryGetValue(id, out obsies)) {
      obsies = new List<IIUnitComponentMutBunchEffectObserver>();
    }
    obsies.Add(observer);
    observersForIUnitComponentMutBunch[id] = obsies;
  }

  public void RemoveIUnitComponentMutBunchObserver(int id, IIUnitComponentMutBunchEffectObserver observer) {
    if (observersForIUnitComponentMutBunch.ContainsKey(id)) {
      var list = observersForIUnitComponentMutBunch[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForIUnitComponentMutBunch.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
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
    EffectInternalCreateIUnitComponentMutBunch(id, rootIncarnation.version, incarnation);
    return new IUnitComponentMutBunch(this, id);
  }
  public void EffectInternalCreateIUnitComponentMutBunch(
      int id,
      int incarnationVersion,
      IUnitComponentMutBunchIncarnation incarnation) {
    CheckUnlocked();
    var effect = new IUnitComponentMutBunchCreateEffect(id);
    rootIncarnation.incarnationsIUnitComponentMutBunch.Add(
        id,
        new VersionAndIncarnation<IUnitComponentMutBunchIncarnation>(
            incarnationVersion,
            incarnation));
    effectsIUnitComponentMutBunchCreateEffect.Add(effect);
  }

  public void EffectIUnitComponentMutBunchDelete(int id) {
    CheckUnlocked();
    var effect = new IUnitComponentMutBunchDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsIUnitComponentMutBunch[id];

    rootIncarnation.incarnationsIUnitComponentMutBunch.Remove(id);
    effectsIUnitComponentMutBunchDeleteEffect.Add(effect);
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
     
  public void BroadcastIUnitComponentMutBunchEffects(
      SortedDictionary<int, List<IIUnitComponentMutBunchEffectObserver>> observers) {
    foreach (var effect in effectsIUnitComponentMutBunchDeleteEffect) {
      if (observers.TryGetValue(0, out List<IIUnitComponentMutBunchEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnIUnitComponentMutBunchEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IIUnitComponentMutBunchEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnIUnitComponentMutBunchEffect(effect);
        }
        observersForIUnitComponentMutBunch.Remove(effect.id);
      }
    }
    effectsIUnitComponentMutBunchDeleteEffect.Clear();


    foreach (var effect in effectsIUnitComponentMutBunchCreateEffect) {
      if (observers.TryGetValue(0, out List<IIUnitComponentMutBunchEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnIUnitComponentMutBunchEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IIUnitComponentMutBunchEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnIUnitComponentMutBunchEffect(effect);
        }
      }
    }
    effectsIUnitComponentMutBunchCreateEffect.Clear();
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
      throw new System.Exception("Invalid NoImpulse: " + id);
    }
  }
  public void AddNoImpulseObserver(int id, INoImpulseEffectObserver observer) {
    List<INoImpulseEffectObserver> obsies;
    if (!observersForNoImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<INoImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersForNoImpulse[id] = obsies;
  }

  public void RemoveNoImpulseObserver(int id, INoImpulseEffectObserver observer) {
    if (observersForNoImpulse.ContainsKey(id)) {
      var list = observersForNoImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForNoImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public NoImpulse EffectNoImpulseCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new NoImpulseIncarnation(

            );
    EffectInternalCreateNoImpulse(id, rootIncarnation.version, incarnation);
    return new NoImpulse(this, id);
  }
  public void EffectInternalCreateNoImpulse(
      int id,
      int incarnationVersion,
      NoImpulseIncarnation incarnation) {
    CheckUnlocked();
    var effect = new NoImpulseCreateEffect(id);
    rootIncarnation.incarnationsNoImpulse.Add(
        id,
        new VersionAndIncarnation<NoImpulseIncarnation>(
            incarnationVersion,
            incarnation));
    effectsNoImpulseCreateEffect.Add(effect);
  }

  public void EffectNoImpulseDelete(int id) {
    CheckUnlocked();
    var effect = new NoImpulseDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsNoImpulse[id];

    rootIncarnation.incarnationsNoImpulse.Remove(id);
    effectsNoImpulseDeleteEffect.Add(effect);
  }

     
  public int GetNoImpulseHash(int id, int version, NoImpulseIncarnation incarnation) {
    int result = id * version;
    return result;
  }
     
  public void BroadcastNoImpulseEffects(
      SortedDictionary<int, List<INoImpulseEffectObserver>> observers) {
    foreach (var effect in effectsNoImpulseDeleteEffect) {
      if (observers.TryGetValue(0, out List<INoImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnNoImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<INoImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnNoImpulseEffect(effect);
        }
        observersForNoImpulse.Remove(effect.id);
      }
    }
    effectsNoImpulseDeleteEffect.Clear();


    foreach (var effect in effectsNoImpulseCreateEffect) {
      if (observers.TryGetValue(0, out List<INoImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnNoImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<INoImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnNoImpulseEffect(effect);
        }
      }
    }
    effectsNoImpulseCreateEffect.Clear();
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
      throw new System.Exception("Invalid ExecutionState: " + id);
    }
  }
  public void AddExecutionStateObserver(int id, IExecutionStateEffectObserver observer) {
    List<IExecutionStateEffectObserver> obsies;
    if (!observersForExecutionState.TryGetValue(id, out obsies)) {
      obsies = new List<IExecutionStateEffectObserver>();
    }
    obsies.Add(observer);
    observersForExecutionState[id] = obsies;
  }

  public void RemoveExecutionStateObserver(int id, IExecutionStateEffectObserver observer) {
    if (observersForExecutionState.ContainsKey(id)) {
      var list = observersForExecutionState[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForExecutionState.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
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
    EffectInternalCreateExecutionState(id, rootIncarnation.version, incarnation);
    return new ExecutionState(this, id);
  }
  public void EffectInternalCreateExecutionState(
      int id,
      int incarnationVersion,
      ExecutionStateIncarnation incarnation) {
    CheckUnlocked();
    var effect = new ExecutionStateCreateEffect(id);
    rootIncarnation.incarnationsExecutionState.Add(
        id,
        new VersionAndIncarnation<ExecutionStateIncarnation>(
            incarnationVersion,
            incarnation));
    effectsExecutionStateCreateEffect.Add(effect);
  }

  public void EffectExecutionStateDelete(int id) {
    CheckUnlocked();
    var effect = new ExecutionStateDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsExecutionState[id];

    rootIncarnation.incarnationsExecutionState.Remove(id);
    effectsExecutionStateDeleteEffect.Add(effect);
  }

     
  public int GetExecutionStateHash(int id, int version, ExecutionStateIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.actingUnit.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.actingUnitDidAction.GetDeterministicHashCode();
    result += id * version * 3 * incarnation.remainingPreActingUnitComponents.GetDeterministicHashCode();
    result += id * version * 4 * incarnation.remainingPostActingUnitComponents.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastExecutionStateEffects(
      SortedDictionary<int, List<IExecutionStateEffectObserver>> observers) {
    foreach (var effect in effectsExecutionStateDeleteEffect) {
      if (observers.TryGetValue(0, out List<IExecutionStateEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnExecutionStateEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IExecutionStateEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnExecutionStateEffect(effect);
        }
        observersForExecutionState.Remove(effect.id);
      }
    }
    effectsExecutionStateDeleteEffect.Clear();


    foreach (var effect in effectsExecutionStateSetActingUnitEffect) {
      if (observers.TryGetValue(0, out List<IExecutionStateEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnExecutionStateEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IExecutionStateEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnExecutionStateEffect(effect);
        }
      }
    }
    effectsExecutionStateSetActingUnitEffect.Clear();

    foreach (var effect in effectsExecutionStateSetActingUnitDidActionEffect) {
      if (observers.TryGetValue(0, out List<IExecutionStateEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnExecutionStateEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IExecutionStateEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnExecutionStateEffect(effect);
        }
      }
    }
    effectsExecutionStateSetActingUnitDidActionEffect.Clear();

    foreach (var effect in effectsExecutionStateSetRemainingPreActingUnitComponentsEffect) {
      if (observers.TryGetValue(0, out List<IExecutionStateEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnExecutionStateEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IExecutionStateEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnExecutionStateEffect(effect);
        }
      }
    }
    effectsExecutionStateSetRemainingPreActingUnitComponentsEffect.Clear();

    foreach (var effect in effectsExecutionStateSetRemainingPostActingUnitComponentsEffect) {
      if (observers.TryGetValue(0, out List<IExecutionStateEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnExecutionStateEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IExecutionStateEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnExecutionStateEffect(effect);
        }
      }
    }
    effectsExecutionStateSetRemainingPostActingUnitComponentsEffect.Clear();

    foreach (var effect in effectsExecutionStateCreateEffect) {
      if (observers.TryGetValue(0, out List<IExecutionStateEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnExecutionStateEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IExecutionStateEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnExecutionStateEffect(effect);
        }
      }
    }
    effectsExecutionStateCreateEffect.Clear();
  }

  public void EffectExecutionStateSetActingUnit(int id, Unit newValue) {
    CheckUnlocked();
    CheckHasExecutionState(id);
    var effect = new ExecutionStateSetActingUnitEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsExecutionState[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldId = oldIncarnationAndVersion.incarnation.actingUnit;
      oldIncarnationAndVersion.incarnation.actingUnit = newValue.id;

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
    }

    effectsExecutionStateSetActingUnitEffect.Add(effect);
  }

  public void EffectExecutionStateSetActingUnitDidAction(int id, bool newValue) {
    CheckUnlocked();
    CheckHasExecutionState(id);
    var effect = new ExecutionStateSetActingUnitDidActionEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsExecutionState[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.actingUnitDidAction;
      oldIncarnationAndVersion.incarnation.actingUnitDidAction = newValue;

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
    }

    effectsExecutionStateSetActingUnitDidActionEffect.Add(effect);
  }

  public void EffectExecutionStateSetRemainingPreActingUnitComponents(int id, IPreActingUCWeakMutBunch newValue) {
    CheckUnlocked();
    CheckHasExecutionState(id);
    var effect = new ExecutionStateSetRemainingPreActingUnitComponentsEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsExecutionState[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldId = oldIncarnationAndVersion.incarnation.remainingPreActingUnitComponents;
      oldIncarnationAndVersion.incarnation.remainingPreActingUnitComponents = newValue.id;

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
    }

    effectsExecutionStateSetRemainingPreActingUnitComponentsEffect.Add(effect);
  }

  public void EffectExecutionStateSetRemainingPostActingUnitComponents(int id, IPostActingUCWeakMutBunch newValue) {
    CheckUnlocked();
    CheckHasExecutionState(id);
    var effect = new ExecutionStateSetRemainingPostActingUnitComponentsEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsExecutionState[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldId = oldIncarnationAndVersion.incarnation.remainingPostActingUnitComponents;
      oldIncarnationAndVersion.incarnation.remainingPostActingUnitComponents = newValue.id;

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
    }

    effectsExecutionStateSetRemainingPostActingUnitComponentsEffect.Add(effect);
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
      throw new System.Exception("Invalid IPostActingUCWeakMutBunch: " + id);
    }
  }
  public void AddIPostActingUCWeakMutBunchObserver(int id, IIPostActingUCWeakMutBunchEffectObserver observer) {
    List<IIPostActingUCWeakMutBunchEffectObserver> obsies;
    if (!observersForIPostActingUCWeakMutBunch.TryGetValue(id, out obsies)) {
      obsies = new List<IIPostActingUCWeakMutBunchEffectObserver>();
    }
    obsies.Add(observer);
    observersForIPostActingUCWeakMutBunch[id] = obsies;
  }

  public void RemoveIPostActingUCWeakMutBunchObserver(int id, IIPostActingUCWeakMutBunchEffectObserver observer) {
    if (observersForIPostActingUCWeakMutBunch.ContainsKey(id)) {
      var list = observersForIPostActingUCWeakMutBunch[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForIPostActingUCWeakMutBunch.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public IPostActingUCWeakMutBunch EffectIPostActingUCWeakMutBunchCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new IPostActingUCWeakMutBunchIncarnation(

            );
    EffectInternalCreateIPostActingUCWeakMutBunch(id, rootIncarnation.version, incarnation);
    return new IPostActingUCWeakMutBunch(this, id);
  }
  public void EffectInternalCreateIPostActingUCWeakMutBunch(
      int id,
      int incarnationVersion,
      IPostActingUCWeakMutBunchIncarnation incarnation) {
    CheckUnlocked();
    var effect = new IPostActingUCWeakMutBunchCreateEffect(id);
    rootIncarnation.incarnationsIPostActingUCWeakMutBunch.Add(
        id,
        new VersionAndIncarnation<IPostActingUCWeakMutBunchIncarnation>(
            incarnationVersion,
            incarnation));
    effectsIPostActingUCWeakMutBunchCreateEffect.Add(effect);
  }

  public void EffectIPostActingUCWeakMutBunchDelete(int id) {
    CheckUnlocked();
    var effect = new IPostActingUCWeakMutBunchDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsIPostActingUCWeakMutBunch[id];

    rootIncarnation.incarnationsIPostActingUCWeakMutBunch.Remove(id);
    effectsIPostActingUCWeakMutBunchDeleteEffect.Add(effect);
  }

     
  public int GetIPostActingUCWeakMutBunchHash(int id, int version, IPostActingUCWeakMutBunchIncarnation incarnation) {
    int result = id * version;
    return result;
  }
     
  public void BroadcastIPostActingUCWeakMutBunchEffects(
      SortedDictionary<int, List<IIPostActingUCWeakMutBunchEffectObserver>> observers) {
    foreach (var effect in effectsIPostActingUCWeakMutBunchDeleteEffect) {
      if (observers.TryGetValue(0, out List<IIPostActingUCWeakMutBunchEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnIPostActingUCWeakMutBunchEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IIPostActingUCWeakMutBunchEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnIPostActingUCWeakMutBunchEffect(effect);
        }
        observersForIPostActingUCWeakMutBunch.Remove(effect.id);
      }
    }
    effectsIPostActingUCWeakMutBunchDeleteEffect.Clear();


    foreach (var effect in effectsIPostActingUCWeakMutBunchCreateEffect) {
      if (observers.TryGetValue(0, out List<IIPostActingUCWeakMutBunchEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnIPostActingUCWeakMutBunchEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IIPostActingUCWeakMutBunchEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnIPostActingUCWeakMutBunchEffect(effect);
        }
      }
    }
    effectsIPostActingUCWeakMutBunchCreateEffect.Clear();
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
      throw new System.Exception("Invalid IPreActingUCWeakMutBunch: " + id);
    }
  }
  public void AddIPreActingUCWeakMutBunchObserver(int id, IIPreActingUCWeakMutBunchEffectObserver observer) {
    List<IIPreActingUCWeakMutBunchEffectObserver> obsies;
    if (!observersForIPreActingUCWeakMutBunch.TryGetValue(id, out obsies)) {
      obsies = new List<IIPreActingUCWeakMutBunchEffectObserver>();
    }
    obsies.Add(observer);
    observersForIPreActingUCWeakMutBunch[id] = obsies;
  }

  public void RemoveIPreActingUCWeakMutBunchObserver(int id, IIPreActingUCWeakMutBunchEffectObserver observer) {
    if (observersForIPreActingUCWeakMutBunch.ContainsKey(id)) {
      var list = observersForIPreActingUCWeakMutBunch[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForIPreActingUCWeakMutBunch.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
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
    EffectInternalCreateIPreActingUCWeakMutBunch(id, rootIncarnation.version, incarnation);
    return new IPreActingUCWeakMutBunch(this, id);
  }
  public void EffectInternalCreateIPreActingUCWeakMutBunch(
      int id,
      int incarnationVersion,
      IPreActingUCWeakMutBunchIncarnation incarnation) {
    CheckUnlocked();
    var effect = new IPreActingUCWeakMutBunchCreateEffect(id);
    rootIncarnation.incarnationsIPreActingUCWeakMutBunch.Add(
        id,
        new VersionAndIncarnation<IPreActingUCWeakMutBunchIncarnation>(
            incarnationVersion,
            incarnation));
    effectsIPreActingUCWeakMutBunchCreateEffect.Add(effect);
  }

  public void EffectIPreActingUCWeakMutBunchDelete(int id) {
    CheckUnlocked();
    var effect = new IPreActingUCWeakMutBunchDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsIPreActingUCWeakMutBunch[id];

    rootIncarnation.incarnationsIPreActingUCWeakMutBunch.Remove(id);
    effectsIPreActingUCWeakMutBunchDeleteEffect.Add(effect);
  }

     
  public int GetIPreActingUCWeakMutBunchHash(int id, int version, IPreActingUCWeakMutBunchIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.membersShieldingUCWeakMutSet.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.membersAttackAICapabilityUCWeakMutSet.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastIPreActingUCWeakMutBunchEffects(
      SortedDictionary<int, List<IIPreActingUCWeakMutBunchEffectObserver>> observers) {
    foreach (var effect in effectsIPreActingUCWeakMutBunchDeleteEffect) {
      if (observers.TryGetValue(0, out List<IIPreActingUCWeakMutBunchEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnIPreActingUCWeakMutBunchEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IIPreActingUCWeakMutBunchEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnIPreActingUCWeakMutBunchEffect(effect);
        }
        observersForIPreActingUCWeakMutBunch.Remove(effect.id);
      }
    }
    effectsIPreActingUCWeakMutBunchDeleteEffect.Clear();


    foreach (var effect in effectsIPreActingUCWeakMutBunchCreateEffect) {
      if (observers.TryGetValue(0, out List<IIPreActingUCWeakMutBunchEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnIPreActingUCWeakMutBunchEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IIPreActingUCWeakMutBunchEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnIPreActingUCWeakMutBunchEffect(effect);
        }
      }
    }
    effectsIPreActingUCWeakMutBunchCreateEffect.Clear();
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
      throw new System.Exception("Invalid Game: " + id);
    }
  }
  public void AddGameObserver(int id, IGameEffectObserver observer) {
    List<IGameEffectObserver> obsies;
    if (!observersForGame.TryGetValue(id, out obsies)) {
      obsies = new List<IGameEffectObserver>();
    }
    obsies.Add(observer);
    observersForGame[id] = obsies;
  }

  public void RemoveGameObserver(int id, IGameEffectObserver observer) {
    if (observersForGame.ContainsKey(id)) {
      var list = observersForGame[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForGame.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
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
    EffectInternalCreateGame(id, rootIncarnation.version, incarnation);
    return new Game(this, id);
  }
  public void EffectInternalCreateGame(
      int id,
      int incarnationVersion,
      GameIncarnation incarnation) {
    CheckUnlocked();
    var effect = new GameCreateEffect(id);
    rootIncarnation.incarnationsGame.Add(
        id,
        new VersionAndIncarnation<GameIncarnation>(
            incarnationVersion,
            incarnation));
    effectsGameCreateEffect.Add(effect);
  }

  public void EffectGameDelete(int id) {
    CheckUnlocked();
    var effect = new GameDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsGame[id];

    rootIncarnation.incarnationsGame.Remove(id);
    effectsGameDeleteEffect.Add(effect);
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
     
  public void BroadcastGameEffects(
      SortedDictionary<int, List<IGameEffectObserver>> observers) {
    foreach (var effect in effectsGameDeleteEffect) {
      if (observers.TryGetValue(0, out List<IGameEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnGameEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IGameEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnGameEffect(effect);
        }
        observersForGame.Remove(effect.id);
      }
    }
    effectsGameDeleteEffect.Clear();


    foreach (var effect in effectsGameSetPlayerEffect) {
      if (observers.TryGetValue(0, out List<IGameEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnGameEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IGameEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnGameEffect(effect);
        }
      }
    }
    effectsGameSetPlayerEffect.Clear();

    foreach (var effect in effectsGameSetLevelEffect) {
      if (observers.TryGetValue(0, out List<IGameEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnGameEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IGameEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnGameEffect(effect);
        }
      }
    }
    effectsGameSetLevelEffect.Clear();

    foreach (var effect in effectsGameSetTimeEffect) {
      if (observers.TryGetValue(0, out List<IGameEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnGameEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IGameEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnGameEffect(effect);
        }
      }
    }
    effectsGameSetTimeEffect.Clear();

    foreach (var effect in effectsGameCreateEffect) {
      if (observers.TryGetValue(0, out List<IGameEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnGameEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IGameEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnGameEffect(effect);
        }
      }
    }
    effectsGameCreateEffect.Clear();
  }

  public void EffectGameSetPlayer(int id, Unit newValue) {
    CheckUnlocked();
    CheckHasGame(id);
    var effect = new GameSetPlayerEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsGame[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldId = oldIncarnationAndVersion.incarnation.player;
      oldIncarnationAndVersion.incarnation.player = newValue.id;

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
    }

    effectsGameSetPlayerEffect.Add(effect);
  }

  public void EffectGameSetLevel(int id, Level newValue) {
    CheckUnlocked();
    CheckHasGame(id);
    var effect = new GameSetLevelEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsGame[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldId = oldIncarnationAndVersion.incarnation.level;
      oldIncarnationAndVersion.incarnation.level = newValue.id;

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
    }

    effectsGameSetLevelEffect.Add(effect);
  }

  public void EffectGameSetTime(int id, int newValue) {
    CheckUnlocked();
    CheckHasGame(id);
    var effect = new GameSetTimeEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsGame[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.time;
      oldIncarnationAndVersion.incarnation.time = newValue;

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
    }

    effectsGameSetTimeEffect.Add(effect);
  }

  public ITerrainTileComponent GetITerrainTileComponent(int id) {
    if (rootIncarnation.incarnationsItemTerrainTileComponent.ContainsKey(id)) {
      return new ItemTerrainTileComponentAsITerrainTileComponent(new ItemTerrainTileComponent(this, id));
    }
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
    if (rootIncarnation.incarnationsItemTerrainTileComponent.ContainsKey(id)) {
      return new ItemTerrainTileComponentAsITerrainTileComponent(new ItemTerrainTileComponent(this, id));
    }
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
  public bool ITerrainTileComponentExists(int id) {
    return GetITerrainTileComponentOrNull(id) != null;
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
  public bool IItemExists(int id) {
    return GetIItemOrNull(id) != null;
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
  public bool IOperationUCExists(int id) {
    return GetIOperationUCOrNull(id) != null;
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
  public bool IDirectiveUCExists(int id) {
    return GetIDirectiveUCOrNull(id) != null;
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
  public bool IAICapabilityUCExists(int id) {
    return GetIAICapabilityUCOrNull(id) != null;
  }
  public void CheckHasIAICapabilityUC(IAICapabilityUC thing) {
    GetIAICapabilityUC(thing.id);
  }
  public void CheckHasIAICapabilityUC(int id) {
    GetIAICapabilityUC(id);
  }

  public IPostActingUC GetIPostActingUC(int id) {
    throw new Exception("Unknown IPostActingUC: " + id);
  }
  public IPostActingUC GetIPostActingUCOrNull(int id) {
    return NullIPostActingUC.Null;
  }
  public bool IPostActingUCExists(int id) {
    return GetIPostActingUCOrNull(id) != null;
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
  public bool IPreActingUCExists(int id) {
    return GetIPreActingUCOrNull(id) != null;
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
  public bool IDefenseUCExists(int id) {
    return GetIDefenseUCOrNull(id) != null;
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
  public bool IUnitComponentExists(int id) {
    return GetIUnitComponentOrNull(id) != null;
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
  public bool IImpulseExists(int id) {
    return GetIImpulseOrNull(id) != null;
  }
  public void CheckHasIImpulse(IImpulse thing) {
    GetIImpulse(thing.id);
  }
  public void CheckHasIImpulse(int id) {
    GetIImpulse(id);
  }

  public IDestructible GetIDestructible(int id) {
    if (rootIncarnation.incarnationsItemTerrainTileComponent.ContainsKey(id)) {
      return new ItemTerrainTileComponentAsIDestructible(new ItemTerrainTileComponent(this, id));
    }
    if (rootIncarnation.incarnationsDecorativeTerrainTileComponent.ContainsKey(id)) {
      return new DecorativeTerrainTileComponentAsIDestructible(new DecorativeTerrainTileComponent(this, id));
    }
    if (rootIncarnation.incarnationsUpStaircaseTerrainTileComponent.ContainsKey(id)) {
      return new UpStaircaseTerrainTileComponentAsIDestructible(new UpStaircaseTerrainTileComponent(this, id));
    }
    if (rootIncarnation.incarnationsDownStaircaseTerrainTileComponent.ContainsKey(id)) {
      return new DownStaircaseTerrainTileComponentAsIDestructible(new DownStaircaseTerrainTileComponent(this, id));
    }
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
    if (rootIncarnation.incarnationsItemTerrainTileComponent.ContainsKey(id)) {
      return new ItemTerrainTileComponentAsIDestructible(new ItemTerrainTileComponent(this, id));
    }
    if (rootIncarnation.incarnationsDecorativeTerrainTileComponent.ContainsKey(id)) {
      return new DecorativeTerrainTileComponentAsIDestructible(new DecorativeTerrainTileComponent(this, id));
    }
    if (rootIncarnation.incarnationsUpStaircaseTerrainTileComponent.ContainsKey(id)) {
      return new UpStaircaseTerrainTileComponentAsIDestructible(new UpStaircaseTerrainTileComponent(this, id));
    }
    if (rootIncarnation.incarnationsDownStaircaseTerrainTileComponent.ContainsKey(id)) {
      return new DownStaircaseTerrainTileComponentAsIDestructible(new DownStaircaseTerrainTileComponent(this, id));
    }
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
  public bool IDestructibleExists(int id) {
    return GetIDestructibleOrNull(id) != null;
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
        throw new System.Exception("Invalid IUnitEventMutList}: " + id);
      }
    }
    public IUnitEventMutList EffectIUnitEventMutListCreate() {
      CheckUnlocked();
      var id = NewId();
      EffectInternalCreateIUnitEventMutList(id, rootIncarnation.version, new IUnitEventMutListIncarnation(new List<IUnitEvent>()));
      return new IUnitEventMutList(this, id);
    }
    public IUnitEventMutList EffectIUnitEventMutListCreate(IEnumerable<IUnitEvent> elements) {
      var id = NewId();
      var incarnation = new IUnitEventMutListIncarnation(new List<IUnitEvent>(elements));
      EffectInternalCreateIUnitEventMutList(id, rootIncarnation.version, incarnation);
      return new IUnitEventMutList(this, id);
    }
    public void EffectInternalCreateIUnitEventMutList(int id, int incarnationVersion, IUnitEventMutListIncarnation incarnation) {
      var effect = new IUnitEventMutListCreateEffect(id);
      rootIncarnation.incarnationsIUnitEventMutList
          .Add(
              id,
              new VersionAndIncarnation<IUnitEventMutListIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsIUnitEventMutListCreateEffect.Add(effect);
    }
    public void EffectIUnitEventMutListDelete(int id) {
      CheckUnlocked();
      var effect = new IUnitEventMutListDeleteEffect(id);
      effectsIUnitEventMutListDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsIUnitEventMutList[id];
      rootIncarnation.incarnationsIUnitEventMutList.Remove(id);
    }
    public void EffectIUnitEventMutListAdd(int listId, IUnitEvent element) {
      CheckUnlocked();
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
      effectsIUnitEventMutListAddEffect.Add(effect);
    }
    public void EffectIUnitEventMutListRemoveAt(int listId, int index) {
      CheckUnlocked();
      CheckHasIUnitEventMutList(listId);

      var effect = new IUnitEventMutListRemoveEffect(listId, index);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsIUnitEventMutList[listId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        var oldElement = oldIncarnationAndVersion.incarnation.list[index];
        oldIncarnationAndVersion.incarnation.list.RemoveAt(index);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.list;
        var newMap = new List<IUnitEvent>(oldMap);
        newMap.RemoveAt(index);
        var newIncarnation = new IUnitEventMutListIncarnation(newMap);
        rootIncarnation.incarnationsIUnitEventMutList[listId] =
            new VersionAndIncarnation<IUnitEventMutListIncarnation>(
                rootIncarnation.version, newIncarnation);

      }
      effectsIUnitEventMutListRemoveEffect.Add(effect);
    }
       
    public void AddIUnitEventMutListObserver(int id, IIUnitEventMutListEffectObserver observer) {
      List<IIUnitEventMutListEffectObserver> obsies;
      if (!observersForIUnitEventMutList.TryGetValue(id, out obsies)) {
        obsies = new List<IIUnitEventMutListEffectObserver>();
      }
      obsies.Add(observer);
      observersForIUnitEventMutList[id] = obsies;
    }

    public void RemoveIUnitEventMutListObserver(int id, IIUnitEventMutListEffectObserver observer) {
      if (observersForIUnitEventMutList.ContainsKey(id)) {
        var list = observersForIUnitEventMutList[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForIUnitEventMutList.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

  public void BroadcastIUnitEventMutListEffects(
      SortedDictionary<int, List<IIUnitEventMutListEffectObserver>> observers) {
    foreach (var effect in effectsIUnitEventMutListDeleteEffect) {
      if (observers.TryGetValue(0, out List<IIUnitEventMutListEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnIUnitEventMutListEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IIUnitEventMutListEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnIUnitEventMutListEffect(effect);
        }
        observersForIUnitEventMutList.Remove(effect.id);
      }
    }
    effectsIUnitEventMutListDeleteEffect.Clear();

    foreach (var effect in effectsIUnitEventMutListAddEffect) {
      if (observers.TryGetValue(0, out List<IIUnitEventMutListEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnIUnitEventMutListEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IIUnitEventMutListEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnIUnitEventMutListEffect(effect);
        }
      }
    }
    effectsIUnitEventMutListAddEffect.Clear();

    foreach (var effect in effectsIUnitEventMutListRemoveEffect) {
      if (observers.TryGetValue(0, out List<IIUnitEventMutListEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnIUnitEventMutListEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IIUnitEventMutListEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnIUnitEventMutListEffect(effect);
        }
      }
    }
    effectsIUnitEventMutListRemoveEffect.Clear();

    foreach (var effect in effectsIUnitEventMutListCreateEffect) {
      if (observers.TryGetValue(0, out List<IIUnitEventMutListEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnIUnitEventMutListEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IIUnitEventMutListEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnIUnitEventMutListEffect(effect);
        }
      }
    }
    effectsIUnitEventMutListCreateEffect.Clear();

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
        throw new System.Exception("Invalid LocationMutList}: " + id);
      }
    }
    public LocationMutList EffectLocationMutListCreate() {
      CheckUnlocked();
      var id = NewId();
      EffectInternalCreateLocationMutList(id, rootIncarnation.version, new LocationMutListIncarnation(new List<Location>()));
      return new LocationMutList(this, id);
    }
    public LocationMutList EffectLocationMutListCreate(IEnumerable<Location> elements) {
      var id = NewId();
      var incarnation = new LocationMutListIncarnation(new List<Location>(elements));
      EffectInternalCreateLocationMutList(id, rootIncarnation.version, incarnation);
      return new LocationMutList(this, id);
    }
    public void EffectInternalCreateLocationMutList(int id, int incarnationVersion, LocationMutListIncarnation incarnation) {
      var effect = new LocationMutListCreateEffect(id);
      rootIncarnation.incarnationsLocationMutList
          .Add(
              id,
              new VersionAndIncarnation<LocationMutListIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsLocationMutListCreateEffect.Add(effect);
    }
    public void EffectLocationMutListDelete(int id) {
      CheckUnlocked();
      var effect = new LocationMutListDeleteEffect(id);
      effectsLocationMutListDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsLocationMutList[id];
      rootIncarnation.incarnationsLocationMutList.Remove(id);
    }
    public void EffectLocationMutListAdd(int listId, Location element) {
      CheckUnlocked();
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
      effectsLocationMutListAddEffect.Add(effect);
    }
    public void EffectLocationMutListRemoveAt(int listId, int index) {
      CheckUnlocked();
      CheckHasLocationMutList(listId);

      var effect = new LocationMutListRemoveEffect(listId, index);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsLocationMutList[listId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        var oldElement = oldIncarnationAndVersion.incarnation.list[index];
        oldIncarnationAndVersion.incarnation.list.RemoveAt(index);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.list;
        var newMap = new List<Location>(oldMap);
        newMap.RemoveAt(index);
        var newIncarnation = new LocationMutListIncarnation(newMap);
        rootIncarnation.incarnationsLocationMutList[listId] =
            new VersionAndIncarnation<LocationMutListIncarnation>(
                rootIncarnation.version, newIncarnation);

      }
      effectsLocationMutListRemoveEffect.Add(effect);
    }
       
    public void AddLocationMutListObserver(int id, ILocationMutListEffectObserver observer) {
      List<ILocationMutListEffectObserver> obsies;
      if (!observersForLocationMutList.TryGetValue(id, out obsies)) {
        obsies = new List<ILocationMutListEffectObserver>();
      }
      obsies.Add(observer);
      observersForLocationMutList[id] = obsies;
    }

    public void RemoveLocationMutListObserver(int id, ILocationMutListEffectObserver observer) {
      if (observersForLocationMutList.ContainsKey(id)) {
        var list = observersForLocationMutList[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForLocationMutList.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

  public void BroadcastLocationMutListEffects(
      SortedDictionary<int, List<ILocationMutListEffectObserver>> observers) {
    foreach (var effect in effectsLocationMutListDeleteEffect) {
      if (observers.TryGetValue(0, out List<ILocationMutListEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnLocationMutListEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ILocationMutListEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnLocationMutListEffect(effect);
        }
        observersForLocationMutList.Remove(effect.id);
      }
    }
    effectsLocationMutListDeleteEffect.Clear();

    foreach (var effect in effectsLocationMutListAddEffect) {
      if (observers.TryGetValue(0, out List<ILocationMutListEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnLocationMutListEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ILocationMutListEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnLocationMutListEffect(effect);
        }
      }
    }
    effectsLocationMutListAddEffect.Clear();

    foreach (var effect in effectsLocationMutListRemoveEffect) {
      if (observers.TryGetValue(0, out List<ILocationMutListEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnLocationMutListEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ILocationMutListEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnLocationMutListEffect(effect);
        }
      }
    }
    effectsLocationMutListRemoveEffect.Clear();

    foreach (var effect in effectsLocationMutListCreateEffect) {
      if (observers.TryGetValue(0, out List<ILocationMutListEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnLocationMutListEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ILocationMutListEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnLocationMutListEffect(effect);
        }
      }
    }
    effectsLocationMutListCreateEffect.Clear();

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
        throw new System.Exception("Invalid LevelMutSet}: " + id);
      }
    }
    public LevelMutSet EffectLevelMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new LevelMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateLevelMutSet(id, rootIncarnation.version, incarnation);
      return new LevelMutSet(this, id);
    }
    public void EffectInternalCreateLevelMutSet(int id, int incarnationVersion, LevelMutSetIncarnation incarnation) {
      var effect = new LevelMutSetCreateEffect(id);
      rootIncarnation.incarnationsLevelMutSet
          .Add(
              id,
              new VersionAndIncarnation<LevelMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsLevelMutSetCreateEffect.Add(effect);
    }
    public void EffectLevelMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new LevelMutSetDeleteEffect(id);
      effectsLevelMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsLevelMutSet[id];
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
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new LevelMutSetIncarnation(newMap);
        rootIncarnation.incarnationsLevelMutSet[setId] =
            new VersionAndIncarnation<LevelMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsLevelMutSetAddEffect.Add(effect);
    }
    public void EffectLevelMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasLevelMutSet(setId);
      CheckHasLevel(elementId);

      var effect = new LevelMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsLevelMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new LevelMutSetIncarnation(newMap);
        rootIncarnation.incarnationsLevelMutSet[setId] =
            new VersionAndIncarnation<LevelMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsLevelMutSetRemoveEffect.Add(effect);
    }

       
    public void AddLevelMutSetObserver(int id, ILevelMutSetEffectObserver observer) {
      List<ILevelMutSetEffectObserver> obsies;
      if (!observersForLevelMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ILevelMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForLevelMutSet[id] = obsies;
    }

    public void RemoveLevelMutSetObserver(int id, ILevelMutSetEffectObserver observer) {
      if (observersForLevelMutSet.ContainsKey(id)) {
        var list = observersForLevelMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForLevelMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastLevelMutSetEffects(
      SortedDictionary<int, List<ILevelMutSetEffectObserver>> observers) {
    foreach (var effect in effectsLevelMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<ILevelMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnLevelMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ILevelMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnLevelMutSetEffect(effect);
        }
        observersForLevelMutSet.Remove(effect.id);
      }
    }
    effectsLevelMutSetDeleteEffect.Clear();

    foreach (var effect in effectsLevelMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<ILevelMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnLevelMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ILevelMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnLevelMutSetEffect(effect);
        }
      }
    }
    effectsLevelMutSetAddEffect.Clear();

    foreach (var effect in effectsLevelMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<ILevelMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnLevelMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ILevelMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnLevelMutSetEffect(effect);
        }
      }
    }
    effectsLevelMutSetRemoveEffect.Clear();

    foreach (var effect in effectsLevelMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<ILevelMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnLevelMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ILevelMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnLevelMutSetEffect(effect);
        }
      }
    }
    effectsLevelMutSetCreateEffect.Clear();

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
        throw new System.Exception("Invalid ShieldingUCWeakMutSet}: " + id);
      }
    }
    public ShieldingUCWeakMutSet EffectShieldingUCWeakMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new ShieldingUCWeakMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateShieldingUCWeakMutSet(id, rootIncarnation.version, incarnation);
      return new ShieldingUCWeakMutSet(this, id);
    }
    public void EffectInternalCreateShieldingUCWeakMutSet(int id, int incarnationVersion, ShieldingUCWeakMutSetIncarnation incarnation) {
      var effect = new ShieldingUCWeakMutSetCreateEffect(id);
      rootIncarnation.incarnationsShieldingUCWeakMutSet
          .Add(
              id,
              new VersionAndIncarnation<ShieldingUCWeakMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsShieldingUCWeakMutSetCreateEffect.Add(effect);
    }
    public void EffectShieldingUCWeakMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new ShieldingUCWeakMutSetDeleteEffect(id);
      effectsShieldingUCWeakMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsShieldingUCWeakMutSet[id];
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
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new ShieldingUCWeakMutSetIncarnation(newMap);
        rootIncarnation.incarnationsShieldingUCWeakMutSet[setId] =
            new VersionAndIncarnation<ShieldingUCWeakMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsShieldingUCWeakMutSetAddEffect.Add(effect);
    }
    public void EffectShieldingUCWeakMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasShieldingUCWeakMutSet(setId);
      CheckHasShieldingUC(elementId);

      var effect = new ShieldingUCWeakMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsShieldingUCWeakMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new ShieldingUCWeakMutSetIncarnation(newMap);
        rootIncarnation.incarnationsShieldingUCWeakMutSet[setId] =
            new VersionAndIncarnation<ShieldingUCWeakMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsShieldingUCWeakMutSetRemoveEffect.Add(effect);
    }

       
    public void AddShieldingUCWeakMutSetObserver(int id, IShieldingUCWeakMutSetEffectObserver observer) {
      List<IShieldingUCWeakMutSetEffectObserver> obsies;
      if (!observersForShieldingUCWeakMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IShieldingUCWeakMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForShieldingUCWeakMutSet[id] = obsies;
    }

    public void RemoveShieldingUCWeakMutSetObserver(int id, IShieldingUCWeakMutSetEffectObserver observer) {
      if (observersForShieldingUCWeakMutSet.ContainsKey(id)) {
        var list = observersForShieldingUCWeakMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForShieldingUCWeakMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastShieldingUCWeakMutSetEffects(
      SortedDictionary<int, List<IShieldingUCWeakMutSetEffectObserver>> observers) {
    foreach (var effect in effectsShieldingUCWeakMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<IShieldingUCWeakMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnShieldingUCWeakMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IShieldingUCWeakMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnShieldingUCWeakMutSetEffect(effect);
        }
        observersForShieldingUCWeakMutSet.Remove(effect.id);
      }
    }
    effectsShieldingUCWeakMutSetDeleteEffect.Clear();

    foreach (var effect in effectsShieldingUCWeakMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<IShieldingUCWeakMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnShieldingUCWeakMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IShieldingUCWeakMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnShieldingUCWeakMutSetEffect(effect);
        }
      }
    }
    effectsShieldingUCWeakMutSetAddEffect.Clear();

    foreach (var effect in effectsShieldingUCWeakMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<IShieldingUCWeakMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnShieldingUCWeakMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IShieldingUCWeakMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnShieldingUCWeakMutSetEffect(effect);
        }
      }
    }
    effectsShieldingUCWeakMutSetRemoveEffect.Clear();

    foreach (var effect in effectsShieldingUCWeakMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<IShieldingUCWeakMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnShieldingUCWeakMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IShieldingUCWeakMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnShieldingUCWeakMutSetEffect(effect);
        }
      }
    }
    effectsShieldingUCWeakMutSetCreateEffect.Clear();

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
        throw new System.Exception("Invalid AttackAICapabilityUCWeakMutSet}: " + id);
      }
    }
    public AttackAICapabilityUCWeakMutSet EffectAttackAICapabilityUCWeakMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new AttackAICapabilityUCWeakMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateAttackAICapabilityUCWeakMutSet(id, rootIncarnation.version, incarnation);
      return new AttackAICapabilityUCWeakMutSet(this, id);
    }
    public void EffectInternalCreateAttackAICapabilityUCWeakMutSet(int id, int incarnationVersion, AttackAICapabilityUCWeakMutSetIncarnation incarnation) {
      var effect = new AttackAICapabilityUCWeakMutSetCreateEffect(id);
      rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet
          .Add(
              id,
              new VersionAndIncarnation<AttackAICapabilityUCWeakMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsAttackAICapabilityUCWeakMutSetCreateEffect.Add(effect);
    }
    public void EffectAttackAICapabilityUCWeakMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new AttackAICapabilityUCWeakMutSetDeleteEffect(id);
      effectsAttackAICapabilityUCWeakMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet[id];
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
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new AttackAICapabilityUCWeakMutSetIncarnation(newMap);
        rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet[setId] =
            new VersionAndIncarnation<AttackAICapabilityUCWeakMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsAttackAICapabilityUCWeakMutSetAddEffect.Add(effect);
    }
    public void EffectAttackAICapabilityUCWeakMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasAttackAICapabilityUCWeakMutSet(setId);
      CheckHasAttackAICapabilityUC(elementId);

      var effect = new AttackAICapabilityUCWeakMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new AttackAICapabilityUCWeakMutSetIncarnation(newMap);
        rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet[setId] =
            new VersionAndIncarnation<AttackAICapabilityUCWeakMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsAttackAICapabilityUCWeakMutSetRemoveEffect.Add(effect);
    }

       
    public void AddAttackAICapabilityUCWeakMutSetObserver(int id, IAttackAICapabilityUCWeakMutSetEffectObserver observer) {
      List<IAttackAICapabilityUCWeakMutSetEffectObserver> obsies;
      if (!observersForAttackAICapabilityUCWeakMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IAttackAICapabilityUCWeakMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForAttackAICapabilityUCWeakMutSet[id] = obsies;
    }

    public void RemoveAttackAICapabilityUCWeakMutSetObserver(int id, IAttackAICapabilityUCWeakMutSetEffectObserver observer) {
      if (observersForAttackAICapabilityUCWeakMutSet.ContainsKey(id)) {
        var list = observersForAttackAICapabilityUCWeakMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForAttackAICapabilityUCWeakMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastAttackAICapabilityUCWeakMutSetEffects(
      SortedDictionary<int, List<IAttackAICapabilityUCWeakMutSetEffectObserver>> observers) {
    foreach (var effect in effectsAttackAICapabilityUCWeakMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<IAttackAICapabilityUCWeakMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnAttackAICapabilityUCWeakMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IAttackAICapabilityUCWeakMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnAttackAICapabilityUCWeakMutSetEffect(effect);
        }
        observersForAttackAICapabilityUCWeakMutSet.Remove(effect.id);
      }
    }
    effectsAttackAICapabilityUCWeakMutSetDeleteEffect.Clear();

    foreach (var effect in effectsAttackAICapabilityUCWeakMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<IAttackAICapabilityUCWeakMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnAttackAICapabilityUCWeakMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IAttackAICapabilityUCWeakMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnAttackAICapabilityUCWeakMutSetEffect(effect);
        }
      }
    }
    effectsAttackAICapabilityUCWeakMutSetAddEffect.Clear();

    foreach (var effect in effectsAttackAICapabilityUCWeakMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<IAttackAICapabilityUCWeakMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnAttackAICapabilityUCWeakMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IAttackAICapabilityUCWeakMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnAttackAICapabilityUCWeakMutSetEffect(effect);
        }
      }
    }
    effectsAttackAICapabilityUCWeakMutSetRemoveEffect.Clear();

    foreach (var effect in effectsAttackAICapabilityUCWeakMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<IAttackAICapabilityUCWeakMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnAttackAICapabilityUCWeakMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IAttackAICapabilityUCWeakMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnAttackAICapabilityUCWeakMutSetEffect(effect);
        }
      }
    }
    effectsAttackAICapabilityUCWeakMutSetCreateEffect.Clear();

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
        throw new System.Exception("Invalid KillDirectiveUCMutSet}: " + id);
      }
    }
    public KillDirectiveUCMutSet EffectKillDirectiveUCMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new KillDirectiveUCMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateKillDirectiveUCMutSet(id, rootIncarnation.version, incarnation);
      return new KillDirectiveUCMutSet(this, id);
    }
    public void EffectInternalCreateKillDirectiveUCMutSet(int id, int incarnationVersion, KillDirectiveUCMutSetIncarnation incarnation) {
      var effect = new KillDirectiveUCMutSetCreateEffect(id);
      rootIncarnation.incarnationsKillDirectiveUCMutSet
          .Add(
              id,
              new VersionAndIncarnation<KillDirectiveUCMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsKillDirectiveUCMutSetCreateEffect.Add(effect);
    }
    public void EffectKillDirectiveUCMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new KillDirectiveUCMutSetDeleteEffect(id);
      effectsKillDirectiveUCMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsKillDirectiveUCMutSet[id];
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
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new KillDirectiveUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsKillDirectiveUCMutSet[setId] =
            new VersionAndIncarnation<KillDirectiveUCMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsKillDirectiveUCMutSetAddEffect.Add(effect);
    }
    public void EffectKillDirectiveUCMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasKillDirectiveUCMutSet(setId);
      CheckHasKillDirectiveUC(elementId);

      var effect = new KillDirectiveUCMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsKillDirectiveUCMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new KillDirectiveUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsKillDirectiveUCMutSet[setId] =
            new VersionAndIncarnation<KillDirectiveUCMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsKillDirectiveUCMutSetRemoveEffect.Add(effect);
    }

       
    public void AddKillDirectiveUCMutSetObserver(int id, IKillDirectiveUCMutSetEffectObserver observer) {
      List<IKillDirectiveUCMutSetEffectObserver> obsies;
      if (!observersForKillDirectiveUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IKillDirectiveUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForKillDirectiveUCMutSet[id] = obsies;
    }

    public void RemoveKillDirectiveUCMutSetObserver(int id, IKillDirectiveUCMutSetEffectObserver observer) {
      if (observersForKillDirectiveUCMutSet.ContainsKey(id)) {
        var list = observersForKillDirectiveUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForKillDirectiveUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastKillDirectiveUCMutSetEffects(
      SortedDictionary<int, List<IKillDirectiveUCMutSetEffectObserver>> observers) {
    foreach (var effect in effectsKillDirectiveUCMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<IKillDirectiveUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnKillDirectiveUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IKillDirectiveUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnKillDirectiveUCMutSetEffect(effect);
        }
        observersForKillDirectiveUCMutSet.Remove(effect.id);
      }
    }
    effectsKillDirectiveUCMutSetDeleteEffect.Clear();

    foreach (var effect in effectsKillDirectiveUCMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<IKillDirectiveUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnKillDirectiveUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IKillDirectiveUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnKillDirectiveUCMutSetEffect(effect);
        }
      }
    }
    effectsKillDirectiveUCMutSetAddEffect.Clear();

    foreach (var effect in effectsKillDirectiveUCMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<IKillDirectiveUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnKillDirectiveUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IKillDirectiveUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnKillDirectiveUCMutSetEffect(effect);
        }
      }
    }
    effectsKillDirectiveUCMutSetRemoveEffect.Clear();

    foreach (var effect in effectsKillDirectiveUCMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<IKillDirectiveUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnKillDirectiveUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IKillDirectiveUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnKillDirectiveUCMutSetEffect(effect);
        }
      }
    }
    effectsKillDirectiveUCMutSetCreateEffect.Clear();

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
        throw new System.Exception("Invalid MoveDirectiveUCMutSet}: " + id);
      }
    }
    public MoveDirectiveUCMutSet EffectMoveDirectiveUCMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new MoveDirectiveUCMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateMoveDirectiveUCMutSet(id, rootIncarnation.version, incarnation);
      return new MoveDirectiveUCMutSet(this, id);
    }
    public void EffectInternalCreateMoveDirectiveUCMutSet(int id, int incarnationVersion, MoveDirectiveUCMutSetIncarnation incarnation) {
      var effect = new MoveDirectiveUCMutSetCreateEffect(id);
      rootIncarnation.incarnationsMoveDirectiveUCMutSet
          .Add(
              id,
              new VersionAndIncarnation<MoveDirectiveUCMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsMoveDirectiveUCMutSetCreateEffect.Add(effect);
    }
    public void EffectMoveDirectiveUCMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new MoveDirectiveUCMutSetDeleteEffect(id);
      effectsMoveDirectiveUCMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsMoveDirectiveUCMutSet[id];
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
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new MoveDirectiveUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsMoveDirectiveUCMutSet[setId] =
            new VersionAndIncarnation<MoveDirectiveUCMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsMoveDirectiveUCMutSetAddEffect.Add(effect);
    }
    public void EffectMoveDirectiveUCMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasMoveDirectiveUCMutSet(setId);
      CheckHasMoveDirectiveUC(elementId);

      var effect = new MoveDirectiveUCMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsMoveDirectiveUCMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new MoveDirectiveUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsMoveDirectiveUCMutSet[setId] =
            new VersionAndIncarnation<MoveDirectiveUCMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsMoveDirectiveUCMutSetRemoveEffect.Add(effect);
    }

       
    public void AddMoveDirectiveUCMutSetObserver(int id, IMoveDirectiveUCMutSetEffectObserver observer) {
      List<IMoveDirectiveUCMutSetEffectObserver> obsies;
      if (!observersForMoveDirectiveUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IMoveDirectiveUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForMoveDirectiveUCMutSet[id] = obsies;
    }

    public void RemoveMoveDirectiveUCMutSetObserver(int id, IMoveDirectiveUCMutSetEffectObserver observer) {
      if (observersForMoveDirectiveUCMutSet.ContainsKey(id)) {
        var list = observersForMoveDirectiveUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForMoveDirectiveUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastMoveDirectiveUCMutSetEffects(
      SortedDictionary<int, List<IMoveDirectiveUCMutSetEffectObserver>> observers) {
    foreach (var effect in effectsMoveDirectiveUCMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<IMoveDirectiveUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnMoveDirectiveUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IMoveDirectiveUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnMoveDirectiveUCMutSetEffect(effect);
        }
        observersForMoveDirectiveUCMutSet.Remove(effect.id);
      }
    }
    effectsMoveDirectiveUCMutSetDeleteEffect.Clear();

    foreach (var effect in effectsMoveDirectiveUCMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<IMoveDirectiveUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnMoveDirectiveUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IMoveDirectiveUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnMoveDirectiveUCMutSetEffect(effect);
        }
      }
    }
    effectsMoveDirectiveUCMutSetAddEffect.Clear();

    foreach (var effect in effectsMoveDirectiveUCMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<IMoveDirectiveUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnMoveDirectiveUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IMoveDirectiveUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnMoveDirectiveUCMutSetEffect(effect);
        }
      }
    }
    effectsMoveDirectiveUCMutSetRemoveEffect.Clear();

    foreach (var effect in effectsMoveDirectiveUCMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<IMoveDirectiveUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnMoveDirectiveUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IMoveDirectiveUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnMoveDirectiveUCMutSetEffect(effect);
        }
      }
    }
    effectsMoveDirectiveUCMutSetCreateEffect.Clear();

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
        throw new System.Exception("Invalid WanderAICapabilityUCMutSet}: " + id);
      }
    }
    public WanderAICapabilityUCMutSet EffectWanderAICapabilityUCMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new WanderAICapabilityUCMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateWanderAICapabilityUCMutSet(id, rootIncarnation.version, incarnation);
      return new WanderAICapabilityUCMutSet(this, id);
    }
    public void EffectInternalCreateWanderAICapabilityUCMutSet(int id, int incarnationVersion, WanderAICapabilityUCMutSetIncarnation incarnation) {
      var effect = new WanderAICapabilityUCMutSetCreateEffect(id);
      rootIncarnation.incarnationsWanderAICapabilityUCMutSet
          .Add(
              id,
              new VersionAndIncarnation<WanderAICapabilityUCMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsWanderAICapabilityUCMutSetCreateEffect.Add(effect);
    }
    public void EffectWanderAICapabilityUCMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new WanderAICapabilityUCMutSetDeleteEffect(id);
      effectsWanderAICapabilityUCMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsWanderAICapabilityUCMutSet[id];
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
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new WanderAICapabilityUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsWanderAICapabilityUCMutSet[setId] =
            new VersionAndIncarnation<WanderAICapabilityUCMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsWanderAICapabilityUCMutSetAddEffect.Add(effect);
    }
    public void EffectWanderAICapabilityUCMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasWanderAICapabilityUCMutSet(setId);
      CheckHasWanderAICapabilityUC(elementId);

      var effect = new WanderAICapabilityUCMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsWanderAICapabilityUCMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new WanderAICapabilityUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsWanderAICapabilityUCMutSet[setId] =
            new VersionAndIncarnation<WanderAICapabilityUCMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsWanderAICapabilityUCMutSetRemoveEffect.Add(effect);
    }

       
    public void AddWanderAICapabilityUCMutSetObserver(int id, IWanderAICapabilityUCMutSetEffectObserver observer) {
      List<IWanderAICapabilityUCMutSetEffectObserver> obsies;
      if (!observersForWanderAICapabilityUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IWanderAICapabilityUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForWanderAICapabilityUCMutSet[id] = obsies;
    }

    public void RemoveWanderAICapabilityUCMutSetObserver(int id, IWanderAICapabilityUCMutSetEffectObserver observer) {
      if (observersForWanderAICapabilityUCMutSet.ContainsKey(id)) {
        var list = observersForWanderAICapabilityUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForWanderAICapabilityUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastWanderAICapabilityUCMutSetEffects(
      SortedDictionary<int, List<IWanderAICapabilityUCMutSetEffectObserver>> observers) {
    foreach (var effect in effectsWanderAICapabilityUCMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<IWanderAICapabilityUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnWanderAICapabilityUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IWanderAICapabilityUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnWanderAICapabilityUCMutSetEffect(effect);
        }
        observersForWanderAICapabilityUCMutSet.Remove(effect.id);
      }
    }
    effectsWanderAICapabilityUCMutSetDeleteEffect.Clear();

    foreach (var effect in effectsWanderAICapabilityUCMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<IWanderAICapabilityUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnWanderAICapabilityUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IWanderAICapabilityUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnWanderAICapabilityUCMutSetEffect(effect);
        }
      }
    }
    effectsWanderAICapabilityUCMutSetAddEffect.Clear();

    foreach (var effect in effectsWanderAICapabilityUCMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<IWanderAICapabilityUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnWanderAICapabilityUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IWanderAICapabilityUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnWanderAICapabilityUCMutSetEffect(effect);
        }
      }
    }
    effectsWanderAICapabilityUCMutSetRemoveEffect.Clear();

    foreach (var effect in effectsWanderAICapabilityUCMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<IWanderAICapabilityUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnWanderAICapabilityUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IWanderAICapabilityUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnWanderAICapabilityUCMutSetEffect(effect);
        }
      }
    }
    effectsWanderAICapabilityUCMutSetCreateEffect.Clear();

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
        throw new System.Exception("Invalid BideAICapabilityUCMutSet}: " + id);
      }
    }
    public BideAICapabilityUCMutSet EffectBideAICapabilityUCMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new BideAICapabilityUCMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateBideAICapabilityUCMutSet(id, rootIncarnation.version, incarnation);
      return new BideAICapabilityUCMutSet(this, id);
    }
    public void EffectInternalCreateBideAICapabilityUCMutSet(int id, int incarnationVersion, BideAICapabilityUCMutSetIncarnation incarnation) {
      var effect = new BideAICapabilityUCMutSetCreateEffect(id);
      rootIncarnation.incarnationsBideAICapabilityUCMutSet
          .Add(
              id,
              new VersionAndIncarnation<BideAICapabilityUCMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsBideAICapabilityUCMutSetCreateEffect.Add(effect);
    }
    public void EffectBideAICapabilityUCMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new BideAICapabilityUCMutSetDeleteEffect(id);
      effectsBideAICapabilityUCMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsBideAICapabilityUCMutSet[id];
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
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new BideAICapabilityUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsBideAICapabilityUCMutSet[setId] =
            new VersionAndIncarnation<BideAICapabilityUCMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsBideAICapabilityUCMutSetAddEffect.Add(effect);
    }
    public void EffectBideAICapabilityUCMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasBideAICapabilityUCMutSet(setId);
      CheckHasBideAICapabilityUC(elementId);

      var effect = new BideAICapabilityUCMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsBideAICapabilityUCMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new BideAICapabilityUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsBideAICapabilityUCMutSet[setId] =
            new VersionAndIncarnation<BideAICapabilityUCMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsBideAICapabilityUCMutSetRemoveEffect.Add(effect);
    }

       
    public void AddBideAICapabilityUCMutSetObserver(int id, IBideAICapabilityUCMutSetEffectObserver observer) {
      List<IBideAICapabilityUCMutSetEffectObserver> obsies;
      if (!observersForBideAICapabilityUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IBideAICapabilityUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForBideAICapabilityUCMutSet[id] = obsies;
    }

    public void RemoveBideAICapabilityUCMutSetObserver(int id, IBideAICapabilityUCMutSetEffectObserver observer) {
      if (observersForBideAICapabilityUCMutSet.ContainsKey(id)) {
        var list = observersForBideAICapabilityUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForBideAICapabilityUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastBideAICapabilityUCMutSetEffects(
      SortedDictionary<int, List<IBideAICapabilityUCMutSetEffectObserver>> observers) {
    foreach (var effect in effectsBideAICapabilityUCMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<IBideAICapabilityUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnBideAICapabilityUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IBideAICapabilityUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnBideAICapabilityUCMutSetEffect(effect);
        }
        observersForBideAICapabilityUCMutSet.Remove(effect.id);
      }
    }
    effectsBideAICapabilityUCMutSetDeleteEffect.Clear();

    foreach (var effect in effectsBideAICapabilityUCMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<IBideAICapabilityUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnBideAICapabilityUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IBideAICapabilityUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnBideAICapabilityUCMutSetEffect(effect);
        }
      }
    }
    effectsBideAICapabilityUCMutSetAddEffect.Clear();

    foreach (var effect in effectsBideAICapabilityUCMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<IBideAICapabilityUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnBideAICapabilityUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IBideAICapabilityUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnBideAICapabilityUCMutSetEffect(effect);
        }
      }
    }
    effectsBideAICapabilityUCMutSetRemoveEffect.Clear();

    foreach (var effect in effectsBideAICapabilityUCMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<IBideAICapabilityUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnBideAICapabilityUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IBideAICapabilityUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnBideAICapabilityUCMutSetEffect(effect);
        }
      }
    }
    effectsBideAICapabilityUCMutSetCreateEffect.Clear();

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
        throw new System.Exception("Invalid AttackAICapabilityUCMutSet}: " + id);
      }
    }
    public AttackAICapabilityUCMutSet EffectAttackAICapabilityUCMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new AttackAICapabilityUCMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateAttackAICapabilityUCMutSet(id, rootIncarnation.version, incarnation);
      return new AttackAICapabilityUCMutSet(this, id);
    }
    public void EffectInternalCreateAttackAICapabilityUCMutSet(int id, int incarnationVersion, AttackAICapabilityUCMutSetIncarnation incarnation) {
      var effect = new AttackAICapabilityUCMutSetCreateEffect(id);
      rootIncarnation.incarnationsAttackAICapabilityUCMutSet
          .Add(
              id,
              new VersionAndIncarnation<AttackAICapabilityUCMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsAttackAICapabilityUCMutSetCreateEffect.Add(effect);
    }
    public void EffectAttackAICapabilityUCMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new AttackAICapabilityUCMutSetDeleteEffect(id);
      effectsAttackAICapabilityUCMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsAttackAICapabilityUCMutSet[id];
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
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new AttackAICapabilityUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsAttackAICapabilityUCMutSet[setId] =
            new VersionAndIncarnation<AttackAICapabilityUCMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsAttackAICapabilityUCMutSetAddEffect.Add(effect);
    }
    public void EffectAttackAICapabilityUCMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasAttackAICapabilityUCMutSet(setId);
      CheckHasAttackAICapabilityUC(elementId);

      var effect = new AttackAICapabilityUCMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsAttackAICapabilityUCMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new AttackAICapabilityUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsAttackAICapabilityUCMutSet[setId] =
            new VersionAndIncarnation<AttackAICapabilityUCMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsAttackAICapabilityUCMutSetRemoveEffect.Add(effect);
    }

       
    public void AddAttackAICapabilityUCMutSetObserver(int id, IAttackAICapabilityUCMutSetEffectObserver observer) {
      List<IAttackAICapabilityUCMutSetEffectObserver> obsies;
      if (!observersForAttackAICapabilityUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IAttackAICapabilityUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForAttackAICapabilityUCMutSet[id] = obsies;
    }

    public void RemoveAttackAICapabilityUCMutSetObserver(int id, IAttackAICapabilityUCMutSetEffectObserver observer) {
      if (observersForAttackAICapabilityUCMutSet.ContainsKey(id)) {
        var list = observersForAttackAICapabilityUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForAttackAICapabilityUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastAttackAICapabilityUCMutSetEffects(
      SortedDictionary<int, List<IAttackAICapabilityUCMutSetEffectObserver>> observers) {
    foreach (var effect in effectsAttackAICapabilityUCMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<IAttackAICapabilityUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnAttackAICapabilityUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IAttackAICapabilityUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnAttackAICapabilityUCMutSetEffect(effect);
        }
        observersForAttackAICapabilityUCMutSet.Remove(effect.id);
      }
    }
    effectsAttackAICapabilityUCMutSetDeleteEffect.Clear();

    foreach (var effect in effectsAttackAICapabilityUCMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<IAttackAICapabilityUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnAttackAICapabilityUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IAttackAICapabilityUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnAttackAICapabilityUCMutSetEffect(effect);
        }
      }
    }
    effectsAttackAICapabilityUCMutSetAddEffect.Clear();

    foreach (var effect in effectsAttackAICapabilityUCMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<IAttackAICapabilityUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnAttackAICapabilityUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IAttackAICapabilityUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnAttackAICapabilityUCMutSetEffect(effect);
        }
      }
    }
    effectsAttackAICapabilityUCMutSetRemoveEffect.Clear();

    foreach (var effect in effectsAttackAICapabilityUCMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<IAttackAICapabilityUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnAttackAICapabilityUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IAttackAICapabilityUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnAttackAICapabilityUCMutSetEffect(effect);
        }
      }
    }
    effectsAttackAICapabilityUCMutSetCreateEffect.Clear();

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
        throw new System.Exception("Invalid ShieldingUCMutSet}: " + id);
      }
    }
    public ShieldingUCMutSet EffectShieldingUCMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new ShieldingUCMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateShieldingUCMutSet(id, rootIncarnation.version, incarnation);
      return new ShieldingUCMutSet(this, id);
    }
    public void EffectInternalCreateShieldingUCMutSet(int id, int incarnationVersion, ShieldingUCMutSetIncarnation incarnation) {
      var effect = new ShieldingUCMutSetCreateEffect(id);
      rootIncarnation.incarnationsShieldingUCMutSet
          .Add(
              id,
              new VersionAndIncarnation<ShieldingUCMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsShieldingUCMutSetCreateEffect.Add(effect);
    }
    public void EffectShieldingUCMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new ShieldingUCMutSetDeleteEffect(id);
      effectsShieldingUCMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsShieldingUCMutSet[id];
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
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new ShieldingUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsShieldingUCMutSet[setId] =
            new VersionAndIncarnation<ShieldingUCMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsShieldingUCMutSetAddEffect.Add(effect);
    }
    public void EffectShieldingUCMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasShieldingUCMutSet(setId);
      CheckHasShieldingUC(elementId);

      var effect = new ShieldingUCMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsShieldingUCMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new ShieldingUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsShieldingUCMutSet[setId] =
            new VersionAndIncarnation<ShieldingUCMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsShieldingUCMutSetRemoveEffect.Add(effect);
    }

       
    public void AddShieldingUCMutSetObserver(int id, IShieldingUCMutSetEffectObserver observer) {
      List<IShieldingUCMutSetEffectObserver> obsies;
      if (!observersForShieldingUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IShieldingUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForShieldingUCMutSet[id] = obsies;
    }

    public void RemoveShieldingUCMutSetObserver(int id, IShieldingUCMutSetEffectObserver observer) {
      if (observersForShieldingUCMutSet.ContainsKey(id)) {
        var list = observersForShieldingUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForShieldingUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastShieldingUCMutSetEffects(
      SortedDictionary<int, List<IShieldingUCMutSetEffectObserver>> observers) {
    foreach (var effect in effectsShieldingUCMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<IShieldingUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnShieldingUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IShieldingUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnShieldingUCMutSetEffect(effect);
        }
        observersForShieldingUCMutSet.Remove(effect.id);
      }
    }
    effectsShieldingUCMutSetDeleteEffect.Clear();

    foreach (var effect in effectsShieldingUCMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<IShieldingUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnShieldingUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IShieldingUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnShieldingUCMutSetEffect(effect);
        }
      }
    }
    effectsShieldingUCMutSetAddEffect.Clear();

    foreach (var effect in effectsShieldingUCMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<IShieldingUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnShieldingUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IShieldingUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnShieldingUCMutSetEffect(effect);
        }
      }
    }
    effectsShieldingUCMutSetRemoveEffect.Clear();

    foreach (var effect in effectsShieldingUCMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<IShieldingUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnShieldingUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IShieldingUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnShieldingUCMutSetEffect(effect);
        }
      }
    }
    effectsShieldingUCMutSetCreateEffect.Clear();

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
        throw new System.Exception("Invalid BidingOperationUCMutSet}: " + id);
      }
    }
    public BidingOperationUCMutSet EffectBidingOperationUCMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new BidingOperationUCMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateBidingOperationUCMutSet(id, rootIncarnation.version, incarnation);
      return new BidingOperationUCMutSet(this, id);
    }
    public void EffectInternalCreateBidingOperationUCMutSet(int id, int incarnationVersion, BidingOperationUCMutSetIncarnation incarnation) {
      var effect = new BidingOperationUCMutSetCreateEffect(id);
      rootIncarnation.incarnationsBidingOperationUCMutSet
          .Add(
              id,
              new VersionAndIncarnation<BidingOperationUCMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsBidingOperationUCMutSetCreateEffect.Add(effect);
    }
    public void EffectBidingOperationUCMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new BidingOperationUCMutSetDeleteEffect(id);
      effectsBidingOperationUCMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsBidingOperationUCMutSet[id];
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
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new BidingOperationUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsBidingOperationUCMutSet[setId] =
            new VersionAndIncarnation<BidingOperationUCMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsBidingOperationUCMutSetAddEffect.Add(effect);
    }
    public void EffectBidingOperationUCMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasBidingOperationUCMutSet(setId);
      CheckHasBidingOperationUC(elementId);

      var effect = new BidingOperationUCMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsBidingOperationUCMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new BidingOperationUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsBidingOperationUCMutSet[setId] =
            new VersionAndIncarnation<BidingOperationUCMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsBidingOperationUCMutSetRemoveEffect.Add(effect);
    }

       
    public void AddBidingOperationUCMutSetObserver(int id, IBidingOperationUCMutSetEffectObserver observer) {
      List<IBidingOperationUCMutSetEffectObserver> obsies;
      if (!observersForBidingOperationUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IBidingOperationUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForBidingOperationUCMutSet[id] = obsies;
    }

    public void RemoveBidingOperationUCMutSetObserver(int id, IBidingOperationUCMutSetEffectObserver observer) {
      if (observersForBidingOperationUCMutSet.ContainsKey(id)) {
        var list = observersForBidingOperationUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForBidingOperationUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastBidingOperationUCMutSetEffects(
      SortedDictionary<int, List<IBidingOperationUCMutSetEffectObserver>> observers) {
    foreach (var effect in effectsBidingOperationUCMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<IBidingOperationUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnBidingOperationUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IBidingOperationUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnBidingOperationUCMutSetEffect(effect);
        }
        observersForBidingOperationUCMutSet.Remove(effect.id);
      }
    }
    effectsBidingOperationUCMutSetDeleteEffect.Clear();

    foreach (var effect in effectsBidingOperationUCMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<IBidingOperationUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnBidingOperationUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IBidingOperationUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnBidingOperationUCMutSetEffect(effect);
        }
      }
    }
    effectsBidingOperationUCMutSetAddEffect.Clear();

    foreach (var effect in effectsBidingOperationUCMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<IBidingOperationUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnBidingOperationUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IBidingOperationUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnBidingOperationUCMutSetEffect(effect);
        }
      }
    }
    effectsBidingOperationUCMutSetRemoveEffect.Clear();

    foreach (var effect in effectsBidingOperationUCMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<IBidingOperationUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnBidingOperationUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IBidingOperationUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnBidingOperationUCMutSetEffect(effect);
        }
      }
    }
    effectsBidingOperationUCMutSetCreateEffect.Clear();

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
        throw new System.Exception("Invalid GlaiveMutSet}: " + id);
      }
    }
    public GlaiveMutSet EffectGlaiveMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new GlaiveMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateGlaiveMutSet(id, rootIncarnation.version, incarnation);
      return new GlaiveMutSet(this, id);
    }
    public void EffectInternalCreateGlaiveMutSet(int id, int incarnationVersion, GlaiveMutSetIncarnation incarnation) {
      var effect = new GlaiveMutSetCreateEffect(id);
      rootIncarnation.incarnationsGlaiveMutSet
          .Add(
              id,
              new VersionAndIncarnation<GlaiveMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsGlaiveMutSetCreateEffect.Add(effect);
    }
    public void EffectGlaiveMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new GlaiveMutSetDeleteEffect(id);
      effectsGlaiveMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsGlaiveMutSet[id];
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
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new GlaiveMutSetIncarnation(newMap);
        rootIncarnation.incarnationsGlaiveMutSet[setId] =
            new VersionAndIncarnation<GlaiveMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsGlaiveMutSetAddEffect.Add(effect);
    }
    public void EffectGlaiveMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasGlaiveMutSet(setId);
      CheckHasGlaive(elementId);

      var effect = new GlaiveMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsGlaiveMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new GlaiveMutSetIncarnation(newMap);
        rootIncarnation.incarnationsGlaiveMutSet[setId] =
            new VersionAndIncarnation<GlaiveMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsGlaiveMutSetRemoveEffect.Add(effect);
    }

       
    public void AddGlaiveMutSetObserver(int id, IGlaiveMutSetEffectObserver observer) {
      List<IGlaiveMutSetEffectObserver> obsies;
      if (!observersForGlaiveMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IGlaiveMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForGlaiveMutSet[id] = obsies;
    }

    public void RemoveGlaiveMutSetObserver(int id, IGlaiveMutSetEffectObserver observer) {
      if (observersForGlaiveMutSet.ContainsKey(id)) {
        var list = observersForGlaiveMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForGlaiveMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastGlaiveMutSetEffects(
      SortedDictionary<int, List<IGlaiveMutSetEffectObserver>> observers) {
    foreach (var effect in effectsGlaiveMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<IGlaiveMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnGlaiveMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IGlaiveMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnGlaiveMutSetEffect(effect);
        }
        observersForGlaiveMutSet.Remove(effect.id);
      }
    }
    effectsGlaiveMutSetDeleteEffect.Clear();

    foreach (var effect in effectsGlaiveMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<IGlaiveMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnGlaiveMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IGlaiveMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnGlaiveMutSetEffect(effect);
        }
      }
    }
    effectsGlaiveMutSetAddEffect.Clear();

    foreach (var effect in effectsGlaiveMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<IGlaiveMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnGlaiveMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IGlaiveMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnGlaiveMutSetEffect(effect);
        }
      }
    }
    effectsGlaiveMutSetRemoveEffect.Clear();

    foreach (var effect in effectsGlaiveMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<IGlaiveMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnGlaiveMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IGlaiveMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnGlaiveMutSetEffect(effect);
        }
      }
    }
    effectsGlaiveMutSetCreateEffect.Clear();

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
        throw new System.Exception("Invalid ArmorMutSet}: " + id);
      }
    }
    public ArmorMutSet EffectArmorMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new ArmorMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateArmorMutSet(id, rootIncarnation.version, incarnation);
      return new ArmorMutSet(this, id);
    }
    public void EffectInternalCreateArmorMutSet(int id, int incarnationVersion, ArmorMutSetIncarnation incarnation) {
      var effect = new ArmorMutSetCreateEffect(id);
      rootIncarnation.incarnationsArmorMutSet
          .Add(
              id,
              new VersionAndIncarnation<ArmorMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsArmorMutSetCreateEffect.Add(effect);
    }
    public void EffectArmorMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new ArmorMutSetDeleteEffect(id);
      effectsArmorMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsArmorMutSet[id];
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
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new ArmorMutSetIncarnation(newMap);
        rootIncarnation.incarnationsArmorMutSet[setId] =
            new VersionAndIncarnation<ArmorMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsArmorMutSetAddEffect.Add(effect);
    }
    public void EffectArmorMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasArmorMutSet(setId);
      CheckHasArmor(elementId);

      var effect = new ArmorMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsArmorMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new ArmorMutSetIncarnation(newMap);
        rootIncarnation.incarnationsArmorMutSet[setId] =
            new VersionAndIncarnation<ArmorMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsArmorMutSetRemoveEffect.Add(effect);
    }

       
    public void AddArmorMutSetObserver(int id, IArmorMutSetEffectObserver observer) {
      List<IArmorMutSetEffectObserver> obsies;
      if (!observersForArmorMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IArmorMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForArmorMutSet[id] = obsies;
    }

    public void RemoveArmorMutSetObserver(int id, IArmorMutSetEffectObserver observer) {
      if (observersForArmorMutSet.ContainsKey(id)) {
        var list = observersForArmorMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForArmorMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastArmorMutSetEffects(
      SortedDictionary<int, List<IArmorMutSetEffectObserver>> observers) {
    foreach (var effect in effectsArmorMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<IArmorMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnArmorMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IArmorMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnArmorMutSetEffect(effect);
        }
        observersForArmorMutSet.Remove(effect.id);
      }
    }
    effectsArmorMutSetDeleteEffect.Clear();

    foreach (var effect in effectsArmorMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<IArmorMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnArmorMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IArmorMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnArmorMutSetEffect(effect);
        }
      }
    }
    effectsArmorMutSetAddEffect.Clear();

    foreach (var effect in effectsArmorMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<IArmorMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnArmorMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IArmorMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnArmorMutSetEffect(effect);
        }
      }
    }
    effectsArmorMutSetRemoveEffect.Clear();

    foreach (var effect in effectsArmorMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<IArmorMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnArmorMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IArmorMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnArmorMutSetEffect(effect);
        }
      }
    }
    effectsArmorMutSetCreateEffect.Clear();

  }

    public int GetItemTerrainTileComponentMutSetHash(int id, int version, ItemTerrainTileComponentMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public ItemTerrainTileComponentMutSetIncarnation GetItemTerrainTileComponentMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsItemTerrainTileComponentMutSet[id].incarnation;
    }
    public ItemTerrainTileComponentMutSet GetItemTerrainTileComponentMutSet(int id) {
      return new ItemTerrainTileComponentMutSet(this, id);
    }
    public List<ItemTerrainTileComponentMutSet> AllItemTerrainTileComponentMutSet() {
      List<ItemTerrainTileComponentMutSet> result = new List<ItemTerrainTileComponentMutSet>(rootIncarnation.incarnationsItemTerrainTileComponentMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsItemTerrainTileComponentMutSet.Keys) {
        result.Add(new ItemTerrainTileComponentMutSet(this, id));
      }
      return result;
    }
    public bool ItemTerrainTileComponentMutSetExists(int id) {
      return rootIncarnation.incarnationsItemTerrainTileComponentMutSet.ContainsKey(id);
    }
    public void CheckHasItemTerrainTileComponentMutSet(ItemTerrainTileComponentMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasItemTerrainTileComponentMutSet(thing.id);
    }
    public void CheckHasItemTerrainTileComponentMutSet(int id) {
      if (!rootIncarnation.incarnationsItemTerrainTileComponentMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid ItemTerrainTileComponentMutSet}: " + id);
      }
    }
    public ItemTerrainTileComponentMutSet EffectItemTerrainTileComponentMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new ItemTerrainTileComponentMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateItemTerrainTileComponentMutSet(id, rootIncarnation.version, incarnation);
      return new ItemTerrainTileComponentMutSet(this, id);
    }
    public void EffectInternalCreateItemTerrainTileComponentMutSet(int id, int incarnationVersion, ItemTerrainTileComponentMutSetIncarnation incarnation) {
      var effect = new ItemTerrainTileComponentMutSetCreateEffect(id);
      rootIncarnation.incarnationsItemTerrainTileComponentMutSet
          .Add(
              id,
              new VersionAndIncarnation<ItemTerrainTileComponentMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsItemTerrainTileComponentMutSetCreateEffect.Add(effect);
    }
    public void EffectItemTerrainTileComponentMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new ItemTerrainTileComponentMutSetDeleteEffect(id);
      effectsItemTerrainTileComponentMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsItemTerrainTileComponentMutSet[id];
      rootIncarnation.incarnationsItemTerrainTileComponentMutSet.Remove(id);
    }

       
    public void EffectItemTerrainTileComponentMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasItemTerrainTileComponentMutSet(setId);
      CheckHasItemTerrainTileComponent(elementId);

      var effect = new ItemTerrainTileComponentMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsItemTerrainTileComponentMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new ItemTerrainTileComponentMutSetIncarnation(newMap);
        rootIncarnation.incarnationsItemTerrainTileComponentMutSet[setId] =
            new VersionAndIncarnation<ItemTerrainTileComponentMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsItemTerrainTileComponentMutSetAddEffect.Add(effect);
    }
    public void EffectItemTerrainTileComponentMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasItemTerrainTileComponentMutSet(setId);
      CheckHasItemTerrainTileComponent(elementId);

      var effect = new ItemTerrainTileComponentMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsItemTerrainTileComponentMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new ItemTerrainTileComponentMutSetIncarnation(newMap);
        rootIncarnation.incarnationsItemTerrainTileComponentMutSet[setId] =
            new VersionAndIncarnation<ItemTerrainTileComponentMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsItemTerrainTileComponentMutSetRemoveEffect.Add(effect);
    }

       
    public void AddItemTerrainTileComponentMutSetObserver(int id, IItemTerrainTileComponentMutSetEffectObserver observer) {
      List<IItemTerrainTileComponentMutSetEffectObserver> obsies;
      if (!observersForItemTerrainTileComponentMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IItemTerrainTileComponentMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForItemTerrainTileComponentMutSet[id] = obsies;
    }

    public void RemoveItemTerrainTileComponentMutSetObserver(int id, IItemTerrainTileComponentMutSetEffectObserver observer) {
      if (observersForItemTerrainTileComponentMutSet.ContainsKey(id)) {
        var list = observersForItemTerrainTileComponentMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForItemTerrainTileComponentMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastItemTerrainTileComponentMutSetEffects(
      SortedDictionary<int, List<IItemTerrainTileComponentMutSetEffectObserver>> observers) {
    foreach (var effect in effectsItemTerrainTileComponentMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<IItemTerrainTileComponentMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnItemTerrainTileComponentMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IItemTerrainTileComponentMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnItemTerrainTileComponentMutSetEffect(effect);
        }
        observersForItemTerrainTileComponentMutSet.Remove(effect.id);
      }
    }
    effectsItemTerrainTileComponentMutSetDeleteEffect.Clear();

    foreach (var effect in effectsItemTerrainTileComponentMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<IItemTerrainTileComponentMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnItemTerrainTileComponentMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IItemTerrainTileComponentMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnItemTerrainTileComponentMutSetEffect(effect);
        }
      }
    }
    effectsItemTerrainTileComponentMutSetAddEffect.Clear();

    foreach (var effect in effectsItemTerrainTileComponentMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<IItemTerrainTileComponentMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnItemTerrainTileComponentMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IItemTerrainTileComponentMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnItemTerrainTileComponentMutSetEffect(effect);
        }
      }
    }
    effectsItemTerrainTileComponentMutSetRemoveEffect.Clear();

    foreach (var effect in effectsItemTerrainTileComponentMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<IItemTerrainTileComponentMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnItemTerrainTileComponentMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IItemTerrainTileComponentMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnItemTerrainTileComponentMutSetEffect(effect);
        }
      }
    }
    effectsItemTerrainTileComponentMutSetCreateEffect.Clear();

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
        throw new System.Exception("Invalid DecorativeTerrainTileComponentMutSet}: " + id);
      }
    }
    public DecorativeTerrainTileComponentMutSet EffectDecorativeTerrainTileComponentMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new DecorativeTerrainTileComponentMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateDecorativeTerrainTileComponentMutSet(id, rootIncarnation.version, incarnation);
      return new DecorativeTerrainTileComponentMutSet(this, id);
    }
    public void EffectInternalCreateDecorativeTerrainTileComponentMutSet(int id, int incarnationVersion, DecorativeTerrainTileComponentMutSetIncarnation incarnation) {
      var effect = new DecorativeTerrainTileComponentMutSetCreateEffect(id);
      rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet
          .Add(
              id,
              new VersionAndIncarnation<DecorativeTerrainTileComponentMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsDecorativeTerrainTileComponentMutSetCreateEffect.Add(effect);
    }
    public void EffectDecorativeTerrainTileComponentMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new DecorativeTerrainTileComponentMutSetDeleteEffect(id);
      effectsDecorativeTerrainTileComponentMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet[id];
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
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new DecorativeTerrainTileComponentMutSetIncarnation(newMap);
        rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet[setId] =
            new VersionAndIncarnation<DecorativeTerrainTileComponentMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsDecorativeTerrainTileComponentMutSetAddEffect.Add(effect);
    }
    public void EffectDecorativeTerrainTileComponentMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasDecorativeTerrainTileComponentMutSet(setId);
      CheckHasDecorativeTerrainTileComponent(elementId);

      var effect = new DecorativeTerrainTileComponentMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new DecorativeTerrainTileComponentMutSetIncarnation(newMap);
        rootIncarnation.incarnationsDecorativeTerrainTileComponentMutSet[setId] =
            new VersionAndIncarnation<DecorativeTerrainTileComponentMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsDecorativeTerrainTileComponentMutSetRemoveEffect.Add(effect);
    }

       
    public void AddDecorativeTerrainTileComponentMutSetObserver(int id, IDecorativeTerrainTileComponentMutSetEffectObserver observer) {
      List<IDecorativeTerrainTileComponentMutSetEffectObserver> obsies;
      if (!observersForDecorativeTerrainTileComponentMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IDecorativeTerrainTileComponentMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForDecorativeTerrainTileComponentMutSet[id] = obsies;
    }

    public void RemoveDecorativeTerrainTileComponentMutSetObserver(int id, IDecorativeTerrainTileComponentMutSetEffectObserver observer) {
      if (observersForDecorativeTerrainTileComponentMutSet.ContainsKey(id)) {
        var list = observersForDecorativeTerrainTileComponentMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForDecorativeTerrainTileComponentMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastDecorativeTerrainTileComponentMutSetEffects(
      SortedDictionary<int, List<IDecorativeTerrainTileComponentMutSetEffectObserver>> observers) {
    foreach (var effect in effectsDecorativeTerrainTileComponentMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<IDecorativeTerrainTileComponentMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnDecorativeTerrainTileComponentMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IDecorativeTerrainTileComponentMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnDecorativeTerrainTileComponentMutSetEffect(effect);
        }
        observersForDecorativeTerrainTileComponentMutSet.Remove(effect.id);
      }
    }
    effectsDecorativeTerrainTileComponentMutSetDeleteEffect.Clear();

    foreach (var effect in effectsDecorativeTerrainTileComponentMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<IDecorativeTerrainTileComponentMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnDecorativeTerrainTileComponentMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IDecorativeTerrainTileComponentMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnDecorativeTerrainTileComponentMutSetEffect(effect);
        }
      }
    }
    effectsDecorativeTerrainTileComponentMutSetAddEffect.Clear();

    foreach (var effect in effectsDecorativeTerrainTileComponentMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<IDecorativeTerrainTileComponentMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnDecorativeTerrainTileComponentMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IDecorativeTerrainTileComponentMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnDecorativeTerrainTileComponentMutSetEffect(effect);
        }
      }
    }
    effectsDecorativeTerrainTileComponentMutSetRemoveEffect.Clear();

    foreach (var effect in effectsDecorativeTerrainTileComponentMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<IDecorativeTerrainTileComponentMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnDecorativeTerrainTileComponentMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IDecorativeTerrainTileComponentMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnDecorativeTerrainTileComponentMutSetEffect(effect);
        }
      }
    }
    effectsDecorativeTerrainTileComponentMutSetCreateEffect.Clear();

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
        throw new System.Exception("Invalid UpStaircaseTerrainTileComponentMutSet}: " + id);
      }
    }
    public UpStaircaseTerrainTileComponentMutSet EffectUpStaircaseTerrainTileComponentMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new UpStaircaseTerrainTileComponentMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateUpStaircaseTerrainTileComponentMutSet(id, rootIncarnation.version, incarnation);
      return new UpStaircaseTerrainTileComponentMutSet(this, id);
    }
    public void EffectInternalCreateUpStaircaseTerrainTileComponentMutSet(int id, int incarnationVersion, UpStaircaseTerrainTileComponentMutSetIncarnation incarnation) {
      var effect = new UpStaircaseTerrainTileComponentMutSetCreateEffect(id);
      rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet
          .Add(
              id,
              new VersionAndIncarnation<UpStaircaseTerrainTileComponentMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsUpStaircaseTerrainTileComponentMutSetCreateEffect.Add(effect);
    }
    public void EffectUpStaircaseTerrainTileComponentMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new UpStaircaseTerrainTileComponentMutSetDeleteEffect(id);
      effectsUpStaircaseTerrainTileComponentMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet[id];
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
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new UpStaircaseTerrainTileComponentMutSetIncarnation(newMap);
        rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet[setId] =
            new VersionAndIncarnation<UpStaircaseTerrainTileComponentMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsUpStaircaseTerrainTileComponentMutSetAddEffect.Add(effect);
    }
    public void EffectUpStaircaseTerrainTileComponentMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasUpStaircaseTerrainTileComponentMutSet(setId);
      CheckHasUpStaircaseTerrainTileComponent(elementId);

      var effect = new UpStaircaseTerrainTileComponentMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new UpStaircaseTerrainTileComponentMutSetIncarnation(newMap);
        rootIncarnation.incarnationsUpStaircaseTerrainTileComponentMutSet[setId] =
            new VersionAndIncarnation<UpStaircaseTerrainTileComponentMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsUpStaircaseTerrainTileComponentMutSetRemoveEffect.Add(effect);
    }

       
    public void AddUpStaircaseTerrainTileComponentMutSetObserver(int id, IUpStaircaseTerrainTileComponentMutSetEffectObserver observer) {
      List<IUpStaircaseTerrainTileComponentMutSetEffectObserver> obsies;
      if (!observersForUpStaircaseTerrainTileComponentMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IUpStaircaseTerrainTileComponentMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForUpStaircaseTerrainTileComponentMutSet[id] = obsies;
    }

    public void RemoveUpStaircaseTerrainTileComponentMutSetObserver(int id, IUpStaircaseTerrainTileComponentMutSetEffectObserver observer) {
      if (observersForUpStaircaseTerrainTileComponentMutSet.ContainsKey(id)) {
        var list = observersForUpStaircaseTerrainTileComponentMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForUpStaircaseTerrainTileComponentMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastUpStaircaseTerrainTileComponentMutSetEffects(
      SortedDictionary<int, List<IUpStaircaseTerrainTileComponentMutSetEffectObserver>> observers) {
    foreach (var effect in effectsUpStaircaseTerrainTileComponentMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<IUpStaircaseTerrainTileComponentMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnUpStaircaseTerrainTileComponentMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IUpStaircaseTerrainTileComponentMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnUpStaircaseTerrainTileComponentMutSetEffect(effect);
        }
        observersForUpStaircaseTerrainTileComponentMutSet.Remove(effect.id);
      }
    }
    effectsUpStaircaseTerrainTileComponentMutSetDeleteEffect.Clear();

    foreach (var effect in effectsUpStaircaseTerrainTileComponentMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<IUpStaircaseTerrainTileComponentMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnUpStaircaseTerrainTileComponentMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IUpStaircaseTerrainTileComponentMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnUpStaircaseTerrainTileComponentMutSetEffect(effect);
        }
      }
    }
    effectsUpStaircaseTerrainTileComponentMutSetAddEffect.Clear();

    foreach (var effect in effectsUpStaircaseTerrainTileComponentMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<IUpStaircaseTerrainTileComponentMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnUpStaircaseTerrainTileComponentMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IUpStaircaseTerrainTileComponentMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnUpStaircaseTerrainTileComponentMutSetEffect(effect);
        }
      }
    }
    effectsUpStaircaseTerrainTileComponentMutSetRemoveEffect.Clear();

    foreach (var effect in effectsUpStaircaseTerrainTileComponentMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<IUpStaircaseTerrainTileComponentMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnUpStaircaseTerrainTileComponentMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IUpStaircaseTerrainTileComponentMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnUpStaircaseTerrainTileComponentMutSetEffect(effect);
        }
      }
    }
    effectsUpStaircaseTerrainTileComponentMutSetCreateEffect.Clear();

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
        throw new System.Exception("Invalid DownStaircaseTerrainTileComponentMutSet}: " + id);
      }
    }
    public DownStaircaseTerrainTileComponentMutSet EffectDownStaircaseTerrainTileComponentMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new DownStaircaseTerrainTileComponentMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateDownStaircaseTerrainTileComponentMutSet(id, rootIncarnation.version, incarnation);
      return new DownStaircaseTerrainTileComponentMutSet(this, id);
    }
    public void EffectInternalCreateDownStaircaseTerrainTileComponentMutSet(int id, int incarnationVersion, DownStaircaseTerrainTileComponentMutSetIncarnation incarnation) {
      var effect = new DownStaircaseTerrainTileComponentMutSetCreateEffect(id);
      rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet
          .Add(
              id,
              new VersionAndIncarnation<DownStaircaseTerrainTileComponentMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsDownStaircaseTerrainTileComponentMutSetCreateEffect.Add(effect);
    }
    public void EffectDownStaircaseTerrainTileComponentMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new DownStaircaseTerrainTileComponentMutSetDeleteEffect(id);
      effectsDownStaircaseTerrainTileComponentMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet[id];
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
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new DownStaircaseTerrainTileComponentMutSetIncarnation(newMap);
        rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet[setId] =
            new VersionAndIncarnation<DownStaircaseTerrainTileComponentMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsDownStaircaseTerrainTileComponentMutSetAddEffect.Add(effect);
    }
    public void EffectDownStaircaseTerrainTileComponentMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasDownStaircaseTerrainTileComponentMutSet(setId);
      CheckHasDownStaircaseTerrainTileComponent(elementId);

      var effect = new DownStaircaseTerrainTileComponentMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new DownStaircaseTerrainTileComponentMutSetIncarnation(newMap);
        rootIncarnation.incarnationsDownStaircaseTerrainTileComponentMutSet[setId] =
            new VersionAndIncarnation<DownStaircaseTerrainTileComponentMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsDownStaircaseTerrainTileComponentMutSetRemoveEffect.Add(effect);
    }

       
    public void AddDownStaircaseTerrainTileComponentMutSetObserver(int id, IDownStaircaseTerrainTileComponentMutSetEffectObserver observer) {
      List<IDownStaircaseTerrainTileComponentMutSetEffectObserver> obsies;
      if (!observersForDownStaircaseTerrainTileComponentMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IDownStaircaseTerrainTileComponentMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForDownStaircaseTerrainTileComponentMutSet[id] = obsies;
    }

    public void RemoveDownStaircaseTerrainTileComponentMutSetObserver(int id, IDownStaircaseTerrainTileComponentMutSetEffectObserver observer) {
      if (observersForDownStaircaseTerrainTileComponentMutSet.ContainsKey(id)) {
        var list = observersForDownStaircaseTerrainTileComponentMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForDownStaircaseTerrainTileComponentMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastDownStaircaseTerrainTileComponentMutSetEffects(
      SortedDictionary<int, List<IDownStaircaseTerrainTileComponentMutSetEffectObserver>> observers) {
    foreach (var effect in effectsDownStaircaseTerrainTileComponentMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<IDownStaircaseTerrainTileComponentMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnDownStaircaseTerrainTileComponentMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IDownStaircaseTerrainTileComponentMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnDownStaircaseTerrainTileComponentMutSetEffect(effect);
        }
        observersForDownStaircaseTerrainTileComponentMutSet.Remove(effect.id);
      }
    }
    effectsDownStaircaseTerrainTileComponentMutSetDeleteEffect.Clear();

    foreach (var effect in effectsDownStaircaseTerrainTileComponentMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<IDownStaircaseTerrainTileComponentMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnDownStaircaseTerrainTileComponentMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IDownStaircaseTerrainTileComponentMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnDownStaircaseTerrainTileComponentMutSetEffect(effect);
        }
      }
    }
    effectsDownStaircaseTerrainTileComponentMutSetAddEffect.Clear();

    foreach (var effect in effectsDownStaircaseTerrainTileComponentMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<IDownStaircaseTerrainTileComponentMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnDownStaircaseTerrainTileComponentMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IDownStaircaseTerrainTileComponentMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnDownStaircaseTerrainTileComponentMutSetEffect(effect);
        }
      }
    }
    effectsDownStaircaseTerrainTileComponentMutSetRemoveEffect.Clear();

    foreach (var effect in effectsDownStaircaseTerrainTileComponentMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<IDownStaircaseTerrainTileComponentMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnDownStaircaseTerrainTileComponentMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IDownStaircaseTerrainTileComponentMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnDownStaircaseTerrainTileComponentMutSetEffect(effect);
        }
      }
    }
    effectsDownStaircaseTerrainTileComponentMutSetCreateEffect.Clear();

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
        throw new System.Exception("Invalid UnitMutSet}: " + id);
      }
    }
    public UnitMutSet EffectUnitMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new UnitMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateUnitMutSet(id, rootIncarnation.version, incarnation);
      return new UnitMutSet(this, id);
    }
    public void EffectInternalCreateUnitMutSet(int id, int incarnationVersion, UnitMutSetIncarnation incarnation) {
      var effect = new UnitMutSetCreateEffect(id);
      rootIncarnation.incarnationsUnitMutSet
          .Add(
              id,
              new VersionAndIncarnation<UnitMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsUnitMutSetCreateEffect.Add(effect);
    }
    public void EffectUnitMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new UnitMutSetDeleteEffect(id);
      effectsUnitMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsUnitMutSet[id];
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
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new UnitMutSetIncarnation(newMap);
        rootIncarnation.incarnationsUnitMutSet[setId] =
            new VersionAndIncarnation<UnitMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsUnitMutSetAddEffect.Add(effect);
    }
    public void EffectUnitMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasUnitMutSet(setId);
      CheckHasUnit(elementId);

      var effect = new UnitMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsUnitMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new UnitMutSetIncarnation(newMap);
        rootIncarnation.incarnationsUnitMutSet[setId] =
            new VersionAndIncarnation<UnitMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsUnitMutSetRemoveEffect.Add(effect);
    }

       
    public void AddUnitMutSetObserver(int id, IUnitMutSetEffectObserver observer) {
      List<IUnitMutSetEffectObserver> obsies;
      if (!observersForUnitMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IUnitMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForUnitMutSet[id] = obsies;
    }

    public void RemoveUnitMutSetObserver(int id, IUnitMutSetEffectObserver observer) {
      if (observersForUnitMutSet.ContainsKey(id)) {
        var list = observersForUnitMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForUnitMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastUnitMutSetEffects(
      SortedDictionary<int, List<IUnitMutSetEffectObserver>> observers) {
    foreach (var effect in effectsUnitMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<IUnitMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnUnitMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IUnitMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnUnitMutSetEffect(effect);
        }
        observersForUnitMutSet.Remove(effect.id);
      }
    }
    effectsUnitMutSetDeleteEffect.Clear();

    foreach (var effect in effectsUnitMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<IUnitMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnUnitMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IUnitMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnUnitMutSetEffect(effect);
        }
      }
    }
    effectsUnitMutSetAddEffect.Clear();

    foreach (var effect in effectsUnitMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<IUnitMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnUnitMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IUnitMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnUnitMutSetEffect(effect);
        }
      }
    }
    effectsUnitMutSetRemoveEffect.Clear();

    foreach (var effect in effectsUnitMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<IUnitMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnUnitMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IUnitMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnUnitMutSetEffect(effect);
        }
      }
    }
    effectsUnitMutSetCreateEffect.Clear();

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
        throw new System.Exception("Invalid TerrainTileByLocationMutMap}: " + id);
      }
    }
    public TerrainTileByLocationMutMap EffectTerrainTileByLocationMutMapCreate() {
      CheckUnlocked();
      var id = NewId();
      EffectInternalCreateTerrainTileByLocationMutMap(
          id,
          rootIncarnation.version,
          new TerrainTileByLocationMutMapIncarnation(
              new SortedDictionary<Location, int>()));
      return new TerrainTileByLocationMutMap(this, id);
    }
       
    public void EffectInternalCreateTerrainTileByLocationMutMap(int id, int incarnationVersion, TerrainTileByLocationMutMapIncarnation incarnation) {
      var effect = new TerrainTileByLocationMutMapCreateEffect(id);
      rootIncarnation.incarnationsTerrainTileByLocationMutMap
          .Add(
              id,
              new VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsTerrainTileByLocationMutMapCreateEffect.Add(effect);
    }
    public void EffectTerrainTileByLocationMutMapDelete(int id) {
      CheckUnlocked();
      var effect = new TerrainTileByLocationMutMapDeleteEffect(id);
      effectsTerrainTileByLocationMutMapDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsTerrainTileByLocationMutMap[id];
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
      effectsTerrainTileByLocationMutMapAddEffect.Add(effect);
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
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.map;
        var newMap = new SortedDictionary<Location, int>(oldMap);
        newMap.Remove(key);
        var newIncarnation = new TerrainTileByLocationMutMapIncarnation(newMap);
        rootIncarnation.incarnationsTerrainTileByLocationMutMap[mapId] =
            new VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsTerrainTileByLocationMutMapRemoveEffect.Add(effect);
    }
    public void AddTerrainTileByLocationMutMapObserver(int id, ITerrainTileByLocationMutMapEffectObserver observer) {
      List<ITerrainTileByLocationMutMapEffectObserver> obsies;
      if (!observersForTerrainTileByLocationMutMap.TryGetValue(id, out obsies)) {
        obsies = new List<ITerrainTileByLocationMutMapEffectObserver>();
      }
      obsies.Add(observer);
      observersForTerrainTileByLocationMutMap[id] = obsies;
    }

    public void RemoveTerrainTileByLocationMutMapObserver(int id, ITerrainTileByLocationMutMapEffectObserver observer) {
      if (observersForTerrainTileByLocationMutMap.ContainsKey(id)) {
        var map = observersForTerrainTileByLocationMutMap[id];
        map.Remove(observer);
        if (map.Count == 0) {
          observersForTerrainTileByLocationMutMap.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

  public void BroadcastTerrainTileByLocationMutMapEffects(
      SortedDictionary<int, List<ITerrainTileByLocationMutMapEffectObserver>> observers) {
    foreach (var effect in effectsTerrainTileByLocationMutMapDeleteEffect) {
      if (observers.TryGetValue(0, out List<ITerrainTileByLocationMutMapEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTerrainTileByLocationMutMapEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITerrainTileByLocationMutMapEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTerrainTileByLocationMutMapEffect(effect);
        }
        observersForTerrainTileByLocationMutMap.Remove(effect.id);
      }
    }
    effectsTerrainTileByLocationMutMapDeleteEffect.Clear();

    foreach (var effect in effectsTerrainTileByLocationMutMapAddEffect) {
      if (observers.TryGetValue(0, out List<ITerrainTileByLocationMutMapEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTerrainTileByLocationMutMapEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITerrainTileByLocationMutMapEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTerrainTileByLocationMutMapEffect(effect);
        }
      }
    }
    effectsTerrainTileByLocationMutMapAddEffect.Clear();

    foreach (var effect in effectsTerrainTileByLocationMutMapRemoveEffect) {
      if (observers.TryGetValue(0, out List<ITerrainTileByLocationMutMapEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTerrainTileByLocationMutMapEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITerrainTileByLocationMutMapEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTerrainTileByLocationMutMapEffect(effect);
        }
      }
    }
    effectsTerrainTileByLocationMutMapRemoveEffect.Clear();

    foreach (var effect in effectsTerrainTileByLocationMutMapCreateEffect) {
      if (observers.TryGetValue(0, out List<ITerrainTileByLocationMutMapEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTerrainTileByLocationMutMapEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITerrainTileByLocationMutMapEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTerrainTileByLocationMutMapEffect(effect);
        }
      }
    }
    effectsTerrainTileByLocationMutMapCreateEffect.Clear();

  }
}

}
