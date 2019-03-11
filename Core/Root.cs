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

  bool locked;

  // 0 means everything

  readonly SortedDictionary<int, List<ISquareCaveLevelControllerEffectObserver>> observersForSquareCaveLevelController =
      new SortedDictionary<int, List<ISquareCaveLevelControllerEffectObserver>>();
  readonly List<SquareCaveLevelControllerCreateEffect> effectsSquareCaveLevelControllerCreateEffect =
      new List<SquareCaveLevelControllerCreateEffect>();
  readonly List<SquareCaveLevelControllerDeleteEffect> effectsSquareCaveLevelControllerDeleteEffect =
      new List<SquareCaveLevelControllerDeleteEffect>();

  readonly SortedDictionary<int, List<IRidgeLevelControllerEffectObserver>> observersForRidgeLevelController =
      new SortedDictionary<int, List<IRidgeLevelControllerEffectObserver>>();
  readonly List<RidgeLevelControllerCreateEffect> effectsRidgeLevelControllerCreateEffect =
      new List<RidgeLevelControllerCreateEffect>();
  readonly List<RidgeLevelControllerDeleteEffect> effectsRidgeLevelControllerDeleteEffect =
      new List<RidgeLevelControllerDeleteEffect>();

  readonly SortedDictionary<int, List<IGauntletLevelControllerEffectObserver>> observersForGauntletLevelController =
      new SortedDictionary<int, List<IGauntletLevelControllerEffectObserver>>();
  readonly List<GauntletLevelControllerCreateEffect> effectsGauntletLevelControllerCreateEffect =
      new List<GauntletLevelControllerCreateEffect>();
  readonly List<GauntletLevelControllerDeleteEffect> effectsGauntletLevelControllerDeleteEffect =
      new List<GauntletLevelControllerDeleteEffect>();

  readonly SortedDictionary<int, List<IPreGauntletLevelControllerEffectObserver>> observersForPreGauntletLevelController =
      new SortedDictionary<int, List<IPreGauntletLevelControllerEffectObserver>>();
  readonly List<PreGauntletLevelControllerCreateEffect> effectsPreGauntletLevelControllerCreateEffect =
      new List<PreGauntletLevelControllerCreateEffect>();
  readonly List<PreGauntletLevelControllerDeleteEffect> effectsPreGauntletLevelControllerDeleteEffect =
      new List<PreGauntletLevelControllerDeleteEffect>();

  readonly SortedDictionary<int, List<IRavashrikeLevelControllerEffectObserver>> observersForRavashrikeLevelController =
      new SortedDictionary<int, List<IRavashrikeLevelControllerEffectObserver>>();
  readonly List<RavashrikeLevelControllerCreateEffect> effectsRavashrikeLevelControllerCreateEffect =
      new List<RavashrikeLevelControllerCreateEffect>();
  readonly List<RavashrikeLevelControllerDeleteEffect> effectsRavashrikeLevelControllerDeleteEffect =
      new List<RavashrikeLevelControllerDeleteEffect>();

  readonly SortedDictionary<int, List<IPentagonalCaveLevelControllerEffectObserver>> observersForPentagonalCaveLevelController =
      new SortedDictionary<int, List<IPentagonalCaveLevelControllerEffectObserver>>();
  readonly List<PentagonalCaveLevelControllerCreateEffect> effectsPentagonalCaveLevelControllerCreateEffect =
      new List<PentagonalCaveLevelControllerCreateEffect>();
  readonly List<PentagonalCaveLevelControllerDeleteEffect> effectsPentagonalCaveLevelControllerDeleteEffect =
      new List<PentagonalCaveLevelControllerDeleteEffect>();

  readonly SortedDictionary<int, List<ICliffLevelControllerEffectObserver>> observersForCliffLevelController =
      new SortedDictionary<int, List<ICliffLevelControllerEffectObserver>>();
  readonly List<CliffLevelControllerCreateEffect> effectsCliffLevelControllerCreateEffect =
      new List<CliffLevelControllerCreateEffect>();
  readonly List<CliffLevelControllerDeleteEffect> effectsCliffLevelControllerDeleteEffect =
      new List<CliffLevelControllerDeleteEffect>();

  readonly SortedDictionary<int, List<ILevelEffectObserver>> observersForLevel =
      new SortedDictionary<int, List<ILevelEffectObserver>>();
  readonly List<LevelCreateEffect> effectsLevelCreateEffect =
      new List<LevelCreateEffect>();
  readonly List<LevelDeleteEffect> effectsLevelDeleteEffect =
      new List<LevelDeleteEffect>();
  readonly List<LevelSetControllerEffect> effectsLevelSetControllerEffect =
      new List<LevelSetControllerEffect>();
  readonly List<LevelSetTimeEffect> effectsLevelSetTimeEffect =
      new List<LevelSetTimeEffect>();

  readonly SortedDictionary<int, List<ITimeAnchorTTCEffectObserver>> observersForTimeAnchorTTC =
      new SortedDictionary<int, List<ITimeAnchorTTCEffectObserver>>();
  readonly List<TimeAnchorTTCCreateEffect> effectsTimeAnchorTTCCreateEffect =
      new List<TimeAnchorTTCCreateEffect>();
  readonly List<TimeAnchorTTCDeleteEffect> effectsTimeAnchorTTCDeleteEffect =
      new List<TimeAnchorTTCDeleteEffect>();

  readonly SortedDictionary<int, List<ITerrainTileEffectObserver>> observersForTerrainTile =
      new SortedDictionary<int, List<ITerrainTileEffectObserver>>();
  readonly List<TerrainTileCreateEffect> effectsTerrainTileCreateEffect =
      new List<TerrainTileCreateEffect>();
  readonly List<TerrainTileDeleteEffect> effectsTerrainTileDeleteEffect =
      new List<TerrainTileDeleteEffect>();
  readonly List<TerrainTileSetElevationEffect> effectsTerrainTileSetElevationEffect =
      new List<TerrainTileSetElevationEffect>();
  readonly List<TerrainTileSetWalkableEffect> effectsTerrainTileSetWalkableEffect =
      new List<TerrainTileSetWalkableEffect>();
  readonly List<TerrainTileSetClassIdEffect> effectsTerrainTileSetClassIdEffect =
      new List<TerrainTileSetClassIdEffect>();

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

  readonly SortedDictionary<int, List<IStaircaseTTCEffectObserver>> observersForStaircaseTTC =
      new SortedDictionary<int, List<IStaircaseTTCEffectObserver>>();
  readonly List<StaircaseTTCCreateEffect> effectsStaircaseTTCCreateEffect =
      new List<StaircaseTTCCreateEffect>();
  readonly List<StaircaseTTCDeleteEffect> effectsStaircaseTTCDeleteEffect =
      new List<StaircaseTTCDeleteEffect>();
  readonly List<StaircaseTTCSetDestinationLevelEffect> effectsStaircaseTTCSetDestinationLevelEffect =
      new List<StaircaseTTCSetDestinationLevelEffect>();
  readonly List<StaircaseTTCSetDestinationLevelPortalIndexEffect> effectsStaircaseTTCSetDestinationLevelPortalIndexEffect =
      new List<StaircaseTTCSetDestinationLevelPortalIndexEffect>();

  readonly SortedDictionary<int, List<IDecorativeTTCEffectObserver>> observersForDecorativeTTC =
      new SortedDictionary<int, List<IDecorativeTTCEffectObserver>>();
  readonly List<DecorativeTTCCreateEffect> effectsDecorativeTTCCreateEffect =
      new List<DecorativeTTCCreateEffect>();
  readonly List<DecorativeTTCDeleteEffect> effectsDecorativeTTCDeleteEffect =
      new List<DecorativeTTCDeleteEffect>();

  readonly SortedDictionary<int, List<IManaPotionEffectObserver>> observersForManaPotion =
      new SortedDictionary<int, List<IManaPotionEffectObserver>>();
  readonly List<ManaPotionCreateEffect> effectsManaPotionCreateEffect =
      new List<ManaPotionCreateEffect>();
  readonly List<ManaPotionDeleteEffect> effectsManaPotionDeleteEffect =
      new List<ManaPotionDeleteEffect>();

  readonly SortedDictionary<int, List<IHealthPotionEffectObserver>> observersForHealthPotion =
      new SortedDictionary<int, List<IHealthPotionEffectObserver>>();
  readonly List<HealthPotionCreateEffect> effectsHealthPotionCreateEffect =
      new List<HealthPotionCreateEffect>();
  readonly List<HealthPotionDeleteEffect> effectsHealthPotionDeleteEffect =
      new List<HealthPotionDeleteEffect>();

  readonly SortedDictionary<int, List<IInertiaRingEffectObserver>> observersForInertiaRing =
      new SortedDictionary<int, List<IInertiaRingEffectObserver>>();
  readonly List<InertiaRingCreateEffect> effectsInertiaRingCreateEffect =
      new List<InertiaRingCreateEffect>();
  readonly List<InertiaRingDeleteEffect> effectsInertiaRingDeleteEffect =
      new List<InertiaRingDeleteEffect>();

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

  readonly SortedDictionary<int, List<ICounteringUCEffectObserver>> observersForCounteringUC =
      new SortedDictionary<int, List<ICounteringUCEffectObserver>>();
  readonly List<CounteringUCCreateEffect> effectsCounteringUCCreateEffect =
      new List<CounteringUCCreateEffect>();
  readonly List<CounteringUCDeleteEffect> effectsCounteringUCDeleteEffect =
      new List<CounteringUCDeleteEffect>();

  readonly SortedDictionary<int, List<IShieldingUCEffectObserver>> observersForShieldingUC =
      new SortedDictionary<int, List<IShieldingUCEffectObserver>>();
  readonly List<ShieldingUCCreateEffect> effectsShieldingUCCreateEffect =
      new List<ShieldingUCCreateEffect>();
  readonly List<ShieldingUCDeleteEffect> effectsShieldingUCDeleteEffect =
      new List<ShieldingUCDeleteEffect>();

  readonly SortedDictionary<int, List<IEvaporateImpulseEffectObserver>> observersForEvaporateImpulse =
      new SortedDictionary<int, List<IEvaporateImpulseEffectObserver>>();
  readonly List<EvaporateImpulseCreateEffect> effectsEvaporateImpulseCreateEffect =
      new List<EvaporateImpulseCreateEffect>();
  readonly List<EvaporateImpulseDeleteEffect> effectsEvaporateImpulseDeleteEffect =
      new List<EvaporateImpulseDeleteEffect>();

  readonly SortedDictionary<int, List<ITimeScriptDirectiveUCEffectObserver>> observersForTimeScriptDirectiveUC =
      new SortedDictionary<int, List<ITimeScriptDirectiveUCEffectObserver>>();
  readonly List<TimeScriptDirectiveUCCreateEffect> effectsTimeScriptDirectiveUCCreateEffect =
      new List<TimeScriptDirectiveUCCreateEffect>();
  readonly List<TimeScriptDirectiveUCDeleteEffect> effectsTimeScriptDirectiveUCDeleteEffect =
      new List<TimeScriptDirectiveUCDeleteEffect>();

  readonly SortedDictionary<int, List<ITimeCloneAICapabilityUCEffectObserver>> observersForTimeCloneAICapabilityUC =
      new SortedDictionary<int, List<ITimeCloneAICapabilityUCEffectObserver>>();
  readonly List<TimeCloneAICapabilityUCCreateEffect> effectsTimeCloneAICapabilityUCCreateEffect =
      new List<TimeCloneAICapabilityUCCreateEffect>();
  readonly List<TimeCloneAICapabilityUCDeleteEffect> effectsTimeCloneAICapabilityUCDeleteEffect =
      new List<TimeCloneAICapabilityUCDeleteEffect>();

  readonly SortedDictionary<int, List<IBidingOperationUCEffectObserver>> observersForBidingOperationUC =
      new SortedDictionary<int, List<IBidingOperationUCEffectObserver>>();
  readonly List<BidingOperationUCCreateEffect> effectsBidingOperationUCCreateEffect =
      new List<BidingOperationUCCreateEffect>();
  readonly List<BidingOperationUCDeleteEffect> effectsBidingOperationUCDeleteEffect =
      new List<BidingOperationUCDeleteEffect>();
  readonly List<BidingOperationUCSetChargeEffect> effectsBidingOperationUCSetChargeEffect =
      new List<BidingOperationUCSetChargeEffect>();

  readonly SortedDictionary<int, List<IUnleashBideImpulseEffectObserver>> observersForUnleashBideImpulse =
      new SortedDictionary<int, List<IUnleashBideImpulseEffectObserver>>();
  readonly List<UnleashBideImpulseCreateEffect> effectsUnleashBideImpulseCreateEffect =
      new List<UnleashBideImpulseCreateEffect>();
  readonly List<UnleashBideImpulseDeleteEffect> effectsUnleashBideImpulseDeleteEffect =
      new List<UnleashBideImpulseDeleteEffect>();

  readonly SortedDictionary<int, List<IContinueBidingImpulseEffectObserver>> observersForContinueBidingImpulse =
      new SortedDictionary<int, List<IContinueBidingImpulseEffectObserver>>();
  readonly List<ContinueBidingImpulseCreateEffect> effectsContinueBidingImpulseCreateEffect =
      new List<ContinueBidingImpulseCreateEffect>();
  readonly List<ContinueBidingImpulseDeleteEffect> effectsContinueBidingImpulseDeleteEffect =
      new List<ContinueBidingImpulseDeleteEffect>();

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

  readonly SortedDictionary<int, List<IFireImpulseEffectObserver>> observersForFireImpulse =
      new SortedDictionary<int, List<IFireImpulseEffectObserver>>();
  readonly List<FireImpulseCreateEffect> effectsFireImpulseCreateEffect =
      new List<FireImpulseCreateEffect>();
  readonly List<FireImpulseDeleteEffect> effectsFireImpulseDeleteEffect =
      new List<FireImpulseDeleteEffect>();

  readonly SortedDictionary<int, List<ICounterImpulseEffectObserver>> observersForCounterImpulse =
      new SortedDictionary<int, List<ICounterImpulseEffectObserver>>();
  readonly List<CounterImpulseCreateEffect> effectsCounterImpulseCreateEffect =
      new List<CounterImpulseCreateEffect>();
  readonly List<CounterImpulseDeleteEffect> effectsCounterImpulseDeleteEffect =
      new List<CounterImpulseDeleteEffect>();

  readonly SortedDictionary<int, List<IDefendImpulseEffectObserver>> observersForDefendImpulse =
      new SortedDictionary<int, List<IDefendImpulseEffectObserver>>();
  readonly List<DefendImpulseCreateEffect> effectsDefendImpulseCreateEffect =
      new List<DefendImpulseCreateEffect>();
  readonly List<DefendImpulseDeleteEffect> effectsDefendImpulseDeleteEffect =
      new List<DefendImpulseDeleteEffect>();

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
  readonly List<GameSetLastPlayerRequestEffect> effectsGameSetLastPlayerRequestEffect =
      new List<GameSetLastPlayerRequestEffect>();
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

  readonly SortedDictionary<int, List<IIRequestMutListEffectObserver>> observersForIRequestMutList =
      new SortedDictionary<int, List<IIRequestMutListEffectObserver>>();
  readonly List<IRequestMutListCreateEffect> effectsIRequestMutListCreateEffect =
      new List<IRequestMutListCreateEffect>();
  readonly List<IRequestMutListDeleteEffect> effectsIRequestMutListDeleteEffect =
      new List<IRequestMutListDeleteEffect>();
  readonly List<IRequestMutListAddEffect> effectsIRequestMutListAddEffect =
      new List<IRequestMutListAddEffect>();
  readonly List<IRequestMutListRemoveEffect> effectsIRequestMutListRemoveEffect =
      new List<IRequestMutListRemoveEffect>();

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

  readonly SortedDictionary<int, List<ICounteringUCWeakMutSetEffectObserver>> observersForCounteringUCWeakMutSet =
      new SortedDictionary<int, List<ICounteringUCWeakMutSetEffectObserver>>();
  readonly List<CounteringUCWeakMutSetCreateEffect> effectsCounteringUCWeakMutSetCreateEffect =
      new List<CounteringUCWeakMutSetCreateEffect>();
  readonly List<CounteringUCWeakMutSetDeleteEffect> effectsCounteringUCWeakMutSetDeleteEffect =
      new List<CounteringUCWeakMutSetDeleteEffect>();
  readonly List<CounteringUCWeakMutSetAddEffect> effectsCounteringUCWeakMutSetAddEffect =
      new List<CounteringUCWeakMutSetAddEffect>();
  readonly List<CounteringUCWeakMutSetRemoveEffect> effectsCounteringUCWeakMutSetRemoveEffect =
      new List<CounteringUCWeakMutSetRemoveEffect>();

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

  readonly SortedDictionary<int, List<ITimeCloneAICapabilityUCWeakMutSetEffectObserver>> observersForTimeCloneAICapabilityUCWeakMutSet =
      new SortedDictionary<int, List<ITimeCloneAICapabilityUCWeakMutSetEffectObserver>>();
  readonly List<TimeCloneAICapabilityUCWeakMutSetCreateEffect> effectsTimeCloneAICapabilityUCWeakMutSetCreateEffect =
      new List<TimeCloneAICapabilityUCWeakMutSetCreateEffect>();
  readonly List<TimeCloneAICapabilityUCWeakMutSetDeleteEffect> effectsTimeCloneAICapabilityUCWeakMutSetDeleteEffect =
      new List<TimeCloneAICapabilityUCWeakMutSetDeleteEffect>();
  readonly List<TimeCloneAICapabilityUCWeakMutSetAddEffect> effectsTimeCloneAICapabilityUCWeakMutSetAddEffect =
      new List<TimeCloneAICapabilityUCWeakMutSetAddEffect>();
  readonly List<TimeCloneAICapabilityUCWeakMutSetRemoveEffect> effectsTimeCloneAICapabilityUCWeakMutSetRemoveEffect =
      new List<TimeCloneAICapabilityUCWeakMutSetRemoveEffect>();

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

  readonly SortedDictionary<int, List<IInertiaRingMutSetEffectObserver>> observersForInertiaRingMutSet =
      new SortedDictionary<int, List<IInertiaRingMutSetEffectObserver>>();
  readonly List<InertiaRingMutSetCreateEffect> effectsInertiaRingMutSetCreateEffect =
      new List<InertiaRingMutSetCreateEffect>();
  readonly List<InertiaRingMutSetDeleteEffect> effectsInertiaRingMutSetDeleteEffect =
      new List<InertiaRingMutSetDeleteEffect>();
  readonly List<InertiaRingMutSetAddEffect> effectsInertiaRingMutSetAddEffect =
      new List<InertiaRingMutSetAddEffect>();
  readonly List<InertiaRingMutSetRemoveEffect> effectsInertiaRingMutSetRemoveEffect =
      new List<InertiaRingMutSetRemoveEffect>();

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

  readonly SortedDictionary<int, List<IManaPotionMutSetEffectObserver>> observersForManaPotionMutSet =
      new SortedDictionary<int, List<IManaPotionMutSetEffectObserver>>();
  readonly List<ManaPotionMutSetCreateEffect> effectsManaPotionMutSetCreateEffect =
      new List<ManaPotionMutSetCreateEffect>();
  readonly List<ManaPotionMutSetDeleteEffect> effectsManaPotionMutSetDeleteEffect =
      new List<ManaPotionMutSetDeleteEffect>();
  readonly List<ManaPotionMutSetAddEffect> effectsManaPotionMutSetAddEffect =
      new List<ManaPotionMutSetAddEffect>();
  readonly List<ManaPotionMutSetRemoveEffect> effectsManaPotionMutSetRemoveEffect =
      new List<ManaPotionMutSetRemoveEffect>();

  readonly SortedDictionary<int, List<IHealthPotionMutSetEffectObserver>> observersForHealthPotionMutSet =
      new SortedDictionary<int, List<IHealthPotionMutSetEffectObserver>>();
  readonly List<HealthPotionMutSetCreateEffect> effectsHealthPotionMutSetCreateEffect =
      new List<HealthPotionMutSetCreateEffect>();
  readonly List<HealthPotionMutSetDeleteEffect> effectsHealthPotionMutSetDeleteEffect =
      new List<HealthPotionMutSetDeleteEffect>();
  readonly List<HealthPotionMutSetAddEffect> effectsHealthPotionMutSetAddEffect =
      new List<HealthPotionMutSetAddEffect>();
  readonly List<HealthPotionMutSetRemoveEffect> effectsHealthPotionMutSetRemoveEffect =
      new List<HealthPotionMutSetRemoveEffect>();

  readonly SortedDictionary<int, List<ITimeScriptDirectiveUCMutSetEffectObserver>> observersForTimeScriptDirectiveUCMutSet =
      new SortedDictionary<int, List<ITimeScriptDirectiveUCMutSetEffectObserver>>();
  readonly List<TimeScriptDirectiveUCMutSetCreateEffect> effectsTimeScriptDirectiveUCMutSetCreateEffect =
      new List<TimeScriptDirectiveUCMutSetCreateEffect>();
  readonly List<TimeScriptDirectiveUCMutSetDeleteEffect> effectsTimeScriptDirectiveUCMutSetDeleteEffect =
      new List<TimeScriptDirectiveUCMutSetDeleteEffect>();
  readonly List<TimeScriptDirectiveUCMutSetAddEffect> effectsTimeScriptDirectiveUCMutSetAddEffect =
      new List<TimeScriptDirectiveUCMutSetAddEffect>();
  readonly List<TimeScriptDirectiveUCMutSetRemoveEffect> effectsTimeScriptDirectiveUCMutSetRemoveEffect =
      new List<TimeScriptDirectiveUCMutSetRemoveEffect>();

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

  readonly SortedDictionary<int, List<ITimeCloneAICapabilityUCMutSetEffectObserver>> observersForTimeCloneAICapabilityUCMutSet =
      new SortedDictionary<int, List<ITimeCloneAICapabilityUCMutSetEffectObserver>>();
  readonly List<TimeCloneAICapabilityUCMutSetCreateEffect> effectsTimeCloneAICapabilityUCMutSetCreateEffect =
      new List<TimeCloneAICapabilityUCMutSetCreateEffect>();
  readonly List<TimeCloneAICapabilityUCMutSetDeleteEffect> effectsTimeCloneAICapabilityUCMutSetDeleteEffect =
      new List<TimeCloneAICapabilityUCMutSetDeleteEffect>();
  readonly List<TimeCloneAICapabilityUCMutSetAddEffect> effectsTimeCloneAICapabilityUCMutSetAddEffect =
      new List<TimeCloneAICapabilityUCMutSetAddEffect>();
  readonly List<TimeCloneAICapabilityUCMutSetRemoveEffect> effectsTimeCloneAICapabilityUCMutSetRemoveEffect =
      new List<TimeCloneAICapabilityUCMutSetRemoveEffect>();

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

  readonly SortedDictionary<int, List<ICounteringUCMutSetEffectObserver>> observersForCounteringUCMutSet =
      new SortedDictionary<int, List<ICounteringUCMutSetEffectObserver>>();
  readonly List<CounteringUCMutSetCreateEffect> effectsCounteringUCMutSetCreateEffect =
      new List<CounteringUCMutSetCreateEffect>();
  readonly List<CounteringUCMutSetDeleteEffect> effectsCounteringUCMutSetDeleteEffect =
      new List<CounteringUCMutSetDeleteEffect>();
  readonly List<CounteringUCMutSetAddEffect> effectsCounteringUCMutSetAddEffect =
      new List<CounteringUCMutSetAddEffect>();
  readonly List<CounteringUCMutSetRemoveEffect> effectsCounteringUCMutSetRemoveEffect =
      new List<CounteringUCMutSetRemoveEffect>();

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

  readonly SortedDictionary<int, List<ITimeAnchorTTCMutSetEffectObserver>> observersForTimeAnchorTTCMutSet =
      new SortedDictionary<int, List<ITimeAnchorTTCMutSetEffectObserver>>();
  readonly List<TimeAnchorTTCMutSetCreateEffect> effectsTimeAnchorTTCMutSetCreateEffect =
      new List<TimeAnchorTTCMutSetCreateEffect>();
  readonly List<TimeAnchorTTCMutSetDeleteEffect> effectsTimeAnchorTTCMutSetDeleteEffect =
      new List<TimeAnchorTTCMutSetDeleteEffect>();
  readonly List<TimeAnchorTTCMutSetAddEffect> effectsTimeAnchorTTCMutSetAddEffect =
      new List<TimeAnchorTTCMutSetAddEffect>();
  readonly List<TimeAnchorTTCMutSetRemoveEffect> effectsTimeAnchorTTCMutSetRemoveEffect =
      new List<TimeAnchorTTCMutSetRemoveEffect>();

  readonly SortedDictionary<int, List<IStaircaseTTCMutSetEffectObserver>> observersForStaircaseTTCMutSet =
      new SortedDictionary<int, List<IStaircaseTTCMutSetEffectObserver>>();
  readonly List<StaircaseTTCMutSetCreateEffect> effectsStaircaseTTCMutSetCreateEffect =
      new List<StaircaseTTCMutSetCreateEffect>();
  readonly List<StaircaseTTCMutSetDeleteEffect> effectsStaircaseTTCMutSetDeleteEffect =
      new List<StaircaseTTCMutSetDeleteEffect>();
  readonly List<StaircaseTTCMutSetAddEffect> effectsStaircaseTTCMutSetAddEffect =
      new List<StaircaseTTCMutSetAddEffect>();
  readonly List<StaircaseTTCMutSetRemoveEffect> effectsStaircaseTTCMutSetRemoveEffect =
      new List<StaircaseTTCMutSetRemoveEffect>();

  readonly SortedDictionary<int, List<IDecorativeTTCMutSetEffectObserver>> observersForDecorativeTTCMutSet =
      new SortedDictionary<int, List<IDecorativeTTCMutSetEffectObserver>>();
  readonly List<DecorativeTTCMutSetCreateEffect> effectsDecorativeTTCMutSetCreateEffect =
      new List<DecorativeTTCMutSetCreateEffect>();
  readonly List<DecorativeTTCMutSetDeleteEffect> effectsDecorativeTTCMutSetDeleteEffect =
      new List<DecorativeTTCMutSetDeleteEffect>();
  readonly List<DecorativeTTCMutSetAddEffect> effectsDecorativeTTCMutSetAddEffect =
      new List<DecorativeTTCMutSetAddEffect>();
  readonly List<DecorativeTTCMutSetRemoveEffect> effectsDecorativeTTCMutSetRemoveEffect =
      new List<DecorativeTTCMutSetRemoveEffect>();

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
    this.locked = true;
  }

  public Root(ILogger logger, RootIncarnation rootIncarnation) {
    this.logger = logger;
    this.rootIncarnation = rootIncarnation;
    this.locked = false;
    this.Snapshot();
    this.locked = true;
  }

  public int version { get { return rootIncarnation.version; } }

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

    foreach (var entry in this.rootIncarnation.incarnationsSquareCaveLevelController) {
      result += GetSquareCaveLevelControllerHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsRidgeLevelController) {
      result += GetRidgeLevelControllerHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsGauntletLevelController) {
      result += GetGauntletLevelControllerHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsPreGauntletLevelController) {
      result += GetPreGauntletLevelControllerHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsRavashrikeLevelController) {
      result += GetRavashrikeLevelControllerHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsPentagonalCaveLevelController) {
      result += GetPentagonalCaveLevelControllerHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsCliffLevelController) {
      result += GetCliffLevelControllerHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsLevel) {
      result += GetLevelHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsTimeAnchorTTC) {
      result += GetTimeAnchorTTCHash(entry.Key, entry.Value.version, entry.Value.incarnation);
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
    foreach (var entry in this.rootIncarnation.incarnationsStaircaseTTC) {
      result += GetStaircaseTTCHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsDecorativeTTC) {
      result += GetDecorativeTTCHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsManaPotion) {
      result += GetManaPotionHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsHealthPotion) {
      result += GetHealthPotionHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsInertiaRing) {
      result += GetInertiaRingHash(entry.Key, entry.Value.version, entry.Value.incarnation);
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
    foreach (var entry in this.rootIncarnation.incarnationsCounteringUC) {
      result += GetCounteringUCHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsShieldingUC) {
      result += GetShieldingUCHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsEvaporateImpulse) {
      result += GetEvaporateImpulseHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsTimeScriptDirectiveUC) {
      result += GetTimeScriptDirectiveUCHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsTimeCloneAICapabilityUC) {
      result += GetTimeCloneAICapabilityUCHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsBidingOperationUC) {
      result += GetBidingOperationUCHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsUnleashBideImpulse) {
      result += GetUnleashBideImpulseHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsContinueBidingImpulse) {
      result += GetContinueBidingImpulseHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsStartBidingImpulse) {
      result += GetStartBidingImpulseHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsBideAICapabilityUC) {
      result += GetBideAICapabilityUCHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsFireImpulse) {
      result += GetFireImpulseHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsCounterImpulse) {
      result += GetCounterImpulseHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsDefendImpulse) {
      result += GetDefendImpulseHash(entry.Key, entry.Value.version, entry.Value.incarnation);
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
    foreach (var entry in this.rootIncarnation.incarnationsIRequestMutList) {
      result += GetIRequestMutListHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsLevelMutSet) {
      result += GetLevelMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsCounteringUCWeakMutSet) {
      result += GetCounteringUCWeakMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsShieldingUCWeakMutSet) {
      result += GetShieldingUCWeakMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsAttackAICapabilityUCWeakMutSet) {
      result += GetAttackAICapabilityUCWeakMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsTimeCloneAICapabilityUCWeakMutSet) {
      result += GetTimeCloneAICapabilityUCWeakMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsArmorMutSet) {
      result += GetArmorMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsInertiaRingMutSet) {
      result += GetInertiaRingMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsGlaiveMutSet) {
      result += GetGlaiveMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsManaPotionMutSet) {
      result += GetManaPotionMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsHealthPotionMutSet) {
      result += GetHealthPotionMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsTimeScriptDirectiveUCMutSet) {
      result += GetTimeScriptDirectiveUCMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
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
    foreach (var entry in this.rootIncarnation.incarnationsTimeCloneAICapabilityUCMutSet) {
      result += GetTimeCloneAICapabilityUCMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsAttackAICapabilityUCMutSet) {
      result += GetAttackAICapabilityUCMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsCounteringUCMutSet) {
      result += GetCounteringUCMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsShieldingUCMutSet) {
      result += GetShieldingUCMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsBidingOperationUCMutSet) {
      result += GetBidingOperationUCMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsTimeAnchorTTCMutSet) {
      result += GetTimeAnchorTTCMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsStaircaseTTCMutSet) {
      result += GetStaircaseTTCMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsDecorativeTTCMutSet) {
      result += GetDecorativeTTCMutSetHash(entry.Key, entry.Value.version, entry.Value.incarnation);
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

    foreach (var obj in this.AllSquareCaveLevelController()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllRidgeLevelController()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllGauntletLevelController()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllPreGauntletLevelController()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllRavashrikeLevelController()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllPentagonalCaveLevelController()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllCliffLevelController()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllLevel()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllTimeAnchorTTC()) {
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
    foreach (var obj in this.AllStaircaseTTC()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllDecorativeTTC()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllManaPotion()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllHealthPotion()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllInertiaRing()) {
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
    foreach (var obj in this.AllCounteringUC()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllShieldingUC()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllEvaporateImpulse()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllTimeScriptDirectiveUC()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllTimeCloneAICapabilityUC()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllBidingOperationUC()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllUnleashBideImpulse()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllContinueBidingImpulse()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllStartBidingImpulse()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllBideAICapabilityUC()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllFireImpulse()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllCounterImpulse()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllDefendImpulse()) {
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
    foreach (var obj in this.AllIRequestMutList()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllLevelMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllCounteringUCWeakMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllShieldingUCWeakMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllAttackAICapabilityUCWeakMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllTimeCloneAICapabilityUCWeakMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllArmorMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllInertiaRingMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllGlaiveMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllManaPotionMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllHealthPotionMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllTimeScriptDirectiveUCMutSet()) {
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
    foreach (var obj in this.AllTimeCloneAICapabilityUCMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllAttackAICapabilityUCMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllCounteringUCMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllShieldingUCMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllBidingOperationUCMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllTimeAnchorTTCMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllStaircaseTTCMutSet()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllDecorativeTTCMutSet()) {
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
    foreach (var obj in this.AllSquareCaveLevelController()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllRidgeLevelController()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllGauntletLevelController()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllPreGauntletLevelController()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllRavashrikeLevelController()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllPentagonalCaveLevelController()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllCliffLevelController()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllLevel()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllTimeAnchorTTC()) {
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
    foreach (var obj in this.AllStaircaseTTC()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllDecorativeTTC()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllManaPotion()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllHealthPotion()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllInertiaRing()) {
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
    foreach (var obj in this.AllCounteringUC()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllShieldingUC()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllEvaporateImpulse()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllTimeScriptDirectiveUC()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllTimeCloneAICapabilityUC()) {
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
    foreach (var obj in this.AllContinueBidingImpulse()) {
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
    foreach (var obj in this.AllFireImpulse()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllCounterImpulse()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllDefendImpulse()) {
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
    foreach (var obj in this.AllIRequestMutList()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllLevelMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllCounteringUCWeakMutSet()) {
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
    foreach (var obj in this.AllTimeCloneAICapabilityUCWeakMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllArmorMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllInertiaRingMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllGlaiveMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllManaPotionMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllHealthPotionMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllTimeScriptDirectiveUCMutSet()) {
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
    foreach (var obj in this.AllTimeCloneAICapabilityUCMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllAttackAICapabilityUCMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllCounteringUCMutSet()) {
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
    foreach (var obj in this.AllTimeAnchorTTCMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllStaircaseTTCMutSet()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllDecorativeTTCMutSet()) {
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
    var copyOfObserversForSquareCaveLevelController =
        new SortedDictionary<int, List<ISquareCaveLevelControllerEffectObserver>>();
    foreach (var entry in observersForSquareCaveLevelController) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForSquareCaveLevelController.Add(
          objectId,
          new List<ISquareCaveLevelControllerEffectObserver>(
              observers));
    }

    var copyOfObserversForRidgeLevelController =
        new SortedDictionary<int, List<IRidgeLevelControllerEffectObserver>>();
    foreach (var entry in observersForRidgeLevelController) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForRidgeLevelController.Add(
          objectId,
          new List<IRidgeLevelControllerEffectObserver>(
              observers));
    }

    var copyOfObserversForGauntletLevelController =
        new SortedDictionary<int, List<IGauntletLevelControllerEffectObserver>>();
    foreach (var entry in observersForGauntletLevelController) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForGauntletLevelController.Add(
          objectId,
          new List<IGauntletLevelControllerEffectObserver>(
              observers));
    }

    var copyOfObserversForPreGauntletLevelController =
        new SortedDictionary<int, List<IPreGauntletLevelControllerEffectObserver>>();
    foreach (var entry in observersForPreGauntletLevelController) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForPreGauntletLevelController.Add(
          objectId,
          new List<IPreGauntletLevelControllerEffectObserver>(
              observers));
    }

    var copyOfObserversForRavashrikeLevelController =
        new SortedDictionary<int, List<IRavashrikeLevelControllerEffectObserver>>();
    foreach (var entry in observersForRavashrikeLevelController) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForRavashrikeLevelController.Add(
          objectId,
          new List<IRavashrikeLevelControllerEffectObserver>(
              observers));
    }

    var copyOfObserversForPentagonalCaveLevelController =
        new SortedDictionary<int, List<IPentagonalCaveLevelControllerEffectObserver>>();
    foreach (var entry in observersForPentagonalCaveLevelController) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForPentagonalCaveLevelController.Add(
          objectId,
          new List<IPentagonalCaveLevelControllerEffectObserver>(
              observers));
    }

    var copyOfObserversForCliffLevelController =
        new SortedDictionary<int, List<ICliffLevelControllerEffectObserver>>();
    foreach (var entry in observersForCliffLevelController) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForCliffLevelController.Add(
          objectId,
          new List<ICliffLevelControllerEffectObserver>(
              observers));
    }

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

    var copyOfObserversForTimeAnchorTTC =
        new SortedDictionary<int, List<ITimeAnchorTTCEffectObserver>>();
    foreach (var entry in observersForTimeAnchorTTC) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForTimeAnchorTTC.Add(
          objectId,
          new List<ITimeAnchorTTCEffectObserver>(
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

    var copyOfObserversForStaircaseTTC =
        new SortedDictionary<int, List<IStaircaseTTCEffectObserver>>();
    foreach (var entry in observersForStaircaseTTC) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForStaircaseTTC.Add(
          objectId,
          new List<IStaircaseTTCEffectObserver>(
              observers));
    }

    var copyOfObserversForDecorativeTTC =
        new SortedDictionary<int, List<IDecorativeTTCEffectObserver>>();
    foreach (var entry in observersForDecorativeTTC) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForDecorativeTTC.Add(
          objectId,
          new List<IDecorativeTTCEffectObserver>(
              observers));
    }

    var copyOfObserversForManaPotion =
        new SortedDictionary<int, List<IManaPotionEffectObserver>>();
    foreach (var entry in observersForManaPotion) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForManaPotion.Add(
          objectId,
          new List<IManaPotionEffectObserver>(
              observers));
    }

    var copyOfObserversForHealthPotion =
        new SortedDictionary<int, List<IHealthPotionEffectObserver>>();
    foreach (var entry in observersForHealthPotion) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForHealthPotion.Add(
          objectId,
          new List<IHealthPotionEffectObserver>(
              observers));
    }

    var copyOfObserversForInertiaRing =
        new SortedDictionary<int, List<IInertiaRingEffectObserver>>();
    foreach (var entry in observersForInertiaRing) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForInertiaRing.Add(
          objectId,
          new List<IInertiaRingEffectObserver>(
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

    var copyOfObserversForCounteringUC =
        new SortedDictionary<int, List<ICounteringUCEffectObserver>>();
    foreach (var entry in observersForCounteringUC) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForCounteringUC.Add(
          objectId,
          new List<ICounteringUCEffectObserver>(
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

    var copyOfObserversForEvaporateImpulse =
        new SortedDictionary<int, List<IEvaporateImpulseEffectObserver>>();
    foreach (var entry in observersForEvaporateImpulse) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForEvaporateImpulse.Add(
          objectId,
          new List<IEvaporateImpulseEffectObserver>(
              observers));
    }

    var copyOfObserversForTimeScriptDirectiveUC =
        new SortedDictionary<int, List<ITimeScriptDirectiveUCEffectObserver>>();
    foreach (var entry in observersForTimeScriptDirectiveUC) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForTimeScriptDirectiveUC.Add(
          objectId,
          new List<ITimeScriptDirectiveUCEffectObserver>(
              observers));
    }

    var copyOfObserversForTimeCloneAICapabilityUC =
        new SortedDictionary<int, List<ITimeCloneAICapabilityUCEffectObserver>>();
    foreach (var entry in observersForTimeCloneAICapabilityUC) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForTimeCloneAICapabilityUC.Add(
          objectId,
          new List<ITimeCloneAICapabilityUCEffectObserver>(
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

    var copyOfObserversForContinueBidingImpulse =
        new SortedDictionary<int, List<IContinueBidingImpulseEffectObserver>>();
    foreach (var entry in observersForContinueBidingImpulse) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForContinueBidingImpulse.Add(
          objectId,
          new List<IContinueBidingImpulseEffectObserver>(
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

    var copyOfObserversForFireImpulse =
        new SortedDictionary<int, List<IFireImpulseEffectObserver>>();
    foreach (var entry in observersForFireImpulse) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForFireImpulse.Add(
          objectId,
          new List<IFireImpulseEffectObserver>(
              observers));
    }

    var copyOfObserversForCounterImpulse =
        new SortedDictionary<int, List<ICounterImpulseEffectObserver>>();
    foreach (var entry in observersForCounterImpulse) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForCounterImpulse.Add(
          objectId,
          new List<ICounterImpulseEffectObserver>(
              observers));
    }

    var copyOfObserversForDefendImpulse =
        new SortedDictionary<int, List<IDefendImpulseEffectObserver>>();
    foreach (var entry in observersForDefendImpulse) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForDefendImpulse.Add(
          objectId,
          new List<IDefendImpulseEffectObserver>(
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

    var copyOfObserversForIRequestMutList =
        new SortedDictionary<int, List<IIRequestMutListEffectObserver>>();
    foreach (var entry in observersForIRequestMutList) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForIRequestMutList.Add(
          objectId,
          new List<IIRequestMutListEffectObserver>(
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

    var copyOfObserversForCounteringUCWeakMutSet =
        new SortedDictionary<int, List<ICounteringUCWeakMutSetEffectObserver>>();
    foreach (var entry in observersForCounteringUCWeakMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForCounteringUCWeakMutSet.Add(
          objectId,
          new List<ICounteringUCWeakMutSetEffectObserver>(
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

    var copyOfObserversForTimeCloneAICapabilityUCWeakMutSet =
        new SortedDictionary<int, List<ITimeCloneAICapabilityUCWeakMutSetEffectObserver>>();
    foreach (var entry in observersForTimeCloneAICapabilityUCWeakMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForTimeCloneAICapabilityUCWeakMutSet.Add(
          objectId,
          new List<ITimeCloneAICapabilityUCWeakMutSetEffectObserver>(
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

    var copyOfObserversForInertiaRingMutSet =
        new SortedDictionary<int, List<IInertiaRingMutSetEffectObserver>>();
    foreach (var entry in observersForInertiaRingMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForInertiaRingMutSet.Add(
          objectId,
          new List<IInertiaRingMutSetEffectObserver>(
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

    var copyOfObserversForManaPotionMutSet =
        new SortedDictionary<int, List<IManaPotionMutSetEffectObserver>>();
    foreach (var entry in observersForManaPotionMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForManaPotionMutSet.Add(
          objectId,
          new List<IManaPotionMutSetEffectObserver>(
              observers));
    }

    var copyOfObserversForHealthPotionMutSet =
        new SortedDictionary<int, List<IHealthPotionMutSetEffectObserver>>();
    foreach (var entry in observersForHealthPotionMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForHealthPotionMutSet.Add(
          objectId,
          new List<IHealthPotionMutSetEffectObserver>(
              observers));
    }

    var copyOfObserversForTimeScriptDirectiveUCMutSet =
        new SortedDictionary<int, List<ITimeScriptDirectiveUCMutSetEffectObserver>>();
    foreach (var entry in observersForTimeScriptDirectiveUCMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForTimeScriptDirectiveUCMutSet.Add(
          objectId,
          new List<ITimeScriptDirectiveUCMutSetEffectObserver>(
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

    var copyOfObserversForTimeCloneAICapabilityUCMutSet =
        new SortedDictionary<int, List<ITimeCloneAICapabilityUCMutSetEffectObserver>>();
    foreach (var entry in observersForTimeCloneAICapabilityUCMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForTimeCloneAICapabilityUCMutSet.Add(
          objectId,
          new List<ITimeCloneAICapabilityUCMutSetEffectObserver>(
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

    var copyOfObserversForCounteringUCMutSet =
        new SortedDictionary<int, List<ICounteringUCMutSetEffectObserver>>();
    foreach (var entry in observersForCounteringUCMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForCounteringUCMutSet.Add(
          objectId,
          new List<ICounteringUCMutSetEffectObserver>(
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

    var copyOfObserversForTimeAnchorTTCMutSet =
        new SortedDictionary<int, List<ITimeAnchorTTCMutSetEffectObserver>>();
    foreach (var entry in observersForTimeAnchorTTCMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForTimeAnchorTTCMutSet.Add(
          objectId,
          new List<ITimeAnchorTTCMutSetEffectObserver>(
              observers));
    }

    var copyOfObserversForStaircaseTTCMutSet =
        new SortedDictionary<int, List<IStaircaseTTCMutSetEffectObserver>>();
    foreach (var entry in observersForStaircaseTTCMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForStaircaseTTCMutSet.Add(
          objectId,
          new List<IStaircaseTTCMutSetEffectObserver>(
              observers));
    }

    var copyOfObserversForDecorativeTTCMutSet =
        new SortedDictionary<int, List<IDecorativeTTCMutSetEffectObserver>>();
    foreach (var entry in observersForDecorativeTTCMutSet) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForDecorativeTTCMutSet.Add(
          objectId,
          new List<IDecorativeTTCMutSetEffectObserver>(
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

    BroadcastSquareCaveLevelControllerEffects(
        copyOfObserversForSquareCaveLevelController);
           
    BroadcastRidgeLevelControllerEffects(
        copyOfObserversForRidgeLevelController);
           
    BroadcastGauntletLevelControllerEffects(
        copyOfObserversForGauntletLevelController);
           
    BroadcastPreGauntletLevelControllerEffects(
        copyOfObserversForPreGauntletLevelController);
           
    BroadcastRavashrikeLevelControllerEffects(
        copyOfObserversForRavashrikeLevelController);
           
    BroadcastPentagonalCaveLevelControllerEffects(
        copyOfObserversForPentagonalCaveLevelController);
           
    BroadcastCliffLevelControllerEffects(
        copyOfObserversForCliffLevelController);
           
    BroadcastLevelEffects(
        copyOfObserversForLevel);
           
    BroadcastTimeAnchorTTCEffects(
        copyOfObserversForTimeAnchorTTC);
           
    BroadcastTerrainTileEffects(
        copyOfObserversForTerrainTile);
           
    BroadcastITerrainTileComponentMutBunchEffects(
        copyOfObserversForITerrainTileComponentMutBunch);
           
    BroadcastTerrainEffects(
        copyOfObserversForTerrain);
           
    BroadcastStaircaseTTCEffects(
        copyOfObserversForStaircaseTTC);
           
    BroadcastDecorativeTTCEffects(
        copyOfObserversForDecorativeTTC);
           
    BroadcastManaPotionEffects(
        copyOfObserversForManaPotion);
           
    BroadcastHealthPotionEffects(
        copyOfObserversForHealthPotion);
           
    BroadcastInertiaRingEffects(
        copyOfObserversForInertiaRing);
           
    BroadcastGlaiveEffects(
        copyOfObserversForGlaive);
           
    BroadcastArmorEffects(
        copyOfObserversForArmor);
           
    BroadcastRandEffects(
        copyOfObserversForRand);
           
    BroadcastWanderAICapabilityUCEffects(
        copyOfObserversForWanderAICapabilityUC);
           
    BroadcastCounteringUCEffects(
        copyOfObserversForCounteringUC);
           
    BroadcastShieldingUCEffects(
        copyOfObserversForShieldingUC);
           
    BroadcastEvaporateImpulseEffects(
        copyOfObserversForEvaporateImpulse);
           
    BroadcastTimeScriptDirectiveUCEffects(
        copyOfObserversForTimeScriptDirectiveUC);
           
    BroadcastTimeCloneAICapabilityUCEffects(
        copyOfObserversForTimeCloneAICapabilityUC);
           
    BroadcastBidingOperationUCEffects(
        copyOfObserversForBidingOperationUC);
           
    BroadcastUnleashBideImpulseEffects(
        copyOfObserversForUnleashBideImpulse);
           
    BroadcastContinueBidingImpulseEffects(
        copyOfObserversForContinueBidingImpulse);
           
    BroadcastStartBidingImpulseEffects(
        copyOfObserversForStartBidingImpulse);
           
    BroadcastBideAICapabilityUCEffects(
        copyOfObserversForBideAICapabilityUC);
           
    BroadcastFireImpulseEffects(
        copyOfObserversForFireImpulse);
           
    BroadcastCounterImpulseEffects(
        copyOfObserversForCounterImpulse);
           
    BroadcastDefendImpulseEffects(
        copyOfObserversForDefendImpulse);
           
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
           
    BroadcastIRequestMutListEffects(
        copyOfObserversForIRequestMutList);
           
    BroadcastLevelMutSetEffects(
        copyOfObserversForLevelMutSet);
           
    BroadcastCounteringUCWeakMutSetEffects(
        copyOfObserversForCounteringUCWeakMutSet);
           
    BroadcastShieldingUCWeakMutSetEffects(
        copyOfObserversForShieldingUCWeakMutSet);
           
    BroadcastAttackAICapabilityUCWeakMutSetEffects(
        copyOfObserversForAttackAICapabilityUCWeakMutSet);
           
    BroadcastTimeCloneAICapabilityUCWeakMutSetEffects(
        copyOfObserversForTimeCloneAICapabilityUCWeakMutSet);
           
    BroadcastArmorMutSetEffects(
        copyOfObserversForArmorMutSet);
           
    BroadcastInertiaRingMutSetEffects(
        copyOfObserversForInertiaRingMutSet);
           
    BroadcastGlaiveMutSetEffects(
        copyOfObserversForGlaiveMutSet);
           
    BroadcastManaPotionMutSetEffects(
        copyOfObserversForManaPotionMutSet);
           
    BroadcastHealthPotionMutSetEffects(
        copyOfObserversForHealthPotionMutSet);
           
    BroadcastTimeScriptDirectiveUCMutSetEffects(
        copyOfObserversForTimeScriptDirectiveUCMutSet);
           
    BroadcastKillDirectiveUCMutSetEffects(
        copyOfObserversForKillDirectiveUCMutSet);
           
    BroadcastMoveDirectiveUCMutSetEffects(
        copyOfObserversForMoveDirectiveUCMutSet);
           
    BroadcastWanderAICapabilityUCMutSetEffects(
        copyOfObserversForWanderAICapabilityUCMutSet);
           
    BroadcastBideAICapabilityUCMutSetEffects(
        copyOfObserversForBideAICapabilityUCMutSet);
           
    BroadcastTimeCloneAICapabilityUCMutSetEffects(
        copyOfObserversForTimeCloneAICapabilityUCMutSet);
           
    BroadcastAttackAICapabilityUCMutSetEffects(
        copyOfObserversForAttackAICapabilityUCMutSet);
           
    BroadcastCounteringUCMutSetEffects(
        copyOfObserversForCounteringUCMutSet);
           
    BroadcastShieldingUCMutSetEffects(
        copyOfObserversForShieldingUCMutSet);
           
    BroadcastBidingOperationUCMutSetEffects(
        copyOfObserversForBidingOperationUCMutSet);
           
    BroadcastTimeAnchorTTCMutSetEffects(
        copyOfObserversForTimeAnchorTTCMutSet);
           
    BroadcastStaircaseTTCMutSetEffects(
        copyOfObserversForStaircaseTTCMutSet);
           
    BroadcastDecorativeTTCMutSetEffects(
        copyOfObserversForDecorativeTTCMutSet);
           
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


    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsSquareCaveLevelController) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsSquareCaveLevelController.ContainsKey(sourceObjId)) {
        EffectInternalCreateSquareCaveLevelController(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsRidgeLevelController) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsRidgeLevelController.ContainsKey(sourceObjId)) {
        EffectInternalCreateRidgeLevelController(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsGauntletLevelController) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsGauntletLevelController.ContainsKey(sourceObjId)) {
        EffectInternalCreateGauntletLevelController(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsPreGauntletLevelController) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsPreGauntletLevelController.ContainsKey(sourceObjId)) {
        EffectInternalCreatePreGauntletLevelController(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsRavashrikeLevelController) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsRavashrikeLevelController.ContainsKey(sourceObjId)) {
        EffectInternalCreateRavashrikeLevelController(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsPentagonalCaveLevelController) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsPentagonalCaveLevelController.ContainsKey(sourceObjId)) {
        EffectInternalCreatePentagonalCaveLevelController(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsCliffLevelController) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsCliffLevelController.ContainsKey(sourceObjId)) {
        EffectInternalCreateCliffLevelController(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsLevel) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsLevel.ContainsKey(sourceObjId)) {
        EffectInternalCreateLevel(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTimeAnchorTTC) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsTimeAnchorTTC.ContainsKey(sourceObjId)) {
        EffectInternalCreateTimeAnchorTTC(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
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
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsStaircaseTTC) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsStaircaseTTC.ContainsKey(sourceObjId)) {
        EffectInternalCreateStaircaseTTC(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDecorativeTTC) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsDecorativeTTC.ContainsKey(sourceObjId)) {
        EffectInternalCreateDecorativeTTC(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsManaPotion) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsManaPotion.ContainsKey(sourceObjId)) {
        EffectInternalCreateManaPotion(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsHealthPotion) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsHealthPotion.ContainsKey(sourceObjId)) {
        EffectInternalCreateHealthPotion(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsInertiaRing) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsInertiaRing.ContainsKey(sourceObjId)) {
        EffectInternalCreateInertiaRing(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
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
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsCounteringUC) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsCounteringUC.ContainsKey(sourceObjId)) {
        EffectInternalCreateCounteringUC(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
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
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsEvaporateImpulse) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsEvaporateImpulse.ContainsKey(sourceObjId)) {
        EffectInternalCreateEvaporateImpulse(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTimeScriptDirectiveUC) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsTimeScriptDirectiveUC.ContainsKey(sourceObjId)) {
        EffectInternalCreateTimeScriptDirectiveUC(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTimeCloneAICapabilityUC) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsTimeCloneAICapabilityUC.ContainsKey(sourceObjId)) {
        EffectInternalCreateTimeCloneAICapabilityUC(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
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
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsContinueBidingImpulse) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsContinueBidingImpulse.ContainsKey(sourceObjId)) {
        EffectInternalCreateContinueBidingImpulse(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
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
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsFireImpulse) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsFireImpulse.ContainsKey(sourceObjId)) {
        EffectInternalCreateFireImpulse(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsCounterImpulse) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsCounterImpulse.ContainsKey(sourceObjId)) {
        EffectInternalCreateCounterImpulse(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDefendImpulse) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsDefendImpulse.ContainsKey(sourceObjId)) {
        EffectInternalCreateDefendImpulse(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
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
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIRequestMutList) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsIRequestMutList.ContainsKey(sourceObjId)) {
        EffectInternalCreateIRequestMutList(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
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
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsCounteringUCWeakMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsCounteringUCWeakMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateCounteringUCWeakMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
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
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTimeCloneAICapabilityUCWeakMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsTimeCloneAICapabilityUCWeakMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateTimeCloneAICapabilityUCWeakMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
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
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsInertiaRingMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsInertiaRingMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateInertiaRingMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
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
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsManaPotionMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsManaPotionMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateManaPotionMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsHealthPotionMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsHealthPotionMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateHealthPotionMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTimeScriptDirectiveUCMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsTimeScriptDirectiveUCMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateTimeScriptDirectiveUCMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
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
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTimeCloneAICapabilityUCMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsTimeCloneAICapabilityUCMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateTimeCloneAICapabilityUCMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
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
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsCounteringUCMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsCounteringUCMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateCounteringUCMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
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
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTimeAnchorTTCMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsTimeAnchorTTCMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateTimeAnchorTTCMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsStaircaseTTCMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsStaircaseTTCMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateStaircaseTTCMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDecorativeTTCMutSet) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsDecorativeTTCMutSet.ContainsKey(sourceObjId)) {
        EffectInternalCreateDecorativeTTCMutSet(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
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
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsIRequestMutList) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsIRequestMutList.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsIRequestMutList[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            for (int i = currentObjIncarnation.list.Count - 1; i >= 0; i--) {
              EffectIRequestMutListRemoveAt(objId, i);
            }
            foreach (var objIdInSourceObjIncarnation in sourceObjIncarnation.list) {
              EffectIRequestMutListAdd(objId, objIdInSourceObjIncarnation);
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
                  rootIncarnation.incarnationsIRequestMutList[objId] = sourceVersionAndObjIncarnation;

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
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
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
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsCounteringUCWeakMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsCounteringUCWeakMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsCounteringUCWeakMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectCounteringUCWeakMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectCounteringUCWeakMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.incarnationsCounteringUCWeakMutSet[objId] = sourceVersionAndObjIncarnation;
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
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
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
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
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
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTimeCloneAICapabilityUCWeakMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsTimeCloneAICapabilityUCWeakMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsTimeCloneAICapabilityUCWeakMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectTimeCloneAICapabilityUCWeakMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectTimeCloneAICapabilityUCWeakMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.incarnationsTimeCloneAICapabilityUCWeakMutSet[objId] = sourceVersionAndObjIncarnation;
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
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
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
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsInertiaRingMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsInertiaRingMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsInertiaRingMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectInertiaRingMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectInertiaRingMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.incarnationsInertiaRingMutSet[objId] = sourceVersionAndObjIncarnation;
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
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
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
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsManaPotionMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsManaPotionMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsManaPotionMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectManaPotionMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectManaPotionMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.incarnationsManaPotionMutSet[objId] = sourceVersionAndObjIncarnation;
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsHealthPotionMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsHealthPotionMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsHealthPotionMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectHealthPotionMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectHealthPotionMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.incarnationsHealthPotionMutSet[objId] = sourceVersionAndObjIncarnation;
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTimeScriptDirectiveUCMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsTimeScriptDirectiveUCMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsTimeScriptDirectiveUCMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectTimeScriptDirectiveUCMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectTimeScriptDirectiveUCMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.incarnationsTimeScriptDirectiveUCMutSet[objId] = sourceVersionAndObjIncarnation;
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
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
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
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
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
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
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
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
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
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTimeCloneAICapabilityUCMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsTimeCloneAICapabilityUCMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsTimeCloneAICapabilityUCMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectTimeCloneAICapabilityUCMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectTimeCloneAICapabilityUCMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.incarnationsTimeCloneAICapabilityUCMutSet[objId] = sourceVersionAndObjIncarnation;
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
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
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
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsCounteringUCMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsCounteringUCMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsCounteringUCMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectCounteringUCMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectCounteringUCMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.incarnationsCounteringUCMutSet[objId] = sourceVersionAndObjIncarnation;
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
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
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
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
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
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTimeAnchorTTCMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsTimeAnchorTTCMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsTimeAnchorTTCMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectTimeAnchorTTCMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectTimeAnchorTTCMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.incarnationsTimeAnchorTTCMutSet[objId] = sourceVersionAndObjIncarnation;
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsStaircaseTTCMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsStaircaseTTCMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsStaircaseTTCMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectStaircaseTTCMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectStaircaseTTCMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.incarnationsStaircaseTTCMutSet[objId] = sourceVersionAndObjIncarnation;
          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDecorativeTTCMutSet) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsDecorativeTTCMutSet.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsDecorativeTTCMutSet[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
                EffectDecorativeTTCMutSetRemove(objId, objIdInCurrentObjIncarnation);
              }
            }
            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
                EffectDecorativeTTCMutSetAdd(objId, unitIdInSourceObjIncarnation);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.incarnationsDecorativeTTCMutSet[objId] = sourceVersionAndObjIncarnation;
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
            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
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
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTerrainTileByLocationMutMap) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsTerrainTileByLocationMutMap.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsTerrainTileByLocationMutMap[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var entryInCurrentObjIncarnation in new SortedDictionary<Location, int>(currentObjIncarnation.map)) {
              var key = entryInCurrentObjIncarnation.Key;
              if (!sourceObjIncarnation.map.ContainsKey(key)) {
                EffectTerrainTileByLocationMutMapRemove(objId, key);
              }
            }
            foreach (var entryInSourceObjIncarnation in sourceObjIncarnation.map) {
              var key = entryInSourceObjIncarnation.Key;
              var element = entryInSourceObjIncarnation.Value;
              if (!currentObjIncarnation.map.ContainsKey(key)) {
                EffectTerrainTileByLocationMutMapAdd(objId, key, element);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.incarnationsTerrainTileByLocationMutMap[objId] = sourceVersionAndObjIncarnation;
          }
        }
      }
             
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsSquareCaveLevelController) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsSquareCaveLevelController.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsSquareCaveLevelController[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsSquareCaveLevelController[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsRidgeLevelController) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsRidgeLevelController.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsRidgeLevelController[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsRidgeLevelController[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsGauntletLevelController) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsGauntletLevelController.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsGauntletLevelController[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsGauntletLevelController[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsPreGauntletLevelController) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsPreGauntletLevelController.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsPreGauntletLevelController[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsPreGauntletLevelController[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsRavashrikeLevelController) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsRavashrikeLevelController.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsRavashrikeLevelController[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsRavashrikeLevelController[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsPentagonalCaveLevelController) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsPentagonalCaveLevelController.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsPentagonalCaveLevelController[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsPentagonalCaveLevelController[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsCliffLevelController) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsCliffLevelController.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsCliffLevelController[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsCliffLevelController[objId] = sourceVersionAndObjIncarnation;
          
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

          if (sourceObjIncarnation.controller != currentObjIncarnation.controller) {
            EffectLevelSetController(objId, GetILevelController(sourceObjIncarnation.controller));
          }

          if (sourceObjIncarnation.time != currentObjIncarnation.time) {
            EffectLevelSetTime(objId, sourceObjIncarnation.time);
          }

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsLevel[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTimeAnchorTTC) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsTimeAnchorTTC.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsTimeAnchorTTC[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsTimeAnchorTTC[objId] = sourceVersionAndObjIncarnation;
          
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

          if (sourceObjIncarnation.walkable != currentObjIncarnation.walkable) {
            EffectTerrainTileSetWalkable(objId, sourceObjIncarnation.walkable);
          }

          if (sourceObjIncarnation.classId != currentObjIncarnation.classId) {
            EffectTerrainTileSetClassId(objId, sourceObjIncarnation.classId);
          }

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsTerrainTile[objId] = sourceVersionAndObjIncarnation;
          
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
          
          rootIncarnation.incarnationsITerrainTileComponentMutBunch[objId] = sourceVersionAndObjIncarnation;
          
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

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsStaircaseTTC) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsStaircaseTTC.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsStaircaseTTC[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          if (sourceObjIncarnation.destinationLevel != currentObjIncarnation.destinationLevel) {
            EffectStaircaseTTCSetDestinationLevel(objId, new Level(this, sourceObjIncarnation.destinationLevel));
          }

          if (sourceObjIncarnation.destinationLevelPortalIndex != currentObjIncarnation.destinationLevelPortalIndex) {
            EffectStaircaseTTCSetDestinationLevelPortalIndex(objId, sourceObjIncarnation.destinationLevelPortalIndex);
          }

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsStaircaseTTC[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDecorativeTTC) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsDecorativeTTC.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsDecorativeTTC[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsDecorativeTTC[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsManaPotion) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsManaPotion.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsManaPotion[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsManaPotion[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsHealthPotion) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsHealthPotion.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsHealthPotion[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsHealthPotion[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsInertiaRing) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsInertiaRing.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsInertiaRing[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsInertiaRing[objId] = sourceVersionAndObjIncarnation;
          
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
          
          rootIncarnation.incarnationsWanderAICapabilityUC[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsCounteringUC) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsCounteringUC.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsCounteringUC[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsCounteringUC[objId] = sourceVersionAndObjIncarnation;
          
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
          
          rootIncarnation.incarnationsShieldingUC[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsEvaporateImpulse) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsEvaporateImpulse.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsEvaporateImpulse[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsEvaporateImpulse[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTimeScriptDirectiveUC) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsTimeScriptDirectiveUC.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsTimeScriptDirectiveUC[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsTimeScriptDirectiveUC[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTimeCloneAICapabilityUC) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsTimeCloneAICapabilityUC.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsTimeCloneAICapabilityUC[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsTimeCloneAICapabilityUC[objId] = sourceVersionAndObjIncarnation;
          
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

          if (sourceObjIncarnation.charge != currentObjIncarnation.charge) {
            EffectBidingOperationUCSetCharge(objId, sourceObjIncarnation.charge);
          }

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsBidingOperationUC[objId] = sourceVersionAndObjIncarnation;
          
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
          
          rootIncarnation.incarnationsUnleashBideImpulse[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsContinueBidingImpulse) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsContinueBidingImpulse.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsContinueBidingImpulse[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsContinueBidingImpulse[objId] = sourceVersionAndObjIncarnation;
          
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
          
          rootIncarnation.incarnationsStartBidingImpulse[objId] = sourceVersionAndObjIncarnation;
          
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
          
          rootIncarnation.incarnationsBideAICapabilityUC[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsFireImpulse) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsFireImpulse.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsFireImpulse[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsFireImpulse[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsCounterImpulse) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsCounterImpulse.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsCounterImpulse[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsCounterImpulse[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsDefendImpulse) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsDefendImpulse.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsDefendImpulse[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsDefendImpulse[objId] = sourceVersionAndObjIncarnation;
          
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
          
          rootIncarnation.incarnationsAttackImpulse[objId] = sourceVersionAndObjIncarnation;
          
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
          
          rootIncarnation.incarnationsPursueImpulse[objId] = sourceVersionAndObjIncarnation;
          
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
          
          rootIncarnation.incarnationsKillDirectiveUC[objId] = sourceVersionAndObjIncarnation;
          
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
          
          rootIncarnation.incarnationsAttackAICapabilityUC[objId] = sourceVersionAndObjIncarnation;
          
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
          
          rootIncarnation.incarnationsMoveImpulse[objId] = sourceVersionAndObjIncarnation;
          
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
          
          rootIncarnation.incarnationsMoveDirectiveUC[objId] = sourceVersionAndObjIncarnation;
          
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
          
          rootIncarnation.incarnationsUnit[objId] = sourceVersionAndObjIncarnation;
          
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
          
          rootIncarnation.incarnationsIUnitComponentMutBunch[objId] = sourceVersionAndObjIncarnation;
          
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
          
          rootIncarnation.incarnationsNoImpulse[objId] = sourceVersionAndObjIncarnation;
          
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
          
          rootIncarnation.incarnationsExecutionState[objId] = sourceVersionAndObjIncarnation;
          
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
          
          rootIncarnation.incarnationsIPostActingUCWeakMutBunch[objId] = sourceVersionAndObjIncarnation;
          
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
          
          rootIncarnation.incarnationsIPreActingUCWeakMutBunch[objId] = sourceVersionAndObjIncarnation;
          
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

          if (sourceObjIncarnation.lastPlayerRequest != currentObjIncarnation.lastPlayerRequest) {
            EffectGameSetLastPlayerRequest(objId, sourceObjIncarnation.lastPlayerRequest);
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

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<SquareCaveLevelControllerIncarnation>>(rootIncarnation.incarnationsSquareCaveLevelController)) {
      if (!sourceIncarnation.incarnationsSquareCaveLevelController.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectSquareCaveLevelControllerDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<RidgeLevelControllerIncarnation>>(rootIncarnation.incarnationsRidgeLevelController)) {
      if (!sourceIncarnation.incarnationsRidgeLevelController.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectRidgeLevelControllerDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<GauntletLevelControllerIncarnation>>(rootIncarnation.incarnationsGauntletLevelController)) {
      if (!sourceIncarnation.incarnationsGauntletLevelController.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectGauntletLevelControllerDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<PreGauntletLevelControllerIncarnation>>(rootIncarnation.incarnationsPreGauntletLevelController)) {
      if (!sourceIncarnation.incarnationsPreGauntletLevelController.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectPreGauntletLevelControllerDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<RavashrikeLevelControllerIncarnation>>(rootIncarnation.incarnationsRavashrikeLevelController)) {
      if (!sourceIncarnation.incarnationsRavashrikeLevelController.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectRavashrikeLevelControllerDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<PentagonalCaveLevelControllerIncarnation>>(rootIncarnation.incarnationsPentagonalCaveLevelController)) {
      if (!sourceIncarnation.incarnationsPentagonalCaveLevelController.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectPentagonalCaveLevelControllerDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<CliffLevelControllerIncarnation>>(rootIncarnation.incarnationsCliffLevelController)) {
      if (!sourceIncarnation.incarnationsCliffLevelController.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectCliffLevelControllerDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<LevelIncarnation>>(rootIncarnation.incarnationsLevel)) {
      if (!sourceIncarnation.incarnationsLevel.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectLevelDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<TimeAnchorTTCIncarnation>>(rootIncarnation.incarnationsTimeAnchorTTC)) {
      if (!sourceIncarnation.incarnationsTimeAnchorTTC.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectTimeAnchorTTCDelete(id);
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

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<StaircaseTTCIncarnation>>(rootIncarnation.incarnationsStaircaseTTC)) {
      if (!sourceIncarnation.incarnationsStaircaseTTC.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectStaircaseTTCDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<DecorativeTTCIncarnation>>(rootIncarnation.incarnationsDecorativeTTC)) {
      if (!sourceIncarnation.incarnationsDecorativeTTC.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectDecorativeTTCDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<ManaPotionIncarnation>>(rootIncarnation.incarnationsManaPotion)) {
      if (!sourceIncarnation.incarnationsManaPotion.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectManaPotionDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<HealthPotionIncarnation>>(rootIncarnation.incarnationsHealthPotion)) {
      if (!sourceIncarnation.incarnationsHealthPotion.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectHealthPotionDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<InertiaRingIncarnation>>(rootIncarnation.incarnationsInertiaRing)) {
      if (!sourceIncarnation.incarnationsInertiaRing.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectInertiaRingDelete(id);
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

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<CounteringUCIncarnation>>(rootIncarnation.incarnationsCounteringUC)) {
      if (!sourceIncarnation.incarnationsCounteringUC.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectCounteringUCDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<ShieldingUCIncarnation>>(rootIncarnation.incarnationsShieldingUC)) {
      if (!sourceIncarnation.incarnationsShieldingUC.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectShieldingUCDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<EvaporateImpulseIncarnation>>(rootIncarnation.incarnationsEvaporateImpulse)) {
      if (!sourceIncarnation.incarnationsEvaporateImpulse.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectEvaporateImpulseDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<TimeScriptDirectiveUCIncarnation>>(rootIncarnation.incarnationsTimeScriptDirectiveUC)) {
      if (!sourceIncarnation.incarnationsTimeScriptDirectiveUC.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectTimeScriptDirectiveUCDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<TimeCloneAICapabilityUCIncarnation>>(rootIncarnation.incarnationsTimeCloneAICapabilityUC)) {
      if (!sourceIncarnation.incarnationsTimeCloneAICapabilityUC.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectTimeCloneAICapabilityUCDelete(id);
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

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<ContinueBidingImpulseIncarnation>>(rootIncarnation.incarnationsContinueBidingImpulse)) {
      if (!sourceIncarnation.incarnationsContinueBidingImpulse.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectContinueBidingImpulseDelete(id);
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

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<FireImpulseIncarnation>>(rootIncarnation.incarnationsFireImpulse)) {
      if (!sourceIncarnation.incarnationsFireImpulse.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectFireImpulseDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<CounterImpulseIncarnation>>(rootIncarnation.incarnationsCounterImpulse)) {
      if (!sourceIncarnation.incarnationsCounterImpulse.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectCounterImpulseDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<DefendImpulseIncarnation>>(rootIncarnation.incarnationsDefendImpulse)) {
      if (!sourceIncarnation.incarnationsDefendImpulse.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectDefendImpulseDelete(id);
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

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<IRequestMutListIncarnation>>(rootIncarnation.incarnationsIRequestMutList)) {
      if (!sourceIncarnation.incarnationsIRequestMutList.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectIRequestMutListDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<LevelMutSetIncarnation>>(rootIncarnation.incarnationsLevelMutSet)) {
      if (!sourceIncarnation.incarnationsLevelMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectLevelMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<CounteringUCWeakMutSetIncarnation>>(rootIncarnation.incarnationsCounteringUCWeakMutSet)) {
      if (!sourceIncarnation.incarnationsCounteringUCWeakMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectCounteringUCWeakMutSetDelete(id);
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

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<TimeCloneAICapabilityUCWeakMutSetIncarnation>>(rootIncarnation.incarnationsTimeCloneAICapabilityUCWeakMutSet)) {
      if (!sourceIncarnation.incarnationsTimeCloneAICapabilityUCWeakMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectTimeCloneAICapabilityUCWeakMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<ArmorMutSetIncarnation>>(rootIncarnation.incarnationsArmorMutSet)) {
      if (!sourceIncarnation.incarnationsArmorMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectArmorMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<InertiaRingMutSetIncarnation>>(rootIncarnation.incarnationsInertiaRingMutSet)) {
      if (!sourceIncarnation.incarnationsInertiaRingMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectInertiaRingMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<GlaiveMutSetIncarnation>>(rootIncarnation.incarnationsGlaiveMutSet)) {
      if (!sourceIncarnation.incarnationsGlaiveMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectGlaiveMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<ManaPotionMutSetIncarnation>>(rootIncarnation.incarnationsManaPotionMutSet)) {
      if (!sourceIncarnation.incarnationsManaPotionMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectManaPotionMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<HealthPotionMutSetIncarnation>>(rootIncarnation.incarnationsHealthPotionMutSet)) {
      if (!sourceIncarnation.incarnationsHealthPotionMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectHealthPotionMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<TimeScriptDirectiveUCMutSetIncarnation>>(rootIncarnation.incarnationsTimeScriptDirectiveUCMutSet)) {
      if (!sourceIncarnation.incarnationsTimeScriptDirectiveUCMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectTimeScriptDirectiveUCMutSetDelete(id);
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

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<TimeCloneAICapabilityUCMutSetIncarnation>>(rootIncarnation.incarnationsTimeCloneAICapabilityUCMutSet)) {
      if (!sourceIncarnation.incarnationsTimeCloneAICapabilityUCMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectTimeCloneAICapabilityUCMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<AttackAICapabilityUCMutSetIncarnation>>(rootIncarnation.incarnationsAttackAICapabilityUCMutSet)) {
      if (!sourceIncarnation.incarnationsAttackAICapabilityUCMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectAttackAICapabilityUCMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<CounteringUCMutSetIncarnation>>(rootIncarnation.incarnationsCounteringUCMutSet)) {
      if (!sourceIncarnation.incarnationsCounteringUCMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectCounteringUCMutSetDelete(id);
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

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<TimeAnchorTTCMutSetIncarnation>>(rootIncarnation.incarnationsTimeAnchorTTCMutSet)) {
      if (!sourceIncarnation.incarnationsTimeAnchorTTCMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectTimeAnchorTTCMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<StaircaseTTCMutSetIncarnation>>(rootIncarnation.incarnationsStaircaseTTCMutSet)) {
      if (!sourceIncarnation.incarnationsStaircaseTTCMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectStaircaseTTCMutSetDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<DecorativeTTCMutSetIncarnation>>(rootIncarnation.incarnationsDecorativeTTCMutSet)) {
      if (!sourceIncarnation.incarnationsDecorativeTTCMutSet.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectDecorativeTTCMutSetDelete(id);
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
       public SquareCaveLevelControllerIncarnation GetSquareCaveLevelControllerIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsSquareCaveLevelController[id].incarnation;
  }
  public bool SquareCaveLevelControllerExists(int id) {
    return rootIncarnation.incarnationsSquareCaveLevelController.ContainsKey(id);
  }
  public SquareCaveLevelController GetSquareCaveLevelController(int id) {
    return new SquareCaveLevelController(this, id);
  }
  public List<SquareCaveLevelController> AllSquareCaveLevelController() {
    List<SquareCaveLevelController> result = new List<SquareCaveLevelController>(rootIncarnation.incarnationsSquareCaveLevelController.Count);
    foreach (var id in rootIncarnation.incarnationsSquareCaveLevelController.Keys) {
      result.Add(new SquareCaveLevelController(this, id));
    }
    return result;
  }
  public IEnumerator<SquareCaveLevelController> EnumAllSquareCaveLevelController() {
    foreach (var id in rootIncarnation.incarnationsSquareCaveLevelController.Keys) {
      yield return GetSquareCaveLevelController(id);
    }
  }
  public void CheckHasSquareCaveLevelController(SquareCaveLevelController thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasSquareCaveLevelController(thing.id);
  }
  public void CheckHasSquareCaveLevelController(int id) {
    if (!rootIncarnation.incarnationsSquareCaveLevelController.ContainsKey(id)) {
      throw new System.Exception("Invalid SquareCaveLevelController: " + id);
    }
  }
  public void AddSquareCaveLevelControllerObserver(int id, ISquareCaveLevelControllerEffectObserver observer) {
    List<ISquareCaveLevelControllerEffectObserver> obsies;
    if (!observersForSquareCaveLevelController.TryGetValue(id, out obsies)) {
      obsies = new List<ISquareCaveLevelControllerEffectObserver>();
    }
    obsies.Add(observer);
    observersForSquareCaveLevelController[id] = obsies;
  }

  public void RemoveSquareCaveLevelControllerObserver(int id, ISquareCaveLevelControllerEffectObserver observer) {
    if (observersForSquareCaveLevelController.ContainsKey(id)) {
      var list = observersForSquareCaveLevelController[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForSquareCaveLevelController.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public SquareCaveLevelController EffectSquareCaveLevelControllerCreate(
      Level level,
      int depth) {
    CheckUnlocked();
    CheckHasLevel(level);

    var id = NewId();
    var incarnation =
        new SquareCaveLevelControllerIncarnation(
            level.id,
            depth
            );
    EffectInternalCreateSquareCaveLevelController(id, rootIncarnation.version, incarnation);
    return new SquareCaveLevelController(this, id);
  }
  public void EffectInternalCreateSquareCaveLevelController(
      int id,
      int incarnationVersion,
      SquareCaveLevelControllerIncarnation incarnation) {
    CheckUnlocked();
    var effect = new SquareCaveLevelControllerCreateEffect(id);
    rootIncarnation.incarnationsSquareCaveLevelController.Add(
        id,
        new VersionAndIncarnation<SquareCaveLevelControllerIncarnation>(
            incarnationVersion,
            incarnation));
    effectsSquareCaveLevelControllerCreateEffect.Add(effect);
  }

  public void EffectSquareCaveLevelControllerDelete(int id) {
    CheckUnlocked();
    var effect = new SquareCaveLevelControllerDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsSquareCaveLevelController[id];

    rootIncarnation.incarnationsSquareCaveLevelController.Remove(id);
    effectsSquareCaveLevelControllerDeleteEffect.Add(effect);
  }

     
  public int GetSquareCaveLevelControllerHash(int id, int version, SquareCaveLevelControllerIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.level.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.depth.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastSquareCaveLevelControllerEffects(
      SortedDictionary<int, List<ISquareCaveLevelControllerEffectObserver>> observers) {
    foreach (var effect in effectsSquareCaveLevelControllerDeleteEffect) {
      if (observers.TryGetValue(0, out List<ISquareCaveLevelControllerEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnSquareCaveLevelControllerEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ISquareCaveLevelControllerEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnSquareCaveLevelControllerEffect(effect);
        }
        observersForSquareCaveLevelController.Remove(effect.id);
      }
    }
    effectsSquareCaveLevelControllerDeleteEffect.Clear();


    foreach (var effect in effectsSquareCaveLevelControllerCreateEffect) {
      if (observers.TryGetValue(0, out List<ISquareCaveLevelControllerEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnSquareCaveLevelControllerEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ISquareCaveLevelControllerEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnSquareCaveLevelControllerEffect(effect);
        }
      }
    }
    effectsSquareCaveLevelControllerCreateEffect.Clear();
  }
  public RidgeLevelControllerIncarnation GetRidgeLevelControllerIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsRidgeLevelController[id].incarnation;
  }
  public bool RidgeLevelControllerExists(int id) {
    return rootIncarnation.incarnationsRidgeLevelController.ContainsKey(id);
  }
  public RidgeLevelController GetRidgeLevelController(int id) {
    return new RidgeLevelController(this, id);
  }
  public List<RidgeLevelController> AllRidgeLevelController() {
    List<RidgeLevelController> result = new List<RidgeLevelController>(rootIncarnation.incarnationsRidgeLevelController.Count);
    foreach (var id in rootIncarnation.incarnationsRidgeLevelController.Keys) {
      result.Add(new RidgeLevelController(this, id));
    }
    return result;
  }
  public IEnumerator<RidgeLevelController> EnumAllRidgeLevelController() {
    foreach (var id in rootIncarnation.incarnationsRidgeLevelController.Keys) {
      yield return GetRidgeLevelController(id);
    }
  }
  public void CheckHasRidgeLevelController(RidgeLevelController thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasRidgeLevelController(thing.id);
  }
  public void CheckHasRidgeLevelController(int id) {
    if (!rootIncarnation.incarnationsRidgeLevelController.ContainsKey(id)) {
      throw new System.Exception("Invalid RidgeLevelController: " + id);
    }
  }
  public void AddRidgeLevelControllerObserver(int id, IRidgeLevelControllerEffectObserver observer) {
    List<IRidgeLevelControllerEffectObserver> obsies;
    if (!observersForRidgeLevelController.TryGetValue(id, out obsies)) {
      obsies = new List<IRidgeLevelControllerEffectObserver>();
    }
    obsies.Add(observer);
    observersForRidgeLevelController[id] = obsies;
  }

  public void RemoveRidgeLevelControllerObserver(int id, IRidgeLevelControllerEffectObserver observer) {
    if (observersForRidgeLevelController.ContainsKey(id)) {
      var list = observersForRidgeLevelController[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForRidgeLevelController.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public RidgeLevelController EffectRidgeLevelControllerCreate(
      Level level) {
    CheckUnlocked();
    CheckHasLevel(level);

    var id = NewId();
    var incarnation =
        new RidgeLevelControllerIncarnation(
            level.id
            );
    EffectInternalCreateRidgeLevelController(id, rootIncarnation.version, incarnation);
    return new RidgeLevelController(this, id);
  }
  public void EffectInternalCreateRidgeLevelController(
      int id,
      int incarnationVersion,
      RidgeLevelControllerIncarnation incarnation) {
    CheckUnlocked();
    var effect = new RidgeLevelControllerCreateEffect(id);
    rootIncarnation.incarnationsRidgeLevelController.Add(
        id,
        new VersionAndIncarnation<RidgeLevelControllerIncarnation>(
            incarnationVersion,
            incarnation));
    effectsRidgeLevelControllerCreateEffect.Add(effect);
  }

  public void EffectRidgeLevelControllerDelete(int id) {
    CheckUnlocked();
    var effect = new RidgeLevelControllerDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsRidgeLevelController[id];

    rootIncarnation.incarnationsRidgeLevelController.Remove(id);
    effectsRidgeLevelControllerDeleteEffect.Add(effect);
  }

     
  public int GetRidgeLevelControllerHash(int id, int version, RidgeLevelControllerIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.level.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastRidgeLevelControllerEffects(
      SortedDictionary<int, List<IRidgeLevelControllerEffectObserver>> observers) {
    foreach (var effect in effectsRidgeLevelControllerDeleteEffect) {
      if (observers.TryGetValue(0, out List<IRidgeLevelControllerEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnRidgeLevelControllerEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IRidgeLevelControllerEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnRidgeLevelControllerEffect(effect);
        }
        observersForRidgeLevelController.Remove(effect.id);
      }
    }
    effectsRidgeLevelControllerDeleteEffect.Clear();


    foreach (var effect in effectsRidgeLevelControllerCreateEffect) {
      if (observers.TryGetValue(0, out List<IRidgeLevelControllerEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnRidgeLevelControllerEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IRidgeLevelControllerEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnRidgeLevelControllerEffect(effect);
        }
      }
    }
    effectsRidgeLevelControllerCreateEffect.Clear();
  }
  public GauntletLevelControllerIncarnation GetGauntletLevelControllerIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsGauntletLevelController[id].incarnation;
  }
  public bool GauntletLevelControllerExists(int id) {
    return rootIncarnation.incarnationsGauntletLevelController.ContainsKey(id);
  }
  public GauntletLevelController GetGauntletLevelController(int id) {
    return new GauntletLevelController(this, id);
  }
  public List<GauntletLevelController> AllGauntletLevelController() {
    List<GauntletLevelController> result = new List<GauntletLevelController>(rootIncarnation.incarnationsGauntletLevelController.Count);
    foreach (var id in rootIncarnation.incarnationsGauntletLevelController.Keys) {
      result.Add(new GauntletLevelController(this, id));
    }
    return result;
  }
  public IEnumerator<GauntletLevelController> EnumAllGauntletLevelController() {
    foreach (var id in rootIncarnation.incarnationsGauntletLevelController.Keys) {
      yield return GetGauntletLevelController(id);
    }
  }
  public void CheckHasGauntletLevelController(GauntletLevelController thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasGauntletLevelController(thing.id);
  }
  public void CheckHasGauntletLevelController(int id) {
    if (!rootIncarnation.incarnationsGauntletLevelController.ContainsKey(id)) {
      throw new System.Exception("Invalid GauntletLevelController: " + id);
    }
  }
  public void AddGauntletLevelControllerObserver(int id, IGauntletLevelControllerEffectObserver observer) {
    List<IGauntletLevelControllerEffectObserver> obsies;
    if (!observersForGauntletLevelController.TryGetValue(id, out obsies)) {
      obsies = new List<IGauntletLevelControllerEffectObserver>();
    }
    obsies.Add(observer);
    observersForGauntletLevelController[id] = obsies;
  }

  public void RemoveGauntletLevelControllerObserver(int id, IGauntletLevelControllerEffectObserver observer) {
    if (observersForGauntletLevelController.ContainsKey(id)) {
      var list = observersForGauntletLevelController[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForGauntletLevelController.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public GauntletLevelController EffectGauntletLevelControllerCreate(
      Level level) {
    CheckUnlocked();
    CheckHasLevel(level);

    var id = NewId();
    var incarnation =
        new GauntletLevelControllerIncarnation(
            level.id
            );
    EffectInternalCreateGauntletLevelController(id, rootIncarnation.version, incarnation);
    return new GauntletLevelController(this, id);
  }
  public void EffectInternalCreateGauntletLevelController(
      int id,
      int incarnationVersion,
      GauntletLevelControllerIncarnation incarnation) {
    CheckUnlocked();
    var effect = new GauntletLevelControllerCreateEffect(id);
    rootIncarnation.incarnationsGauntletLevelController.Add(
        id,
        new VersionAndIncarnation<GauntletLevelControllerIncarnation>(
            incarnationVersion,
            incarnation));
    effectsGauntletLevelControllerCreateEffect.Add(effect);
  }

  public void EffectGauntletLevelControllerDelete(int id) {
    CheckUnlocked();
    var effect = new GauntletLevelControllerDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsGauntletLevelController[id];

    rootIncarnation.incarnationsGauntletLevelController.Remove(id);
    effectsGauntletLevelControllerDeleteEffect.Add(effect);
  }

     
  public int GetGauntletLevelControllerHash(int id, int version, GauntletLevelControllerIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.level.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastGauntletLevelControllerEffects(
      SortedDictionary<int, List<IGauntletLevelControllerEffectObserver>> observers) {
    foreach (var effect in effectsGauntletLevelControllerDeleteEffect) {
      if (observers.TryGetValue(0, out List<IGauntletLevelControllerEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnGauntletLevelControllerEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IGauntletLevelControllerEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnGauntletLevelControllerEffect(effect);
        }
        observersForGauntletLevelController.Remove(effect.id);
      }
    }
    effectsGauntletLevelControllerDeleteEffect.Clear();


    foreach (var effect in effectsGauntletLevelControllerCreateEffect) {
      if (observers.TryGetValue(0, out List<IGauntletLevelControllerEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnGauntletLevelControllerEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IGauntletLevelControllerEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnGauntletLevelControllerEffect(effect);
        }
      }
    }
    effectsGauntletLevelControllerCreateEffect.Clear();
  }
  public PreGauntletLevelControllerIncarnation GetPreGauntletLevelControllerIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsPreGauntletLevelController[id].incarnation;
  }
  public bool PreGauntletLevelControllerExists(int id) {
    return rootIncarnation.incarnationsPreGauntletLevelController.ContainsKey(id);
  }
  public PreGauntletLevelController GetPreGauntletLevelController(int id) {
    return new PreGauntletLevelController(this, id);
  }
  public List<PreGauntletLevelController> AllPreGauntletLevelController() {
    List<PreGauntletLevelController> result = new List<PreGauntletLevelController>(rootIncarnation.incarnationsPreGauntletLevelController.Count);
    foreach (var id in rootIncarnation.incarnationsPreGauntletLevelController.Keys) {
      result.Add(new PreGauntletLevelController(this, id));
    }
    return result;
  }
  public IEnumerator<PreGauntletLevelController> EnumAllPreGauntletLevelController() {
    foreach (var id in rootIncarnation.incarnationsPreGauntletLevelController.Keys) {
      yield return GetPreGauntletLevelController(id);
    }
  }
  public void CheckHasPreGauntletLevelController(PreGauntletLevelController thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasPreGauntletLevelController(thing.id);
  }
  public void CheckHasPreGauntletLevelController(int id) {
    if (!rootIncarnation.incarnationsPreGauntletLevelController.ContainsKey(id)) {
      throw new System.Exception("Invalid PreGauntletLevelController: " + id);
    }
  }
  public void AddPreGauntletLevelControllerObserver(int id, IPreGauntletLevelControllerEffectObserver observer) {
    List<IPreGauntletLevelControllerEffectObserver> obsies;
    if (!observersForPreGauntletLevelController.TryGetValue(id, out obsies)) {
      obsies = new List<IPreGauntletLevelControllerEffectObserver>();
    }
    obsies.Add(observer);
    observersForPreGauntletLevelController[id] = obsies;
  }

  public void RemovePreGauntletLevelControllerObserver(int id, IPreGauntletLevelControllerEffectObserver observer) {
    if (observersForPreGauntletLevelController.ContainsKey(id)) {
      var list = observersForPreGauntletLevelController[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForPreGauntletLevelController.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public PreGauntletLevelController EffectPreGauntletLevelControllerCreate(
      Level level) {
    CheckUnlocked();
    CheckHasLevel(level);

    var id = NewId();
    var incarnation =
        new PreGauntletLevelControllerIncarnation(
            level.id
            );
    EffectInternalCreatePreGauntletLevelController(id, rootIncarnation.version, incarnation);
    return new PreGauntletLevelController(this, id);
  }
  public void EffectInternalCreatePreGauntletLevelController(
      int id,
      int incarnationVersion,
      PreGauntletLevelControllerIncarnation incarnation) {
    CheckUnlocked();
    var effect = new PreGauntletLevelControllerCreateEffect(id);
    rootIncarnation.incarnationsPreGauntletLevelController.Add(
        id,
        new VersionAndIncarnation<PreGauntletLevelControllerIncarnation>(
            incarnationVersion,
            incarnation));
    effectsPreGauntletLevelControllerCreateEffect.Add(effect);
  }

  public void EffectPreGauntletLevelControllerDelete(int id) {
    CheckUnlocked();
    var effect = new PreGauntletLevelControllerDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsPreGauntletLevelController[id];

    rootIncarnation.incarnationsPreGauntletLevelController.Remove(id);
    effectsPreGauntletLevelControllerDeleteEffect.Add(effect);
  }

     
  public int GetPreGauntletLevelControllerHash(int id, int version, PreGauntletLevelControllerIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.level.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastPreGauntletLevelControllerEffects(
      SortedDictionary<int, List<IPreGauntletLevelControllerEffectObserver>> observers) {
    foreach (var effect in effectsPreGauntletLevelControllerDeleteEffect) {
      if (observers.TryGetValue(0, out List<IPreGauntletLevelControllerEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnPreGauntletLevelControllerEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IPreGauntletLevelControllerEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnPreGauntletLevelControllerEffect(effect);
        }
        observersForPreGauntletLevelController.Remove(effect.id);
      }
    }
    effectsPreGauntletLevelControllerDeleteEffect.Clear();


    foreach (var effect in effectsPreGauntletLevelControllerCreateEffect) {
      if (observers.TryGetValue(0, out List<IPreGauntletLevelControllerEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnPreGauntletLevelControllerEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IPreGauntletLevelControllerEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnPreGauntletLevelControllerEffect(effect);
        }
      }
    }
    effectsPreGauntletLevelControllerCreateEffect.Clear();
  }
  public RavashrikeLevelControllerIncarnation GetRavashrikeLevelControllerIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsRavashrikeLevelController[id].incarnation;
  }
  public bool RavashrikeLevelControllerExists(int id) {
    return rootIncarnation.incarnationsRavashrikeLevelController.ContainsKey(id);
  }
  public RavashrikeLevelController GetRavashrikeLevelController(int id) {
    return new RavashrikeLevelController(this, id);
  }
  public List<RavashrikeLevelController> AllRavashrikeLevelController() {
    List<RavashrikeLevelController> result = new List<RavashrikeLevelController>(rootIncarnation.incarnationsRavashrikeLevelController.Count);
    foreach (var id in rootIncarnation.incarnationsRavashrikeLevelController.Keys) {
      result.Add(new RavashrikeLevelController(this, id));
    }
    return result;
  }
  public IEnumerator<RavashrikeLevelController> EnumAllRavashrikeLevelController() {
    foreach (var id in rootIncarnation.incarnationsRavashrikeLevelController.Keys) {
      yield return GetRavashrikeLevelController(id);
    }
  }
  public void CheckHasRavashrikeLevelController(RavashrikeLevelController thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasRavashrikeLevelController(thing.id);
  }
  public void CheckHasRavashrikeLevelController(int id) {
    if (!rootIncarnation.incarnationsRavashrikeLevelController.ContainsKey(id)) {
      throw new System.Exception("Invalid RavashrikeLevelController: " + id);
    }
  }
  public void AddRavashrikeLevelControllerObserver(int id, IRavashrikeLevelControllerEffectObserver observer) {
    List<IRavashrikeLevelControllerEffectObserver> obsies;
    if (!observersForRavashrikeLevelController.TryGetValue(id, out obsies)) {
      obsies = new List<IRavashrikeLevelControllerEffectObserver>();
    }
    obsies.Add(observer);
    observersForRavashrikeLevelController[id] = obsies;
  }

  public void RemoveRavashrikeLevelControllerObserver(int id, IRavashrikeLevelControllerEffectObserver observer) {
    if (observersForRavashrikeLevelController.ContainsKey(id)) {
      var list = observersForRavashrikeLevelController[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForRavashrikeLevelController.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public RavashrikeLevelController EffectRavashrikeLevelControllerCreate(
      Level level) {
    CheckUnlocked();
    CheckHasLevel(level);

    var id = NewId();
    var incarnation =
        new RavashrikeLevelControllerIncarnation(
            level.id
            );
    EffectInternalCreateRavashrikeLevelController(id, rootIncarnation.version, incarnation);
    return new RavashrikeLevelController(this, id);
  }
  public void EffectInternalCreateRavashrikeLevelController(
      int id,
      int incarnationVersion,
      RavashrikeLevelControllerIncarnation incarnation) {
    CheckUnlocked();
    var effect = new RavashrikeLevelControllerCreateEffect(id);
    rootIncarnation.incarnationsRavashrikeLevelController.Add(
        id,
        new VersionAndIncarnation<RavashrikeLevelControllerIncarnation>(
            incarnationVersion,
            incarnation));
    effectsRavashrikeLevelControllerCreateEffect.Add(effect);
  }

  public void EffectRavashrikeLevelControllerDelete(int id) {
    CheckUnlocked();
    var effect = new RavashrikeLevelControllerDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsRavashrikeLevelController[id];

    rootIncarnation.incarnationsRavashrikeLevelController.Remove(id);
    effectsRavashrikeLevelControllerDeleteEffect.Add(effect);
  }

     
  public int GetRavashrikeLevelControllerHash(int id, int version, RavashrikeLevelControllerIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.level.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastRavashrikeLevelControllerEffects(
      SortedDictionary<int, List<IRavashrikeLevelControllerEffectObserver>> observers) {
    foreach (var effect in effectsRavashrikeLevelControllerDeleteEffect) {
      if (observers.TryGetValue(0, out List<IRavashrikeLevelControllerEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnRavashrikeLevelControllerEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IRavashrikeLevelControllerEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnRavashrikeLevelControllerEffect(effect);
        }
        observersForRavashrikeLevelController.Remove(effect.id);
      }
    }
    effectsRavashrikeLevelControllerDeleteEffect.Clear();


    foreach (var effect in effectsRavashrikeLevelControllerCreateEffect) {
      if (observers.TryGetValue(0, out List<IRavashrikeLevelControllerEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnRavashrikeLevelControllerEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IRavashrikeLevelControllerEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnRavashrikeLevelControllerEffect(effect);
        }
      }
    }
    effectsRavashrikeLevelControllerCreateEffect.Clear();
  }
  public PentagonalCaveLevelControllerIncarnation GetPentagonalCaveLevelControllerIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsPentagonalCaveLevelController[id].incarnation;
  }
  public bool PentagonalCaveLevelControllerExists(int id) {
    return rootIncarnation.incarnationsPentagonalCaveLevelController.ContainsKey(id);
  }
  public PentagonalCaveLevelController GetPentagonalCaveLevelController(int id) {
    return new PentagonalCaveLevelController(this, id);
  }
  public List<PentagonalCaveLevelController> AllPentagonalCaveLevelController() {
    List<PentagonalCaveLevelController> result = new List<PentagonalCaveLevelController>(rootIncarnation.incarnationsPentagonalCaveLevelController.Count);
    foreach (var id in rootIncarnation.incarnationsPentagonalCaveLevelController.Keys) {
      result.Add(new PentagonalCaveLevelController(this, id));
    }
    return result;
  }
  public IEnumerator<PentagonalCaveLevelController> EnumAllPentagonalCaveLevelController() {
    foreach (var id in rootIncarnation.incarnationsPentagonalCaveLevelController.Keys) {
      yield return GetPentagonalCaveLevelController(id);
    }
  }
  public void CheckHasPentagonalCaveLevelController(PentagonalCaveLevelController thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasPentagonalCaveLevelController(thing.id);
  }
  public void CheckHasPentagonalCaveLevelController(int id) {
    if (!rootIncarnation.incarnationsPentagonalCaveLevelController.ContainsKey(id)) {
      throw new System.Exception("Invalid PentagonalCaveLevelController: " + id);
    }
  }
  public void AddPentagonalCaveLevelControllerObserver(int id, IPentagonalCaveLevelControllerEffectObserver observer) {
    List<IPentagonalCaveLevelControllerEffectObserver> obsies;
    if (!observersForPentagonalCaveLevelController.TryGetValue(id, out obsies)) {
      obsies = new List<IPentagonalCaveLevelControllerEffectObserver>();
    }
    obsies.Add(observer);
    observersForPentagonalCaveLevelController[id] = obsies;
  }

  public void RemovePentagonalCaveLevelControllerObserver(int id, IPentagonalCaveLevelControllerEffectObserver observer) {
    if (observersForPentagonalCaveLevelController.ContainsKey(id)) {
      var list = observersForPentagonalCaveLevelController[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForPentagonalCaveLevelController.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public PentagonalCaveLevelController EffectPentagonalCaveLevelControllerCreate(
      Level level,
      int depth) {
    CheckUnlocked();
    CheckHasLevel(level);

    var id = NewId();
    var incarnation =
        new PentagonalCaveLevelControllerIncarnation(
            level.id,
            depth
            );
    EffectInternalCreatePentagonalCaveLevelController(id, rootIncarnation.version, incarnation);
    return new PentagonalCaveLevelController(this, id);
  }
  public void EffectInternalCreatePentagonalCaveLevelController(
      int id,
      int incarnationVersion,
      PentagonalCaveLevelControllerIncarnation incarnation) {
    CheckUnlocked();
    var effect = new PentagonalCaveLevelControllerCreateEffect(id);
    rootIncarnation.incarnationsPentagonalCaveLevelController.Add(
        id,
        new VersionAndIncarnation<PentagonalCaveLevelControllerIncarnation>(
            incarnationVersion,
            incarnation));
    effectsPentagonalCaveLevelControllerCreateEffect.Add(effect);
  }

  public void EffectPentagonalCaveLevelControllerDelete(int id) {
    CheckUnlocked();
    var effect = new PentagonalCaveLevelControllerDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsPentagonalCaveLevelController[id];

    rootIncarnation.incarnationsPentagonalCaveLevelController.Remove(id);
    effectsPentagonalCaveLevelControllerDeleteEffect.Add(effect);
  }

     
  public int GetPentagonalCaveLevelControllerHash(int id, int version, PentagonalCaveLevelControllerIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.level.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.depth.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastPentagonalCaveLevelControllerEffects(
      SortedDictionary<int, List<IPentagonalCaveLevelControllerEffectObserver>> observers) {
    foreach (var effect in effectsPentagonalCaveLevelControllerDeleteEffect) {
      if (observers.TryGetValue(0, out List<IPentagonalCaveLevelControllerEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnPentagonalCaveLevelControllerEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IPentagonalCaveLevelControllerEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnPentagonalCaveLevelControllerEffect(effect);
        }
        observersForPentagonalCaveLevelController.Remove(effect.id);
      }
    }
    effectsPentagonalCaveLevelControllerDeleteEffect.Clear();


    foreach (var effect in effectsPentagonalCaveLevelControllerCreateEffect) {
      if (observers.TryGetValue(0, out List<IPentagonalCaveLevelControllerEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnPentagonalCaveLevelControllerEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IPentagonalCaveLevelControllerEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnPentagonalCaveLevelControllerEffect(effect);
        }
      }
    }
    effectsPentagonalCaveLevelControllerCreateEffect.Clear();
  }
  public CliffLevelControllerIncarnation GetCliffLevelControllerIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsCliffLevelController[id].incarnation;
  }
  public bool CliffLevelControllerExists(int id) {
    return rootIncarnation.incarnationsCliffLevelController.ContainsKey(id);
  }
  public CliffLevelController GetCliffLevelController(int id) {
    return new CliffLevelController(this, id);
  }
  public List<CliffLevelController> AllCliffLevelController() {
    List<CliffLevelController> result = new List<CliffLevelController>(rootIncarnation.incarnationsCliffLevelController.Count);
    foreach (var id in rootIncarnation.incarnationsCliffLevelController.Keys) {
      result.Add(new CliffLevelController(this, id));
    }
    return result;
  }
  public IEnumerator<CliffLevelController> EnumAllCliffLevelController() {
    foreach (var id in rootIncarnation.incarnationsCliffLevelController.Keys) {
      yield return GetCliffLevelController(id);
    }
  }
  public void CheckHasCliffLevelController(CliffLevelController thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasCliffLevelController(thing.id);
  }
  public void CheckHasCliffLevelController(int id) {
    if (!rootIncarnation.incarnationsCliffLevelController.ContainsKey(id)) {
      throw new System.Exception("Invalid CliffLevelController: " + id);
    }
  }
  public void AddCliffLevelControllerObserver(int id, ICliffLevelControllerEffectObserver observer) {
    List<ICliffLevelControllerEffectObserver> obsies;
    if (!observersForCliffLevelController.TryGetValue(id, out obsies)) {
      obsies = new List<ICliffLevelControllerEffectObserver>();
    }
    obsies.Add(observer);
    observersForCliffLevelController[id] = obsies;
  }

  public void RemoveCliffLevelControllerObserver(int id, ICliffLevelControllerEffectObserver observer) {
    if (observersForCliffLevelController.ContainsKey(id)) {
      var list = observersForCliffLevelController[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForCliffLevelController.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public CliffLevelController EffectCliffLevelControllerCreate(
      Level level,
      int depth) {
    CheckUnlocked();
    CheckHasLevel(level);

    var id = NewId();
    var incarnation =
        new CliffLevelControllerIncarnation(
            level.id,
            depth
            );
    EffectInternalCreateCliffLevelController(id, rootIncarnation.version, incarnation);
    return new CliffLevelController(this, id);
  }
  public void EffectInternalCreateCliffLevelController(
      int id,
      int incarnationVersion,
      CliffLevelControllerIncarnation incarnation) {
    CheckUnlocked();
    var effect = new CliffLevelControllerCreateEffect(id);
    rootIncarnation.incarnationsCliffLevelController.Add(
        id,
        new VersionAndIncarnation<CliffLevelControllerIncarnation>(
            incarnationVersion,
            incarnation));
    effectsCliffLevelControllerCreateEffect.Add(effect);
  }

  public void EffectCliffLevelControllerDelete(int id) {
    CheckUnlocked();
    var effect = new CliffLevelControllerDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsCliffLevelController[id];

    rootIncarnation.incarnationsCliffLevelController.Remove(id);
    effectsCliffLevelControllerDeleteEffect.Add(effect);
  }

     
  public int GetCliffLevelControllerHash(int id, int version, CliffLevelControllerIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.level.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.depth.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastCliffLevelControllerEffects(
      SortedDictionary<int, List<ICliffLevelControllerEffectObserver>> observers) {
    foreach (var effect in effectsCliffLevelControllerDeleteEffect) {
      if (observers.TryGetValue(0, out List<ICliffLevelControllerEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnCliffLevelControllerEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ICliffLevelControllerEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnCliffLevelControllerEffect(effect);
        }
        observersForCliffLevelController.Remove(effect.id);
      }
    }
    effectsCliffLevelControllerDeleteEffect.Clear();


    foreach (var effect in effectsCliffLevelControllerCreateEffect) {
      if (observers.TryGetValue(0, out List<ICliffLevelControllerEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnCliffLevelControllerEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ICliffLevelControllerEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnCliffLevelControllerEffect(effect);
        }
      }
    }
    effectsCliffLevelControllerCreateEffect.Clear();
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
      Terrain terrain,
      UnitMutSet units,
      int depth,
      ILevelController controller,
      int time) {
    CheckUnlocked();
    CheckHasTerrain(terrain);
    CheckHasUnitMutSet(units);

    var id = NewId();
    var incarnation =
        new LevelIncarnation(
            terrain.id,
            units.id,
            depth,
            controller.id,
            time
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
    result += id * version * 1 * incarnation.terrain.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.units.GetDeterministicHashCode();
    result += id * version * 3 * incarnation.depth.GetDeterministicHashCode();
    if (!object.ReferenceEquals(incarnation.controller, null)) {
      result += id * version * 4 * incarnation.controller.GetDeterministicHashCode();
    }
    result += id * version * 5 * incarnation.time.GetDeterministicHashCode();
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


    foreach (var effect in effectsLevelSetControllerEffect) {
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
    effectsLevelSetControllerEffect.Clear();

    foreach (var effect in effectsLevelSetTimeEffect) {
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
    effectsLevelSetTimeEffect.Clear();

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

  public void EffectLevelSetController(int id, ILevelController newValue) {
    CheckUnlocked();
    CheckHasLevel(id);
    var effect = new LevelSetControllerEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsLevel[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldId = oldIncarnationAndVersion.incarnation.controller;
      oldIncarnationAndVersion.incarnation.controller = newValue.id;

    } else {
      var newIncarnation =
          new LevelIncarnation(
              oldIncarnationAndVersion.incarnation.terrain,
              oldIncarnationAndVersion.incarnation.units,
              oldIncarnationAndVersion.incarnation.depth,
              newValue.id,
              oldIncarnationAndVersion.incarnation.time);
      rootIncarnation.incarnationsLevel[id] =
          new VersionAndIncarnation<LevelIncarnation>(
              rootIncarnation.version,
              newIncarnation);
    }

    effectsLevelSetControllerEffect.Add(effect);
  }

  public void EffectLevelSetTime(int id, int newValue) {
    CheckUnlocked();
    CheckHasLevel(id);
    var effect = new LevelSetTimeEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsLevel[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.time;
      oldIncarnationAndVersion.incarnation.time = newValue;

    } else {
      var newIncarnation =
          new LevelIncarnation(
              oldIncarnationAndVersion.incarnation.terrain,
              oldIncarnationAndVersion.incarnation.units,
              oldIncarnationAndVersion.incarnation.depth,
              oldIncarnationAndVersion.incarnation.controller,
              newValue);
      rootIncarnation.incarnationsLevel[id] =
          new VersionAndIncarnation<LevelIncarnation>(
              rootIncarnation.version,
              newIncarnation);
    }

    effectsLevelSetTimeEffect.Add(effect);
  }
  public TimeAnchorTTCIncarnation GetTimeAnchorTTCIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsTimeAnchorTTC[id].incarnation;
  }
  public bool TimeAnchorTTCExists(int id) {
    return rootIncarnation.incarnationsTimeAnchorTTC.ContainsKey(id);
  }
  public TimeAnchorTTC GetTimeAnchorTTC(int id) {
    return new TimeAnchorTTC(this, id);
  }
  public List<TimeAnchorTTC> AllTimeAnchorTTC() {
    List<TimeAnchorTTC> result = new List<TimeAnchorTTC>(rootIncarnation.incarnationsTimeAnchorTTC.Count);
    foreach (var id in rootIncarnation.incarnationsTimeAnchorTTC.Keys) {
      result.Add(new TimeAnchorTTC(this, id));
    }
    return result;
  }
  public IEnumerator<TimeAnchorTTC> EnumAllTimeAnchorTTC() {
    foreach (var id in rootIncarnation.incarnationsTimeAnchorTTC.Keys) {
      yield return GetTimeAnchorTTC(id);
    }
  }
  public void CheckHasTimeAnchorTTC(TimeAnchorTTC thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasTimeAnchorTTC(thing.id);
  }
  public void CheckHasTimeAnchorTTC(int id) {
    if (!rootIncarnation.incarnationsTimeAnchorTTC.ContainsKey(id)) {
      throw new System.Exception("Invalid TimeAnchorTTC: " + id);
    }
  }
  public void AddTimeAnchorTTCObserver(int id, ITimeAnchorTTCEffectObserver observer) {
    List<ITimeAnchorTTCEffectObserver> obsies;
    if (!observersForTimeAnchorTTC.TryGetValue(id, out obsies)) {
      obsies = new List<ITimeAnchorTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForTimeAnchorTTC[id] = obsies;
  }

  public void RemoveTimeAnchorTTCObserver(int id, ITimeAnchorTTCEffectObserver observer) {
    if (observersForTimeAnchorTTC.ContainsKey(id)) {
      var list = observersForTimeAnchorTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForTimeAnchorTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public TimeAnchorTTC EffectTimeAnchorTTCCreate(
      int pastVersion) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new TimeAnchorTTCIncarnation(
            pastVersion
            );
    EffectInternalCreateTimeAnchorTTC(id, rootIncarnation.version, incarnation);
    return new TimeAnchorTTC(this, id);
  }
  public void EffectInternalCreateTimeAnchorTTC(
      int id,
      int incarnationVersion,
      TimeAnchorTTCIncarnation incarnation) {
    CheckUnlocked();
    var effect = new TimeAnchorTTCCreateEffect(id);
    rootIncarnation.incarnationsTimeAnchorTTC.Add(
        id,
        new VersionAndIncarnation<TimeAnchorTTCIncarnation>(
            incarnationVersion,
            incarnation));
    effectsTimeAnchorTTCCreateEffect.Add(effect);
  }

  public void EffectTimeAnchorTTCDelete(int id) {
    CheckUnlocked();
    var effect = new TimeAnchorTTCDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsTimeAnchorTTC[id];

    rootIncarnation.incarnationsTimeAnchorTTC.Remove(id);
    effectsTimeAnchorTTCDeleteEffect.Add(effect);
  }

     
  public int GetTimeAnchorTTCHash(int id, int version, TimeAnchorTTCIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.pastVersion.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastTimeAnchorTTCEffects(
      SortedDictionary<int, List<ITimeAnchorTTCEffectObserver>> observers) {
    foreach (var effect in effectsTimeAnchorTTCDeleteEffect) {
      if (observers.TryGetValue(0, out List<ITimeAnchorTTCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeAnchorTTCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeAnchorTTCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeAnchorTTCEffect(effect);
        }
        observersForTimeAnchorTTC.Remove(effect.id);
      }
    }
    effectsTimeAnchorTTCDeleteEffect.Clear();


    foreach (var effect in effectsTimeAnchorTTCCreateEffect) {
      if (observers.TryGetValue(0, out List<ITimeAnchorTTCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeAnchorTTCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeAnchorTTCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeAnchorTTCEffect(effect);
        }
      }
    }
    effectsTimeAnchorTTCCreateEffect.Clear();
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

    foreach (var effect in effectsTerrainTileSetWalkableEffect) {
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
    effectsTerrainTileSetWalkableEffect.Clear();

    foreach (var effect in effectsTerrainTileSetClassIdEffect) {
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
    effectsTerrainTileSetClassIdEffect.Clear();

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

  public void EffectTerrainTileSetWalkable(int id, bool newValue) {
    CheckUnlocked();
    CheckHasTerrainTile(id);
    var effect = new TerrainTileSetWalkableEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsTerrainTile[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.walkable;
      oldIncarnationAndVersion.incarnation.walkable = newValue;

    } else {
      var newIncarnation =
          new TerrainTileIncarnation(
              oldIncarnationAndVersion.incarnation.elevation,
              newValue,
              oldIncarnationAndVersion.incarnation.classId,
              oldIncarnationAndVersion.incarnation.components);
      rootIncarnation.incarnationsTerrainTile[id] =
          new VersionAndIncarnation<TerrainTileIncarnation>(
              rootIncarnation.version,
              newIncarnation);
    }

    effectsTerrainTileSetWalkableEffect.Add(effect);
  }

  public void EffectTerrainTileSetClassId(int id, string newValue) {
    CheckUnlocked();
    CheckHasTerrainTile(id);
    var effect = new TerrainTileSetClassIdEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsTerrainTile[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.classId;
      oldIncarnationAndVersion.incarnation.classId = newValue;

    } else {
      var newIncarnation =
          new TerrainTileIncarnation(
              oldIncarnationAndVersion.incarnation.elevation,
              oldIncarnationAndVersion.incarnation.walkable,
              newValue,
              oldIncarnationAndVersion.incarnation.components);
      rootIncarnation.incarnationsTerrainTile[id] =
          new VersionAndIncarnation<TerrainTileIncarnation>(
              rootIncarnation.version,
              newIncarnation);
    }

    effectsTerrainTileSetClassIdEffect.Add(effect);
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
      ArmorMutSet membersArmorMutSet,
      InertiaRingMutSet membersInertiaRingMutSet,
      GlaiveMutSet membersGlaiveMutSet,
      ManaPotionMutSet membersManaPotionMutSet,
      HealthPotionMutSet membersHealthPotionMutSet,
      TimeAnchorTTCMutSet membersTimeAnchorTTCMutSet,
      StaircaseTTCMutSet membersStaircaseTTCMutSet,
      DecorativeTTCMutSet membersDecorativeTTCMutSet) {
    CheckUnlocked();
    CheckHasArmorMutSet(membersArmorMutSet);
    CheckHasInertiaRingMutSet(membersInertiaRingMutSet);
    CheckHasGlaiveMutSet(membersGlaiveMutSet);
    CheckHasManaPotionMutSet(membersManaPotionMutSet);
    CheckHasHealthPotionMutSet(membersHealthPotionMutSet);
    CheckHasTimeAnchorTTCMutSet(membersTimeAnchorTTCMutSet);
    CheckHasStaircaseTTCMutSet(membersStaircaseTTCMutSet);
    CheckHasDecorativeTTCMutSet(membersDecorativeTTCMutSet);

    var id = NewId();
    var incarnation =
        new ITerrainTileComponentMutBunchIncarnation(
            membersArmorMutSet.id,
            membersInertiaRingMutSet.id,
            membersGlaiveMutSet.id,
            membersManaPotionMutSet.id,
            membersHealthPotionMutSet.id,
            membersTimeAnchorTTCMutSet.id,
            membersStaircaseTTCMutSet.id,
            membersDecorativeTTCMutSet.id
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
    result += id * version * 1 * incarnation.membersArmorMutSet.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.membersInertiaRingMutSet.GetDeterministicHashCode();
    result += id * version * 3 * incarnation.membersGlaiveMutSet.GetDeterministicHashCode();
    result += id * version * 4 * incarnation.membersManaPotionMutSet.GetDeterministicHashCode();
    result += id * version * 5 * incarnation.membersHealthPotionMutSet.GetDeterministicHashCode();
    result += id * version * 6 * incarnation.membersTimeAnchorTTCMutSet.GetDeterministicHashCode();
    result += id * version * 7 * incarnation.membersStaircaseTTCMutSet.GetDeterministicHashCode();
    result += id * version * 8 * incarnation.membersDecorativeTTCMutSet.GetDeterministicHashCode();
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
  public StaircaseTTCIncarnation GetStaircaseTTCIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsStaircaseTTC[id].incarnation;
  }
  public bool StaircaseTTCExists(int id) {
    return rootIncarnation.incarnationsStaircaseTTC.ContainsKey(id);
  }
  public StaircaseTTC GetStaircaseTTC(int id) {
    return new StaircaseTTC(this, id);
  }
  public List<StaircaseTTC> AllStaircaseTTC() {
    List<StaircaseTTC> result = new List<StaircaseTTC>(rootIncarnation.incarnationsStaircaseTTC.Count);
    foreach (var id in rootIncarnation.incarnationsStaircaseTTC.Keys) {
      result.Add(new StaircaseTTC(this, id));
    }
    return result;
  }
  public IEnumerator<StaircaseTTC> EnumAllStaircaseTTC() {
    foreach (var id in rootIncarnation.incarnationsStaircaseTTC.Keys) {
      yield return GetStaircaseTTC(id);
    }
  }
  public void CheckHasStaircaseTTC(StaircaseTTC thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasStaircaseTTC(thing.id);
  }
  public void CheckHasStaircaseTTC(int id) {
    if (!rootIncarnation.incarnationsStaircaseTTC.ContainsKey(id)) {
      throw new System.Exception("Invalid StaircaseTTC: " + id);
    }
  }
  public void AddStaircaseTTCObserver(int id, IStaircaseTTCEffectObserver observer) {
    List<IStaircaseTTCEffectObserver> obsies;
    if (!observersForStaircaseTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IStaircaseTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForStaircaseTTC[id] = obsies;
  }

  public void RemoveStaircaseTTCObserver(int id, IStaircaseTTCEffectObserver observer) {
    if (observersForStaircaseTTC.ContainsKey(id)) {
      var list = observersForStaircaseTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForStaircaseTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public StaircaseTTC EffectStaircaseTTCCreate(
      int portalIndex,
      Level destinationLevel,
      int destinationLevelPortalIndex) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new StaircaseTTCIncarnation(
            portalIndex,
            destinationLevel.id,
            destinationLevelPortalIndex
            );
    EffectInternalCreateStaircaseTTC(id, rootIncarnation.version, incarnation);
    return new StaircaseTTC(this, id);
  }
  public void EffectInternalCreateStaircaseTTC(
      int id,
      int incarnationVersion,
      StaircaseTTCIncarnation incarnation) {
    CheckUnlocked();
    var effect = new StaircaseTTCCreateEffect(id);
    rootIncarnation.incarnationsStaircaseTTC.Add(
        id,
        new VersionAndIncarnation<StaircaseTTCIncarnation>(
            incarnationVersion,
            incarnation));
    effectsStaircaseTTCCreateEffect.Add(effect);
  }

  public void EffectStaircaseTTCDelete(int id) {
    CheckUnlocked();
    var effect = new StaircaseTTCDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsStaircaseTTC[id];

    rootIncarnation.incarnationsStaircaseTTC.Remove(id);
    effectsStaircaseTTCDeleteEffect.Add(effect);
  }

     
  public int GetStaircaseTTCHash(int id, int version, StaircaseTTCIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.portalIndex.GetDeterministicHashCode();
    if (!object.ReferenceEquals(incarnation.destinationLevel, null)) {
      result += id * version * 2 * incarnation.destinationLevel.GetDeterministicHashCode();
    }
    result += id * version * 3 * incarnation.destinationLevelPortalIndex.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastStaircaseTTCEffects(
      SortedDictionary<int, List<IStaircaseTTCEffectObserver>> observers) {
    foreach (var effect in effectsStaircaseTTCDeleteEffect) {
      if (observers.TryGetValue(0, out List<IStaircaseTTCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnStaircaseTTCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IStaircaseTTCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnStaircaseTTCEffect(effect);
        }
        observersForStaircaseTTC.Remove(effect.id);
      }
    }
    effectsStaircaseTTCDeleteEffect.Clear();


    foreach (var effect in effectsStaircaseTTCSetDestinationLevelEffect) {
      if (observers.TryGetValue(0, out List<IStaircaseTTCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnStaircaseTTCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IStaircaseTTCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnStaircaseTTCEffect(effect);
        }
      }
    }
    effectsStaircaseTTCSetDestinationLevelEffect.Clear();

    foreach (var effect in effectsStaircaseTTCSetDestinationLevelPortalIndexEffect) {
      if (observers.TryGetValue(0, out List<IStaircaseTTCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnStaircaseTTCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IStaircaseTTCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnStaircaseTTCEffect(effect);
        }
      }
    }
    effectsStaircaseTTCSetDestinationLevelPortalIndexEffect.Clear();

    foreach (var effect in effectsStaircaseTTCCreateEffect) {
      if (observers.TryGetValue(0, out List<IStaircaseTTCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnStaircaseTTCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IStaircaseTTCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnStaircaseTTCEffect(effect);
        }
      }
    }
    effectsStaircaseTTCCreateEffect.Clear();
  }

  public void EffectStaircaseTTCSetDestinationLevel(int id, Level newValue) {
    CheckUnlocked();
    CheckHasStaircaseTTC(id);
    var effect = new StaircaseTTCSetDestinationLevelEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsStaircaseTTC[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldId = oldIncarnationAndVersion.incarnation.destinationLevel;
      oldIncarnationAndVersion.incarnation.destinationLevel = newValue.id;

    } else {
      var newIncarnation =
          new StaircaseTTCIncarnation(
              oldIncarnationAndVersion.incarnation.portalIndex,
              newValue.id,
              oldIncarnationAndVersion.incarnation.destinationLevelPortalIndex);
      rootIncarnation.incarnationsStaircaseTTC[id] =
          new VersionAndIncarnation<StaircaseTTCIncarnation>(
              rootIncarnation.version,
              newIncarnation);
    }

    effectsStaircaseTTCSetDestinationLevelEffect.Add(effect);
  }

  public void EffectStaircaseTTCSetDestinationLevelPortalIndex(int id, int newValue) {
    CheckUnlocked();
    CheckHasStaircaseTTC(id);
    var effect = new StaircaseTTCSetDestinationLevelPortalIndexEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsStaircaseTTC[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.destinationLevelPortalIndex;
      oldIncarnationAndVersion.incarnation.destinationLevelPortalIndex = newValue;

    } else {
      var newIncarnation =
          new StaircaseTTCIncarnation(
              oldIncarnationAndVersion.incarnation.portalIndex,
              oldIncarnationAndVersion.incarnation.destinationLevel,
              newValue);
      rootIncarnation.incarnationsStaircaseTTC[id] =
          new VersionAndIncarnation<StaircaseTTCIncarnation>(
              rootIncarnation.version,
              newIncarnation);
    }

    effectsStaircaseTTCSetDestinationLevelPortalIndexEffect.Add(effect);
  }
  public DecorativeTTCIncarnation GetDecorativeTTCIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsDecorativeTTC[id].incarnation;
  }
  public bool DecorativeTTCExists(int id) {
    return rootIncarnation.incarnationsDecorativeTTC.ContainsKey(id);
  }
  public DecorativeTTC GetDecorativeTTC(int id) {
    return new DecorativeTTC(this, id);
  }
  public List<DecorativeTTC> AllDecorativeTTC() {
    List<DecorativeTTC> result = new List<DecorativeTTC>(rootIncarnation.incarnationsDecorativeTTC.Count);
    foreach (var id in rootIncarnation.incarnationsDecorativeTTC.Keys) {
      result.Add(new DecorativeTTC(this, id));
    }
    return result;
  }
  public IEnumerator<DecorativeTTC> EnumAllDecorativeTTC() {
    foreach (var id in rootIncarnation.incarnationsDecorativeTTC.Keys) {
      yield return GetDecorativeTTC(id);
    }
  }
  public void CheckHasDecorativeTTC(DecorativeTTC thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasDecorativeTTC(thing.id);
  }
  public void CheckHasDecorativeTTC(int id) {
    if (!rootIncarnation.incarnationsDecorativeTTC.ContainsKey(id)) {
      throw new System.Exception("Invalid DecorativeTTC: " + id);
    }
  }
  public void AddDecorativeTTCObserver(int id, IDecorativeTTCEffectObserver observer) {
    List<IDecorativeTTCEffectObserver> obsies;
    if (!observersForDecorativeTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IDecorativeTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForDecorativeTTC[id] = obsies;
  }

  public void RemoveDecorativeTTCObserver(int id, IDecorativeTTCEffectObserver observer) {
    if (observersForDecorativeTTC.ContainsKey(id)) {
      var list = observersForDecorativeTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForDecorativeTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public DecorativeTTC EffectDecorativeTTCCreate(
      string symbolId) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new DecorativeTTCIncarnation(
            symbolId
            );
    EffectInternalCreateDecorativeTTC(id, rootIncarnation.version, incarnation);
    return new DecorativeTTC(this, id);
  }
  public void EffectInternalCreateDecorativeTTC(
      int id,
      int incarnationVersion,
      DecorativeTTCIncarnation incarnation) {
    CheckUnlocked();
    var effect = new DecorativeTTCCreateEffect(id);
    rootIncarnation.incarnationsDecorativeTTC.Add(
        id,
        new VersionAndIncarnation<DecorativeTTCIncarnation>(
            incarnationVersion,
            incarnation));
    effectsDecorativeTTCCreateEffect.Add(effect);
  }

  public void EffectDecorativeTTCDelete(int id) {
    CheckUnlocked();
    var effect = new DecorativeTTCDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsDecorativeTTC[id];

    rootIncarnation.incarnationsDecorativeTTC.Remove(id);
    effectsDecorativeTTCDeleteEffect.Add(effect);
  }

     
  public int GetDecorativeTTCHash(int id, int version, DecorativeTTCIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.symbolId.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastDecorativeTTCEffects(
      SortedDictionary<int, List<IDecorativeTTCEffectObserver>> observers) {
    foreach (var effect in effectsDecorativeTTCDeleteEffect) {
      if (observers.TryGetValue(0, out List<IDecorativeTTCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnDecorativeTTCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IDecorativeTTCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnDecorativeTTCEffect(effect);
        }
        observersForDecorativeTTC.Remove(effect.id);
      }
    }
    effectsDecorativeTTCDeleteEffect.Clear();


    foreach (var effect in effectsDecorativeTTCCreateEffect) {
      if (observers.TryGetValue(0, out List<IDecorativeTTCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnDecorativeTTCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IDecorativeTTCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnDecorativeTTCEffect(effect);
        }
      }
    }
    effectsDecorativeTTCCreateEffect.Clear();
  }
  public ManaPotionIncarnation GetManaPotionIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsManaPotion[id].incarnation;
  }
  public bool ManaPotionExists(int id) {
    return rootIncarnation.incarnationsManaPotion.ContainsKey(id);
  }
  public ManaPotion GetManaPotion(int id) {
    return new ManaPotion(this, id);
  }
  public List<ManaPotion> AllManaPotion() {
    List<ManaPotion> result = new List<ManaPotion>(rootIncarnation.incarnationsManaPotion.Count);
    foreach (var id in rootIncarnation.incarnationsManaPotion.Keys) {
      result.Add(new ManaPotion(this, id));
    }
    return result;
  }
  public IEnumerator<ManaPotion> EnumAllManaPotion() {
    foreach (var id in rootIncarnation.incarnationsManaPotion.Keys) {
      yield return GetManaPotion(id);
    }
  }
  public void CheckHasManaPotion(ManaPotion thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasManaPotion(thing.id);
  }
  public void CheckHasManaPotion(int id) {
    if (!rootIncarnation.incarnationsManaPotion.ContainsKey(id)) {
      throw new System.Exception("Invalid ManaPotion: " + id);
    }
  }
  public void AddManaPotionObserver(int id, IManaPotionEffectObserver observer) {
    List<IManaPotionEffectObserver> obsies;
    if (!observersForManaPotion.TryGetValue(id, out obsies)) {
      obsies = new List<IManaPotionEffectObserver>();
    }
    obsies.Add(observer);
    observersForManaPotion[id] = obsies;
  }

  public void RemoveManaPotionObserver(int id, IManaPotionEffectObserver observer) {
    if (observersForManaPotion.ContainsKey(id)) {
      var list = observersForManaPotion[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForManaPotion.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public ManaPotion EffectManaPotionCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new ManaPotionIncarnation(

            );
    EffectInternalCreateManaPotion(id, rootIncarnation.version, incarnation);
    return new ManaPotion(this, id);
  }
  public void EffectInternalCreateManaPotion(
      int id,
      int incarnationVersion,
      ManaPotionIncarnation incarnation) {
    CheckUnlocked();
    var effect = new ManaPotionCreateEffect(id);
    rootIncarnation.incarnationsManaPotion.Add(
        id,
        new VersionAndIncarnation<ManaPotionIncarnation>(
            incarnationVersion,
            incarnation));
    effectsManaPotionCreateEffect.Add(effect);
  }

  public void EffectManaPotionDelete(int id) {
    CheckUnlocked();
    var effect = new ManaPotionDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsManaPotion[id];

    rootIncarnation.incarnationsManaPotion.Remove(id);
    effectsManaPotionDeleteEffect.Add(effect);
  }

     
  public int GetManaPotionHash(int id, int version, ManaPotionIncarnation incarnation) {
    int result = id * version;
    return result;
  }
     
  public void BroadcastManaPotionEffects(
      SortedDictionary<int, List<IManaPotionEffectObserver>> observers) {
    foreach (var effect in effectsManaPotionDeleteEffect) {
      if (observers.TryGetValue(0, out List<IManaPotionEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnManaPotionEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IManaPotionEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnManaPotionEffect(effect);
        }
        observersForManaPotion.Remove(effect.id);
      }
    }
    effectsManaPotionDeleteEffect.Clear();


    foreach (var effect in effectsManaPotionCreateEffect) {
      if (observers.TryGetValue(0, out List<IManaPotionEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnManaPotionEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IManaPotionEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnManaPotionEffect(effect);
        }
      }
    }
    effectsManaPotionCreateEffect.Clear();
  }
  public HealthPotionIncarnation GetHealthPotionIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsHealthPotion[id].incarnation;
  }
  public bool HealthPotionExists(int id) {
    return rootIncarnation.incarnationsHealthPotion.ContainsKey(id);
  }
  public HealthPotion GetHealthPotion(int id) {
    return new HealthPotion(this, id);
  }
  public List<HealthPotion> AllHealthPotion() {
    List<HealthPotion> result = new List<HealthPotion>(rootIncarnation.incarnationsHealthPotion.Count);
    foreach (var id in rootIncarnation.incarnationsHealthPotion.Keys) {
      result.Add(new HealthPotion(this, id));
    }
    return result;
  }
  public IEnumerator<HealthPotion> EnumAllHealthPotion() {
    foreach (var id in rootIncarnation.incarnationsHealthPotion.Keys) {
      yield return GetHealthPotion(id);
    }
  }
  public void CheckHasHealthPotion(HealthPotion thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasHealthPotion(thing.id);
  }
  public void CheckHasHealthPotion(int id) {
    if (!rootIncarnation.incarnationsHealthPotion.ContainsKey(id)) {
      throw new System.Exception("Invalid HealthPotion: " + id);
    }
  }
  public void AddHealthPotionObserver(int id, IHealthPotionEffectObserver observer) {
    List<IHealthPotionEffectObserver> obsies;
    if (!observersForHealthPotion.TryGetValue(id, out obsies)) {
      obsies = new List<IHealthPotionEffectObserver>();
    }
    obsies.Add(observer);
    observersForHealthPotion[id] = obsies;
  }

  public void RemoveHealthPotionObserver(int id, IHealthPotionEffectObserver observer) {
    if (observersForHealthPotion.ContainsKey(id)) {
      var list = observersForHealthPotion[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForHealthPotion.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public HealthPotion EffectHealthPotionCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new HealthPotionIncarnation(

            );
    EffectInternalCreateHealthPotion(id, rootIncarnation.version, incarnation);
    return new HealthPotion(this, id);
  }
  public void EffectInternalCreateHealthPotion(
      int id,
      int incarnationVersion,
      HealthPotionIncarnation incarnation) {
    CheckUnlocked();
    var effect = new HealthPotionCreateEffect(id);
    rootIncarnation.incarnationsHealthPotion.Add(
        id,
        new VersionAndIncarnation<HealthPotionIncarnation>(
            incarnationVersion,
            incarnation));
    effectsHealthPotionCreateEffect.Add(effect);
  }

  public void EffectHealthPotionDelete(int id) {
    CheckUnlocked();
    var effect = new HealthPotionDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsHealthPotion[id];

    rootIncarnation.incarnationsHealthPotion.Remove(id);
    effectsHealthPotionDeleteEffect.Add(effect);
  }

     
  public int GetHealthPotionHash(int id, int version, HealthPotionIncarnation incarnation) {
    int result = id * version;
    return result;
  }
     
  public void BroadcastHealthPotionEffects(
      SortedDictionary<int, List<IHealthPotionEffectObserver>> observers) {
    foreach (var effect in effectsHealthPotionDeleteEffect) {
      if (observers.TryGetValue(0, out List<IHealthPotionEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnHealthPotionEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IHealthPotionEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnHealthPotionEffect(effect);
        }
        observersForHealthPotion.Remove(effect.id);
      }
    }
    effectsHealthPotionDeleteEffect.Clear();


    foreach (var effect in effectsHealthPotionCreateEffect) {
      if (observers.TryGetValue(0, out List<IHealthPotionEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnHealthPotionEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IHealthPotionEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnHealthPotionEffect(effect);
        }
      }
    }
    effectsHealthPotionCreateEffect.Clear();
  }
  public InertiaRingIncarnation GetInertiaRingIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsInertiaRing[id].incarnation;
  }
  public bool InertiaRingExists(int id) {
    return rootIncarnation.incarnationsInertiaRing.ContainsKey(id);
  }
  public InertiaRing GetInertiaRing(int id) {
    return new InertiaRing(this, id);
  }
  public List<InertiaRing> AllInertiaRing() {
    List<InertiaRing> result = new List<InertiaRing>(rootIncarnation.incarnationsInertiaRing.Count);
    foreach (var id in rootIncarnation.incarnationsInertiaRing.Keys) {
      result.Add(new InertiaRing(this, id));
    }
    return result;
  }
  public IEnumerator<InertiaRing> EnumAllInertiaRing() {
    foreach (var id in rootIncarnation.incarnationsInertiaRing.Keys) {
      yield return GetInertiaRing(id);
    }
  }
  public void CheckHasInertiaRing(InertiaRing thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasInertiaRing(thing.id);
  }
  public void CheckHasInertiaRing(int id) {
    if (!rootIncarnation.incarnationsInertiaRing.ContainsKey(id)) {
      throw new System.Exception("Invalid InertiaRing: " + id);
    }
  }
  public void AddInertiaRingObserver(int id, IInertiaRingEffectObserver observer) {
    List<IInertiaRingEffectObserver> obsies;
    if (!observersForInertiaRing.TryGetValue(id, out obsies)) {
      obsies = new List<IInertiaRingEffectObserver>();
    }
    obsies.Add(observer);
    observersForInertiaRing[id] = obsies;
  }

  public void RemoveInertiaRingObserver(int id, IInertiaRingEffectObserver observer) {
    if (observersForInertiaRing.ContainsKey(id)) {
      var list = observersForInertiaRing[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForInertiaRing.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public InertiaRing EffectInertiaRingCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new InertiaRingIncarnation(

            );
    EffectInternalCreateInertiaRing(id, rootIncarnation.version, incarnation);
    return new InertiaRing(this, id);
  }
  public void EffectInternalCreateInertiaRing(
      int id,
      int incarnationVersion,
      InertiaRingIncarnation incarnation) {
    CheckUnlocked();
    var effect = new InertiaRingCreateEffect(id);
    rootIncarnation.incarnationsInertiaRing.Add(
        id,
        new VersionAndIncarnation<InertiaRingIncarnation>(
            incarnationVersion,
            incarnation));
    effectsInertiaRingCreateEffect.Add(effect);
  }

  public void EffectInertiaRingDelete(int id) {
    CheckUnlocked();
    var effect = new InertiaRingDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsInertiaRing[id];

    rootIncarnation.incarnationsInertiaRing.Remove(id);
    effectsInertiaRingDeleteEffect.Add(effect);
  }

     
  public int GetInertiaRingHash(int id, int version, InertiaRingIncarnation incarnation) {
    int result = id * version;
    return result;
  }
     
  public void BroadcastInertiaRingEffects(
      SortedDictionary<int, List<IInertiaRingEffectObserver>> observers) {
    foreach (var effect in effectsInertiaRingDeleteEffect) {
      if (observers.TryGetValue(0, out List<IInertiaRingEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnInertiaRingEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IInertiaRingEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnInertiaRingEffect(effect);
        }
        observersForInertiaRing.Remove(effect.id);
      }
    }
    effectsInertiaRingDeleteEffect.Clear();


    foreach (var effect in effectsInertiaRingCreateEffect) {
      if (observers.TryGetValue(0, out List<IInertiaRingEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnInertiaRingEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IInertiaRingEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnInertiaRingEffect(effect);
        }
      }
    }
    effectsInertiaRingCreateEffect.Clear();
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
  public CounteringUCIncarnation GetCounteringUCIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsCounteringUC[id].incarnation;
  }
  public bool CounteringUCExists(int id) {
    return rootIncarnation.incarnationsCounteringUC.ContainsKey(id);
  }
  public CounteringUC GetCounteringUC(int id) {
    return new CounteringUC(this, id);
  }
  public List<CounteringUC> AllCounteringUC() {
    List<CounteringUC> result = new List<CounteringUC>(rootIncarnation.incarnationsCounteringUC.Count);
    foreach (var id in rootIncarnation.incarnationsCounteringUC.Keys) {
      result.Add(new CounteringUC(this, id));
    }
    return result;
  }
  public IEnumerator<CounteringUC> EnumAllCounteringUC() {
    foreach (var id in rootIncarnation.incarnationsCounteringUC.Keys) {
      yield return GetCounteringUC(id);
    }
  }
  public void CheckHasCounteringUC(CounteringUC thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasCounteringUC(thing.id);
  }
  public void CheckHasCounteringUC(int id) {
    if (!rootIncarnation.incarnationsCounteringUC.ContainsKey(id)) {
      throw new System.Exception("Invalid CounteringUC: " + id);
    }
  }
  public void AddCounteringUCObserver(int id, ICounteringUCEffectObserver observer) {
    List<ICounteringUCEffectObserver> obsies;
    if (!observersForCounteringUC.TryGetValue(id, out obsies)) {
      obsies = new List<ICounteringUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForCounteringUC[id] = obsies;
  }

  public void RemoveCounteringUCObserver(int id, ICounteringUCEffectObserver observer) {
    if (observersForCounteringUC.ContainsKey(id)) {
      var list = observersForCounteringUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForCounteringUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public CounteringUC EffectCounteringUCCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new CounteringUCIncarnation(

            );
    EffectInternalCreateCounteringUC(id, rootIncarnation.version, incarnation);
    return new CounteringUC(this, id);
  }
  public void EffectInternalCreateCounteringUC(
      int id,
      int incarnationVersion,
      CounteringUCIncarnation incarnation) {
    CheckUnlocked();
    var effect = new CounteringUCCreateEffect(id);
    rootIncarnation.incarnationsCounteringUC.Add(
        id,
        new VersionAndIncarnation<CounteringUCIncarnation>(
            incarnationVersion,
            incarnation));
    effectsCounteringUCCreateEffect.Add(effect);
  }

  public void EffectCounteringUCDelete(int id) {
    CheckUnlocked();
    var effect = new CounteringUCDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsCounteringUC[id];

    rootIncarnation.incarnationsCounteringUC.Remove(id);
    effectsCounteringUCDeleteEffect.Add(effect);
  }

     
  public int GetCounteringUCHash(int id, int version, CounteringUCIncarnation incarnation) {
    int result = id * version;
    return result;
  }
     
  public void BroadcastCounteringUCEffects(
      SortedDictionary<int, List<ICounteringUCEffectObserver>> observers) {
    foreach (var effect in effectsCounteringUCDeleteEffect) {
      if (observers.TryGetValue(0, out List<ICounteringUCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnCounteringUCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ICounteringUCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnCounteringUCEffect(effect);
        }
        observersForCounteringUC.Remove(effect.id);
      }
    }
    effectsCounteringUCDeleteEffect.Clear();


    foreach (var effect in effectsCounteringUCCreateEffect) {
      if (observers.TryGetValue(0, out List<ICounteringUCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnCounteringUCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ICounteringUCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnCounteringUCEffect(effect);
        }
      }
    }
    effectsCounteringUCCreateEffect.Clear();
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
  public EvaporateImpulseIncarnation GetEvaporateImpulseIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsEvaporateImpulse[id].incarnation;
  }
  public bool EvaporateImpulseExists(int id) {
    return rootIncarnation.incarnationsEvaporateImpulse.ContainsKey(id);
  }
  public EvaporateImpulse GetEvaporateImpulse(int id) {
    return new EvaporateImpulse(this, id);
  }
  public List<EvaporateImpulse> AllEvaporateImpulse() {
    List<EvaporateImpulse> result = new List<EvaporateImpulse>(rootIncarnation.incarnationsEvaporateImpulse.Count);
    foreach (var id in rootIncarnation.incarnationsEvaporateImpulse.Keys) {
      result.Add(new EvaporateImpulse(this, id));
    }
    return result;
  }
  public IEnumerator<EvaporateImpulse> EnumAllEvaporateImpulse() {
    foreach (var id in rootIncarnation.incarnationsEvaporateImpulse.Keys) {
      yield return GetEvaporateImpulse(id);
    }
  }
  public void CheckHasEvaporateImpulse(EvaporateImpulse thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasEvaporateImpulse(thing.id);
  }
  public void CheckHasEvaporateImpulse(int id) {
    if (!rootIncarnation.incarnationsEvaporateImpulse.ContainsKey(id)) {
      throw new System.Exception("Invalid EvaporateImpulse: " + id);
    }
  }
  public void AddEvaporateImpulseObserver(int id, IEvaporateImpulseEffectObserver observer) {
    List<IEvaporateImpulseEffectObserver> obsies;
    if (!observersForEvaporateImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<IEvaporateImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersForEvaporateImpulse[id] = obsies;
  }

  public void RemoveEvaporateImpulseObserver(int id, IEvaporateImpulseEffectObserver observer) {
    if (observersForEvaporateImpulse.ContainsKey(id)) {
      var list = observersForEvaporateImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForEvaporateImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public EvaporateImpulse EffectEvaporateImpulseCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new EvaporateImpulseIncarnation(

            );
    EffectInternalCreateEvaporateImpulse(id, rootIncarnation.version, incarnation);
    return new EvaporateImpulse(this, id);
  }
  public void EffectInternalCreateEvaporateImpulse(
      int id,
      int incarnationVersion,
      EvaporateImpulseIncarnation incarnation) {
    CheckUnlocked();
    var effect = new EvaporateImpulseCreateEffect(id);
    rootIncarnation.incarnationsEvaporateImpulse.Add(
        id,
        new VersionAndIncarnation<EvaporateImpulseIncarnation>(
            incarnationVersion,
            incarnation));
    effectsEvaporateImpulseCreateEffect.Add(effect);
  }

  public void EffectEvaporateImpulseDelete(int id) {
    CheckUnlocked();
    var effect = new EvaporateImpulseDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsEvaporateImpulse[id];

    rootIncarnation.incarnationsEvaporateImpulse.Remove(id);
    effectsEvaporateImpulseDeleteEffect.Add(effect);
  }

     
  public int GetEvaporateImpulseHash(int id, int version, EvaporateImpulseIncarnation incarnation) {
    int result = id * version;
    return result;
  }
     
  public void BroadcastEvaporateImpulseEffects(
      SortedDictionary<int, List<IEvaporateImpulseEffectObserver>> observers) {
    foreach (var effect in effectsEvaporateImpulseDeleteEffect) {
      if (observers.TryGetValue(0, out List<IEvaporateImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnEvaporateImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IEvaporateImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnEvaporateImpulseEffect(effect);
        }
        observersForEvaporateImpulse.Remove(effect.id);
      }
    }
    effectsEvaporateImpulseDeleteEffect.Clear();


    foreach (var effect in effectsEvaporateImpulseCreateEffect) {
      if (observers.TryGetValue(0, out List<IEvaporateImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnEvaporateImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IEvaporateImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnEvaporateImpulseEffect(effect);
        }
      }
    }
    effectsEvaporateImpulseCreateEffect.Clear();
  }
  public TimeScriptDirectiveUCIncarnation GetTimeScriptDirectiveUCIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsTimeScriptDirectiveUC[id].incarnation;
  }
  public bool TimeScriptDirectiveUCExists(int id) {
    return rootIncarnation.incarnationsTimeScriptDirectiveUC.ContainsKey(id);
  }
  public TimeScriptDirectiveUC GetTimeScriptDirectiveUC(int id) {
    return new TimeScriptDirectiveUC(this, id);
  }
  public List<TimeScriptDirectiveUC> AllTimeScriptDirectiveUC() {
    List<TimeScriptDirectiveUC> result = new List<TimeScriptDirectiveUC>(rootIncarnation.incarnationsTimeScriptDirectiveUC.Count);
    foreach (var id in rootIncarnation.incarnationsTimeScriptDirectiveUC.Keys) {
      result.Add(new TimeScriptDirectiveUC(this, id));
    }
    return result;
  }
  public IEnumerator<TimeScriptDirectiveUC> EnumAllTimeScriptDirectiveUC() {
    foreach (var id in rootIncarnation.incarnationsTimeScriptDirectiveUC.Keys) {
      yield return GetTimeScriptDirectiveUC(id);
    }
  }
  public void CheckHasTimeScriptDirectiveUC(TimeScriptDirectiveUC thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasTimeScriptDirectiveUC(thing.id);
  }
  public void CheckHasTimeScriptDirectiveUC(int id) {
    if (!rootIncarnation.incarnationsTimeScriptDirectiveUC.ContainsKey(id)) {
      throw new System.Exception("Invalid TimeScriptDirectiveUC: " + id);
    }
  }
  public void AddTimeScriptDirectiveUCObserver(int id, ITimeScriptDirectiveUCEffectObserver observer) {
    List<ITimeScriptDirectiveUCEffectObserver> obsies;
    if (!observersForTimeScriptDirectiveUC.TryGetValue(id, out obsies)) {
      obsies = new List<ITimeScriptDirectiveUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForTimeScriptDirectiveUC[id] = obsies;
  }

  public void RemoveTimeScriptDirectiveUCObserver(int id, ITimeScriptDirectiveUCEffectObserver observer) {
    if (observersForTimeScriptDirectiveUC.ContainsKey(id)) {
      var list = observersForTimeScriptDirectiveUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForTimeScriptDirectiveUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public TimeScriptDirectiveUC EffectTimeScriptDirectiveUCCreate(
      IRequestMutList script) {
    CheckUnlocked();
    CheckHasIRequestMutList(script);

    var id = NewId();
    var incarnation =
        new TimeScriptDirectiveUCIncarnation(
            script.id
            );
    EffectInternalCreateTimeScriptDirectiveUC(id, rootIncarnation.version, incarnation);
    return new TimeScriptDirectiveUC(this, id);
  }
  public void EffectInternalCreateTimeScriptDirectiveUC(
      int id,
      int incarnationVersion,
      TimeScriptDirectiveUCIncarnation incarnation) {
    CheckUnlocked();
    var effect = new TimeScriptDirectiveUCCreateEffect(id);
    rootIncarnation.incarnationsTimeScriptDirectiveUC.Add(
        id,
        new VersionAndIncarnation<TimeScriptDirectiveUCIncarnation>(
            incarnationVersion,
            incarnation));
    effectsTimeScriptDirectiveUCCreateEffect.Add(effect);
  }

  public void EffectTimeScriptDirectiveUCDelete(int id) {
    CheckUnlocked();
    var effect = new TimeScriptDirectiveUCDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsTimeScriptDirectiveUC[id];

    rootIncarnation.incarnationsTimeScriptDirectiveUC.Remove(id);
    effectsTimeScriptDirectiveUCDeleteEffect.Add(effect);
  }

     
  public int GetTimeScriptDirectiveUCHash(int id, int version, TimeScriptDirectiveUCIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.script.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastTimeScriptDirectiveUCEffects(
      SortedDictionary<int, List<ITimeScriptDirectiveUCEffectObserver>> observers) {
    foreach (var effect in effectsTimeScriptDirectiveUCDeleteEffect) {
      if (observers.TryGetValue(0, out List<ITimeScriptDirectiveUCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeScriptDirectiveUCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeScriptDirectiveUCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeScriptDirectiveUCEffect(effect);
        }
        observersForTimeScriptDirectiveUC.Remove(effect.id);
      }
    }
    effectsTimeScriptDirectiveUCDeleteEffect.Clear();


    foreach (var effect in effectsTimeScriptDirectiveUCCreateEffect) {
      if (observers.TryGetValue(0, out List<ITimeScriptDirectiveUCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeScriptDirectiveUCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeScriptDirectiveUCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeScriptDirectiveUCEffect(effect);
        }
      }
    }
    effectsTimeScriptDirectiveUCCreateEffect.Clear();
  }
  public TimeCloneAICapabilityUCIncarnation GetTimeCloneAICapabilityUCIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsTimeCloneAICapabilityUC[id].incarnation;
  }
  public bool TimeCloneAICapabilityUCExists(int id) {
    return rootIncarnation.incarnationsTimeCloneAICapabilityUC.ContainsKey(id);
  }
  public TimeCloneAICapabilityUC GetTimeCloneAICapabilityUC(int id) {
    return new TimeCloneAICapabilityUC(this, id);
  }
  public List<TimeCloneAICapabilityUC> AllTimeCloneAICapabilityUC() {
    List<TimeCloneAICapabilityUC> result = new List<TimeCloneAICapabilityUC>(rootIncarnation.incarnationsTimeCloneAICapabilityUC.Count);
    foreach (var id in rootIncarnation.incarnationsTimeCloneAICapabilityUC.Keys) {
      result.Add(new TimeCloneAICapabilityUC(this, id));
    }
    return result;
  }
  public IEnumerator<TimeCloneAICapabilityUC> EnumAllTimeCloneAICapabilityUC() {
    foreach (var id in rootIncarnation.incarnationsTimeCloneAICapabilityUC.Keys) {
      yield return GetTimeCloneAICapabilityUC(id);
    }
  }
  public void CheckHasTimeCloneAICapabilityUC(TimeCloneAICapabilityUC thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasTimeCloneAICapabilityUC(thing.id);
  }
  public void CheckHasTimeCloneAICapabilityUC(int id) {
    if (!rootIncarnation.incarnationsTimeCloneAICapabilityUC.ContainsKey(id)) {
      throw new System.Exception("Invalid TimeCloneAICapabilityUC: " + id);
    }
  }
  public void AddTimeCloneAICapabilityUCObserver(int id, ITimeCloneAICapabilityUCEffectObserver observer) {
    List<ITimeCloneAICapabilityUCEffectObserver> obsies;
    if (!observersForTimeCloneAICapabilityUC.TryGetValue(id, out obsies)) {
      obsies = new List<ITimeCloneAICapabilityUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForTimeCloneAICapabilityUC[id] = obsies;
  }

  public void RemoveTimeCloneAICapabilityUCObserver(int id, ITimeCloneAICapabilityUCEffectObserver observer) {
    if (observersForTimeCloneAICapabilityUC.ContainsKey(id)) {
      var list = observersForTimeCloneAICapabilityUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForTimeCloneAICapabilityUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public TimeCloneAICapabilityUC EffectTimeCloneAICapabilityUCCreate(
) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new TimeCloneAICapabilityUCIncarnation(

            );
    EffectInternalCreateTimeCloneAICapabilityUC(id, rootIncarnation.version, incarnation);
    return new TimeCloneAICapabilityUC(this, id);
  }
  public void EffectInternalCreateTimeCloneAICapabilityUC(
      int id,
      int incarnationVersion,
      TimeCloneAICapabilityUCIncarnation incarnation) {
    CheckUnlocked();
    var effect = new TimeCloneAICapabilityUCCreateEffect(id);
    rootIncarnation.incarnationsTimeCloneAICapabilityUC.Add(
        id,
        new VersionAndIncarnation<TimeCloneAICapabilityUCIncarnation>(
            incarnationVersion,
            incarnation));
    effectsTimeCloneAICapabilityUCCreateEffect.Add(effect);
  }

  public void EffectTimeCloneAICapabilityUCDelete(int id) {
    CheckUnlocked();
    var effect = new TimeCloneAICapabilityUCDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsTimeCloneAICapabilityUC[id];

    rootIncarnation.incarnationsTimeCloneAICapabilityUC.Remove(id);
    effectsTimeCloneAICapabilityUCDeleteEffect.Add(effect);
  }

     
  public int GetTimeCloneAICapabilityUCHash(int id, int version, TimeCloneAICapabilityUCIncarnation incarnation) {
    int result = id * version;
    return result;
  }
     
  public void BroadcastTimeCloneAICapabilityUCEffects(
      SortedDictionary<int, List<ITimeCloneAICapabilityUCEffectObserver>> observers) {
    foreach (var effect in effectsTimeCloneAICapabilityUCDeleteEffect) {
      if (observers.TryGetValue(0, out List<ITimeCloneAICapabilityUCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeCloneAICapabilityUCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeCloneAICapabilityUCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeCloneAICapabilityUCEffect(effect);
        }
        observersForTimeCloneAICapabilityUC.Remove(effect.id);
      }
    }
    effectsTimeCloneAICapabilityUCDeleteEffect.Clear();


    foreach (var effect in effectsTimeCloneAICapabilityUCCreateEffect) {
      if (observers.TryGetValue(0, out List<ITimeCloneAICapabilityUCEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeCloneAICapabilityUCEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeCloneAICapabilityUCEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeCloneAICapabilityUCEffect(effect);
        }
      }
    }
    effectsTimeCloneAICapabilityUCCreateEffect.Clear();
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
      int charge) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new BidingOperationUCIncarnation(
            charge
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
    result += id * version * 1 * incarnation.charge.GetDeterministicHashCode();
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


    foreach (var effect in effectsBidingOperationUCSetChargeEffect) {
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
    effectsBidingOperationUCSetChargeEffect.Clear();

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

  public void EffectBidingOperationUCSetCharge(int id, int newValue) {
    CheckUnlocked();
    CheckHasBidingOperationUC(id);
    var effect = new BidingOperationUCSetChargeEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsBidingOperationUC[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.charge;
      oldIncarnationAndVersion.incarnation.charge = newValue;

    } else {
      var newIncarnation =
          new BidingOperationUCIncarnation(
              newValue);
      rootIncarnation.incarnationsBidingOperationUC[id] =
          new VersionAndIncarnation<BidingOperationUCIncarnation>(
              rootIncarnation.version,
              newIncarnation);
    }

    effectsBidingOperationUCSetChargeEffect.Add(effect);
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
  public ContinueBidingImpulseIncarnation GetContinueBidingImpulseIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsContinueBidingImpulse[id].incarnation;
  }
  public bool ContinueBidingImpulseExists(int id) {
    return rootIncarnation.incarnationsContinueBidingImpulse.ContainsKey(id);
  }
  public ContinueBidingImpulse GetContinueBidingImpulse(int id) {
    return new ContinueBidingImpulse(this, id);
  }
  public List<ContinueBidingImpulse> AllContinueBidingImpulse() {
    List<ContinueBidingImpulse> result = new List<ContinueBidingImpulse>(rootIncarnation.incarnationsContinueBidingImpulse.Count);
    foreach (var id in rootIncarnation.incarnationsContinueBidingImpulse.Keys) {
      result.Add(new ContinueBidingImpulse(this, id));
    }
    return result;
  }
  public IEnumerator<ContinueBidingImpulse> EnumAllContinueBidingImpulse() {
    foreach (var id in rootIncarnation.incarnationsContinueBidingImpulse.Keys) {
      yield return GetContinueBidingImpulse(id);
    }
  }
  public void CheckHasContinueBidingImpulse(ContinueBidingImpulse thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasContinueBidingImpulse(thing.id);
  }
  public void CheckHasContinueBidingImpulse(int id) {
    if (!rootIncarnation.incarnationsContinueBidingImpulse.ContainsKey(id)) {
      throw new System.Exception("Invalid ContinueBidingImpulse: " + id);
    }
  }
  public void AddContinueBidingImpulseObserver(int id, IContinueBidingImpulseEffectObserver observer) {
    List<IContinueBidingImpulseEffectObserver> obsies;
    if (!observersForContinueBidingImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<IContinueBidingImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersForContinueBidingImpulse[id] = obsies;
  }

  public void RemoveContinueBidingImpulseObserver(int id, IContinueBidingImpulseEffectObserver observer) {
    if (observersForContinueBidingImpulse.ContainsKey(id)) {
      var list = observersForContinueBidingImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForContinueBidingImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public ContinueBidingImpulse EffectContinueBidingImpulseCreate(
      int weight) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new ContinueBidingImpulseIncarnation(
            weight
            );
    EffectInternalCreateContinueBidingImpulse(id, rootIncarnation.version, incarnation);
    return new ContinueBidingImpulse(this, id);
  }
  public void EffectInternalCreateContinueBidingImpulse(
      int id,
      int incarnationVersion,
      ContinueBidingImpulseIncarnation incarnation) {
    CheckUnlocked();
    var effect = new ContinueBidingImpulseCreateEffect(id);
    rootIncarnation.incarnationsContinueBidingImpulse.Add(
        id,
        new VersionAndIncarnation<ContinueBidingImpulseIncarnation>(
            incarnationVersion,
            incarnation));
    effectsContinueBidingImpulseCreateEffect.Add(effect);
  }

  public void EffectContinueBidingImpulseDelete(int id) {
    CheckUnlocked();
    var effect = new ContinueBidingImpulseDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsContinueBidingImpulse[id];

    rootIncarnation.incarnationsContinueBidingImpulse.Remove(id);
    effectsContinueBidingImpulseDeleteEffect.Add(effect);
  }

     
  public int GetContinueBidingImpulseHash(int id, int version, ContinueBidingImpulseIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.weight.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastContinueBidingImpulseEffects(
      SortedDictionary<int, List<IContinueBidingImpulseEffectObserver>> observers) {
    foreach (var effect in effectsContinueBidingImpulseDeleteEffect) {
      if (observers.TryGetValue(0, out List<IContinueBidingImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnContinueBidingImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IContinueBidingImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnContinueBidingImpulseEffect(effect);
        }
        observersForContinueBidingImpulse.Remove(effect.id);
      }
    }
    effectsContinueBidingImpulseDeleteEffect.Clear();


    foreach (var effect in effectsContinueBidingImpulseCreateEffect) {
      if (observers.TryGetValue(0, out List<IContinueBidingImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnContinueBidingImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IContinueBidingImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnContinueBidingImpulseEffect(effect);
        }
      }
    }
    effectsContinueBidingImpulseCreateEffect.Clear();
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
  public FireImpulseIncarnation GetFireImpulseIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsFireImpulse[id].incarnation;
  }
  public bool FireImpulseExists(int id) {
    return rootIncarnation.incarnationsFireImpulse.ContainsKey(id);
  }
  public FireImpulse GetFireImpulse(int id) {
    return new FireImpulse(this, id);
  }
  public List<FireImpulse> AllFireImpulse() {
    List<FireImpulse> result = new List<FireImpulse>(rootIncarnation.incarnationsFireImpulse.Count);
    foreach (var id in rootIncarnation.incarnationsFireImpulse.Keys) {
      result.Add(new FireImpulse(this, id));
    }
    return result;
  }
  public IEnumerator<FireImpulse> EnumAllFireImpulse() {
    foreach (var id in rootIncarnation.incarnationsFireImpulse.Keys) {
      yield return GetFireImpulse(id);
    }
  }
  public void CheckHasFireImpulse(FireImpulse thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasFireImpulse(thing.id);
  }
  public void CheckHasFireImpulse(int id) {
    if (!rootIncarnation.incarnationsFireImpulse.ContainsKey(id)) {
      throw new System.Exception("Invalid FireImpulse: " + id);
    }
  }
  public void AddFireImpulseObserver(int id, IFireImpulseEffectObserver observer) {
    List<IFireImpulseEffectObserver> obsies;
    if (!observersForFireImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<IFireImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersForFireImpulse[id] = obsies;
  }

  public void RemoveFireImpulseObserver(int id, IFireImpulseEffectObserver observer) {
    if (observersForFireImpulse.ContainsKey(id)) {
      var list = observersForFireImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForFireImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public FireImpulse EffectFireImpulseCreate(
      int weight,
      Unit targetUnit) {
    CheckUnlocked();
    CheckHasUnit(targetUnit);

    var id = NewId();
    var incarnation =
        new FireImpulseIncarnation(
            weight,
            targetUnit.id
            );
    EffectInternalCreateFireImpulse(id, rootIncarnation.version, incarnation);
    return new FireImpulse(this, id);
  }
  public void EffectInternalCreateFireImpulse(
      int id,
      int incarnationVersion,
      FireImpulseIncarnation incarnation) {
    CheckUnlocked();
    var effect = new FireImpulseCreateEffect(id);
    rootIncarnation.incarnationsFireImpulse.Add(
        id,
        new VersionAndIncarnation<FireImpulseIncarnation>(
            incarnationVersion,
            incarnation));
    effectsFireImpulseCreateEffect.Add(effect);
  }

  public void EffectFireImpulseDelete(int id) {
    CheckUnlocked();
    var effect = new FireImpulseDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsFireImpulse[id];

    rootIncarnation.incarnationsFireImpulse.Remove(id);
    effectsFireImpulseDeleteEffect.Add(effect);
  }

     
  public int GetFireImpulseHash(int id, int version, FireImpulseIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.weight.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.targetUnit.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastFireImpulseEffects(
      SortedDictionary<int, List<IFireImpulseEffectObserver>> observers) {
    foreach (var effect in effectsFireImpulseDeleteEffect) {
      if (observers.TryGetValue(0, out List<IFireImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnFireImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IFireImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnFireImpulseEffect(effect);
        }
        observersForFireImpulse.Remove(effect.id);
      }
    }
    effectsFireImpulseDeleteEffect.Clear();


    foreach (var effect in effectsFireImpulseCreateEffect) {
      if (observers.TryGetValue(0, out List<IFireImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnFireImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IFireImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnFireImpulseEffect(effect);
        }
      }
    }
    effectsFireImpulseCreateEffect.Clear();
  }
  public CounterImpulseIncarnation GetCounterImpulseIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsCounterImpulse[id].incarnation;
  }
  public bool CounterImpulseExists(int id) {
    return rootIncarnation.incarnationsCounterImpulse.ContainsKey(id);
  }
  public CounterImpulse GetCounterImpulse(int id) {
    return new CounterImpulse(this, id);
  }
  public List<CounterImpulse> AllCounterImpulse() {
    List<CounterImpulse> result = new List<CounterImpulse>(rootIncarnation.incarnationsCounterImpulse.Count);
    foreach (var id in rootIncarnation.incarnationsCounterImpulse.Keys) {
      result.Add(new CounterImpulse(this, id));
    }
    return result;
  }
  public IEnumerator<CounterImpulse> EnumAllCounterImpulse() {
    foreach (var id in rootIncarnation.incarnationsCounterImpulse.Keys) {
      yield return GetCounterImpulse(id);
    }
  }
  public void CheckHasCounterImpulse(CounterImpulse thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasCounterImpulse(thing.id);
  }
  public void CheckHasCounterImpulse(int id) {
    if (!rootIncarnation.incarnationsCounterImpulse.ContainsKey(id)) {
      throw new System.Exception("Invalid CounterImpulse: " + id);
    }
  }
  public void AddCounterImpulseObserver(int id, ICounterImpulseEffectObserver observer) {
    List<ICounterImpulseEffectObserver> obsies;
    if (!observersForCounterImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<ICounterImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersForCounterImpulse[id] = obsies;
  }

  public void RemoveCounterImpulseObserver(int id, ICounterImpulseEffectObserver observer) {
    if (observersForCounterImpulse.ContainsKey(id)) {
      var list = observersForCounterImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForCounterImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public CounterImpulse EffectCounterImpulseCreate(
      int weight) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new CounterImpulseIncarnation(
            weight
            );
    EffectInternalCreateCounterImpulse(id, rootIncarnation.version, incarnation);
    return new CounterImpulse(this, id);
  }
  public void EffectInternalCreateCounterImpulse(
      int id,
      int incarnationVersion,
      CounterImpulseIncarnation incarnation) {
    CheckUnlocked();
    var effect = new CounterImpulseCreateEffect(id);
    rootIncarnation.incarnationsCounterImpulse.Add(
        id,
        new VersionAndIncarnation<CounterImpulseIncarnation>(
            incarnationVersion,
            incarnation));
    effectsCounterImpulseCreateEffect.Add(effect);
  }

  public void EffectCounterImpulseDelete(int id) {
    CheckUnlocked();
    var effect = new CounterImpulseDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsCounterImpulse[id];

    rootIncarnation.incarnationsCounterImpulse.Remove(id);
    effectsCounterImpulseDeleteEffect.Add(effect);
  }

     
  public int GetCounterImpulseHash(int id, int version, CounterImpulseIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.weight.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastCounterImpulseEffects(
      SortedDictionary<int, List<ICounterImpulseEffectObserver>> observers) {
    foreach (var effect in effectsCounterImpulseDeleteEffect) {
      if (observers.TryGetValue(0, out List<ICounterImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnCounterImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ICounterImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnCounterImpulseEffect(effect);
        }
        observersForCounterImpulse.Remove(effect.id);
      }
    }
    effectsCounterImpulseDeleteEffect.Clear();


    foreach (var effect in effectsCounterImpulseCreateEffect) {
      if (observers.TryGetValue(0, out List<ICounterImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnCounterImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ICounterImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnCounterImpulseEffect(effect);
        }
      }
    }
    effectsCounterImpulseCreateEffect.Clear();
  }
  public DefendImpulseIncarnation GetDefendImpulseIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsDefendImpulse[id].incarnation;
  }
  public bool DefendImpulseExists(int id) {
    return rootIncarnation.incarnationsDefendImpulse.ContainsKey(id);
  }
  public DefendImpulse GetDefendImpulse(int id) {
    return new DefendImpulse(this, id);
  }
  public List<DefendImpulse> AllDefendImpulse() {
    List<DefendImpulse> result = new List<DefendImpulse>(rootIncarnation.incarnationsDefendImpulse.Count);
    foreach (var id in rootIncarnation.incarnationsDefendImpulse.Keys) {
      result.Add(new DefendImpulse(this, id));
    }
    return result;
  }
  public IEnumerator<DefendImpulse> EnumAllDefendImpulse() {
    foreach (var id in rootIncarnation.incarnationsDefendImpulse.Keys) {
      yield return GetDefendImpulse(id);
    }
  }
  public void CheckHasDefendImpulse(DefendImpulse thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasDefendImpulse(thing.id);
  }
  public void CheckHasDefendImpulse(int id) {
    if (!rootIncarnation.incarnationsDefendImpulse.ContainsKey(id)) {
      throw new System.Exception("Invalid DefendImpulse: " + id);
    }
  }
  public void AddDefendImpulseObserver(int id, IDefendImpulseEffectObserver observer) {
    List<IDefendImpulseEffectObserver> obsies;
    if (!observersForDefendImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<IDefendImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersForDefendImpulse[id] = obsies;
  }

  public void RemoveDefendImpulseObserver(int id, IDefendImpulseEffectObserver observer) {
    if (observersForDefendImpulse.ContainsKey(id)) {
      var list = observersForDefendImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForDefendImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public DefendImpulse EffectDefendImpulseCreate(
      int weight) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new DefendImpulseIncarnation(
            weight
            );
    EffectInternalCreateDefendImpulse(id, rootIncarnation.version, incarnation);
    return new DefendImpulse(this, id);
  }
  public void EffectInternalCreateDefendImpulse(
      int id,
      int incarnationVersion,
      DefendImpulseIncarnation incarnation) {
    CheckUnlocked();
    var effect = new DefendImpulseCreateEffect(id);
    rootIncarnation.incarnationsDefendImpulse.Add(
        id,
        new VersionAndIncarnation<DefendImpulseIncarnation>(
            incarnationVersion,
            incarnation));
    effectsDefendImpulseCreateEffect.Add(effect);
  }

  public void EffectDefendImpulseDelete(int id) {
    CheckUnlocked();
    var effect = new DefendImpulseDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsDefendImpulse[id];

    rootIncarnation.incarnationsDefendImpulse.Remove(id);
    effectsDefendImpulseDeleteEffect.Add(effect);
  }

     
  public int GetDefendImpulseHash(int id, int version, DefendImpulseIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.weight.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastDefendImpulseEffects(
      SortedDictionary<int, List<IDefendImpulseEffectObserver>> observers) {
    foreach (var effect in effectsDefendImpulseDeleteEffect) {
      if (observers.TryGetValue(0, out List<IDefendImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnDefendImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IDefendImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnDefendImpulseEffect(effect);
        }
        observersForDefendImpulse.Remove(effect.id);
      }
    }
    effectsDefendImpulseDeleteEffect.Clear();


    foreach (var effect in effectsDefendImpulseCreateEffect) {
      if (observers.TryGetValue(0, out List<IDefendImpulseEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnDefendImpulseEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IDefendImpulseEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnDefendImpulseEffect(effect);
        }
      }
    }
    effectsDefendImpulseCreateEffect.Clear();
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
      int weight,
      Unit targetUnit) {
    CheckUnlocked();
    CheckHasUnit(targetUnit);

    var id = NewId();
    var incarnation =
        new AttackImpulseIncarnation(
            weight,
            targetUnit.id
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
    result += id * version * 2 * incarnation.targetUnit.GetDeterministicHashCode();
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
      bool good,
      int strength) {
    CheckUnlocked();
    CheckHasIUnitEventMutList(events);
    CheckHasIUnitComponentMutBunch(components);

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
            good,
            strength
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
    result += id * version * 13 * incarnation.good.GetDeterministicHashCode();
    result += id * version * 14 * incarnation.strength.GetDeterministicHashCode();
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
              oldIncarnationAndVersion.incarnation.good,
              oldIncarnationAndVersion.incarnation.strength);
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
              oldIncarnationAndVersion.incarnation.good,
              oldIncarnationAndVersion.incarnation.strength);
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
              oldIncarnationAndVersion.incarnation.good,
              oldIncarnationAndVersion.incarnation.strength);
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
              oldIncarnationAndVersion.incarnation.good,
              oldIncarnationAndVersion.incarnation.strength);
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
              oldIncarnationAndVersion.incarnation.good,
              oldIncarnationAndVersion.incarnation.strength);
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
              oldIncarnationAndVersion.incarnation.good,
              oldIncarnationAndVersion.incarnation.strength);
      rootIncarnation.incarnationsUnit[id] =
          new VersionAndIncarnation<UnitIncarnation>(
              rootIncarnation.version,
              newIncarnation);
    }

    effectsUnitSetNextActionTimeEffect.Add(effect);
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
      ArmorMutSet membersArmorMutSet,
      InertiaRingMutSet membersInertiaRingMutSet,
      GlaiveMutSet membersGlaiveMutSet,
      ManaPotionMutSet membersManaPotionMutSet,
      HealthPotionMutSet membersHealthPotionMutSet,
      TimeScriptDirectiveUCMutSet membersTimeScriptDirectiveUCMutSet,
      KillDirectiveUCMutSet membersKillDirectiveUCMutSet,
      MoveDirectiveUCMutSet membersMoveDirectiveUCMutSet,
      WanderAICapabilityUCMutSet membersWanderAICapabilityUCMutSet,
      BideAICapabilityUCMutSet membersBideAICapabilityUCMutSet,
      TimeCloneAICapabilityUCMutSet membersTimeCloneAICapabilityUCMutSet,
      AttackAICapabilityUCMutSet membersAttackAICapabilityUCMutSet,
      CounteringUCMutSet membersCounteringUCMutSet,
      ShieldingUCMutSet membersShieldingUCMutSet,
      BidingOperationUCMutSet membersBidingOperationUCMutSet) {
    CheckUnlocked();
    CheckHasArmorMutSet(membersArmorMutSet);
    CheckHasInertiaRingMutSet(membersInertiaRingMutSet);
    CheckHasGlaiveMutSet(membersGlaiveMutSet);
    CheckHasManaPotionMutSet(membersManaPotionMutSet);
    CheckHasHealthPotionMutSet(membersHealthPotionMutSet);
    CheckHasTimeScriptDirectiveUCMutSet(membersTimeScriptDirectiveUCMutSet);
    CheckHasKillDirectiveUCMutSet(membersKillDirectiveUCMutSet);
    CheckHasMoveDirectiveUCMutSet(membersMoveDirectiveUCMutSet);
    CheckHasWanderAICapabilityUCMutSet(membersWanderAICapabilityUCMutSet);
    CheckHasBideAICapabilityUCMutSet(membersBideAICapabilityUCMutSet);
    CheckHasTimeCloneAICapabilityUCMutSet(membersTimeCloneAICapabilityUCMutSet);
    CheckHasAttackAICapabilityUCMutSet(membersAttackAICapabilityUCMutSet);
    CheckHasCounteringUCMutSet(membersCounteringUCMutSet);
    CheckHasShieldingUCMutSet(membersShieldingUCMutSet);
    CheckHasBidingOperationUCMutSet(membersBidingOperationUCMutSet);

    var id = NewId();
    var incarnation =
        new IUnitComponentMutBunchIncarnation(
            membersArmorMutSet.id,
            membersInertiaRingMutSet.id,
            membersGlaiveMutSet.id,
            membersManaPotionMutSet.id,
            membersHealthPotionMutSet.id,
            membersTimeScriptDirectiveUCMutSet.id,
            membersKillDirectiveUCMutSet.id,
            membersMoveDirectiveUCMutSet.id,
            membersWanderAICapabilityUCMutSet.id,
            membersBideAICapabilityUCMutSet.id,
            membersTimeCloneAICapabilityUCMutSet.id,
            membersAttackAICapabilityUCMutSet.id,
            membersCounteringUCMutSet.id,
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
    result += id * version * 1 * incarnation.membersArmorMutSet.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.membersInertiaRingMutSet.GetDeterministicHashCode();
    result += id * version * 3 * incarnation.membersGlaiveMutSet.GetDeterministicHashCode();
    result += id * version * 4 * incarnation.membersManaPotionMutSet.GetDeterministicHashCode();
    result += id * version * 5 * incarnation.membersHealthPotionMutSet.GetDeterministicHashCode();
    result += id * version * 6 * incarnation.membersTimeScriptDirectiveUCMutSet.GetDeterministicHashCode();
    result += id * version * 7 * incarnation.membersKillDirectiveUCMutSet.GetDeterministicHashCode();
    result += id * version * 8 * incarnation.membersMoveDirectiveUCMutSet.GetDeterministicHashCode();
    result += id * version * 9 * incarnation.membersWanderAICapabilityUCMutSet.GetDeterministicHashCode();
    result += id * version * 10 * incarnation.membersBideAICapabilityUCMutSet.GetDeterministicHashCode();
    result += id * version * 11 * incarnation.membersTimeCloneAICapabilityUCMutSet.GetDeterministicHashCode();
    result += id * version * 12 * incarnation.membersAttackAICapabilityUCMutSet.GetDeterministicHashCode();
    result += id * version * 13 * incarnation.membersCounteringUCMutSet.GetDeterministicHashCode();
    result += id * version * 14 * incarnation.membersShieldingUCMutSet.GetDeterministicHashCode();
    result += id * version * 15 * incarnation.membersBidingOperationUCMutSet.GetDeterministicHashCode();
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
    if (!object.ReferenceEquals(incarnation.actingUnit, null)) {
      result += id * version * 1 * incarnation.actingUnit.GetDeterministicHashCode();
    }
    result += id * version * 2 * incarnation.actingUnitDidAction.GetDeterministicHashCode();
    if (!object.ReferenceEquals(incarnation.remainingPreActingUnitComponents, null)) {
      result += id * version * 3 * incarnation.remainingPreActingUnitComponents.GetDeterministicHashCode();
    }
    if (!object.ReferenceEquals(incarnation.remainingPostActingUnitComponents, null)) {
      result += id * version * 4 * incarnation.remainingPostActingUnitComponents.GetDeterministicHashCode();
    }
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
      TimeCloneAICapabilityUCWeakMutSet membersTimeCloneAICapabilityUCWeakMutSet) {
    CheckUnlocked();
    CheckHasTimeCloneAICapabilityUCWeakMutSet(membersTimeCloneAICapabilityUCWeakMutSet);

    var id = NewId();
    var incarnation =
        new IPostActingUCWeakMutBunchIncarnation(
            membersTimeCloneAICapabilityUCWeakMutSet.id
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
    result += id * version * 1 * incarnation.membersTimeCloneAICapabilityUCWeakMutSet.GetDeterministicHashCode();
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
      CounteringUCWeakMutSet membersCounteringUCWeakMutSet,
      ShieldingUCWeakMutSet membersShieldingUCWeakMutSet,
      AttackAICapabilityUCWeakMutSet membersAttackAICapabilityUCWeakMutSet) {
    CheckUnlocked();
    CheckHasCounteringUCWeakMutSet(membersCounteringUCWeakMutSet);
    CheckHasShieldingUCWeakMutSet(membersShieldingUCWeakMutSet);
    CheckHasAttackAICapabilityUCWeakMutSet(membersAttackAICapabilityUCWeakMutSet);

    var id = NewId();
    var incarnation =
        new IPreActingUCWeakMutBunchIncarnation(
            membersCounteringUCWeakMutSet.id,
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
    result += id * version * 1 * incarnation.membersCounteringUCWeakMutSet.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.membersShieldingUCWeakMutSet.GetDeterministicHashCode();
    result += id * version * 3 * incarnation.membersAttackAICapabilityUCWeakMutSet.GetDeterministicHashCode();
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
      bool gauntletMode,
      LevelMutSet levels,
      Unit player,
      IRequest lastPlayerRequest,
      Level level,
      int time,
      ExecutionState executionState) {
    CheckUnlocked();
    CheckHasRand(rand);
    CheckHasLevelMutSet(levels);
    CheckHasExecutionState(executionState);

    var id = NewId();
    var incarnation =
        new GameIncarnation(
            rand.id,
            squareLevelsOnly,
            gauntletMode,
            levels.id,
            player.id,
            lastPlayerRequest,
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
    result += id * version * 3 * incarnation.gauntletMode.GetDeterministicHashCode();
    result += id * version * 4 * incarnation.levels.GetDeterministicHashCode();
    if (!object.ReferenceEquals(incarnation.player, null)) {
      result += id * version * 5 * incarnation.player.GetDeterministicHashCode();
    }
    if (!object.ReferenceEquals(incarnation.lastPlayerRequest, null)) {
      result += id * version * 6 * incarnation.lastPlayerRequest.GetDeterministicHashCode();
    }
    if (!object.ReferenceEquals(incarnation.level, null)) {
      result += id * version * 7 * incarnation.level.GetDeterministicHashCode();
    }
    result += id * version * 8 * incarnation.time.GetDeterministicHashCode();
    result += id * version * 9 * incarnation.executionState.GetDeterministicHashCode();
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

    foreach (var effect in effectsGameSetLastPlayerRequestEffect) {
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
    effectsGameSetLastPlayerRequestEffect.Clear();

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
              oldIncarnationAndVersion.incarnation.gauntletMode,
              oldIncarnationAndVersion.incarnation.levels,
              newValue.id,
              oldIncarnationAndVersion.incarnation.lastPlayerRequest,
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

  public void EffectGameSetLastPlayerRequest(int id, IRequest newValue) {
    CheckUnlocked();
    CheckHasGame(id);
    var effect = new GameSetLastPlayerRequestEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsGame[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.lastPlayerRequest;
      oldIncarnationAndVersion.incarnation.lastPlayerRequest = newValue;

    } else {
      var newIncarnation =
          new GameIncarnation(
              oldIncarnationAndVersion.incarnation.rand,
              oldIncarnationAndVersion.incarnation.squareLevelsOnly,
              oldIncarnationAndVersion.incarnation.gauntletMode,
              oldIncarnationAndVersion.incarnation.levels,
              oldIncarnationAndVersion.incarnation.player,
              newValue,
              oldIncarnationAndVersion.incarnation.level,
              oldIncarnationAndVersion.incarnation.time,
              oldIncarnationAndVersion.incarnation.executionState);
      rootIncarnation.incarnationsGame[id] =
          new VersionAndIncarnation<GameIncarnation>(
              rootIncarnation.version,
              newIncarnation);
    }

    effectsGameSetLastPlayerRequestEffect.Add(effect);
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
              oldIncarnationAndVersion.incarnation.gauntletMode,
              oldIncarnationAndVersion.incarnation.levels,
              oldIncarnationAndVersion.incarnation.player,
              oldIncarnationAndVersion.incarnation.lastPlayerRequest,
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
              oldIncarnationAndVersion.incarnation.gauntletMode,
              oldIncarnationAndVersion.incarnation.levels,
              oldIncarnationAndVersion.incarnation.player,
              oldIncarnationAndVersion.incarnation.lastPlayerRequest,
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

  public ILevelController GetILevelController(int id) {
    if (rootIncarnation.incarnationsSquareCaveLevelController.ContainsKey(id)) {
      return new SquareCaveLevelControllerAsILevelController(new SquareCaveLevelController(this, id));
    }
    if (rootIncarnation.incarnationsRidgeLevelController.ContainsKey(id)) {
      return new RidgeLevelControllerAsILevelController(new RidgeLevelController(this, id));
    }
    if (rootIncarnation.incarnationsGauntletLevelController.ContainsKey(id)) {
      return new GauntletLevelControllerAsILevelController(new GauntletLevelController(this, id));
    }
    if (rootIncarnation.incarnationsPreGauntletLevelController.ContainsKey(id)) {
      return new PreGauntletLevelControllerAsILevelController(new PreGauntletLevelController(this, id));
    }
    if (rootIncarnation.incarnationsRavashrikeLevelController.ContainsKey(id)) {
      return new RavashrikeLevelControllerAsILevelController(new RavashrikeLevelController(this, id));
    }
    if (rootIncarnation.incarnationsPentagonalCaveLevelController.ContainsKey(id)) {
      return new PentagonalCaveLevelControllerAsILevelController(new PentagonalCaveLevelController(this, id));
    }
    if (rootIncarnation.incarnationsCliffLevelController.ContainsKey(id)) {
      return new CliffLevelControllerAsILevelController(new CliffLevelController(this, id));
    }
    throw new Exception("Unknown ILevelController: " + id);
  }
  public ILevelController GetILevelControllerOrNull(int id) {
    if (rootIncarnation.incarnationsSquareCaveLevelController.ContainsKey(id)) {
      return new SquareCaveLevelControllerAsILevelController(new SquareCaveLevelController(this, id));
    }
    if (rootIncarnation.incarnationsRidgeLevelController.ContainsKey(id)) {
      return new RidgeLevelControllerAsILevelController(new RidgeLevelController(this, id));
    }
    if (rootIncarnation.incarnationsGauntletLevelController.ContainsKey(id)) {
      return new GauntletLevelControllerAsILevelController(new GauntletLevelController(this, id));
    }
    if (rootIncarnation.incarnationsPreGauntletLevelController.ContainsKey(id)) {
      return new PreGauntletLevelControllerAsILevelController(new PreGauntletLevelController(this, id));
    }
    if (rootIncarnation.incarnationsRavashrikeLevelController.ContainsKey(id)) {
      return new RavashrikeLevelControllerAsILevelController(new RavashrikeLevelController(this, id));
    }
    if (rootIncarnation.incarnationsPentagonalCaveLevelController.ContainsKey(id)) {
      return new PentagonalCaveLevelControllerAsILevelController(new PentagonalCaveLevelController(this, id));
    }
    if (rootIncarnation.incarnationsCliffLevelController.ContainsKey(id)) {
      return new CliffLevelControllerAsILevelController(new CliffLevelController(this, id));
    }
    return NullILevelController.Null;
  }
  public bool ILevelControllerExists(int id) {
    return GetILevelControllerOrNull(id) != null;
  }
  public void CheckHasILevelController(ILevelController thing) {
    GetILevelController(thing.id);
  }
  public void CheckHasILevelController(int id) {
    GetILevelController(id);
  }

  public ITerrainTileComponent GetITerrainTileComponent(int id) {
    if (rootIncarnation.incarnationsArmor.ContainsKey(id)) {
      return new ArmorAsITerrainTileComponent(new Armor(this, id));
    }
    if (rootIncarnation.incarnationsInertiaRing.ContainsKey(id)) {
      return new InertiaRingAsITerrainTileComponent(new InertiaRing(this, id));
    }
    if (rootIncarnation.incarnationsGlaive.ContainsKey(id)) {
      return new GlaiveAsITerrainTileComponent(new Glaive(this, id));
    }
    if (rootIncarnation.incarnationsManaPotion.ContainsKey(id)) {
      return new ManaPotionAsITerrainTileComponent(new ManaPotion(this, id));
    }
    if (rootIncarnation.incarnationsHealthPotion.ContainsKey(id)) {
      return new HealthPotionAsITerrainTileComponent(new HealthPotion(this, id));
    }
    if (rootIncarnation.incarnationsTimeAnchorTTC.ContainsKey(id)) {
      return new TimeAnchorTTCAsITerrainTileComponent(new TimeAnchorTTC(this, id));
    }
    if (rootIncarnation.incarnationsStaircaseTTC.ContainsKey(id)) {
      return new StaircaseTTCAsITerrainTileComponent(new StaircaseTTC(this, id));
    }
    if (rootIncarnation.incarnationsDecorativeTTC.ContainsKey(id)) {
      return new DecorativeTTCAsITerrainTileComponent(new DecorativeTTC(this, id));
    }
    throw new Exception("Unknown ITerrainTileComponent: " + id);
  }
  public ITerrainTileComponent GetITerrainTileComponentOrNull(int id) {
    if (rootIncarnation.incarnationsArmor.ContainsKey(id)) {
      return new ArmorAsITerrainTileComponent(new Armor(this, id));
    }
    if (rootIncarnation.incarnationsInertiaRing.ContainsKey(id)) {
      return new InertiaRingAsITerrainTileComponent(new InertiaRing(this, id));
    }
    if (rootIncarnation.incarnationsGlaive.ContainsKey(id)) {
      return new GlaiveAsITerrainTileComponent(new Glaive(this, id));
    }
    if (rootIncarnation.incarnationsManaPotion.ContainsKey(id)) {
      return new ManaPotionAsITerrainTileComponent(new ManaPotion(this, id));
    }
    if (rootIncarnation.incarnationsHealthPotion.ContainsKey(id)) {
      return new HealthPotionAsITerrainTileComponent(new HealthPotion(this, id));
    }
    if (rootIncarnation.incarnationsTimeAnchorTTC.ContainsKey(id)) {
      return new TimeAnchorTTCAsITerrainTileComponent(new TimeAnchorTTC(this, id));
    }
    if (rootIncarnation.incarnationsStaircaseTTC.ContainsKey(id)) {
      return new StaircaseTTCAsITerrainTileComponent(new StaircaseTTC(this, id));
    }
    if (rootIncarnation.incarnationsDecorativeTTC.ContainsKey(id)) {
      return new DecorativeTTCAsITerrainTileComponent(new DecorativeTTC(this, id));
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

  public IDefenseItem GetIDefenseItem(int id) {
    if (rootIncarnation.incarnationsArmor.ContainsKey(id)) {
      return new ArmorAsIDefenseItem(new Armor(this, id));
    }
    throw new Exception("Unknown IDefenseItem: " + id);
  }
  public IDefenseItem GetIDefenseItemOrNull(int id) {
    if (rootIncarnation.incarnationsArmor.ContainsKey(id)) {
      return new ArmorAsIDefenseItem(new Armor(this, id));
    }
    return NullIDefenseItem.Null;
  }
  public bool IDefenseItemExists(int id) {
    return GetIDefenseItemOrNull(id) != null;
  }
  public void CheckHasIDefenseItem(IDefenseItem thing) {
    GetIDefenseItem(thing.id);
  }
  public void CheckHasIDefenseItem(int id) {
    GetIDefenseItem(id);
  }

  public IInertiaItem GetIInertiaItem(int id) {
    if (rootIncarnation.incarnationsInertiaRing.ContainsKey(id)) {
      return new InertiaRingAsIInertiaItem(new InertiaRing(this, id));
    }
    throw new Exception("Unknown IInertiaItem: " + id);
  }
  public IInertiaItem GetIInertiaItemOrNull(int id) {
    if (rootIncarnation.incarnationsInertiaRing.ContainsKey(id)) {
      return new InertiaRingAsIInertiaItem(new InertiaRing(this, id));
    }
    return NullIInertiaItem.Null;
  }
  public bool IInertiaItemExists(int id) {
    return GetIInertiaItemOrNull(id) != null;
  }
  public void CheckHasIInertiaItem(IInertiaItem thing) {
    GetIInertiaItem(thing.id);
  }
  public void CheckHasIInertiaItem(int id) {
    GetIInertiaItem(id);
  }

  public IOffenseItem GetIOffenseItem(int id) {
    if (rootIncarnation.incarnationsGlaive.ContainsKey(id)) {
      return new GlaiveAsIOffenseItem(new Glaive(this, id));
    }
    throw new Exception("Unknown IOffenseItem: " + id);
  }
  public IOffenseItem GetIOffenseItemOrNull(int id) {
    if (rootIncarnation.incarnationsGlaive.ContainsKey(id)) {
      return new GlaiveAsIOffenseItem(new Glaive(this, id));
    }
    return NullIOffenseItem.Null;
  }
  public bool IOffenseItemExists(int id) {
    return GetIOffenseItemOrNull(id) != null;
  }
  public void CheckHasIOffenseItem(IOffenseItem thing) {
    GetIOffenseItem(thing.id);
  }
  public void CheckHasIOffenseItem(int id) {
    GetIOffenseItem(id);
  }

  public IUsableItem GetIUsableItem(int id) {
    if (rootIncarnation.incarnationsManaPotion.ContainsKey(id)) {
      return new ManaPotionAsIUsableItem(new ManaPotion(this, id));
    }
    if (rootIncarnation.incarnationsHealthPotion.ContainsKey(id)) {
      return new HealthPotionAsIUsableItem(new HealthPotion(this, id));
    }
    throw new Exception("Unknown IUsableItem: " + id);
  }
  public IUsableItem GetIUsableItemOrNull(int id) {
    if (rootIncarnation.incarnationsManaPotion.ContainsKey(id)) {
      return new ManaPotionAsIUsableItem(new ManaPotion(this, id));
    }
    if (rootIncarnation.incarnationsHealthPotion.ContainsKey(id)) {
      return new HealthPotionAsIUsableItem(new HealthPotion(this, id));
    }
    return NullIUsableItem.Null;
  }
  public bool IUsableItemExists(int id) {
    return GetIUsableItemOrNull(id) != null;
  }
  public void CheckHasIUsableItem(IUsableItem thing) {
    GetIUsableItem(thing.id);
  }
  public void CheckHasIUsableItem(int id) {
    GetIUsableItem(id);
  }

  public IItem GetIItem(int id) {
    if (rootIncarnation.incarnationsArmor.ContainsKey(id)) {
      return new ArmorAsIItem(new Armor(this, id));
    }
    if (rootIncarnation.incarnationsInertiaRing.ContainsKey(id)) {
      return new InertiaRingAsIItem(new InertiaRing(this, id));
    }
    if (rootIncarnation.incarnationsGlaive.ContainsKey(id)) {
      return new GlaiveAsIItem(new Glaive(this, id));
    }
    if (rootIncarnation.incarnationsManaPotion.ContainsKey(id)) {
      return new ManaPotionAsIItem(new ManaPotion(this, id));
    }
    if (rootIncarnation.incarnationsHealthPotion.ContainsKey(id)) {
      return new HealthPotionAsIItem(new HealthPotion(this, id));
    }
    throw new Exception("Unknown IItem: " + id);
  }
  public IItem GetIItemOrNull(int id) {
    if (rootIncarnation.incarnationsArmor.ContainsKey(id)) {
      return new ArmorAsIItem(new Armor(this, id));
    }
    if (rootIncarnation.incarnationsInertiaRing.ContainsKey(id)) {
      return new InertiaRingAsIItem(new InertiaRing(this, id));
    }
    if (rootIncarnation.incarnationsGlaive.ContainsKey(id)) {
      return new GlaiveAsIItem(new Glaive(this, id));
    }
    if (rootIncarnation.incarnationsManaPotion.ContainsKey(id)) {
      return new ManaPotionAsIItem(new ManaPotion(this, id));
    }
    if (rootIncarnation.incarnationsHealthPotion.ContainsKey(id)) {
      return new HealthPotionAsIItem(new HealthPotion(this, id));
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
    if (rootIncarnation.incarnationsTimeScriptDirectiveUC.ContainsKey(id)) {
      return new TimeScriptDirectiveUCAsIDirectiveUC(new TimeScriptDirectiveUC(this, id));
    }
    if (rootIncarnation.incarnationsKillDirectiveUC.ContainsKey(id)) {
      return new KillDirectiveUCAsIDirectiveUC(new KillDirectiveUC(this, id));
    }
    if (rootIncarnation.incarnationsMoveDirectiveUC.ContainsKey(id)) {
      return new MoveDirectiveUCAsIDirectiveUC(new MoveDirectiveUC(this, id));
    }
    throw new Exception("Unknown IDirectiveUC: " + id);
  }
  public IDirectiveUC GetIDirectiveUCOrNull(int id) {
    if (rootIncarnation.incarnationsTimeScriptDirectiveUC.ContainsKey(id)) {
      return new TimeScriptDirectiveUCAsIDirectiveUC(new TimeScriptDirectiveUC(this, id));
    }
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
    if (rootIncarnation.incarnationsTimeCloneAICapabilityUC.ContainsKey(id)) {
      return new TimeCloneAICapabilityUCAsIAICapabilityUC(new TimeCloneAICapabilityUC(this, id));
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
    if (rootIncarnation.incarnationsTimeCloneAICapabilityUC.ContainsKey(id)) {
      return new TimeCloneAICapabilityUCAsIAICapabilityUC(new TimeCloneAICapabilityUC(this, id));
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
    if (rootIncarnation.incarnationsTimeCloneAICapabilityUC.ContainsKey(id)) {
      return new TimeCloneAICapabilityUCAsIPostActingUC(new TimeCloneAICapabilityUC(this, id));
    }
    throw new Exception("Unknown IPostActingUC: " + id);
  }
  public IPostActingUC GetIPostActingUCOrNull(int id) {
    if (rootIncarnation.incarnationsTimeCloneAICapabilityUC.ContainsKey(id)) {
      return new TimeCloneAICapabilityUCAsIPostActingUC(new TimeCloneAICapabilityUC(this, id));
    }
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
    if (rootIncarnation.incarnationsCounteringUC.ContainsKey(id)) {
      return new CounteringUCAsIPreActingUC(new CounteringUC(this, id));
    }
    if (rootIncarnation.incarnationsShieldingUC.ContainsKey(id)) {
      return new ShieldingUCAsIPreActingUC(new ShieldingUC(this, id));
    }
    if (rootIncarnation.incarnationsAttackAICapabilityUC.ContainsKey(id)) {
      return new AttackAICapabilityUCAsIPreActingUC(new AttackAICapabilityUC(this, id));
    }
    throw new Exception("Unknown IPreActingUC: " + id);
  }
  public IPreActingUC GetIPreActingUCOrNull(int id) {
    if (rootIncarnation.incarnationsCounteringUC.ContainsKey(id)) {
      return new CounteringUCAsIPreActingUC(new CounteringUC(this, id));
    }
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

  public IReactingToAttacksUC GetIReactingToAttacksUC(int id) {
    if (rootIncarnation.incarnationsCounteringUC.ContainsKey(id)) {
      return new CounteringUCAsIReactingToAttacksUC(new CounteringUC(this, id));
    }
    throw new Exception("Unknown IReactingToAttacksUC: " + id);
  }
  public IReactingToAttacksUC GetIReactingToAttacksUCOrNull(int id) {
    if (rootIncarnation.incarnationsCounteringUC.ContainsKey(id)) {
      return new CounteringUCAsIReactingToAttacksUC(new CounteringUC(this, id));
    }
    return NullIReactingToAttacksUC.Null;
  }
  public bool IReactingToAttacksUCExists(int id) {
    return GetIReactingToAttacksUCOrNull(id) != null;
  }
  public void CheckHasIReactingToAttacksUC(IReactingToAttacksUC thing) {
    GetIReactingToAttacksUC(thing.id);
  }
  public void CheckHasIReactingToAttacksUC(int id) {
    GetIReactingToAttacksUC(id);
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
    if (rootIncarnation.incarnationsArmor.ContainsKey(id)) {
      return new ArmorAsIUnitComponent(new Armor(this, id));
    }
    if (rootIncarnation.incarnationsInertiaRing.ContainsKey(id)) {
      return new InertiaRingAsIUnitComponent(new InertiaRing(this, id));
    }
    if (rootIncarnation.incarnationsGlaive.ContainsKey(id)) {
      return new GlaiveAsIUnitComponent(new Glaive(this, id));
    }
    if (rootIncarnation.incarnationsManaPotion.ContainsKey(id)) {
      return new ManaPotionAsIUnitComponent(new ManaPotion(this, id));
    }
    if (rootIncarnation.incarnationsHealthPotion.ContainsKey(id)) {
      return new HealthPotionAsIUnitComponent(new HealthPotion(this, id));
    }
    if (rootIncarnation.incarnationsTimeScriptDirectiveUC.ContainsKey(id)) {
      return new TimeScriptDirectiveUCAsIUnitComponent(new TimeScriptDirectiveUC(this, id));
    }
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
    if (rootIncarnation.incarnationsTimeCloneAICapabilityUC.ContainsKey(id)) {
      return new TimeCloneAICapabilityUCAsIUnitComponent(new TimeCloneAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsAttackAICapabilityUC.ContainsKey(id)) {
      return new AttackAICapabilityUCAsIUnitComponent(new AttackAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsCounteringUC.ContainsKey(id)) {
      return new CounteringUCAsIUnitComponent(new CounteringUC(this, id));
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
    if (rootIncarnation.incarnationsArmor.ContainsKey(id)) {
      return new ArmorAsIUnitComponent(new Armor(this, id));
    }
    if (rootIncarnation.incarnationsInertiaRing.ContainsKey(id)) {
      return new InertiaRingAsIUnitComponent(new InertiaRing(this, id));
    }
    if (rootIncarnation.incarnationsGlaive.ContainsKey(id)) {
      return new GlaiveAsIUnitComponent(new Glaive(this, id));
    }
    if (rootIncarnation.incarnationsManaPotion.ContainsKey(id)) {
      return new ManaPotionAsIUnitComponent(new ManaPotion(this, id));
    }
    if (rootIncarnation.incarnationsHealthPotion.ContainsKey(id)) {
      return new HealthPotionAsIUnitComponent(new HealthPotion(this, id));
    }
    if (rootIncarnation.incarnationsTimeScriptDirectiveUC.ContainsKey(id)) {
      return new TimeScriptDirectiveUCAsIUnitComponent(new TimeScriptDirectiveUC(this, id));
    }
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
    if (rootIncarnation.incarnationsTimeCloneAICapabilityUC.ContainsKey(id)) {
      return new TimeCloneAICapabilityUCAsIUnitComponent(new TimeCloneAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsAttackAICapabilityUC.ContainsKey(id)) {
      return new AttackAICapabilityUCAsIUnitComponent(new AttackAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsCounteringUC.ContainsKey(id)) {
      return new CounteringUCAsIUnitComponent(new CounteringUC(this, id));
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
    if (rootIncarnation.incarnationsEvaporateImpulse.ContainsKey(id)) {
      return new EvaporateImpulseAsIImpulse(new EvaporateImpulse(this, id));
    }
    if (rootIncarnation.incarnationsUnleashBideImpulse.ContainsKey(id)) {
      return new UnleashBideImpulseAsIImpulse(new UnleashBideImpulse(this, id));
    }
    if (rootIncarnation.incarnationsContinueBidingImpulse.ContainsKey(id)) {
      return new ContinueBidingImpulseAsIImpulse(new ContinueBidingImpulse(this, id));
    }
    if (rootIncarnation.incarnationsStartBidingImpulse.ContainsKey(id)) {
      return new StartBidingImpulseAsIImpulse(new StartBidingImpulse(this, id));
    }
    if (rootIncarnation.incarnationsFireImpulse.ContainsKey(id)) {
      return new FireImpulseAsIImpulse(new FireImpulse(this, id));
    }
    if (rootIncarnation.incarnationsCounterImpulse.ContainsKey(id)) {
      return new CounterImpulseAsIImpulse(new CounterImpulse(this, id));
    }
    if (rootIncarnation.incarnationsDefendImpulse.ContainsKey(id)) {
      return new DefendImpulseAsIImpulse(new DefendImpulse(this, id));
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
    if (rootIncarnation.incarnationsEvaporateImpulse.ContainsKey(id)) {
      return new EvaporateImpulseAsIImpulse(new EvaporateImpulse(this, id));
    }
    if (rootIncarnation.incarnationsUnleashBideImpulse.ContainsKey(id)) {
      return new UnleashBideImpulseAsIImpulse(new UnleashBideImpulse(this, id));
    }
    if (rootIncarnation.incarnationsContinueBidingImpulse.ContainsKey(id)) {
      return new ContinueBidingImpulseAsIImpulse(new ContinueBidingImpulse(this, id));
    }
    if (rootIncarnation.incarnationsStartBidingImpulse.ContainsKey(id)) {
      return new StartBidingImpulseAsIImpulse(new StartBidingImpulse(this, id));
    }
    if (rootIncarnation.incarnationsFireImpulse.ContainsKey(id)) {
      return new FireImpulseAsIImpulse(new FireImpulse(this, id));
    }
    if (rootIncarnation.incarnationsCounterImpulse.ContainsKey(id)) {
      return new CounterImpulseAsIImpulse(new CounterImpulse(this, id));
    }
    if (rootIncarnation.incarnationsDefendImpulse.ContainsKey(id)) {
      return new DefendImpulseAsIImpulse(new DefendImpulse(this, id));
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
    if (rootIncarnation.incarnationsTimeAnchorTTC.ContainsKey(id)) {
      return new TimeAnchorTTCAsIDestructible(new TimeAnchorTTC(this, id));
    }
    if (rootIncarnation.incarnationsStaircaseTTC.ContainsKey(id)) {
      return new StaircaseTTCAsIDestructible(new StaircaseTTC(this, id));
    }
    if (rootIncarnation.incarnationsDecorativeTTC.ContainsKey(id)) {
      return new DecorativeTTCAsIDestructible(new DecorativeTTC(this, id));
    }
    if (rootIncarnation.incarnationsArmor.ContainsKey(id)) {
      return new ArmorAsIDestructible(new Armor(this, id));
    }
    if (rootIncarnation.incarnationsInertiaRing.ContainsKey(id)) {
      return new InertiaRingAsIDestructible(new InertiaRing(this, id));
    }
    if (rootIncarnation.incarnationsGlaive.ContainsKey(id)) {
      return new GlaiveAsIDestructible(new Glaive(this, id));
    }
    if (rootIncarnation.incarnationsManaPotion.ContainsKey(id)) {
      return new ManaPotionAsIDestructible(new ManaPotion(this, id));
    }
    if (rootIncarnation.incarnationsHealthPotion.ContainsKey(id)) {
      return new HealthPotionAsIDestructible(new HealthPotion(this, id));
    }
    if (rootIncarnation.incarnationsTimeScriptDirectiveUC.ContainsKey(id)) {
      return new TimeScriptDirectiveUCAsIDestructible(new TimeScriptDirectiveUC(this, id));
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
    if (rootIncarnation.incarnationsTimeCloneAICapabilityUC.ContainsKey(id)) {
      return new TimeCloneAICapabilityUCAsIDestructible(new TimeCloneAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsAttackAICapabilityUC.ContainsKey(id)) {
      return new AttackAICapabilityUCAsIDestructible(new AttackAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsCounteringUC.ContainsKey(id)) {
      return new CounteringUCAsIDestructible(new CounteringUC(this, id));
    }
    if (rootIncarnation.incarnationsShieldingUC.ContainsKey(id)) {
      return new ShieldingUCAsIDestructible(new ShieldingUC(this, id));
    }
    if (rootIncarnation.incarnationsBidingOperationUC.ContainsKey(id)) {
      return new BidingOperationUCAsIDestructible(new BidingOperationUC(this, id));
    }
    if (rootIncarnation.incarnationsEvaporateImpulse.ContainsKey(id)) {
      return new EvaporateImpulseAsIDestructible(new EvaporateImpulse(this, id));
    }
    if (rootIncarnation.incarnationsUnleashBideImpulse.ContainsKey(id)) {
      return new UnleashBideImpulseAsIDestructible(new UnleashBideImpulse(this, id));
    }
    if (rootIncarnation.incarnationsContinueBidingImpulse.ContainsKey(id)) {
      return new ContinueBidingImpulseAsIDestructible(new ContinueBidingImpulse(this, id));
    }
    if (rootIncarnation.incarnationsStartBidingImpulse.ContainsKey(id)) {
      return new StartBidingImpulseAsIDestructible(new StartBidingImpulse(this, id));
    }
    if (rootIncarnation.incarnationsFireImpulse.ContainsKey(id)) {
      return new FireImpulseAsIDestructible(new FireImpulse(this, id));
    }
    if (rootIncarnation.incarnationsCounterImpulse.ContainsKey(id)) {
      return new CounterImpulseAsIDestructible(new CounterImpulse(this, id));
    }
    if (rootIncarnation.incarnationsDefendImpulse.ContainsKey(id)) {
      return new DefendImpulseAsIDestructible(new DefendImpulse(this, id));
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
    if (rootIncarnation.incarnationsTimeAnchorTTC.ContainsKey(id)) {
      return new TimeAnchorTTCAsIDestructible(new TimeAnchorTTC(this, id));
    }
    if (rootIncarnation.incarnationsStaircaseTTC.ContainsKey(id)) {
      return new StaircaseTTCAsIDestructible(new StaircaseTTC(this, id));
    }
    if (rootIncarnation.incarnationsDecorativeTTC.ContainsKey(id)) {
      return new DecorativeTTCAsIDestructible(new DecorativeTTC(this, id));
    }
    if (rootIncarnation.incarnationsArmor.ContainsKey(id)) {
      return new ArmorAsIDestructible(new Armor(this, id));
    }
    if (rootIncarnation.incarnationsInertiaRing.ContainsKey(id)) {
      return new InertiaRingAsIDestructible(new InertiaRing(this, id));
    }
    if (rootIncarnation.incarnationsGlaive.ContainsKey(id)) {
      return new GlaiveAsIDestructible(new Glaive(this, id));
    }
    if (rootIncarnation.incarnationsManaPotion.ContainsKey(id)) {
      return new ManaPotionAsIDestructible(new ManaPotion(this, id));
    }
    if (rootIncarnation.incarnationsHealthPotion.ContainsKey(id)) {
      return new HealthPotionAsIDestructible(new HealthPotion(this, id));
    }
    if (rootIncarnation.incarnationsTimeScriptDirectiveUC.ContainsKey(id)) {
      return new TimeScriptDirectiveUCAsIDestructible(new TimeScriptDirectiveUC(this, id));
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
    if (rootIncarnation.incarnationsTimeCloneAICapabilityUC.ContainsKey(id)) {
      return new TimeCloneAICapabilityUCAsIDestructible(new TimeCloneAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsAttackAICapabilityUC.ContainsKey(id)) {
      return new AttackAICapabilityUCAsIDestructible(new AttackAICapabilityUC(this, id));
    }
    if (rootIncarnation.incarnationsCounteringUC.ContainsKey(id)) {
      return new CounteringUCAsIDestructible(new CounteringUC(this, id));
    }
    if (rootIncarnation.incarnationsShieldingUC.ContainsKey(id)) {
      return new ShieldingUCAsIDestructible(new ShieldingUC(this, id));
    }
    if (rootIncarnation.incarnationsBidingOperationUC.ContainsKey(id)) {
      return new BidingOperationUCAsIDestructible(new BidingOperationUC(this, id));
    }
    if (rootIncarnation.incarnationsEvaporateImpulse.ContainsKey(id)) {
      return new EvaporateImpulseAsIDestructible(new EvaporateImpulse(this, id));
    }
    if (rootIncarnation.incarnationsUnleashBideImpulse.ContainsKey(id)) {
      return new UnleashBideImpulseAsIDestructible(new UnleashBideImpulse(this, id));
    }
    if (rootIncarnation.incarnationsContinueBidingImpulse.ContainsKey(id)) {
      return new ContinueBidingImpulseAsIDestructible(new ContinueBidingImpulse(this, id));
    }
    if (rootIncarnation.incarnationsStartBidingImpulse.ContainsKey(id)) {
      return new StartBidingImpulseAsIDestructible(new StartBidingImpulse(this, id));
    }
    if (rootIncarnation.incarnationsFireImpulse.ContainsKey(id)) {
      return new FireImpulseAsIDestructible(new FireImpulse(this, id));
    }
    if (rootIncarnation.incarnationsCounterImpulse.ContainsKey(id)) {
      return new CounterImpulseAsIDestructible(new CounterImpulse(this, id));
    }
    if (rootIncarnation.incarnationsDefendImpulse.ContainsKey(id)) {
      return new DefendImpulseAsIDestructible(new DefendImpulse(this, id));
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

    public int GetIRequestMutListHash(int id, int version, IRequestMutListIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.list) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public IRequestMutListIncarnation GetIRequestMutListIncarnation(int id) {
      return rootIncarnation.incarnationsIRequestMutList[id].incarnation;
    }
    public IRequestMutList GetIRequestMutList(int id) {
      return new IRequestMutList(this, id);
    }
    public List<IRequestMutList> AllIRequestMutList() {
      List<IRequestMutList> result = new List<IRequestMutList>(rootIncarnation.incarnationsIRequestMutList.Count);
      foreach (var id in rootIncarnation.incarnationsIRequestMutList.Keys) {
        result.Add(new IRequestMutList(this, id));
      }
      return result;
    }
    public bool IRequestMutListExists(int id) {
      return rootIncarnation.incarnationsIRequestMutList.ContainsKey(id);
    }
    public void CheckHasIRequestMutList(IRequestMutList thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasIRequestMutList(thing.id);
    }
    public void CheckHasIRequestMutList(int id) {
      if (!rootIncarnation.incarnationsIRequestMutList.ContainsKey(id)) {
        throw new System.Exception("Invalid IRequestMutList}: " + id);
      }
    }
    public IRequestMutList EffectIRequestMutListCreate() {
      CheckUnlocked();
      var id = NewId();
      EffectInternalCreateIRequestMutList(id, rootIncarnation.version, new IRequestMutListIncarnation(new List<IRequest>()));
      return new IRequestMutList(this, id);
    }
    public IRequestMutList EffectIRequestMutListCreate(IEnumerable<IRequest> elements) {
      var id = NewId();
      var incarnation = new IRequestMutListIncarnation(new List<IRequest>(elements));
      EffectInternalCreateIRequestMutList(id, rootIncarnation.version, incarnation);
      return new IRequestMutList(this, id);
    }
    public void EffectInternalCreateIRequestMutList(int id, int incarnationVersion, IRequestMutListIncarnation incarnation) {
      var effect = new IRequestMutListCreateEffect(id);
      rootIncarnation.incarnationsIRequestMutList
          .Add(
              id,
              new VersionAndIncarnation<IRequestMutListIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsIRequestMutListCreateEffect.Add(effect);
    }
    public void EffectIRequestMutListDelete(int id) {
      CheckUnlocked();
      var effect = new IRequestMutListDeleteEffect(id);
      effectsIRequestMutListDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsIRequestMutList[id];
      rootIncarnation.incarnationsIRequestMutList.Remove(id);
    }
    public void EffectIRequestMutListAdd(int listId, IRequest element) {
      CheckUnlocked();
      CheckHasIRequestMutList(listId);

    
      var effect = new IRequestMutListAddEffect(listId, element);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsIRequestMutList[listId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.list.Add(element);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.list;
        var newMap = new List<IRequest>(oldMap);
        newMap.Add(element);
        var newIncarnation = new IRequestMutListIncarnation(newMap);
        rootIncarnation.incarnationsIRequestMutList[listId] =
            new VersionAndIncarnation<IRequestMutListIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsIRequestMutListAddEffect.Add(effect);
    }
    public void EffectIRequestMutListRemoveAt(int listId, int index) {
      CheckUnlocked();
      CheckHasIRequestMutList(listId);

      var effect = new IRequestMutListRemoveEffect(listId, index);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsIRequestMutList[listId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        var oldElement = oldIncarnationAndVersion.incarnation.list[index];
        oldIncarnationAndVersion.incarnation.list.RemoveAt(index);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.list;
        var newMap = new List<IRequest>(oldMap);
        newMap.RemoveAt(index);
        var newIncarnation = new IRequestMutListIncarnation(newMap);
        rootIncarnation.incarnationsIRequestMutList[listId] =
            new VersionAndIncarnation<IRequestMutListIncarnation>(
                rootIncarnation.version, newIncarnation);

      }
      effectsIRequestMutListRemoveEffect.Add(effect);
    }
       
    public void AddIRequestMutListObserver(int id, IIRequestMutListEffectObserver observer) {
      List<IIRequestMutListEffectObserver> obsies;
      if (!observersForIRequestMutList.TryGetValue(id, out obsies)) {
        obsies = new List<IIRequestMutListEffectObserver>();
      }
      obsies.Add(observer);
      observersForIRequestMutList[id] = obsies;
    }

    public void RemoveIRequestMutListObserver(int id, IIRequestMutListEffectObserver observer) {
      if (observersForIRequestMutList.ContainsKey(id)) {
        var list = observersForIRequestMutList[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForIRequestMutList.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

  public void BroadcastIRequestMutListEffects(
      SortedDictionary<int, List<IIRequestMutListEffectObserver>> observers) {
    foreach (var effect in effectsIRequestMutListDeleteEffect) {
      if (observers.TryGetValue(0, out List<IIRequestMutListEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnIRequestMutListEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IIRequestMutListEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnIRequestMutListEffect(effect);
        }
        observersForIRequestMutList.Remove(effect.id);
      }
    }
    effectsIRequestMutListDeleteEffect.Clear();

    foreach (var effect in effectsIRequestMutListAddEffect) {
      if (observers.TryGetValue(0, out List<IIRequestMutListEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnIRequestMutListEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IIRequestMutListEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnIRequestMutListEffect(effect);
        }
      }
    }
    effectsIRequestMutListAddEffect.Clear();

    foreach (var effect in effectsIRequestMutListRemoveEffect) {
      if (observers.TryGetValue(0, out List<IIRequestMutListEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnIRequestMutListEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IIRequestMutListEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnIRequestMutListEffect(effect);
        }
      }
    }
    effectsIRequestMutListRemoveEffect.Clear();

    foreach (var effect in effectsIRequestMutListCreateEffect) {
      if (observers.TryGetValue(0, out List<IIRequestMutListEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnIRequestMutListEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IIRequestMutListEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnIRequestMutListEffect(effect);
        }
      }
    }
    effectsIRequestMutListCreateEffect.Clear();

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

    public int GetCounteringUCWeakMutSetHash(int id, int version, CounteringUCWeakMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public CounteringUCWeakMutSetIncarnation GetCounteringUCWeakMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsCounteringUCWeakMutSet[id].incarnation;
    }
    public CounteringUCWeakMutSet GetCounteringUCWeakMutSet(int id) {
      return new CounteringUCWeakMutSet(this, id);
    }
    public List<CounteringUCWeakMutSet> AllCounteringUCWeakMutSet() {
      List<CounteringUCWeakMutSet> result = new List<CounteringUCWeakMutSet>(rootIncarnation.incarnationsCounteringUCWeakMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsCounteringUCWeakMutSet.Keys) {
        result.Add(new CounteringUCWeakMutSet(this, id));
      }
      return result;
    }
    public bool CounteringUCWeakMutSetExists(int id) {
      return rootIncarnation.incarnationsCounteringUCWeakMutSet.ContainsKey(id);
    }
    public void CheckHasCounteringUCWeakMutSet(CounteringUCWeakMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasCounteringUCWeakMutSet(thing.id);
    }
    public void CheckHasCounteringUCWeakMutSet(int id) {
      if (!rootIncarnation.incarnationsCounteringUCWeakMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid CounteringUCWeakMutSet}: " + id);
      }
    }
    public CounteringUCWeakMutSet EffectCounteringUCWeakMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new CounteringUCWeakMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateCounteringUCWeakMutSet(id, rootIncarnation.version, incarnation);
      return new CounteringUCWeakMutSet(this, id);
    }
    public void EffectInternalCreateCounteringUCWeakMutSet(int id, int incarnationVersion, CounteringUCWeakMutSetIncarnation incarnation) {
      var effect = new CounteringUCWeakMutSetCreateEffect(id);
      rootIncarnation.incarnationsCounteringUCWeakMutSet
          .Add(
              id,
              new VersionAndIncarnation<CounteringUCWeakMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsCounteringUCWeakMutSetCreateEffect.Add(effect);
    }
    public void EffectCounteringUCWeakMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new CounteringUCWeakMutSetDeleteEffect(id);
      effectsCounteringUCWeakMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsCounteringUCWeakMutSet[id];
      rootIncarnation.incarnationsCounteringUCWeakMutSet.Remove(id);
    }

       
    public void EffectCounteringUCWeakMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasCounteringUCWeakMutSet(setId);
      CheckHasCounteringUC(elementId);

      var effect = new CounteringUCWeakMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsCounteringUCWeakMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new CounteringUCWeakMutSetIncarnation(newMap);
        rootIncarnation.incarnationsCounteringUCWeakMutSet[setId] =
            new VersionAndIncarnation<CounteringUCWeakMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsCounteringUCWeakMutSetAddEffect.Add(effect);
    }
    public void EffectCounteringUCWeakMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasCounteringUCWeakMutSet(setId);
      CheckHasCounteringUC(elementId);

      var effect = new CounteringUCWeakMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsCounteringUCWeakMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new CounteringUCWeakMutSetIncarnation(newMap);
        rootIncarnation.incarnationsCounteringUCWeakMutSet[setId] =
            new VersionAndIncarnation<CounteringUCWeakMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsCounteringUCWeakMutSetRemoveEffect.Add(effect);
    }

       
    public void AddCounteringUCWeakMutSetObserver(int id, ICounteringUCWeakMutSetEffectObserver observer) {
      List<ICounteringUCWeakMutSetEffectObserver> obsies;
      if (!observersForCounteringUCWeakMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ICounteringUCWeakMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForCounteringUCWeakMutSet[id] = obsies;
    }

    public void RemoveCounteringUCWeakMutSetObserver(int id, ICounteringUCWeakMutSetEffectObserver observer) {
      if (observersForCounteringUCWeakMutSet.ContainsKey(id)) {
        var list = observersForCounteringUCWeakMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForCounteringUCWeakMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastCounteringUCWeakMutSetEffects(
      SortedDictionary<int, List<ICounteringUCWeakMutSetEffectObserver>> observers) {
    foreach (var effect in effectsCounteringUCWeakMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<ICounteringUCWeakMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnCounteringUCWeakMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ICounteringUCWeakMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnCounteringUCWeakMutSetEffect(effect);
        }
        observersForCounteringUCWeakMutSet.Remove(effect.id);
      }
    }
    effectsCounteringUCWeakMutSetDeleteEffect.Clear();

    foreach (var effect in effectsCounteringUCWeakMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<ICounteringUCWeakMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnCounteringUCWeakMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ICounteringUCWeakMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnCounteringUCWeakMutSetEffect(effect);
        }
      }
    }
    effectsCounteringUCWeakMutSetAddEffect.Clear();

    foreach (var effect in effectsCounteringUCWeakMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<ICounteringUCWeakMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnCounteringUCWeakMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ICounteringUCWeakMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnCounteringUCWeakMutSetEffect(effect);
        }
      }
    }
    effectsCounteringUCWeakMutSetRemoveEffect.Clear();

    foreach (var effect in effectsCounteringUCWeakMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<ICounteringUCWeakMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnCounteringUCWeakMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ICounteringUCWeakMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnCounteringUCWeakMutSetEffect(effect);
        }
      }
    }
    effectsCounteringUCWeakMutSetCreateEffect.Clear();

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

    public int GetTimeCloneAICapabilityUCWeakMutSetHash(int id, int version, TimeCloneAICapabilityUCWeakMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public TimeCloneAICapabilityUCWeakMutSetIncarnation GetTimeCloneAICapabilityUCWeakMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsTimeCloneAICapabilityUCWeakMutSet[id].incarnation;
    }
    public TimeCloneAICapabilityUCWeakMutSet GetTimeCloneAICapabilityUCWeakMutSet(int id) {
      return new TimeCloneAICapabilityUCWeakMutSet(this, id);
    }
    public List<TimeCloneAICapabilityUCWeakMutSet> AllTimeCloneAICapabilityUCWeakMutSet() {
      List<TimeCloneAICapabilityUCWeakMutSet> result = new List<TimeCloneAICapabilityUCWeakMutSet>(rootIncarnation.incarnationsTimeCloneAICapabilityUCWeakMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsTimeCloneAICapabilityUCWeakMutSet.Keys) {
        result.Add(new TimeCloneAICapabilityUCWeakMutSet(this, id));
      }
      return result;
    }
    public bool TimeCloneAICapabilityUCWeakMutSetExists(int id) {
      return rootIncarnation.incarnationsTimeCloneAICapabilityUCWeakMutSet.ContainsKey(id);
    }
    public void CheckHasTimeCloneAICapabilityUCWeakMutSet(TimeCloneAICapabilityUCWeakMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasTimeCloneAICapabilityUCWeakMutSet(thing.id);
    }
    public void CheckHasTimeCloneAICapabilityUCWeakMutSet(int id) {
      if (!rootIncarnation.incarnationsTimeCloneAICapabilityUCWeakMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid TimeCloneAICapabilityUCWeakMutSet}: " + id);
      }
    }
    public TimeCloneAICapabilityUCWeakMutSet EffectTimeCloneAICapabilityUCWeakMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new TimeCloneAICapabilityUCWeakMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateTimeCloneAICapabilityUCWeakMutSet(id, rootIncarnation.version, incarnation);
      return new TimeCloneAICapabilityUCWeakMutSet(this, id);
    }
    public void EffectInternalCreateTimeCloneAICapabilityUCWeakMutSet(int id, int incarnationVersion, TimeCloneAICapabilityUCWeakMutSetIncarnation incarnation) {
      var effect = new TimeCloneAICapabilityUCWeakMutSetCreateEffect(id);
      rootIncarnation.incarnationsTimeCloneAICapabilityUCWeakMutSet
          .Add(
              id,
              new VersionAndIncarnation<TimeCloneAICapabilityUCWeakMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsTimeCloneAICapabilityUCWeakMutSetCreateEffect.Add(effect);
    }
    public void EffectTimeCloneAICapabilityUCWeakMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new TimeCloneAICapabilityUCWeakMutSetDeleteEffect(id);
      effectsTimeCloneAICapabilityUCWeakMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsTimeCloneAICapabilityUCWeakMutSet[id];
      rootIncarnation.incarnationsTimeCloneAICapabilityUCWeakMutSet.Remove(id);
    }

       
    public void EffectTimeCloneAICapabilityUCWeakMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasTimeCloneAICapabilityUCWeakMutSet(setId);
      CheckHasTimeCloneAICapabilityUC(elementId);

      var effect = new TimeCloneAICapabilityUCWeakMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsTimeCloneAICapabilityUCWeakMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new TimeCloneAICapabilityUCWeakMutSetIncarnation(newMap);
        rootIncarnation.incarnationsTimeCloneAICapabilityUCWeakMutSet[setId] =
            new VersionAndIncarnation<TimeCloneAICapabilityUCWeakMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsTimeCloneAICapabilityUCWeakMutSetAddEffect.Add(effect);
    }
    public void EffectTimeCloneAICapabilityUCWeakMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasTimeCloneAICapabilityUCWeakMutSet(setId);
      CheckHasTimeCloneAICapabilityUC(elementId);

      var effect = new TimeCloneAICapabilityUCWeakMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsTimeCloneAICapabilityUCWeakMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new TimeCloneAICapabilityUCWeakMutSetIncarnation(newMap);
        rootIncarnation.incarnationsTimeCloneAICapabilityUCWeakMutSet[setId] =
            new VersionAndIncarnation<TimeCloneAICapabilityUCWeakMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsTimeCloneAICapabilityUCWeakMutSetRemoveEffect.Add(effect);
    }

       
    public void AddTimeCloneAICapabilityUCWeakMutSetObserver(int id, ITimeCloneAICapabilityUCWeakMutSetEffectObserver observer) {
      List<ITimeCloneAICapabilityUCWeakMutSetEffectObserver> obsies;
      if (!observersForTimeCloneAICapabilityUCWeakMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ITimeCloneAICapabilityUCWeakMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForTimeCloneAICapabilityUCWeakMutSet[id] = obsies;
    }

    public void RemoveTimeCloneAICapabilityUCWeakMutSetObserver(int id, ITimeCloneAICapabilityUCWeakMutSetEffectObserver observer) {
      if (observersForTimeCloneAICapabilityUCWeakMutSet.ContainsKey(id)) {
        var list = observersForTimeCloneAICapabilityUCWeakMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForTimeCloneAICapabilityUCWeakMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastTimeCloneAICapabilityUCWeakMutSetEffects(
      SortedDictionary<int, List<ITimeCloneAICapabilityUCWeakMutSetEffectObserver>> observers) {
    foreach (var effect in effectsTimeCloneAICapabilityUCWeakMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<ITimeCloneAICapabilityUCWeakMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeCloneAICapabilityUCWeakMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeCloneAICapabilityUCWeakMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeCloneAICapabilityUCWeakMutSetEffect(effect);
        }
        observersForTimeCloneAICapabilityUCWeakMutSet.Remove(effect.id);
      }
    }
    effectsTimeCloneAICapabilityUCWeakMutSetDeleteEffect.Clear();

    foreach (var effect in effectsTimeCloneAICapabilityUCWeakMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<ITimeCloneAICapabilityUCWeakMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeCloneAICapabilityUCWeakMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeCloneAICapabilityUCWeakMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeCloneAICapabilityUCWeakMutSetEffect(effect);
        }
      }
    }
    effectsTimeCloneAICapabilityUCWeakMutSetAddEffect.Clear();

    foreach (var effect in effectsTimeCloneAICapabilityUCWeakMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<ITimeCloneAICapabilityUCWeakMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeCloneAICapabilityUCWeakMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeCloneAICapabilityUCWeakMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeCloneAICapabilityUCWeakMutSetEffect(effect);
        }
      }
    }
    effectsTimeCloneAICapabilityUCWeakMutSetRemoveEffect.Clear();

    foreach (var effect in effectsTimeCloneAICapabilityUCWeakMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<ITimeCloneAICapabilityUCWeakMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeCloneAICapabilityUCWeakMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeCloneAICapabilityUCWeakMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeCloneAICapabilityUCWeakMutSetEffect(effect);
        }
      }
    }
    effectsTimeCloneAICapabilityUCWeakMutSetCreateEffect.Clear();

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

    public int GetInertiaRingMutSetHash(int id, int version, InertiaRingMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public InertiaRingMutSetIncarnation GetInertiaRingMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsInertiaRingMutSet[id].incarnation;
    }
    public InertiaRingMutSet GetInertiaRingMutSet(int id) {
      return new InertiaRingMutSet(this, id);
    }
    public List<InertiaRingMutSet> AllInertiaRingMutSet() {
      List<InertiaRingMutSet> result = new List<InertiaRingMutSet>(rootIncarnation.incarnationsInertiaRingMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsInertiaRingMutSet.Keys) {
        result.Add(new InertiaRingMutSet(this, id));
      }
      return result;
    }
    public bool InertiaRingMutSetExists(int id) {
      return rootIncarnation.incarnationsInertiaRingMutSet.ContainsKey(id);
    }
    public void CheckHasInertiaRingMutSet(InertiaRingMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasInertiaRingMutSet(thing.id);
    }
    public void CheckHasInertiaRingMutSet(int id) {
      if (!rootIncarnation.incarnationsInertiaRingMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid InertiaRingMutSet}: " + id);
      }
    }
    public InertiaRingMutSet EffectInertiaRingMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new InertiaRingMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateInertiaRingMutSet(id, rootIncarnation.version, incarnation);
      return new InertiaRingMutSet(this, id);
    }
    public void EffectInternalCreateInertiaRingMutSet(int id, int incarnationVersion, InertiaRingMutSetIncarnation incarnation) {
      var effect = new InertiaRingMutSetCreateEffect(id);
      rootIncarnation.incarnationsInertiaRingMutSet
          .Add(
              id,
              new VersionAndIncarnation<InertiaRingMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsInertiaRingMutSetCreateEffect.Add(effect);
    }
    public void EffectInertiaRingMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new InertiaRingMutSetDeleteEffect(id);
      effectsInertiaRingMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsInertiaRingMutSet[id];
      rootIncarnation.incarnationsInertiaRingMutSet.Remove(id);
    }

       
    public void EffectInertiaRingMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasInertiaRingMutSet(setId);
      CheckHasInertiaRing(elementId);

      var effect = new InertiaRingMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsInertiaRingMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new InertiaRingMutSetIncarnation(newMap);
        rootIncarnation.incarnationsInertiaRingMutSet[setId] =
            new VersionAndIncarnation<InertiaRingMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsInertiaRingMutSetAddEffect.Add(effect);
    }
    public void EffectInertiaRingMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasInertiaRingMutSet(setId);
      CheckHasInertiaRing(elementId);

      var effect = new InertiaRingMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsInertiaRingMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new InertiaRingMutSetIncarnation(newMap);
        rootIncarnation.incarnationsInertiaRingMutSet[setId] =
            new VersionAndIncarnation<InertiaRingMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsInertiaRingMutSetRemoveEffect.Add(effect);
    }

       
    public void AddInertiaRingMutSetObserver(int id, IInertiaRingMutSetEffectObserver observer) {
      List<IInertiaRingMutSetEffectObserver> obsies;
      if (!observersForInertiaRingMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IInertiaRingMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForInertiaRingMutSet[id] = obsies;
    }

    public void RemoveInertiaRingMutSetObserver(int id, IInertiaRingMutSetEffectObserver observer) {
      if (observersForInertiaRingMutSet.ContainsKey(id)) {
        var list = observersForInertiaRingMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForInertiaRingMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastInertiaRingMutSetEffects(
      SortedDictionary<int, List<IInertiaRingMutSetEffectObserver>> observers) {
    foreach (var effect in effectsInertiaRingMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<IInertiaRingMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnInertiaRingMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IInertiaRingMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnInertiaRingMutSetEffect(effect);
        }
        observersForInertiaRingMutSet.Remove(effect.id);
      }
    }
    effectsInertiaRingMutSetDeleteEffect.Clear();

    foreach (var effect in effectsInertiaRingMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<IInertiaRingMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnInertiaRingMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IInertiaRingMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnInertiaRingMutSetEffect(effect);
        }
      }
    }
    effectsInertiaRingMutSetAddEffect.Clear();

    foreach (var effect in effectsInertiaRingMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<IInertiaRingMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnInertiaRingMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IInertiaRingMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnInertiaRingMutSetEffect(effect);
        }
      }
    }
    effectsInertiaRingMutSetRemoveEffect.Clear();

    foreach (var effect in effectsInertiaRingMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<IInertiaRingMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnInertiaRingMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IInertiaRingMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnInertiaRingMutSetEffect(effect);
        }
      }
    }
    effectsInertiaRingMutSetCreateEffect.Clear();

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

    public int GetManaPotionMutSetHash(int id, int version, ManaPotionMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public ManaPotionMutSetIncarnation GetManaPotionMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsManaPotionMutSet[id].incarnation;
    }
    public ManaPotionMutSet GetManaPotionMutSet(int id) {
      return new ManaPotionMutSet(this, id);
    }
    public List<ManaPotionMutSet> AllManaPotionMutSet() {
      List<ManaPotionMutSet> result = new List<ManaPotionMutSet>(rootIncarnation.incarnationsManaPotionMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsManaPotionMutSet.Keys) {
        result.Add(new ManaPotionMutSet(this, id));
      }
      return result;
    }
    public bool ManaPotionMutSetExists(int id) {
      return rootIncarnation.incarnationsManaPotionMutSet.ContainsKey(id);
    }
    public void CheckHasManaPotionMutSet(ManaPotionMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasManaPotionMutSet(thing.id);
    }
    public void CheckHasManaPotionMutSet(int id) {
      if (!rootIncarnation.incarnationsManaPotionMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid ManaPotionMutSet}: " + id);
      }
    }
    public ManaPotionMutSet EffectManaPotionMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new ManaPotionMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateManaPotionMutSet(id, rootIncarnation.version, incarnation);
      return new ManaPotionMutSet(this, id);
    }
    public void EffectInternalCreateManaPotionMutSet(int id, int incarnationVersion, ManaPotionMutSetIncarnation incarnation) {
      var effect = new ManaPotionMutSetCreateEffect(id);
      rootIncarnation.incarnationsManaPotionMutSet
          .Add(
              id,
              new VersionAndIncarnation<ManaPotionMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsManaPotionMutSetCreateEffect.Add(effect);
    }
    public void EffectManaPotionMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new ManaPotionMutSetDeleteEffect(id);
      effectsManaPotionMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsManaPotionMutSet[id];
      rootIncarnation.incarnationsManaPotionMutSet.Remove(id);
    }

       
    public void EffectManaPotionMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasManaPotionMutSet(setId);
      CheckHasManaPotion(elementId);

      var effect = new ManaPotionMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsManaPotionMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new ManaPotionMutSetIncarnation(newMap);
        rootIncarnation.incarnationsManaPotionMutSet[setId] =
            new VersionAndIncarnation<ManaPotionMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsManaPotionMutSetAddEffect.Add(effect);
    }
    public void EffectManaPotionMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasManaPotionMutSet(setId);
      CheckHasManaPotion(elementId);

      var effect = new ManaPotionMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsManaPotionMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new ManaPotionMutSetIncarnation(newMap);
        rootIncarnation.incarnationsManaPotionMutSet[setId] =
            new VersionAndIncarnation<ManaPotionMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsManaPotionMutSetRemoveEffect.Add(effect);
    }

       
    public void AddManaPotionMutSetObserver(int id, IManaPotionMutSetEffectObserver observer) {
      List<IManaPotionMutSetEffectObserver> obsies;
      if (!observersForManaPotionMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IManaPotionMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForManaPotionMutSet[id] = obsies;
    }

    public void RemoveManaPotionMutSetObserver(int id, IManaPotionMutSetEffectObserver observer) {
      if (observersForManaPotionMutSet.ContainsKey(id)) {
        var list = observersForManaPotionMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForManaPotionMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastManaPotionMutSetEffects(
      SortedDictionary<int, List<IManaPotionMutSetEffectObserver>> observers) {
    foreach (var effect in effectsManaPotionMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<IManaPotionMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnManaPotionMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IManaPotionMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnManaPotionMutSetEffect(effect);
        }
        observersForManaPotionMutSet.Remove(effect.id);
      }
    }
    effectsManaPotionMutSetDeleteEffect.Clear();

    foreach (var effect in effectsManaPotionMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<IManaPotionMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnManaPotionMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IManaPotionMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnManaPotionMutSetEffect(effect);
        }
      }
    }
    effectsManaPotionMutSetAddEffect.Clear();

    foreach (var effect in effectsManaPotionMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<IManaPotionMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnManaPotionMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IManaPotionMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnManaPotionMutSetEffect(effect);
        }
      }
    }
    effectsManaPotionMutSetRemoveEffect.Clear();

    foreach (var effect in effectsManaPotionMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<IManaPotionMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnManaPotionMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IManaPotionMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnManaPotionMutSetEffect(effect);
        }
      }
    }
    effectsManaPotionMutSetCreateEffect.Clear();

  }

    public int GetHealthPotionMutSetHash(int id, int version, HealthPotionMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public HealthPotionMutSetIncarnation GetHealthPotionMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsHealthPotionMutSet[id].incarnation;
    }
    public HealthPotionMutSet GetHealthPotionMutSet(int id) {
      return new HealthPotionMutSet(this, id);
    }
    public List<HealthPotionMutSet> AllHealthPotionMutSet() {
      List<HealthPotionMutSet> result = new List<HealthPotionMutSet>(rootIncarnation.incarnationsHealthPotionMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsHealthPotionMutSet.Keys) {
        result.Add(new HealthPotionMutSet(this, id));
      }
      return result;
    }
    public bool HealthPotionMutSetExists(int id) {
      return rootIncarnation.incarnationsHealthPotionMutSet.ContainsKey(id);
    }
    public void CheckHasHealthPotionMutSet(HealthPotionMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasHealthPotionMutSet(thing.id);
    }
    public void CheckHasHealthPotionMutSet(int id) {
      if (!rootIncarnation.incarnationsHealthPotionMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid HealthPotionMutSet}: " + id);
      }
    }
    public HealthPotionMutSet EffectHealthPotionMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new HealthPotionMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateHealthPotionMutSet(id, rootIncarnation.version, incarnation);
      return new HealthPotionMutSet(this, id);
    }
    public void EffectInternalCreateHealthPotionMutSet(int id, int incarnationVersion, HealthPotionMutSetIncarnation incarnation) {
      var effect = new HealthPotionMutSetCreateEffect(id);
      rootIncarnation.incarnationsHealthPotionMutSet
          .Add(
              id,
              new VersionAndIncarnation<HealthPotionMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsHealthPotionMutSetCreateEffect.Add(effect);
    }
    public void EffectHealthPotionMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new HealthPotionMutSetDeleteEffect(id);
      effectsHealthPotionMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsHealthPotionMutSet[id];
      rootIncarnation.incarnationsHealthPotionMutSet.Remove(id);
    }

       
    public void EffectHealthPotionMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasHealthPotionMutSet(setId);
      CheckHasHealthPotion(elementId);

      var effect = new HealthPotionMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsHealthPotionMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new HealthPotionMutSetIncarnation(newMap);
        rootIncarnation.incarnationsHealthPotionMutSet[setId] =
            new VersionAndIncarnation<HealthPotionMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsHealthPotionMutSetAddEffect.Add(effect);
    }
    public void EffectHealthPotionMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasHealthPotionMutSet(setId);
      CheckHasHealthPotion(elementId);

      var effect = new HealthPotionMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsHealthPotionMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new HealthPotionMutSetIncarnation(newMap);
        rootIncarnation.incarnationsHealthPotionMutSet[setId] =
            new VersionAndIncarnation<HealthPotionMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsHealthPotionMutSetRemoveEffect.Add(effect);
    }

       
    public void AddHealthPotionMutSetObserver(int id, IHealthPotionMutSetEffectObserver observer) {
      List<IHealthPotionMutSetEffectObserver> obsies;
      if (!observersForHealthPotionMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IHealthPotionMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForHealthPotionMutSet[id] = obsies;
    }

    public void RemoveHealthPotionMutSetObserver(int id, IHealthPotionMutSetEffectObserver observer) {
      if (observersForHealthPotionMutSet.ContainsKey(id)) {
        var list = observersForHealthPotionMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForHealthPotionMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastHealthPotionMutSetEffects(
      SortedDictionary<int, List<IHealthPotionMutSetEffectObserver>> observers) {
    foreach (var effect in effectsHealthPotionMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<IHealthPotionMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnHealthPotionMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IHealthPotionMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnHealthPotionMutSetEffect(effect);
        }
        observersForHealthPotionMutSet.Remove(effect.id);
      }
    }
    effectsHealthPotionMutSetDeleteEffect.Clear();

    foreach (var effect in effectsHealthPotionMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<IHealthPotionMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnHealthPotionMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IHealthPotionMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnHealthPotionMutSetEffect(effect);
        }
      }
    }
    effectsHealthPotionMutSetAddEffect.Clear();

    foreach (var effect in effectsHealthPotionMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<IHealthPotionMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnHealthPotionMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IHealthPotionMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnHealthPotionMutSetEffect(effect);
        }
      }
    }
    effectsHealthPotionMutSetRemoveEffect.Clear();

    foreach (var effect in effectsHealthPotionMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<IHealthPotionMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnHealthPotionMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IHealthPotionMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnHealthPotionMutSetEffect(effect);
        }
      }
    }
    effectsHealthPotionMutSetCreateEffect.Clear();

  }

    public int GetTimeScriptDirectiveUCMutSetHash(int id, int version, TimeScriptDirectiveUCMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public TimeScriptDirectiveUCMutSetIncarnation GetTimeScriptDirectiveUCMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsTimeScriptDirectiveUCMutSet[id].incarnation;
    }
    public TimeScriptDirectiveUCMutSet GetTimeScriptDirectiveUCMutSet(int id) {
      return new TimeScriptDirectiveUCMutSet(this, id);
    }
    public List<TimeScriptDirectiveUCMutSet> AllTimeScriptDirectiveUCMutSet() {
      List<TimeScriptDirectiveUCMutSet> result = new List<TimeScriptDirectiveUCMutSet>(rootIncarnation.incarnationsTimeScriptDirectiveUCMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsTimeScriptDirectiveUCMutSet.Keys) {
        result.Add(new TimeScriptDirectiveUCMutSet(this, id));
      }
      return result;
    }
    public bool TimeScriptDirectiveUCMutSetExists(int id) {
      return rootIncarnation.incarnationsTimeScriptDirectiveUCMutSet.ContainsKey(id);
    }
    public void CheckHasTimeScriptDirectiveUCMutSet(TimeScriptDirectiveUCMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasTimeScriptDirectiveUCMutSet(thing.id);
    }
    public void CheckHasTimeScriptDirectiveUCMutSet(int id) {
      if (!rootIncarnation.incarnationsTimeScriptDirectiveUCMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid TimeScriptDirectiveUCMutSet}: " + id);
      }
    }
    public TimeScriptDirectiveUCMutSet EffectTimeScriptDirectiveUCMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new TimeScriptDirectiveUCMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateTimeScriptDirectiveUCMutSet(id, rootIncarnation.version, incarnation);
      return new TimeScriptDirectiveUCMutSet(this, id);
    }
    public void EffectInternalCreateTimeScriptDirectiveUCMutSet(int id, int incarnationVersion, TimeScriptDirectiveUCMutSetIncarnation incarnation) {
      var effect = new TimeScriptDirectiveUCMutSetCreateEffect(id);
      rootIncarnation.incarnationsTimeScriptDirectiveUCMutSet
          .Add(
              id,
              new VersionAndIncarnation<TimeScriptDirectiveUCMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsTimeScriptDirectiveUCMutSetCreateEffect.Add(effect);
    }
    public void EffectTimeScriptDirectiveUCMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new TimeScriptDirectiveUCMutSetDeleteEffect(id);
      effectsTimeScriptDirectiveUCMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsTimeScriptDirectiveUCMutSet[id];
      rootIncarnation.incarnationsTimeScriptDirectiveUCMutSet.Remove(id);
    }

       
    public void EffectTimeScriptDirectiveUCMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasTimeScriptDirectiveUCMutSet(setId);
      CheckHasTimeScriptDirectiveUC(elementId);

      var effect = new TimeScriptDirectiveUCMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsTimeScriptDirectiveUCMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new TimeScriptDirectiveUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsTimeScriptDirectiveUCMutSet[setId] =
            new VersionAndIncarnation<TimeScriptDirectiveUCMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsTimeScriptDirectiveUCMutSetAddEffect.Add(effect);
    }
    public void EffectTimeScriptDirectiveUCMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasTimeScriptDirectiveUCMutSet(setId);
      CheckHasTimeScriptDirectiveUC(elementId);

      var effect = new TimeScriptDirectiveUCMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsTimeScriptDirectiveUCMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new TimeScriptDirectiveUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsTimeScriptDirectiveUCMutSet[setId] =
            new VersionAndIncarnation<TimeScriptDirectiveUCMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsTimeScriptDirectiveUCMutSetRemoveEffect.Add(effect);
    }

       
    public void AddTimeScriptDirectiveUCMutSetObserver(int id, ITimeScriptDirectiveUCMutSetEffectObserver observer) {
      List<ITimeScriptDirectiveUCMutSetEffectObserver> obsies;
      if (!observersForTimeScriptDirectiveUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ITimeScriptDirectiveUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForTimeScriptDirectiveUCMutSet[id] = obsies;
    }

    public void RemoveTimeScriptDirectiveUCMutSetObserver(int id, ITimeScriptDirectiveUCMutSetEffectObserver observer) {
      if (observersForTimeScriptDirectiveUCMutSet.ContainsKey(id)) {
        var list = observersForTimeScriptDirectiveUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForTimeScriptDirectiveUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastTimeScriptDirectiveUCMutSetEffects(
      SortedDictionary<int, List<ITimeScriptDirectiveUCMutSetEffectObserver>> observers) {
    foreach (var effect in effectsTimeScriptDirectiveUCMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<ITimeScriptDirectiveUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeScriptDirectiveUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeScriptDirectiveUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeScriptDirectiveUCMutSetEffect(effect);
        }
        observersForTimeScriptDirectiveUCMutSet.Remove(effect.id);
      }
    }
    effectsTimeScriptDirectiveUCMutSetDeleteEffect.Clear();

    foreach (var effect in effectsTimeScriptDirectiveUCMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<ITimeScriptDirectiveUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeScriptDirectiveUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeScriptDirectiveUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeScriptDirectiveUCMutSetEffect(effect);
        }
      }
    }
    effectsTimeScriptDirectiveUCMutSetAddEffect.Clear();

    foreach (var effect in effectsTimeScriptDirectiveUCMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<ITimeScriptDirectiveUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeScriptDirectiveUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeScriptDirectiveUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeScriptDirectiveUCMutSetEffect(effect);
        }
      }
    }
    effectsTimeScriptDirectiveUCMutSetRemoveEffect.Clear();

    foreach (var effect in effectsTimeScriptDirectiveUCMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<ITimeScriptDirectiveUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeScriptDirectiveUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeScriptDirectiveUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeScriptDirectiveUCMutSetEffect(effect);
        }
      }
    }
    effectsTimeScriptDirectiveUCMutSetCreateEffect.Clear();

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

    public int GetTimeCloneAICapabilityUCMutSetHash(int id, int version, TimeCloneAICapabilityUCMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public TimeCloneAICapabilityUCMutSetIncarnation GetTimeCloneAICapabilityUCMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsTimeCloneAICapabilityUCMutSet[id].incarnation;
    }
    public TimeCloneAICapabilityUCMutSet GetTimeCloneAICapabilityUCMutSet(int id) {
      return new TimeCloneAICapabilityUCMutSet(this, id);
    }
    public List<TimeCloneAICapabilityUCMutSet> AllTimeCloneAICapabilityUCMutSet() {
      List<TimeCloneAICapabilityUCMutSet> result = new List<TimeCloneAICapabilityUCMutSet>(rootIncarnation.incarnationsTimeCloneAICapabilityUCMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsTimeCloneAICapabilityUCMutSet.Keys) {
        result.Add(new TimeCloneAICapabilityUCMutSet(this, id));
      }
      return result;
    }
    public bool TimeCloneAICapabilityUCMutSetExists(int id) {
      return rootIncarnation.incarnationsTimeCloneAICapabilityUCMutSet.ContainsKey(id);
    }
    public void CheckHasTimeCloneAICapabilityUCMutSet(TimeCloneAICapabilityUCMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasTimeCloneAICapabilityUCMutSet(thing.id);
    }
    public void CheckHasTimeCloneAICapabilityUCMutSet(int id) {
      if (!rootIncarnation.incarnationsTimeCloneAICapabilityUCMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid TimeCloneAICapabilityUCMutSet}: " + id);
      }
    }
    public TimeCloneAICapabilityUCMutSet EffectTimeCloneAICapabilityUCMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new TimeCloneAICapabilityUCMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateTimeCloneAICapabilityUCMutSet(id, rootIncarnation.version, incarnation);
      return new TimeCloneAICapabilityUCMutSet(this, id);
    }
    public void EffectInternalCreateTimeCloneAICapabilityUCMutSet(int id, int incarnationVersion, TimeCloneAICapabilityUCMutSetIncarnation incarnation) {
      var effect = new TimeCloneAICapabilityUCMutSetCreateEffect(id);
      rootIncarnation.incarnationsTimeCloneAICapabilityUCMutSet
          .Add(
              id,
              new VersionAndIncarnation<TimeCloneAICapabilityUCMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsTimeCloneAICapabilityUCMutSetCreateEffect.Add(effect);
    }
    public void EffectTimeCloneAICapabilityUCMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new TimeCloneAICapabilityUCMutSetDeleteEffect(id);
      effectsTimeCloneAICapabilityUCMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsTimeCloneAICapabilityUCMutSet[id];
      rootIncarnation.incarnationsTimeCloneAICapabilityUCMutSet.Remove(id);
    }

       
    public void EffectTimeCloneAICapabilityUCMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasTimeCloneAICapabilityUCMutSet(setId);
      CheckHasTimeCloneAICapabilityUC(elementId);

      var effect = new TimeCloneAICapabilityUCMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsTimeCloneAICapabilityUCMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new TimeCloneAICapabilityUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsTimeCloneAICapabilityUCMutSet[setId] =
            new VersionAndIncarnation<TimeCloneAICapabilityUCMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsTimeCloneAICapabilityUCMutSetAddEffect.Add(effect);
    }
    public void EffectTimeCloneAICapabilityUCMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasTimeCloneAICapabilityUCMutSet(setId);
      CheckHasTimeCloneAICapabilityUC(elementId);

      var effect = new TimeCloneAICapabilityUCMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsTimeCloneAICapabilityUCMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new TimeCloneAICapabilityUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsTimeCloneAICapabilityUCMutSet[setId] =
            new VersionAndIncarnation<TimeCloneAICapabilityUCMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsTimeCloneAICapabilityUCMutSetRemoveEffect.Add(effect);
    }

       
    public void AddTimeCloneAICapabilityUCMutSetObserver(int id, ITimeCloneAICapabilityUCMutSetEffectObserver observer) {
      List<ITimeCloneAICapabilityUCMutSetEffectObserver> obsies;
      if (!observersForTimeCloneAICapabilityUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ITimeCloneAICapabilityUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForTimeCloneAICapabilityUCMutSet[id] = obsies;
    }

    public void RemoveTimeCloneAICapabilityUCMutSetObserver(int id, ITimeCloneAICapabilityUCMutSetEffectObserver observer) {
      if (observersForTimeCloneAICapabilityUCMutSet.ContainsKey(id)) {
        var list = observersForTimeCloneAICapabilityUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForTimeCloneAICapabilityUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastTimeCloneAICapabilityUCMutSetEffects(
      SortedDictionary<int, List<ITimeCloneAICapabilityUCMutSetEffectObserver>> observers) {
    foreach (var effect in effectsTimeCloneAICapabilityUCMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<ITimeCloneAICapabilityUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeCloneAICapabilityUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeCloneAICapabilityUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeCloneAICapabilityUCMutSetEffect(effect);
        }
        observersForTimeCloneAICapabilityUCMutSet.Remove(effect.id);
      }
    }
    effectsTimeCloneAICapabilityUCMutSetDeleteEffect.Clear();

    foreach (var effect in effectsTimeCloneAICapabilityUCMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<ITimeCloneAICapabilityUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeCloneAICapabilityUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeCloneAICapabilityUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeCloneAICapabilityUCMutSetEffect(effect);
        }
      }
    }
    effectsTimeCloneAICapabilityUCMutSetAddEffect.Clear();

    foreach (var effect in effectsTimeCloneAICapabilityUCMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<ITimeCloneAICapabilityUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeCloneAICapabilityUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeCloneAICapabilityUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeCloneAICapabilityUCMutSetEffect(effect);
        }
      }
    }
    effectsTimeCloneAICapabilityUCMutSetRemoveEffect.Clear();

    foreach (var effect in effectsTimeCloneAICapabilityUCMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<ITimeCloneAICapabilityUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeCloneAICapabilityUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeCloneAICapabilityUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeCloneAICapabilityUCMutSetEffect(effect);
        }
      }
    }
    effectsTimeCloneAICapabilityUCMutSetCreateEffect.Clear();

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

    public int GetCounteringUCMutSetHash(int id, int version, CounteringUCMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public CounteringUCMutSetIncarnation GetCounteringUCMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsCounteringUCMutSet[id].incarnation;
    }
    public CounteringUCMutSet GetCounteringUCMutSet(int id) {
      return new CounteringUCMutSet(this, id);
    }
    public List<CounteringUCMutSet> AllCounteringUCMutSet() {
      List<CounteringUCMutSet> result = new List<CounteringUCMutSet>(rootIncarnation.incarnationsCounteringUCMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsCounteringUCMutSet.Keys) {
        result.Add(new CounteringUCMutSet(this, id));
      }
      return result;
    }
    public bool CounteringUCMutSetExists(int id) {
      return rootIncarnation.incarnationsCounteringUCMutSet.ContainsKey(id);
    }
    public void CheckHasCounteringUCMutSet(CounteringUCMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasCounteringUCMutSet(thing.id);
    }
    public void CheckHasCounteringUCMutSet(int id) {
      if (!rootIncarnation.incarnationsCounteringUCMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid CounteringUCMutSet}: " + id);
      }
    }
    public CounteringUCMutSet EffectCounteringUCMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new CounteringUCMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateCounteringUCMutSet(id, rootIncarnation.version, incarnation);
      return new CounteringUCMutSet(this, id);
    }
    public void EffectInternalCreateCounteringUCMutSet(int id, int incarnationVersion, CounteringUCMutSetIncarnation incarnation) {
      var effect = new CounteringUCMutSetCreateEffect(id);
      rootIncarnation.incarnationsCounteringUCMutSet
          .Add(
              id,
              new VersionAndIncarnation<CounteringUCMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsCounteringUCMutSetCreateEffect.Add(effect);
    }
    public void EffectCounteringUCMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new CounteringUCMutSetDeleteEffect(id);
      effectsCounteringUCMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsCounteringUCMutSet[id];
      rootIncarnation.incarnationsCounteringUCMutSet.Remove(id);
    }

       
    public void EffectCounteringUCMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasCounteringUCMutSet(setId);
      CheckHasCounteringUC(elementId);

      var effect = new CounteringUCMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsCounteringUCMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new CounteringUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsCounteringUCMutSet[setId] =
            new VersionAndIncarnation<CounteringUCMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsCounteringUCMutSetAddEffect.Add(effect);
    }
    public void EffectCounteringUCMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasCounteringUCMutSet(setId);
      CheckHasCounteringUC(elementId);

      var effect = new CounteringUCMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsCounteringUCMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new CounteringUCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsCounteringUCMutSet[setId] =
            new VersionAndIncarnation<CounteringUCMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsCounteringUCMutSetRemoveEffect.Add(effect);
    }

       
    public void AddCounteringUCMutSetObserver(int id, ICounteringUCMutSetEffectObserver observer) {
      List<ICounteringUCMutSetEffectObserver> obsies;
      if (!observersForCounteringUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ICounteringUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForCounteringUCMutSet[id] = obsies;
    }

    public void RemoveCounteringUCMutSetObserver(int id, ICounteringUCMutSetEffectObserver observer) {
      if (observersForCounteringUCMutSet.ContainsKey(id)) {
        var list = observersForCounteringUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForCounteringUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastCounteringUCMutSetEffects(
      SortedDictionary<int, List<ICounteringUCMutSetEffectObserver>> observers) {
    foreach (var effect in effectsCounteringUCMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<ICounteringUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnCounteringUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ICounteringUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnCounteringUCMutSetEffect(effect);
        }
        observersForCounteringUCMutSet.Remove(effect.id);
      }
    }
    effectsCounteringUCMutSetDeleteEffect.Clear();

    foreach (var effect in effectsCounteringUCMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<ICounteringUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnCounteringUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ICounteringUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnCounteringUCMutSetEffect(effect);
        }
      }
    }
    effectsCounteringUCMutSetAddEffect.Clear();

    foreach (var effect in effectsCounteringUCMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<ICounteringUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnCounteringUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ICounteringUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnCounteringUCMutSetEffect(effect);
        }
      }
    }
    effectsCounteringUCMutSetRemoveEffect.Clear();

    foreach (var effect in effectsCounteringUCMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<ICounteringUCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnCounteringUCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ICounteringUCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnCounteringUCMutSetEffect(effect);
        }
      }
    }
    effectsCounteringUCMutSetCreateEffect.Clear();

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

    public int GetTimeAnchorTTCMutSetHash(int id, int version, TimeAnchorTTCMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public TimeAnchorTTCMutSetIncarnation GetTimeAnchorTTCMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsTimeAnchorTTCMutSet[id].incarnation;
    }
    public TimeAnchorTTCMutSet GetTimeAnchorTTCMutSet(int id) {
      return new TimeAnchorTTCMutSet(this, id);
    }
    public List<TimeAnchorTTCMutSet> AllTimeAnchorTTCMutSet() {
      List<TimeAnchorTTCMutSet> result = new List<TimeAnchorTTCMutSet>(rootIncarnation.incarnationsTimeAnchorTTCMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsTimeAnchorTTCMutSet.Keys) {
        result.Add(new TimeAnchorTTCMutSet(this, id));
      }
      return result;
    }
    public bool TimeAnchorTTCMutSetExists(int id) {
      return rootIncarnation.incarnationsTimeAnchorTTCMutSet.ContainsKey(id);
    }
    public void CheckHasTimeAnchorTTCMutSet(TimeAnchorTTCMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasTimeAnchorTTCMutSet(thing.id);
    }
    public void CheckHasTimeAnchorTTCMutSet(int id) {
      if (!rootIncarnation.incarnationsTimeAnchorTTCMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid TimeAnchorTTCMutSet}: " + id);
      }
    }
    public TimeAnchorTTCMutSet EffectTimeAnchorTTCMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new TimeAnchorTTCMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateTimeAnchorTTCMutSet(id, rootIncarnation.version, incarnation);
      return new TimeAnchorTTCMutSet(this, id);
    }
    public void EffectInternalCreateTimeAnchorTTCMutSet(int id, int incarnationVersion, TimeAnchorTTCMutSetIncarnation incarnation) {
      var effect = new TimeAnchorTTCMutSetCreateEffect(id);
      rootIncarnation.incarnationsTimeAnchorTTCMutSet
          .Add(
              id,
              new VersionAndIncarnation<TimeAnchorTTCMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsTimeAnchorTTCMutSetCreateEffect.Add(effect);
    }
    public void EffectTimeAnchorTTCMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new TimeAnchorTTCMutSetDeleteEffect(id);
      effectsTimeAnchorTTCMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsTimeAnchorTTCMutSet[id];
      rootIncarnation.incarnationsTimeAnchorTTCMutSet.Remove(id);
    }

       
    public void EffectTimeAnchorTTCMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasTimeAnchorTTCMutSet(setId);
      CheckHasTimeAnchorTTC(elementId);

      var effect = new TimeAnchorTTCMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsTimeAnchorTTCMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new TimeAnchorTTCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsTimeAnchorTTCMutSet[setId] =
            new VersionAndIncarnation<TimeAnchorTTCMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsTimeAnchorTTCMutSetAddEffect.Add(effect);
    }
    public void EffectTimeAnchorTTCMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasTimeAnchorTTCMutSet(setId);
      CheckHasTimeAnchorTTC(elementId);

      var effect = new TimeAnchorTTCMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsTimeAnchorTTCMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new TimeAnchorTTCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsTimeAnchorTTCMutSet[setId] =
            new VersionAndIncarnation<TimeAnchorTTCMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsTimeAnchorTTCMutSetRemoveEffect.Add(effect);
    }

       
    public void AddTimeAnchorTTCMutSetObserver(int id, ITimeAnchorTTCMutSetEffectObserver observer) {
      List<ITimeAnchorTTCMutSetEffectObserver> obsies;
      if (!observersForTimeAnchorTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ITimeAnchorTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForTimeAnchorTTCMutSet[id] = obsies;
    }

    public void RemoveTimeAnchorTTCMutSetObserver(int id, ITimeAnchorTTCMutSetEffectObserver observer) {
      if (observersForTimeAnchorTTCMutSet.ContainsKey(id)) {
        var list = observersForTimeAnchorTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForTimeAnchorTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastTimeAnchorTTCMutSetEffects(
      SortedDictionary<int, List<ITimeAnchorTTCMutSetEffectObserver>> observers) {
    foreach (var effect in effectsTimeAnchorTTCMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<ITimeAnchorTTCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeAnchorTTCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeAnchorTTCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeAnchorTTCMutSetEffect(effect);
        }
        observersForTimeAnchorTTCMutSet.Remove(effect.id);
      }
    }
    effectsTimeAnchorTTCMutSetDeleteEffect.Clear();

    foreach (var effect in effectsTimeAnchorTTCMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<ITimeAnchorTTCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeAnchorTTCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeAnchorTTCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeAnchorTTCMutSetEffect(effect);
        }
      }
    }
    effectsTimeAnchorTTCMutSetAddEffect.Clear();

    foreach (var effect in effectsTimeAnchorTTCMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<ITimeAnchorTTCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeAnchorTTCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeAnchorTTCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeAnchorTTCMutSetEffect(effect);
        }
      }
    }
    effectsTimeAnchorTTCMutSetRemoveEffect.Clear();

    foreach (var effect in effectsTimeAnchorTTCMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<ITimeAnchorTTCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTimeAnchorTTCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITimeAnchorTTCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTimeAnchorTTCMutSetEffect(effect);
        }
      }
    }
    effectsTimeAnchorTTCMutSetCreateEffect.Clear();

  }

    public int GetStaircaseTTCMutSetHash(int id, int version, StaircaseTTCMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public StaircaseTTCMutSetIncarnation GetStaircaseTTCMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsStaircaseTTCMutSet[id].incarnation;
    }
    public StaircaseTTCMutSet GetStaircaseTTCMutSet(int id) {
      return new StaircaseTTCMutSet(this, id);
    }
    public List<StaircaseTTCMutSet> AllStaircaseTTCMutSet() {
      List<StaircaseTTCMutSet> result = new List<StaircaseTTCMutSet>(rootIncarnation.incarnationsStaircaseTTCMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsStaircaseTTCMutSet.Keys) {
        result.Add(new StaircaseTTCMutSet(this, id));
      }
      return result;
    }
    public bool StaircaseTTCMutSetExists(int id) {
      return rootIncarnation.incarnationsStaircaseTTCMutSet.ContainsKey(id);
    }
    public void CheckHasStaircaseTTCMutSet(StaircaseTTCMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasStaircaseTTCMutSet(thing.id);
    }
    public void CheckHasStaircaseTTCMutSet(int id) {
      if (!rootIncarnation.incarnationsStaircaseTTCMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid StaircaseTTCMutSet}: " + id);
      }
    }
    public StaircaseTTCMutSet EffectStaircaseTTCMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new StaircaseTTCMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateStaircaseTTCMutSet(id, rootIncarnation.version, incarnation);
      return new StaircaseTTCMutSet(this, id);
    }
    public void EffectInternalCreateStaircaseTTCMutSet(int id, int incarnationVersion, StaircaseTTCMutSetIncarnation incarnation) {
      var effect = new StaircaseTTCMutSetCreateEffect(id);
      rootIncarnation.incarnationsStaircaseTTCMutSet
          .Add(
              id,
              new VersionAndIncarnation<StaircaseTTCMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsStaircaseTTCMutSetCreateEffect.Add(effect);
    }
    public void EffectStaircaseTTCMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new StaircaseTTCMutSetDeleteEffect(id);
      effectsStaircaseTTCMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsStaircaseTTCMutSet[id];
      rootIncarnation.incarnationsStaircaseTTCMutSet.Remove(id);
    }

       
    public void EffectStaircaseTTCMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasStaircaseTTCMutSet(setId);
      CheckHasStaircaseTTC(elementId);

      var effect = new StaircaseTTCMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsStaircaseTTCMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new StaircaseTTCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsStaircaseTTCMutSet[setId] =
            new VersionAndIncarnation<StaircaseTTCMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsStaircaseTTCMutSetAddEffect.Add(effect);
    }
    public void EffectStaircaseTTCMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasStaircaseTTCMutSet(setId);
      CheckHasStaircaseTTC(elementId);

      var effect = new StaircaseTTCMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsStaircaseTTCMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new StaircaseTTCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsStaircaseTTCMutSet[setId] =
            new VersionAndIncarnation<StaircaseTTCMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsStaircaseTTCMutSetRemoveEffect.Add(effect);
    }

       
    public void AddStaircaseTTCMutSetObserver(int id, IStaircaseTTCMutSetEffectObserver observer) {
      List<IStaircaseTTCMutSetEffectObserver> obsies;
      if (!observersForStaircaseTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IStaircaseTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForStaircaseTTCMutSet[id] = obsies;
    }

    public void RemoveStaircaseTTCMutSetObserver(int id, IStaircaseTTCMutSetEffectObserver observer) {
      if (observersForStaircaseTTCMutSet.ContainsKey(id)) {
        var list = observersForStaircaseTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForStaircaseTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastStaircaseTTCMutSetEffects(
      SortedDictionary<int, List<IStaircaseTTCMutSetEffectObserver>> observers) {
    foreach (var effect in effectsStaircaseTTCMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<IStaircaseTTCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnStaircaseTTCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IStaircaseTTCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnStaircaseTTCMutSetEffect(effect);
        }
        observersForStaircaseTTCMutSet.Remove(effect.id);
      }
    }
    effectsStaircaseTTCMutSetDeleteEffect.Clear();

    foreach (var effect in effectsStaircaseTTCMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<IStaircaseTTCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnStaircaseTTCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IStaircaseTTCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnStaircaseTTCMutSetEffect(effect);
        }
      }
    }
    effectsStaircaseTTCMutSetAddEffect.Clear();

    foreach (var effect in effectsStaircaseTTCMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<IStaircaseTTCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnStaircaseTTCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IStaircaseTTCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnStaircaseTTCMutSetEffect(effect);
        }
      }
    }
    effectsStaircaseTTCMutSetRemoveEffect.Clear();

    foreach (var effect in effectsStaircaseTTCMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<IStaircaseTTCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnStaircaseTTCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IStaircaseTTCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnStaircaseTTCMutSetEffect(effect);
        }
      }
    }
    effectsStaircaseTTCMutSetCreateEffect.Clear();

  }

    public int GetDecorativeTTCMutSetHash(int id, int version, DecorativeTTCMutSetIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.set) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public DecorativeTTCMutSetIncarnation GetDecorativeTTCMutSetIncarnation(int id) {
      return rootIncarnation.incarnationsDecorativeTTCMutSet[id].incarnation;
    }
    public DecorativeTTCMutSet GetDecorativeTTCMutSet(int id) {
      return new DecorativeTTCMutSet(this, id);
    }
    public List<DecorativeTTCMutSet> AllDecorativeTTCMutSet() {
      List<DecorativeTTCMutSet> result = new List<DecorativeTTCMutSet>(rootIncarnation.incarnationsDecorativeTTCMutSet.Count);
      foreach (var id in rootIncarnation.incarnationsDecorativeTTCMutSet.Keys) {
        result.Add(new DecorativeTTCMutSet(this, id));
      }
      return result;
    }
    public bool DecorativeTTCMutSetExists(int id) {
      return rootIncarnation.incarnationsDecorativeTTCMutSet.ContainsKey(id);
    }
    public void CheckHasDecorativeTTCMutSet(DecorativeTTCMutSet thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasDecorativeTTCMutSet(thing.id);
    }
    public void CheckHasDecorativeTTCMutSet(int id) {
      if (!rootIncarnation.incarnationsDecorativeTTCMutSet.ContainsKey(id)) {
        throw new System.Exception("Invalid DecorativeTTCMutSet}: " + id);
      }
    }
    public DecorativeTTCMutSet EffectDecorativeTTCMutSetCreate() {
      CheckUnlocked();
      var id = NewId();
      var incarnation = new DecorativeTTCMutSetIncarnation(new SortedSet<int>());
      EffectInternalCreateDecorativeTTCMutSet(id, rootIncarnation.version, incarnation);
      return new DecorativeTTCMutSet(this, id);
    }
    public void EffectInternalCreateDecorativeTTCMutSet(int id, int incarnationVersion, DecorativeTTCMutSetIncarnation incarnation) {
      var effect = new DecorativeTTCMutSetCreateEffect(id);
      rootIncarnation.incarnationsDecorativeTTCMutSet
          .Add(
              id,
              new VersionAndIncarnation<DecorativeTTCMutSetIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsDecorativeTTCMutSetCreateEffect.Add(effect);
    }
    public void EffectDecorativeTTCMutSetDelete(int id) {
      CheckUnlocked();
      var effect = new DecorativeTTCMutSetDeleteEffect(id);
      effectsDecorativeTTCMutSetDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsDecorativeTTCMutSet[id];
      rootIncarnation.incarnationsDecorativeTTCMutSet.Remove(id);
    }

       
    public void EffectDecorativeTTCMutSetAdd(int setId, int elementId) {
      CheckUnlocked();
      CheckHasDecorativeTTCMutSet(setId);
      CheckHasDecorativeTTC(elementId);

      var effect = new DecorativeTTCMutSetAddEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsDecorativeTTCMutSet[setId];
      Asserts.Assert(!oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Add(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Add(elementId);
        var newIncarnation = new DecorativeTTCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsDecorativeTTCMutSet[setId] =
            new VersionAndIncarnation<DecorativeTTCMutSetIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsDecorativeTTCMutSetAddEffect.Add(effect);
    }
    public void EffectDecorativeTTCMutSetRemove(int setId, int elementId) {
      CheckUnlocked();
      CheckHasDecorativeTTCMutSet(setId);
      CheckHasDecorativeTTC(elementId);

      var effect = new DecorativeTTCMutSetRemoveEffect(setId, elementId);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsDecorativeTTCMutSet[setId];
      Asserts.Assert(oldIncarnationAndVersion.incarnation.set.Contains(elementId));
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.set;
        var newMap = new SortedSet<int>(oldMap);
        newMap.Remove(elementId);
        var newIncarnation = new DecorativeTTCMutSetIncarnation(newMap);
        rootIncarnation.incarnationsDecorativeTTCMutSet[setId] =
            new VersionAndIncarnation<DecorativeTTCMutSetIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsDecorativeTTCMutSetRemoveEffect.Add(effect);
    }

       
    public void AddDecorativeTTCMutSetObserver(int id, IDecorativeTTCMutSetEffectObserver observer) {
      List<IDecorativeTTCMutSetEffectObserver> obsies;
      if (!observersForDecorativeTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IDecorativeTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForDecorativeTTCMutSet[id] = obsies;
    }

    public void RemoveDecorativeTTCMutSetObserver(int id, IDecorativeTTCMutSetEffectObserver observer) {
      if (observersForDecorativeTTCMutSet.ContainsKey(id)) {
        var list = observersForDecorativeTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForDecorativeTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }
       
  public void BroadcastDecorativeTTCMutSetEffects(
      SortedDictionary<int, List<IDecorativeTTCMutSetEffectObserver>> observers) {
    foreach (var effect in effectsDecorativeTTCMutSetDeleteEffect) {
      if (observers.TryGetValue(0, out List<IDecorativeTTCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnDecorativeTTCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IDecorativeTTCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnDecorativeTTCMutSetEffect(effect);
        }
        observersForDecorativeTTCMutSet.Remove(effect.id);
      }
    }
    effectsDecorativeTTCMutSetDeleteEffect.Clear();

    foreach (var effect in effectsDecorativeTTCMutSetAddEffect) {
      if (observers.TryGetValue(0, out List<IDecorativeTTCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnDecorativeTTCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IDecorativeTTCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnDecorativeTTCMutSetEffect(effect);
        }
      }
    }
    effectsDecorativeTTCMutSetAddEffect.Clear();

    foreach (var effect in effectsDecorativeTTCMutSetRemoveEffect) {
      if (observers.TryGetValue(0, out List<IDecorativeTTCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnDecorativeTTCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IDecorativeTTCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnDecorativeTTCMutSetEffect(effect);
        }
      }
    }
    effectsDecorativeTTCMutSetRemoveEffect.Clear();

    foreach (var effect in effectsDecorativeTTCMutSetCreateEffect) {
      if (observers.TryGetValue(0, out List<IDecorativeTTCMutSetEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnDecorativeTTCMutSetEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IDecorativeTTCMutSetEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnDecorativeTTCMutSetEffect(effect);
        }
      }
    }
    effectsDecorativeTTCMutSetCreateEffect.Clear();

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
