using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class RootIncarnation {
  public readonly int version;
  public int nextId;
  public int hash;
  public readonly SortedDictionary<int, VersionAndIncarnation<SquareCaveLevelControllerIncarnation>> incarnationsSquareCaveLevelController;
  public readonly SortedDictionary<int, VersionAndIncarnation<RidgeLevelControllerIncarnation>> incarnationsRidgeLevelController;
  public readonly SortedDictionary<int, VersionAndIncarnation<RavashrikeLevelControllerIncarnation>> incarnationsRavashrikeLevelController;
  public readonly SortedDictionary<int, VersionAndIncarnation<PentagonalCaveLevelControllerIncarnation>> incarnationsPentagonalCaveLevelController;
  public readonly SortedDictionary<int, VersionAndIncarnation<CliffLevelControllerIncarnation>> incarnationsCliffLevelController;
  public readonly SortedDictionary<int, VersionAndIncarnation<LevelIncarnation>> incarnationsLevel;
  public readonly SortedDictionary<int, VersionAndIncarnation<TimeAnchorTTCIncarnation>> incarnationsTimeAnchorTTC;
  public readonly SortedDictionary<int, VersionAndIncarnation<TerrainTileIncarnation>> incarnationsTerrainTile;
  public readonly SortedDictionary<int, VersionAndIncarnation<ITerrainTileComponentMutBunchIncarnation>> incarnationsITerrainTileComponentMutBunch;
  public readonly SortedDictionary<int, VersionAndIncarnation<TerrainIncarnation>> incarnationsTerrain;
  public readonly SortedDictionary<int, VersionAndIncarnation<StaircaseTTCIncarnation>> incarnationsStaircaseTTC;
  public readonly SortedDictionary<int, VersionAndIncarnation<DecorativeTTCIncarnation>> incarnationsDecorativeTTC;
  public readonly SortedDictionary<int, VersionAndIncarnation<ManaPotionIncarnation>> incarnationsManaPotion;
  public readonly SortedDictionary<int, VersionAndIncarnation<HealthPotionIncarnation>> incarnationsHealthPotion;
  public readonly SortedDictionary<int, VersionAndIncarnation<GlaiveIncarnation>> incarnationsGlaive;
  public readonly SortedDictionary<int, VersionAndIncarnation<ArmorIncarnation>> incarnationsArmor;
  public readonly SortedDictionary<int, VersionAndIncarnation<RandIncarnation>> incarnationsRand;
  public readonly SortedDictionary<int, VersionAndIncarnation<WanderAICapabilityUCIncarnation>> incarnationsWanderAICapabilityUC;
  public readonly SortedDictionary<int, VersionAndIncarnation<CounteringUCIncarnation>> incarnationsCounteringUC;
  public readonly SortedDictionary<int, VersionAndIncarnation<ShieldingUCIncarnation>> incarnationsShieldingUC;
  public readonly SortedDictionary<int, VersionAndIncarnation<EvaporateImpulseIncarnation>> incarnationsEvaporateImpulse;
  public readonly SortedDictionary<int, VersionAndIncarnation<TimeScriptDirectiveUCIncarnation>> incarnationsTimeScriptDirectiveUC;
  public readonly SortedDictionary<int, VersionAndIncarnation<TimeCloneAICapabilityUCIncarnation>> incarnationsTimeCloneAICapabilityUC;
  public readonly SortedDictionary<int, VersionAndIncarnation<BidingOperationUCIncarnation>> incarnationsBidingOperationUC;
  public readonly SortedDictionary<int, VersionAndIncarnation<UnleashBideImpulseIncarnation>> incarnationsUnleashBideImpulse;
  public readonly SortedDictionary<int, VersionAndIncarnation<ContinueBidingImpulseIncarnation>> incarnationsContinueBidingImpulse;
  public readonly SortedDictionary<int, VersionAndIncarnation<StartBidingImpulseIncarnation>> incarnationsStartBidingImpulse;
  public readonly SortedDictionary<int, VersionAndIncarnation<BideAICapabilityUCIncarnation>> incarnationsBideAICapabilityUC;
  public readonly SortedDictionary<int, VersionAndIncarnation<FireImpulseIncarnation>> incarnationsFireImpulse;
  public readonly SortedDictionary<int, VersionAndIncarnation<CounterImpulseIncarnation>> incarnationsCounterImpulse;
  public readonly SortedDictionary<int, VersionAndIncarnation<DefendImpulseIncarnation>> incarnationsDefendImpulse;
  public readonly SortedDictionary<int, VersionAndIncarnation<AttackImpulseIncarnation>> incarnationsAttackImpulse;
  public readonly SortedDictionary<int, VersionAndIncarnation<PursueImpulseIncarnation>> incarnationsPursueImpulse;
  public readonly SortedDictionary<int, VersionAndIncarnation<KillDirectiveUCIncarnation>> incarnationsKillDirectiveUC;
  public readonly SortedDictionary<int, VersionAndIncarnation<AttackAICapabilityUCIncarnation>> incarnationsAttackAICapabilityUC;
  public readonly SortedDictionary<int, VersionAndIncarnation<MoveImpulseIncarnation>> incarnationsMoveImpulse;
  public readonly SortedDictionary<int, VersionAndIncarnation<MoveDirectiveUCIncarnation>> incarnationsMoveDirectiveUC;
  public readonly SortedDictionary<int, VersionAndIncarnation<UnitIncarnation>> incarnationsUnit;
  public readonly SortedDictionary<int, VersionAndIncarnation<IUnitComponentMutBunchIncarnation>> incarnationsIUnitComponentMutBunch;
  public readonly SortedDictionary<int, VersionAndIncarnation<NoImpulseIncarnation>> incarnationsNoImpulse;
  public readonly SortedDictionary<int, VersionAndIncarnation<ExecutionStateIncarnation>> incarnationsExecutionState;
  public readonly SortedDictionary<int, VersionAndIncarnation<IPostActingUCWeakMutBunchIncarnation>> incarnationsIPostActingUCWeakMutBunch;
  public readonly SortedDictionary<int, VersionAndIncarnation<IPreActingUCWeakMutBunchIncarnation>> incarnationsIPreActingUCWeakMutBunch;
  public readonly SortedDictionary<int, VersionAndIncarnation<GameIncarnation>> incarnationsGame;
  public readonly SortedDictionary<int, VersionAndIncarnation<IUnitEventMutListIncarnation>> incarnationsIUnitEventMutList;
  public readonly SortedDictionary<int, VersionAndIncarnation<LocationMutListIncarnation>> incarnationsLocationMutList;
  public readonly SortedDictionary<int, VersionAndIncarnation<IRequestMutListIncarnation>> incarnationsIRequestMutList;
  public readonly SortedDictionary<int, VersionAndIncarnation<LevelMutSetIncarnation>> incarnationsLevelMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<CounteringUCWeakMutSetIncarnation>> incarnationsCounteringUCWeakMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<ShieldingUCWeakMutSetIncarnation>> incarnationsShieldingUCWeakMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<AttackAICapabilityUCWeakMutSetIncarnation>> incarnationsAttackAICapabilityUCWeakMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<TimeCloneAICapabilityUCWeakMutSetIncarnation>> incarnationsTimeCloneAICapabilityUCWeakMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<ArmorMutSetIncarnation>> incarnationsArmorMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<GlaiveMutSetIncarnation>> incarnationsGlaiveMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<ManaPotionMutSetIncarnation>> incarnationsManaPotionMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<HealthPotionMutSetIncarnation>> incarnationsHealthPotionMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<TimeScriptDirectiveUCMutSetIncarnation>> incarnationsTimeScriptDirectiveUCMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<KillDirectiveUCMutSetIncarnation>> incarnationsKillDirectiveUCMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<MoveDirectiveUCMutSetIncarnation>> incarnationsMoveDirectiveUCMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<WanderAICapabilityUCMutSetIncarnation>> incarnationsWanderAICapabilityUCMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<BideAICapabilityUCMutSetIncarnation>> incarnationsBideAICapabilityUCMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<TimeCloneAICapabilityUCMutSetIncarnation>> incarnationsTimeCloneAICapabilityUCMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<AttackAICapabilityUCMutSetIncarnation>> incarnationsAttackAICapabilityUCMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<CounteringUCMutSetIncarnation>> incarnationsCounteringUCMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<ShieldingUCMutSetIncarnation>> incarnationsShieldingUCMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<BidingOperationUCMutSetIncarnation>> incarnationsBidingOperationUCMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<TimeAnchorTTCMutSetIncarnation>> incarnationsTimeAnchorTTCMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<StaircaseTTCMutSetIncarnation>> incarnationsStaircaseTTCMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<DecorativeTTCMutSetIncarnation>> incarnationsDecorativeTTCMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<UnitMutSetIncarnation>> incarnationsUnitMutSet;
  public readonly SortedDictionary<int, VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>> incarnationsTerrainTileByLocationMutMap;
  public RootIncarnation(int version, int nextId, int hash) {
    this.version = version;
    this.nextId = nextId;
    this.hash = hash;
    this.incarnationsSquareCaveLevelController = new SortedDictionary<int, VersionAndIncarnation<SquareCaveLevelControllerIncarnation>>();
    this.incarnationsRidgeLevelController = new SortedDictionary<int, VersionAndIncarnation<RidgeLevelControllerIncarnation>>();
    this.incarnationsRavashrikeLevelController = new SortedDictionary<int, VersionAndIncarnation<RavashrikeLevelControllerIncarnation>>();
    this.incarnationsPentagonalCaveLevelController = new SortedDictionary<int, VersionAndIncarnation<PentagonalCaveLevelControllerIncarnation>>();
    this.incarnationsCliffLevelController = new SortedDictionary<int, VersionAndIncarnation<CliffLevelControllerIncarnation>>();
    this.incarnationsLevel = new SortedDictionary<int, VersionAndIncarnation<LevelIncarnation>>();
    this.incarnationsTimeAnchorTTC = new SortedDictionary<int, VersionAndIncarnation<TimeAnchorTTCIncarnation>>();
    this.incarnationsTerrainTile = new SortedDictionary<int, VersionAndIncarnation<TerrainTileIncarnation>>();
    this.incarnationsITerrainTileComponentMutBunch = new SortedDictionary<int, VersionAndIncarnation<ITerrainTileComponentMutBunchIncarnation>>();
    this.incarnationsTerrain = new SortedDictionary<int, VersionAndIncarnation<TerrainIncarnation>>();
    this.incarnationsStaircaseTTC = new SortedDictionary<int, VersionAndIncarnation<StaircaseTTCIncarnation>>();
    this.incarnationsDecorativeTTC = new SortedDictionary<int, VersionAndIncarnation<DecorativeTTCIncarnation>>();
    this.incarnationsManaPotion = new SortedDictionary<int, VersionAndIncarnation<ManaPotionIncarnation>>();
    this.incarnationsHealthPotion = new SortedDictionary<int, VersionAndIncarnation<HealthPotionIncarnation>>();
    this.incarnationsGlaive = new SortedDictionary<int, VersionAndIncarnation<GlaiveIncarnation>>();
    this.incarnationsArmor = new SortedDictionary<int, VersionAndIncarnation<ArmorIncarnation>>();
    this.incarnationsRand = new SortedDictionary<int, VersionAndIncarnation<RandIncarnation>>();
    this.incarnationsWanderAICapabilityUC = new SortedDictionary<int, VersionAndIncarnation<WanderAICapabilityUCIncarnation>>();
    this.incarnationsCounteringUC = new SortedDictionary<int, VersionAndIncarnation<CounteringUCIncarnation>>();
    this.incarnationsShieldingUC = new SortedDictionary<int, VersionAndIncarnation<ShieldingUCIncarnation>>();
    this.incarnationsEvaporateImpulse = new SortedDictionary<int, VersionAndIncarnation<EvaporateImpulseIncarnation>>();
    this.incarnationsTimeScriptDirectiveUC = new SortedDictionary<int, VersionAndIncarnation<TimeScriptDirectiveUCIncarnation>>();
    this.incarnationsTimeCloneAICapabilityUC = new SortedDictionary<int, VersionAndIncarnation<TimeCloneAICapabilityUCIncarnation>>();
    this.incarnationsBidingOperationUC = new SortedDictionary<int, VersionAndIncarnation<BidingOperationUCIncarnation>>();
    this.incarnationsUnleashBideImpulse = new SortedDictionary<int, VersionAndIncarnation<UnleashBideImpulseIncarnation>>();
    this.incarnationsContinueBidingImpulse = new SortedDictionary<int, VersionAndIncarnation<ContinueBidingImpulseIncarnation>>();
    this.incarnationsStartBidingImpulse = new SortedDictionary<int, VersionAndIncarnation<StartBidingImpulseIncarnation>>();
    this.incarnationsBideAICapabilityUC = new SortedDictionary<int, VersionAndIncarnation<BideAICapabilityUCIncarnation>>();
    this.incarnationsFireImpulse = new SortedDictionary<int, VersionAndIncarnation<FireImpulseIncarnation>>();
    this.incarnationsCounterImpulse = new SortedDictionary<int, VersionAndIncarnation<CounterImpulseIncarnation>>();
    this.incarnationsDefendImpulse = new SortedDictionary<int, VersionAndIncarnation<DefendImpulseIncarnation>>();
    this.incarnationsAttackImpulse = new SortedDictionary<int, VersionAndIncarnation<AttackImpulseIncarnation>>();
    this.incarnationsPursueImpulse = new SortedDictionary<int, VersionAndIncarnation<PursueImpulseIncarnation>>();
    this.incarnationsKillDirectiveUC = new SortedDictionary<int, VersionAndIncarnation<KillDirectiveUCIncarnation>>();
    this.incarnationsAttackAICapabilityUC = new SortedDictionary<int, VersionAndIncarnation<AttackAICapabilityUCIncarnation>>();
    this.incarnationsMoveImpulse = new SortedDictionary<int, VersionAndIncarnation<MoveImpulseIncarnation>>();
    this.incarnationsMoveDirectiveUC = new SortedDictionary<int, VersionAndIncarnation<MoveDirectiveUCIncarnation>>();
    this.incarnationsUnit = new SortedDictionary<int, VersionAndIncarnation<UnitIncarnation>>();
    this.incarnationsIUnitComponentMutBunch = new SortedDictionary<int, VersionAndIncarnation<IUnitComponentMutBunchIncarnation>>();
    this.incarnationsNoImpulse = new SortedDictionary<int, VersionAndIncarnation<NoImpulseIncarnation>>();
    this.incarnationsExecutionState = new SortedDictionary<int, VersionAndIncarnation<ExecutionStateIncarnation>>();
    this.incarnationsIPostActingUCWeakMutBunch = new SortedDictionary<int, VersionAndIncarnation<IPostActingUCWeakMutBunchIncarnation>>();
    this.incarnationsIPreActingUCWeakMutBunch = new SortedDictionary<int, VersionAndIncarnation<IPreActingUCWeakMutBunchIncarnation>>();
    this.incarnationsGame = new SortedDictionary<int, VersionAndIncarnation<GameIncarnation>>();
    this.incarnationsIUnitEventMutList = new SortedDictionary<int, VersionAndIncarnation<IUnitEventMutListIncarnation>>();
    this.incarnationsLocationMutList = new SortedDictionary<int, VersionAndIncarnation<LocationMutListIncarnation>>();
    this.incarnationsIRequestMutList = new SortedDictionary<int, VersionAndIncarnation<IRequestMutListIncarnation>>();
    this.incarnationsLevelMutSet = new SortedDictionary<int, VersionAndIncarnation<LevelMutSetIncarnation>>();
    this.incarnationsCounteringUCWeakMutSet = new SortedDictionary<int, VersionAndIncarnation<CounteringUCWeakMutSetIncarnation>>();
    this.incarnationsShieldingUCWeakMutSet = new SortedDictionary<int, VersionAndIncarnation<ShieldingUCWeakMutSetIncarnation>>();
    this.incarnationsAttackAICapabilityUCWeakMutSet = new SortedDictionary<int, VersionAndIncarnation<AttackAICapabilityUCWeakMutSetIncarnation>>();
    this.incarnationsTimeCloneAICapabilityUCWeakMutSet = new SortedDictionary<int, VersionAndIncarnation<TimeCloneAICapabilityUCWeakMutSetIncarnation>>();
    this.incarnationsArmorMutSet = new SortedDictionary<int, VersionAndIncarnation<ArmorMutSetIncarnation>>();
    this.incarnationsGlaiveMutSet = new SortedDictionary<int, VersionAndIncarnation<GlaiveMutSetIncarnation>>();
    this.incarnationsManaPotionMutSet = new SortedDictionary<int, VersionAndIncarnation<ManaPotionMutSetIncarnation>>();
    this.incarnationsHealthPotionMutSet = new SortedDictionary<int, VersionAndIncarnation<HealthPotionMutSetIncarnation>>();
    this.incarnationsTimeScriptDirectiveUCMutSet = new SortedDictionary<int, VersionAndIncarnation<TimeScriptDirectiveUCMutSetIncarnation>>();
    this.incarnationsKillDirectiveUCMutSet = new SortedDictionary<int, VersionAndIncarnation<KillDirectiveUCMutSetIncarnation>>();
    this.incarnationsMoveDirectiveUCMutSet = new SortedDictionary<int, VersionAndIncarnation<MoveDirectiveUCMutSetIncarnation>>();
    this.incarnationsWanderAICapabilityUCMutSet = new SortedDictionary<int, VersionAndIncarnation<WanderAICapabilityUCMutSetIncarnation>>();
    this.incarnationsBideAICapabilityUCMutSet = new SortedDictionary<int, VersionAndIncarnation<BideAICapabilityUCMutSetIncarnation>>();
    this.incarnationsTimeCloneAICapabilityUCMutSet = new SortedDictionary<int, VersionAndIncarnation<TimeCloneAICapabilityUCMutSetIncarnation>>();
    this.incarnationsAttackAICapabilityUCMutSet = new SortedDictionary<int, VersionAndIncarnation<AttackAICapabilityUCMutSetIncarnation>>();
    this.incarnationsCounteringUCMutSet = new SortedDictionary<int, VersionAndIncarnation<CounteringUCMutSetIncarnation>>();
    this.incarnationsShieldingUCMutSet = new SortedDictionary<int, VersionAndIncarnation<ShieldingUCMutSetIncarnation>>();
    this.incarnationsBidingOperationUCMutSet = new SortedDictionary<int, VersionAndIncarnation<BidingOperationUCMutSetIncarnation>>();
    this.incarnationsTimeAnchorTTCMutSet = new SortedDictionary<int, VersionAndIncarnation<TimeAnchorTTCMutSetIncarnation>>();
    this.incarnationsStaircaseTTCMutSet = new SortedDictionary<int, VersionAndIncarnation<StaircaseTTCMutSetIncarnation>>();
    this.incarnationsDecorativeTTCMutSet = new SortedDictionary<int, VersionAndIncarnation<DecorativeTTCMutSetIncarnation>>();
    this.incarnationsUnitMutSet = new SortedDictionary<int, VersionAndIncarnation<UnitMutSetIncarnation>>();
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
    this.incarnationsSquareCaveLevelController = new SortedDictionary<int, VersionAndIncarnation<SquareCaveLevelControllerIncarnation>>(that.incarnationsSquareCaveLevelController);
    this.incarnationsRidgeLevelController = new SortedDictionary<int, VersionAndIncarnation<RidgeLevelControllerIncarnation>>(that.incarnationsRidgeLevelController);
    this.incarnationsRavashrikeLevelController = new SortedDictionary<int, VersionAndIncarnation<RavashrikeLevelControllerIncarnation>>(that.incarnationsRavashrikeLevelController);
    this.incarnationsPentagonalCaveLevelController = new SortedDictionary<int, VersionAndIncarnation<PentagonalCaveLevelControllerIncarnation>>(that.incarnationsPentagonalCaveLevelController);
    this.incarnationsCliffLevelController = new SortedDictionary<int, VersionAndIncarnation<CliffLevelControllerIncarnation>>(that.incarnationsCliffLevelController);
    this.incarnationsLevel = new SortedDictionary<int, VersionAndIncarnation<LevelIncarnation>>(that.incarnationsLevel);
    this.incarnationsTimeAnchorTTC = new SortedDictionary<int, VersionAndIncarnation<TimeAnchorTTCIncarnation>>(that.incarnationsTimeAnchorTTC);
    this.incarnationsTerrainTile = new SortedDictionary<int, VersionAndIncarnation<TerrainTileIncarnation>>(that.incarnationsTerrainTile);
    this.incarnationsITerrainTileComponentMutBunch = new SortedDictionary<int, VersionAndIncarnation<ITerrainTileComponentMutBunchIncarnation>>(that.incarnationsITerrainTileComponentMutBunch);
    this.incarnationsTerrain = new SortedDictionary<int, VersionAndIncarnation<TerrainIncarnation>>(that.incarnationsTerrain);
    this.incarnationsStaircaseTTC = new SortedDictionary<int, VersionAndIncarnation<StaircaseTTCIncarnation>>(that.incarnationsStaircaseTTC);
    this.incarnationsDecorativeTTC = new SortedDictionary<int, VersionAndIncarnation<DecorativeTTCIncarnation>>(that.incarnationsDecorativeTTC);
    this.incarnationsManaPotion = new SortedDictionary<int, VersionAndIncarnation<ManaPotionIncarnation>>(that.incarnationsManaPotion);
    this.incarnationsHealthPotion = new SortedDictionary<int, VersionAndIncarnation<HealthPotionIncarnation>>(that.incarnationsHealthPotion);
    this.incarnationsGlaive = new SortedDictionary<int, VersionAndIncarnation<GlaiveIncarnation>>(that.incarnationsGlaive);
    this.incarnationsArmor = new SortedDictionary<int, VersionAndIncarnation<ArmorIncarnation>>(that.incarnationsArmor);
    this.incarnationsRand = new SortedDictionary<int, VersionAndIncarnation<RandIncarnation>>(that.incarnationsRand);
    this.incarnationsWanderAICapabilityUC = new SortedDictionary<int, VersionAndIncarnation<WanderAICapabilityUCIncarnation>>(that.incarnationsWanderAICapabilityUC);
    this.incarnationsCounteringUC = new SortedDictionary<int, VersionAndIncarnation<CounteringUCIncarnation>>(that.incarnationsCounteringUC);
    this.incarnationsShieldingUC = new SortedDictionary<int, VersionAndIncarnation<ShieldingUCIncarnation>>(that.incarnationsShieldingUC);
    this.incarnationsEvaporateImpulse = new SortedDictionary<int, VersionAndIncarnation<EvaporateImpulseIncarnation>>(that.incarnationsEvaporateImpulse);
    this.incarnationsTimeScriptDirectiveUC = new SortedDictionary<int, VersionAndIncarnation<TimeScriptDirectiveUCIncarnation>>(that.incarnationsTimeScriptDirectiveUC);
    this.incarnationsTimeCloneAICapabilityUC = new SortedDictionary<int, VersionAndIncarnation<TimeCloneAICapabilityUCIncarnation>>(that.incarnationsTimeCloneAICapabilityUC);
    this.incarnationsBidingOperationUC = new SortedDictionary<int, VersionAndIncarnation<BidingOperationUCIncarnation>>(that.incarnationsBidingOperationUC);
    this.incarnationsUnleashBideImpulse = new SortedDictionary<int, VersionAndIncarnation<UnleashBideImpulseIncarnation>>(that.incarnationsUnleashBideImpulse);
    this.incarnationsContinueBidingImpulse = new SortedDictionary<int, VersionAndIncarnation<ContinueBidingImpulseIncarnation>>(that.incarnationsContinueBidingImpulse);
    this.incarnationsStartBidingImpulse = new SortedDictionary<int, VersionAndIncarnation<StartBidingImpulseIncarnation>>(that.incarnationsStartBidingImpulse);
    this.incarnationsBideAICapabilityUC = new SortedDictionary<int, VersionAndIncarnation<BideAICapabilityUCIncarnation>>(that.incarnationsBideAICapabilityUC);
    this.incarnationsFireImpulse = new SortedDictionary<int, VersionAndIncarnation<FireImpulseIncarnation>>(that.incarnationsFireImpulse);
    this.incarnationsCounterImpulse = new SortedDictionary<int, VersionAndIncarnation<CounterImpulseIncarnation>>(that.incarnationsCounterImpulse);
    this.incarnationsDefendImpulse = new SortedDictionary<int, VersionAndIncarnation<DefendImpulseIncarnation>>(that.incarnationsDefendImpulse);
    this.incarnationsAttackImpulse = new SortedDictionary<int, VersionAndIncarnation<AttackImpulseIncarnation>>(that.incarnationsAttackImpulse);
    this.incarnationsPursueImpulse = new SortedDictionary<int, VersionAndIncarnation<PursueImpulseIncarnation>>(that.incarnationsPursueImpulse);
    this.incarnationsKillDirectiveUC = new SortedDictionary<int, VersionAndIncarnation<KillDirectiveUCIncarnation>>(that.incarnationsKillDirectiveUC);
    this.incarnationsAttackAICapabilityUC = new SortedDictionary<int, VersionAndIncarnation<AttackAICapabilityUCIncarnation>>(that.incarnationsAttackAICapabilityUC);
    this.incarnationsMoveImpulse = new SortedDictionary<int, VersionAndIncarnation<MoveImpulseIncarnation>>(that.incarnationsMoveImpulse);
    this.incarnationsMoveDirectiveUC = new SortedDictionary<int, VersionAndIncarnation<MoveDirectiveUCIncarnation>>(that.incarnationsMoveDirectiveUC);
    this.incarnationsUnit = new SortedDictionary<int, VersionAndIncarnation<UnitIncarnation>>(that.incarnationsUnit);
    this.incarnationsIUnitComponentMutBunch = new SortedDictionary<int, VersionAndIncarnation<IUnitComponentMutBunchIncarnation>>(that.incarnationsIUnitComponentMutBunch);
    this.incarnationsNoImpulse = new SortedDictionary<int, VersionAndIncarnation<NoImpulseIncarnation>>(that.incarnationsNoImpulse);
    this.incarnationsExecutionState = new SortedDictionary<int, VersionAndIncarnation<ExecutionStateIncarnation>>(that.incarnationsExecutionState);
    this.incarnationsIPostActingUCWeakMutBunch = new SortedDictionary<int, VersionAndIncarnation<IPostActingUCWeakMutBunchIncarnation>>(that.incarnationsIPostActingUCWeakMutBunch);
    this.incarnationsIPreActingUCWeakMutBunch = new SortedDictionary<int, VersionAndIncarnation<IPreActingUCWeakMutBunchIncarnation>>(that.incarnationsIPreActingUCWeakMutBunch);
    this.incarnationsGame = new SortedDictionary<int, VersionAndIncarnation<GameIncarnation>>(that.incarnationsGame);
    this.incarnationsIUnitEventMutList = new SortedDictionary<int, VersionAndIncarnation<IUnitEventMutListIncarnation>>(that.incarnationsIUnitEventMutList);
    this.incarnationsLocationMutList = new SortedDictionary<int, VersionAndIncarnation<LocationMutListIncarnation>>(that.incarnationsLocationMutList);
    this.incarnationsIRequestMutList = new SortedDictionary<int, VersionAndIncarnation<IRequestMutListIncarnation>>(that.incarnationsIRequestMutList);
    this.incarnationsLevelMutSet = new SortedDictionary<int, VersionAndIncarnation<LevelMutSetIncarnation>>(that.incarnationsLevelMutSet);
    this.incarnationsCounteringUCWeakMutSet = new SortedDictionary<int, VersionAndIncarnation<CounteringUCWeakMutSetIncarnation>>(that.incarnationsCounteringUCWeakMutSet);
    this.incarnationsShieldingUCWeakMutSet = new SortedDictionary<int, VersionAndIncarnation<ShieldingUCWeakMutSetIncarnation>>(that.incarnationsShieldingUCWeakMutSet);
    this.incarnationsAttackAICapabilityUCWeakMutSet = new SortedDictionary<int, VersionAndIncarnation<AttackAICapabilityUCWeakMutSetIncarnation>>(that.incarnationsAttackAICapabilityUCWeakMutSet);
    this.incarnationsTimeCloneAICapabilityUCWeakMutSet = new SortedDictionary<int, VersionAndIncarnation<TimeCloneAICapabilityUCWeakMutSetIncarnation>>(that.incarnationsTimeCloneAICapabilityUCWeakMutSet);
    this.incarnationsArmorMutSet = new SortedDictionary<int, VersionAndIncarnation<ArmorMutSetIncarnation>>(that.incarnationsArmorMutSet);
    this.incarnationsGlaiveMutSet = new SortedDictionary<int, VersionAndIncarnation<GlaiveMutSetIncarnation>>(that.incarnationsGlaiveMutSet);
    this.incarnationsManaPotionMutSet = new SortedDictionary<int, VersionAndIncarnation<ManaPotionMutSetIncarnation>>(that.incarnationsManaPotionMutSet);
    this.incarnationsHealthPotionMutSet = new SortedDictionary<int, VersionAndIncarnation<HealthPotionMutSetIncarnation>>(that.incarnationsHealthPotionMutSet);
    this.incarnationsTimeScriptDirectiveUCMutSet = new SortedDictionary<int, VersionAndIncarnation<TimeScriptDirectiveUCMutSetIncarnation>>(that.incarnationsTimeScriptDirectiveUCMutSet);
    this.incarnationsKillDirectiveUCMutSet = new SortedDictionary<int, VersionAndIncarnation<KillDirectiveUCMutSetIncarnation>>(that.incarnationsKillDirectiveUCMutSet);
    this.incarnationsMoveDirectiveUCMutSet = new SortedDictionary<int, VersionAndIncarnation<MoveDirectiveUCMutSetIncarnation>>(that.incarnationsMoveDirectiveUCMutSet);
    this.incarnationsWanderAICapabilityUCMutSet = new SortedDictionary<int, VersionAndIncarnation<WanderAICapabilityUCMutSetIncarnation>>(that.incarnationsWanderAICapabilityUCMutSet);
    this.incarnationsBideAICapabilityUCMutSet = new SortedDictionary<int, VersionAndIncarnation<BideAICapabilityUCMutSetIncarnation>>(that.incarnationsBideAICapabilityUCMutSet);
    this.incarnationsTimeCloneAICapabilityUCMutSet = new SortedDictionary<int, VersionAndIncarnation<TimeCloneAICapabilityUCMutSetIncarnation>>(that.incarnationsTimeCloneAICapabilityUCMutSet);
    this.incarnationsAttackAICapabilityUCMutSet = new SortedDictionary<int, VersionAndIncarnation<AttackAICapabilityUCMutSetIncarnation>>(that.incarnationsAttackAICapabilityUCMutSet);
    this.incarnationsCounteringUCMutSet = new SortedDictionary<int, VersionAndIncarnation<CounteringUCMutSetIncarnation>>(that.incarnationsCounteringUCMutSet);
    this.incarnationsShieldingUCMutSet = new SortedDictionary<int, VersionAndIncarnation<ShieldingUCMutSetIncarnation>>(that.incarnationsShieldingUCMutSet);
    this.incarnationsBidingOperationUCMutSet = new SortedDictionary<int, VersionAndIncarnation<BidingOperationUCMutSetIncarnation>>(that.incarnationsBidingOperationUCMutSet);
    this.incarnationsTimeAnchorTTCMutSet = new SortedDictionary<int, VersionAndIncarnation<TimeAnchorTTCMutSetIncarnation>>(that.incarnationsTimeAnchorTTCMutSet);
    this.incarnationsStaircaseTTCMutSet = new SortedDictionary<int, VersionAndIncarnation<StaircaseTTCMutSetIncarnation>>(that.incarnationsStaircaseTTCMutSet);
    this.incarnationsDecorativeTTCMutSet = new SortedDictionary<int, VersionAndIncarnation<DecorativeTTCMutSetIncarnation>>(that.incarnationsDecorativeTTCMutSet);
    this.incarnationsUnitMutSet = new SortedDictionary<int, VersionAndIncarnation<UnitMutSetIncarnation>>(that.incarnationsUnitMutSet);
    this.incarnationsTerrainTileByLocationMutMap = new SortedDictionary<int, VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>>(that.incarnationsTerrainTileByLocationMutMap);
  }
}

}
