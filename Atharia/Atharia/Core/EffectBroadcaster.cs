using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class EffectBroadcaster : IEffectVisitor {

  List<IEffectObserver> globalEffectObservers;


  readonly SortedDictionary<int, List<IRandEffectObserver>> observersForRand =
      new SortedDictionary<int, List<IRandEffectObserver>>();

  readonly SortedDictionary<int, List<IHoldPositionImpulseEffectObserver>> observersForHoldPositionImpulse =
      new SortedDictionary<int, List<IHoldPositionImpulseEffectObserver>>();

  readonly SortedDictionary<int, List<IWanderAICapabilityUCEffectObserver>> observersForWanderAICapabilityUC =
      new SortedDictionary<int, List<IWanderAICapabilityUCEffectObserver>>();

  readonly SortedDictionary<int, List<ITutorialDefyCounterUCEffectObserver>> observersForTutorialDefyCounterUC =
      new SortedDictionary<int, List<ITutorialDefyCounterUCEffectObserver>>();

  readonly SortedDictionary<int, List<IUnitEffectObserver>> observersForUnit =
      new SortedDictionary<int, List<IUnitEffectObserver>>();

  readonly SortedDictionary<int, List<IIUnitComponentMutBunchEffectObserver>> observersForIUnitComponentMutBunch =
      new SortedDictionary<int, List<IIUnitComponentMutBunchEffectObserver>>();

  readonly SortedDictionary<int, List<ILightningChargedUCEffectObserver>> observersForLightningChargedUC =
      new SortedDictionary<int, List<ILightningChargedUCEffectObserver>>();

  readonly SortedDictionary<int, List<ILightningChargingUCEffectObserver>> observersForLightningChargingUC =
      new SortedDictionary<int, List<ILightningChargingUCEffectObserver>>();

  readonly SortedDictionary<int, List<IDoomedUCEffectObserver>> observersForDoomedUC =
      new SortedDictionary<int, List<IDoomedUCEffectObserver>>();

  readonly SortedDictionary<int, List<ITemporaryCloneImpulseEffectObserver>> observersForTemporaryCloneImpulse =
      new SortedDictionary<int, List<ITemporaryCloneImpulseEffectObserver>>();

  readonly SortedDictionary<int, List<ITemporaryCloneAICapabilityUCEffectObserver>> observersForTemporaryCloneAICapabilityUC =
      new SortedDictionary<int, List<ITemporaryCloneAICapabilityUCEffectObserver>>();

  readonly SortedDictionary<int, List<IDeathTriggerUCEffectObserver>> observersForDeathTriggerUC =
      new SortedDictionary<int, List<IDeathTriggerUCEffectObserver>>();

  readonly SortedDictionary<int, List<IChallengingUCEffectObserver>> observersForChallengingUC =
      new SortedDictionary<int, List<IChallengingUCEffectObserver>>();

  readonly SortedDictionary<int, List<IBequeathUCEffectObserver>> observersForBequeathUC =
      new SortedDictionary<int, List<IBequeathUCEffectObserver>>();

  readonly SortedDictionary<int, List<ISummonImpulseEffectObserver>> observersForSummonImpulse =
      new SortedDictionary<int, List<ISummonImpulseEffectObserver>>();

  readonly SortedDictionary<int, List<ISummonAICapabilityUCEffectObserver>> observersForSummonAICapabilityUC =
      new SortedDictionary<int, List<ISummonAICapabilityUCEffectObserver>>();

  readonly SortedDictionary<int, List<ISorcerousUCEffectObserver>> observersForSorcerousUC =
      new SortedDictionary<int, List<ISorcerousUCEffectObserver>>();

  readonly SortedDictionary<int, List<IBaseOffenseUCEffectObserver>> observersForBaseOffenseUC =
      new SortedDictionary<int, List<IBaseOffenseUCEffectObserver>>();

  readonly SortedDictionary<int, List<IBaseSightRangeUCEffectObserver>> observersForBaseSightRangeUC =
      new SortedDictionary<int, List<IBaseSightRangeUCEffectObserver>>();

  readonly SortedDictionary<int, List<IBaseMovementTimeUCEffectObserver>> observersForBaseMovementTimeUC =
      new SortedDictionary<int, List<IBaseMovementTimeUCEffectObserver>>();

  readonly SortedDictionary<int, List<IBaseDefenseUCEffectObserver>> observersForBaseDefenseUC =
      new SortedDictionary<int, List<IBaseDefenseUCEffectObserver>>();

  readonly SortedDictionary<int, List<IBaseCombatTimeUCEffectObserver>> observersForBaseCombatTimeUC =
      new SortedDictionary<int, List<IBaseCombatTimeUCEffectObserver>>();

  readonly SortedDictionary<int, List<IMiredUCEffectObserver>> observersForMiredUC =
      new SortedDictionary<int, List<IMiredUCEffectObserver>>();

  readonly SortedDictionary<int, List<IMireImpulseEffectObserver>> observersForMireImpulse =
      new SortedDictionary<int, List<IMireImpulseEffectObserver>>();

  readonly SortedDictionary<int, List<IEvaporateImpulseEffectObserver>> observersForEvaporateImpulse =
      new SortedDictionary<int, List<IEvaporateImpulseEffectObserver>>();

  readonly SortedDictionary<int, List<ITimeCloneAICapabilityUCEffectObserver>> observersForTimeCloneAICapabilityUC =
      new SortedDictionary<int, List<ITimeCloneAICapabilityUCEffectObserver>>();

  readonly SortedDictionary<int, List<IMoveImpulseEffectObserver>> observersForMoveImpulse =
      new SortedDictionary<int, List<IMoveImpulseEffectObserver>>();

  readonly SortedDictionary<int, List<IKamikazeTargetTTCEffectObserver>> observersForKamikazeTargetTTC =
      new SortedDictionary<int, List<IKamikazeTargetTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IKamikazeJumpImpulseEffectObserver>> observersForKamikazeJumpImpulse =
      new SortedDictionary<int, List<IKamikazeJumpImpulseEffectObserver>>();

  readonly SortedDictionary<int, List<IKamikazeTargetImpulseEffectObserver>> observersForKamikazeTargetImpulse =
      new SortedDictionary<int, List<IKamikazeTargetImpulseEffectObserver>>();

  readonly SortedDictionary<int, List<IKamikazeAICapabilityUCEffectObserver>> observersForKamikazeAICapabilityUC =
      new SortedDictionary<int, List<IKamikazeAICapabilityUCEffectObserver>>();

  readonly SortedDictionary<int, List<IInvincibilityUCEffectObserver>> observersForInvincibilityUC =
      new SortedDictionary<int, List<IInvincibilityUCEffectObserver>>();

  readonly SortedDictionary<int, List<IGuardAICapabilityUCEffectObserver>> observersForGuardAICapabilityUC =
      new SortedDictionary<int, List<IGuardAICapabilityUCEffectObserver>>();

  readonly SortedDictionary<int, List<INoImpulseEffectObserver>> observersForNoImpulse =
      new SortedDictionary<int, List<INoImpulseEffectObserver>>();

  readonly SortedDictionary<int, List<IEvolvifyImpulseEffectObserver>> observersForEvolvifyImpulse =
      new SortedDictionary<int, List<IEvolvifyImpulseEffectObserver>>();

  readonly SortedDictionary<int, List<IEvolvifyAICapabilityUCEffectObserver>> observersForEvolvifyAICapabilityUC =
      new SortedDictionary<int, List<IEvolvifyAICapabilityUCEffectObserver>>();

  readonly SortedDictionary<int, List<IFireImpulseEffectObserver>> observersForFireImpulse =
      new SortedDictionary<int, List<IFireImpulseEffectObserver>>();

  readonly SortedDictionary<int, List<IOnFireUCEffectObserver>> observersForOnFireUC =
      new SortedDictionary<int, List<IOnFireUCEffectObserver>>();

  readonly SortedDictionary<int, List<IDefyingUCEffectObserver>> observersForDefyingUC =
      new SortedDictionary<int, List<IDefyingUCEffectObserver>>();

  readonly SortedDictionary<int, List<IDefyImpulseEffectObserver>> observersForDefyImpulse =
      new SortedDictionary<int, List<IDefyImpulseEffectObserver>>();

  readonly SortedDictionary<int, List<ICounteringUCEffectObserver>> observersForCounteringUC =
      new SortedDictionary<int, List<ICounteringUCEffectObserver>>();

  readonly SortedDictionary<int, List<ICounterImpulseEffectObserver>> observersForCounterImpulse =
      new SortedDictionary<int, List<ICounterImpulseEffectObserver>>();

  readonly SortedDictionary<int, List<IUnleashBideImpulseEffectObserver>> observersForUnleashBideImpulse =
      new SortedDictionary<int, List<IUnleashBideImpulseEffectObserver>>();

  readonly SortedDictionary<int, List<IContinueBidingImpulseEffectObserver>> observersForContinueBidingImpulse =
      new SortedDictionary<int, List<IContinueBidingImpulseEffectObserver>>();

  readonly SortedDictionary<int, List<IStartBidingImpulseEffectObserver>> observersForStartBidingImpulse =
      new SortedDictionary<int, List<IStartBidingImpulseEffectObserver>>();

  readonly SortedDictionary<int, List<IBideAICapabilityUCEffectObserver>> observersForBideAICapabilityUC =
      new SortedDictionary<int, List<IBideAICapabilityUCEffectObserver>>();

  readonly SortedDictionary<int, List<IAttackImpulseEffectObserver>> observersForAttackImpulse =
      new SortedDictionary<int, List<IAttackImpulseEffectObserver>>();

  readonly SortedDictionary<int, List<IPursueImpulseEffectObserver>> observersForPursueImpulse =
      new SortedDictionary<int, List<IPursueImpulseEffectObserver>>();

  readonly SortedDictionary<int, List<IKillDirectiveEffectObserver>> observersForKillDirective =
      new SortedDictionary<int, List<IKillDirectiveEffectObserver>>();

  readonly SortedDictionary<int, List<IAttackAICapabilityUCEffectObserver>> observersForAttackAICapabilityUC =
      new SortedDictionary<int, List<IAttackAICapabilityUCEffectObserver>>();

  readonly SortedDictionary<int, List<IWarperTTCEffectObserver>> observersForWarperTTC =
      new SortedDictionary<int, List<IWarperTTCEffectObserver>>();

  readonly SortedDictionary<int, List<ITimeAnchorTTCEffectObserver>> observersForTimeAnchorTTC =
      new SortedDictionary<int, List<ITimeAnchorTTCEffectObserver>>();

  readonly SortedDictionary<int, List<ITerrainTileEffectObserver>> observersForTerrainTile =
      new SortedDictionary<int, List<ITerrainTileEffectObserver>>();

  readonly SortedDictionary<int, List<IITerrainTileComponentMutBunchEffectObserver>> observersForITerrainTileComponentMutBunch =
      new SortedDictionary<int, List<IITerrainTileComponentMutBunchEffectObserver>>();

  readonly SortedDictionary<int, List<ITerrainEffectObserver>> observersForTerrain =
      new SortedDictionary<int, List<ITerrainEffectObserver>>();

  readonly SortedDictionary<int, List<ISimplePresenceTriggerTTCEffectObserver>> observersForSimplePresenceTriggerTTC =
      new SortedDictionary<int, List<ISimplePresenceTriggerTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IFireBombImpulseEffectObserver>> observersForFireBombImpulse =
      new SortedDictionary<int, List<IFireBombImpulseEffectObserver>>();

  readonly SortedDictionary<int, List<IFireBombTTCEffectObserver>> observersForFireBombTTC =
      new SortedDictionary<int, List<IFireBombTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IFlowerTTCEffectObserver>> observersForFlowerTTC =
      new SortedDictionary<int, List<IFlowerTTCEffectObserver>>();

  readonly SortedDictionary<int, List<ILotusTTCEffectObserver>> observersForLotusTTC =
      new SortedDictionary<int, List<ILotusTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IRoseTTCEffectObserver>> observersForRoseTTC =
      new SortedDictionary<int, List<IRoseTTCEffectObserver>>();

  readonly SortedDictionary<int, List<ILeafTTCEffectObserver>> observersForLeafTTC =
      new SortedDictionary<int, List<ILeafTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IOnFireTTCEffectObserver>> observersForOnFireTTC =
      new SortedDictionary<int, List<IOnFireTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IMarkerTTCEffectObserver>> observersForMarkerTTC =
      new SortedDictionary<int, List<IMarkerTTCEffectObserver>>();

  readonly SortedDictionary<int, List<ILevelLinkTTCEffectObserver>> observersForLevelLinkTTC =
      new SortedDictionary<int, List<ILevelLinkTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IMudTTCEffectObserver>> observersForMudTTC =
      new SortedDictionary<int, List<IMudTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IDirtTTCEffectObserver>> observersForDirtTTC =
      new SortedDictionary<int, List<IDirtTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IObsidianTTCEffectObserver>> observersForObsidianTTC =
      new SortedDictionary<int, List<IObsidianTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IDownStairsTTCEffectObserver>> observersForDownStairsTTC =
      new SortedDictionary<int, List<IDownStairsTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IUpStairsTTCEffectObserver>> observersForUpStairsTTC =
      new SortedDictionary<int, List<IUpStairsTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IWallTTCEffectObserver>> observersForWallTTC =
      new SortedDictionary<int, List<IWallTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IBloodTTCEffectObserver>> observersForBloodTTC =
      new SortedDictionary<int, List<IBloodTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IRocksTTCEffectObserver>> observersForRocksTTC =
      new SortedDictionary<int, List<IRocksTTCEffectObserver>>();

  readonly SortedDictionary<int, List<ITreeTTCEffectObserver>> observersForTreeTTC =
      new SortedDictionary<int, List<ITreeTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IWaterTTCEffectObserver>> observersForWaterTTC =
      new SortedDictionary<int, List<IWaterTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IFloorTTCEffectObserver>> observersForFloorTTC =
      new SortedDictionary<int, List<IFloorTTCEffectObserver>>();

  readonly SortedDictionary<int, List<ICaveWallTTCEffectObserver>> observersForCaveWallTTC =
      new SortedDictionary<int, List<ICaveWallTTCEffectObserver>>();

  readonly SortedDictionary<int, List<ICaveTTCEffectObserver>> observersForCaveTTC =
      new SortedDictionary<int, List<ICaveTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IFallsTTCEffectObserver>> observersForFallsTTC =
      new SortedDictionary<int, List<IFallsTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IObsidianFloorTTCEffectObserver>> observersForObsidianFloorTTC =
      new SortedDictionary<int, List<IObsidianFloorTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IMagmaTTCEffectObserver>> observersForMagmaTTC =
      new SortedDictionary<int, List<IMagmaTTCEffectObserver>>();

  readonly SortedDictionary<int, List<ICliffTTCEffectObserver>> observersForCliffTTC =
      new SortedDictionary<int, List<ICliffTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IRavaNestTTCEffectObserver>> observersForRavaNestTTC =
      new SortedDictionary<int, List<IRavaNestTTCEffectObserver>>();

  readonly SortedDictionary<int, List<ICliffLandingTTCEffectObserver>> observersForCliffLandingTTC =
      new SortedDictionary<int, List<ICliffLandingTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IStoneTTCEffectObserver>> observersForStoneTTC =
      new SortedDictionary<int, List<IStoneTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IGrassTTCEffectObserver>> observersForGrassTTC =
      new SortedDictionary<int, List<IGrassTTCEffectObserver>>();

  readonly SortedDictionary<int, List<ILevelEffectObserver>> observersForLevel =
      new SortedDictionary<int, List<ILevelEffectObserver>>();

  readonly SortedDictionary<int, List<ISpeedRingEffectObserver>> observersForSpeedRing =
      new SortedDictionary<int, List<ISpeedRingEffectObserver>>();

  readonly SortedDictionary<int, List<IManaPotionEffectObserver>> observersForManaPotion =
      new SortedDictionary<int, List<IManaPotionEffectObserver>>();

  readonly SortedDictionary<int, List<IWatEffectObserver>> observersForWat =
      new SortedDictionary<int, List<IWatEffectObserver>>();

  readonly SortedDictionary<int, List<IIPreActingUCWeakMutBunchEffectObserver>> observersForIPreActingUCWeakMutBunch =
      new SortedDictionary<int, List<IIPreActingUCWeakMutBunchEffectObserver>>();

  readonly SortedDictionary<int, List<IIPostActingUCWeakMutBunchEffectObserver>> observersForIPostActingUCWeakMutBunch =
      new SortedDictionary<int, List<IIPostActingUCWeakMutBunchEffectObserver>>();

  readonly SortedDictionary<int, List<IIImpulseStrongMutBunchEffectObserver>> observersForIImpulseStrongMutBunch =
      new SortedDictionary<int, List<IIImpulseStrongMutBunchEffectObserver>>();

  readonly SortedDictionary<int, List<IIItemStrongMutBunchEffectObserver>> observersForIItemStrongMutBunch =
      new SortedDictionary<int, List<IIItemStrongMutBunchEffectObserver>>();

  readonly SortedDictionary<int, List<IItemTTCEffectObserver>> observersForItemTTC =
      new SortedDictionary<int, List<IItemTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IHealthPotionEffectObserver>> observersForHealthPotion =
      new SortedDictionary<int, List<IHealthPotionEffectObserver>>();

  readonly SortedDictionary<int, List<IGlaiveEffectObserver>> observersForGlaive =
      new SortedDictionary<int, List<IGlaiveEffectObserver>>();

  readonly SortedDictionary<int, List<ISlowRodEffectObserver>> observersForSlowRod =
      new SortedDictionary<int, List<ISlowRodEffectObserver>>();

  readonly SortedDictionary<int, List<IExplosionRodEffectObserver>> observersForExplosionRod =
      new SortedDictionary<int, List<IExplosionRodEffectObserver>>();

  readonly SortedDictionary<int, List<IBlazeRodEffectObserver>> observersForBlazeRod =
      new SortedDictionary<int, List<IBlazeRodEffectObserver>>();

  readonly SortedDictionary<int, List<IBlastRodEffectObserver>> observersForBlastRod =
      new SortedDictionary<int, List<IBlastRodEffectObserver>>();

  readonly SortedDictionary<int, List<IArmorEffectObserver>> observersForArmor =
      new SortedDictionary<int, List<IArmorEffectObserver>>();

  readonly SortedDictionary<int, List<IVolcaetusLevelControllerEffectObserver>> observersForVolcaetusLevelController =
      new SortedDictionary<int, List<IVolcaetusLevelControllerEffectObserver>>();

  readonly SortedDictionary<int, List<ITutorial2LevelControllerEffectObserver>> observersForTutorial2LevelController =
      new SortedDictionary<int, List<ITutorial2LevelControllerEffectObserver>>();

  readonly SortedDictionary<int, List<ITutorial1LevelControllerEffectObserver>> observersForTutorial1LevelController =
      new SortedDictionary<int, List<ITutorial1LevelControllerEffectObserver>>();

  readonly SortedDictionary<int, List<IRetreatLevelControllerEffectObserver>> observersForRetreatLevelController =
      new SortedDictionary<int, List<IRetreatLevelControllerEffectObserver>>();

  readonly SortedDictionary<int, List<ISotaventoLevelControllerEffectObserver>> observersForSotaventoLevelController =
      new SortedDictionary<int, List<ISotaventoLevelControllerEffectObserver>>();

  readonly SortedDictionary<int, List<INestLevelControllerEffectObserver>> observersForNestLevelController =
      new SortedDictionary<int, List<INestLevelControllerEffectObserver>>();

  readonly SortedDictionary<int, List<ILakeLevelControllerEffectObserver>> observersForLakeLevelController =
      new SortedDictionary<int, List<ILakeLevelControllerEffectObserver>>();

  readonly SortedDictionary<int, List<IEmberDeepLevelLinkerTTCEffectObserver>> observersForEmberDeepLevelLinkerTTC =
      new SortedDictionary<int, List<IEmberDeepLevelLinkerTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IDirtRoadLevelControllerEffectObserver>> observersForDirtRoadLevelController =
      new SortedDictionary<int, List<IDirtRoadLevelControllerEffectObserver>>();

  readonly SortedDictionary<int, List<ICaveLevelControllerEffectObserver>> observersForCaveLevelController =
      new SortedDictionary<int, List<ICaveLevelControllerEffectObserver>>();

  readonly SortedDictionary<int, List<IBridgesLevelControllerEffectObserver>> observersForBridgesLevelController =
      new SortedDictionary<int, List<IBridgesLevelControllerEffectObserver>>();

  readonly SortedDictionary<int, List<IAncientTownLevelControllerEffectObserver>> observersForAncientTownLevelController =
      new SortedDictionary<int, List<IAncientTownLevelControllerEffectObserver>>();

  readonly SortedDictionary<int, List<ISquareCaveLevelControllerEffectObserver>> observersForSquareCaveLevelController =
      new SortedDictionary<int, List<ISquareCaveLevelControllerEffectObserver>>();

  readonly SortedDictionary<int, List<IRavashrikeLevelControllerEffectObserver>> observersForRavashrikeLevelController =
      new SortedDictionary<int, List<IRavashrikeLevelControllerEffectObserver>>();

  readonly SortedDictionary<int, List<IPentagonalCaveLevelControllerEffectObserver>> observersForPentagonalCaveLevelController =
      new SortedDictionary<int, List<IPentagonalCaveLevelControllerEffectObserver>>();

  readonly SortedDictionary<int, List<IIncendianFallsLevelLinkerTTCEffectObserver>> observersForIncendianFallsLevelLinkerTTC =
      new SortedDictionary<int, List<IIncendianFallsLevelLinkerTTCEffectObserver>>();

  readonly SortedDictionary<int, List<ICliffLevelControllerEffectObserver>> observersForCliffLevelController =
      new SortedDictionary<int, List<ICliffLevelControllerEffectObserver>>();

  readonly SortedDictionary<int, List<IPreGauntletLevelControllerEffectObserver>> observersForPreGauntletLevelController =
      new SortedDictionary<int, List<IPreGauntletLevelControllerEffectObserver>>();

  readonly SortedDictionary<int, List<IGauntletLevelControllerEffectObserver>> observersForGauntletLevelController =
      new SortedDictionary<int, List<IGauntletLevelControllerEffectObserver>>();

  readonly SortedDictionary<int, List<IRavaArcanaLevelLinkerTTCEffectObserver>> observersForRavaArcanaLevelLinkerTTC =
      new SortedDictionary<int, List<IRavaArcanaLevelLinkerTTCEffectObserver>>();

  readonly SortedDictionary<int, List<IJumpingCaveLevelControllerEffectObserver>> observersForJumpingCaveLevelController =
      new SortedDictionary<int, List<IJumpingCaveLevelControllerEffectObserver>>();

  readonly SortedDictionary<int, List<ICommEffectObserver>> observersForComm =
      new SortedDictionary<int, List<ICommEffectObserver>>();

  readonly SortedDictionary<int, List<IGameEffectObserver>> observersForGame =
      new SortedDictionary<int, List<IGameEffectObserver>>();

  readonly SortedDictionary<int, List<ICommMutListEffectObserver>> observersForCommMutList =
      new SortedDictionary<int, List<ICommMutListEffectObserver>>();

  readonly SortedDictionary<int, List<ILocationMutListEffectObserver>> observersForLocationMutList =
      new SortedDictionary<int, List<ILocationMutListEffectObserver>>();

  readonly SortedDictionary<int, List<IIRequestMutListEffectObserver>> observersForIRequestMutList =
      new SortedDictionary<int, List<IIRequestMutListEffectObserver>>();

  readonly Dictionary<int, List<ILevelMutSetEffectObserver>> observersForLevelMutSet =
      new Dictionary<int, List<ILevelMutSetEffectObserver>>();

  readonly Dictionary<int, List<IManaPotionStrongMutSetEffectObserver>> observersForManaPotionStrongMutSet =
      new Dictionary<int, List<IManaPotionStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<IHealthPotionStrongMutSetEffectObserver>> observersForHealthPotionStrongMutSet =
      new Dictionary<int, List<IHealthPotionStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<ISpeedRingStrongMutSetEffectObserver>> observersForSpeedRingStrongMutSet =
      new Dictionary<int, List<ISpeedRingStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<IGlaiveStrongMutSetEffectObserver>> observersForGlaiveStrongMutSet =
      new Dictionary<int, List<IGlaiveStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<ISlowRodStrongMutSetEffectObserver>> observersForSlowRodStrongMutSet =
      new Dictionary<int, List<ISlowRodStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<IExplosionRodStrongMutSetEffectObserver>> observersForExplosionRodStrongMutSet =
      new Dictionary<int, List<IExplosionRodStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<IBlazeRodStrongMutSetEffectObserver>> observersForBlazeRodStrongMutSet =
      new Dictionary<int, List<IBlazeRodStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<IBlastRodStrongMutSetEffectObserver>> observersForBlastRodStrongMutSet =
      new Dictionary<int, List<IBlastRodStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<IArmorStrongMutSetEffectObserver>> observersForArmorStrongMutSet =
      new Dictionary<int, List<IArmorStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<IHoldPositionImpulseStrongMutSetEffectObserver>> observersForHoldPositionImpulseStrongMutSet =
      new Dictionary<int, List<IHoldPositionImpulseStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<ITemporaryCloneImpulseStrongMutSetEffectObserver>> observersForTemporaryCloneImpulseStrongMutSet =
      new Dictionary<int, List<ITemporaryCloneImpulseStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<ISummonImpulseStrongMutSetEffectObserver>> observersForSummonImpulseStrongMutSet =
      new Dictionary<int, List<ISummonImpulseStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<IMireImpulseStrongMutSetEffectObserver>> observersForMireImpulseStrongMutSet =
      new Dictionary<int, List<IMireImpulseStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<IEvaporateImpulseStrongMutSetEffectObserver>> observersForEvaporateImpulseStrongMutSet =
      new Dictionary<int, List<IEvaporateImpulseStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<IMoveImpulseStrongMutSetEffectObserver>> observersForMoveImpulseStrongMutSet =
      new Dictionary<int, List<IMoveImpulseStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<IKamikazeJumpImpulseStrongMutSetEffectObserver>> observersForKamikazeJumpImpulseStrongMutSet =
      new Dictionary<int, List<IKamikazeJumpImpulseStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<IKamikazeTargetImpulseStrongMutSetEffectObserver>> observersForKamikazeTargetImpulseStrongMutSet =
      new Dictionary<int, List<IKamikazeTargetImpulseStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<INoImpulseStrongMutSetEffectObserver>> observersForNoImpulseStrongMutSet =
      new Dictionary<int, List<INoImpulseStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<IEvolvifyImpulseStrongMutSetEffectObserver>> observersForEvolvifyImpulseStrongMutSet =
      new Dictionary<int, List<IEvolvifyImpulseStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<IFireImpulseStrongMutSetEffectObserver>> observersForFireImpulseStrongMutSet =
      new Dictionary<int, List<IFireImpulseStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<IDefyImpulseStrongMutSetEffectObserver>> observersForDefyImpulseStrongMutSet =
      new Dictionary<int, List<IDefyImpulseStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<ICounterImpulseStrongMutSetEffectObserver>> observersForCounterImpulseStrongMutSet =
      new Dictionary<int, List<ICounterImpulseStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<IUnleashBideImpulseStrongMutSetEffectObserver>> observersForUnleashBideImpulseStrongMutSet =
      new Dictionary<int, List<IUnleashBideImpulseStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<IContinueBidingImpulseStrongMutSetEffectObserver>> observersForContinueBidingImpulseStrongMutSet =
      new Dictionary<int, List<IContinueBidingImpulseStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<IStartBidingImpulseStrongMutSetEffectObserver>> observersForStartBidingImpulseStrongMutSet =
      new Dictionary<int, List<IStartBidingImpulseStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<IAttackImpulseStrongMutSetEffectObserver>> observersForAttackImpulseStrongMutSet =
      new Dictionary<int, List<IAttackImpulseStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<IPursueImpulseStrongMutSetEffectObserver>> observersForPursueImpulseStrongMutSet =
      new Dictionary<int, List<IPursueImpulseStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<IFireBombImpulseStrongMutSetEffectObserver>> observersForFireBombImpulseStrongMutSet =
      new Dictionary<int, List<IFireBombImpulseStrongMutSetEffectObserver>>();

  readonly Dictionary<int, List<ILightningChargedUCWeakMutSetEffectObserver>> observersForLightningChargedUCWeakMutSet =
      new Dictionary<int, List<ILightningChargedUCWeakMutSetEffectObserver>>();

  readonly Dictionary<int, List<ITimeCloneAICapabilityUCWeakMutSetEffectObserver>> observersForTimeCloneAICapabilityUCWeakMutSet =
      new Dictionary<int, List<ITimeCloneAICapabilityUCWeakMutSetEffectObserver>>();

  readonly Dictionary<int, List<IDoomedUCWeakMutSetEffectObserver>> observersForDoomedUCWeakMutSet =
      new Dictionary<int, List<IDoomedUCWeakMutSetEffectObserver>>();

  readonly Dictionary<int, List<IMiredUCWeakMutSetEffectObserver>> observersForMiredUCWeakMutSet =
      new Dictionary<int, List<IMiredUCWeakMutSetEffectObserver>>();

  readonly Dictionary<int, List<IInvincibilityUCWeakMutSetEffectObserver>> observersForInvincibilityUCWeakMutSet =
      new Dictionary<int, List<IInvincibilityUCWeakMutSetEffectObserver>>();

  readonly Dictionary<int, List<IOnFireUCWeakMutSetEffectObserver>> observersForOnFireUCWeakMutSet =
      new Dictionary<int, List<IOnFireUCWeakMutSetEffectObserver>>();

  readonly Dictionary<int, List<IDefyingUCWeakMutSetEffectObserver>> observersForDefyingUCWeakMutSet =
      new Dictionary<int, List<IDefyingUCWeakMutSetEffectObserver>>();

  readonly Dictionary<int, List<ICounteringUCWeakMutSetEffectObserver>> observersForCounteringUCWeakMutSet =
      new Dictionary<int, List<ICounteringUCWeakMutSetEffectObserver>>();

  readonly Dictionary<int, List<IAttackAICapabilityUCWeakMutSetEffectObserver>> observersForAttackAICapabilityUCWeakMutSet =
      new Dictionary<int, List<IAttackAICapabilityUCWeakMutSetEffectObserver>>();

  readonly Dictionary<int, List<IUnitMutSetEffectObserver>> observersForUnitMutSet =
      new Dictionary<int, List<IUnitMutSetEffectObserver>>();

  readonly Dictionary<int, List<ISimplePresenceTriggerTTCMutSetEffectObserver>> observersForSimplePresenceTriggerTTCMutSet =
      new Dictionary<int, List<ISimplePresenceTriggerTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IItemTTCMutSetEffectObserver>> observersForItemTTCMutSet =
      new Dictionary<int, List<IItemTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IFlowerTTCMutSetEffectObserver>> observersForFlowerTTCMutSet =
      new Dictionary<int, List<IFlowerTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<ILotusTTCMutSetEffectObserver>> observersForLotusTTCMutSet =
      new Dictionary<int, List<ILotusTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IRoseTTCMutSetEffectObserver>> observersForRoseTTCMutSet =
      new Dictionary<int, List<IRoseTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<ILeafTTCMutSetEffectObserver>> observersForLeafTTCMutSet =
      new Dictionary<int, List<ILeafTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IKamikazeTargetTTCMutSetEffectObserver>> observersForKamikazeTargetTTCMutSet =
      new Dictionary<int, List<IKamikazeTargetTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IWarperTTCMutSetEffectObserver>> observersForWarperTTCMutSet =
      new Dictionary<int, List<IWarperTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<ITimeAnchorTTCMutSetEffectObserver>> observersForTimeAnchorTTCMutSet =
      new Dictionary<int, List<ITimeAnchorTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IFireBombTTCMutSetEffectObserver>> observersForFireBombTTCMutSet =
      new Dictionary<int, List<IFireBombTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IOnFireTTCMutSetEffectObserver>> observersForOnFireTTCMutSet =
      new Dictionary<int, List<IOnFireTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IMarkerTTCMutSetEffectObserver>> observersForMarkerTTCMutSet =
      new Dictionary<int, List<IMarkerTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<ILevelLinkTTCMutSetEffectObserver>> observersForLevelLinkTTCMutSet =
      new Dictionary<int, List<ILevelLinkTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IMudTTCMutSetEffectObserver>> observersForMudTTCMutSet =
      new Dictionary<int, List<IMudTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IDirtTTCMutSetEffectObserver>> observersForDirtTTCMutSet =
      new Dictionary<int, List<IDirtTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IObsidianTTCMutSetEffectObserver>> observersForObsidianTTCMutSet =
      new Dictionary<int, List<IObsidianTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IDownStairsTTCMutSetEffectObserver>> observersForDownStairsTTCMutSet =
      new Dictionary<int, List<IDownStairsTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IUpStairsTTCMutSetEffectObserver>> observersForUpStairsTTCMutSet =
      new Dictionary<int, List<IUpStairsTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IWallTTCMutSetEffectObserver>> observersForWallTTCMutSet =
      new Dictionary<int, List<IWallTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IBloodTTCMutSetEffectObserver>> observersForBloodTTCMutSet =
      new Dictionary<int, List<IBloodTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IRocksTTCMutSetEffectObserver>> observersForRocksTTCMutSet =
      new Dictionary<int, List<IRocksTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<ITreeTTCMutSetEffectObserver>> observersForTreeTTCMutSet =
      new Dictionary<int, List<ITreeTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IWaterTTCMutSetEffectObserver>> observersForWaterTTCMutSet =
      new Dictionary<int, List<IWaterTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IFloorTTCMutSetEffectObserver>> observersForFloorTTCMutSet =
      new Dictionary<int, List<IFloorTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<ICaveWallTTCMutSetEffectObserver>> observersForCaveWallTTCMutSet =
      new Dictionary<int, List<ICaveWallTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<ICaveTTCMutSetEffectObserver>> observersForCaveTTCMutSet =
      new Dictionary<int, List<ICaveTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IFallsTTCMutSetEffectObserver>> observersForFallsTTCMutSet =
      new Dictionary<int, List<IFallsTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IObsidianFloorTTCMutSetEffectObserver>> observersForObsidianFloorTTCMutSet =
      new Dictionary<int, List<IObsidianFloorTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IMagmaTTCMutSetEffectObserver>> observersForMagmaTTCMutSet =
      new Dictionary<int, List<IMagmaTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<ICliffTTCMutSetEffectObserver>> observersForCliffTTCMutSet =
      new Dictionary<int, List<ICliffTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IRavaNestTTCMutSetEffectObserver>> observersForRavaNestTTCMutSet =
      new Dictionary<int, List<IRavaNestTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<ICliffLandingTTCMutSetEffectObserver>> observersForCliffLandingTTCMutSet =
      new Dictionary<int, List<ICliffLandingTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IStoneTTCMutSetEffectObserver>> observersForStoneTTCMutSet =
      new Dictionary<int, List<IStoneTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IGrassTTCMutSetEffectObserver>> observersForGrassTTCMutSet =
      new Dictionary<int, List<IGrassTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IEmberDeepLevelLinkerTTCMutSetEffectObserver>> observersForEmberDeepLevelLinkerTTCMutSet =
      new Dictionary<int, List<IEmberDeepLevelLinkerTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IIncendianFallsLevelLinkerTTCMutSetEffectObserver>> observersForIncendianFallsLevelLinkerTTCMutSet =
      new Dictionary<int, List<IIncendianFallsLevelLinkerTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IRavaArcanaLevelLinkerTTCMutSetEffectObserver>> observersForRavaArcanaLevelLinkerTTCMutSet =
      new Dictionary<int, List<IRavaArcanaLevelLinkerTTCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IDeathTriggerUCMutSetEffectObserver>> observersForDeathTriggerUCMutSet =
      new Dictionary<int, List<IDeathTriggerUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IBequeathUCMutSetEffectObserver>> observersForBequeathUCMutSet =
      new Dictionary<int, List<IBequeathUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<ITutorialDefyCounterUCMutSetEffectObserver>> observersForTutorialDefyCounterUCMutSet =
      new Dictionary<int, List<ITutorialDefyCounterUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<ILightningChargingUCMutSetEffectObserver>> observersForLightningChargingUCMutSet =
      new Dictionary<int, List<ILightningChargingUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IWanderAICapabilityUCMutSetEffectObserver>> observersForWanderAICapabilityUCMutSet =
      new Dictionary<int, List<IWanderAICapabilityUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<ITemporaryCloneAICapabilityUCMutSetEffectObserver>> observersForTemporaryCloneAICapabilityUCMutSet =
      new Dictionary<int, List<ITemporaryCloneAICapabilityUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<ISummonAICapabilityUCMutSetEffectObserver>> observersForSummonAICapabilityUCMutSet =
      new Dictionary<int, List<ISummonAICapabilityUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IKamikazeAICapabilityUCMutSetEffectObserver>> observersForKamikazeAICapabilityUCMutSet =
      new Dictionary<int, List<IKamikazeAICapabilityUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IGuardAICapabilityUCMutSetEffectObserver>> observersForGuardAICapabilityUCMutSet =
      new Dictionary<int, List<IGuardAICapabilityUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IEvolvifyAICapabilityUCMutSetEffectObserver>> observersForEvolvifyAICapabilityUCMutSet =
      new Dictionary<int, List<IEvolvifyAICapabilityUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<ITimeCloneAICapabilityUCMutSetEffectObserver>> observersForTimeCloneAICapabilityUCMutSet =
      new Dictionary<int, List<ITimeCloneAICapabilityUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IDoomedUCMutSetEffectObserver>> observersForDoomedUCMutSet =
      new Dictionary<int, List<IDoomedUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IMiredUCMutSetEffectObserver>> observersForMiredUCMutSet =
      new Dictionary<int, List<IMiredUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IOnFireUCMutSetEffectObserver>> observersForOnFireUCMutSet =
      new Dictionary<int, List<IOnFireUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IAttackAICapabilityUCMutSetEffectObserver>> observersForAttackAICapabilityUCMutSet =
      new Dictionary<int, List<IAttackAICapabilityUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<ICounteringUCMutSetEffectObserver>> observersForCounteringUCMutSet =
      new Dictionary<int, List<ICounteringUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<ILightningChargedUCMutSetEffectObserver>> observersForLightningChargedUCMutSet =
      new Dictionary<int, List<ILightningChargedUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IInvincibilityUCMutSetEffectObserver>> observersForInvincibilityUCMutSet =
      new Dictionary<int, List<IInvincibilityUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IDefyingUCMutSetEffectObserver>> observersForDefyingUCMutSet =
      new Dictionary<int, List<IDefyingUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IBideAICapabilityUCMutSetEffectObserver>> observersForBideAICapabilityUCMutSet =
      new Dictionary<int, List<IBideAICapabilityUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IBaseSightRangeUCMutSetEffectObserver>> observersForBaseSightRangeUCMutSet =
      new Dictionary<int, List<IBaseSightRangeUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IBaseMovementTimeUCMutSetEffectObserver>> observersForBaseMovementTimeUCMutSet =
      new Dictionary<int, List<IBaseMovementTimeUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IBaseCombatTimeUCMutSetEffectObserver>> observersForBaseCombatTimeUCMutSet =
      new Dictionary<int, List<IBaseCombatTimeUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IManaPotionMutSetEffectObserver>> observersForManaPotionMutSet =
      new Dictionary<int, List<IManaPotionMutSetEffectObserver>>();

  readonly Dictionary<int, List<IHealthPotionMutSetEffectObserver>> observersForHealthPotionMutSet =
      new Dictionary<int, List<IHealthPotionMutSetEffectObserver>>();

  readonly Dictionary<int, List<ISpeedRingMutSetEffectObserver>> observersForSpeedRingMutSet =
      new Dictionary<int, List<ISpeedRingMutSetEffectObserver>>();

  readonly Dictionary<int, List<IGlaiveMutSetEffectObserver>> observersForGlaiveMutSet =
      new Dictionary<int, List<IGlaiveMutSetEffectObserver>>();

  readonly Dictionary<int, List<ISlowRodMutSetEffectObserver>> observersForSlowRodMutSet =
      new Dictionary<int, List<ISlowRodMutSetEffectObserver>>();

  readonly Dictionary<int, List<IExplosionRodMutSetEffectObserver>> observersForExplosionRodMutSet =
      new Dictionary<int, List<IExplosionRodMutSetEffectObserver>>();

  readonly Dictionary<int, List<IBlazeRodMutSetEffectObserver>> observersForBlazeRodMutSet =
      new Dictionary<int, List<IBlazeRodMutSetEffectObserver>>();

  readonly Dictionary<int, List<IBlastRodMutSetEffectObserver>> observersForBlastRodMutSet =
      new Dictionary<int, List<IBlastRodMutSetEffectObserver>>();

  readonly Dictionary<int, List<IArmorMutSetEffectObserver>> observersForArmorMutSet =
      new Dictionary<int, List<IArmorMutSetEffectObserver>>();

  readonly Dictionary<int, List<IChallengingUCMutSetEffectObserver>> observersForChallengingUCMutSet =
      new Dictionary<int, List<IChallengingUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<ISorcerousUCMutSetEffectObserver>> observersForSorcerousUCMutSet =
      new Dictionary<int, List<ISorcerousUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IBaseOffenseUCMutSetEffectObserver>> observersForBaseOffenseUCMutSet =
      new Dictionary<int, List<IBaseOffenseUCMutSetEffectObserver>>();

  readonly Dictionary<int, List<IBaseDefenseUCMutSetEffectObserver>> observersForBaseDefenseUCMutSet =
      new Dictionary<int, List<IBaseDefenseUCMutSetEffectObserver>>();

  readonly SortedDictionary<int, List<ITerrainTileByLocationMutMapEffectObserver>> observersForTerrainTileByLocationMutMap =
      new SortedDictionary<int, List<ITerrainTileByLocationMutMapEffectObserver>>();

  readonly SortedDictionary<int, List<IKamikazeTargetTTCStrongByLocationMutMapEffectObserver>> observersForKamikazeTargetTTCStrongByLocationMutMap =
      new SortedDictionary<int, List<IKamikazeTargetTTCStrongByLocationMutMapEffectObserver>>();

  public EffectBroadcaster() {
    globalEffectObservers = new List<IEffectObserver>();
  }

  public void AddGlobalObserver(IEffectObserver obs) {
    this.globalEffectObservers.Add(obs);
  }

  public void RemoveGlobalObserver(IEffectObserver obs) {
    this.globalEffectObservers.Remove(obs);
  }


  public void visitRandEffect(IRandEffect effect) {
    if (observersForRand.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IRandEffectObserver>(observers)) {
        observer.OnRandEffect(effect);
      }
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

  public void visitHoldPositionImpulseEffect(IHoldPositionImpulseEffect effect) {
    if (observersForHoldPositionImpulse.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IHoldPositionImpulseEffectObserver>(observers)) {
        observer.OnHoldPositionImpulseEffect(effect);
      }
    }
  }
  public void AddHoldPositionImpulseObserver(int id, IHoldPositionImpulseEffectObserver observer) {
    List<IHoldPositionImpulseEffectObserver> obsies;
    if (!observersForHoldPositionImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<IHoldPositionImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersForHoldPositionImpulse[id] = obsies;
  }

  public void RemoveHoldPositionImpulseObserver(int id, IHoldPositionImpulseEffectObserver observer) {
    if (observersForHoldPositionImpulse.ContainsKey(id)) {
      var list = observersForHoldPositionImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForHoldPositionImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitWanderAICapabilityUCEffect(IWanderAICapabilityUCEffect effect) {
    if (observersForWanderAICapabilityUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IWanderAICapabilityUCEffectObserver>(observers)) {
        observer.OnWanderAICapabilityUCEffect(effect);
      }
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

  public void visitTutorialDefyCounterUCEffect(ITutorialDefyCounterUCEffect effect) {
    if (observersForTutorialDefyCounterUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ITutorialDefyCounterUCEffectObserver>(observers)) {
        observer.OnTutorialDefyCounterUCEffect(effect);
      }
    }
  }
  public void AddTutorialDefyCounterUCObserver(int id, ITutorialDefyCounterUCEffectObserver observer) {
    List<ITutorialDefyCounterUCEffectObserver> obsies;
    if (!observersForTutorialDefyCounterUC.TryGetValue(id, out obsies)) {
      obsies = new List<ITutorialDefyCounterUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForTutorialDefyCounterUC[id] = obsies;
  }

  public void RemoveTutorialDefyCounterUCObserver(int id, ITutorialDefyCounterUCEffectObserver observer) {
    if (observersForTutorialDefyCounterUC.ContainsKey(id)) {
      var list = observersForTutorialDefyCounterUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForTutorialDefyCounterUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitUnitEffect(IUnitEffect effect) {
    if (observersForUnit.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IUnitEffectObserver>(observers)) {
        observer.OnUnitEffect(effect);
      }
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

  public void visitIUnitComponentMutBunchEffect(IIUnitComponentMutBunchEffect effect) {
    if (observersForIUnitComponentMutBunch.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IIUnitComponentMutBunchEffectObserver>(observers)) {
        observer.OnIUnitComponentMutBunchEffect(effect);
      }
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

  public void visitLightningChargedUCEffect(ILightningChargedUCEffect effect) {
    if (observersForLightningChargedUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ILightningChargedUCEffectObserver>(observers)) {
        observer.OnLightningChargedUCEffect(effect);
      }
    }
  }
  public void AddLightningChargedUCObserver(int id, ILightningChargedUCEffectObserver observer) {
    List<ILightningChargedUCEffectObserver> obsies;
    if (!observersForLightningChargedUC.TryGetValue(id, out obsies)) {
      obsies = new List<ILightningChargedUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForLightningChargedUC[id] = obsies;
  }

  public void RemoveLightningChargedUCObserver(int id, ILightningChargedUCEffectObserver observer) {
    if (observersForLightningChargedUC.ContainsKey(id)) {
      var list = observersForLightningChargedUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForLightningChargedUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitLightningChargingUCEffect(ILightningChargingUCEffect effect) {
    if (observersForLightningChargingUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ILightningChargingUCEffectObserver>(observers)) {
        observer.OnLightningChargingUCEffect(effect);
      }
    }
  }
  public void AddLightningChargingUCObserver(int id, ILightningChargingUCEffectObserver observer) {
    List<ILightningChargingUCEffectObserver> obsies;
    if (!observersForLightningChargingUC.TryGetValue(id, out obsies)) {
      obsies = new List<ILightningChargingUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForLightningChargingUC[id] = obsies;
  }

  public void RemoveLightningChargingUCObserver(int id, ILightningChargingUCEffectObserver observer) {
    if (observersForLightningChargingUC.ContainsKey(id)) {
      var list = observersForLightningChargingUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForLightningChargingUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitDoomedUCEffect(IDoomedUCEffect effect) {
    if (observersForDoomedUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IDoomedUCEffectObserver>(observers)) {
        observer.OnDoomedUCEffect(effect);
      }
    }
  }
  public void AddDoomedUCObserver(int id, IDoomedUCEffectObserver observer) {
    List<IDoomedUCEffectObserver> obsies;
    if (!observersForDoomedUC.TryGetValue(id, out obsies)) {
      obsies = new List<IDoomedUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForDoomedUC[id] = obsies;
  }

  public void RemoveDoomedUCObserver(int id, IDoomedUCEffectObserver observer) {
    if (observersForDoomedUC.ContainsKey(id)) {
      var list = observersForDoomedUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForDoomedUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitTemporaryCloneImpulseEffect(ITemporaryCloneImpulseEffect effect) {
    if (observersForTemporaryCloneImpulse.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ITemporaryCloneImpulseEffectObserver>(observers)) {
        observer.OnTemporaryCloneImpulseEffect(effect);
      }
    }
  }
  public void AddTemporaryCloneImpulseObserver(int id, ITemporaryCloneImpulseEffectObserver observer) {
    List<ITemporaryCloneImpulseEffectObserver> obsies;
    if (!observersForTemporaryCloneImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<ITemporaryCloneImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersForTemporaryCloneImpulse[id] = obsies;
  }

  public void RemoveTemporaryCloneImpulseObserver(int id, ITemporaryCloneImpulseEffectObserver observer) {
    if (observersForTemporaryCloneImpulse.ContainsKey(id)) {
      var list = observersForTemporaryCloneImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForTemporaryCloneImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitTemporaryCloneAICapabilityUCEffect(ITemporaryCloneAICapabilityUCEffect effect) {
    if (observersForTemporaryCloneAICapabilityUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ITemporaryCloneAICapabilityUCEffectObserver>(observers)) {
        observer.OnTemporaryCloneAICapabilityUCEffect(effect);
      }
    }
  }
  public void AddTemporaryCloneAICapabilityUCObserver(int id, ITemporaryCloneAICapabilityUCEffectObserver observer) {
    List<ITemporaryCloneAICapabilityUCEffectObserver> obsies;
    if (!observersForTemporaryCloneAICapabilityUC.TryGetValue(id, out obsies)) {
      obsies = new List<ITemporaryCloneAICapabilityUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForTemporaryCloneAICapabilityUC[id] = obsies;
  }

  public void RemoveTemporaryCloneAICapabilityUCObserver(int id, ITemporaryCloneAICapabilityUCEffectObserver observer) {
    if (observersForTemporaryCloneAICapabilityUC.ContainsKey(id)) {
      var list = observersForTemporaryCloneAICapabilityUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForTemporaryCloneAICapabilityUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitDeathTriggerUCEffect(IDeathTriggerUCEffect effect) {
    if (observersForDeathTriggerUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IDeathTriggerUCEffectObserver>(observers)) {
        observer.OnDeathTriggerUCEffect(effect);
      }
    }
  }
  public void AddDeathTriggerUCObserver(int id, IDeathTriggerUCEffectObserver observer) {
    List<IDeathTriggerUCEffectObserver> obsies;
    if (!observersForDeathTriggerUC.TryGetValue(id, out obsies)) {
      obsies = new List<IDeathTriggerUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForDeathTriggerUC[id] = obsies;
  }

  public void RemoveDeathTriggerUCObserver(int id, IDeathTriggerUCEffectObserver observer) {
    if (observersForDeathTriggerUC.ContainsKey(id)) {
      var list = observersForDeathTriggerUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForDeathTriggerUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitChallengingUCEffect(IChallengingUCEffect effect) {
    if (observersForChallengingUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IChallengingUCEffectObserver>(observers)) {
        observer.OnChallengingUCEffect(effect);
      }
    }
  }
  public void AddChallengingUCObserver(int id, IChallengingUCEffectObserver observer) {
    List<IChallengingUCEffectObserver> obsies;
    if (!observersForChallengingUC.TryGetValue(id, out obsies)) {
      obsies = new List<IChallengingUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForChallengingUC[id] = obsies;
  }

  public void RemoveChallengingUCObserver(int id, IChallengingUCEffectObserver observer) {
    if (observersForChallengingUC.ContainsKey(id)) {
      var list = observersForChallengingUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForChallengingUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitBequeathUCEffect(IBequeathUCEffect effect) {
    if (observersForBequeathUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IBequeathUCEffectObserver>(observers)) {
        observer.OnBequeathUCEffect(effect);
      }
    }
  }
  public void AddBequeathUCObserver(int id, IBequeathUCEffectObserver observer) {
    List<IBequeathUCEffectObserver> obsies;
    if (!observersForBequeathUC.TryGetValue(id, out obsies)) {
      obsies = new List<IBequeathUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForBequeathUC[id] = obsies;
  }

  public void RemoveBequeathUCObserver(int id, IBequeathUCEffectObserver observer) {
    if (observersForBequeathUC.ContainsKey(id)) {
      var list = observersForBequeathUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForBequeathUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitSummonImpulseEffect(ISummonImpulseEffect effect) {
    if (observersForSummonImpulse.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ISummonImpulseEffectObserver>(observers)) {
        observer.OnSummonImpulseEffect(effect);
      }
    }
  }
  public void AddSummonImpulseObserver(int id, ISummonImpulseEffectObserver observer) {
    List<ISummonImpulseEffectObserver> obsies;
    if (!observersForSummonImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<ISummonImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersForSummonImpulse[id] = obsies;
  }

  public void RemoveSummonImpulseObserver(int id, ISummonImpulseEffectObserver observer) {
    if (observersForSummonImpulse.ContainsKey(id)) {
      var list = observersForSummonImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForSummonImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitSummonAICapabilityUCEffect(ISummonAICapabilityUCEffect effect) {
    if (observersForSummonAICapabilityUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ISummonAICapabilityUCEffectObserver>(observers)) {
        observer.OnSummonAICapabilityUCEffect(effect);
      }
    }
  }
  public void AddSummonAICapabilityUCObserver(int id, ISummonAICapabilityUCEffectObserver observer) {
    List<ISummonAICapabilityUCEffectObserver> obsies;
    if (!observersForSummonAICapabilityUC.TryGetValue(id, out obsies)) {
      obsies = new List<ISummonAICapabilityUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForSummonAICapabilityUC[id] = obsies;
  }

  public void RemoveSummonAICapabilityUCObserver(int id, ISummonAICapabilityUCEffectObserver observer) {
    if (observersForSummonAICapabilityUC.ContainsKey(id)) {
      var list = observersForSummonAICapabilityUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForSummonAICapabilityUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitSorcerousUCEffect(ISorcerousUCEffect effect) {
    if (observersForSorcerousUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ISorcerousUCEffectObserver>(observers)) {
        observer.OnSorcerousUCEffect(effect);
      }
    }
  }
  public void AddSorcerousUCObserver(int id, ISorcerousUCEffectObserver observer) {
    List<ISorcerousUCEffectObserver> obsies;
    if (!observersForSorcerousUC.TryGetValue(id, out obsies)) {
      obsies = new List<ISorcerousUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForSorcerousUC[id] = obsies;
  }

  public void RemoveSorcerousUCObserver(int id, ISorcerousUCEffectObserver observer) {
    if (observersForSorcerousUC.ContainsKey(id)) {
      var list = observersForSorcerousUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForSorcerousUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitBaseOffenseUCEffect(IBaseOffenseUCEffect effect) {
    if (observersForBaseOffenseUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IBaseOffenseUCEffectObserver>(observers)) {
        observer.OnBaseOffenseUCEffect(effect);
      }
    }
  }
  public void AddBaseOffenseUCObserver(int id, IBaseOffenseUCEffectObserver observer) {
    List<IBaseOffenseUCEffectObserver> obsies;
    if (!observersForBaseOffenseUC.TryGetValue(id, out obsies)) {
      obsies = new List<IBaseOffenseUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForBaseOffenseUC[id] = obsies;
  }

  public void RemoveBaseOffenseUCObserver(int id, IBaseOffenseUCEffectObserver observer) {
    if (observersForBaseOffenseUC.ContainsKey(id)) {
      var list = observersForBaseOffenseUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForBaseOffenseUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitBaseSightRangeUCEffect(IBaseSightRangeUCEffect effect) {
    if (observersForBaseSightRangeUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IBaseSightRangeUCEffectObserver>(observers)) {
        observer.OnBaseSightRangeUCEffect(effect);
      }
    }
  }
  public void AddBaseSightRangeUCObserver(int id, IBaseSightRangeUCEffectObserver observer) {
    List<IBaseSightRangeUCEffectObserver> obsies;
    if (!observersForBaseSightRangeUC.TryGetValue(id, out obsies)) {
      obsies = new List<IBaseSightRangeUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForBaseSightRangeUC[id] = obsies;
  }

  public void RemoveBaseSightRangeUCObserver(int id, IBaseSightRangeUCEffectObserver observer) {
    if (observersForBaseSightRangeUC.ContainsKey(id)) {
      var list = observersForBaseSightRangeUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForBaseSightRangeUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitBaseMovementTimeUCEffect(IBaseMovementTimeUCEffect effect) {
    if (observersForBaseMovementTimeUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IBaseMovementTimeUCEffectObserver>(observers)) {
        observer.OnBaseMovementTimeUCEffect(effect);
      }
    }
  }
  public void AddBaseMovementTimeUCObserver(int id, IBaseMovementTimeUCEffectObserver observer) {
    List<IBaseMovementTimeUCEffectObserver> obsies;
    if (!observersForBaseMovementTimeUC.TryGetValue(id, out obsies)) {
      obsies = new List<IBaseMovementTimeUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForBaseMovementTimeUC[id] = obsies;
  }

  public void RemoveBaseMovementTimeUCObserver(int id, IBaseMovementTimeUCEffectObserver observer) {
    if (observersForBaseMovementTimeUC.ContainsKey(id)) {
      var list = observersForBaseMovementTimeUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForBaseMovementTimeUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitBaseDefenseUCEffect(IBaseDefenseUCEffect effect) {
    if (observersForBaseDefenseUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IBaseDefenseUCEffectObserver>(observers)) {
        observer.OnBaseDefenseUCEffect(effect);
      }
    }
  }
  public void AddBaseDefenseUCObserver(int id, IBaseDefenseUCEffectObserver observer) {
    List<IBaseDefenseUCEffectObserver> obsies;
    if (!observersForBaseDefenseUC.TryGetValue(id, out obsies)) {
      obsies = new List<IBaseDefenseUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForBaseDefenseUC[id] = obsies;
  }

  public void RemoveBaseDefenseUCObserver(int id, IBaseDefenseUCEffectObserver observer) {
    if (observersForBaseDefenseUC.ContainsKey(id)) {
      var list = observersForBaseDefenseUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForBaseDefenseUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitBaseCombatTimeUCEffect(IBaseCombatTimeUCEffect effect) {
    if (observersForBaseCombatTimeUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IBaseCombatTimeUCEffectObserver>(observers)) {
        observer.OnBaseCombatTimeUCEffect(effect);
      }
    }
  }
  public void AddBaseCombatTimeUCObserver(int id, IBaseCombatTimeUCEffectObserver observer) {
    List<IBaseCombatTimeUCEffectObserver> obsies;
    if (!observersForBaseCombatTimeUC.TryGetValue(id, out obsies)) {
      obsies = new List<IBaseCombatTimeUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForBaseCombatTimeUC[id] = obsies;
  }

  public void RemoveBaseCombatTimeUCObserver(int id, IBaseCombatTimeUCEffectObserver observer) {
    if (observersForBaseCombatTimeUC.ContainsKey(id)) {
      var list = observersForBaseCombatTimeUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForBaseCombatTimeUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitMiredUCEffect(IMiredUCEffect effect) {
    if (observersForMiredUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IMiredUCEffectObserver>(observers)) {
        observer.OnMiredUCEffect(effect);
      }
    }
  }
  public void AddMiredUCObserver(int id, IMiredUCEffectObserver observer) {
    List<IMiredUCEffectObserver> obsies;
    if (!observersForMiredUC.TryGetValue(id, out obsies)) {
      obsies = new List<IMiredUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForMiredUC[id] = obsies;
  }

  public void RemoveMiredUCObserver(int id, IMiredUCEffectObserver observer) {
    if (observersForMiredUC.ContainsKey(id)) {
      var list = observersForMiredUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForMiredUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitMireImpulseEffect(IMireImpulseEffect effect) {
    if (observersForMireImpulse.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IMireImpulseEffectObserver>(observers)) {
        observer.OnMireImpulseEffect(effect);
      }
    }
  }
  public void AddMireImpulseObserver(int id, IMireImpulseEffectObserver observer) {
    List<IMireImpulseEffectObserver> obsies;
    if (!observersForMireImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<IMireImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersForMireImpulse[id] = obsies;
  }

  public void RemoveMireImpulseObserver(int id, IMireImpulseEffectObserver observer) {
    if (observersForMireImpulse.ContainsKey(id)) {
      var list = observersForMireImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForMireImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitEvaporateImpulseEffect(IEvaporateImpulseEffect effect) {
    if (observersForEvaporateImpulse.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IEvaporateImpulseEffectObserver>(observers)) {
        observer.OnEvaporateImpulseEffect(effect);
      }
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

  public void visitTimeCloneAICapabilityUCEffect(ITimeCloneAICapabilityUCEffect effect) {
    if (observersForTimeCloneAICapabilityUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ITimeCloneAICapabilityUCEffectObserver>(observers)) {
        observer.OnTimeCloneAICapabilityUCEffect(effect);
      }
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

  public void visitMoveImpulseEffect(IMoveImpulseEffect effect) {
    if (observersForMoveImpulse.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IMoveImpulseEffectObserver>(observers)) {
        observer.OnMoveImpulseEffect(effect);
      }
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

  public void visitKamikazeTargetTTCEffect(IKamikazeTargetTTCEffect effect) {
    if (observersForKamikazeTargetTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IKamikazeTargetTTCEffectObserver>(observers)) {
        observer.OnKamikazeTargetTTCEffect(effect);
      }
    }
  }
  public void AddKamikazeTargetTTCObserver(int id, IKamikazeTargetTTCEffectObserver observer) {
    List<IKamikazeTargetTTCEffectObserver> obsies;
    if (!observersForKamikazeTargetTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IKamikazeTargetTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForKamikazeTargetTTC[id] = obsies;
  }

  public void RemoveKamikazeTargetTTCObserver(int id, IKamikazeTargetTTCEffectObserver observer) {
    if (observersForKamikazeTargetTTC.ContainsKey(id)) {
      var list = observersForKamikazeTargetTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForKamikazeTargetTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitKamikazeJumpImpulseEffect(IKamikazeJumpImpulseEffect effect) {
    if (observersForKamikazeJumpImpulse.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IKamikazeJumpImpulseEffectObserver>(observers)) {
        observer.OnKamikazeJumpImpulseEffect(effect);
      }
    }
  }
  public void AddKamikazeJumpImpulseObserver(int id, IKamikazeJumpImpulseEffectObserver observer) {
    List<IKamikazeJumpImpulseEffectObserver> obsies;
    if (!observersForKamikazeJumpImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<IKamikazeJumpImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersForKamikazeJumpImpulse[id] = obsies;
  }

  public void RemoveKamikazeJumpImpulseObserver(int id, IKamikazeJumpImpulseEffectObserver observer) {
    if (observersForKamikazeJumpImpulse.ContainsKey(id)) {
      var list = observersForKamikazeJumpImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForKamikazeJumpImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitKamikazeTargetImpulseEffect(IKamikazeTargetImpulseEffect effect) {
    if (observersForKamikazeTargetImpulse.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IKamikazeTargetImpulseEffectObserver>(observers)) {
        observer.OnKamikazeTargetImpulseEffect(effect);
      }
    }
  }
  public void AddKamikazeTargetImpulseObserver(int id, IKamikazeTargetImpulseEffectObserver observer) {
    List<IKamikazeTargetImpulseEffectObserver> obsies;
    if (!observersForKamikazeTargetImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<IKamikazeTargetImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersForKamikazeTargetImpulse[id] = obsies;
  }

  public void RemoveKamikazeTargetImpulseObserver(int id, IKamikazeTargetImpulseEffectObserver observer) {
    if (observersForKamikazeTargetImpulse.ContainsKey(id)) {
      var list = observersForKamikazeTargetImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForKamikazeTargetImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitKamikazeAICapabilityUCEffect(IKamikazeAICapabilityUCEffect effect) {
    if (observersForKamikazeAICapabilityUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IKamikazeAICapabilityUCEffectObserver>(observers)) {
        observer.OnKamikazeAICapabilityUCEffect(effect);
      }
    }
  }
  public void AddKamikazeAICapabilityUCObserver(int id, IKamikazeAICapabilityUCEffectObserver observer) {
    List<IKamikazeAICapabilityUCEffectObserver> obsies;
    if (!observersForKamikazeAICapabilityUC.TryGetValue(id, out obsies)) {
      obsies = new List<IKamikazeAICapabilityUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForKamikazeAICapabilityUC[id] = obsies;
  }

  public void RemoveKamikazeAICapabilityUCObserver(int id, IKamikazeAICapabilityUCEffectObserver observer) {
    if (observersForKamikazeAICapabilityUC.ContainsKey(id)) {
      var list = observersForKamikazeAICapabilityUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForKamikazeAICapabilityUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitInvincibilityUCEffect(IInvincibilityUCEffect effect) {
    if (observersForInvincibilityUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IInvincibilityUCEffectObserver>(observers)) {
        observer.OnInvincibilityUCEffect(effect);
      }
    }
  }
  public void AddInvincibilityUCObserver(int id, IInvincibilityUCEffectObserver observer) {
    List<IInvincibilityUCEffectObserver> obsies;
    if (!observersForInvincibilityUC.TryGetValue(id, out obsies)) {
      obsies = new List<IInvincibilityUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForInvincibilityUC[id] = obsies;
  }

  public void RemoveInvincibilityUCObserver(int id, IInvincibilityUCEffectObserver observer) {
    if (observersForInvincibilityUC.ContainsKey(id)) {
      var list = observersForInvincibilityUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForInvincibilityUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitGuardAICapabilityUCEffect(IGuardAICapabilityUCEffect effect) {
    if (observersForGuardAICapabilityUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IGuardAICapabilityUCEffectObserver>(observers)) {
        observer.OnGuardAICapabilityUCEffect(effect);
      }
    }
  }
  public void AddGuardAICapabilityUCObserver(int id, IGuardAICapabilityUCEffectObserver observer) {
    List<IGuardAICapabilityUCEffectObserver> obsies;
    if (!observersForGuardAICapabilityUC.TryGetValue(id, out obsies)) {
      obsies = new List<IGuardAICapabilityUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForGuardAICapabilityUC[id] = obsies;
  }

  public void RemoveGuardAICapabilityUCObserver(int id, IGuardAICapabilityUCEffectObserver observer) {
    if (observersForGuardAICapabilityUC.ContainsKey(id)) {
      var list = observersForGuardAICapabilityUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForGuardAICapabilityUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitNoImpulseEffect(INoImpulseEffect effect) {
    if (observersForNoImpulse.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<INoImpulseEffectObserver>(observers)) {
        observer.OnNoImpulseEffect(effect);
      }
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

  public void visitEvolvifyImpulseEffect(IEvolvifyImpulseEffect effect) {
    if (observersForEvolvifyImpulse.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IEvolvifyImpulseEffectObserver>(observers)) {
        observer.OnEvolvifyImpulseEffect(effect);
      }
    }
  }
  public void AddEvolvifyImpulseObserver(int id, IEvolvifyImpulseEffectObserver observer) {
    List<IEvolvifyImpulseEffectObserver> obsies;
    if (!observersForEvolvifyImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<IEvolvifyImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersForEvolvifyImpulse[id] = obsies;
  }

  public void RemoveEvolvifyImpulseObserver(int id, IEvolvifyImpulseEffectObserver observer) {
    if (observersForEvolvifyImpulse.ContainsKey(id)) {
      var list = observersForEvolvifyImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForEvolvifyImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitEvolvifyAICapabilityUCEffect(IEvolvifyAICapabilityUCEffect effect) {
    if (observersForEvolvifyAICapabilityUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IEvolvifyAICapabilityUCEffectObserver>(observers)) {
        observer.OnEvolvifyAICapabilityUCEffect(effect);
      }
    }
  }
  public void AddEvolvifyAICapabilityUCObserver(int id, IEvolvifyAICapabilityUCEffectObserver observer) {
    List<IEvolvifyAICapabilityUCEffectObserver> obsies;
    if (!observersForEvolvifyAICapabilityUC.TryGetValue(id, out obsies)) {
      obsies = new List<IEvolvifyAICapabilityUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForEvolvifyAICapabilityUC[id] = obsies;
  }

  public void RemoveEvolvifyAICapabilityUCObserver(int id, IEvolvifyAICapabilityUCEffectObserver observer) {
    if (observersForEvolvifyAICapabilityUC.ContainsKey(id)) {
      var list = observersForEvolvifyAICapabilityUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForEvolvifyAICapabilityUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitFireImpulseEffect(IFireImpulseEffect effect) {
    if (observersForFireImpulse.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IFireImpulseEffectObserver>(observers)) {
        observer.OnFireImpulseEffect(effect);
      }
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

  public void visitOnFireUCEffect(IOnFireUCEffect effect) {
    if (observersForOnFireUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IOnFireUCEffectObserver>(observers)) {
        observer.OnOnFireUCEffect(effect);
      }
    }
  }
  public void AddOnFireUCObserver(int id, IOnFireUCEffectObserver observer) {
    List<IOnFireUCEffectObserver> obsies;
    if (!observersForOnFireUC.TryGetValue(id, out obsies)) {
      obsies = new List<IOnFireUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForOnFireUC[id] = obsies;
  }

  public void RemoveOnFireUCObserver(int id, IOnFireUCEffectObserver observer) {
    if (observersForOnFireUC.ContainsKey(id)) {
      var list = observersForOnFireUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForOnFireUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitDefyingUCEffect(IDefyingUCEffect effect) {
    if (observersForDefyingUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IDefyingUCEffectObserver>(observers)) {
        observer.OnDefyingUCEffect(effect);
      }
    }
  }
  public void AddDefyingUCObserver(int id, IDefyingUCEffectObserver observer) {
    List<IDefyingUCEffectObserver> obsies;
    if (!observersForDefyingUC.TryGetValue(id, out obsies)) {
      obsies = new List<IDefyingUCEffectObserver>();
    }
    obsies.Add(observer);
    observersForDefyingUC[id] = obsies;
  }

  public void RemoveDefyingUCObserver(int id, IDefyingUCEffectObserver observer) {
    if (observersForDefyingUC.ContainsKey(id)) {
      var list = observersForDefyingUC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForDefyingUC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitDefyImpulseEffect(IDefyImpulseEffect effect) {
    if (observersForDefyImpulse.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IDefyImpulseEffectObserver>(observers)) {
        observer.OnDefyImpulseEffect(effect);
      }
    }
  }
  public void AddDefyImpulseObserver(int id, IDefyImpulseEffectObserver observer) {
    List<IDefyImpulseEffectObserver> obsies;
    if (!observersForDefyImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<IDefyImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersForDefyImpulse[id] = obsies;
  }

  public void RemoveDefyImpulseObserver(int id, IDefyImpulseEffectObserver observer) {
    if (observersForDefyImpulse.ContainsKey(id)) {
      var list = observersForDefyImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForDefyImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitCounteringUCEffect(ICounteringUCEffect effect) {
    if (observersForCounteringUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ICounteringUCEffectObserver>(observers)) {
        observer.OnCounteringUCEffect(effect);
      }
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

  public void visitCounterImpulseEffect(ICounterImpulseEffect effect) {
    if (observersForCounterImpulse.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ICounterImpulseEffectObserver>(observers)) {
        observer.OnCounterImpulseEffect(effect);
      }
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

  public void visitUnleashBideImpulseEffect(IUnleashBideImpulseEffect effect) {
    if (observersForUnleashBideImpulse.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IUnleashBideImpulseEffectObserver>(observers)) {
        observer.OnUnleashBideImpulseEffect(effect);
      }
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

  public void visitContinueBidingImpulseEffect(IContinueBidingImpulseEffect effect) {
    if (observersForContinueBidingImpulse.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IContinueBidingImpulseEffectObserver>(observers)) {
        observer.OnContinueBidingImpulseEffect(effect);
      }
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

  public void visitStartBidingImpulseEffect(IStartBidingImpulseEffect effect) {
    if (observersForStartBidingImpulse.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IStartBidingImpulseEffectObserver>(observers)) {
        observer.OnStartBidingImpulseEffect(effect);
      }
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

  public void visitBideAICapabilityUCEffect(IBideAICapabilityUCEffect effect) {
    if (observersForBideAICapabilityUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IBideAICapabilityUCEffectObserver>(observers)) {
        observer.OnBideAICapabilityUCEffect(effect);
      }
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

  public void visitAttackImpulseEffect(IAttackImpulseEffect effect) {
    if (observersForAttackImpulse.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IAttackImpulseEffectObserver>(observers)) {
        observer.OnAttackImpulseEffect(effect);
      }
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

  public void visitPursueImpulseEffect(IPursueImpulseEffect effect) {
    if (observersForPursueImpulse.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IPursueImpulseEffectObserver>(observers)) {
        observer.OnPursueImpulseEffect(effect);
      }
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

  public void visitKillDirectiveEffect(IKillDirectiveEffect effect) {
    if (observersForKillDirective.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IKillDirectiveEffectObserver>(observers)) {
        observer.OnKillDirectiveEffect(effect);
      }
    }
  }
  public void AddKillDirectiveObserver(int id, IKillDirectiveEffectObserver observer) {
    List<IKillDirectiveEffectObserver> obsies;
    if (!observersForKillDirective.TryGetValue(id, out obsies)) {
      obsies = new List<IKillDirectiveEffectObserver>();
    }
    obsies.Add(observer);
    observersForKillDirective[id] = obsies;
  }

  public void RemoveKillDirectiveObserver(int id, IKillDirectiveEffectObserver observer) {
    if (observersForKillDirective.ContainsKey(id)) {
      var list = observersForKillDirective[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForKillDirective.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitAttackAICapabilityUCEffect(IAttackAICapabilityUCEffect effect) {
    if (observersForAttackAICapabilityUC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IAttackAICapabilityUCEffectObserver>(observers)) {
        observer.OnAttackAICapabilityUCEffect(effect);
      }
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

  public void visitWarperTTCEffect(IWarperTTCEffect effect) {
    if (observersForWarperTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IWarperTTCEffectObserver>(observers)) {
        observer.OnWarperTTCEffect(effect);
      }
    }
  }
  public void AddWarperTTCObserver(int id, IWarperTTCEffectObserver observer) {
    List<IWarperTTCEffectObserver> obsies;
    if (!observersForWarperTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IWarperTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForWarperTTC[id] = obsies;
  }

  public void RemoveWarperTTCObserver(int id, IWarperTTCEffectObserver observer) {
    if (observersForWarperTTC.ContainsKey(id)) {
      var list = observersForWarperTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForWarperTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitTimeAnchorTTCEffect(ITimeAnchorTTCEffect effect) {
    if (observersForTimeAnchorTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ITimeAnchorTTCEffectObserver>(observers)) {
        observer.OnTimeAnchorTTCEffect(effect);
      }
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

  public void visitTerrainTileEffect(ITerrainTileEffect effect) {
    if (observersForTerrainTile.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ITerrainTileEffectObserver>(observers)) {
        observer.OnTerrainTileEffect(effect);
      }
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

  public void visitITerrainTileComponentMutBunchEffect(IITerrainTileComponentMutBunchEffect effect) {
    if (observersForITerrainTileComponentMutBunch.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IITerrainTileComponentMutBunchEffectObserver>(observers)) {
        observer.OnITerrainTileComponentMutBunchEffect(effect);
      }
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

  public void visitTerrainEffect(ITerrainEffect effect) {
    if (observersForTerrain.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ITerrainEffectObserver>(observers)) {
        observer.OnTerrainEffect(effect);
      }
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

  public void visitSimplePresenceTriggerTTCEffect(ISimplePresenceTriggerTTCEffect effect) {
    if (observersForSimplePresenceTriggerTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ISimplePresenceTriggerTTCEffectObserver>(observers)) {
        observer.OnSimplePresenceTriggerTTCEffect(effect);
      }
    }
  }
  public void AddSimplePresenceTriggerTTCObserver(int id, ISimplePresenceTriggerTTCEffectObserver observer) {
    List<ISimplePresenceTriggerTTCEffectObserver> obsies;
    if (!observersForSimplePresenceTriggerTTC.TryGetValue(id, out obsies)) {
      obsies = new List<ISimplePresenceTriggerTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForSimplePresenceTriggerTTC[id] = obsies;
  }

  public void RemoveSimplePresenceTriggerTTCObserver(int id, ISimplePresenceTriggerTTCEffectObserver observer) {
    if (observersForSimplePresenceTriggerTTC.ContainsKey(id)) {
      var list = observersForSimplePresenceTriggerTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForSimplePresenceTriggerTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitFireBombImpulseEffect(IFireBombImpulseEffect effect) {
    if (observersForFireBombImpulse.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IFireBombImpulseEffectObserver>(observers)) {
        observer.OnFireBombImpulseEffect(effect);
      }
    }
  }
  public void AddFireBombImpulseObserver(int id, IFireBombImpulseEffectObserver observer) {
    List<IFireBombImpulseEffectObserver> obsies;
    if (!observersForFireBombImpulse.TryGetValue(id, out obsies)) {
      obsies = new List<IFireBombImpulseEffectObserver>();
    }
    obsies.Add(observer);
    observersForFireBombImpulse[id] = obsies;
  }

  public void RemoveFireBombImpulseObserver(int id, IFireBombImpulseEffectObserver observer) {
    if (observersForFireBombImpulse.ContainsKey(id)) {
      var list = observersForFireBombImpulse[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForFireBombImpulse.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitFireBombTTCEffect(IFireBombTTCEffect effect) {
    if (observersForFireBombTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IFireBombTTCEffectObserver>(observers)) {
        observer.OnFireBombTTCEffect(effect);
      }
    }
  }
  public void AddFireBombTTCObserver(int id, IFireBombTTCEffectObserver observer) {
    List<IFireBombTTCEffectObserver> obsies;
    if (!observersForFireBombTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IFireBombTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForFireBombTTC[id] = obsies;
  }

  public void RemoveFireBombTTCObserver(int id, IFireBombTTCEffectObserver observer) {
    if (observersForFireBombTTC.ContainsKey(id)) {
      var list = observersForFireBombTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForFireBombTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitFlowerTTCEffect(IFlowerTTCEffect effect) {
    if (observersForFlowerTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IFlowerTTCEffectObserver>(observers)) {
        observer.OnFlowerTTCEffect(effect);
      }
    }
  }
  public void AddFlowerTTCObserver(int id, IFlowerTTCEffectObserver observer) {
    List<IFlowerTTCEffectObserver> obsies;
    if (!observersForFlowerTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IFlowerTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForFlowerTTC[id] = obsies;
  }

  public void RemoveFlowerTTCObserver(int id, IFlowerTTCEffectObserver observer) {
    if (observersForFlowerTTC.ContainsKey(id)) {
      var list = observersForFlowerTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForFlowerTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitLotusTTCEffect(ILotusTTCEffect effect) {
    if (observersForLotusTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ILotusTTCEffectObserver>(observers)) {
        observer.OnLotusTTCEffect(effect);
      }
    }
  }
  public void AddLotusTTCObserver(int id, ILotusTTCEffectObserver observer) {
    List<ILotusTTCEffectObserver> obsies;
    if (!observersForLotusTTC.TryGetValue(id, out obsies)) {
      obsies = new List<ILotusTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForLotusTTC[id] = obsies;
  }

  public void RemoveLotusTTCObserver(int id, ILotusTTCEffectObserver observer) {
    if (observersForLotusTTC.ContainsKey(id)) {
      var list = observersForLotusTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForLotusTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitRoseTTCEffect(IRoseTTCEffect effect) {
    if (observersForRoseTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IRoseTTCEffectObserver>(observers)) {
        observer.OnRoseTTCEffect(effect);
      }
    }
  }
  public void AddRoseTTCObserver(int id, IRoseTTCEffectObserver observer) {
    List<IRoseTTCEffectObserver> obsies;
    if (!observersForRoseTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IRoseTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForRoseTTC[id] = obsies;
  }

  public void RemoveRoseTTCObserver(int id, IRoseTTCEffectObserver observer) {
    if (observersForRoseTTC.ContainsKey(id)) {
      var list = observersForRoseTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForRoseTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitLeafTTCEffect(ILeafTTCEffect effect) {
    if (observersForLeafTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ILeafTTCEffectObserver>(observers)) {
        observer.OnLeafTTCEffect(effect);
      }
    }
  }
  public void AddLeafTTCObserver(int id, ILeafTTCEffectObserver observer) {
    List<ILeafTTCEffectObserver> obsies;
    if (!observersForLeafTTC.TryGetValue(id, out obsies)) {
      obsies = new List<ILeafTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForLeafTTC[id] = obsies;
  }

  public void RemoveLeafTTCObserver(int id, ILeafTTCEffectObserver observer) {
    if (observersForLeafTTC.ContainsKey(id)) {
      var list = observersForLeafTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForLeafTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitOnFireTTCEffect(IOnFireTTCEffect effect) {
    if (observersForOnFireTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IOnFireTTCEffectObserver>(observers)) {
        observer.OnOnFireTTCEffect(effect);
      }
    }
  }
  public void AddOnFireTTCObserver(int id, IOnFireTTCEffectObserver observer) {
    List<IOnFireTTCEffectObserver> obsies;
    if (!observersForOnFireTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IOnFireTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForOnFireTTC[id] = obsies;
  }

  public void RemoveOnFireTTCObserver(int id, IOnFireTTCEffectObserver observer) {
    if (observersForOnFireTTC.ContainsKey(id)) {
      var list = observersForOnFireTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForOnFireTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitMarkerTTCEffect(IMarkerTTCEffect effect) {
    if (observersForMarkerTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IMarkerTTCEffectObserver>(observers)) {
        observer.OnMarkerTTCEffect(effect);
      }
    }
  }
  public void AddMarkerTTCObserver(int id, IMarkerTTCEffectObserver observer) {
    List<IMarkerTTCEffectObserver> obsies;
    if (!observersForMarkerTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IMarkerTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForMarkerTTC[id] = obsies;
  }

  public void RemoveMarkerTTCObserver(int id, IMarkerTTCEffectObserver observer) {
    if (observersForMarkerTTC.ContainsKey(id)) {
      var list = observersForMarkerTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForMarkerTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitLevelLinkTTCEffect(ILevelLinkTTCEffect effect) {
    if (observersForLevelLinkTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ILevelLinkTTCEffectObserver>(observers)) {
        observer.OnLevelLinkTTCEffect(effect);
      }
    }
  }
  public void AddLevelLinkTTCObserver(int id, ILevelLinkTTCEffectObserver observer) {
    List<ILevelLinkTTCEffectObserver> obsies;
    if (!observersForLevelLinkTTC.TryGetValue(id, out obsies)) {
      obsies = new List<ILevelLinkTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForLevelLinkTTC[id] = obsies;
  }

  public void RemoveLevelLinkTTCObserver(int id, ILevelLinkTTCEffectObserver observer) {
    if (observersForLevelLinkTTC.ContainsKey(id)) {
      var list = observersForLevelLinkTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForLevelLinkTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitMudTTCEffect(IMudTTCEffect effect) {
    if (observersForMudTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IMudTTCEffectObserver>(observers)) {
        observer.OnMudTTCEffect(effect);
      }
    }
  }
  public void AddMudTTCObserver(int id, IMudTTCEffectObserver observer) {
    List<IMudTTCEffectObserver> obsies;
    if (!observersForMudTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IMudTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForMudTTC[id] = obsies;
  }

  public void RemoveMudTTCObserver(int id, IMudTTCEffectObserver observer) {
    if (observersForMudTTC.ContainsKey(id)) {
      var list = observersForMudTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForMudTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitDirtTTCEffect(IDirtTTCEffect effect) {
    if (observersForDirtTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IDirtTTCEffectObserver>(observers)) {
        observer.OnDirtTTCEffect(effect);
      }
    }
  }
  public void AddDirtTTCObserver(int id, IDirtTTCEffectObserver observer) {
    List<IDirtTTCEffectObserver> obsies;
    if (!observersForDirtTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IDirtTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForDirtTTC[id] = obsies;
  }

  public void RemoveDirtTTCObserver(int id, IDirtTTCEffectObserver observer) {
    if (observersForDirtTTC.ContainsKey(id)) {
      var list = observersForDirtTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForDirtTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitObsidianTTCEffect(IObsidianTTCEffect effect) {
    if (observersForObsidianTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IObsidianTTCEffectObserver>(observers)) {
        observer.OnObsidianTTCEffect(effect);
      }
    }
  }
  public void AddObsidianTTCObserver(int id, IObsidianTTCEffectObserver observer) {
    List<IObsidianTTCEffectObserver> obsies;
    if (!observersForObsidianTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IObsidianTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForObsidianTTC[id] = obsies;
  }

  public void RemoveObsidianTTCObserver(int id, IObsidianTTCEffectObserver observer) {
    if (observersForObsidianTTC.ContainsKey(id)) {
      var list = observersForObsidianTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForObsidianTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitDownStairsTTCEffect(IDownStairsTTCEffect effect) {
    if (observersForDownStairsTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IDownStairsTTCEffectObserver>(observers)) {
        observer.OnDownStairsTTCEffect(effect);
      }
    }
  }
  public void AddDownStairsTTCObserver(int id, IDownStairsTTCEffectObserver observer) {
    List<IDownStairsTTCEffectObserver> obsies;
    if (!observersForDownStairsTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IDownStairsTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForDownStairsTTC[id] = obsies;
  }

  public void RemoveDownStairsTTCObserver(int id, IDownStairsTTCEffectObserver observer) {
    if (observersForDownStairsTTC.ContainsKey(id)) {
      var list = observersForDownStairsTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForDownStairsTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitUpStairsTTCEffect(IUpStairsTTCEffect effect) {
    if (observersForUpStairsTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IUpStairsTTCEffectObserver>(observers)) {
        observer.OnUpStairsTTCEffect(effect);
      }
    }
  }
  public void AddUpStairsTTCObserver(int id, IUpStairsTTCEffectObserver observer) {
    List<IUpStairsTTCEffectObserver> obsies;
    if (!observersForUpStairsTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IUpStairsTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForUpStairsTTC[id] = obsies;
  }

  public void RemoveUpStairsTTCObserver(int id, IUpStairsTTCEffectObserver observer) {
    if (observersForUpStairsTTC.ContainsKey(id)) {
      var list = observersForUpStairsTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForUpStairsTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitWallTTCEffect(IWallTTCEffect effect) {
    if (observersForWallTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IWallTTCEffectObserver>(observers)) {
        observer.OnWallTTCEffect(effect);
      }
    }
  }
  public void AddWallTTCObserver(int id, IWallTTCEffectObserver observer) {
    List<IWallTTCEffectObserver> obsies;
    if (!observersForWallTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IWallTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForWallTTC[id] = obsies;
  }

  public void RemoveWallTTCObserver(int id, IWallTTCEffectObserver observer) {
    if (observersForWallTTC.ContainsKey(id)) {
      var list = observersForWallTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForWallTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitBloodTTCEffect(IBloodTTCEffect effect) {
    if (observersForBloodTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IBloodTTCEffectObserver>(observers)) {
        observer.OnBloodTTCEffect(effect);
      }
    }
  }
  public void AddBloodTTCObserver(int id, IBloodTTCEffectObserver observer) {
    List<IBloodTTCEffectObserver> obsies;
    if (!observersForBloodTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IBloodTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForBloodTTC[id] = obsies;
  }

  public void RemoveBloodTTCObserver(int id, IBloodTTCEffectObserver observer) {
    if (observersForBloodTTC.ContainsKey(id)) {
      var list = observersForBloodTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForBloodTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitRocksTTCEffect(IRocksTTCEffect effect) {
    if (observersForRocksTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IRocksTTCEffectObserver>(observers)) {
        observer.OnRocksTTCEffect(effect);
      }
    }
  }
  public void AddRocksTTCObserver(int id, IRocksTTCEffectObserver observer) {
    List<IRocksTTCEffectObserver> obsies;
    if (!observersForRocksTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IRocksTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForRocksTTC[id] = obsies;
  }

  public void RemoveRocksTTCObserver(int id, IRocksTTCEffectObserver observer) {
    if (observersForRocksTTC.ContainsKey(id)) {
      var list = observersForRocksTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForRocksTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitTreeTTCEffect(ITreeTTCEffect effect) {
    if (observersForTreeTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ITreeTTCEffectObserver>(observers)) {
        observer.OnTreeTTCEffect(effect);
      }
    }
  }
  public void AddTreeTTCObserver(int id, ITreeTTCEffectObserver observer) {
    List<ITreeTTCEffectObserver> obsies;
    if (!observersForTreeTTC.TryGetValue(id, out obsies)) {
      obsies = new List<ITreeTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForTreeTTC[id] = obsies;
  }

  public void RemoveTreeTTCObserver(int id, ITreeTTCEffectObserver observer) {
    if (observersForTreeTTC.ContainsKey(id)) {
      var list = observersForTreeTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForTreeTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitWaterTTCEffect(IWaterTTCEffect effect) {
    if (observersForWaterTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IWaterTTCEffectObserver>(observers)) {
        observer.OnWaterTTCEffect(effect);
      }
    }
  }
  public void AddWaterTTCObserver(int id, IWaterTTCEffectObserver observer) {
    List<IWaterTTCEffectObserver> obsies;
    if (!observersForWaterTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IWaterTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForWaterTTC[id] = obsies;
  }

  public void RemoveWaterTTCObserver(int id, IWaterTTCEffectObserver observer) {
    if (observersForWaterTTC.ContainsKey(id)) {
      var list = observersForWaterTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForWaterTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitFloorTTCEffect(IFloorTTCEffect effect) {
    if (observersForFloorTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IFloorTTCEffectObserver>(observers)) {
        observer.OnFloorTTCEffect(effect);
      }
    }
  }
  public void AddFloorTTCObserver(int id, IFloorTTCEffectObserver observer) {
    List<IFloorTTCEffectObserver> obsies;
    if (!observersForFloorTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IFloorTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForFloorTTC[id] = obsies;
  }

  public void RemoveFloorTTCObserver(int id, IFloorTTCEffectObserver observer) {
    if (observersForFloorTTC.ContainsKey(id)) {
      var list = observersForFloorTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForFloorTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitCaveWallTTCEffect(ICaveWallTTCEffect effect) {
    if (observersForCaveWallTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ICaveWallTTCEffectObserver>(observers)) {
        observer.OnCaveWallTTCEffect(effect);
      }
    }
  }
  public void AddCaveWallTTCObserver(int id, ICaveWallTTCEffectObserver observer) {
    List<ICaveWallTTCEffectObserver> obsies;
    if (!observersForCaveWallTTC.TryGetValue(id, out obsies)) {
      obsies = new List<ICaveWallTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForCaveWallTTC[id] = obsies;
  }

  public void RemoveCaveWallTTCObserver(int id, ICaveWallTTCEffectObserver observer) {
    if (observersForCaveWallTTC.ContainsKey(id)) {
      var list = observersForCaveWallTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForCaveWallTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitCaveTTCEffect(ICaveTTCEffect effect) {
    if (observersForCaveTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ICaveTTCEffectObserver>(observers)) {
        observer.OnCaveTTCEffect(effect);
      }
    }
  }
  public void AddCaveTTCObserver(int id, ICaveTTCEffectObserver observer) {
    List<ICaveTTCEffectObserver> obsies;
    if (!observersForCaveTTC.TryGetValue(id, out obsies)) {
      obsies = new List<ICaveTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForCaveTTC[id] = obsies;
  }

  public void RemoveCaveTTCObserver(int id, ICaveTTCEffectObserver observer) {
    if (observersForCaveTTC.ContainsKey(id)) {
      var list = observersForCaveTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForCaveTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitFallsTTCEffect(IFallsTTCEffect effect) {
    if (observersForFallsTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IFallsTTCEffectObserver>(observers)) {
        observer.OnFallsTTCEffect(effect);
      }
    }
  }
  public void AddFallsTTCObserver(int id, IFallsTTCEffectObserver observer) {
    List<IFallsTTCEffectObserver> obsies;
    if (!observersForFallsTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IFallsTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForFallsTTC[id] = obsies;
  }

  public void RemoveFallsTTCObserver(int id, IFallsTTCEffectObserver observer) {
    if (observersForFallsTTC.ContainsKey(id)) {
      var list = observersForFallsTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForFallsTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitObsidianFloorTTCEffect(IObsidianFloorTTCEffect effect) {
    if (observersForObsidianFloorTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IObsidianFloorTTCEffectObserver>(observers)) {
        observer.OnObsidianFloorTTCEffect(effect);
      }
    }
  }
  public void AddObsidianFloorTTCObserver(int id, IObsidianFloorTTCEffectObserver observer) {
    List<IObsidianFloorTTCEffectObserver> obsies;
    if (!observersForObsidianFloorTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IObsidianFloorTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForObsidianFloorTTC[id] = obsies;
  }

  public void RemoveObsidianFloorTTCObserver(int id, IObsidianFloorTTCEffectObserver observer) {
    if (observersForObsidianFloorTTC.ContainsKey(id)) {
      var list = observersForObsidianFloorTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForObsidianFloorTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitMagmaTTCEffect(IMagmaTTCEffect effect) {
    if (observersForMagmaTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IMagmaTTCEffectObserver>(observers)) {
        observer.OnMagmaTTCEffect(effect);
      }
    }
  }
  public void AddMagmaTTCObserver(int id, IMagmaTTCEffectObserver observer) {
    List<IMagmaTTCEffectObserver> obsies;
    if (!observersForMagmaTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IMagmaTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForMagmaTTC[id] = obsies;
  }

  public void RemoveMagmaTTCObserver(int id, IMagmaTTCEffectObserver observer) {
    if (observersForMagmaTTC.ContainsKey(id)) {
      var list = observersForMagmaTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForMagmaTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitCliffTTCEffect(ICliffTTCEffect effect) {
    if (observersForCliffTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ICliffTTCEffectObserver>(observers)) {
        observer.OnCliffTTCEffect(effect);
      }
    }
  }
  public void AddCliffTTCObserver(int id, ICliffTTCEffectObserver observer) {
    List<ICliffTTCEffectObserver> obsies;
    if (!observersForCliffTTC.TryGetValue(id, out obsies)) {
      obsies = new List<ICliffTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForCliffTTC[id] = obsies;
  }

  public void RemoveCliffTTCObserver(int id, ICliffTTCEffectObserver observer) {
    if (observersForCliffTTC.ContainsKey(id)) {
      var list = observersForCliffTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForCliffTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitRavaNestTTCEffect(IRavaNestTTCEffect effect) {
    if (observersForRavaNestTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IRavaNestTTCEffectObserver>(observers)) {
        observer.OnRavaNestTTCEffect(effect);
      }
    }
  }
  public void AddRavaNestTTCObserver(int id, IRavaNestTTCEffectObserver observer) {
    List<IRavaNestTTCEffectObserver> obsies;
    if (!observersForRavaNestTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IRavaNestTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForRavaNestTTC[id] = obsies;
  }

  public void RemoveRavaNestTTCObserver(int id, IRavaNestTTCEffectObserver observer) {
    if (observersForRavaNestTTC.ContainsKey(id)) {
      var list = observersForRavaNestTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForRavaNestTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitCliffLandingTTCEffect(ICliffLandingTTCEffect effect) {
    if (observersForCliffLandingTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ICliffLandingTTCEffectObserver>(observers)) {
        observer.OnCliffLandingTTCEffect(effect);
      }
    }
  }
  public void AddCliffLandingTTCObserver(int id, ICliffLandingTTCEffectObserver observer) {
    List<ICliffLandingTTCEffectObserver> obsies;
    if (!observersForCliffLandingTTC.TryGetValue(id, out obsies)) {
      obsies = new List<ICliffLandingTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForCliffLandingTTC[id] = obsies;
  }

  public void RemoveCliffLandingTTCObserver(int id, ICliffLandingTTCEffectObserver observer) {
    if (observersForCliffLandingTTC.ContainsKey(id)) {
      var list = observersForCliffLandingTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForCliffLandingTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitStoneTTCEffect(IStoneTTCEffect effect) {
    if (observersForStoneTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IStoneTTCEffectObserver>(observers)) {
        observer.OnStoneTTCEffect(effect);
      }
    }
  }
  public void AddStoneTTCObserver(int id, IStoneTTCEffectObserver observer) {
    List<IStoneTTCEffectObserver> obsies;
    if (!observersForStoneTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IStoneTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForStoneTTC[id] = obsies;
  }

  public void RemoveStoneTTCObserver(int id, IStoneTTCEffectObserver observer) {
    if (observersForStoneTTC.ContainsKey(id)) {
      var list = observersForStoneTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForStoneTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitGrassTTCEffect(IGrassTTCEffect effect) {
    if (observersForGrassTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IGrassTTCEffectObserver>(observers)) {
        observer.OnGrassTTCEffect(effect);
      }
    }
  }
  public void AddGrassTTCObserver(int id, IGrassTTCEffectObserver observer) {
    List<IGrassTTCEffectObserver> obsies;
    if (!observersForGrassTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IGrassTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForGrassTTC[id] = obsies;
  }

  public void RemoveGrassTTCObserver(int id, IGrassTTCEffectObserver observer) {
    if (observersForGrassTTC.ContainsKey(id)) {
      var list = observersForGrassTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForGrassTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitLevelEffect(ILevelEffect effect) {
    if (observersForLevel.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ILevelEffectObserver>(observers)) {
        observer.OnLevelEffect(effect);
      }
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

  public void visitSpeedRingEffect(ISpeedRingEffect effect) {
    if (observersForSpeedRing.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ISpeedRingEffectObserver>(observers)) {
        observer.OnSpeedRingEffect(effect);
      }
    }
  }
  public void AddSpeedRingObserver(int id, ISpeedRingEffectObserver observer) {
    List<ISpeedRingEffectObserver> obsies;
    if (!observersForSpeedRing.TryGetValue(id, out obsies)) {
      obsies = new List<ISpeedRingEffectObserver>();
    }
    obsies.Add(observer);
    observersForSpeedRing[id] = obsies;
  }

  public void RemoveSpeedRingObserver(int id, ISpeedRingEffectObserver observer) {
    if (observersForSpeedRing.ContainsKey(id)) {
      var list = observersForSpeedRing[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForSpeedRing.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitManaPotionEffect(IManaPotionEffect effect) {
    if (observersForManaPotion.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IManaPotionEffectObserver>(observers)) {
        observer.OnManaPotionEffect(effect);
      }
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

  public void visitWatEffect(IWatEffect effect) {
    if (observersForWat.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IWatEffectObserver>(observers)) {
        observer.OnWatEffect(effect);
      }
    }
  }
  public void AddWatObserver(int id, IWatEffectObserver observer) {
    List<IWatEffectObserver> obsies;
    if (!observersForWat.TryGetValue(id, out obsies)) {
      obsies = new List<IWatEffectObserver>();
    }
    obsies.Add(observer);
    observersForWat[id] = obsies;
  }

  public void RemoveWatObserver(int id, IWatEffectObserver observer) {
    if (observersForWat.ContainsKey(id)) {
      var list = observersForWat[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForWat.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitIPreActingUCWeakMutBunchEffect(IIPreActingUCWeakMutBunchEffect effect) {
    if (observersForIPreActingUCWeakMutBunch.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IIPreActingUCWeakMutBunchEffectObserver>(observers)) {
        observer.OnIPreActingUCWeakMutBunchEffect(effect);
      }
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

  public void visitIPostActingUCWeakMutBunchEffect(IIPostActingUCWeakMutBunchEffect effect) {
    if (observersForIPostActingUCWeakMutBunch.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IIPostActingUCWeakMutBunchEffectObserver>(observers)) {
        observer.OnIPostActingUCWeakMutBunchEffect(effect);
      }
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

  public void visitIImpulseStrongMutBunchEffect(IIImpulseStrongMutBunchEffect effect) {
    if (observersForIImpulseStrongMutBunch.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IIImpulseStrongMutBunchEffectObserver>(observers)) {
        observer.OnIImpulseStrongMutBunchEffect(effect);
      }
    }
  }
  public void AddIImpulseStrongMutBunchObserver(int id, IIImpulseStrongMutBunchEffectObserver observer) {
    List<IIImpulseStrongMutBunchEffectObserver> obsies;
    if (!observersForIImpulseStrongMutBunch.TryGetValue(id, out obsies)) {
      obsies = new List<IIImpulseStrongMutBunchEffectObserver>();
    }
    obsies.Add(observer);
    observersForIImpulseStrongMutBunch[id] = obsies;
  }

  public void RemoveIImpulseStrongMutBunchObserver(int id, IIImpulseStrongMutBunchEffectObserver observer) {
    if (observersForIImpulseStrongMutBunch.ContainsKey(id)) {
      var list = observersForIImpulseStrongMutBunch[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForIImpulseStrongMutBunch.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitIItemStrongMutBunchEffect(IIItemStrongMutBunchEffect effect) {
    if (observersForIItemStrongMutBunch.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IIItemStrongMutBunchEffectObserver>(observers)) {
        observer.OnIItemStrongMutBunchEffect(effect);
      }
    }
  }
  public void AddIItemStrongMutBunchObserver(int id, IIItemStrongMutBunchEffectObserver observer) {
    List<IIItemStrongMutBunchEffectObserver> obsies;
    if (!observersForIItemStrongMutBunch.TryGetValue(id, out obsies)) {
      obsies = new List<IIItemStrongMutBunchEffectObserver>();
    }
    obsies.Add(observer);
    observersForIItemStrongMutBunch[id] = obsies;
  }

  public void RemoveIItemStrongMutBunchObserver(int id, IIItemStrongMutBunchEffectObserver observer) {
    if (observersForIItemStrongMutBunch.ContainsKey(id)) {
      var list = observersForIItemStrongMutBunch[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForIItemStrongMutBunch.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitItemTTCEffect(IItemTTCEffect effect) {
    if (observersForItemTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IItemTTCEffectObserver>(observers)) {
        observer.OnItemTTCEffect(effect);
      }
    }
  }
  public void AddItemTTCObserver(int id, IItemTTCEffectObserver observer) {
    List<IItemTTCEffectObserver> obsies;
    if (!observersForItemTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IItemTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForItemTTC[id] = obsies;
  }

  public void RemoveItemTTCObserver(int id, IItemTTCEffectObserver observer) {
    if (observersForItemTTC.ContainsKey(id)) {
      var list = observersForItemTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForItemTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitHealthPotionEffect(IHealthPotionEffect effect) {
    if (observersForHealthPotion.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IHealthPotionEffectObserver>(observers)) {
        observer.OnHealthPotionEffect(effect);
      }
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

  public void visitGlaiveEffect(IGlaiveEffect effect) {
    if (observersForGlaive.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IGlaiveEffectObserver>(observers)) {
        observer.OnGlaiveEffect(effect);
      }
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

  public void visitSlowRodEffect(ISlowRodEffect effect) {
    if (observersForSlowRod.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ISlowRodEffectObserver>(observers)) {
        observer.OnSlowRodEffect(effect);
      }
    }
  }
  public void AddSlowRodObserver(int id, ISlowRodEffectObserver observer) {
    List<ISlowRodEffectObserver> obsies;
    if (!observersForSlowRod.TryGetValue(id, out obsies)) {
      obsies = new List<ISlowRodEffectObserver>();
    }
    obsies.Add(observer);
    observersForSlowRod[id] = obsies;
  }

  public void RemoveSlowRodObserver(int id, ISlowRodEffectObserver observer) {
    if (observersForSlowRod.ContainsKey(id)) {
      var list = observersForSlowRod[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForSlowRod.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitExplosionRodEffect(IExplosionRodEffect effect) {
    if (observersForExplosionRod.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IExplosionRodEffectObserver>(observers)) {
        observer.OnExplosionRodEffect(effect);
      }
    }
  }
  public void AddExplosionRodObserver(int id, IExplosionRodEffectObserver observer) {
    List<IExplosionRodEffectObserver> obsies;
    if (!observersForExplosionRod.TryGetValue(id, out obsies)) {
      obsies = new List<IExplosionRodEffectObserver>();
    }
    obsies.Add(observer);
    observersForExplosionRod[id] = obsies;
  }

  public void RemoveExplosionRodObserver(int id, IExplosionRodEffectObserver observer) {
    if (observersForExplosionRod.ContainsKey(id)) {
      var list = observersForExplosionRod[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForExplosionRod.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitBlazeRodEffect(IBlazeRodEffect effect) {
    if (observersForBlazeRod.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IBlazeRodEffectObserver>(observers)) {
        observer.OnBlazeRodEffect(effect);
      }
    }
  }
  public void AddBlazeRodObserver(int id, IBlazeRodEffectObserver observer) {
    List<IBlazeRodEffectObserver> obsies;
    if (!observersForBlazeRod.TryGetValue(id, out obsies)) {
      obsies = new List<IBlazeRodEffectObserver>();
    }
    obsies.Add(observer);
    observersForBlazeRod[id] = obsies;
  }

  public void RemoveBlazeRodObserver(int id, IBlazeRodEffectObserver observer) {
    if (observersForBlazeRod.ContainsKey(id)) {
      var list = observersForBlazeRod[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForBlazeRod.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitBlastRodEffect(IBlastRodEffect effect) {
    if (observersForBlastRod.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IBlastRodEffectObserver>(observers)) {
        observer.OnBlastRodEffect(effect);
      }
    }
  }
  public void AddBlastRodObserver(int id, IBlastRodEffectObserver observer) {
    List<IBlastRodEffectObserver> obsies;
    if (!observersForBlastRod.TryGetValue(id, out obsies)) {
      obsies = new List<IBlastRodEffectObserver>();
    }
    obsies.Add(observer);
    observersForBlastRod[id] = obsies;
  }

  public void RemoveBlastRodObserver(int id, IBlastRodEffectObserver observer) {
    if (observersForBlastRod.ContainsKey(id)) {
      var list = observersForBlastRod[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForBlastRod.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitArmorEffect(IArmorEffect effect) {
    if (observersForArmor.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IArmorEffectObserver>(observers)) {
        observer.OnArmorEffect(effect);
      }
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

  public void visitVolcaetusLevelControllerEffect(IVolcaetusLevelControllerEffect effect) {
    if (observersForVolcaetusLevelController.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IVolcaetusLevelControllerEffectObserver>(observers)) {
        observer.OnVolcaetusLevelControllerEffect(effect);
      }
    }
  }
  public void AddVolcaetusLevelControllerObserver(int id, IVolcaetusLevelControllerEffectObserver observer) {
    List<IVolcaetusLevelControllerEffectObserver> obsies;
    if (!observersForVolcaetusLevelController.TryGetValue(id, out obsies)) {
      obsies = new List<IVolcaetusLevelControllerEffectObserver>();
    }
    obsies.Add(observer);
    observersForVolcaetusLevelController[id] = obsies;
  }

  public void RemoveVolcaetusLevelControllerObserver(int id, IVolcaetusLevelControllerEffectObserver observer) {
    if (observersForVolcaetusLevelController.ContainsKey(id)) {
      var list = observersForVolcaetusLevelController[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForVolcaetusLevelController.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitTutorial2LevelControllerEffect(ITutorial2LevelControllerEffect effect) {
    if (observersForTutorial2LevelController.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ITutorial2LevelControllerEffectObserver>(observers)) {
        observer.OnTutorial2LevelControllerEffect(effect);
      }
    }
  }
  public void AddTutorial2LevelControllerObserver(int id, ITutorial2LevelControllerEffectObserver observer) {
    List<ITutorial2LevelControllerEffectObserver> obsies;
    if (!observersForTutorial2LevelController.TryGetValue(id, out obsies)) {
      obsies = new List<ITutorial2LevelControllerEffectObserver>();
    }
    obsies.Add(observer);
    observersForTutorial2LevelController[id] = obsies;
  }

  public void RemoveTutorial2LevelControllerObserver(int id, ITutorial2LevelControllerEffectObserver observer) {
    if (observersForTutorial2LevelController.ContainsKey(id)) {
      var list = observersForTutorial2LevelController[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForTutorial2LevelController.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitTutorial1LevelControllerEffect(ITutorial1LevelControllerEffect effect) {
    if (observersForTutorial1LevelController.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ITutorial1LevelControllerEffectObserver>(observers)) {
        observer.OnTutorial1LevelControllerEffect(effect);
      }
    }
  }
  public void AddTutorial1LevelControllerObserver(int id, ITutorial1LevelControllerEffectObserver observer) {
    List<ITutorial1LevelControllerEffectObserver> obsies;
    if (!observersForTutorial1LevelController.TryGetValue(id, out obsies)) {
      obsies = new List<ITutorial1LevelControllerEffectObserver>();
    }
    obsies.Add(observer);
    observersForTutorial1LevelController[id] = obsies;
  }

  public void RemoveTutorial1LevelControllerObserver(int id, ITutorial1LevelControllerEffectObserver observer) {
    if (observersForTutorial1LevelController.ContainsKey(id)) {
      var list = observersForTutorial1LevelController[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForTutorial1LevelController.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitRetreatLevelControllerEffect(IRetreatLevelControllerEffect effect) {
    if (observersForRetreatLevelController.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IRetreatLevelControllerEffectObserver>(observers)) {
        observer.OnRetreatLevelControllerEffect(effect);
      }
    }
  }
  public void AddRetreatLevelControllerObserver(int id, IRetreatLevelControllerEffectObserver observer) {
    List<IRetreatLevelControllerEffectObserver> obsies;
    if (!observersForRetreatLevelController.TryGetValue(id, out obsies)) {
      obsies = new List<IRetreatLevelControllerEffectObserver>();
    }
    obsies.Add(observer);
    observersForRetreatLevelController[id] = obsies;
  }

  public void RemoveRetreatLevelControllerObserver(int id, IRetreatLevelControllerEffectObserver observer) {
    if (observersForRetreatLevelController.ContainsKey(id)) {
      var list = observersForRetreatLevelController[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForRetreatLevelController.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitSotaventoLevelControllerEffect(ISotaventoLevelControllerEffect effect) {
    if (observersForSotaventoLevelController.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ISotaventoLevelControllerEffectObserver>(observers)) {
        observer.OnSotaventoLevelControllerEffect(effect);
      }
    }
  }
  public void AddSotaventoLevelControllerObserver(int id, ISotaventoLevelControllerEffectObserver observer) {
    List<ISotaventoLevelControllerEffectObserver> obsies;
    if (!observersForSotaventoLevelController.TryGetValue(id, out obsies)) {
      obsies = new List<ISotaventoLevelControllerEffectObserver>();
    }
    obsies.Add(observer);
    observersForSotaventoLevelController[id] = obsies;
  }

  public void RemoveSotaventoLevelControllerObserver(int id, ISotaventoLevelControllerEffectObserver observer) {
    if (observersForSotaventoLevelController.ContainsKey(id)) {
      var list = observersForSotaventoLevelController[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForSotaventoLevelController.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitNestLevelControllerEffect(INestLevelControllerEffect effect) {
    if (observersForNestLevelController.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<INestLevelControllerEffectObserver>(observers)) {
        observer.OnNestLevelControllerEffect(effect);
      }
    }
  }
  public void AddNestLevelControllerObserver(int id, INestLevelControllerEffectObserver observer) {
    List<INestLevelControllerEffectObserver> obsies;
    if (!observersForNestLevelController.TryGetValue(id, out obsies)) {
      obsies = new List<INestLevelControllerEffectObserver>();
    }
    obsies.Add(observer);
    observersForNestLevelController[id] = obsies;
  }

  public void RemoveNestLevelControllerObserver(int id, INestLevelControllerEffectObserver observer) {
    if (observersForNestLevelController.ContainsKey(id)) {
      var list = observersForNestLevelController[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForNestLevelController.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitLakeLevelControllerEffect(ILakeLevelControllerEffect effect) {
    if (observersForLakeLevelController.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ILakeLevelControllerEffectObserver>(observers)) {
        observer.OnLakeLevelControllerEffect(effect);
      }
    }
  }
  public void AddLakeLevelControllerObserver(int id, ILakeLevelControllerEffectObserver observer) {
    List<ILakeLevelControllerEffectObserver> obsies;
    if (!observersForLakeLevelController.TryGetValue(id, out obsies)) {
      obsies = new List<ILakeLevelControllerEffectObserver>();
    }
    obsies.Add(observer);
    observersForLakeLevelController[id] = obsies;
  }

  public void RemoveLakeLevelControllerObserver(int id, ILakeLevelControllerEffectObserver observer) {
    if (observersForLakeLevelController.ContainsKey(id)) {
      var list = observersForLakeLevelController[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForLakeLevelController.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitEmberDeepLevelLinkerTTCEffect(IEmberDeepLevelLinkerTTCEffect effect) {
    if (observersForEmberDeepLevelLinkerTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IEmberDeepLevelLinkerTTCEffectObserver>(observers)) {
        observer.OnEmberDeepLevelLinkerTTCEffect(effect);
      }
    }
  }
  public void AddEmberDeepLevelLinkerTTCObserver(int id, IEmberDeepLevelLinkerTTCEffectObserver observer) {
    List<IEmberDeepLevelLinkerTTCEffectObserver> obsies;
    if (!observersForEmberDeepLevelLinkerTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IEmberDeepLevelLinkerTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForEmberDeepLevelLinkerTTC[id] = obsies;
  }

  public void RemoveEmberDeepLevelLinkerTTCObserver(int id, IEmberDeepLevelLinkerTTCEffectObserver observer) {
    if (observersForEmberDeepLevelLinkerTTC.ContainsKey(id)) {
      var list = observersForEmberDeepLevelLinkerTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForEmberDeepLevelLinkerTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitDirtRoadLevelControllerEffect(IDirtRoadLevelControllerEffect effect) {
    if (observersForDirtRoadLevelController.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IDirtRoadLevelControllerEffectObserver>(observers)) {
        observer.OnDirtRoadLevelControllerEffect(effect);
      }
    }
  }
  public void AddDirtRoadLevelControllerObserver(int id, IDirtRoadLevelControllerEffectObserver observer) {
    List<IDirtRoadLevelControllerEffectObserver> obsies;
    if (!observersForDirtRoadLevelController.TryGetValue(id, out obsies)) {
      obsies = new List<IDirtRoadLevelControllerEffectObserver>();
    }
    obsies.Add(observer);
    observersForDirtRoadLevelController[id] = obsies;
  }

  public void RemoveDirtRoadLevelControllerObserver(int id, IDirtRoadLevelControllerEffectObserver observer) {
    if (observersForDirtRoadLevelController.ContainsKey(id)) {
      var list = observersForDirtRoadLevelController[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForDirtRoadLevelController.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitCaveLevelControllerEffect(ICaveLevelControllerEffect effect) {
    if (observersForCaveLevelController.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ICaveLevelControllerEffectObserver>(observers)) {
        observer.OnCaveLevelControllerEffect(effect);
      }
    }
  }
  public void AddCaveLevelControllerObserver(int id, ICaveLevelControllerEffectObserver observer) {
    List<ICaveLevelControllerEffectObserver> obsies;
    if (!observersForCaveLevelController.TryGetValue(id, out obsies)) {
      obsies = new List<ICaveLevelControllerEffectObserver>();
    }
    obsies.Add(observer);
    observersForCaveLevelController[id] = obsies;
  }

  public void RemoveCaveLevelControllerObserver(int id, ICaveLevelControllerEffectObserver observer) {
    if (observersForCaveLevelController.ContainsKey(id)) {
      var list = observersForCaveLevelController[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForCaveLevelController.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitBridgesLevelControllerEffect(IBridgesLevelControllerEffect effect) {
    if (observersForBridgesLevelController.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IBridgesLevelControllerEffectObserver>(observers)) {
        observer.OnBridgesLevelControllerEffect(effect);
      }
    }
  }
  public void AddBridgesLevelControllerObserver(int id, IBridgesLevelControllerEffectObserver observer) {
    List<IBridgesLevelControllerEffectObserver> obsies;
    if (!observersForBridgesLevelController.TryGetValue(id, out obsies)) {
      obsies = new List<IBridgesLevelControllerEffectObserver>();
    }
    obsies.Add(observer);
    observersForBridgesLevelController[id] = obsies;
  }

  public void RemoveBridgesLevelControllerObserver(int id, IBridgesLevelControllerEffectObserver observer) {
    if (observersForBridgesLevelController.ContainsKey(id)) {
      var list = observersForBridgesLevelController[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForBridgesLevelController.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitAncientTownLevelControllerEffect(IAncientTownLevelControllerEffect effect) {
    if (observersForAncientTownLevelController.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IAncientTownLevelControllerEffectObserver>(observers)) {
        observer.OnAncientTownLevelControllerEffect(effect);
      }
    }
  }
  public void AddAncientTownLevelControllerObserver(int id, IAncientTownLevelControllerEffectObserver observer) {
    List<IAncientTownLevelControllerEffectObserver> obsies;
    if (!observersForAncientTownLevelController.TryGetValue(id, out obsies)) {
      obsies = new List<IAncientTownLevelControllerEffectObserver>();
    }
    obsies.Add(observer);
    observersForAncientTownLevelController[id] = obsies;
  }

  public void RemoveAncientTownLevelControllerObserver(int id, IAncientTownLevelControllerEffectObserver observer) {
    if (observersForAncientTownLevelController.ContainsKey(id)) {
      var list = observersForAncientTownLevelController[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForAncientTownLevelController.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitSquareCaveLevelControllerEffect(ISquareCaveLevelControllerEffect effect) {
    if (observersForSquareCaveLevelController.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ISquareCaveLevelControllerEffectObserver>(observers)) {
        observer.OnSquareCaveLevelControllerEffect(effect);
      }
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

  public void visitRavashrikeLevelControllerEffect(IRavashrikeLevelControllerEffect effect) {
    if (observersForRavashrikeLevelController.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IRavashrikeLevelControllerEffectObserver>(observers)) {
        observer.OnRavashrikeLevelControllerEffect(effect);
      }
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

  public void visitPentagonalCaveLevelControllerEffect(IPentagonalCaveLevelControllerEffect effect) {
    if (observersForPentagonalCaveLevelController.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IPentagonalCaveLevelControllerEffectObserver>(observers)) {
        observer.OnPentagonalCaveLevelControllerEffect(effect);
      }
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

  public void visitIncendianFallsLevelLinkerTTCEffect(IIncendianFallsLevelLinkerTTCEffect effect) {
    if (observersForIncendianFallsLevelLinkerTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IIncendianFallsLevelLinkerTTCEffectObserver>(observers)) {
        observer.OnIncendianFallsLevelLinkerTTCEffect(effect);
      }
    }
  }
  public void AddIncendianFallsLevelLinkerTTCObserver(int id, IIncendianFallsLevelLinkerTTCEffectObserver observer) {
    List<IIncendianFallsLevelLinkerTTCEffectObserver> obsies;
    if (!observersForIncendianFallsLevelLinkerTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IIncendianFallsLevelLinkerTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForIncendianFallsLevelLinkerTTC[id] = obsies;
  }

  public void RemoveIncendianFallsLevelLinkerTTCObserver(int id, IIncendianFallsLevelLinkerTTCEffectObserver observer) {
    if (observersForIncendianFallsLevelLinkerTTC.ContainsKey(id)) {
      var list = observersForIncendianFallsLevelLinkerTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForIncendianFallsLevelLinkerTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitCliffLevelControllerEffect(ICliffLevelControllerEffect effect) {
    if (observersForCliffLevelController.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ICliffLevelControllerEffectObserver>(observers)) {
        observer.OnCliffLevelControllerEffect(effect);
      }
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

  public void visitPreGauntletLevelControllerEffect(IPreGauntletLevelControllerEffect effect) {
    if (observersForPreGauntletLevelController.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IPreGauntletLevelControllerEffectObserver>(observers)) {
        observer.OnPreGauntletLevelControllerEffect(effect);
      }
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

  public void visitGauntletLevelControllerEffect(IGauntletLevelControllerEffect effect) {
    if (observersForGauntletLevelController.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IGauntletLevelControllerEffectObserver>(observers)) {
        observer.OnGauntletLevelControllerEffect(effect);
      }
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

  public void visitRavaArcanaLevelLinkerTTCEffect(IRavaArcanaLevelLinkerTTCEffect effect) {
    if (observersForRavaArcanaLevelLinkerTTC.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IRavaArcanaLevelLinkerTTCEffectObserver>(observers)) {
        observer.OnRavaArcanaLevelLinkerTTCEffect(effect);
      }
    }
  }
  public void AddRavaArcanaLevelLinkerTTCObserver(int id, IRavaArcanaLevelLinkerTTCEffectObserver observer) {
    List<IRavaArcanaLevelLinkerTTCEffectObserver> obsies;
    if (!observersForRavaArcanaLevelLinkerTTC.TryGetValue(id, out obsies)) {
      obsies = new List<IRavaArcanaLevelLinkerTTCEffectObserver>();
    }
    obsies.Add(observer);
    observersForRavaArcanaLevelLinkerTTC[id] = obsies;
  }

  public void RemoveRavaArcanaLevelLinkerTTCObserver(int id, IRavaArcanaLevelLinkerTTCEffectObserver observer) {
    if (observersForRavaArcanaLevelLinkerTTC.ContainsKey(id)) {
      var list = observersForRavaArcanaLevelLinkerTTC[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForRavaArcanaLevelLinkerTTC.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitJumpingCaveLevelControllerEffect(IJumpingCaveLevelControllerEffect effect) {
    if (observersForJumpingCaveLevelController.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IJumpingCaveLevelControllerEffectObserver>(observers)) {
        observer.OnJumpingCaveLevelControllerEffect(effect);
      }
    }
  }
  public void AddJumpingCaveLevelControllerObserver(int id, IJumpingCaveLevelControllerEffectObserver observer) {
    List<IJumpingCaveLevelControllerEffectObserver> obsies;
    if (!observersForJumpingCaveLevelController.TryGetValue(id, out obsies)) {
      obsies = new List<IJumpingCaveLevelControllerEffectObserver>();
    }
    obsies.Add(observer);
    observersForJumpingCaveLevelController[id] = obsies;
  }

  public void RemoveJumpingCaveLevelControllerObserver(int id, IJumpingCaveLevelControllerEffectObserver observer) {
    if (observersForJumpingCaveLevelController.ContainsKey(id)) {
      var list = observersForJumpingCaveLevelController[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForJumpingCaveLevelController.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitCommEffect(ICommEffect effect) {
    if (observersForComm.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ICommEffectObserver>(observers)) {
        observer.OnCommEffect(effect);
      }
    }
  }
  public void AddCommObserver(int id, ICommEffectObserver observer) {
    List<ICommEffectObserver> obsies;
    if (!observersForComm.TryGetValue(id, out obsies)) {
      obsies = new List<ICommEffectObserver>();
    }
    obsies.Add(observer);
    observersForComm[id] = obsies;
  }

  public void RemoveCommObserver(int id, ICommEffectObserver observer) {
    if (observersForComm.ContainsKey(id)) {
      var list = observersForComm[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForComm.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }

  public void visitGameEffect(IGameEffect effect) {
    if (observersForGame.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IGameEffectObserver>(observers)) {
        observer.OnGameEffect(effect);
      }
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

    public void visitCommMutListEffect(ICommMutListEffect effect) {
      if (observersForCommMutList.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ICommMutListEffectObserver>(observers)) {
          observer.OnCommMutListEffect(effect);
        }
      }
    }
    public void AddCommMutListObserver(int id, ICommMutListEffectObserver observer) {
      List<ICommMutListEffectObserver> obsies;
      if (!observersForCommMutList.TryGetValue(id, out obsies)) {
        obsies = new List<ICommMutListEffectObserver>();
      }
      obsies.Add(observer);
      observersForCommMutList[id] = obsies;
    }
    public void RemoveCommMutListObserver(int id, ICommMutListEffectObserver observer) {
      if (observersForCommMutList.ContainsKey(id)) {
        var map = observersForCommMutList[id];
        map.Remove(observer);
        if (map.Count == 0) {
          observersForCommMutList.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitLocationMutListEffect(ILocationMutListEffect effect) {
      if (observersForLocationMutList.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ILocationMutListEffectObserver>(observers)) {
          observer.OnLocationMutListEffect(effect);
        }
      }
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
        var map = observersForLocationMutList[id];
        map.Remove(observer);
        if (map.Count == 0) {
          observersForLocationMutList.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitIRequestMutListEffect(IIRequestMutListEffect effect) {
      if (observersForIRequestMutList.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IIRequestMutListEffectObserver>(observers)) {
          observer.OnIRequestMutListEffect(effect);
        }
      }
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
        var map = observersForIRequestMutList[id];
        map.Remove(observer);
        if (map.Count == 0) {
          observersForIRequestMutList.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitLevelMutSetEffect(ILevelMutSetEffect effect) {
      if (observersForLevelMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ILevelMutSetEffectObserver>(observers)) {
          observer.OnLevelMutSetEffect(effect);
        }
      }
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

    public void visitManaPotionStrongMutSetEffect(IManaPotionStrongMutSetEffect effect) {
      if (observersForManaPotionStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IManaPotionStrongMutSetEffectObserver>(observers)) {
          observer.OnManaPotionStrongMutSetEffect(effect);
        }
      }
    }
    public void AddManaPotionStrongMutSetObserver(int id, IManaPotionStrongMutSetEffectObserver observer) {
      List<IManaPotionStrongMutSetEffectObserver> obsies;
      if (!observersForManaPotionStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IManaPotionStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForManaPotionStrongMutSet[id] = obsies;
    }
    public void RemoveManaPotionStrongMutSetObserver(int id, IManaPotionStrongMutSetEffectObserver observer) {
      if (observersForManaPotionStrongMutSet.ContainsKey(id)) {
        var list = observersForManaPotionStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForManaPotionStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitHealthPotionStrongMutSetEffect(IHealthPotionStrongMutSetEffect effect) {
      if (observersForHealthPotionStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IHealthPotionStrongMutSetEffectObserver>(observers)) {
          observer.OnHealthPotionStrongMutSetEffect(effect);
        }
      }
    }
    public void AddHealthPotionStrongMutSetObserver(int id, IHealthPotionStrongMutSetEffectObserver observer) {
      List<IHealthPotionStrongMutSetEffectObserver> obsies;
      if (!observersForHealthPotionStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IHealthPotionStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForHealthPotionStrongMutSet[id] = obsies;
    }
    public void RemoveHealthPotionStrongMutSetObserver(int id, IHealthPotionStrongMutSetEffectObserver observer) {
      if (observersForHealthPotionStrongMutSet.ContainsKey(id)) {
        var list = observersForHealthPotionStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForHealthPotionStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitSpeedRingStrongMutSetEffect(ISpeedRingStrongMutSetEffect effect) {
      if (observersForSpeedRingStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ISpeedRingStrongMutSetEffectObserver>(observers)) {
          observer.OnSpeedRingStrongMutSetEffect(effect);
        }
      }
    }
    public void AddSpeedRingStrongMutSetObserver(int id, ISpeedRingStrongMutSetEffectObserver observer) {
      List<ISpeedRingStrongMutSetEffectObserver> obsies;
      if (!observersForSpeedRingStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ISpeedRingStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForSpeedRingStrongMutSet[id] = obsies;
    }
    public void RemoveSpeedRingStrongMutSetObserver(int id, ISpeedRingStrongMutSetEffectObserver observer) {
      if (observersForSpeedRingStrongMutSet.ContainsKey(id)) {
        var list = observersForSpeedRingStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForSpeedRingStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitGlaiveStrongMutSetEffect(IGlaiveStrongMutSetEffect effect) {
      if (observersForGlaiveStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IGlaiveStrongMutSetEffectObserver>(observers)) {
          observer.OnGlaiveStrongMutSetEffect(effect);
        }
      }
    }
    public void AddGlaiveStrongMutSetObserver(int id, IGlaiveStrongMutSetEffectObserver observer) {
      List<IGlaiveStrongMutSetEffectObserver> obsies;
      if (!observersForGlaiveStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IGlaiveStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForGlaiveStrongMutSet[id] = obsies;
    }
    public void RemoveGlaiveStrongMutSetObserver(int id, IGlaiveStrongMutSetEffectObserver observer) {
      if (observersForGlaiveStrongMutSet.ContainsKey(id)) {
        var list = observersForGlaiveStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForGlaiveStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitSlowRodStrongMutSetEffect(ISlowRodStrongMutSetEffect effect) {
      if (observersForSlowRodStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ISlowRodStrongMutSetEffectObserver>(observers)) {
          observer.OnSlowRodStrongMutSetEffect(effect);
        }
      }
    }
    public void AddSlowRodStrongMutSetObserver(int id, ISlowRodStrongMutSetEffectObserver observer) {
      List<ISlowRodStrongMutSetEffectObserver> obsies;
      if (!observersForSlowRodStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ISlowRodStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForSlowRodStrongMutSet[id] = obsies;
    }
    public void RemoveSlowRodStrongMutSetObserver(int id, ISlowRodStrongMutSetEffectObserver observer) {
      if (observersForSlowRodStrongMutSet.ContainsKey(id)) {
        var list = observersForSlowRodStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForSlowRodStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitExplosionRodStrongMutSetEffect(IExplosionRodStrongMutSetEffect effect) {
      if (observersForExplosionRodStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IExplosionRodStrongMutSetEffectObserver>(observers)) {
          observer.OnExplosionRodStrongMutSetEffect(effect);
        }
      }
    }
    public void AddExplosionRodStrongMutSetObserver(int id, IExplosionRodStrongMutSetEffectObserver observer) {
      List<IExplosionRodStrongMutSetEffectObserver> obsies;
      if (!observersForExplosionRodStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IExplosionRodStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForExplosionRodStrongMutSet[id] = obsies;
    }
    public void RemoveExplosionRodStrongMutSetObserver(int id, IExplosionRodStrongMutSetEffectObserver observer) {
      if (observersForExplosionRodStrongMutSet.ContainsKey(id)) {
        var list = observersForExplosionRodStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForExplosionRodStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitBlazeRodStrongMutSetEffect(IBlazeRodStrongMutSetEffect effect) {
      if (observersForBlazeRodStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IBlazeRodStrongMutSetEffectObserver>(observers)) {
          observer.OnBlazeRodStrongMutSetEffect(effect);
        }
      }
    }
    public void AddBlazeRodStrongMutSetObserver(int id, IBlazeRodStrongMutSetEffectObserver observer) {
      List<IBlazeRodStrongMutSetEffectObserver> obsies;
      if (!observersForBlazeRodStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IBlazeRodStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForBlazeRodStrongMutSet[id] = obsies;
    }
    public void RemoveBlazeRodStrongMutSetObserver(int id, IBlazeRodStrongMutSetEffectObserver observer) {
      if (observersForBlazeRodStrongMutSet.ContainsKey(id)) {
        var list = observersForBlazeRodStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForBlazeRodStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitBlastRodStrongMutSetEffect(IBlastRodStrongMutSetEffect effect) {
      if (observersForBlastRodStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IBlastRodStrongMutSetEffectObserver>(observers)) {
          observer.OnBlastRodStrongMutSetEffect(effect);
        }
      }
    }
    public void AddBlastRodStrongMutSetObserver(int id, IBlastRodStrongMutSetEffectObserver observer) {
      List<IBlastRodStrongMutSetEffectObserver> obsies;
      if (!observersForBlastRodStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IBlastRodStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForBlastRodStrongMutSet[id] = obsies;
    }
    public void RemoveBlastRodStrongMutSetObserver(int id, IBlastRodStrongMutSetEffectObserver observer) {
      if (observersForBlastRodStrongMutSet.ContainsKey(id)) {
        var list = observersForBlastRodStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForBlastRodStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitArmorStrongMutSetEffect(IArmorStrongMutSetEffect effect) {
      if (observersForArmorStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IArmorStrongMutSetEffectObserver>(observers)) {
          observer.OnArmorStrongMutSetEffect(effect);
        }
      }
    }
    public void AddArmorStrongMutSetObserver(int id, IArmorStrongMutSetEffectObserver observer) {
      List<IArmorStrongMutSetEffectObserver> obsies;
      if (!observersForArmorStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IArmorStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForArmorStrongMutSet[id] = obsies;
    }
    public void RemoveArmorStrongMutSetObserver(int id, IArmorStrongMutSetEffectObserver observer) {
      if (observersForArmorStrongMutSet.ContainsKey(id)) {
        var list = observersForArmorStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForArmorStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitHoldPositionImpulseStrongMutSetEffect(IHoldPositionImpulseStrongMutSetEffect effect) {
      if (observersForHoldPositionImpulseStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IHoldPositionImpulseStrongMutSetEffectObserver>(observers)) {
          observer.OnHoldPositionImpulseStrongMutSetEffect(effect);
        }
      }
    }
    public void AddHoldPositionImpulseStrongMutSetObserver(int id, IHoldPositionImpulseStrongMutSetEffectObserver observer) {
      List<IHoldPositionImpulseStrongMutSetEffectObserver> obsies;
      if (!observersForHoldPositionImpulseStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IHoldPositionImpulseStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForHoldPositionImpulseStrongMutSet[id] = obsies;
    }
    public void RemoveHoldPositionImpulseStrongMutSetObserver(int id, IHoldPositionImpulseStrongMutSetEffectObserver observer) {
      if (observersForHoldPositionImpulseStrongMutSet.ContainsKey(id)) {
        var list = observersForHoldPositionImpulseStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForHoldPositionImpulseStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitTemporaryCloneImpulseStrongMutSetEffect(ITemporaryCloneImpulseStrongMutSetEffect effect) {
      if (observersForTemporaryCloneImpulseStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ITemporaryCloneImpulseStrongMutSetEffectObserver>(observers)) {
          observer.OnTemporaryCloneImpulseStrongMutSetEffect(effect);
        }
      }
    }
    public void AddTemporaryCloneImpulseStrongMutSetObserver(int id, ITemporaryCloneImpulseStrongMutSetEffectObserver observer) {
      List<ITemporaryCloneImpulseStrongMutSetEffectObserver> obsies;
      if (!observersForTemporaryCloneImpulseStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ITemporaryCloneImpulseStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForTemporaryCloneImpulseStrongMutSet[id] = obsies;
    }
    public void RemoveTemporaryCloneImpulseStrongMutSetObserver(int id, ITemporaryCloneImpulseStrongMutSetEffectObserver observer) {
      if (observersForTemporaryCloneImpulseStrongMutSet.ContainsKey(id)) {
        var list = observersForTemporaryCloneImpulseStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForTemporaryCloneImpulseStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitSummonImpulseStrongMutSetEffect(ISummonImpulseStrongMutSetEffect effect) {
      if (observersForSummonImpulseStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ISummonImpulseStrongMutSetEffectObserver>(observers)) {
          observer.OnSummonImpulseStrongMutSetEffect(effect);
        }
      }
    }
    public void AddSummonImpulseStrongMutSetObserver(int id, ISummonImpulseStrongMutSetEffectObserver observer) {
      List<ISummonImpulseStrongMutSetEffectObserver> obsies;
      if (!observersForSummonImpulseStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ISummonImpulseStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForSummonImpulseStrongMutSet[id] = obsies;
    }
    public void RemoveSummonImpulseStrongMutSetObserver(int id, ISummonImpulseStrongMutSetEffectObserver observer) {
      if (observersForSummonImpulseStrongMutSet.ContainsKey(id)) {
        var list = observersForSummonImpulseStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForSummonImpulseStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitMireImpulseStrongMutSetEffect(IMireImpulseStrongMutSetEffect effect) {
      if (observersForMireImpulseStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IMireImpulseStrongMutSetEffectObserver>(observers)) {
          observer.OnMireImpulseStrongMutSetEffect(effect);
        }
      }
    }
    public void AddMireImpulseStrongMutSetObserver(int id, IMireImpulseStrongMutSetEffectObserver observer) {
      List<IMireImpulseStrongMutSetEffectObserver> obsies;
      if (!observersForMireImpulseStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IMireImpulseStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForMireImpulseStrongMutSet[id] = obsies;
    }
    public void RemoveMireImpulseStrongMutSetObserver(int id, IMireImpulseStrongMutSetEffectObserver observer) {
      if (observersForMireImpulseStrongMutSet.ContainsKey(id)) {
        var list = observersForMireImpulseStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForMireImpulseStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitEvaporateImpulseStrongMutSetEffect(IEvaporateImpulseStrongMutSetEffect effect) {
      if (observersForEvaporateImpulseStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IEvaporateImpulseStrongMutSetEffectObserver>(observers)) {
          observer.OnEvaporateImpulseStrongMutSetEffect(effect);
        }
      }
    }
    public void AddEvaporateImpulseStrongMutSetObserver(int id, IEvaporateImpulseStrongMutSetEffectObserver observer) {
      List<IEvaporateImpulseStrongMutSetEffectObserver> obsies;
      if (!observersForEvaporateImpulseStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IEvaporateImpulseStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForEvaporateImpulseStrongMutSet[id] = obsies;
    }
    public void RemoveEvaporateImpulseStrongMutSetObserver(int id, IEvaporateImpulseStrongMutSetEffectObserver observer) {
      if (observersForEvaporateImpulseStrongMutSet.ContainsKey(id)) {
        var list = observersForEvaporateImpulseStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForEvaporateImpulseStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitMoveImpulseStrongMutSetEffect(IMoveImpulseStrongMutSetEffect effect) {
      if (observersForMoveImpulseStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IMoveImpulseStrongMutSetEffectObserver>(observers)) {
          observer.OnMoveImpulseStrongMutSetEffect(effect);
        }
      }
    }
    public void AddMoveImpulseStrongMutSetObserver(int id, IMoveImpulseStrongMutSetEffectObserver observer) {
      List<IMoveImpulseStrongMutSetEffectObserver> obsies;
      if (!observersForMoveImpulseStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IMoveImpulseStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForMoveImpulseStrongMutSet[id] = obsies;
    }
    public void RemoveMoveImpulseStrongMutSetObserver(int id, IMoveImpulseStrongMutSetEffectObserver observer) {
      if (observersForMoveImpulseStrongMutSet.ContainsKey(id)) {
        var list = observersForMoveImpulseStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForMoveImpulseStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitKamikazeJumpImpulseStrongMutSetEffect(IKamikazeJumpImpulseStrongMutSetEffect effect) {
      if (observersForKamikazeJumpImpulseStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IKamikazeJumpImpulseStrongMutSetEffectObserver>(observers)) {
          observer.OnKamikazeJumpImpulseStrongMutSetEffect(effect);
        }
      }
    }
    public void AddKamikazeJumpImpulseStrongMutSetObserver(int id, IKamikazeJumpImpulseStrongMutSetEffectObserver observer) {
      List<IKamikazeJumpImpulseStrongMutSetEffectObserver> obsies;
      if (!observersForKamikazeJumpImpulseStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IKamikazeJumpImpulseStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForKamikazeJumpImpulseStrongMutSet[id] = obsies;
    }
    public void RemoveKamikazeJumpImpulseStrongMutSetObserver(int id, IKamikazeJumpImpulseStrongMutSetEffectObserver observer) {
      if (observersForKamikazeJumpImpulseStrongMutSet.ContainsKey(id)) {
        var list = observersForKamikazeJumpImpulseStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForKamikazeJumpImpulseStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitKamikazeTargetImpulseStrongMutSetEffect(IKamikazeTargetImpulseStrongMutSetEffect effect) {
      if (observersForKamikazeTargetImpulseStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IKamikazeTargetImpulseStrongMutSetEffectObserver>(observers)) {
          observer.OnKamikazeTargetImpulseStrongMutSetEffect(effect);
        }
      }
    }
    public void AddKamikazeTargetImpulseStrongMutSetObserver(int id, IKamikazeTargetImpulseStrongMutSetEffectObserver observer) {
      List<IKamikazeTargetImpulseStrongMutSetEffectObserver> obsies;
      if (!observersForKamikazeTargetImpulseStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IKamikazeTargetImpulseStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForKamikazeTargetImpulseStrongMutSet[id] = obsies;
    }
    public void RemoveKamikazeTargetImpulseStrongMutSetObserver(int id, IKamikazeTargetImpulseStrongMutSetEffectObserver observer) {
      if (observersForKamikazeTargetImpulseStrongMutSet.ContainsKey(id)) {
        var list = observersForKamikazeTargetImpulseStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForKamikazeTargetImpulseStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitNoImpulseStrongMutSetEffect(INoImpulseStrongMutSetEffect effect) {
      if (observersForNoImpulseStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<INoImpulseStrongMutSetEffectObserver>(observers)) {
          observer.OnNoImpulseStrongMutSetEffect(effect);
        }
      }
    }
    public void AddNoImpulseStrongMutSetObserver(int id, INoImpulseStrongMutSetEffectObserver observer) {
      List<INoImpulseStrongMutSetEffectObserver> obsies;
      if (!observersForNoImpulseStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<INoImpulseStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForNoImpulseStrongMutSet[id] = obsies;
    }
    public void RemoveNoImpulseStrongMutSetObserver(int id, INoImpulseStrongMutSetEffectObserver observer) {
      if (observersForNoImpulseStrongMutSet.ContainsKey(id)) {
        var list = observersForNoImpulseStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForNoImpulseStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitEvolvifyImpulseStrongMutSetEffect(IEvolvifyImpulseStrongMutSetEffect effect) {
      if (observersForEvolvifyImpulseStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IEvolvifyImpulseStrongMutSetEffectObserver>(observers)) {
          observer.OnEvolvifyImpulseStrongMutSetEffect(effect);
        }
      }
    }
    public void AddEvolvifyImpulseStrongMutSetObserver(int id, IEvolvifyImpulseStrongMutSetEffectObserver observer) {
      List<IEvolvifyImpulseStrongMutSetEffectObserver> obsies;
      if (!observersForEvolvifyImpulseStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IEvolvifyImpulseStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForEvolvifyImpulseStrongMutSet[id] = obsies;
    }
    public void RemoveEvolvifyImpulseStrongMutSetObserver(int id, IEvolvifyImpulseStrongMutSetEffectObserver observer) {
      if (observersForEvolvifyImpulseStrongMutSet.ContainsKey(id)) {
        var list = observersForEvolvifyImpulseStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForEvolvifyImpulseStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitFireImpulseStrongMutSetEffect(IFireImpulseStrongMutSetEffect effect) {
      if (observersForFireImpulseStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IFireImpulseStrongMutSetEffectObserver>(observers)) {
          observer.OnFireImpulseStrongMutSetEffect(effect);
        }
      }
    }
    public void AddFireImpulseStrongMutSetObserver(int id, IFireImpulseStrongMutSetEffectObserver observer) {
      List<IFireImpulseStrongMutSetEffectObserver> obsies;
      if (!observersForFireImpulseStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IFireImpulseStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForFireImpulseStrongMutSet[id] = obsies;
    }
    public void RemoveFireImpulseStrongMutSetObserver(int id, IFireImpulseStrongMutSetEffectObserver observer) {
      if (observersForFireImpulseStrongMutSet.ContainsKey(id)) {
        var list = observersForFireImpulseStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForFireImpulseStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitDefyImpulseStrongMutSetEffect(IDefyImpulseStrongMutSetEffect effect) {
      if (observersForDefyImpulseStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IDefyImpulseStrongMutSetEffectObserver>(observers)) {
          observer.OnDefyImpulseStrongMutSetEffect(effect);
        }
      }
    }
    public void AddDefyImpulseStrongMutSetObserver(int id, IDefyImpulseStrongMutSetEffectObserver observer) {
      List<IDefyImpulseStrongMutSetEffectObserver> obsies;
      if (!observersForDefyImpulseStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IDefyImpulseStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForDefyImpulseStrongMutSet[id] = obsies;
    }
    public void RemoveDefyImpulseStrongMutSetObserver(int id, IDefyImpulseStrongMutSetEffectObserver observer) {
      if (observersForDefyImpulseStrongMutSet.ContainsKey(id)) {
        var list = observersForDefyImpulseStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForDefyImpulseStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitCounterImpulseStrongMutSetEffect(ICounterImpulseStrongMutSetEffect effect) {
      if (observersForCounterImpulseStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ICounterImpulseStrongMutSetEffectObserver>(observers)) {
          observer.OnCounterImpulseStrongMutSetEffect(effect);
        }
      }
    }
    public void AddCounterImpulseStrongMutSetObserver(int id, ICounterImpulseStrongMutSetEffectObserver observer) {
      List<ICounterImpulseStrongMutSetEffectObserver> obsies;
      if (!observersForCounterImpulseStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ICounterImpulseStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForCounterImpulseStrongMutSet[id] = obsies;
    }
    public void RemoveCounterImpulseStrongMutSetObserver(int id, ICounterImpulseStrongMutSetEffectObserver observer) {
      if (observersForCounterImpulseStrongMutSet.ContainsKey(id)) {
        var list = observersForCounterImpulseStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForCounterImpulseStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitUnleashBideImpulseStrongMutSetEffect(IUnleashBideImpulseStrongMutSetEffect effect) {
      if (observersForUnleashBideImpulseStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IUnleashBideImpulseStrongMutSetEffectObserver>(observers)) {
          observer.OnUnleashBideImpulseStrongMutSetEffect(effect);
        }
      }
    }
    public void AddUnleashBideImpulseStrongMutSetObserver(int id, IUnleashBideImpulseStrongMutSetEffectObserver observer) {
      List<IUnleashBideImpulseStrongMutSetEffectObserver> obsies;
      if (!observersForUnleashBideImpulseStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IUnleashBideImpulseStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForUnleashBideImpulseStrongMutSet[id] = obsies;
    }
    public void RemoveUnleashBideImpulseStrongMutSetObserver(int id, IUnleashBideImpulseStrongMutSetEffectObserver observer) {
      if (observersForUnleashBideImpulseStrongMutSet.ContainsKey(id)) {
        var list = observersForUnleashBideImpulseStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForUnleashBideImpulseStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitContinueBidingImpulseStrongMutSetEffect(IContinueBidingImpulseStrongMutSetEffect effect) {
      if (observersForContinueBidingImpulseStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IContinueBidingImpulseStrongMutSetEffectObserver>(observers)) {
          observer.OnContinueBidingImpulseStrongMutSetEffect(effect);
        }
      }
    }
    public void AddContinueBidingImpulseStrongMutSetObserver(int id, IContinueBidingImpulseStrongMutSetEffectObserver observer) {
      List<IContinueBidingImpulseStrongMutSetEffectObserver> obsies;
      if (!observersForContinueBidingImpulseStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IContinueBidingImpulseStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForContinueBidingImpulseStrongMutSet[id] = obsies;
    }
    public void RemoveContinueBidingImpulseStrongMutSetObserver(int id, IContinueBidingImpulseStrongMutSetEffectObserver observer) {
      if (observersForContinueBidingImpulseStrongMutSet.ContainsKey(id)) {
        var list = observersForContinueBidingImpulseStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForContinueBidingImpulseStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitStartBidingImpulseStrongMutSetEffect(IStartBidingImpulseStrongMutSetEffect effect) {
      if (observersForStartBidingImpulseStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IStartBidingImpulseStrongMutSetEffectObserver>(observers)) {
          observer.OnStartBidingImpulseStrongMutSetEffect(effect);
        }
      }
    }
    public void AddStartBidingImpulseStrongMutSetObserver(int id, IStartBidingImpulseStrongMutSetEffectObserver observer) {
      List<IStartBidingImpulseStrongMutSetEffectObserver> obsies;
      if (!observersForStartBidingImpulseStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IStartBidingImpulseStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForStartBidingImpulseStrongMutSet[id] = obsies;
    }
    public void RemoveStartBidingImpulseStrongMutSetObserver(int id, IStartBidingImpulseStrongMutSetEffectObserver observer) {
      if (observersForStartBidingImpulseStrongMutSet.ContainsKey(id)) {
        var list = observersForStartBidingImpulseStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForStartBidingImpulseStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitAttackImpulseStrongMutSetEffect(IAttackImpulseStrongMutSetEffect effect) {
      if (observersForAttackImpulseStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IAttackImpulseStrongMutSetEffectObserver>(observers)) {
          observer.OnAttackImpulseStrongMutSetEffect(effect);
        }
      }
    }
    public void AddAttackImpulseStrongMutSetObserver(int id, IAttackImpulseStrongMutSetEffectObserver observer) {
      List<IAttackImpulseStrongMutSetEffectObserver> obsies;
      if (!observersForAttackImpulseStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IAttackImpulseStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForAttackImpulseStrongMutSet[id] = obsies;
    }
    public void RemoveAttackImpulseStrongMutSetObserver(int id, IAttackImpulseStrongMutSetEffectObserver observer) {
      if (observersForAttackImpulseStrongMutSet.ContainsKey(id)) {
        var list = observersForAttackImpulseStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForAttackImpulseStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitPursueImpulseStrongMutSetEffect(IPursueImpulseStrongMutSetEffect effect) {
      if (observersForPursueImpulseStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IPursueImpulseStrongMutSetEffectObserver>(observers)) {
          observer.OnPursueImpulseStrongMutSetEffect(effect);
        }
      }
    }
    public void AddPursueImpulseStrongMutSetObserver(int id, IPursueImpulseStrongMutSetEffectObserver observer) {
      List<IPursueImpulseStrongMutSetEffectObserver> obsies;
      if (!observersForPursueImpulseStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IPursueImpulseStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForPursueImpulseStrongMutSet[id] = obsies;
    }
    public void RemovePursueImpulseStrongMutSetObserver(int id, IPursueImpulseStrongMutSetEffectObserver observer) {
      if (observersForPursueImpulseStrongMutSet.ContainsKey(id)) {
        var list = observersForPursueImpulseStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForPursueImpulseStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitFireBombImpulseStrongMutSetEffect(IFireBombImpulseStrongMutSetEffect effect) {
      if (observersForFireBombImpulseStrongMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IFireBombImpulseStrongMutSetEffectObserver>(observers)) {
          observer.OnFireBombImpulseStrongMutSetEffect(effect);
        }
      }
    }
    public void AddFireBombImpulseStrongMutSetObserver(int id, IFireBombImpulseStrongMutSetEffectObserver observer) {
      List<IFireBombImpulseStrongMutSetEffectObserver> obsies;
      if (!observersForFireBombImpulseStrongMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IFireBombImpulseStrongMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForFireBombImpulseStrongMutSet[id] = obsies;
    }
    public void RemoveFireBombImpulseStrongMutSetObserver(int id, IFireBombImpulseStrongMutSetEffectObserver observer) {
      if (observersForFireBombImpulseStrongMutSet.ContainsKey(id)) {
        var list = observersForFireBombImpulseStrongMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForFireBombImpulseStrongMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitLightningChargedUCWeakMutSetEffect(ILightningChargedUCWeakMutSetEffect effect) {
      if (observersForLightningChargedUCWeakMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ILightningChargedUCWeakMutSetEffectObserver>(observers)) {
          observer.OnLightningChargedUCWeakMutSetEffect(effect);
        }
      }
    }
    public void AddLightningChargedUCWeakMutSetObserver(int id, ILightningChargedUCWeakMutSetEffectObserver observer) {
      List<ILightningChargedUCWeakMutSetEffectObserver> obsies;
      if (!observersForLightningChargedUCWeakMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ILightningChargedUCWeakMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForLightningChargedUCWeakMutSet[id] = obsies;
    }
    public void RemoveLightningChargedUCWeakMutSetObserver(int id, ILightningChargedUCWeakMutSetEffectObserver observer) {
      if (observersForLightningChargedUCWeakMutSet.ContainsKey(id)) {
        var list = observersForLightningChargedUCWeakMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForLightningChargedUCWeakMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitTimeCloneAICapabilityUCWeakMutSetEffect(ITimeCloneAICapabilityUCWeakMutSetEffect effect) {
      if (observersForTimeCloneAICapabilityUCWeakMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ITimeCloneAICapabilityUCWeakMutSetEffectObserver>(observers)) {
          observer.OnTimeCloneAICapabilityUCWeakMutSetEffect(effect);
        }
      }
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

    public void visitDoomedUCWeakMutSetEffect(IDoomedUCWeakMutSetEffect effect) {
      if (observersForDoomedUCWeakMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IDoomedUCWeakMutSetEffectObserver>(observers)) {
          observer.OnDoomedUCWeakMutSetEffect(effect);
        }
      }
    }
    public void AddDoomedUCWeakMutSetObserver(int id, IDoomedUCWeakMutSetEffectObserver observer) {
      List<IDoomedUCWeakMutSetEffectObserver> obsies;
      if (!observersForDoomedUCWeakMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IDoomedUCWeakMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForDoomedUCWeakMutSet[id] = obsies;
    }
    public void RemoveDoomedUCWeakMutSetObserver(int id, IDoomedUCWeakMutSetEffectObserver observer) {
      if (observersForDoomedUCWeakMutSet.ContainsKey(id)) {
        var list = observersForDoomedUCWeakMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForDoomedUCWeakMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitMiredUCWeakMutSetEffect(IMiredUCWeakMutSetEffect effect) {
      if (observersForMiredUCWeakMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IMiredUCWeakMutSetEffectObserver>(observers)) {
          observer.OnMiredUCWeakMutSetEffect(effect);
        }
      }
    }
    public void AddMiredUCWeakMutSetObserver(int id, IMiredUCWeakMutSetEffectObserver observer) {
      List<IMiredUCWeakMutSetEffectObserver> obsies;
      if (!observersForMiredUCWeakMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IMiredUCWeakMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForMiredUCWeakMutSet[id] = obsies;
    }
    public void RemoveMiredUCWeakMutSetObserver(int id, IMiredUCWeakMutSetEffectObserver observer) {
      if (observersForMiredUCWeakMutSet.ContainsKey(id)) {
        var list = observersForMiredUCWeakMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForMiredUCWeakMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitInvincibilityUCWeakMutSetEffect(IInvincibilityUCWeakMutSetEffect effect) {
      if (observersForInvincibilityUCWeakMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IInvincibilityUCWeakMutSetEffectObserver>(observers)) {
          observer.OnInvincibilityUCWeakMutSetEffect(effect);
        }
      }
    }
    public void AddInvincibilityUCWeakMutSetObserver(int id, IInvincibilityUCWeakMutSetEffectObserver observer) {
      List<IInvincibilityUCWeakMutSetEffectObserver> obsies;
      if (!observersForInvincibilityUCWeakMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IInvincibilityUCWeakMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForInvincibilityUCWeakMutSet[id] = obsies;
    }
    public void RemoveInvincibilityUCWeakMutSetObserver(int id, IInvincibilityUCWeakMutSetEffectObserver observer) {
      if (observersForInvincibilityUCWeakMutSet.ContainsKey(id)) {
        var list = observersForInvincibilityUCWeakMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForInvincibilityUCWeakMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitOnFireUCWeakMutSetEffect(IOnFireUCWeakMutSetEffect effect) {
      if (observersForOnFireUCWeakMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IOnFireUCWeakMutSetEffectObserver>(observers)) {
          observer.OnOnFireUCWeakMutSetEffect(effect);
        }
      }
    }
    public void AddOnFireUCWeakMutSetObserver(int id, IOnFireUCWeakMutSetEffectObserver observer) {
      List<IOnFireUCWeakMutSetEffectObserver> obsies;
      if (!observersForOnFireUCWeakMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IOnFireUCWeakMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForOnFireUCWeakMutSet[id] = obsies;
    }
    public void RemoveOnFireUCWeakMutSetObserver(int id, IOnFireUCWeakMutSetEffectObserver observer) {
      if (observersForOnFireUCWeakMutSet.ContainsKey(id)) {
        var list = observersForOnFireUCWeakMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForOnFireUCWeakMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitDefyingUCWeakMutSetEffect(IDefyingUCWeakMutSetEffect effect) {
      if (observersForDefyingUCWeakMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IDefyingUCWeakMutSetEffectObserver>(observers)) {
          observer.OnDefyingUCWeakMutSetEffect(effect);
        }
      }
    }
    public void AddDefyingUCWeakMutSetObserver(int id, IDefyingUCWeakMutSetEffectObserver observer) {
      List<IDefyingUCWeakMutSetEffectObserver> obsies;
      if (!observersForDefyingUCWeakMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IDefyingUCWeakMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForDefyingUCWeakMutSet[id] = obsies;
    }
    public void RemoveDefyingUCWeakMutSetObserver(int id, IDefyingUCWeakMutSetEffectObserver observer) {
      if (observersForDefyingUCWeakMutSet.ContainsKey(id)) {
        var list = observersForDefyingUCWeakMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForDefyingUCWeakMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitCounteringUCWeakMutSetEffect(ICounteringUCWeakMutSetEffect effect) {
      if (observersForCounteringUCWeakMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ICounteringUCWeakMutSetEffectObserver>(observers)) {
          observer.OnCounteringUCWeakMutSetEffect(effect);
        }
      }
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

    public void visitAttackAICapabilityUCWeakMutSetEffect(IAttackAICapabilityUCWeakMutSetEffect effect) {
      if (observersForAttackAICapabilityUCWeakMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IAttackAICapabilityUCWeakMutSetEffectObserver>(observers)) {
          observer.OnAttackAICapabilityUCWeakMutSetEffect(effect);
        }
      }
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

    public void visitUnitMutSetEffect(IUnitMutSetEffect effect) {
      if (observersForUnitMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IUnitMutSetEffectObserver>(observers)) {
          observer.OnUnitMutSetEffect(effect);
        }
      }
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

    public void visitSimplePresenceTriggerTTCMutSetEffect(ISimplePresenceTriggerTTCMutSetEffect effect) {
      if (observersForSimplePresenceTriggerTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ISimplePresenceTriggerTTCMutSetEffectObserver>(observers)) {
          observer.OnSimplePresenceTriggerTTCMutSetEffect(effect);
        }
      }
    }
    public void AddSimplePresenceTriggerTTCMutSetObserver(int id, ISimplePresenceTriggerTTCMutSetEffectObserver observer) {
      List<ISimplePresenceTriggerTTCMutSetEffectObserver> obsies;
      if (!observersForSimplePresenceTriggerTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ISimplePresenceTriggerTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForSimplePresenceTriggerTTCMutSet[id] = obsies;
    }
    public void RemoveSimplePresenceTriggerTTCMutSetObserver(int id, ISimplePresenceTriggerTTCMutSetEffectObserver observer) {
      if (observersForSimplePresenceTriggerTTCMutSet.ContainsKey(id)) {
        var list = observersForSimplePresenceTriggerTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForSimplePresenceTriggerTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitItemTTCMutSetEffect(IItemTTCMutSetEffect effect) {
      if (observersForItemTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IItemTTCMutSetEffectObserver>(observers)) {
          observer.OnItemTTCMutSetEffect(effect);
        }
      }
    }
    public void AddItemTTCMutSetObserver(int id, IItemTTCMutSetEffectObserver observer) {
      List<IItemTTCMutSetEffectObserver> obsies;
      if (!observersForItemTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IItemTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForItemTTCMutSet[id] = obsies;
    }
    public void RemoveItemTTCMutSetObserver(int id, IItemTTCMutSetEffectObserver observer) {
      if (observersForItemTTCMutSet.ContainsKey(id)) {
        var list = observersForItemTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForItemTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitFlowerTTCMutSetEffect(IFlowerTTCMutSetEffect effect) {
      if (observersForFlowerTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IFlowerTTCMutSetEffectObserver>(observers)) {
          observer.OnFlowerTTCMutSetEffect(effect);
        }
      }
    }
    public void AddFlowerTTCMutSetObserver(int id, IFlowerTTCMutSetEffectObserver observer) {
      List<IFlowerTTCMutSetEffectObserver> obsies;
      if (!observersForFlowerTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IFlowerTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForFlowerTTCMutSet[id] = obsies;
    }
    public void RemoveFlowerTTCMutSetObserver(int id, IFlowerTTCMutSetEffectObserver observer) {
      if (observersForFlowerTTCMutSet.ContainsKey(id)) {
        var list = observersForFlowerTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForFlowerTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitLotusTTCMutSetEffect(ILotusTTCMutSetEffect effect) {
      if (observersForLotusTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ILotusTTCMutSetEffectObserver>(observers)) {
          observer.OnLotusTTCMutSetEffect(effect);
        }
      }
    }
    public void AddLotusTTCMutSetObserver(int id, ILotusTTCMutSetEffectObserver observer) {
      List<ILotusTTCMutSetEffectObserver> obsies;
      if (!observersForLotusTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ILotusTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForLotusTTCMutSet[id] = obsies;
    }
    public void RemoveLotusTTCMutSetObserver(int id, ILotusTTCMutSetEffectObserver observer) {
      if (observersForLotusTTCMutSet.ContainsKey(id)) {
        var list = observersForLotusTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForLotusTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitRoseTTCMutSetEffect(IRoseTTCMutSetEffect effect) {
      if (observersForRoseTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IRoseTTCMutSetEffectObserver>(observers)) {
          observer.OnRoseTTCMutSetEffect(effect);
        }
      }
    }
    public void AddRoseTTCMutSetObserver(int id, IRoseTTCMutSetEffectObserver observer) {
      List<IRoseTTCMutSetEffectObserver> obsies;
      if (!observersForRoseTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IRoseTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForRoseTTCMutSet[id] = obsies;
    }
    public void RemoveRoseTTCMutSetObserver(int id, IRoseTTCMutSetEffectObserver observer) {
      if (observersForRoseTTCMutSet.ContainsKey(id)) {
        var list = observersForRoseTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForRoseTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitLeafTTCMutSetEffect(ILeafTTCMutSetEffect effect) {
      if (observersForLeafTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ILeafTTCMutSetEffectObserver>(observers)) {
          observer.OnLeafTTCMutSetEffect(effect);
        }
      }
    }
    public void AddLeafTTCMutSetObserver(int id, ILeafTTCMutSetEffectObserver observer) {
      List<ILeafTTCMutSetEffectObserver> obsies;
      if (!observersForLeafTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ILeafTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForLeafTTCMutSet[id] = obsies;
    }
    public void RemoveLeafTTCMutSetObserver(int id, ILeafTTCMutSetEffectObserver observer) {
      if (observersForLeafTTCMutSet.ContainsKey(id)) {
        var list = observersForLeafTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForLeafTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitKamikazeTargetTTCMutSetEffect(IKamikazeTargetTTCMutSetEffect effect) {
      if (observersForKamikazeTargetTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IKamikazeTargetTTCMutSetEffectObserver>(observers)) {
          observer.OnKamikazeTargetTTCMutSetEffect(effect);
        }
      }
    }
    public void AddKamikazeTargetTTCMutSetObserver(int id, IKamikazeTargetTTCMutSetEffectObserver observer) {
      List<IKamikazeTargetTTCMutSetEffectObserver> obsies;
      if (!observersForKamikazeTargetTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IKamikazeTargetTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForKamikazeTargetTTCMutSet[id] = obsies;
    }
    public void RemoveKamikazeTargetTTCMutSetObserver(int id, IKamikazeTargetTTCMutSetEffectObserver observer) {
      if (observersForKamikazeTargetTTCMutSet.ContainsKey(id)) {
        var list = observersForKamikazeTargetTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForKamikazeTargetTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitWarperTTCMutSetEffect(IWarperTTCMutSetEffect effect) {
      if (observersForWarperTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IWarperTTCMutSetEffectObserver>(observers)) {
          observer.OnWarperTTCMutSetEffect(effect);
        }
      }
    }
    public void AddWarperTTCMutSetObserver(int id, IWarperTTCMutSetEffectObserver observer) {
      List<IWarperTTCMutSetEffectObserver> obsies;
      if (!observersForWarperTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IWarperTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForWarperTTCMutSet[id] = obsies;
    }
    public void RemoveWarperTTCMutSetObserver(int id, IWarperTTCMutSetEffectObserver observer) {
      if (observersForWarperTTCMutSet.ContainsKey(id)) {
        var list = observersForWarperTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForWarperTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitTimeAnchorTTCMutSetEffect(ITimeAnchorTTCMutSetEffect effect) {
      if (observersForTimeAnchorTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ITimeAnchorTTCMutSetEffectObserver>(observers)) {
          observer.OnTimeAnchorTTCMutSetEffect(effect);
        }
      }
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

    public void visitFireBombTTCMutSetEffect(IFireBombTTCMutSetEffect effect) {
      if (observersForFireBombTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IFireBombTTCMutSetEffectObserver>(observers)) {
          observer.OnFireBombTTCMutSetEffect(effect);
        }
      }
    }
    public void AddFireBombTTCMutSetObserver(int id, IFireBombTTCMutSetEffectObserver observer) {
      List<IFireBombTTCMutSetEffectObserver> obsies;
      if (!observersForFireBombTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IFireBombTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForFireBombTTCMutSet[id] = obsies;
    }
    public void RemoveFireBombTTCMutSetObserver(int id, IFireBombTTCMutSetEffectObserver observer) {
      if (observersForFireBombTTCMutSet.ContainsKey(id)) {
        var list = observersForFireBombTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForFireBombTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitOnFireTTCMutSetEffect(IOnFireTTCMutSetEffect effect) {
      if (observersForOnFireTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IOnFireTTCMutSetEffectObserver>(observers)) {
          observer.OnOnFireTTCMutSetEffect(effect);
        }
      }
    }
    public void AddOnFireTTCMutSetObserver(int id, IOnFireTTCMutSetEffectObserver observer) {
      List<IOnFireTTCMutSetEffectObserver> obsies;
      if (!observersForOnFireTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IOnFireTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForOnFireTTCMutSet[id] = obsies;
    }
    public void RemoveOnFireTTCMutSetObserver(int id, IOnFireTTCMutSetEffectObserver observer) {
      if (observersForOnFireTTCMutSet.ContainsKey(id)) {
        var list = observersForOnFireTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForOnFireTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitMarkerTTCMutSetEffect(IMarkerTTCMutSetEffect effect) {
      if (observersForMarkerTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IMarkerTTCMutSetEffectObserver>(observers)) {
          observer.OnMarkerTTCMutSetEffect(effect);
        }
      }
    }
    public void AddMarkerTTCMutSetObserver(int id, IMarkerTTCMutSetEffectObserver observer) {
      List<IMarkerTTCMutSetEffectObserver> obsies;
      if (!observersForMarkerTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IMarkerTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForMarkerTTCMutSet[id] = obsies;
    }
    public void RemoveMarkerTTCMutSetObserver(int id, IMarkerTTCMutSetEffectObserver observer) {
      if (observersForMarkerTTCMutSet.ContainsKey(id)) {
        var list = observersForMarkerTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForMarkerTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitLevelLinkTTCMutSetEffect(ILevelLinkTTCMutSetEffect effect) {
      if (observersForLevelLinkTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ILevelLinkTTCMutSetEffectObserver>(observers)) {
          observer.OnLevelLinkTTCMutSetEffect(effect);
        }
      }
    }
    public void AddLevelLinkTTCMutSetObserver(int id, ILevelLinkTTCMutSetEffectObserver observer) {
      List<ILevelLinkTTCMutSetEffectObserver> obsies;
      if (!observersForLevelLinkTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ILevelLinkTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForLevelLinkTTCMutSet[id] = obsies;
    }
    public void RemoveLevelLinkTTCMutSetObserver(int id, ILevelLinkTTCMutSetEffectObserver observer) {
      if (observersForLevelLinkTTCMutSet.ContainsKey(id)) {
        var list = observersForLevelLinkTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForLevelLinkTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitMudTTCMutSetEffect(IMudTTCMutSetEffect effect) {
      if (observersForMudTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IMudTTCMutSetEffectObserver>(observers)) {
          observer.OnMudTTCMutSetEffect(effect);
        }
      }
    }
    public void AddMudTTCMutSetObserver(int id, IMudTTCMutSetEffectObserver observer) {
      List<IMudTTCMutSetEffectObserver> obsies;
      if (!observersForMudTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IMudTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForMudTTCMutSet[id] = obsies;
    }
    public void RemoveMudTTCMutSetObserver(int id, IMudTTCMutSetEffectObserver observer) {
      if (observersForMudTTCMutSet.ContainsKey(id)) {
        var list = observersForMudTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForMudTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitDirtTTCMutSetEffect(IDirtTTCMutSetEffect effect) {
      if (observersForDirtTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IDirtTTCMutSetEffectObserver>(observers)) {
          observer.OnDirtTTCMutSetEffect(effect);
        }
      }
    }
    public void AddDirtTTCMutSetObserver(int id, IDirtTTCMutSetEffectObserver observer) {
      List<IDirtTTCMutSetEffectObserver> obsies;
      if (!observersForDirtTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IDirtTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForDirtTTCMutSet[id] = obsies;
    }
    public void RemoveDirtTTCMutSetObserver(int id, IDirtTTCMutSetEffectObserver observer) {
      if (observersForDirtTTCMutSet.ContainsKey(id)) {
        var list = observersForDirtTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForDirtTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitObsidianTTCMutSetEffect(IObsidianTTCMutSetEffect effect) {
      if (observersForObsidianTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IObsidianTTCMutSetEffectObserver>(observers)) {
          observer.OnObsidianTTCMutSetEffect(effect);
        }
      }
    }
    public void AddObsidianTTCMutSetObserver(int id, IObsidianTTCMutSetEffectObserver observer) {
      List<IObsidianTTCMutSetEffectObserver> obsies;
      if (!observersForObsidianTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IObsidianTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForObsidianTTCMutSet[id] = obsies;
    }
    public void RemoveObsidianTTCMutSetObserver(int id, IObsidianTTCMutSetEffectObserver observer) {
      if (observersForObsidianTTCMutSet.ContainsKey(id)) {
        var list = observersForObsidianTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForObsidianTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitDownStairsTTCMutSetEffect(IDownStairsTTCMutSetEffect effect) {
      if (observersForDownStairsTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IDownStairsTTCMutSetEffectObserver>(observers)) {
          observer.OnDownStairsTTCMutSetEffect(effect);
        }
      }
    }
    public void AddDownStairsTTCMutSetObserver(int id, IDownStairsTTCMutSetEffectObserver observer) {
      List<IDownStairsTTCMutSetEffectObserver> obsies;
      if (!observersForDownStairsTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IDownStairsTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForDownStairsTTCMutSet[id] = obsies;
    }
    public void RemoveDownStairsTTCMutSetObserver(int id, IDownStairsTTCMutSetEffectObserver observer) {
      if (observersForDownStairsTTCMutSet.ContainsKey(id)) {
        var list = observersForDownStairsTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForDownStairsTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitUpStairsTTCMutSetEffect(IUpStairsTTCMutSetEffect effect) {
      if (observersForUpStairsTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IUpStairsTTCMutSetEffectObserver>(observers)) {
          observer.OnUpStairsTTCMutSetEffect(effect);
        }
      }
    }
    public void AddUpStairsTTCMutSetObserver(int id, IUpStairsTTCMutSetEffectObserver observer) {
      List<IUpStairsTTCMutSetEffectObserver> obsies;
      if (!observersForUpStairsTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IUpStairsTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForUpStairsTTCMutSet[id] = obsies;
    }
    public void RemoveUpStairsTTCMutSetObserver(int id, IUpStairsTTCMutSetEffectObserver observer) {
      if (observersForUpStairsTTCMutSet.ContainsKey(id)) {
        var list = observersForUpStairsTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForUpStairsTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitWallTTCMutSetEffect(IWallTTCMutSetEffect effect) {
      if (observersForWallTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IWallTTCMutSetEffectObserver>(observers)) {
          observer.OnWallTTCMutSetEffect(effect);
        }
      }
    }
    public void AddWallTTCMutSetObserver(int id, IWallTTCMutSetEffectObserver observer) {
      List<IWallTTCMutSetEffectObserver> obsies;
      if (!observersForWallTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IWallTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForWallTTCMutSet[id] = obsies;
    }
    public void RemoveWallTTCMutSetObserver(int id, IWallTTCMutSetEffectObserver observer) {
      if (observersForWallTTCMutSet.ContainsKey(id)) {
        var list = observersForWallTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForWallTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitBloodTTCMutSetEffect(IBloodTTCMutSetEffect effect) {
      if (observersForBloodTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IBloodTTCMutSetEffectObserver>(observers)) {
          observer.OnBloodTTCMutSetEffect(effect);
        }
      }
    }
    public void AddBloodTTCMutSetObserver(int id, IBloodTTCMutSetEffectObserver observer) {
      List<IBloodTTCMutSetEffectObserver> obsies;
      if (!observersForBloodTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IBloodTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForBloodTTCMutSet[id] = obsies;
    }
    public void RemoveBloodTTCMutSetObserver(int id, IBloodTTCMutSetEffectObserver observer) {
      if (observersForBloodTTCMutSet.ContainsKey(id)) {
        var list = observersForBloodTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForBloodTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitRocksTTCMutSetEffect(IRocksTTCMutSetEffect effect) {
      if (observersForRocksTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IRocksTTCMutSetEffectObserver>(observers)) {
          observer.OnRocksTTCMutSetEffect(effect);
        }
      }
    }
    public void AddRocksTTCMutSetObserver(int id, IRocksTTCMutSetEffectObserver observer) {
      List<IRocksTTCMutSetEffectObserver> obsies;
      if (!observersForRocksTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IRocksTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForRocksTTCMutSet[id] = obsies;
    }
    public void RemoveRocksTTCMutSetObserver(int id, IRocksTTCMutSetEffectObserver observer) {
      if (observersForRocksTTCMutSet.ContainsKey(id)) {
        var list = observersForRocksTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForRocksTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitTreeTTCMutSetEffect(ITreeTTCMutSetEffect effect) {
      if (observersForTreeTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ITreeTTCMutSetEffectObserver>(observers)) {
          observer.OnTreeTTCMutSetEffect(effect);
        }
      }
    }
    public void AddTreeTTCMutSetObserver(int id, ITreeTTCMutSetEffectObserver observer) {
      List<ITreeTTCMutSetEffectObserver> obsies;
      if (!observersForTreeTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ITreeTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForTreeTTCMutSet[id] = obsies;
    }
    public void RemoveTreeTTCMutSetObserver(int id, ITreeTTCMutSetEffectObserver observer) {
      if (observersForTreeTTCMutSet.ContainsKey(id)) {
        var list = observersForTreeTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForTreeTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitWaterTTCMutSetEffect(IWaterTTCMutSetEffect effect) {
      if (observersForWaterTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IWaterTTCMutSetEffectObserver>(observers)) {
          observer.OnWaterTTCMutSetEffect(effect);
        }
      }
    }
    public void AddWaterTTCMutSetObserver(int id, IWaterTTCMutSetEffectObserver observer) {
      List<IWaterTTCMutSetEffectObserver> obsies;
      if (!observersForWaterTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IWaterTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForWaterTTCMutSet[id] = obsies;
    }
    public void RemoveWaterTTCMutSetObserver(int id, IWaterTTCMutSetEffectObserver observer) {
      if (observersForWaterTTCMutSet.ContainsKey(id)) {
        var list = observersForWaterTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForWaterTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitFloorTTCMutSetEffect(IFloorTTCMutSetEffect effect) {
      if (observersForFloorTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IFloorTTCMutSetEffectObserver>(observers)) {
          observer.OnFloorTTCMutSetEffect(effect);
        }
      }
    }
    public void AddFloorTTCMutSetObserver(int id, IFloorTTCMutSetEffectObserver observer) {
      List<IFloorTTCMutSetEffectObserver> obsies;
      if (!observersForFloorTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IFloorTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForFloorTTCMutSet[id] = obsies;
    }
    public void RemoveFloorTTCMutSetObserver(int id, IFloorTTCMutSetEffectObserver observer) {
      if (observersForFloorTTCMutSet.ContainsKey(id)) {
        var list = observersForFloorTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForFloorTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitCaveWallTTCMutSetEffect(ICaveWallTTCMutSetEffect effect) {
      if (observersForCaveWallTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ICaveWallTTCMutSetEffectObserver>(observers)) {
          observer.OnCaveWallTTCMutSetEffect(effect);
        }
      }
    }
    public void AddCaveWallTTCMutSetObserver(int id, ICaveWallTTCMutSetEffectObserver observer) {
      List<ICaveWallTTCMutSetEffectObserver> obsies;
      if (!observersForCaveWallTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ICaveWallTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForCaveWallTTCMutSet[id] = obsies;
    }
    public void RemoveCaveWallTTCMutSetObserver(int id, ICaveWallTTCMutSetEffectObserver observer) {
      if (observersForCaveWallTTCMutSet.ContainsKey(id)) {
        var list = observersForCaveWallTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForCaveWallTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitCaveTTCMutSetEffect(ICaveTTCMutSetEffect effect) {
      if (observersForCaveTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ICaveTTCMutSetEffectObserver>(observers)) {
          observer.OnCaveTTCMutSetEffect(effect);
        }
      }
    }
    public void AddCaveTTCMutSetObserver(int id, ICaveTTCMutSetEffectObserver observer) {
      List<ICaveTTCMutSetEffectObserver> obsies;
      if (!observersForCaveTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ICaveTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForCaveTTCMutSet[id] = obsies;
    }
    public void RemoveCaveTTCMutSetObserver(int id, ICaveTTCMutSetEffectObserver observer) {
      if (observersForCaveTTCMutSet.ContainsKey(id)) {
        var list = observersForCaveTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForCaveTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitFallsTTCMutSetEffect(IFallsTTCMutSetEffect effect) {
      if (observersForFallsTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IFallsTTCMutSetEffectObserver>(observers)) {
          observer.OnFallsTTCMutSetEffect(effect);
        }
      }
    }
    public void AddFallsTTCMutSetObserver(int id, IFallsTTCMutSetEffectObserver observer) {
      List<IFallsTTCMutSetEffectObserver> obsies;
      if (!observersForFallsTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IFallsTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForFallsTTCMutSet[id] = obsies;
    }
    public void RemoveFallsTTCMutSetObserver(int id, IFallsTTCMutSetEffectObserver observer) {
      if (observersForFallsTTCMutSet.ContainsKey(id)) {
        var list = observersForFallsTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForFallsTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitObsidianFloorTTCMutSetEffect(IObsidianFloorTTCMutSetEffect effect) {
      if (observersForObsidianFloorTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IObsidianFloorTTCMutSetEffectObserver>(observers)) {
          observer.OnObsidianFloorTTCMutSetEffect(effect);
        }
      }
    }
    public void AddObsidianFloorTTCMutSetObserver(int id, IObsidianFloorTTCMutSetEffectObserver observer) {
      List<IObsidianFloorTTCMutSetEffectObserver> obsies;
      if (!observersForObsidianFloorTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IObsidianFloorTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForObsidianFloorTTCMutSet[id] = obsies;
    }
    public void RemoveObsidianFloorTTCMutSetObserver(int id, IObsidianFloorTTCMutSetEffectObserver observer) {
      if (observersForObsidianFloorTTCMutSet.ContainsKey(id)) {
        var list = observersForObsidianFloorTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForObsidianFloorTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitMagmaTTCMutSetEffect(IMagmaTTCMutSetEffect effect) {
      if (observersForMagmaTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IMagmaTTCMutSetEffectObserver>(observers)) {
          observer.OnMagmaTTCMutSetEffect(effect);
        }
      }
    }
    public void AddMagmaTTCMutSetObserver(int id, IMagmaTTCMutSetEffectObserver observer) {
      List<IMagmaTTCMutSetEffectObserver> obsies;
      if (!observersForMagmaTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IMagmaTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForMagmaTTCMutSet[id] = obsies;
    }
    public void RemoveMagmaTTCMutSetObserver(int id, IMagmaTTCMutSetEffectObserver observer) {
      if (observersForMagmaTTCMutSet.ContainsKey(id)) {
        var list = observersForMagmaTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForMagmaTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitCliffTTCMutSetEffect(ICliffTTCMutSetEffect effect) {
      if (observersForCliffTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ICliffTTCMutSetEffectObserver>(observers)) {
          observer.OnCliffTTCMutSetEffect(effect);
        }
      }
    }
    public void AddCliffTTCMutSetObserver(int id, ICliffTTCMutSetEffectObserver observer) {
      List<ICliffTTCMutSetEffectObserver> obsies;
      if (!observersForCliffTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ICliffTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForCliffTTCMutSet[id] = obsies;
    }
    public void RemoveCliffTTCMutSetObserver(int id, ICliffTTCMutSetEffectObserver observer) {
      if (observersForCliffTTCMutSet.ContainsKey(id)) {
        var list = observersForCliffTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForCliffTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitRavaNestTTCMutSetEffect(IRavaNestTTCMutSetEffect effect) {
      if (observersForRavaNestTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IRavaNestTTCMutSetEffectObserver>(observers)) {
          observer.OnRavaNestTTCMutSetEffect(effect);
        }
      }
    }
    public void AddRavaNestTTCMutSetObserver(int id, IRavaNestTTCMutSetEffectObserver observer) {
      List<IRavaNestTTCMutSetEffectObserver> obsies;
      if (!observersForRavaNestTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IRavaNestTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForRavaNestTTCMutSet[id] = obsies;
    }
    public void RemoveRavaNestTTCMutSetObserver(int id, IRavaNestTTCMutSetEffectObserver observer) {
      if (observersForRavaNestTTCMutSet.ContainsKey(id)) {
        var list = observersForRavaNestTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForRavaNestTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitCliffLandingTTCMutSetEffect(ICliffLandingTTCMutSetEffect effect) {
      if (observersForCliffLandingTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ICliffLandingTTCMutSetEffectObserver>(observers)) {
          observer.OnCliffLandingTTCMutSetEffect(effect);
        }
      }
    }
    public void AddCliffLandingTTCMutSetObserver(int id, ICliffLandingTTCMutSetEffectObserver observer) {
      List<ICliffLandingTTCMutSetEffectObserver> obsies;
      if (!observersForCliffLandingTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ICliffLandingTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForCliffLandingTTCMutSet[id] = obsies;
    }
    public void RemoveCliffLandingTTCMutSetObserver(int id, ICliffLandingTTCMutSetEffectObserver observer) {
      if (observersForCliffLandingTTCMutSet.ContainsKey(id)) {
        var list = observersForCliffLandingTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForCliffLandingTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitStoneTTCMutSetEffect(IStoneTTCMutSetEffect effect) {
      if (observersForStoneTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IStoneTTCMutSetEffectObserver>(observers)) {
          observer.OnStoneTTCMutSetEffect(effect);
        }
      }
    }
    public void AddStoneTTCMutSetObserver(int id, IStoneTTCMutSetEffectObserver observer) {
      List<IStoneTTCMutSetEffectObserver> obsies;
      if (!observersForStoneTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IStoneTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForStoneTTCMutSet[id] = obsies;
    }
    public void RemoveStoneTTCMutSetObserver(int id, IStoneTTCMutSetEffectObserver observer) {
      if (observersForStoneTTCMutSet.ContainsKey(id)) {
        var list = observersForStoneTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForStoneTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitGrassTTCMutSetEffect(IGrassTTCMutSetEffect effect) {
      if (observersForGrassTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IGrassTTCMutSetEffectObserver>(observers)) {
          observer.OnGrassTTCMutSetEffect(effect);
        }
      }
    }
    public void AddGrassTTCMutSetObserver(int id, IGrassTTCMutSetEffectObserver observer) {
      List<IGrassTTCMutSetEffectObserver> obsies;
      if (!observersForGrassTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IGrassTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForGrassTTCMutSet[id] = obsies;
    }
    public void RemoveGrassTTCMutSetObserver(int id, IGrassTTCMutSetEffectObserver observer) {
      if (observersForGrassTTCMutSet.ContainsKey(id)) {
        var list = observersForGrassTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForGrassTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitEmberDeepLevelLinkerTTCMutSetEffect(IEmberDeepLevelLinkerTTCMutSetEffect effect) {
      if (observersForEmberDeepLevelLinkerTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IEmberDeepLevelLinkerTTCMutSetEffectObserver>(observers)) {
          observer.OnEmberDeepLevelLinkerTTCMutSetEffect(effect);
        }
      }
    }
    public void AddEmberDeepLevelLinkerTTCMutSetObserver(int id, IEmberDeepLevelLinkerTTCMutSetEffectObserver observer) {
      List<IEmberDeepLevelLinkerTTCMutSetEffectObserver> obsies;
      if (!observersForEmberDeepLevelLinkerTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IEmberDeepLevelLinkerTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForEmberDeepLevelLinkerTTCMutSet[id] = obsies;
    }
    public void RemoveEmberDeepLevelLinkerTTCMutSetObserver(int id, IEmberDeepLevelLinkerTTCMutSetEffectObserver observer) {
      if (observersForEmberDeepLevelLinkerTTCMutSet.ContainsKey(id)) {
        var list = observersForEmberDeepLevelLinkerTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForEmberDeepLevelLinkerTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitIncendianFallsLevelLinkerTTCMutSetEffect(IIncendianFallsLevelLinkerTTCMutSetEffect effect) {
      if (observersForIncendianFallsLevelLinkerTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IIncendianFallsLevelLinkerTTCMutSetEffectObserver>(observers)) {
          observer.OnIncendianFallsLevelLinkerTTCMutSetEffect(effect);
        }
      }
    }
    public void AddIncendianFallsLevelLinkerTTCMutSetObserver(int id, IIncendianFallsLevelLinkerTTCMutSetEffectObserver observer) {
      List<IIncendianFallsLevelLinkerTTCMutSetEffectObserver> obsies;
      if (!observersForIncendianFallsLevelLinkerTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IIncendianFallsLevelLinkerTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForIncendianFallsLevelLinkerTTCMutSet[id] = obsies;
    }
    public void RemoveIncendianFallsLevelLinkerTTCMutSetObserver(int id, IIncendianFallsLevelLinkerTTCMutSetEffectObserver observer) {
      if (observersForIncendianFallsLevelLinkerTTCMutSet.ContainsKey(id)) {
        var list = observersForIncendianFallsLevelLinkerTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForIncendianFallsLevelLinkerTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitRavaArcanaLevelLinkerTTCMutSetEffect(IRavaArcanaLevelLinkerTTCMutSetEffect effect) {
      if (observersForRavaArcanaLevelLinkerTTCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IRavaArcanaLevelLinkerTTCMutSetEffectObserver>(observers)) {
          observer.OnRavaArcanaLevelLinkerTTCMutSetEffect(effect);
        }
      }
    }
    public void AddRavaArcanaLevelLinkerTTCMutSetObserver(int id, IRavaArcanaLevelLinkerTTCMutSetEffectObserver observer) {
      List<IRavaArcanaLevelLinkerTTCMutSetEffectObserver> obsies;
      if (!observersForRavaArcanaLevelLinkerTTCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IRavaArcanaLevelLinkerTTCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForRavaArcanaLevelLinkerTTCMutSet[id] = obsies;
    }
    public void RemoveRavaArcanaLevelLinkerTTCMutSetObserver(int id, IRavaArcanaLevelLinkerTTCMutSetEffectObserver observer) {
      if (observersForRavaArcanaLevelLinkerTTCMutSet.ContainsKey(id)) {
        var list = observersForRavaArcanaLevelLinkerTTCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForRavaArcanaLevelLinkerTTCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitDeathTriggerUCMutSetEffect(IDeathTriggerUCMutSetEffect effect) {
      if (observersForDeathTriggerUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IDeathTriggerUCMutSetEffectObserver>(observers)) {
          observer.OnDeathTriggerUCMutSetEffect(effect);
        }
      }
    }
    public void AddDeathTriggerUCMutSetObserver(int id, IDeathTriggerUCMutSetEffectObserver observer) {
      List<IDeathTriggerUCMutSetEffectObserver> obsies;
      if (!observersForDeathTriggerUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IDeathTriggerUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForDeathTriggerUCMutSet[id] = obsies;
    }
    public void RemoveDeathTriggerUCMutSetObserver(int id, IDeathTriggerUCMutSetEffectObserver observer) {
      if (observersForDeathTriggerUCMutSet.ContainsKey(id)) {
        var list = observersForDeathTriggerUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForDeathTriggerUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitBequeathUCMutSetEffect(IBequeathUCMutSetEffect effect) {
      if (observersForBequeathUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IBequeathUCMutSetEffectObserver>(observers)) {
          observer.OnBequeathUCMutSetEffect(effect);
        }
      }
    }
    public void AddBequeathUCMutSetObserver(int id, IBequeathUCMutSetEffectObserver observer) {
      List<IBequeathUCMutSetEffectObserver> obsies;
      if (!observersForBequeathUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IBequeathUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForBequeathUCMutSet[id] = obsies;
    }
    public void RemoveBequeathUCMutSetObserver(int id, IBequeathUCMutSetEffectObserver observer) {
      if (observersForBequeathUCMutSet.ContainsKey(id)) {
        var list = observersForBequeathUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForBequeathUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitTutorialDefyCounterUCMutSetEffect(ITutorialDefyCounterUCMutSetEffect effect) {
      if (observersForTutorialDefyCounterUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ITutorialDefyCounterUCMutSetEffectObserver>(observers)) {
          observer.OnTutorialDefyCounterUCMutSetEffect(effect);
        }
      }
    }
    public void AddTutorialDefyCounterUCMutSetObserver(int id, ITutorialDefyCounterUCMutSetEffectObserver observer) {
      List<ITutorialDefyCounterUCMutSetEffectObserver> obsies;
      if (!observersForTutorialDefyCounterUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ITutorialDefyCounterUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForTutorialDefyCounterUCMutSet[id] = obsies;
    }
    public void RemoveTutorialDefyCounterUCMutSetObserver(int id, ITutorialDefyCounterUCMutSetEffectObserver observer) {
      if (observersForTutorialDefyCounterUCMutSet.ContainsKey(id)) {
        var list = observersForTutorialDefyCounterUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForTutorialDefyCounterUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitLightningChargingUCMutSetEffect(ILightningChargingUCMutSetEffect effect) {
      if (observersForLightningChargingUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ILightningChargingUCMutSetEffectObserver>(observers)) {
          observer.OnLightningChargingUCMutSetEffect(effect);
        }
      }
    }
    public void AddLightningChargingUCMutSetObserver(int id, ILightningChargingUCMutSetEffectObserver observer) {
      List<ILightningChargingUCMutSetEffectObserver> obsies;
      if (!observersForLightningChargingUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ILightningChargingUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForLightningChargingUCMutSet[id] = obsies;
    }
    public void RemoveLightningChargingUCMutSetObserver(int id, ILightningChargingUCMutSetEffectObserver observer) {
      if (observersForLightningChargingUCMutSet.ContainsKey(id)) {
        var list = observersForLightningChargingUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForLightningChargingUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitWanderAICapabilityUCMutSetEffect(IWanderAICapabilityUCMutSetEffect effect) {
      if (observersForWanderAICapabilityUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IWanderAICapabilityUCMutSetEffectObserver>(observers)) {
          observer.OnWanderAICapabilityUCMutSetEffect(effect);
        }
      }
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

    public void visitTemporaryCloneAICapabilityUCMutSetEffect(ITemporaryCloneAICapabilityUCMutSetEffect effect) {
      if (observersForTemporaryCloneAICapabilityUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ITemporaryCloneAICapabilityUCMutSetEffectObserver>(observers)) {
          observer.OnTemporaryCloneAICapabilityUCMutSetEffect(effect);
        }
      }
    }
    public void AddTemporaryCloneAICapabilityUCMutSetObserver(int id, ITemporaryCloneAICapabilityUCMutSetEffectObserver observer) {
      List<ITemporaryCloneAICapabilityUCMutSetEffectObserver> obsies;
      if (!observersForTemporaryCloneAICapabilityUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ITemporaryCloneAICapabilityUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForTemporaryCloneAICapabilityUCMutSet[id] = obsies;
    }
    public void RemoveTemporaryCloneAICapabilityUCMutSetObserver(int id, ITemporaryCloneAICapabilityUCMutSetEffectObserver observer) {
      if (observersForTemporaryCloneAICapabilityUCMutSet.ContainsKey(id)) {
        var list = observersForTemporaryCloneAICapabilityUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForTemporaryCloneAICapabilityUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitSummonAICapabilityUCMutSetEffect(ISummonAICapabilityUCMutSetEffect effect) {
      if (observersForSummonAICapabilityUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ISummonAICapabilityUCMutSetEffectObserver>(observers)) {
          observer.OnSummonAICapabilityUCMutSetEffect(effect);
        }
      }
    }
    public void AddSummonAICapabilityUCMutSetObserver(int id, ISummonAICapabilityUCMutSetEffectObserver observer) {
      List<ISummonAICapabilityUCMutSetEffectObserver> obsies;
      if (!observersForSummonAICapabilityUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ISummonAICapabilityUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForSummonAICapabilityUCMutSet[id] = obsies;
    }
    public void RemoveSummonAICapabilityUCMutSetObserver(int id, ISummonAICapabilityUCMutSetEffectObserver observer) {
      if (observersForSummonAICapabilityUCMutSet.ContainsKey(id)) {
        var list = observersForSummonAICapabilityUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForSummonAICapabilityUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitKamikazeAICapabilityUCMutSetEffect(IKamikazeAICapabilityUCMutSetEffect effect) {
      if (observersForKamikazeAICapabilityUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IKamikazeAICapabilityUCMutSetEffectObserver>(observers)) {
          observer.OnKamikazeAICapabilityUCMutSetEffect(effect);
        }
      }
    }
    public void AddKamikazeAICapabilityUCMutSetObserver(int id, IKamikazeAICapabilityUCMutSetEffectObserver observer) {
      List<IKamikazeAICapabilityUCMutSetEffectObserver> obsies;
      if (!observersForKamikazeAICapabilityUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IKamikazeAICapabilityUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForKamikazeAICapabilityUCMutSet[id] = obsies;
    }
    public void RemoveKamikazeAICapabilityUCMutSetObserver(int id, IKamikazeAICapabilityUCMutSetEffectObserver observer) {
      if (observersForKamikazeAICapabilityUCMutSet.ContainsKey(id)) {
        var list = observersForKamikazeAICapabilityUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForKamikazeAICapabilityUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitGuardAICapabilityUCMutSetEffect(IGuardAICapabilityUCMutSetEffect effect) {
      if (observersForGuardAICapabilityUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IGuardAICapabilityUCMutSetEffectObserver>(observers)) {
          observer.OnGuardAICapabilityUCMutSetEffect(effect);
        }
      }
    }
    public void AddGuardAICapabilityUCMutSetObserver(int id, IGuardAICapabilityUCMutSetEffectObserver observer) {
      List<IGuardAICapabilityUCMutSetEffectObserver> obsies;
      if (!observersForGuardAICapabilityUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IGuardAICapabilityUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForGuardAICapabilityUCMutSet[id] = obsies;
    }
    public void RemoveGuardAICapabilityUCMutSetObserver(int id, IGuardAICapabilityUCMutSetEffectObserver observer) {
      if (observersForGuardAICapabilityUCMutSet.ContainsKey(id)) {
        var list = observersForGuardAICapabilityUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForGuardAICapabilityUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitEvolvifyAICapabilityUCMutSetEffect(IEvolvifyAICapabilityUCMutSetEffect effect) {
      if (observersForEvolvifyAICapabilityUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IEvolvifyAICapabilityUCMutSetEffectObserver>(observers)) {
          observer.OnEvolvifyAICapabilityUCMutSetEffect(effect);
        }
      }
    }
    public void AddEvolvifyAICapabilityUCMutSetObserver(int id, IEvolvifyAICapabilityUCMutSetEffectObserver observer) {
      List<IEvolvifyAICapabilityUCMutSetEffectObserver> obsies;
      if (!observersForEvolvifyAICapabilityUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IEvolvifyAICapabilityUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForEvolvifyAICapabilityUCMutSet[id] = obsies;
    }
    public void RemoveEvolvifyAICapabilityUCMutSetObserver(int id, IEvolvifyAICapabilityUCMutSetEffectObserver observer) {
      if (observersForEvolvifyAICapabilityUCMutSet.ContainsKey(id)) {
        var list = observersForEvolvifyAICapabilityUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForEvolvifyAICapabilityUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitTimeCloneAICapabilityUCMutSetEffect(ITimeCloneAICapabilityUCMutSetEffect effect) {
      if (observersForTimeCloneAICapabilityUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ITimeCloneAICapabilityUCMutSetEffectObserver>(observers)) {
          observer.OnTimeCloneAICapabilityUCMutSetEffect(effect);
        }
      }
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

    public void visitDoomedUCMutSetEffect(IDoomedUCMutSetEffect effect) {
      if (observersForDoomedUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IDoomedUCMutSetEffectObserver>(observers)) {
          observer.OnDoomedUCMutSetEffect(effect);
        }
      }
    }
    public void AddDoomedUCMutSetObserver(int id, IDoomedUCMutSetEffectObserver observer) {
      List<IDoomedUCMutSetEffectObserver> obsies;
      if (!observersForDoomedUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IDoomedUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForDoomedUCMutSet[id] = obsies;
    }
    public void RemoveDoomedUCMutSetObserver(int id, IDoomedUCMutSetEffectObserver observer) {
      if (observersForDoomedUCMutSet.ContainsKey(id)) {
        var list = observersForDoomedUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForDoomedUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitMiredUCMutSetEffect(IMiredUCMutSetEffect effect) {
      if (observersForMiredUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IMiredUCMutSetEffectObserver>(observers)) {
          observer.OnMiredUCMutSetEffect(effect);
        }
      }
    }
    public void AddMiredUCMutSetObserver(int id, IMiredUCMutSetEffectObserver observer) {
      List<IMiredUCMutSetEffectObserver> obsies;
      if (!observersForMiredUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IMiredUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForMiredUCMutSet[id] = obsies;
    }
    public void RemoveMiredUCMutSetObserver(int id, IMiredUCMutSetEffectObserver observer) {
      if (observersForMiredUCMutSet.ContainsKey(id)) {
        var list = observersForMiredUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForMiredUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitOnFireUCMutSetEffect(IOnFireUCMutSetEffect effect) {
      if (observersForOnFireUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IOnFireUCMutSetEffectObserver>(observers)) {
          observer.OnOnFireUCMutSetEffect(effect);
        }
      }
    }
    public void AddOnFireUCMutSetObserver(int id, IOnFireUCMutSetEffectObserver observer) {
      List<IOnFireUCMutSetEffectObserver> obsies;
      if (!observersForOnFireUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IOnFireUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForOnFireUCMutSet[id] = obsies;
    }
    public void RemoveOnFireUCMutSetObserver(int id, IOnFireUCMutSetEffectObserver observer) {
      if (observersForOnFireUCMutSet.ContainsKey(id)) {
        var list = observersForOnFireUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForOnFireUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitAttackAICapabilityUCMutSetEffect(IAttackAICapabilityUCMutSetEffect effect) {
      if (observersForAttackAICapabilityUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IAttackAICapabilityUCMutSetEffectObserver>(observers)) {
          observer.OnAttackAICapabilityUCMutSetEffect(effect);
        }
      }
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

    public void visitCounteringUCMutSetEffect(ICounteringUCMutSetEffect effect) {
      if (observersForCounteringUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ICounteringUCMutSetEffectObserver>(observers)) {
          observer.OnCounteringUCMutSetEffect(effect);
        }
      }
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

    public void visitLightningChargedUCMutSetEffect(ILightningChargedUCMutSetEffect effect) {
      if (observersForLightningChargedUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ILightningChargedUCMutSetEffectObserver>(observers)) {
          observer.OnLightningChargedUCMutSetEffect(effect);
        }
      }
    }
    public void AddLightningChargedUCMutSetObserver(int id, ILightningChargedUCMutSetEffectObserver observer) {
      List<ILightningChargedUCMutSetEffectObserver> obsies;
      if (!observersForLightningChargedUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ILightningChargedUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForLightningChargedUCMutSet[id] = obsies;
    }
    public void RemoveLightningChargedUCMutSetObserver(int id, ILightningChargedUCMutSetEffectObserver observer) {
      if (observersForLightningChargedUCMutSet.ContainsKey(id)) {
        var list = observersForLightningChargedUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForLightningChargedUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitInvincibilityUCMutSetEffect(IInvincibilityUCMutSetEffect effect) {
      if (observersForInvincibilityUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IInvincibilityUCMutSetEffectObserver>(observers)) {
          observer.OnInvincibilityUCMutSetEffect(effect);
        }
      }
    }
    public void AddInvincibilityUCMutSetObserver(int id, IInvincibilityUCMutSetEffectObserver observer) {
      List<IInvincibilityUCMutSetEffectObserver> obsies;
      if (!observersForInvincibilityUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IInvincibilityUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForInvincibilityUCMutSet[id] = obsies;
    }
    public void RemoveInvincibilityUCMutSetObserver(int id, IInvincibilityUCMutSetEffectObserver observer) {
      if (observersForInvincibilityUCMutSet.ContainsKey(id)) {
        var list = observersForInvincibilityUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForInvincibilityUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitDefyingUCMutSetEffect(IDefyingUCMutSetEffect effect) {
      if (observersForDefyingUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IDefyingUCMutSetEffectObserver>(observers)) {
          observer.OnDefyingUCMutSetEffect(effect);
        }
      }
    }
    public void AddDefyingUCMutSetObserver(int id, IDefyingUCMutSetEffectObserver observer) {
      List<IDefyingUCMutSetEffectObserver> obsies;
      if (!observersForDefyingUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IDefyingUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForDefyingUCMutSet[id] = obsies;
    }
    public void RemoveDefyingUCMutSetObserver(int id, IDefyingUCMutSetEffectObserver observer) {
      if (observersForDefyingUCMutSet.ContainsKey(id)) {
        var list = observersForDefyingUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForDefyingUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitBideAICapabilityUCMutSetEffect(IBideAICapabilityUCMutSetEffect effect) {
      if (observersForBideAICapabilityUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IBideAICapabilityUCMutSetEffectObserver>(observers)) {
          observer.OnBideAICapabilityUCMutSetEffect(effect);
        }
      }
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

    public void visitBaseSightRangeUCMutSetEffect(IBaseSightRangeUCMutSetEffect effect) {
      if (observersForBaseSightRangeUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IBaseSightRangeUCMutSetEffectObserver>(observers)) {
          observer.OnBaseSightRangeUCMutSetEffect(effect);
        }
      }
    }
    public void AddBaseSightRangeUCMutSetObserver(int id, IBaseSightRangeUCMutSetEffectObserver observer) {
      List<IBaseSightRangeUCMutSetEffectObserver> obsies;
      if (!observersForBaseSightRangeUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IBaseSightRangeUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForBaseSightRangeUCMutSet[id] = obsies;
    }
    public void RemoveBaseSightRangeUCMutSetObserver(int id, IBaseSightRangeUCMutSetEffectObserver observer) {
      if (observersForBaseSightRangeUCMutSet.ContainsKey(id)) {
        var list = observersForBaseSightRangeUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForBaseSightRangeUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitBaseMovementTimeUCMutSetEffect(IBaseMovementTimeUCMutSetEffect effect) {
      if (observersForBaseMovementTimeUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IBaseMovementTimeUCMutSetEffectObserver>(observers)) {
          observer.OnBaseMovementTimeUCMutSetEffect(effect);
        }
      }
    }
    public void AddBaseMovementTimeUCMutSetObserver(int id, IBaseMovementTimeUCMutSetEffectObserver observer) {
      List<IBaseMovementTimeUCMutSetEffectObserver> obsies;
      if (!observersForBaseMovementTimeUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IBaseMovementTimeUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForBaseMovementTimeUCMutSet[id] = obsies;
    }
    public void RemoveBaseMovementTimeUCMutSetObserver(int id, IBaseMovementTimeUCMutSetEffectObserver observer) {
      if (observersForBaseMovementTimeUCMutSet.ContainsKey(id)) {
        var list = observersForBaseMovementTimeUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForBaseMovementTimeUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitBaseCombatTimeUCMutSetEffect(IBaseCombatTimeUCMutSetEffect effect) {
      if (observersForBaseCombatTimeUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IBaseCombatTimeUCMutSetEffectObserver>(observers)) {
          observer.OnBaseCombatTimeUCMutSetEffect(effect);
        }
      }
    }
    public void AddBaseCombatTimeUCMutSetObserver(int id, IBaseCombatTimeUCMutSetEffectObserver observer) {
      List<IBaseCombatTimeUCMutSetEffectObserver> obsies;
      if (!observersForBaseCombatTimeUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IBaseCombatTimeUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForBaseCombatTimeUCMutSet[id] = obsies;
    }
    public void RemoveBaseCombatTimeUCMutSetObserver(int id, IBaseCombatTimeUCMutSetEffectObserver observer) {
      if (observersForBaseCombatTimeUCMutSet.ContainsKey(id)) {
        var list = observersForBaseCombatTimeUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForBaseCombatTimeUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitManaPotionMutSetEffect(IManaPotionMutSetEffect effect) {
      if (observersForManaPotionMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IManaPotionMutSetEffectObserver>(observers)) {
          observer.OnManaPotionMutSetEffect(effect);
        }
      }
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

    public void visitHealthPotionMutSetEffect(IHealthPotionMutSetEffect effect) {
      if (observersForHealthPotionMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IHealthPotionMutSetEffectObserver>(observers)) {
          observer.OnHealthPotionMutSetEffect(effect);
        }
      }
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

    public void visitSpeedRingMutSetEffect(ISpeedRingMutSetEffect effect) {
      if (observersForSpeedRingMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ISpeedRingMutSetEffectObserver>(observers)) {
          observer.OnSpeedRingMutSetEffect(effect);
        }
      }
    }
    public void AddSpeedRingMutSetObserver(int id, ISpeedRingMutSetEffectObserver observer) {
      List<ISpeedRingMutSetEffectObserver> obsies;
      if (!observersForSpeedRingMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ISpeedRingMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForSpeedRingMutSet[id] = obsies;
    }
    public void RemoveSpeedRingMutSetObserver(int id, ISpeedRingMutSetEffectObserver observer) {
      if (observersForSpeedRingMutSet.ContainsKey(id)) {
        var list = observersForSpeedRingMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForSpeedRingMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitGlaiveMutSetEffect(IGlaiveMutSetEffect effect) {
      if (observersForGlaiveMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IGlaiveMutSetEffectObserver>(observers)) {
          observer.OnGlaiveMutSetEffect(effect);
        }
      }
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

    public void visitSlowRodMutSetEffect(ISlowRodMutSetEffect effect) {
      if (observersForSlowRodMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ISlowRodMutSetEffectObserver>(observers)) {
          observer.OnSlowRodMutSetEffect(effect);
        }
      }
    }
    public void AddSlowRodMutSetObserver(int id, ISlowRodMutSetEffectObserver observer) {
      List<ISlowRodMutSetEffectObserver> obsies;
      if (!observersForSlowRodMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ISlowRodMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForSlowRodMutSet[id] = obsies;
    }
    public void RemoveSlowRodMutSetObserver(int id, ISlowRodMutSetEffectObserver observer) {
      if (observersForSlowRodMutSet.ContainsKey(id)) {
        var list = observersForSlowRodMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForSlowRodMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitExplosionRodMutSetEffect(IExplosionRodMutSetEffect effect) {
      if (observersForExplosionRodMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IExplosionRodMutSetEffectObserver>(observers)) {
          observer.OnExplosionRodMutSetEffect(effect);
        }
      }
    }
    public void AddExplosionRodMutSetObserver(int id, IExplosionRodMutSetEffectObserver observer) {
      List<IExplosionRodMutSetEffectObserver> obsies;
      if (!observersForExplosionRodMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IExplosionRodMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForExplosionRodMutSet[id] = obsies;
    }
    public void RemoveExplosionRodMutSetObserver(int id, IExplosionRodMutSetEffectObserver observer) {
      if (observersForExplosionRodMutSet.ContainsKey(id)) {
        var list = observersForExplosionRodMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForExplosionRodMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitBlazeRodMutSetEffect(IBlazeRodMutSetEffect effect) {
      if (observersForBlazeRodMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IBlazeRodMutSetEffectObserver>(observers)) {
          observer.OnBlazeRodMutSetEffect(effect);
        }
      }
    }
    public void AddBlazeRodMutSetObserver(int id, IBlazeRodMutSetEffectObserver observer) {
      List<IBlazeRodMutSetEffectObserver> obsies;
      if (!observersForBlazeRodMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IBlazeRodMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForBlazeRodMutSet[id] = obsies;
    }
    public void RemoveBlazeRodMutSetObserver(int id, IBlazeRodMutSetEffectObserver observer) {
      if (observersForBlazeRodMutSet.ContainsKey(id)) {
        var list = observersForBlazeRodMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForBlazeRodMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitBlastRodMutSetEffect(IBlastRodMutSetEffect effect) {
      if (observersForBlastRodMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IBlastRodMutSetEffectObserver>(observers)) {
          observer.OnBlastRodMutSetEffect(effect);
        }
      }
    }
    public void AddBlastRodMutSetObserver(int id, IBlastRodMutSetEffectObserver observer) {
      List<IBlastRodMutSetEffectObserver> obsies;
      if (!observersForBlastRodMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IBlastRodMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForBlastRodMutSet[id] = obsies;
    }
    public void RemoveBlastRodMutSetObserver(int id, IBlastRodMutSetEffectObserver observer) {
      if (observersForBlastRodMutSet.ContainsKey(id)) {
        var list = observersForBlastRodMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForBlastRodMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitArmorMutSetEffect(IArmorMutSetEffect effect) {
      if (observersForArmorMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IArmorMutSetEffectObserver>(observers)) {
          observer.OnArmorMutSetEffect(effect);
        }
      }
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

    public void visitChallengingUCMutSetEffect(IChallengingUCMutSetEffect effect) {
      if (observersForChallengingUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IChallengingUCMutSetEffectObserver>(observers)) {
          observer.OnChallengingUCMutSetEffect(effect);
        }
      }
    }
    public void AddChallengingUCMutSetObserver(int id, IChallengingUCMutSetEffectObserver observer) {
      List<IChallengingUCMutSetEffectObserver> obsies;
      if (!observersForChallengingUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IChallengingUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForChallengingUCMutSet[id] = obsies;
    }
    public void RemoveChallengingUCMutSetObserver(int id, IChallengingUCMutSetEffectObserver observer) {
      if (observersForChallengingUCMutSet.ContainsKey(id)) {
        var list = observersForChallengingUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForChallengingUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitSorcerousUCMutSetEffect(ISorcerousUCMutSetEffect effect) {
      if (observersForSorcerousUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<ISorcerousUCMutSetEffectObserver>(observers)) {
          observer.OnSorcerousUCMutSetEffect(effect);
        }
      }
    }
    public void AddSorcerousUCMutSetObserver(int id, ISorcerousUCMutSetEffectObserver observer) {
      List<ISorcerousUCMutSetEffectObserver> obsies;
      if (!observersForSorcerousUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<ISorcerousUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForSorcerousUCMutSet[id] = obsies;
    }
    public void RemoveSorcerousUCMutSetObserver(int id, ISorcerousUCMutSetEffectObserver observer) {
      if (observersForSorcerousUCMutSet.ContainsKey(id)) {
        var list = observersForSorcerousUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForSorcerousUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitBaseOffenseUCMutSetEffect(IBaseOffenseUCMutSetEffect effect) {
      if (observersForBaseOffenseUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IBaseOffenseUCMutSetEffectObserver>(observers)) {
          observer.OnBaseOffenseUCMutSetEffect(effect);
        }
      }
    }
    public void AddBaseOffenseUCMutSetObserver(int id, IBaseOffenseUCMutSetEffectObserver observer) {
      List<IBaseOffenseUCMutSetEffectObserver> obsies;
      if (!observersForBaseOffenseUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IBaseOffenseUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForBaseOffenseUCMutSet[id] = obsies;
    }
    public void RemoveBaseOffenseUCMutSetObserver(int id, IBaseOffenseUCMutSetEffectObserver observer) {
      if (observersForBaseOffenseUCMutSet.ContainsKey(id)) {
        var list = observersForBaseOffenseUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForBaseOffenseUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

    public void visitBaseDefenseUCMutSetEffect(IBaseDefenseUCMutSetEffect effect) {
      if (observersForBaseDefenseUCMutSet.TryGetValue(effect.id, out var observers)) {
        foreach (var observer in new List<IBaseDefenseUCMutSetEffectObserver>(observers)) {
          observer.OnBaseDefenseUCMutSetEffect(effect);
        }
      }
    }
    public void AddBaseDefenseUCMutSetObserver(int id, IBaseDefenseUCMutSetEffectObserver observer) {
      List<IBaseDefenseUCMutSetEffectObserver> obsies;
      if (!observersForBaseDefenseUCMutSet.TryGetValue(id, out obsies)) {
        obsies = new List<IBaseDefenseUCMutSetEffectObserver>();
      }
      obsies.Add(observer);
      observersForBaseDefenseUCMutSet[id] = obsies;
    }
    public void RemoveBaseDefenseUCMutSetObserver(int id, IBaseDefenseUCMutSetEffectObserver observer) {
      if (observersForBaseDefenseUCMutSet.ContainsKey(id)) {
        var list = observersForBaseDefenseUCMutSet[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForBaseDefenseUCMutSet.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

  public void visitTerrainTileByLocationMutMapEffect(ITerrainTileByLocationMutMapEffect effect) {
    if (observersForTerrainTileByLocationMutMap.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<ITerrainTileByLocationMutMapEffectObserver>(observers)) {
        observer.OnTerrainTileByLocationMutMapEffect(effect);
      }
    }
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

  public void visitKamikazeTargetTTCStrongByLocationMutMapEffect(IKamikazeTargetTTCStrongByLocationMutMapEffect effect) {
    if (observersForKamikazeTargetTTCStrongByLocationMutMap.TryGetValue(effect.id, out var observers)) {
      foreach (var observer in new List<IKamikazeTargetTTCStrongByLocationMutMapEffectObserver>(observers)) {
        observer.OnKamikazeTargetTTCStrongByLocationMutMapEffect(effect);
      }
    }
  }
    public void AddKamikazeTargetTTCStrongByLocationMutMapObserver(int id, IKamikazeTargetTTCStrongByLocationMutMapEffectObserver observer) {
      List<IKamikazeTargetTTCStrongByLocationMutMapEffectObserver> obsies;
      if (!observersForKamikazeTargetTTCStrongByLocationMutMap.TryGetValue(id, out obsies)) {
        obsies = new List<IKamikazeTargetTTCStrongByLocationMutMapEffectObserver>();
      }
      obsies.Add(observer);
      observersForKamikazeTargetTTCStrongByLocationMutMap[id] = obsies;
    }
    public void RemoveKamikazeTargetTTCStrongByLocationMutMapObserver(int id, IKamikazeTargetTTCStrongByLocationMutMapEffectObserver observer) {
      if (observersForKamikazeTargetTTCStrongByLocationMutMap.ContainsKey(id)) {
        var map = observersForKamikazeTargetTTCStrongByLocationMutMap[id];
        map.Remove(observer);
        if (map.Count == 0) {
          observersForKamikazeTargetTTCStrongByLocationMutMap.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

  public void Broadcast(IEffect effect) {
    foreach (var obs in globalEffectObservers) {
      obs(effect);
    }
    effect.visitIEffect(this);
  }
}
         
}
