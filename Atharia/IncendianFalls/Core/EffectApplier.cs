using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class EffectApplier : IEffectVisitor ,
IRandEffectVisitor,
IHoldPositionImpulseEffectVisitor,
IWanderAICapabilityUCEffectVisitor,
ITutorialDefyCounterUCEffectVisitor,
IUnitEffectVisitor,
IIUnitComponentMutBunchEffectVisitor,
ILightningChargedUCEffectVisitor,
ILightningChargingUCEffectVisitor,
IDoomedUCEffectVisitor,
ITemporaryCloneImpulseEffectVisitor,
ITemporaryCloneAICapabilityUCEffectVisitor,
ISummonImpulseEffectVisitor,
ISummonAICapabilityUCEffectVisitor,
ISorcerousUCEffectVisitor,
IBaseOffenseUCEffectVisitor,
IBaseSightRangeUCEffectVisitor,
IBaseMovementTimeUCEffectVisitor,
IBaseDefenseUCEffectVisitor,
IBaseCombatTimeUCEffectVisitor,
IMiredUCEffectVisitor,
IMireImpulseEffectVisitor,
IEvaporateImpulseEffectVisitor,
ITimeCloneAICapabilityUCEffectVisitor,
IMoveImpulseEffectVisitor,
IKamikazeTargetTTCEffectVisitor,
IKamikazeJumpImpulseEffectVisitor,
IKamikazeTargetImpulseEffectVisitor,
IKamikazeAICapabilityUCEffectVisitor,
IInvincibilityUCEffectVisitor,
IGuardAICapabilityUCEffectVisitor,
INoImpulseEffectVisitor,
IFireImpulseEffectVisitor,
IDefyingUCEffectVisitor,
IDefyImpulseEffectVisitor,
ICounteringUCEffectVisitor,
ICounterImpulseEffectVisitor,
IUnleashBideImpulseEffectVisitor,
IContinueBidingImpulseEffectVisitor,
IStartBidingImpulseEffectVisitor,
IBideAICapabilityUCEffectVisitor,
IAttackImpulseEffectVisitor,
IPursueImpulseEffectVisitor,
IKillDirectiveEffectVisitor,
IAttackAICapabilityUCEffectVisitor,
IWarperTTCEffectVisitor,
ITimeAnchorTTCEffectVisitor,
ITerrainTileEffectVisitor,
IITerrainTileComponentMutBunchEffectVisitor,
ITerrainEffectVisitor,
ISimplePresenceTriggerTTCEffectVisitor,
IFireBombImpulseEffectVisitor,
IFireBombTTCEffectVisitor,
IMarkerTTCEffectVisitor,
ILevelLinkTTCEffectVisitor,
IMudTTCEffectVisitor,
IDirtTTCEffectVisitor,
IObsidianTTCEffectVisitor,
IDownStairsTTCEffectVisitor,
IUpStairsTTCEffectVisitor,
IWallTTCEffectVisitor,
IBloodTTCEffectVisitor,
IRocksTTCEffectVisitor,
ITreeTTCEffectVisitor,
IWaterTTCEffectVisitor,
IFloorTTCEffectVisitor,
ICaveWallTTCEffectVisitor,
ICaveTTCEffectVisitor,
IFallsTTCEffectVisitor,
IFireTTCEffectVisitor,
IObsidianFloorTTCEffectVisitor,
IMagmaTTCEffectVisitor,
ICliffTTCEffectVisitor,
IRavaNestTTCEffectVisitor,
ICliffLandingTTCEffectVisitor,
IStoneTTCEffectVisitor,
IGrassTTCEffectVisitor,
ILevelEffectVisitor,
ISpeedRingEffectVisitor,
IManaPotionEffectVisitor,
IWatEffectVisitor,
IIPreActingUCWeakMutBunchEffectVisitor,
IIPostActingUCWeakMutBunchEffectVisitor,
IIImpulseStrongMutBunchEffectVisitor,
IIItemStrongMutBunchEffectVisitor,
IItemTTCEffectVisitor,
IHealthPotionEffectVisitor,
IGlaiveEffectVisitor,
ISlowRodEffectVisitor,
IBlastRodEffectVisitor,
IArmorEffectVisitor,
ISquareCaveLevelControllerEffectVisitor,
IRavashrikeLevelControllerEffectVisitor,
IPentagonalCaveLevelControllerEffectVisitor,
IIncendianFallsLevelLinkerTTCEffectVisitor,
ICliffLevelControllerEffectVisitor,
IPreGauntletLevelControllerEffectVisitor,
IGauntletLevelControllerEffectVisitor,
ICommEffectVisitor,
IGameEffectVisitor,
IVolcaetusLevelControllerEffectVisitor,
ITutorial2LevelControllerEffectVisitor,
ITutorial1LevelControllerEffectVisitor,
IRetreatLevelControllerEffectVisitor,
ISotaventoLevelControllerEffectVisitor,
INestLevelControllerEffectVisitor,
ILakeLevelControllerEffectVisitor,
IEmberDeepLevelLinkerTTCEffectVisitor,
IDirtRoadLevelControllerEffectVisitor,
ICaveLevelControllerEffectVisitor,
IBridgesLevelControllerEffectVisitor,
IAncientTownLevelControllerEffectVisitor,
ICommMutListEffectVisitor,
ILocationMutListEffectVisitor,
IIRequestMutListEffectVisitor,
ILevelMutSetEffectVisitor,
IManaPotionStrongMutSetEffectVisitor,
IHealthPotionStrongMutSetEffectVisitor,
ISpeedRingStrongMutSetEffectVisitor,
IGlaiveStrongMutSetEffectVisitor,
ISlowRodStrongMutSetEffectVisitor,
IBlastRodStrongMutSetEffectVisitor,
IArmorStrongMutSetEffectVisitor,
IHoldPositionImpulseStrongMutSetEffectVisitor,
ITemporaryCloneImpulseStrongMutSetEffectVisitor,
ISummonImpulseStrongMutSetEffectVisitor,
IMireImpulseStrongMutSetEffectVisitor,
IEvaporateImpulseStrongMutSetEffectVisitor,
IMoveImpulseStrongMutSetEffectVisitor,
IKamikazeJumpImpulseStrongMutSetEffectVisitor,
IKamikazeTargetImpulseStrongMutSetEffectVisitor,
INoImpulseStrongMutSetEffectVisitor,
IFireImpulseStrongMutSetEffectVisitor,
IDefyImpulseStrongMutSetEffectVisitor,
ICounterImpulseStrongMutSetEffectVisitor,
IUnleashBideImpulseStrongMutSetEffectVisitor,
IContinueBidingImpulseStrongMutSetEffectVisitor,
IStartBidingImpulseStrongMutSetEffectVisitor,
IAttackImpulseStrongMutSetEffectVisitor,
IPursueImpulseStrongMutSetEffectVisitor,
IFireBombImpulseStrongMutSetEffectVisitor,
ILightningChargedUCWeakMutSetEffectVisitor,
ITimeCloneAICapabilityUCWeakMutSetEffectVisitor,
IDoomedUCWeakMutSetEffectVisitor,
IMiredUCWeakMutSetEffectVisitor,
IInvincibilityUCWeakMutSetEffectVisitor,
IDefyingUCWeakMutSetEffectVisitor,
ICounteringUCWeakMutSetEffectVisitor,
IAttackAICapabilityUCWeakMutSetEffectVisitor,
IUnitMutSetEffectVisitor,
ISimplePresenceTriggerTTCMutSetEffectVisitor,
IItemTTCMutSetEffectVisitor,
IKamikazeTargetTTCMutSetEffectVisitor,
IWarperTTCMutSetEffectVisitor,
ITimeAnchorTTCMutSetEffectVisitor,
IFireBombTTCMutSetEffectVisitor,
IMarkerTTCMutSetEffectVisitor,
ILevelLinkTTCMutSetEffectVisitor,
IMudTTCMutSetEffectVisitor,
IDirtTTCMutSetEffectVisitor,
IObsidianTTCMutSetEffectVisitor,
IDownStairsTTCMutSetEffectVisitor,
IUpStairsTTCMutSetEffectVisitor,
IWallTTCMutSetEffectVisitor,
IBloodTTCMutSetEffectVisitor,
IRocksTTCMutSetEffectVisitor,
ITreeTTCMutSetEffectVisitor,
IWaterTTCMutSetEffectVisitor,
IFloorTTCMutSetEffectVisitor,
ICaveWallTTCMutSetEffectVisitor,
ICaveTTCMutSetEffectVisitor,
IFallsTTCMutSetEffectVisitor,
IFireTTCMutSetEffectVisitor,
IObsidianFloorTTCMutSetEffectVisitor,
IMagmaTTCMutSetEffectVisitor,
ICliffTTCMutSetEffectVisitor,
IRavaNestTTCMutSetEffectVisitor,
ICliffLandingTTCMutSetEffectVisitor,
IStoneTTCMutSetEffectVisitor,
IGrassTTCMutSetEffectVisitor,
IIncendianFallsLevelLinkerTTCMutSetEffectVisitor,
IEmberDeepLevelLinkerTTCMutSetEffectVisitor,
ITutorialDefyCounterUCMutSetEffectVisitor,
ILightningChargingUCMutSetEffectVisitor,
IWanderAICapabilityUCMutSetEffectVisitor,
ITemporaryCloneAICapabilityUCMutSetEffectVisitor,
ISummonAICapabilityUCMutSetEffectVisitor,
IKamikazeAICapabilityUCMutSetEffectVisitor,
IGuardAICapabilityUCMutSetEffectVisitor,
ITimeCloneAICapabilityUCMutSetEffectVisitor,
IDoomedUCMutSetEffectVisitor,
IMiredUCMutSetEffectVisitor,
IAttackAICapabilityUCMutSetEffectVisitor,
ICounteringUCMutSetEffectVisitor,
ILightningChargedUCMutSetEffectVisitor,
IInvincibilityUCMutSetEffectVisitor,
IDefyingUCMutSetEffectVisitor,
IBideAICapabilityUCMutSetEffectVisitor,
IBaseSightRangeUCMutSetEffectVisitor,
IBaseMovementTimeUCMutSetEffectVisitor,
IBaseCombatTimeUCMutSetEffectVisitor,
IManaPotionMutSetEffectVisitor,
IHealthPotionMutSetEffectVisitor,
ISpeedRingMutSetEffectVisitor,
IGlaiveMutSetEffectVisitor,
ISlowRodMutSetEffectVisitor,
IBlastRodMutSetEffectVisitor,
IArmorMutSetEffectVisitor,
ISorcerousUCMutSetEffectVisitor,
IBaseOffenseUCMutSetEffectVisitor,
IBaseDefenseUCMutSetEffectVisitor,
ITerrainTileByLocationMutMapEffectVisitor,
IKamikazeTargetTTCStrongByLocationMutMapEffectVisitor {
  Root root;
  public EffectApplier(Root root) {
    this.root = root;
  }


public void visitRandEffect(IRandEffect effect) { effect.visitIRandEffect(this); }
  public void visitRandCreateEffect(RandCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectRandCreateWithId(effect.id
,  effect.incarnation.rand    );

}

  public void visitRandDeleteEffect(RandDeleteEffect effect) {
    root.EffectRandDelete(effect.id);
  }

     
  public void visitRandSetRandEffect(RandSetRandEffect effect) {
    root.EffectRandSetRand(
      effect.id,
  effect.newValue
    );
  }

public void visitHoldPositionImpulseEffect(IHoldPositionImpulseEffect effect) { effect.visitIHoldPositionImpulseEffect(this); }
  public void visitHoldPositionImpulseCreateEffect(HoldPositionImpulseCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectHoldPositionImpulseCreateWithId(effect.id
,  effect.incarnation.weight
,  effect.incarnation.duration    );

}

  public void visitHoldPositionImpulseDeleteEffect(HoldPositionImpulseDeleteEffect effect) {
    root.EffectHoldPositionImpulseDelete(effect.id);
  }

     
public void visitWanderAICapabilityUCEffect(IWanderAICapabilityUCEffect effect) { effect.visitIWanderAICapabilityUCEffect(this); }
  public void visitWanderAICapabilityUCCreateEffect(WanderAICapabilityUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectWanderAICapabilityUCCreateWithId(effect.id
    );

}

  public void visitWanderAICapabilityUCDeleteEffect(WanderAICapabilityUCDeleteEffect effect) {
    root.EffectWanderAICapabilityUCDelete(effect.id);
  }

     
public void visitTutorialDefyCounterUCEffect(ITutorialDefyCounterUCEffect effect) { effect.visitITutorialDefyCounterUCEffect(this); }
  public void visitTutorialDefyCounterUCCreateEffect(TutorialDefyCounterUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectTutorialDefyCounterUCCreateWithId(effect.id
,  effect.incarnation.numDefiesRemaining
,  effect.incarnation.onChangeTriggerName    );

}

  public void visitTutorialDefyCounterUCDeleteEffect(TutorialDefyCounterUCDeleteEffect effect) {
    root.EffectTutorialDefyCounterUCDelete(effect.id);
  }

     
  public void visitTutorialDefyCounterUCSetNumDefiesRemainingEffect(TutorialDefyCounterUCSetNumDefiesRemainingEffect effect) {
    root.EffectTutorialDefyCounterUCSetNumDefiesRemaining(
      effect.id,
  effect.newValue
    );
  }

public void visitUnitEffect(IUnitEffect effect) { effect.visitIUnitEffect(this); }
  public void visitUnitCreateEffect(UnitCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectUnitCreateWithId(effect.id
,  effect.incarnation.evvent
,  effect.incarnation.lifeEndTime
,  effect.incarnation.location
,  effect.incarnation.classId
,  effect.incarnation.nextActionTime
,  effect.incarnation.hp
,  effect.incarnation.maxHp
,  root.GetIUnitComponentMutBunch(effect.incarnation.components)
,  effect.incarnation.good    );

}

  public void visitUnitDeleteEffect(UnitDeleteEffect effect) {
    root.EffectUnitDelete(effect.id);
  }

     
  public void visitUnitSetEvventEffect(UnitSetEvventEffect effect) {
    root.EffectUnitSetEvvent(
      effect.id,
  effect.newValue
    );
  }

  public void visitUnitSetLifeEndTimeEffect(UnitSetLifeEndTimeEffect effect) {
    root.EffectUnitSetLifeEndTime(
      effect.id,
  effect.newValue
    );
  }

  public void visitUnitSetLocationEffect(UnitSetLocationEffect effect) {
    root.EffectUnitSetLocation(
      effect.id,
  effect.newValue
    );
  }

  public void visitUnitSetNextActionTimeEffect(UnitSetNextActionTimeEffect effect) {
    root.EffectUnitSetNextActionTime(
      effect.id,
  effect.newValue
    );
  }

  public void visitUnitSetHpEffect(UnitSetHpEffect effect) {
    root.EffectUnitSetHp(
      effect.id,
  effect.newValue
    );
  }

  public void visitUnitSetMaxHpEffect(UnitSetMaxHpEffect effect) {
    root.EffectUnitSetMaxHp(
      effect.id,
  effect.newValue
    );
  }

public void visitIUnitComponentMutBunchEffect(IIUnitComponentMutBunchEffect effect) { effect.visitIIUnitComponentMutBunchEffect(this); }
  public void visitIUnitComponentMutBunchCreateEffect(IUnitComponentMutBunchCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectIUnitComponentMutBunchCreateWithId(effect.id
,  root.GetTutorialDefyCounterUCMutSet(effect.incarnation.membersTutorialDefyCounterUCMutSet)
,  root.GetLightningChargingUCMutSet(effect.incarnation.membersLightningChargingUCMutSet)
,  root.GetWanderAICapabilityUCMutSet(effect.incarnation.membersWanderAICapabilityUCMutSet)
,  root.GetTemporaryCloneAICapabilityUCMutSet(effect.incarnation.membersTemporaryCloneAICapabilityUCMutSet)
,  root.GetSummonAICapabilityUCMutSet(effect.incarnation.membersSummonAICapabilityUCMutSet)
,  root.GetKamikazeAICapabilityUCMutSet(effect.incarnation.membersKamikazeAICapabilityUCMutSet)
,  root.GetGuardAICapabilityUCMutSet(effect.incarnation.membersGuardAICapabilityUCMutSet)
,  root.GetTimeCloneAICapabilityUCMutSet(effect.incarnation.membersTimeCloneAICapabilityUCMutSet)
,  root.GetDoomedUCMutSet(effect.incarnation.membersDoomedUCMutSet)
,  root.GetMiredUCMutSet(effect.incarnation.membersMiredUCMutSet)
,  root.GetAttackAICapabilityUCMutSet(effect.incarnation.membersAttackAICapabilityUCMutSet)
,  root.GetCounteringUCMutSet(effect.incarnation.membersCounteringUCMutSet)
,  root.GetLightningChargedUCMutSet(effect.incarnation.membersLightningChargedUCMutSet)
,  root.GetInvincibilityUCMutSet(effect.incarnation.membersInvincibilityUCMutSet)
,  root.GetDefyingUCMutSet(effect.incarnation.membersDefyingUCMutSet)
,  root.GetBideAICapabilityUCMutSet(effect.incarnation.membersBideAICapabilityUCMutSet)
,  root.GetBaseSightRangeUCMutSet(effect.incarnation.membersBaseSightRangeUCMutSet)
,  root.GetBaseMovementTimeUCMutSet(effect.incarnation.membersBaseMovementTimeUCMutSet)
,  root.GetBaseCombatTimeUCMutSet(effect.incarnation.membersBaseCombatTimeUCMutSet)
,  root.GetManaPotionMutSet(effect.incarnation.membersManaPotionMutSet)
,  root.GetHealthPotionMutSet(effect.incarnation.membersHealthPotionMutSet)
,  root.GetSpeedRingMutSet(effect.incarnation.membersSpeedRingMutSet)
,  root.GetGlaiveMutSet(effect.incarnation.membersGlaiveMutSet)
,  root.GetSlowRodMutSet(effect.incarnation.membersSlowRodMutSet)
,  root.GetBlastRodMutSet(effect.incarnation.membersBlastRodMutSet)
,  root.GetArmorMutSet(effect.incarnation.membersArmorMutSet)
,  root.GetSorcerousUCMutSet(effect.incarnation.membersSorcerousUCMutSet)
,  root.GetBaseOffenseUCMutSet(effect.incarnation.membersBaseOffenseUCMutSet)
,  root.GetBaseDefenseUCMutSet(effect.incarnation.membersBaseDefenseUCMutSet)    );

}

  public void visitIUnitComponentMutBunchDeleteEffect(IUnitComponentMutBunchDeleteEffect effect) {
    root.EffectIUnitComponentMutBunchDelete(effect.id);
  }

     
public void visitLightningChargedUCEffect(ILightningChargedUCEffect effect) { effect.visitILightningChargedUCEffect(this); }
  public void visitLightningChargedUCCreateEffect(LightningChargedUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectLightningChargedUCCreateWithId(effect.id
    );

}

  public void visitLightningChargedUCDeleteEffect(LightningChargedUCDeleteEffect effect) {
    root.EffectLightningChargedUCDelete(effect.id);
  }

     
public void visitLightningChargingUCEffect(ILightningChargingUCEffect effect) { effect.visitILightningChargingUCEffect(this); }
  public void visitLightningChargingUCCreateEffect(LightningChargingUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectLightningChargingUCCreateWithId(effect.id
    );

}

  public void visitLightningChargingUCDeleteEffect(LightningChargingUCDeleteEffect effect) {
    root.EffectLightningChargingUCDelete(effect.id);
  }

     
public void visitDoomedUCEffect(IDoomedUCEffect effect) { effect.visitIDoomedUCEffect(this); }
  public void visitDoomedUCCreateEffect(DoomedUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectDoomedUCCreateWithId(effect.id
,  effect.incarnation.deathTime    );

}

  public void visitDoomedUCDeleteEffect(DoomedUCDeleteEffect effect) {
    root.EffectDoomedUCDelete(effect.id);
  }

     
public void visitTemporaryCloneImpulseEffect(ITemporaryCloneImpulseEffect effect) { effect.visitITemporaryCloneImpulseEffect(this); }
  public void visitTemporaryCloneImpulseCreateEffect(TemporaryCloneImpulseCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectTemporaryCloneImpulseCreateWithId(effect.id
,  effect.incarnation.weight
,  effect.incarnation.blueprintName
,  effect.incarnation.location
,  effect.incarnation.hp    );

}

  public void visitTemporaryCloneImpulseDeleteEffect(TemporaryCloneImpulseDeleteEffect effect) {
    root.EffectTemporaryCloneImpulseDelete(effect.id);
  }

     
public void visitTemporaryCloneAICapabilityUCEffect(ITemporaryCloneAICapabilityUCEffect effect) { effect.visitITemporaryCloneAICapabilityUCEffect(this); }
  public void visitTemporaryCloneAICapabilityUCCreateEffect(TemporaryCloneAICapabilityUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectTemporaryCloneAICapabilityUCCreateWithId(effect.id
,  effect.incarnation.blueprintName
,  effect.incarnation.charges    );

}

  public void visitTemporaryCloneAICapabilityUCDeleteEffect(TemporaryCloneAICapabilityUCDeleteEffect effect) {
    root.EffectTemporaryCloneAICapabilityUCDelete(effect.id);
  }

     
  public void visitTemporaryCloneAICapabilityUCSetChargesEffect(TemporaryCloneAICapabilityUCSetChargesEffect effect) {
    root.EffectTemporaryCloneAICapabilityUCSetCharges(
      effect.id,
  effect.newValue
    );
  }

public void visitSummonImpulseEffect(ISummonImpulseEffect effect) { effect.visitISummonImpulseEffect(this); }
  public void visitSummonImpulseCreateEffect(SummonImpulseCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectSummonImpulseCreateWithId(effect.id
,  effect.incarnation.weight
,  effect.incarnation.blueprintName
,  effect.incarnation.location    );

}

  public void visitSummonImpulseDeleteEffect(SummonImpulseDeleteEffect effect) {
    root.EffectSummonImpulseDelete(effect.id);
  }

     
public void visitSummonAICapabilityUCEffect(ISummonAICapabilityUCEffect effect) { effect.visitISummonAICapabilityUCEffect(this); }
  public void visitSummonAICapabilityUCCreateEffect(SummonAICapabilityUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectSummonAICapabilityUCCreateWithId(effect.id
,  effect.incarnation.blueprintName
,  effect.incarnation.charges    );

}

  public void visitSummonAICapabilityUCDeleteEffect(SummonAICapabilityUCDeleteEffect effect) {
    root.EffectSummonAICapabilityUCDelete(effect.id);
  }

     
  public void visitSummonAICapabilityUCSetChargesEffect(SummonAICapabilityUCSetChargesEffect effect) {
    root.EffectSummonAICapabilityUCSetCharges(
      effect.id,
  effect.newValue
    );
  }

public void visitSorcerousUCEffect(ISorcerousUCEffect effect) { effect.visitISorcerousUCEffect(this); }
  public void visitSorcerousUCCreateEffect(SorcerousUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectSorcerousUCCreateWithId(effect.id
,  effect.incarnation.mp
,  effect.incarnation.maxMp    );

}

  public void visitSorcerousUCDeleteEffect(SorcerousUCDeleteEffect effect) {
    root.EffectSorcerousUCDelete(effect.id);
  }

     
  public void visitSorcerousUCSetMpEffect(SorcerousUCSetMpEffect effect) {
    root.EffectSorcerousUCSetMp(
      effect.id,
  effect.newValue
    );
  }

  public void visitSorcerousUCSetMaxMpEffect(SorcerousUCSetMaxMpEffect effect) {
    root.EffectSorcerousUCSetMaxMp(
      effect.id,
  effect.newValue
    );
  }

public void visitBaseOffenseUCEffect(IBaseOffenseUCEffect effect) { effect.visitIBaseOffenseUCEffect(this); }
  public void visitBaseOffenseUCCreateEffect(BaseOffenseUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectBaseOffenseUCCreateWithId(effect.id
,  effect.incarnation.outgoingDamageAddConstant
,  effect.incarnation.outgoingDamageMultiplierPercent    );

}

  public void visitBaseOffenseUCDeleteEffect(BaseOffenseUCDeleteEffect effect) {
    root.EffectBaseOffenseUCDelete(effect.id);
  }

     
public void visitBaseSightRangeUCEffect(IBaseSightRangeUCEffect effect) { effect.visitIBaseSightRangeUCEffect(this); }
  public void visitBaseSightRangeUCCreateEffect(BaseSightRangeUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectBaseSightRangeUCCreateWithId(effect.id
,  effect.incarnation.sightRangeAddConstant
,  effect.incarnation.sightRangeMultiplierPercent    );

}

  public void visitBaseSightRangeUCDeleteEffect(BaseSightRangeUCDeleteEffect effect) {
    root.EffectBaseSightRangeUCDelete(effect.id);
  }

     
public void visitBaseMovementTimeUCEffect(IBaseMovementTimeUCEffect effect) { effect.visitIBaseMovementTimeUCEffect(this); }
  public void visitBaseMovementTimeUCCreateEffect(BaseMovementTimeUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectBaseMovementTimeUCCreateWithId(effect.id
,  effect.incarnation.movementTimeAddConstant
,  effect.incarnation.movementTimeMultiplierPercent    );

}

  public void visitBaseMovementTimeUCDeleteEffect(BaseMovementTimeUCDeleteEffect effect) {
    root.EffectBaseMovementTimeUCDelete(effect.id);
  }

     
public void visitBaseDefenseUCEffect(IBaseDefenseUCEffect effect) { effect.visitIBaseDefenseUCEffect(this); }
  public void visitBaseDefenseUCCreateEffect(BaseDefenseUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectBaseDefenseUCCreateWithId(effect.id
,  effect.incarnation.incomingDamageAddConstant
,  effect.incarnation.incomingDamageMultiplierPercent    );

}

  public void visitBaseDefenseUCDeleteEffect(BaseDefenseUCDeleteEffect effect) {
    root.EffectBaseDefenseUCDelete(effect.id);
  }

     
public void visitBaseCombatTimeUCEffect(IBaseCombatTimeUCEffect effect) { effect.visitIBaseCombatTimeUCEffect(this); }
  public void visitBaseCombatTimeUCCreateEffect(BaseCombatTimeUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectBaseCombatTimeUCCreateWithId(effect.id
,  effect.incarnation.combatTimeAddConstant
,  effect.incarnation.combatTimeMultiplierPercent    );

}

  public void visitBaseCombatTimeUCDeleteEffect(BaseCombatTimeUCDeleteEffect effect) {
    root.EffectBaseCombatTimeUCDelete(effect.id);
  }

     
public void visitMiredUCEffect(IMiredUCEffect effect) { effect.visitIMiredUCEffect(this); }
  public void visitMiredUCCreateEffect(MiredUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectMiredUCCreateWithId(effect.id
    );

}

  public void visitMiredUCDeleteEffect(MiredUCDeleteEffect effect) {
    root.EffectMiredUCDelete(effect.id);
  }

     
public void visitMireImpulseEffect(IMireImpulseEffect effect) { effect.visitIMireImpulseEffect(this); }
  public void visitMireImpulseCreateEffect(MireImpulseCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectMireImpulseCreateWithId(effect.id
,  effect.incarnation.weight
,  root.GetUnit(effect.incarnation.targetUnit)    );

}

  public void visitMireImpulseDeleteEffect(MireImpulseDeleteEffect effect) {
    root.EffectMireImpulseDelete(effect.id);
  }

     
public void visitEvaporateImpulseEffect(IEvaporateImpulseEffect effect) { effect.visitIEvaporateImpulseEffect(this); }
  public void visitEvaporateImpulseCreateEffect(EvaporateImpulseCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectEvaporateImpulseCreateWithId(effect.id
    );

}

  public void visitEvaporateImpulseDeleteEffect(EvaporateImpulseDeleteEffect effect) {
    root.EffectEvaporateImpulseDelete(effect.id);
  }

     
public void visitTimeCloneAICapabilityUCEffect(ITimeCloneAICapabilityUCEffect effect) { effect.visitITimeCloneAICapabilityUCEffect(this); }
  public void visitTimeCloneAICapabilityUCCreateEffect(TimeCloneAICapabilityUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectTimeCloneAICapabilityUCCreateWithId(effect.id
,  root.GetIRequestMutListOrNull(effect.incarnation.script)    );

}

  public void visitTimeCloneAICapabilityUCDeleteEffect(TimeCloneAICapabilityUCDeleteEffect effect) {
    root.EffectTimeCloneAICapabilityUCDelete(effect.id);
  }

     
public void visitMoveImpulseEffect(IMoveImpulseEffect effect) { effect.visitIMoveImpulseEffect(this); }
  public void visitMoveImpulseCreateEffect(MoveImpulseCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectMoveImpulseCreateWithId(effect.id
,  effect.incarnation.weight
,  effect.incarnation.stepLocation    );

}

  public void visitMoveImpulseDeleteEffect(MoveImpulseDeleteEffect effect) {
    root.EffectMoveImpulseDelete(effect.id);
  }

     
public void visitKamikazeTargetTTCEffect(IKamikazeTargetTTCEffect effect) { effect.visitIKamikazeTargetTTCEffect(this); }
  public void visitKamikazeTargetTTCCreateEffect(KamikazeTargetTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectKamikazeTargetTTCCreateWithId(effect.id
,  root.GetKamikazeAICapabilityUC(effect.incarnation.capability)    );

}

  public void visitKamikazeTargetTTCDeleteEffect(KamikazeTargetTTCDeleteEffect effect) {
    root.EffectKamikazeTargetTTCDelete(effect.id);
  }

     
public void visitKamikazeJumpImpulseEffect(IKamikazeJumpImpulseEffect effect) { effect.visitIKamikazeJumpImpulseEffect(this); }
  public void visitKamikazeJumpImpulseCreateEffect(KamikazeJumpImpulseCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectKamikazeJumpImpulseCreateWithId(effect.id
,  effect.incarnation.weight
,  root.GetKamikazeAICapabilityUC(effect.incarnation.capability)
,  effect.incarnation.jumpTarget    );

}

  public void visitKamikazeJumpImpulseDeleteEffect(KamikazeJumpImpulseDeleteEffect effect) {
    root.EffectKamikazeJumpImpulseDelete(effect.id);
  }

     
public void visitKamikazeTargetImpulseEffect(IKamikazeTargetImpulseEffect effect) { effect.visitIKamikazeTargetImpulseEffect(this); }
  public void visitKamikazeTargetImpulseCreateEffect(KamikazeTargetImpulseCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectKamikazeTargetImpulseCreateWithId(effect.id
,  effect.incarnation.weight
,  root.GetKamikazeAICapabilityUC(effect.incarnation.capability)
,  effect.incarnation.targetLocationCenter
,  effect.incarnation.targetLocations    );

}

  public void visitKamikazeTargetImpulseDeleteEffect(KamikazeTargetImpulseDeleteEffect effect) {
    root.EffectKamikazeTargetImpulseDelete(effect.id);
  }

     
public void visitKamikazeAICapabilityUCEffect(IKamikazeAICapabilityUCEffect effect) { effect.visitIKamikazeAICapabilityUCEffect(this); }
  public void visitKamikazeAICapabilityUCCreateEffect(KamikazeAICapabilityUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectKamikazeAICapabilityUCCreateWithId(effect.id
,  root.GetKamikazeTargetTTCStrongByLocationMutMap(effect.incarnation.targetByLocation)
,  effect.incarnation.targetLocationCenter    );

}

  public void visitKamikazeAICapabilityUCDeleteEffect(KamikazeAICapabilityUCDeleteEffect effect) {
    root.EffectKamikazeAICapabilityUCDelete(effect.id);
  }

     
  public void visitKamikazeAICapabilityUCSetTargetByLocationEffect(KamikazeAICapabilityUCSetTargetByLocationEffect effect) {
    root.EffectKamikazeAICapabilityUCSetTargetByLocation(
      effect.id,
  root.GetKamikazeTargetTTCStrongByLocationMutMap(effect.newValue)
    );
  }

  public void visitKamikazeAICapabilityUCSetTargetLocationCenterEffect(KamikazeAICapabilityUCSetTargetLocationCenterEffect effect) {
    root.EffectKamikazeAICapabilityUCSetTargetLocationCenter(
      effect.id,
  effect.newValue
    );
  }

public void visitInvincibilityUCEffect(IInvincibilityUCEffect effect) { effect.visitIInvincibilityUCEffect(this); }
  public void visitInvincibilityUCCreateEffect(InvincibilityUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectInvincibilityUCCreateWithId(effect.id
    );

}

  public void visitInvincibilityUCDeleteEffect(InvincibilityUCDeleteEffect effect) {
    root.EffectInvincibilityUCDelete(effect.id);
  }

     
public void visitGuardAICapabilityUCEffect(IGuardAICapabilityUCEffect effect) { effect.visitIGuardAICapabilityUCEffect(this); }
  public void visitGuardAICapabilityUCCreateEffect(GuardAICapabilityUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectGuardAICapabilityUCCreateWithId(effect.id
,  effect.incarnation.guardCenterLocation
,  effect.incarnation.guardRadius    );

}

  public void visitGuardAICapabilityUCDeleteEffect(GuardAICapabilityUCDeleteEffect effect) {
    root.EffectGuardAICapabilityUCDelete(effect.id);
  }

     
public void visitNoImpulseEffect(INoImpulseEffect effect) { effect.visitINoImpulseEffect(this); }
  public void visitNoImpulseCreateEffect(NoImpulseCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectNoImpulseCreateWithId(effect.id
    );

}

  public void visitNoImpulseDeleteEffect(NoImpulseDeleteEffect effect) {
    root.EffectNoImpulseDelete(effect.id);
  }

     
public void visitFireImpulseEffect(IFireImpulseEffect effect) { effect.visitIFireImpulseEffect(this); }
  public void visitFireImpulseCreateEffect(FireImpulseCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectFireImpulseCreateWithId(effect.id
,  effect.incarnation.weight
,  root.GetUnit(effect.incarnation.targetUnit)    );

}

  public void visitFireImpulseDeleteEffect(FireImpulseDeleteEffect effect) {
    root.EffectFireImpulseDelete(effect.id);
  }

     
public void visitDefyingUCEffect(IDefyingUCEffect effect) { effect.visitIDefyingUCEffect(this); }
  public void visitDefyingUCCreateEffect(DefyingUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectDefyingUCCreateWithId(effect.id
    );

}

  public void visitDefyingUCDeleteEffect(DefyingUCDeleteEffect effect) {
    root.EffectDefyingUCDelete(effect.id);
  }

     
public void visitDefyImpulseEffect(IDefyImpulseEffect effect) { effect.visitIDefyImpulseEffect(this); }
  public void visitDefyImpulseCreateEffect(DefyImpulseCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectDefyImpulseCreateWithId(effect.id
,  effect.incarnation.weight    );

}

  public void visitDefyImpulseDeleteEffect(DefyImpulseDeleteEffect effect) {
    root.EffectDefyImpulseDelete(effect.id);
  }

     
public void visitCounteringUCEffect(ICounteringUCEffect effect) { effect.visitICounteringUCEffect(this); }
  public void visitCounteringUCCreateEffect(CounteringUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectCounteringUCCreateWithId(effect.id
    );

}

  public void visitCounteringUCDeleteEffect(CounteringUCDeleteEffect effect) {
    root.EffectCounteringUCDelete(effect.id);
  }

     
public void visitCounterImpulseEffect(ICounterImpulseEffect effect) { effect.visitICounterImpulseEffect(this); }
  public void visitCounterImpulseCreateEffect(CounterImpulseCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectCounterImpulseCreateWithId(effect.id
,  effect.incarnation.weight    );

}

  public void visitCounterImpulseDeleteEffect(CounterImpulseDeleteEffect effect) {
    root.EffectCounterImpulseDelete(effect.id);
  }

     
public void visitUnleashBideImpulseEffect(IUnleashBideImpulseEffect effect) { effect.visitIUnleashBideImpulseEffect(this); }
  public void visitUnleashBideImpulseCreateEffect(UnleashBideImpulseCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectUnleashBideImpulseCreateWithId(effect.id
,  effect.incarnation.weight    );

}

  public void visitUnleashBideImpulseDeleteEffect(UnleashBideImpulseDeleteEffect effect) {
    root.EffectUnleashBideImpulseDelete(effect.id);
  }

     
public void visitContinueBidingImpulseEffect(IContinueBidingImpulseEffect effect) { effect.visitIContinueBidingImpulseEffect(this); }
  public void visitContinueBidingImpulseCreateEffect(ContinueBidingImpulseCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectContinueBidingImpulseCreateWithId(effect.id
,  effect.incarnation.weight    );

}

  public void visitContinueBidingImpulseDeleteEffect(ContinueBidingImpulseDeleteEffect effect) {
    root.EffectContinueBidingImpulseDelete(effect.id);
  }

     
public void visitStartBidingImpulseEffect(IStartBidingImpulseEffect effect) { effect.visitIStartBidingImpulseEffect(this); }
  public void visitStartBidingImpulseCreateEffect(StartBidingImpulseCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectStartBidingImpulseCreateWithId(effect.id
,  effect.incarnation.weight    );

}

  public void visitStartBidingImpulseDeleteEffect(StartBidingImpulseDeleteEffect effect) {
    root.EffectStartBidingImpulseDelete(effect.id);
  }

     
public void visitBideAICapabilityUCEffect(IBideAICapabilityUCEffect effect) { effect.visitIBideAICapabilityUCEffect(this); }
  public void visitBideAICapabilityUCCreateEffect(BideAICapabilityUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectBideAICapabilityUCCreateWithId(effect.id
,  effect.incarnation.charge    );

}

  public void visitBideAICapabilityUCDeleteEffect(BideAICapabilityUCDeleteEffect effect) {
    root.EffectBideAICapabilityUCDelete(effect.id);
  }

     
  public void visitBideAICapabilityUCSetChargeEffect(BideAICapabilityUCSetChargeEffect effect) {
    root.EffectBideAICapabilityUCSetCharge(
      effect.id,
  effect.newValue
    );
  }

public void visitAttackImpulseEffect(IAttackImpulseEffect effect) { effect.visitIAttackImpulseEffect(this); }
  public void visitAttackImpulseCreateEffect(AttackImpulseCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectAttackImpulseCreateWithId(effect.id
,  effect.incarnation.weight
,  root.GetUnit(effect.incarnation.targetUnit)    );

}

  public void visitAttackImpulseDeleteEffect(AttackImpulseDeleteEffect effect) {
    root.EffectAttackImpulseDelete(effect.id);
  }

     
public void visitPursueImpulseEffect(IPursueImpulseEffect effect) { effect.visitIPursueImpulseEffect(this); }
  public void visitPursueImpulseCreateEffect(PursueImpulseCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectPursueImpulseCreateWithId(effect.id
,  effect.incarnation.weight
,  effect.incarnation.isClearPath    );

}

  public void visitPursueImpulseDeleteEffect(PursueImpulseDeleteEffect effect) {
    root.EffectPursueImpulseDelete(effect.id);
  }

     
public void visitKillDirectiveEffect(IKillDirectiveEffect effect) { effect.visitIKillDirectiveEffect(this); }
  public void visitKillDirectiveCreateEffect(KillDirectiveCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectKillDirectiveCreateWithId(effect.id
,  root.GetUnitOrNull(effect.incarnation.targetUnit)
,  root.GetLocationMutList(effect.incarnation.pathToLastSeenLocation)    );

}

  public void visitKillDirectiveDeleteEffect(KillDirectiveDeleteEffect effect) {
    root.EffectKillDirectiveDelete(effect.id);
  }

     
public void visitAttackAICapabilityUCEffect(IAttackAICapabilityUCEffect effect) { effect.visitIAttackAICapabilityUCEffect(this); }
  public void visitAttackAICapabilityUCCreateEffect(AttackAICapabilityUCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectAttackAICapabilityUCCreateWithId(effect.id
,  root.GetKillDirectiveOrNull(effect.incarnation.killDirective)    );

}

  public void visitAttackAICapabilityUCDeleteEffect(AttackAICapabilityUCDeleteEffect effect) {
    root.EffectAttackAICapabilityUCDelete(effect.id);
  }

     
  public void visitAttackAICapabilityUCSetKillDirectiveEffect(AttackAICapabilityUCSetKillDirectiveEffect effect) {
    root.EffectAttackAICapabilityUCSetKillDirective(
      effect.id,
  root.GetKillDirectiveOrNull(effect.newValue)
    );
  }

public void visitWarperTTCEffect(IWarperTTCEffect effect) { effect.visitIWarperTTCEffect(this); }
  public void visitWarperTTCCreateEffect(WarperTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectWarperTTCCreateWithId(effect.id
,  effect.incarnation.destinationLocation    );

}

  public void visitWarperTTCDeleteEffect(WarperTTCDeleteEffect effect) {
    root.EffectWarperTTCDelete(effect.id);
  }

     
public void visitTimeAnchorTTCEffect(ITimeAnchorTTCEffect effect) { effect.visitITimeAnchorTTCEffect(this); }
  public void visitTimeAnchorTTCCreateEffect(TimeAnchorTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectTimeAnchorTTCCreateWithId(effect.id
,  effect.incarnation.pastVersion    );

}

  public void visitTimeAnchorTTCDeleteEffect(TimeAnchorTTCDeleteEffect effect) {
    root.EffectTimeAnchorTTCDelete(effect.id);
  }

     
public void visitTerrainTileEffect(ITerrainTileEffect effect) { effect.visitITerrainTileEffect(this); }
  public void visitTerrainTileCreateEffect(TerrainTileCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectTerrainTileCreateWithId(effect.id
,  effect.incarnation.evvent
,  effect.incarnation.elevation
,  root.GetITerrainTileComponentMutBunch(effect.incarnation.components)    );

}

  public void visitTerrainTileDeleteEffect(TerrainTileDeleteEffect effect) {
    root.EffectTerrainTileDelete(effect.id);
  }

     
  public void visitTerrainTileSetEvventEffect(TerrainTileSetEvventEffect effect) {
    root.EffectTerrainTileSetEvvent(
      effect.id,
  effect.newValue
    );
  }

  public void visitTerrainTileSetElevationEffect(TerrainTileSetElevationEffect effect) {
    root.EffectTerrainTileSetElevation(
      effect.id,
  effect.newValue
    );
  }

public void visitITerrainTileComponentMutBunchEffect(IITerrainTileComponentMutBunchEffect effect) { effect.visitIITerrainTileComponentMutBunchEffect(this); }
  public void visitITerrainTileComponentMutBunchCreateEffect(ITerrainTileComponentMutBunchCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectITerrainTileComponentMutBunchCreateWithId(effect.id
,  root.GetSimplePresenceTriggerTTCMutSet(effect.incarnation.membersSimplePresenceTriggerTTCMutSet)
,  root.GetItemTTCMutSet(effect.incarnation.membersItemTTCMutSet)
,  root.GetKamikazeTargetTTCMutSet(effect.incarnation.membersKamikazeTargetTTCMutSet)
,  root.GetWarperTTCMutSet(effect.incarnation.membersWarperTTCMutSet)
,  root.GetTimeAnchorTTCMutSet(effect.incarnation.membersTimeAnchorTTCMutSet)
,  root.GetFireBombTTCMutSet(effect.incarnation.membersFireBombTTCMutSet)
,  root.GetMarkerTTCMutSet(effect.incarnation.membersMarkerTTCMutSet)
,  root.GetLevelLinkTTCMutSet(effect.incarnation.membersLevelLinkTTCMutSet)
,  root.GetMudTTCMutSet(effect.incarnation.membersMudTTCMutSet)
,  root.GetDirtTTCMutSet(effect.incarnation.membersDirtTTCMutSet)
,  root.GetObsidianTTCMutSet(effect.incarnation.membersObsidianTTCMutSet)
,  root.GetDownStairsTTCMutSet(effect.incarnation.membersDownStairsTTCMutSet)
,  root.GetUpStairsTTCMutSet(effect.incarnation.membersUpStairsTTCMutSet)
,  root.GetWallTTCMutSet(effect.incarnation.membersWallTTCMutSet)
,  root.GetBloodTTCMutSet(effect.incarnation.membersBloodTTCMutSet)
,  root.GetRocksTTCMutSet(effect.incarnation.membersRocksTTCMutSet)
,  root.GetTreeTTCMutSet(effect.incarnation.membersTreeTTCMutSet)
,  root.GetWaterTTCMutSet(effect.incarnation.membersWaterTTCMutSet)
,  root.GetFloorTTCMutSet(effect.incarnation.membersFloorTTCMutSet)
,  root.GetCaveWallTTCMutSet(effect.incarnation.membersCaveWallTTCMutSet)
,  root.GetCaveTTCMutSet(effect.incarnation.membersCaveTTCMutSet)
,  root.GetFallsTTCMutSet(effect.incarnation.membersFallsTTCMutSet)
,  root.GetFireTTCMutSet(effect.incarnation.membersFireTTCMutSet)
,  root.GetObsidianFloorTTCMutSet(effect.incarnation.membersObsidianFloorTTCMutSet)
,  root.GetMagmaTTCMutSet(effect.incarnation.membersMagmaTTCMutSet)
,  root.GetCliffTTCMutSet(effect.incarnation.membersCliffTTCMutSet)
,  root.GetRavaNestTTCMutSet(effect.incarnation.membersRavaNestTTCMutSet)
,  root.GetCliffLandingTTCMutSet(effect.incarnation.membersCliffLandingTTCMutSet)
,  root.GetStoneTTCMutSet(effect.incarnation.membersStoneTTCMutSet)
,  root.GetGrassTTCMutSet(effect.incarnation.membersGrassTTCMutSet)
,  root.GetIncendianFallsLevelLinkerTTCMutSet(effect.incarnation.membersIncendianFallsLevelLinkerTTCMutSet)
,  root.GetEmberDeepLevelLinkerTTCMutSet(effect.incarnation.membersEmberDeepLevelLinkerTTCMutSet)    );

}

  public void visitITerrainTileComponentMutBunchDeleteEffect(ITerrainTileComponentMutBunchDeleteEffect effect) {
    root.EffectITerrainTileComponentMutBunchDelete(effect.id);
  }

     
public void visitTerrainEffect(ITerrainEffect effect) { effect.visitITerrainEffect(this); }
  public void visitTerrainCreateEffect(TerrainCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectTerrainCreateWithId(effect.id
,  effect.incarnation.pattern
,  effect.incarnation.elevationStepHeight
,  root.GetTerrainTileByLocationMutMap(effect.incarnation.tiles)    );

}

  public void visitTerrainDeleteEffect(TerrainDeleteEffect effect) {
    root.EffectTerrainDelete(effect.id);
  }

     
  public void visitTerrainSetPatternEffect(TerrainSetPatternEffect effect) {
    root.EffectTerrainSetPattern(
      effect.id,
  effect.newValue
    );
  }

public void visitSimplePresenceTriggerTTCEffect(ISimplePresenceTriggerTTCEffect effect) { effect.visitISimplePresenceTriggerTTCEffect(this); }
  public void visitSimplePresenceTriggerTTCCreateEffect(SimplePresenceTriggerTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectSimplePresenceTriggerTTCCreateWithId(effect.id
,  effect.incarnation.name    );

}

  public void visitSimplePresenceTriggerTTCDeleteEffect(SimplePresenceTriggerTTCDeleteEffect effect) {
    root.EffectSimplePresenceTriggerTTCDelete(effect.id);
  }

     
public void visitFireBombImpulseEffect(IFireBombImpulseEffect effect) { effect.visitIFireBombImpulseEffect(this); }
  public void visitFireBombImpulseCreateEffect(FireBombImpulseCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectFireBombImpulseCreateWithId(effect.id
,  effect.incarnation.weight
,  effect.incarnation.location    );

}

  public void visitFireBombImpulseDeleteEffect(FireBombImpulseDeleteEffect effect) {
    root.EffectFireBombImpulseDelete(effect.id);
  }

     
public void visitFireBombTTCEffect(IFireBombTTCEffect effect) { effect.visitIFireBombTTCEffect(this); }
  public void visitFireBombTTCCreateEffect(FireBombTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectFireBombTTCCreateWithId(effect.id
,  effect.incarnation.turnsUntilExplosion    );

}

  public void visitFireBombTTCDeleteEffect(FireBombTTCDeleteEffect effect) {
    root.EffectFireBombTTCDelete(effect.id);
  }

     
  public void visitFireBombTTCSetTurnsUntilExplosionEffect(FireBombTTCSetTurnsUntilExplosionEffect effect) {
    root.EffectFireBombTTCSetTurnsUntilExplosion(
      effect.id,
  effect.newValue
    );
  }

public void visitMarkerTTCEffect(IMarkerTTCEffect effect) { effect.visitIMarkerTTCEffect(this); }
  public void visitMarkerTTCCreateEffect(MarkerTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectMarkerTTCCreateWithId(effect.id
,  effect.incarnation.name    );

}

  public void visitMarkerTTCDeleteEffect(MarkerTTCDeleteEffect effect) {
    root.EffectMarkerTTCDelete(effect.id);
  }

     
public void visitLevelLinkTTCEffect(ILevelLinkTTCEffect effect) { effect.visitILevelLinkTTCEffect(this); }
  public void visitLevelLinkTTCCreateEffect(LevelLinkTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectLevelLinkTTCCreateWithId(effect.id
,  effect.incarnation.destroyThisLevel
,  root.GetLevel(effect.incarnation.destinationLevel)
,  effect.incarnation.destinationLevelLocation    );

}

  public void visitLevelLinkTTCDeleteEffect(LevelLinkTTCDeleteEffect effect) {
    root.EffectLevelLinkTTCDelete(effect.id);
  }

     
public void visitMudTTCEffect(IMudTTCEffect effect) { effect.visitIMudTTCEffect(this); }
  public void visitMudTTCCreateEffect(MudTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectMudTTCCreateWithId(effect.id
    );

}

  public void visitMudTTCDeleteEffect(MudTTCDeleteEffect effect) {
    root.EffectMudTTCDelete(effect.id);
  }

     
public void visitDirtTTCEffect(IDirtTTCEffect effect) { effect.visitIDirtTTCEffect(this); }
  public void visitDirtTTCCreateEffect(DirtTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectDirtTTCCreateWithId(effect.id
    );

}

  public void visitDirtTTCDeleteEffect(DirtTTCDeleteEffect effect) {
    root.EffectDirtTTCDelete(effect.id);
  }

     
public void visitObsidianTTCEffect(IObsidianTTCEffect effect) { effect.visitIObsidianTTCEffect(this); }
  public void visitObsidianTTCCreateEffect(ObsidianTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectObsidianTTCCreateWithId(effect.id
    );

}

  public void visitObsidianTTCDeleteEffect(ObsidianTTCDeleteEffect effect) {
    root.EffectObsidianTTCDelete(effect.id);
  }

     
public void visitDownStairsTTCEffect(IDownStairsTTCEffect effect) { effect.visitIDownStairsTTCEffect(this); }
  public void visitDownStairsTTCCreateEffect(DownStairsTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectDownStairsTTCCreateWithId(effect.id
    );

}

  public void visitDownStairsTTCDeleteEffect(DownStairsTTCDeleteEffect effect) {
    root.EffectDownStairsTTCDelete(effect.id);
  }

     
public void visitUpStairsTTCEffect(IUpStairsTTCEffect effect) { effect.visitIUpStairsTTCEffect(this); }
  public void visitUpStairsTTCCreateEffect(UpStairsTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectUpStairsTTCCreateWithId(effect.id
    );

}

  public void visitUpStairsTTCDeleteEffect(UpStairsTTCDeleteEffect effect) {
    root.EffectUpStairsTTCDelete(effect.id);
  }

     
public void visitWallTTCEffect(IWallTTCEffect effect) { effect.visitIWallTTCEffect(this); }
  public void visitWallTTCCreateEffect(WallTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectWallTTCCreateWithId(effect.id
    );

}

  public void visitWallTTCDeleteEffect(WallTTCDeleteEffect effect) {
    root.EffectWallTTCDelete(effect.id);
  }

     
public void visitBloodTTCEffect(IBloodTTCEffect effect) { effect.visitIBloodTTCEffect(this); }
  public void visitBloodTTCCreateEffect(BloodTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectBloodTTCCreateWithId(effect.id
    );

}

  public void visitBloodTTCDeleteEffect(BloodTTCDeleteEffect effect) {
    root.EffectBloodTTCDelete(effect.id);
  }

     
public void visitRocksTTCEffect(IRocksTTCEffect effect) { effect.visitIRocksTTCEffect(this); }
  public void visitRocksTTCCreateEffect(RocksTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectRocksTTCCreateWithId(effect.id
    );

}

  public void visitRocksTTCDeleteEffect(RocksTTCDeleteEffect effect) {
    root.EffectRocksTTCDelete(effect.id);
  }

     
public void visitTreeTTCEffect(ITreeTTCEffect effect) { effect.visitITreeTTCEffect(this); }
  public void visitTreeTTCCreateEffect(TreeTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectTreeTTCCreateWithId(effect.id
    );

}

  public void visitTreeTTCDeleteEffect(TreeTTCDeleteEffect effect) {
    root.EffectTreeTTCDelete(effect.id);
  }

     
public void visitWaterTTCEffect(IWaterTTCEffect effect) { effect.visitIWaterTTCEffect(this); }
  public void visitWaterTTCCreateEffect(WaterTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectWaterTTCCreateWithId(effect.id
    );

}

  public void visitWaterTTCDeleteEffect(WaterTTCDeleteEffect effect) {
    root.EffectWaterTTCDelete(effect.id);
  }

     
public void visitFloorTTCEffect(IFloorTTCEffect effect) { effect.visitIFloorTTCEffect(this); }
  public void visitFloorTTCCreateEffect(FloorTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectFloorTTCCreateWithId(effect.id
    );

}

  public void visitFloorTTCDeleteEffect(FloorTTCDeleteEffect effect) {
    root.EffectFloorTTCDelete(effect.id);
  }

     
public void visitCaveWallTTCEffect(ICaveWallTTCEffect effect) { effect.visitICaveWallTTCEffect(this); }
  public void visitCaveWallTTCCreateEffect(CaveWallTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectCaveWallTTCCreateWithId(effect.id
    );

}

  public void visitCaveWallTTCDeleteEffect(CaveWallTTCDeleteEffect effect) {
    root.EffectCaveWallTTCDelete(effect.id);
  }

     
public void visitCaveTTCEffect(ICaveTTCEffect effect) { effect.visitICaveTTCEffect(this); }
  public void visitCaveTTCCreateEffect(CaveTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectCaveTTCCreateWithId(effect.id
    );

}

  public void visitCaveTTCDeleteEffect(CaveTTCDeleteEffect effect) {
    root.EffectCaveTTCDelete(effect.id);
  }

     
public void visitFallsTTCEffect(IFallsTTCEffect effect) { effect.visitIFallsTTCEffect(this); }
  public void visitFallsTTCCreateEffect(FallsTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectFallsTTCCreateWithId(effect.id
    );

}

  public void visitFallsTTCDeleteEffect(FallsTTCDeleteEffect effect) {
    root.EffectFallsTTCDelete(effect.id);
  }

     
public void visitFireTTCEffect(IFireTTCEffect effect) { effect.visitIFireTTCEffect(this); }
  public void visitFireTTCCreateEffect(FireTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectFireTTCCreateWithId(effect.id
    );

}

  public void visitFireTTCDeleteEffect(FireTTCDeleteEffect effect) {
    root.EffectFireTTCDelete(effect.id);
  }

     
public void visitObsidianFloorTTCEffect(IObsidianFloorTTCEffect effect) { effect.visitIObsidianFloorTTCEffect(this); }
  public void visitObsidianFloorTTCCreateEffect(ObsidianFloorTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectObsidianFloorTTCCreateWithId(effect.id
    );

}

  public void visitObsidianFloorTTCDeleteEffect(ObsidianFloorTTCDeleteEffect effect) {
    root.EffectObsidianFloorTTCDelete(effect.id);
  }

     
public void visitMagmaTTCEffect(IMagmaTTCEffect effect) { effect.visitIMagmaTTCEffect(this); }
  public void visitMagmaTTCCreateEffect(MagmaTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectMagmaTTCCreateWithId(effect.id
    );

}

  public void visitMagmaTTCDeleteEffect(MagmaTTCDeleteEffect effect) {
    root.EffectMagmaTTCDelete(effect.id);
  }

     
public void visitCliffTTCEffect(ICliffTTCEffect effect) { effect.visitICliffTTCEffect(this); }
  public void visitCliffTTCCreateEffect(CliffTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectCliffTTCCreateWithId(effect.id
    );

}

  public void visitCliffTTCDeleteEffect(CliffTTCDeleteEffect effect) {
    root.EffectCliffTTCDelete(effect.id);
  }

     
public void visitRavaNestTTCEffect(IRavaNestTTCEffect effect) { effect.visitIRavaNestTTCEffect(this); }
  public void visitRavaNestTTCCreateEffect(RavaNestTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectRavaNestTTCCreateWithId(effect.id
    );

}

  public void visitRavaNestTTCDeleteEffect(RavaNestTTCDeleteEffect effect) {
    root.EffectRavaNestTTCDelete(effect.id);
  }

     
public void visitCliffLandingTTCEffect(ICliffLandingTTCEffect effect) { effect.visitICliffLandingTTCEffect(this); }
  public void visitCliffLandingTTCCreateEffect(CliffLandingTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectCliffLandingTTCCreateWithId(effect.id
    );

}

  public void visitCliffLandingTTCDeleteEffect(CliffLandingTTCDeleteEffect effect) {
    root.EffectCliffLandingTTCDelete(effect.id);
  }

     
public void visitStoneTTCEffect(IStoneTTCEffect effect) { effect.visitIStoneTTCEffect(this); }
  public void visitStoneTTCCreateEffect(StoneTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectStoneTTCCreateWithId(effect.id
    );

}

  public void visitStoneTTCDeleteEffect(StoneTTCDeleteEffect effect) {
    root.EffectStoneTTCDelete(effect.id);
  }

     
public void visitGrassTTCEffect(IGrassTTCEffect effect) { effect.visitIGrassTTCEffect(this); }
  public void visitGrassTTCCreateEffect(GrassTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectGrassTTCCreateWithId(effect.id
    );

}

  public void visitGrassTTCDeleteEffect(GrassTTCDeleteEffect effect) {
    root.EffectGrassTTCDelete(effect.id);
  }

     
public void visitLevelEffect(ILevelEffect effect) { effect.visitILevelEffect(this); }
  public void visitLevelCreateEffect(LevelCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectLevelCreateWithId(effect.id
,  effect.incarnation.cameraAngle
,  root.GetTerrain(effect.incarnation.terrain)
,  root.GetUnitMutSet(effect.incarnation.units)
,  root.GetILevelControllerOrNull(effect.incarnation.controller)
,  effect.incarnation.time    );

}

  public void visitLevelDeleteEffect(LevelDeleteEffect effect) {
    root.EffectLevelDelete(effect.id);
  }

     
  public void visitLevelSetControllerEffect(LevelSetControllerEffect effect) {
    root.EffectLevelSetController(
      effect.id,
  root.GetILevelControllerOrNull(effect.newValue)
    );
  }

  public void visitLevelSetTimeEffect(LevelSetTimeEffect effect) {
    root.EffectLevelSetTime(
      effect.id,
  effect.newValue
    );
  }

public void visitSpeedRingEffect(ISpeedRingEffect effect) { effect.visitISpeedRingEffect(this); }
  public void visitSpeedRingCreateEffect(SpeedRingCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectSpeedRingCreateWithId(effect.id
    );

}

  public void visitSpeedRingDeleteEffect(SpeedRingDeleteEffect effect) {
    root.EffectSpeedRingDelete(effect.id);
  }

     
public void visitManaPotionEffect(IManaPotionEffect effect) { effect.visitIManaPotionEffect(this); }
  public void visitManaPotionCreateEffect(ManaPotionCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectManaPotionCreateWithId(effect.id
    );

}

  public void visitManaPotionDeleteEffect(ManaPotionDeleteEffect effect) {
    root.EffectManaPotionDelete(effect.id);
  }

     
public void visitWatEffect(IWatEffect effect) { effect.visitIWatEffect(this); }
  public void visitWatCreateEffect(WatCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectWatCreateWithId(effect.id
,  root.GetIItemStrongMutBunch(effect.incarnation.items)
,  root.GetIImpulseStrongMutBunch(effect.incarnation.impulses)
,  root.GetIPostActingUCWeakMutBunch(effect.incarnation.blah)
,  root.GetIPreActingUCWeakMutBunch(effect.incarnation.bloop)    );

}

  public void visitWatDeleteEffect(WatDeleteEffect effect) {
    root.EffectWatDelete(effect.id);
  }

     
public void visitIPreActingUCWeakMutBunchEffect(IIPreActingUCWeakMutBunchEffect effect) { effect.visitIIPreActingUCWeakMutBunchEffect(this); }
  public void visitIPreActingUCWeakMutBunchCreateEffect(IPreActingUCWeakMutBunchCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectIPreActingUCWeakMutBunchCreateWithId(effect.id
,  root.GetDoomedUCWeakMutSet(effect.incarnation.membersDoomedUCWeakMutSet)
,  root.GetMiredUCWeakMutSet(effect.incarnation.membersMiredUCWeakMutSet)
,  root.GetInvincibilityUCWeakMutSet(effect.incarnation.membersInvincibilityUCWeakMutSet)
,  root.GetDefyingUCWeakMutSet(effect.incarnation.membersDefyingUCWeakMutSet)
,  root.GetCounteringUCWeakMutSet(effect.incarnation.membersCounteringUCWeakMutSet)
,  root.GetAttackAICapabilityUCWeakMutSet(effect.incarnation.membersAttackAICapabilityUCWeakMutSet)    );

}

  public void visitIPreActingUCWeakMutBunchDeleteEffect(IPreActingUCWeakMutBunchDeleteEffect effect) {
    root.EffectIPreActingUCWeakMutBunchDelete(effect.id);
  }

     
public void visitIPostActingUCWeakMutBunchEffect(IIPostActingUCWeakMutBunchEffect effect) { effect.visitIIPostActingUCWeakMutBunchEffect(this); }
  public void visitIPostActingUCWeakMutBunchCreateEffect(IPostActingUCWeakMutBunchCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectIPostActingUCWeakMutBunchCreateWithId(effect.id
,  root.GetLightningChargedUCWeakMutSet(effect.incarnation.membersLightningChargedUCWeakMutSet)
,  root.GetTimeCloneAICapabilityUCWeakMutSet(effect.incarnation.membersTimeCloneAICapabilityUCWeakMutSet)    );

}

  public void visitIPostActingUCWeakMutBunchDeleteEffect(IPostActingUCWeakMutBunchDeleteEffect effect) {
    root.EffectIPostActingUCWeakMutBunchDelete(effect.id);
  }

     
public void visitIImpulseStrongMutBunchEffect(IIImpulseStrongMutBunchEffect effect) { effect.visitIIImpulseStrongMutBunchEffect(this); }
  public void visitIImpulseStrongMutBunchCreateEffect(IImpulseStrongMutBunchCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectIImpulseStrongMutBunchCreateWithId(effect.id
,  root.GetHoldPositionImpulseStrongMutSet(effect.incarnation.membersHoldPositionImpulseStrongMutSet)
,  root.GetTemporaryCloneImpulseStrongMutSet(effect.incarnation.membersTemporaryCloneImpulseStrongMutSet)
,  root.GetSummonImpulseStrongMutSet(effect.incarnation.membersSummonImpulseStrongMutSet)
,  root.GetMireImpulseStrongMutSet(effect.incarnation.membersMireImpulseStrongMutSet)
,  root.GetEvaporateImpulseStrongMutSet(effect.incarnation.membersEvaporateImpulseStrongMutSet)
,  root.GetMoveImpulseStrongMutSet(effect.incarnation.membersMoveImpulseStrongMutSet)
,  root.GetKamikazeJumpImpulseStrongMutSet(effect.incarnation.membersKamikazeJumpImpulseStrongMutSet)
,  root.GetKamikazeTargetImpulseStrongMutSet(effect.incarnation.membersKamikazeTargetImpulseStrongMutSet)
,  root.GetNoImpulseStrongMutSet(effect.incarnation.membersNoImpulseStrongMutSet)
,  root.GetFireImpulseStrongMutSet(effect.incarnation.membersFireImpulseStrongMutSet)
,  root.GetDefyImpulseStrongMutSet(effect.incarnation.membersDefyImpulseStrongMutSet)
,  root.GetCounterImpulseStrongMutSet(effect.incarnation.membersCounterImpulseStrongMutSet)
,  root.GetUnleashBideImpulseStrongMutSet(effect.incarnation.membersUnleashBideImpulseStrongMutSet)
,  root.GetContinueBidingImpulseStrongMutSet(effect.incarnation.membersContinueBidingImpulseStrongMutSet)
,  root.GetStartBidingImpulseStrongMutSet(effect.incarnation.membersStartBidingImpulseStrongMutSet)
,  root.GetAttackImpulseStrongMutSet(effect.incarnation.membersAttackImpulseStrongMutSet)
,  root.GetPursueImpulseStrongMutSet(effect.incarnation.membersPursueImpulseStrongMutSet)
,  root.GetFireBombImpulseStrongMutSet(effect.incarnation.membersFireBombImpulseStrongMutSet)    );

}

  public void visitIImpulseStrongMutBunchDeleteEffect(IImpulseStrongMutBunchDeleteEffect effect) {
    root.EffectIImpulseStrongMutBunchDelete(effect.id);
  }

     
public void visitIItemStrongMutBunchEffect(IIItemStrongMutBunchEffect effect) { effect.visitIIItemStrongMutBunchEffect(this); }
  public void visitIItemStrongMutBunchCreateEffect(IItemStrongMutBunchCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectIItemStrongMutBunchCreateWithId(effect.id
,  root.GetManaPotionStrongMutSet(effect.incarnation.membersManaPotionStrongMutSet)
,  root.GetHealthPotionStrongMutSet(effect.incarnation.membersHealthPotionStrongMutSet)
,  root.GetSpeedRingStrongMutSet(effect.incarnation.membersSpeedRingStrongMutSet)
,  root.GetGlaiveStrongMutSet(effect.incarnation.membersGlaiveStrongMutSet)
,  root.GetSlowRodStrongMutSet(effect.incarnation.membersSlowRodStrongMutSet)
,  root.GetBlastRodStrongMutSet(effect.incarnation.membersBlastRodStrongMutSet)
,  root.GetArmorStrongMutSet(effect.incarnation.membersArmorStrongMutSet)    );

}

  public void visitIItemStrongMutBunchDeleteEffect(IItemStrongMutBunchDeleteEffect effect) {
    root.EffectIItemStrongMutBunchDelete(effect.id);
  }

     
public void visitItemTTCEffect(IItemTTCEffect effect) { effect.visitIItemTTCEffect(this); }
  public void visitItemTTCCreateEffect(ItemTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectItemTTCCreateWithId(effect.id
,  root.GetIItem(effect.incarnation.item)    );

}

  public void visitItemTTCDeleteEffect(ItemTTCDeleteEffect effect) {
    root.EffectItemTTCDelete(effect.id);
  }

     
public void visitHealthPotionEffect(IHealthPotionEffect effect) { effect.visitIHealthPotionEffect(this); }
  public void visitHealthPotionCreateEffect(HealthPotionCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectHealthPotionCreateWithId(effect.id
    );

}

  public void visitHealthPotionDeleteEffect(HealthPotionDeleteEffect effect) {
    root.EffectHealthPotionDelete(effect.id);
  }

     
public void visitGlaiveEffect(IGlaiveEffect effect) { effect.visitIGlaiveEffect(this); }
  public void visitGlaiveCreateEffect(GlaiveCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectGlaiveCreateWithId(effect.id
    );

}

  public void visitGlaiveDeleteEffect(GlaiveDeleteEffect effect) {
    root.EffectGlaiveDelete(effect.id);
  }

     
public void visitSlowRodEffect(ISlowRodEffect effect) { effect.visitISlowRodEffect(this); }
  public void visitSlowRodCreateEffect(SlowRodCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectSlowRodCreateWithId(effect.id
    );

}

  public void visitSlowRodDeleteEffect(SlowRodDeleteEffect effect) {
    root.EffectSlowRodDelete(effect.id);
  }

     
public void visitBlastRodEffect(IBlastRodEffect effect) { effect.visitIBlastRodEffect(this); }
  public void visitBlastRodCreateEffect(BlastRodCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectBlastRodCreateWithId(effect.id
    );

}

  public void visitBlastRodDeleteEffect(BlastRodDeleteEffect effect) {
    root.EffectBlastRodDelete(effect.id);
  }

     
public void visitArmorEffect(IArmorEffect effect) { effect.visitIArmorEffect(this); }
  public void visitArmorCreateEffect(ArmorCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectArmorCreateWithId(effect.id
    );

}

  public void visitArmorDeleteEffect(ArmorDeleteEffect effect) {
    root.EffectArmorDelete(effect.id);
  }

     
public void visitSquareCaveLevelControllerEffect(ISquareCaveLevelControllerEffect effect) { effect.visitISquareCaveLevelControllerEffect(this); }
  public void visitSquareCaveLevelControllerCreateEffect(SquareCaveLevelControllerCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectSquareCaveLevelControllerCreateWithId(effect.id
,  root.GetLevel(effect.incarnation.level)
,  effect.incarnation.depth    );

}

  public void visitSquareCaveLevelControllerDeleteEffect(SquareCaveLevelControllerDeleteEffect effect) {
    root.EffectSquareCaveLevelControllerDelete(effect.id);
  }

     
public void visitRavashrikeLevelControllerEffect(IRavashrikeLevelControllerEffect effect) { effect.visitIRavashrikeLevelControllerEffect(this); }
  public void visitRavashrikeLevelControllerCreateEffect(RavashrikeLevelControllerCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectRavashrikeLevelControllerCreateWithId(effect.id
,  root.GetLevel(effect.incarnation.level)    );

}

  public void visitRavashrikeLevelControllerDeleteEffect(RavashrikeLevelControllerDeleteEffect effect) {
    root.EffectRavashrikeLevelControllerDelete(effect.id);
  }

     
public void visitPentagonalCaveLevelControllerEffect(IPentagonalCaveLevelControllerEffect effect) { effect.visitIPentagonalCaveLevelControllerEffect(this); }
  public void visitPentagonalCaveLevelControllerCreateEffect(PentagonalCaveLevelControllerCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectPentagonalCaveLevelControllerCreateWithId(effect.id
,  root.GetLevel(effect.incarnation.level)
,  effect.incarnation.depth    );

}

  public void visitPentagonalCaveLevelControllerDeleteEffect(PentagonalCaveLevelControllerDeleteEffect effect) {
    root.EffectPentagonalCaveLevelControllerDelete(effect.id);
  }

     
public void visitIncendianFallsLevelLinkerTTCEffect(IIncendianFallsLevelLinkerTTCEffect effect) { effect.visitIIncendianFallsLevelLinkerTTCEffect(this); }
  public void visitIncendianFallsLevelLinkerTTCCreateEffect(IncendianFallsLevelLinkerTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectIncendianFallsLevelLinkerTTCCreateWithId(effect.id
,  effect.incarnation.thisLevelDepth    );

}

  public void visitIncendianFallsLevelLinkerTTCDeleteEffect(IncendianFallsLevelLinkerTTCDeleteEffect effect) {
    root.EffectIncendianFallsLevelLinkerTTCDelete(effect.id);
  }

     
public void visitCliffLevelControllerEffect(ICliffLevelControllerEffect effect) { effect.visitICliffLevelControllerEffect(this); }
  public void visitCliffLevelControllerCreateEffect(CliffLevelControllerCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectCliffLevelControllerCreateWithId(effect.id
,  root.GetLevel(effect.incarnation.level)
,  effect.incarnation.depth    );

}

  public void visitCliffLevelControllerDeleteEffect(CliffLevelControllerDeleteEffect effect) {
    root.EffectCliffLevelControllerDelete(effect.id);
  }

     
public void visitPreGauntletLevelControllerEffect(IPreGauntletLevelControllerEffect effect) { effect.visitIPreGauntletLevelControllerEffect(this); }
  public void visitPreGauntletLevelControllerCreateEffect(PreGauntletLevelControllerCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectPreGauntletLevelControllerCreateWithId(effect.id
,  root.GetLevel(effect.incarnation.level)    );

}

  public void visitPreGauntletLevelControllerDeleteEffect(PreGauntletLevelControllerDeleteEffect effect) {
    root.EffectPreGauntletLevelControllerDelete(effect.id);
  }

     
public void visitGauntletLevelControllerEffect(IGauntletLevelControllerEffect effect) { effect.visitIGauntletLevelControllerEffect(this); }
  public void visitGauntletLevelControllerCreateEffect(GauntletLevelControllerCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectGauntletLevelControllerCreateWithId(effect.id
,  root.GetLevel(effect.incarnation.level)    );

}

  public void visitGauntletLevelControllerDeleteEffect(GauntletLevelControllerDeleteEffect effect) {
    root.EffectGauntletLevelControllerDelete(effect.id);
  }

     
public void visitCommEffect(ICommEffect effect) { effect.visitICommEffect(this); }
  public void visitCommCreateEffect(CommCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectCommCreateWithId(effect.id
,  effect.incarnation.template
,  effect.incarnation.actions
,  effect.incarnation.texts    );

}

  public void visitCommDeleteEffect(CommDeleteEffect effect) {
    root.EffectCommDelete(effect.id);
  }

     
public void visitGameEffect(IGameEffect effect) { effect.visitIGameEffect(this); }
  public void visitGameCreateEffect(GameCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectGameCreateWithId(effect.id
,  root.GetRand(effect.incarnation.rand)
,  effect.incarnation.squareLevelsOnly
,  root.GetLevelMutSet(effect.incarnation.levels)
,  root.GetUnitOrNull(effect.incarnation.player)
,  root.GetLevelOrNull(effect.incarnation.level)
,  effect.incarnation.time
,  root.GetUnitOrNull(effect.incarnation.actingUnit)
,  effect.incarnation.pauseBeforeNextUnit
,  effect.incarnation.actionNum
,  effect.incarnation.instructions
,  effect.incarnation.hideInput
,  effect.incarnation.evvent
,  root.GetCommMutList(effect.incarnation.comms)    );

}

  public void visitGameDeleteEffect(GameDeleteEffect effect) {
    root.EffectGameDelete(effect.id);
  }

     
  public void visitGameSetPlayerEffect(GameSetPlayerEffect effect) {
    root.EffectGameSetPlayer(
      effect.id,
  root.GetUnitOrNull(effect.newValue)
    );
  }

  public void visitGameSetLevelEffect(GameSetLevelEffect effect) {
    root.EffectGameSetLevel(
      effect.id,
  root.GetLevelOrNull(effect.newValue)
    );
  }

  public void visitGameSetTimeEffect(GameSetTimeEffect effect) {
    root.EffectGameSetTime(
      effect.id,
  effect.newValue
    );
  }

  public void visitGameSetActingUnitEffect(GameSetActingUnitEffect effect) {
    root.EffectGameSetActingUnit(
      effect.id,
  root.GetUnitOrNull(effect.newValue)
    );
  }

  public void visitGameSetPauseBeforeNextUnitEffect(GameSetPauseBeforeNextUnitEffect effect) {
    root.EffectGameSetPauseBeforeNextUnit(
      effect.id,
  effect.newValue
    );
  }

  public void visitGameSetActionNumEffect(GameSetActionNumEffect effect) {
    root.EffectGameSetActionNum(
      effect.id,
  effect.newValue
    );
  }

  public void visitGameSetInstructionsEffect(GameSetInstructionsEffect effect) {
    root.EffectGameSetInstructions(
      effect.id,
  effect.newValue
    );
  }

  public void visitGameSetHideInputEffect(GameSetHideInputEffect effect) {
    root.EffectGameSetHideInput(
      effect.id,
  effect.newValue
    );
  }

  public void visitGameSetEvventEffect(GameSetEvventEffect effect) {
    root.EffectGameSetEvvent(
      effect.id,
  effect.newValue
    );
  }

public void visitVolcaetusLevelControllerEffect(IVolcaetusLevelControllerEffect effect) { effect.visitIVolcaetusLevelControllerEffect(this); }
  public void visitVolcaetusLevelControllerCreateEffect(VolcaetusLevelControllerCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectVolcaetusLevelControllerCreateWithId(effect.id
,  root.GetLevel(effect.incarnation.level)    );

}

  public void visitVolcaetusLevelControllerDeleteEffect(VolcaetusLevelControllerDeleteEffect effect) {
    root.EffectVolcaetusLevelControllerDelete(effect.id);
  }

     
public void visitTutorial2LevelControllerEffect(ITutorial2LevelControllerEffect effect) { effect.visitITutorial2LevelControllerEffect(this); }
  public void visitTutorial2LevelControllerCreateEffect(Tutorial2LevelControllerCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectTutorial2LevelControllerCreateWithId(effect.id
,  root.GetLevel(effect.incarnation.level)    );

}

  public void visitTutorial2LevelControllerDeleteEffect(Tutorial2LevelControllerDeleteEffect effect) {
    root.EffectTutorial2LevelControllerDelete(effect.id);
  }

     
public void visitTutorial1LevelControllerEffect(ITutorial1LevelControllerEffect effect) { effect.visitITutorial1LevelControllerEffect(this); }
  public void visitTutorial1LevelControllerCreateEffect(Tutorial1LevelControllerCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectTutorial1LevelControllerCreateWithId(effect.id
,  root.GetLevel(effect.incarnation.level)    );

}

  public void visitTutorial1LevelControllerDeleteEffect(Tutorial1LevelControllerDeleteEffect effect) {
    root.EffectTutorial1LevelControllerDelete(effect.id);
  }

     
public void visitRetreatLevelControllerEffect(IRetreatLevelControllerEffect effect) { effect.visitIRetreatLevelControllerEffect(this); }
  public void visitRetreatLevelControllerCreateEffect(RetreatLevelControllerCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectRetreatLevelControllerCreateWithId(effect.id
,  root.GetLevel(effect.incarnation.level)    );

}

  public void visitRetreatLevelControllerDeleteEffect(RetreatLevelControllerDeleteEffect effect) {
    root.EffectRetreatLevelControllerDelete(effect.id);
  }

     
public void visitSotaventoLevelControllerEffect(ISotaventoLevelControllerEffect effect) { effect.visitISotaventoLevelControllerEffect(this); }
  public void visitSotaventoLevelControllerCreateEffect(SotaventoLevelControllerCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectSotaventoLevelControllerCreateWithId(effect.id
,  root.GetLevel(effect.incarnation.level)    );

}

  public void visitSotaventoLevelControllerDeleteEffect(SotaventoLevelControllerDeleteEffect effect) {
    root.EffectSotaventoLevelControllerDelete(effect.id);
  }

     
public void visitNestLevelControllerEffect(INestLevelControllerEffect effect) { effect.visitINestLevelControllerEffect(this); }
  public void visitNestLevelControllerCreateEffect(NestLevelControllerCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectNestLevelControllerCreateWithId(effect.id
,  root.GetLevel(effect.incarnation.level)    );

}

  public void visitNestLevelControllerDeleteEffect(NestLevelControllerDeleteEffect effect) {
    root.EffectNestLevelControllerDelete(effect.id);
  }

     
public void visitLakeLevelControllerEffect(ILakeLevelControllerEffect effect) { effect.visitILakeLevelControllerEffect(this); }
  public void visitLakeLevelControllerCreateEffect(LakeLevelControllerCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectLakeLevelControllerCreateWithId(effect.id
,  root.GetLevel(effect.incarnation.level)    );

}

  public void visitLakeLevelControllerDeleteEffect(LakeLevelControllerDeleteEffect effect) {
    root.EffectLakeLevelControllerDelete(effect.id);
  }

     
public void visitEmberDeepLevelLinkerTTCEffect(IEmberDeepLevelLinkerTTCEffect effect) { effect.visitIEmberDeepLevelLinkerTTCEffect(this); }
  public void visitEmberDeepLevelLinkerTTCCreateEffect(EmberDeepLevelLinkerTTCCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectEmberDeepLevelLinkerTTCCreateWithId(effect.id
,  effect.incarnation.nextLevelDepth    );

}

  public void visitEmberDeepLevelLinkerTTCDeleteEffect(EmberDeepLevelLinkerTTCDeleteEffect effect) {
    root.EffectEmberDeepLevelLinkerTTCDelete(effect.id);
  }

     
public void visitDirtRoadLevelControllerEffect(IDirtRoadLevelControllerEffect effect) { effect.visitIDirtRoadLevelControllerEffect(this); }
  public void visitDirtRoadLevelControllerCreateEffect(DirtRoadLevelControllerCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectDirtRoadLevelControllerCreateWithId(effect.id
,  root.GetLevel(effect.incarnation.level)    );

}

  public void visitDirtRoadLevelControllerDeleteEffect(DirtRoadLevelControllerDeleteEffect effect) {
    root.EffectDirtRoadLevelControllerDelete(effect.id);
  }

     
public void visitCaveLevelControllerEffect(ICaveLevelControllerEffect effect) { effect.visitICaveLevelControllerEffect(this); }
  public void visitCaveLevelControllerCreateEffect(CaveLevelControllerCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectCaveLevelControllerCreateWithId(effect.id
,  root.GetLevel(effect.incarnation.level)
,  effect.incarnation.depth    );

}

  public void visitCaveLevelControllerDeleteEffect(CaveLevelControllerDeleteEffect effect) {
    root.EffectCaveLevelControllerDelete(effect.id);
  }

     
public void visitBridgesLevelControllerEffect(IBridgesLevelControllerEffect effect) { effect.visitIBridgesLevelControllerEffect(this); }
  public void visitBridgesLevelControllerCreateEffect(BridgesLevelControllerCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectBridgesLevelControllerCreateWithId(effect.id
,  root.GetLevel(effect.incarnation.level)    );

}

  public void visitBridgesLevelControllerDeleteEffect(BridgesLevelControllerDeleteEffect effect) {
    root.EffectBridgesLevelControllerDelete(effect.id);
  }

     
public void visitAncientTownLevelControllerEffect(IAncientTownLevelControllerEffect effect) { effect.visitIAncientTownLevelControllerEffect(this); }
  public void visitAncientTownLevelControllerCreateEffect(AncientTownLevelControllerCreateEffect effect) {
    // For now we're just feeding the remote ID in. Someday we might want to have a map
    // in the applier instead.
    root.EffectAncientTownLevelControllerCreateWithId(effect.id
,  root.GetLevel(effect.incarnation.level)    );

}

  public void visitAncientTownLevelControllerDeleteEffect(AncientTownLevelControllerDeleteEffect effect) {
    root.EffectAncientTownLevelControllerDelete(effect.id);
  }

     
    public void visitCommMutListEffect(ICommMutListEffect effect) { effect.visitICommMutListEffect(this); }
    public void visitCommMutListCreateEffect(CommMutListCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectCommMutListCreateWithId(effect.id);
    }
    public void visitCommMutListDeleteEffect(CommMutListDeleteEffect effect) {
      root.EffectCommMutListDelete(effect.id);
    }
    public void visitCommMutListAddEffect(CommMutListAddEffect effect) {
      root.EffectCommMutListAdd(effect.id, effect.index, effect.element);
    }
    public void visitCommMutListRemoveEffect(CommMutListRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectCommMutListRemoveAt(effect.id, effect.index);
    }
       
    public void visitLocationMutListEffect(ILocationMutListEffect effect) { effect.visitILocationMutListEffect(this); }
    public void visitLocationMutListCreateEffect(LocationMutListCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectLocationMutListCreateWithId(effect.id);
    }
    public void visitLocationMutListDeleteEffect(LocationMutListDeleteEffect effect) {
      root.EffectLocationMutListDelete(effect.id);
    }
    public void visitLocationMutListAddEffect(LocationMutListAddEffect effect) {
      root.EffectLocationMutListAdd(effect.id, effect.index, effect.element);
    }
    public void visitLocationMutListRemoveEffect(LocationMutListRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectLocationMutListRemoveAt(effect.id, effect.index);
    }
       
    public void visitIRequestMutListEffect(IIRequestMutListEffect effect) { effect.visitIIRequestMutListEffect(this); }
    public void visitIRequestMutListCreateEffect(IRequestMutListCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectIRequestMutListCreateWithId(effect.id);
    }
    public void visitIRequestMutListDeleteEffect(IRequestMutListDeleteEffect effect) {
      root.EffectIRequestMutListDelete(effect.id);
    }
    public void visitIRequestMutListAddEffect(IRequestMutListAddEffect effect) {
      root.EffectIRequestMutListAdd(effect.id, effect.index, effect.element);
    }
    public void visitIRequestMutListRemoveEffect(IRequestMutListRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectIRequestMutListRemoveAt(effect.id, effect.index);
    }
       
    public void visitLevelMutSetEffect(ILevelMutSetEffect effect) { effect.visitILevelMutSetEffect(this); }
    public void visitLevelMutSetCreateEffect(LevelMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectLevelMutSetCreateWithId(effect.id);
    }
    public void visitLevelMutSetDeleteEffect(LevelMutSetDeleteEffect effect) {
      root.EffectLevelMutSetDelete(effect.id);
    }
    public void visitLevelMutSetAddEffect(LevelMutSetAddEffect effect) {
     root.EffectLevelMutSetAdd(effect.id, effect.element);
 }
    public void visitLevelMutSetRemoveEffect(LevelMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectLevelMutSetRemove(effect.id, effect.element);
    }
       
    public void visitManaPotionStrongMutSetEffect(IManaPotionStrongMutSetEffect effect) { effect.visitIManaPotionStrongMutSetEffect(this); }
    public void visitManaPotionStrongMutSetCreateEffect(ManaPotionStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectManaPotionStrongMutSetCreateWithId(effect.id);
    }
    public void visitManaPotionStrongMutSetDeleteEffect(ManaPotionStrongMutSetDeleteEffect effect) {
      root.EffectManaPotionStrongMutSetDelete(effect.id);
    }
    public void visitManaPotionStrongMutSetAddEffect(ManaPotionStrongMutSetAddEffect effect) {
     root.EffectManaPotionStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitManaPotionStrongMutSetRemoveEffect(ManaPotionStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectManaPotionStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitHealthPotionStrongMutSetEffect(IHealthPotionStrongMutSetEffect effect) { effect.visitIHealthPotionStrongMutSetEffect(this); }
    public void visitHealthPotionStrongMutSetCreateEffect(HealthPotionStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectHealthPotionStrongMutSetCreateWithId(effect.id);
    }
    public void visitHealthPotionStrongMutSetDeleteEffect(HealthPotionStrongMutSetDeleteEffect effect) {
      root.EffectHealthPotionStrongMutSetDelete(effect.id);
    }
    public void visitHealthPotionStrongMutSetAddEffect(HealthPotionStrongMutSetAddEffect effect) {
     root.EffectHealthPotionStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitHealthPotionStrongMutSetRemoveEffect(HealthPotionStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectHealthPotionStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitSpeedRingStrongMutSetEffect(ISpeedRingStrongMutSetEffect effect) { effect.visitISpeedRingStrongMutSetEffect(this); }
    public void visitSpeedRingStrongMutSetCreateEffect(SpeedRingStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectSpeedRingStrongMutSetCreateWithId(effect.id);
    }
    public void visitSpeedRingStrongMutSetDeleteEffect(SpeedRingStrongMutSetDeleteEffect effect) {
      root.EffectSpeedRingStrongMutSetDelete(effect.id);
    }
    public void visitSpeedRingStrongMutSetAddEffect(SpeedRingStrongMutSetAddEffect effect) {
     root.EffectSpeedRingStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitSpeedRingStrongMutSetRemoveEffect(SpeedRingStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectSpeedRingStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitGlaiveStrongMutSetEffect(IGlaiveStrongMutSetEffect effect) { effect.visitIGlaiveStrongMutSetEffect(this); }
    public void visitGlaiveStrongMutSetCreateEffect(GlaiveStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectGlaiveStrongMutSetCreateWithId(effect.id);
    }
    public void visitGlaiveStrongMutSetDeleteEffect(GlaiveStrongMutSetDeleteEffect effect) {
      root.EffectGlaiveStrongMutSetDelete(effect.id);
    }
    public void visitGlaiveStrongMutSetAddEffect(GlaiveStrongMutSetAddEffect effect) {
     root.EffectGlaiveStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitGlaiveStrongMutSetRemoveEffect(GlaiveStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectGlaiveStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitSlowRodStrongMutSetEffect(ISlowRodStrongMutSetEffect effect) { effect.visitISlowRodStrongMutSetEffect(this); }
    public void visitSlowRodStrongMutSetCreateEffect(SlowRodStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectSlowRodStrongMutSetCreateWithId(effect.id);
    }
    public void visitSlowRodStrongMutSetDeleteEffect(SlowRodStrongMutSetDeleteEffect effect) {
      root.EffectSlowRodStrongMutSetDelete(effect.id);
    }
    public void visitSlowRodStrongMutSetAddEffect(SlowRodStrongMutSetAddEffect effect) {
     root.EffectSlowRodStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitSlowRodStrongMutSetRemoveEffect(SlowRodStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectSlowRodStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitBlastRodStrongMutSetEffect(IBlastRodStrongMutSetEffect effect) { effect.visitIBlastRodStrongMutSetEffect(this); }
    public void visitBlastRodStrongMutSetCreateEffect(BlastRodStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectBlastRodStrongMutSetCreateWithId(effect.id);
    }
    public void visitBlastRodStrongMutSetDeleteEffect(BlastRodStrongMutSetDeleteEffect effect) {
      root.EffectBlastRodStrongMutSetDelete(effect.id);
    }
    public void visitBlastRodStrongMutSetAddEffect(BlastRodStrongMutSetAddEffect effect) {
     root.EffectBlastRodStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitBlastRodStrongMutSetRemoveEffect(BlastRodStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectBlastRodStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitArmorStrongMutSetEffect(IArmorStrongMutSetEffect effect) { effect.visitIArmorStrongMutSetEffect(this); }
    public void visitArmorStrongMutSetCreateEffect(ArmorStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectArmorStrongMutSetCreateWithId(effect.id);
    }
    public void visitArmorStrongMutSetDeleteEffect(ArmorStrongMutSetDeleteEffect effect) {
      root.EffectArmorStrongMutSetDelete(effect.id);
    }
    public void visitArmorStrongMutSetAddEffect(ArmorStrongMutSetAddEffect effect) {
     root.EffectArmorStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitArmorStrongMutSetRemoveEffect(ArmorStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectArmorStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitHoldPositionImpulseStrongMutSetEffect(IHoldPositionImpulseStrongMutSetEffect effect) { effect.visitIHoldPositionImpulseStrongMutSetEffect(this); }
    public void visitHoldPositionImpulseStrongMutSetCreateEffect(HoldPositionImpulseStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectHoldPositionImpulseStrongMutSetCreateWithId(effect.id);
    }
    public void visitHoldPositionImpulseStrongMutSetDeleteEffect(HoldPositionImpulseStrongMutSetDeleteEffect effect) {
      root.EffectHoldPositionImpulseStrongMutSetDelete(effect.id);
    }
    public void visitHoldPositionImpulseStrongMutSetAddEffect(HoldPositionImpulseStrongMutSetAddEffect effect) {
     root.EffectHoldPositionImpulseStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitHoldPositionImpulseStrongMutSetRemoveEffect(HoldPositionImpulseStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectHoldPositionImpulseStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitTemporaryCloneImpulseStrongMutSetEffect(ITemporaryCloneImpulseStrongMutSetEffect effect) { effect.visitITemporaryCloneImpulseStrongMutSetEffect(this); }
    public void visitTemporaryCloneImpulseStrongMutSetCreateEffect(TemporaryCloneImpulseStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectTemporaryCloneImpulseStrongMutSetCreateWithId(effect.id);
    }
    public void visitTemporaryCloneImpulseStrongMutSetDeleteEffect(TemporaryCloneImpulseStrongMutSetDeleteEffect effect) {
      root.EffectTemporaryCloneImpulseStrongMutSetDelete(effect.id);
    }
    public void visitTemporaryCloneImpulseStrongMutSetAddEffect(TemporaryCloneImpulseStrongMutSetAddEffect effect) {
     root.EffectTemporaryCloneImpulseStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitTemporaryCloneImpulseStrongMutSetRemoveEffect(TemporaryCloneImpulseStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectTemporaryCloneImpulseStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitSummonImpulseStrongMutSetEffect(ISummonImpulseStrongMutSetEffect effect) { effect.visitISummonImpulseStrongMutSetEffect(this); }
    public void visitSummonImpulseStrongMutSetCreateEffect(SummonImpulseStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectSummonImpulseStrongMutSetCreateWithId(effect.id);
    }
    public void visitSummonImpulseStrongMutSetDeleteEffect(SummonImpulseStrongMutSetDeleteEffect effect) {
      root.EffectSummonImpulseStrongMutSetDelete(effect.id);
    }
    public void visitSummonImpulseStrongMutSetAddEffect(SummonImpulseStrongMutSetAddEffect effect) {
     root.EffectSummonImpulseStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitSummonImpulseStrongMutSetRemoveEffect(SummonImpulseStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectSummonImpulseStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitMireImpulseStrongMutSetEffect(IMireImpulseStrongMutSetEffect effect) { effect.visitIMireImpulseStrongMutSetEffect(this); }
    public void visitMireImpulseStrongMutSetCreateEffect(MireImpulseStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectMireImpulseStrongMutSetCreateWithId(effect.id);
    }
    public void visitMireImpulseStrongMutSetDeleteEffect(MireImpulseStrongMutSetDeleteEffect effect) {
      root.EffectMireImpulseStrongMutSetDelete(effect.id);
    }
    public void visitMireImpulseStrongMutSetAddEffect(MireImpulseStrongMutSetAddEffect effect) {
     root.EffectMireImpulseStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitMireImpulseStrongMutSetRemoveEffect(MireImpulseStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectMireImpulseStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitEvaporateImpulseStrongMutSetEffect(IEvaporateImpulseStrongMutSetEffect effect) { effect.visitIEvaporateImpulseStrongMutSetEffect(this); }
    public void visitEvaporateImpulseStrongMutSetCreateEffect(EvaporateImpulseStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectEvaporateImpulseStrongMutSetCreateWithId(effect.id);
    }
    public void visitEvaporateImpulseStrongMutSetDeleteEffect(EvaporateImpulseStrongMutSetDeleteEffect effect) {
      root.EffectEvaporateImpulseStrongMutSetDelete(effect.id);
    }
    public void visitEvaporateImpulseStrongMutSetAddEffect(EvaporateImpulseStrongMutSetAddEffect effect) {
     root.EffectEvaporateImpulseStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitEvaporateImpulseStrongMutSetRemoveEffect(EvaporateImpulseStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectEvaporateImpulseStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitMoveImpulseStrongMutSetEffect(IMoveImpulseStrongMutSetEffect effect) { effect.visitIMoveImpulseStrongMutSetEffect(this); }
    public void visitMoveImpulseStrongMutSetCreateEffect(MoveImpulseStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectMoveImpulseStrongMutSetCreateWithId(effect.id);
    }
    public void visitMoveImpulseStrongMutSetDeleteEffect(MoveImpulseStrongMutSetDeleteEffect effect) {
      root.EffectMoveImpulseStrongMutSetDelete(effect.id);
    }
    public void visitMoveImpulseStrongMutSetAddEffect(MoveImpulseStrongMutSetAddEffect effect) {
     root.EffectMoveImpulseStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitMoveImpulseStrongMutSetRemoveEffect(MoveImpulseStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectMoveImpulseStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitKamikazeJumpImpulseStrongMutSetEffect(IKamikazeJumpImpulseStrongMutSetEffect effect) { effect.visitIKamikazeJumpImpulseStrongMutSetEffect(this); }
    public void visitKamikazeJumpImpulseStrongMutSetCreateEffect(KamikazeJumpImpulseStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectKamikazeJumpImpulseStrongMutSetCreateWithId(effect.id);
    }
    public void visitKamikazeJumpImpulseStrongMutSetDeleteEffect(KamikazeJumpImpulseStrongMutSetDeleteEffect effect) {
      root.EffectKamikazeJumpImpulseStrongMutSetDelete(effect.id);
    }
    public void visitKamikazeJumpImpulseStrongMutSetAddEffect(KamikazeJumpImpulseStrongMutSetAddEffect effect) {
     root.EffectKamikazeJumpImpulseStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitKamikazeJumpImpulseStrongMutSetRemoveEffect(KamikazeJumpImpulseStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectKamikazeJumpImpulseStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitKamikazeTargetImpulseStrongMutSetEffect(IKamikazeTargetImpulseStrongMutSetEffect effect) { effect.visitIKamikazeTargetImpulseStrongMutSetEffect(this); }
    public void visitKamikazeTargetImpulseStrongMutSetCreateEffect(KamikazeTargetImpulseStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectKamikazeTargetImpulseStrongMutSetCreateWithId(effect.id);
    }
    public void visitKamikazeTargetImpulseStrongMutSetDeleteEffect(KamikazeTargetImpulseStrongMutSetDeleteEffect effect) {
      root.EffectKamikazeTargetImpulseStrongMutSetDelete(effect.id);
    }
    public void visitKamikazeTargetImpulseStrongMutSetAddEffect(KamikazeTargetImpulseStrongMutSetAddEffect effect) {
     root.EffectKamikazeTargetImpulseStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitKamikazeTargetImpulseStrongMutSetRemoveEffect(KamikazeTargetImpulseStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectKamikazeTargetImpulseStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitNoImpulseStrongMutSetEffect(INoImpulseStrongMutSetEffect effect) { effect.visitINoImpulseStrongMutSetEffect(this); }
    public void visitNoImpulseStrongMutSetCreateEffect(NoImpulseStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectNoImpulseStrongMutSetCreateWithId(effect.id);
    }
    public void visitNoImpulseStrongMutSetDeleteEffect(NoImpulseStrongMutSetDeleteEffect effect) {
      root.EffectNoImpulseStrongMutSetDelete(effect.id);
    }
    public void visitNoImpulseStrongMutSetAddEffect(NoImpulseStrongMutSetAddEffect effect) {
     root.EffectNoImpulseStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitNoImpulseStrongMutSetRemoveEffect(NoImpulseStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectNoImpulseStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitFireImpulseStrongMutSetEffect(IFireImpulseStrongMutSetEffect effect) { effect.visitIFireImpulseStrongMutSetEffect(this); }
    public void visitFireImpulseStrongMutSetCreateEffect(FireImpulseStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectFireImpulseStrongMutSetCreateWithId(effect.id);
    }
    public void visitFireImpulseStrongMutSetDeleteEffect(FireImpulseStrongMutSetDeleteEffect effect) {
      root.EffectFireImpulseStrongMutSetDelete(effect.id);
    }
    public void visitFireImpulseStrongMutSetAddEffect(FireImpulseStrongMutSetAddEffect effect) {
     root.EffectFireImpulseStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitFireImpulseStrongMutSetRemoveEffect(FireImpulseStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectFireImpulseStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitDefyImpulseStrongMutSetEffect(IDefyImpulseStrongMutSetEffect effect) { effect.visitIDefyImpulseStrongMutSetEffect(this); }
    public void visitDefyImpulseStrongMutSetCreateEffect(DefyImpulseStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectDefyImpulseStrongMutSetCreateWithId(effect.id);
    }
    public void visitDefyImpulseStrongMutSetDeleteEffect(DefyImpulseStrongMutSetDeleteEffect effect) {
      root.EffectDefyImpulseStrongMutSetDelete(effect.id);
    }
    public void visitDefyImpulseStrongMutSetAddEffect(DefyImpulseStrongMutSetAddEffect effect) {
     root.EffectDefyImpulseStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitDefyImpulseStrongMutSetRemoveEffect(DefyImpulseStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectDefyImpulseStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitCounterImpulseStrongMutSetEffect(ICounterImpulseStrongMutSetEffect effect) { effect.visitICounterImpulseStrongMutSetEffect(this); }
    public void visitCounterImpulseStrongMutSetCreateEffect(CounterImpulseStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectCounterImpulseStrongMutSetCreateWithId(effect.id);
    }
    public void visitCounterImpulseStrongMutSetDeleteEffect(CounterImpulseStrongMutSetDeleteEffect effect) {
      root.EffectCounterImpulseStrongMutSetDelete(effect.id);
    }
    public void visitCounterImpulseStrongMutSetAddEffect(CounterImpulseStrongMutSetAddEffect effect) {
     root.EffectCounterImpulseStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitCounterImpulseStrongMutSetRemoveEffect(CounterImpulseStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectCounterImpulseStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitUnleashBideImpulseStrongMutSetEffect(IUnleashBideImpulseStrongMutSetEffect effect) { effect.visitIUnleashBideImpulseStrongMutSetEffect(this); }
    public void visitUnleashBideImpulseStrongMutSetCreateEffect(UnleashBideImpulseStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectUnleashBideImpulseStrongMutSetCreateWithId(effect.id);
    }
    public void visitUnleashBideImpulseStrongMutSetDeleteEffect(UnleashBideImpulseStrongMutSetDeleteEffect effect) {
      root.EffectUnleashBideImpulseStrongMutSetDelete(effect.id);
    }
    public void visitUnleashBideImpulseStrongMutSetAddEffect(UnleashBideImpulseStrongMutSetAddEffect effect) {
     root.EffectUnleashBideImpulseStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitUnleashBideImpulseStrongMutSetRemoveEffect(UnleashBideImpulseStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectUnleashBideImpulseStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitContinueBidingImpulseStrongMutSetEffect(IContinueBidingImpulseStrongMutSetEffect effect) { effect.visitIContinueBidingImpulseStrongMutSetEffect(this); }
    public void visitContinueBidingImpulseStrongMutSetCreateEffect(ContinueBidingImpulseStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectContinueBidingImpulseStrongMutSetCreateWithId(effect.id);
    }
    public void visitContinueBidingImpulseStrongMutSetDeleteEffect(ContinueBidingImpulseStrongMutSetDeleteEffect effect) {
      root.EffectContinueBidingImpulseStrongMutSetDelete(effect.id);
    }
    public void visitContinueBidingImpulseStrongMutSetAddEffect(ContinueBidingImpulseStrongMutSetAddEffect effect) {
     root.EffectContinueBidingImpulseStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitContinueBidingImpulseStrongMutSetRemoveEffect(ContinueBidingImpulseStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectContinueBidingImpulseStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitStartBidingImpulseStrongMutSetEffect(IStartBidingImpulseStrongMutSetEffect effect) { effect.visitIStartBidingImpulseStrongMutSetEffect(this); }
    public void visitStartBidingImpulseStrongMutSetCreateEffect(StartBidingImpulseStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectStartBidingImpulseStrongMutSetCreateWithId(effect.id);
    }
    public void visitStartBidingImpulseStrongMutSetDeleteEffect(StartBidingImpulseStrongMutSetDeleteEffect effect) {
      root.EffectStartBidingImpulseStrongMutSetDelete(effect.id);
    }
    public void visitStartBidingImpulseStrongMutSetAddEffect(StartBidingImpulseStrongMutSetAddEffect effect) {
     root.EffectStartBidingImpulseStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitStartBidingImpulseStrongMutSetRemoveEffect(StartBidingImpulseStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectStartBidingImpulseStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitAttackImpulseStrongMutSetEffect(IAttackImpulseStrongMutSetEffect effect) { effect.visitIAttackImpulseStrongMutSetEffect(this); }
    public void visitAttackImpulseStrongMutSetCreateEffect(AttackImpulseStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectAttackImpulseStrongMutSetCreateWithId(effect.id);
    }
    public void visitAttackImpulseStrongMutSetDeleteEffect(AttackImpulseStrongMutSetDeleteEffect effect) {
      root.EffectAttackImpulseStrongMutSetDelete(effect.id);
    }
    public void visitAttackImpulseStrongMutSetAddEffect(AttackImpulseStrongMutSetAddEffect effect) {
     root.EffectAttackImpulseStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitAttackImpulseStrongMutSetRemoveEffect(AttackImpulseStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectAttackImpulseStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitPursueImpulseStrongMutSetEffect(IPursueImpulseStrongMutSetEffect effect) { effect.visitIPursueImpulseStrongMutSetEffect(this); }
    public void visitPursueImpulseStrongMutSetCreateEffect(PursueImpulseStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectPursueImpulseStrongMutSetCreateWithId(effect.id);
    }
    public void visitPursueImpulseStrongMutSetDeleteEffect(PursueImpulseStrongMutSetDeleteEffect effect) {
      root.EffectPursueImpulseStrongMutSetDelete(effect.id);
    }
    public void visitPursueImpulseStrongMutSetAddEffect(PursueImpulseStrongMutSetAddEffect effect) {
     root.EffectPursueImpulseStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitPursueImpulseStrongMutSetRemoveEffect(PursueImpulseStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectPursueImpulseStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitFireBombImpulseStrongMutSetEffect(IFireBombImpulseStrongMutSetEffect effect) { effect.visitIFireBombImpulseStrongMutSetEffect(this); }
    public void visitFireBombImpulseStrongMutSetCreateEffect(FireBombImpulseStrongMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectFireBombImpulseStrongMutSetCreateWithId(effect.id);
    }
    public void visitFireBombImpulseStrongMutSetDeleteEffect(FireBombImpulseStrongMutSetDeleteEffect effect) {
      root.EffectFireBombImpulseStrongMutSetDelete(effect.id);
    }
    public void visitFireBombImpulseStrongMutSetAddEffect(FireBombImpulseStrongMutSetAddEffect effect) {
     root.EffectFireBombImpulseStrongMutSetAdd(effect.id, effect.element);
 }
    public void visitFireBombImpulseStrongMutSetRemoveEffect(FireBombImpulseStrongMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectFireBombImpulseStrongMutSetRemove(effect.id, effect.element);
    }
       
    public void visitLightningChargedUCWeakMutSetEffect(ILightningChargedUCWeakMutSetEffect effect) { effect.visitILightningChargedUCWeakMutSetEffect(this); }
    public void visitLightningChargedUCWeakMutSetCreateEffect(LightningChargedUCWeakMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectLightningChargedUCWeakMutSetCreateWithId(effect.id);
    }
    public void visitLightningChargedUCWeakMutSetDeleteEffect(LightningChargedUCWeakMutSetDeleteEffect effect) {
      root.EffectLightningChargedUCWeakMutSetDelete(effect.id);
    }
    public void visitLightningChargedUCWeakMutSetAddEffect(LightningChargedUCWeakMutSetAddEffect effect) {
     root.EffectLightningChargedUCWeakMutSetAdd(effect.id, effect.element);
 }
    public void visitLightningChargedUCWeakMutSetRemoveEffect(LightningChargedUCWeakMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectLightningChargedUCWeakMutSetRemove(effect.id, effect.element);
    }
       
    public void visitTimeCloneAICapabilityUCWeakMutSetEffect(ITimeCloneAICapabilityUCWeakMutSetEffect effect) { effect.visitITimeCloneAICapabilityUCWeakMutSetEffect(this); }
    public void visitTimeCloneAICapabilityUCWeakMutSetCreateEffect(TimeCloneAICapabilityUCWeakMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectTimeCloneAICapabilityUCWeakMutSetCreateWithId(effect.id);
    }
    public void visitTimeCloneAICapabilityUCWeakMutSetDeleteEffect(TimeCloneAICapabilityUCWeakMutSetDeleteEffect effect) {
      root.EffectTimeCloneAICapabilityUCWeakMutSetDelete(effect.id);
    }
    public void visitTimeCloneAICapabilityUCWeakMutSetAddEffect(TimeCloneAICapabilityUCWeakMutSetAddEffect effect) {
     root.EffectTimeCloneAICapabilityUCWeakMutSetAdd(effect.id, effect.element);
 }
    public void visitTimeCloneAICapabilityUCWeakMutSetRemoveEffect(TimeCloneAICapabilityUCWeakMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectTimeCloneAICapabilityUCWeakMutSetRemove(effect.id, effect.element);
    }
       
    public void visitDoomedUCWeakMutSetEffect(IDoomedUCWeakMutSetEffect effect) { effect.visitIDoomedUCWeakMutSetEffect(this); }
    public void visitDoomedUCWeakMutSetCreateEffect(DoomedUCWeakMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectDoomedUCWeakMutSetCreateWithId(effect.id);
    }
    public void visitDoomedUCWeakMutSetDeleteEffect(DoomedUCWeakMutSetDeleteEffect effect) {
      root.EffectDoomedUCWeakMutSetDelete(effect.id);
    }
    public void visitDoomedUCWeakMutSetAddEffect(DoomedUCWeakMutSetAddEffect effect) {
     root.EffectDoomedUCWeakMutSetAdd(effect.id, effect.element);
 }
    public void visitDoomedUCWeakMutSetRemoveEffect(DoomedUCWeakMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectDoomedUCWeakMutSetRemove(effect.id, effect.element);
    }
       
    public void visitMiredUCWeakMutSetEffect(IMiredUCWeakMutSetEffect effect) { effect.visitIMiredUCWeakMutSetEffect(this); }
    public void visitMiredUCWeakMutSetCreateEffect(MiredUCWeakMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectMiredUCWeakMutSetCreateWithId(effect.id);
    }
    public void visitMiredUCWeakMutSetDeleteEffect(MiredUCWeakMutSetDeleteEffect effect) {
      root.EffectMiredUCWeakMutSetDelete(effect.id);
    }
    public void visitMiredUCWeakMutSetAddEffect(MiredUCWeakMutSetAddEffect effect) {
     root.EffectMiredUCWeakMutSetAdd(effect.id, effect.element);
 }
    public void visitMiredUCWeakMutSetRemoveEffect(MiredUCWeakMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectMiredUCWeakMutSetRemove(effect.id, effect.element);
    }
       
    public void visitInvincibilityUCWeakMutSetEffect(IInvincibilityUCWeakMutSetEffect effect) { effect.visitIInvincibilityUCWeakMutSetEffect(this); }
    public void visitInvincibilityUCWeakMutSetCreateEffect(InvincibilityUCWeakMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectInvincibilityUCWeakMutSetCreateWithId(effect.id);
    }
    public void visitInvincibilityUCWeakMutSetDeleteEffect(InvincibilityUCWeakMutSetDeleteEffect effect) {
      root.EffectInvincibilityUCWeakMutSetDelete(effect.id);
    }
    public void visitInvincibilityUCWeakMutSetAddEffect(InvincibilityUCWeakMutSetAddEffect effect) {
     root.EffectInvincibilityUCWeakMutSetAdd(effect.id, effect.element);
 }
    public void visitInvincibilityUCWeakMutSetRemoveEffect(InvincibilityUCWeakMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectInvincibilityUCWeakMutSetRemove(effect.id, effect.element);
    }
       
    public void visitDefyingUCWeakMutSetEffect(IDefyingUCWeakMutSetEffect effect) { effect.visitIDefyingUCWeakMutSetEffect(this); }
    public void visitDefyingUCWeakMutSetCreateEffect(DefyingUCWeakMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectDefyingUCWeakMutSetCreateWithId(effect.id);
    }
    public void visitDefyingUCWeakMutSetDeleteEffect(DefyingUCWeakMutSetDeleteEffect effect) {
      root.EffectDefyingUCWeakMutSetDelete(effect.id);
    }
    public void visitDefyingUCWeakMutSetAddEffect(DefyingUCWeakMutSetAddEffect effect) {
     root.EffectDefyingUCWeakMutSetAdd(effect.id, effect.element);
 }
    public void visitDefyingUCWeakMutSetRemoveEffect(DefyingUCWeakMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectDefyingUCWeakMutSetRemove(effect.id, effect.element);
    }
       
    public void visitCounteringUCWeakMutSetEffect(ICounteringUCWeakMutSetEffect effect) { effect.visitICounteringUCWeakMutSetEffect(this); }
    public void visitCounteringUCWeakMutSetCreateEffect(CounteringUCWeakMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectCounteringUCWeakMutSetCreateWithId(effect.id);
    }
    public void visitCounteringUCWeakMutSetDeleteEffect(CounteringUCWeakMutSetDeleteEffect effect) {
      root.EffectCounteringUCWeakMutSetDelete(effect.id);
    }
    public void visitCounteringUCWeakMutSetAddEffect(CounteringUCWeakMutSetAddEffect effect) {
     root.EffectCounteringUCWeakMutSetAdd(effect.id, effect.element);
 }
    public void visitCounteringUCWeakMutSetRemoveEffect(CounteringUCWeakMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectCounteringUCWeakMutSetRemove(effect.id, effect.element);
    }
       
    public void visitAttackAICapabilityUCWeakMutSetEffect(IAttackAICapabilityUCWeakMutSetEffect effect) { effect.visitIAttackAICapabilityUCWeakMutSetEffect(this); }
    public void visitAttackAICapabilityUCWeakMutSetCreateEffect(AttackAICapabilityUCWeakMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectAttackAICapabilityUCWeakMutSetCreateWithId(effect.id);
    }
    public void visitAttackAICapabilityUCWeakMutSetDeleteEffect(AttackAICapabilityUCWeakMutSetDeleteEffect effect) {
      root.EffectAttackAICapabilityUCWeakMutSetDelete(effect.id);
    }
    public void visitAttackAICapabilityUCWeakMutSetAddEffect(AttackAICapabilityUCWeakMutSetAddEffect effect) {
     root.EffectAttackAICapabilityUCWeakMutSetAdd(effect.id, effect.element);
 }
    public void visitAttackAICapabilityUCWeakMutSetRemoveEffect(AttackAICapabilityUCWeakMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectAttackAICapabilityUCWeakMutSetRemove(effect.id, effect.element);
    }
       
    public void visitUnitMutSetEffect(IUnitMutSetEffect effect) { effect.visitIUnitMutSetEffect(this); }
    public void visitUnitMutSetCreateEffect(UnitMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectUnitMutSetCreateWithId(effect.id);
    }
    public void visitUnitMutSetDeleteEffect(UnitMutSetDeleteEffect effect) {
      root.EffectUnitMutSetDelete(effect.id);
    }
    public void visitUnitMutSetAddEffect(UnitMutSetAddEffect effect) {
     root.EffectUnitMutSetAdd(effect.id, effect.element);
 }
    public void visitUnitMutSetRemoveEffect(UnitMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectUnitMutSetRemove(effect.id, effect.element);
    }
       
    public void visitSimplePresenceTriggerTTCMutSetEffect(ISimplePresenceTriggerTTCMutSetEffect effect) { effect.visitISimplePresenceTriggerTTCMutSetEffect(this); }
    public void visitSimplePresenceTriggerTTCMutSetCreateEffect(SimplePresenceTriggerTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectSimplePresenceTriggerTTCMutSetCreateWithId(effect.id);
    }
    public void visitSimplePresenceTriggerTTCMutSetDeleteEffect(SimplePresenceTriggerTTCMutSetDeleteEffect effect) {
      root.EffectSimplePresenceTriggerTTCMutSetDelete(effect.id);
    }
    public void visitSimplePresenceTriggerTTCMutSetAddEffect(SimplePresenceTriggerTTCMutSetAddEffect effect) {
     root.EffectSimplePresenceTriggerTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitSimplePresenceTriggerTTCMutSetRemoveEffect(SimplePresenceTriggerTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectSimplePresenceTriggerTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitItemTTCMutSetEffect(IItemTTCMutSetEffect effect) { effect.visitIItemTTCMutSetEffect(this); }
    public void visitItemTTCMutSetCreateEffect(ItemTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectItemTTCMutSetCreateWithId(effect.id);
    }
    public void visitItemTTCMutSetDeleteEffect(ItemTTCMutSetDeleteEffect effect) {
      root.EffectItemTTCMutSetDelete(effect.id);
    }
    public void visitItemTTCMutSetAddEffect(ItemTTCMutSetAddEffect effect) {
     root.EffectItemTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitItemTTCMutSetRemoveEffect(ItemTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectItemTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitKamikazeTargetTTCMutSetEffect(IKamikazeTargetTTCMutSetEffect effect) { effect.visitIKamikazeTargetTTCMutSetEffect(this); }
    public void visitKamikazeTargetTTCMutSetCreateEffect(KamikazeTargetTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectKamikazeTargetTTCMutSetCreateWithId(effect.id);
    }
    public void visitKamikazeTargetTTCMutSetDeleteEffect(KamikazeTargetTTCMutSetDeleteEffect effect) {
      root.EffectKamikazeTargetTTCMutSetDelete(effect.id);
    }
    public void visitKamikazeTargetTTCMutSetAddEffect(KamikazeTargetTTCMutSetAddEffect effect) {
     root.EffectKamikazeTargetTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitKamikazeTargetTTCMutSetRemoveEffect(KamikazeTargetTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectKamikazeTargetTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitWarperTTCMutSetEffect(IWarperTTCMutSetEffect effect) { effect.visitIWarperTTCMutSetEffect(this); }
    public void visitWarperTTCMutSetCreateEffect(WarperTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectWarperTTCMutSetCreateWithId(effect.id);
    }
    public void visitWarperTTCMutSetDeleteEffect(WarperTTCMutSetDeleteEffect effect) {
      root.EffectWarperTTCMutSetDelete(effect.id);
    }
    public void visitWarperTTCMutSetAddEffect(WarperTTCMutSetAddEffect effect) {
     root.EffectWarperTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitWarperTTCMutSetRemoveEffect(WarperTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectWarperTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitTimeAnchorTTCMutSetEffect(ITimeAnchorTTCMutSetEffect effect) { effect.visitITimeAnchorTTCMutSetEffect(this); }
    public void visitTimeAnchorTTCMutSetCreateEffect(TimeAnchorTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectTimeAnchorTTCMutSetCreateWithId(effect.id);
    }
    public void visitTimeAnchorTTCMutSetDeleteEffect(TimeAnchorTTCMutSetDeleteEffect effect) {
      root.EffectTimeAnchorTTCMutSetDelete(effect.id);
    }
    public void visitTimeAnchorTTCMutSetAddEffect(TimeAnchorTTCMutSetAddEffect effect) {
     root.EffectTimeAnchorTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitTimeAnchorTTCMutSetRemoveEffect(TimeAnchorTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectTimeAnchorTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitFireBombTTCMutSetEffect(IFireBombTTCMutSetEffect effect) { effect.visitIFireBombTTCMutSetEffect(this); }
    public void visitFireBombTTCMutSetCreateEffect(FireBombTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectFireBombTTCMutSetCreateWithId(effect.id);
    }
    public void visitFireBombTTCMutSetDeleteEffect(FireBombTTCMutSetDeleteEffect effect) {
      root.EffectFireBombTTCMutSetDelete(effect.id);
    }
    public void visitFireBombTTCMutSetAddEffect(FireBombTTCMutSetAddEffect effect) {
     root.EffectFireBombTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitFireBombTTCMutSetRemoveEffect(FireBombTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectFireBombTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitMarkerTTCMutSetEffect(IMarkerTTCMutSetEffect effect) { effect.visitIMarkerTTCMutSetEffect(this); }
    public void visitMarkerTTCMutSetCreateEffect(MarkerTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectMarkerTTCMutSetCreateWithId(effect.id);
    }
    public void visitMarkerTTCMutSetDeleteEffect(MarkerTTCMutSetDeleteEffect effect) {
      root.EffectMarkerTTCMutSetDelete(effect.id);
    }
    public void visitMarkerTTCMutSetAddEffect(MarkerTTCMutSetAddEffect effect) {
     root.EffectMarkerTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitMarkerTTCMutSetRemoveEffect(MarkerTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectMarkerTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitLevelLinkTTCMutSetEffect(ILevelLinkTTCMutSetEffect effect) { effect.visitILevelLinkTTCMutSetEffect(this); }
    public void visitLevelLinkTTCMutSetCreateEffect(LevelLinkTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectLevelLinkTTCMutSetCreateWithId(effect.id);
    }
    public void visitLevelLinkTTCMutSetDeleteEffect(LevelLinkTTCMutSetDeleteEffect effect) {
      root.EffectLevelLinkTTCMutSetDelete(effect.id);
    }
    public void visitLevelLinkTTCMutSetAddEffect(LevelLinkTTCMutSetAddEffect effect) {
     root.EffectLevelLinkTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitLevelLinkTTCMutSetRemoveEffect(LevelLinkTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectLevelLinkTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitMudTTCMutSetEffect(IMudTTCMutSetEffect effect) { effect.visitIMudTTCMutSetEffect(this); }
    public void visitMudTTCMutSetCreateEffect(MudTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectMudTTCMutSetCreateWithId(effect.id);
    }
    public void visitMudTTCMutSetDeleteEffect(MudTTCMutSetDeleteEffect effect) {
      root.EffectMudTTCMutSetDelete(effect.id);
    }
    public void visitMudTTCMutSetAddEffect(MudTTCMutSetAddEffect effect) {
     root.EffectMudTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitMudTTCMutSetRemoveEffect(MudTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectMudTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitDirtTTCMutSetEffect(IDirtTTCMutSetEffect effect) { effect.visitIDirtTTCMutSetEffect(this); }
    public void visitDirtTTCMutSetCreateEffect(DirtTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectDirtTTCMutSetCreateWithId(effect.id);
    }
    public void visitDirtTTCMutSetDeleteEffect(DirtTTCMutSetDeleteEffect effect) {
      root.EffectDirtTTCMutSetDelete(effect.id);
    }
    public void visitDirtTTCMutSetAddEffect(DirtTTCMutSetAddEffect effect) {
     root.EffectDirtTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitDirtTTCMutSetRemoveEffect(DirtTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectDirtTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitObsidianTTCMutSetEffect(IObsidianTTCMutSetEffect effect) { effect.visitIObsidianTTCMutSetEffect(this); }
    public void visitObsidianTTCMutSetCreateEffect(ObsidianTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectObsidianTTCMutSetCreateWithId(effect.id);
    }
    public void visitObsidianTTCMutSetDeleteEffect(ObsidianTTCMutSetDeleteEffect effect) {
      root.EffectObsidianTTCMutSetDelete(effect.id);
    }
    public void visitObsidianTTCMutSetAddEffect(ObsidianTTCMutSetAddEffect effect) {
     root.EffectObsidianTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitObsidianTTCMutSetRemoveEffect(ObsidianTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectObsidianTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitDownStairsTTCMutSetEffect(IDownStairsTTCMutSetEffect effect) { effect.visitIDownStairsTTCMutSetEffect(this); }
    public void visitDownStairsTTCMutSetCreateEffect(DownStairsTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectDownStairsTTCMutSetCreateWithId(effect.id);
    }
    public void visitDownStairsTTCMutSetDeleteEffect(DownStairsTTCMutSetDeleteEffect effect) {
      root.EffectDownStairsTTCMutSetDelete(effect.id);
    }
    public void visitDownStairsTTCMutSetAddEffect(DownStairsTTCMutSetAddEffect effect) {
     root.EffectDownStairsTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitDownStairsTTCMutSetRemoveEffect(DownStairsTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectDownStairsTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitUpStairsTTCMutSetEffect(IUpStairsTTCMutSetEffect effect) { effect.visitIUpStairsTTCMutSetEffect(this); }
    public void visitUpStairsTTCMutSetCreateEffect(UpStairsTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectUpStairsTTCMutSetCreateWithId(effect.id);
    }
    public void visitUpStairsTTCMutSetDeleteEffect(UpStairsTTCMutSetDeleteEffect effect) {
      root.EffectUpStairsTTCMutSetDelete(effect.id);
    }
    public void visitUpStairsTTCMutSetAddEffect(UpStairsTTCMutSetAddEffect effect) {
     root.EffectUpStairsTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitUpStairsTTCMutSetRemoveEffect(UpStairsTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectUpStairsTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitWallTTCMutSetEffect(IWallTTCMutSetEffect effect) { effect.visitIWallTTCMutSetEffect(this); }
    public void visitWallTTCMutSetCreateEffect(WallTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectWallTTCMutSetCreateWithId(effect.id);
    }
    public void visitWallTTCMutSetDeleteEffect(WallTTCMutSetDeleteEffect effect) {
      root.EffectWallTTCMutSetDelete(effect.id);
    }
    public void visitWallTTCMutSetAddEffect(WallTTCMutSetAddEffect effect) {
     root.EffectWallTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitWallTTCMutSetRemoveEffect(WallTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectWallTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitBloodTTCMutSetEffect(IBloodTTCMutSetEffect effect) { effect.visitIBloodTTCMutSetEffect(this); }
    public void visitBloodTTCMutSetCreateEffect(BloodTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectBloodTTCMutSetCreateWithId(effect.id);
    }
    public void visitBloodTTCMutSetDeleteEffect(BloodTTCMutSetDeleteEffect effect) {
      root.EffectBloodTTCMutSetDelete(effect.id);
    }
    public void visitBloodTTCMutSetAddEffect(BloodTTCMutSetAddEffect effect) {
     root.EffectBloodTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitBloodTTCMutSetRemoveEffect(BloodTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectBloodTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitRocksTTCMutSetEffect(IRocksTTCMutSetEffect effect) { effect.visitIRocksTTCMutSetEffect(this); }
    public void visitRocksTTCMutSetCreateEffect(RocksTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectRocksTTCMutSetCreateWithId(effect.id);
    }
    public void visitRocksTTCMutSetDeleteEffect(RocksTTCMutSetDeleteEffect effect) {
      root.EffectRocksTTCMutSetDelete(effect.id);
    }
    public void visitRocksTTCMutSetAddEffect(RocksTTCMutSetAddEffect effect) {
     root.EffectRocksTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitRocksTTCMutSetRemoveEffect(RocksTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectRocksTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitTreeTTCMutSetEffect(ITreeTTCMutSetEffect effect) { effect.visitITreeTTCMutSetEffect(this); }
    public void visitTreeTTCMutSetCreateEffect(TreeTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectTreeTTCMutSetCreateWithId(effect.id);
    }
    public void visitTreeTTCMutSetDeleteEffect(TreeTTCMutSetDeleteEffect effect) {
      root.EffectTreeTTCMutSetDelete(effect.id);
    }
    public void visitTreeTTCMutSetAddEffect(TreeTTCMutSetAddEffect effect) {
     root.EffectTreeTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitTreeTTCMutSetRemoveEffect(TreeTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectTreeTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitWaterTTCMutSetEffect(IWaterTTCMutSetEffect effect) { effect.visitIWaterTTCMutSetEffect(this); }
    public void visitWaterTTCMutSetCreateEffect(WaterTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectWaterTTCMutSetCreateWithId(effect.id);
    }
    public void visitWaterTTCMutSetDeleteEffect(WaterTTCMutSetDeleteEffect effect) {
      root.EffectWaterTTCMutSetDelete(effect.id);
    }
    public void visitWaterTTCMutSetAddEffect(WaterTTCMutSetAddEffect effect) {
     root.EffectWaterTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitWaterTTCMutSetRemoveEffect(WaterTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectWaterTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitFloorTTCMutSetEffect(IFloorTTCMutSetEffect effect) { effect.visitIFloorTTCMutSetEffect(this); }
    public void visitFloorTTCMutSetCreateEffect(FloorTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectFloorTTCMutSetCreateWithId(effect.id);
    }
    public void visitFloorTTCMutSetDeleteEffect(FloorTTCMutSetDeleteEffect effect) {
      root.EffectFloorTTCMutSetDelete(effect.id);
    }
    public void visitFloorTTCMutSetAddEffect(FloorTTCMutSetAddEffect effect) {
     root.EffectFloorTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitFloorTTCMutSetRemoveEffect(FloorTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectFloorTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitCaveWallTTCMutSetEffect(ICaveWallTTCMutSetEffect effect) { effect.visitICaveWallTTCMutSetEffect(this); }
    public void visitCaveWallTTCMutSetCreateEffect(CaveWallTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectCaveWallTTCMutSetCreateWithId(effect.id);
    }
    public void visitCaveWallTTCMutSetDeleteEffect(CaveWallTTCMutSetDeleteEffect effect) {
      root.EffectCaveWallTTCMutSetDelete(effect.id);
    }
    public void visitCaveWallTTCMutSetAddEffect(CaveWallTTCMutSetAddEffect effect) {
     root.EffectCaveWallTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitCaveWallTTCMutSetRemoveEffect(CaveWallTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectCaveWallTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitCaveTTCMutSetEffect(ICaveTTCMutSetEffect effect) { effect.visitICaveTTCMutSetEffect(this); }
    public void visitCaveTTCMutSetCreateEffect(CaveTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectCaveTTCMutSetCreateWithId(effect.id);
    }
    public void visitCaveTTCMutSetDeleteEffect(CaveTTCMutSetDeleteEffect effect) {
      root.EffectCaveTTCMutSetDelete(effect.id);
    }
    public void visitCaveTTCMutSetAddEffect(CaveTTCMutSetAddEffect effect) {
     root.EffectCaveTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitCaveTTCMutSetRemoveEffect(CaveTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectCaveTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitFallsTTCMutSetEffect(IFallsTTCMutSetEffect effect) { effect.visitIFallsTTCMutSetEffect(this); }
    public void visitFallsTTCMutSetCreateEffect(FallsTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectFallsTTCMutSetCreateWithId(effect.id);
    }
    public void visitFallsTTCMutSetDeleteEffect(FallsTTCMutSetDeleteEffect effect) {
      root.EffectFallsTTCMutSetDelete(effect.id);
    }
    public void visitFallsTTCMutSetAddEffect(FallsTTCMutSetAddEffect effect) {
     root.EffectFallsTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitFallsTTCMutSetRemoveEffect(FallsTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectFallsTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitFireTTCMutSetEffect(IFireTTCMutSetEffect effect) { effect.visitIFireTTCMutSetEffect(this); }
    public void visitFireTTCMutSetCreateEffect(FireTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectFireTTCMutSetCreateWithId(effect.id);
    }
    public void visitFireTTCMutSetDeleteEffect(FireTTCMutSetDeleteEffect effect) {
      root.EffectFireTTCMutSetDelete(effect.id);
    }
    public void visitFireTTCMutSetAddEffect(FireTTCMutSetAddEffect effect) {
     root.EffectFireTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitFireTTCMutSetRemoveEffect(FireTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectFireTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitObsidianFloorTTCMutSetEffect(IObsidianFloorTTCMutSetEffect effect) { effect.visitIObsidianFloorTTCMutSetEffect(this); }
    public void visitObsidianFloorTTCMutSetCreateEffect(ObsidianFloorTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectObsidianFloorTTCMutSetCreateWithId(effect.id);
    }
    public void visitObsidianFloorTTCMutSetDeleteEffect(ObsidianFloorTTCMutSetDeleteEffect effect) {
      root.EffectObsidianFloorTTCMutSetDelete(effect.id);
    }
    public void visitObsidianFloorTTCMutSetAddEffect(ObsidianFloorTTCMutSetAddEffect effect) {
     root.EffectObsidianFloorTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitObsidianFloorTTCMutSetRemoveEffect(ObsidianFloorTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectObsidianFloorTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitMagmaTTCMutSetEffect(IMagmaTTCMutSetEffect effect) { effect.visitIMagmaTTCMutSetEffect(this); }
    public void visitMagmaTTCMutSetCreateEffect(MagmaTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectMagmaTTCMutSetCreateWithId(effect.id);
    }
    public void visitMagmaTTCMutSetDeleteEffect(MagmaTTCMutSetDeleteEffect effect) {
      root.EffectMagmaTTCMutSetDelete(effect.id);
    }
    public void visitMagmaTTCMutSetAddEffect(MagmaTTCMutSetAddEffect effect) {
     root.EffectMagmaTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitMagmaTTCMutSetRemoveEffect(MagmaTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectMagmaTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitCliffTTCMutSetEffect(ICliffTTCMutSetEffect effect) { effect.visitICliffTTCMutSetEffect(this); }
    public void visitCliffTTCMutSetCreateEffect(CliffTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectCliffTTCMutSetCreateWithId(effect.id);
    }
    public void visitCliffTTCMutSetDeleteEffect(CliffTTCMutSetDeleteEffect effect) {
      root.EffectCliffTTCMutSetDelete(effect.id);
    }
    public void visitCliffTTCMutSetAddEffect(CliffTTCMutSetAddEffect effect) {
     root.EffectCliffTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitCliffTTCMutSetRemoveEffect(CliffTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectCliffTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitRavaNestTTCMutSetEffect(IRavaNestTTCMutSetEffect effect) { effect.visitIRavaNestTTCMutSetEffect(this); }
    public void visitRavaNestTTCMutSetCreateEffect(RavaNestTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectRavaNestTTCMutSetCreateWithId(effect.id);
    }
    public void visitRavaNestTTCMutSetDeleteEffect(RavaNestTTCMutSetDeleteEffect effect) {
      root.EffectRavaNestTTCMutSetDelete(effect.id);
    }
    public void visitRavaNestTTCMutSetAddEffect(RavaNestTTCMutSetAddEffect effect) {
     root.EffectRavaNestTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitRavaNestTTCMutSetRemoveEffect(RavaNestTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectRavaNestTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitCliffLandingTTCMutSetEffect(ICliffLandingTTCMutSetEffect effect) { effect.visitICliffLandingTTCMutSetEffect(this); }
    public void visitCliffLandingTTCMutSetCreateEffect(CliffLandingTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectCliffLandingTTCMutSetCreateWithId(effect.id);
    }
    public void visitCliffLandingTTCMutSetDeleteEffect(CliffLandingTTCMutSetDeleteEffect effect) {
      root.EffectCliffLandingTTCMutSetDelete(effect.id);
    }
    public void visitCliffLandingTTCMutSetAddEffect(CliffLandingTTCMutSetAddEffect effect) {
     root.EffectCliffLandingTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitCliffLandingTTCMutSetRemoveEffect(CliffLandingTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectCliffLandingTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitStoneTTCMutSetEffect(IStoneTTCMutSetEffect effect) { effect.visitIStoneTTCMutSetEffect(this); }
    public void visitStoneTTCMutSetCreateEffect(StoneTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectStoneTTCMutSetCreateWithId(effect.id);
    }
    public void visitStoneTTCMutSetDeleteEffect(StoneTTCMutSetDeleteEffect effect) {
      root.EffectStoneTTCMutSetDelete(effect.id);
    }
    public void visitStoneTTCMutSetAddEffect(StoneTTCMutSetAddEffect effect) {
     root.EffectStoneTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitStoneTTCMutSetRemoveEffect(StoneTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectStoneTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitGrassTTCMutSetEffect(IGrassTTCMutSetEffect effect) { effect.visitIGrassTTCMutSetEffect(this); }
    public void visitGrassTTCMutSetCreateEffect(GrassTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectGrassTTCMutSetCreateWithId(effect.id);
    }
    public void visitGrassTTCMutSetDeleteEffect(GrassTTCMutSetDeleteEffect effect) {
      root.EffectGrassTTCMutSetDelete(effect.id);
    }
    public void visitGrassTTCMutSetAddEffect(GrassTTCMutSetAddEffect effect) {
     root.EffectGrassTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitGrassTTCMutSetRemoveEffect(GrassTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectGrassTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitIncendianFallsLevelLinkerTTCMutSetEffect(IIncendianFallsLevelLinkerTTCMutSetEffect effect) { effect.visitIIncendianFallsLevelLinkerTTCMutSetEffect(this); }
    public void visitIncendianFallsLevelLinkerTTCMutSetCreateEffect(IncendianFallsLevelLinkerTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectIncendianFallsLevelLinkerTTCMutSetCreateWithId(effect.id);
    }
    public void visitIncendianFallsLevelLinkerTTCMutSetDeleteEffect(IncendianFallsLevelLinkerTTCMutSetDeleteEffect effect) {
      root.EffectIncendianFallsLevelLinkerTTCMutSetDelete(effect.id);
    }
    public void visitIncendianFallsLevelLinkerTTCMutSetAddEffect(IncendianFallsLevelLinkerTTCMutSetAddEffect effect) {
     root.EffectIncendianFallsLevelLinkerTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitIncendianFallsLevelLinkerTTCMutSetRemoveEffect(IncendianFallsLevelLinkerTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectIncendianFallsLevelLinkerTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitEmberDeepLevelLinkerTTCMutSetEffect(IEmberDeepLevelLinkerTTCMutSetEffect effect) { effect.visitIEmberDeepLevelLinkerTTCMutSetEffect(this); }
    public void visitEmberDeepLevelLinkerTTCMutSetCreateEffect(EmberDeepLevelLinkerTTCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectEmberDeepLevelLinkerTTCMutSetCreateWithId(effect.id);
    }
    public void visitEmberDeepLevelLinkerTTCMutSetDeleteEffect(EmberDeepLevelLinkerTTCMutSetDeleteEffect effect) {
      root.EffectEmberDeepLevelLinkerTTCMutSetDelete(effect.id);
    }
    public void visitEmberDeepLevelLinkerTTCMutSetAddEffect(EmberDeepLevelLinkerTTCMutSetAddEffect effect) {
     root.EffectEmberDeepLevelLinkerTTCMutSetAdd(effect.id, effect.element);
 }
    public void visitEmberDeepLevelLinkerTTCMutSetRemoveEffect(EmberDeepLevelLinkerTTCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectEmberDeepLevelLinkerTTCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitTutorialDefyCounterUCMutSetEffect(ITutorialDefyCounterUCMutSetEffect effect) { effect.visitITutorialDefyCounterUCMutSetEffect(this); }
    public void visitTutorialDefyCounterUCMutSetCreateEffect(TutorialDefyCounterUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectTutorialDefyCounterUCMutSetCreateWithId(effect.id);
    }
    public void visitTutorialDefyCounterUCMutSetDeleteEffect(TutorialDefyCounterUCMutSetDeleteEffect effect) {
      root.EffectTutorialDefyCounterUCMutSetDelete(effect.id);
    }
    public void visitTutorialDefyCounterUCMutSetAddEffect(TutorialDefyCounterUCMutSetAddEffect effect) {
     root.EffectTutorialDefyCounterUCMutSetAdd(effect.id, effect.element);
 }
    public void visitTutorialDefyCounterUCMutSetRemoveEffect(TutorialDefyCounterUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectTutorialDefyCounterUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitLightningChargingUCMutSetEffect(ILightningChargingUCMutSetEffect effect) { effect.visitILightningChargingUCMutSetEffect(this); }
    public void visitLightningChargingUCMutSetCreateEffect(LightningChargingUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectLightningChargingUCMutSetCreateWithId(effect.id);
    }
    public void visitLightningChargingUCMutSetDeleteEffect(LightningChargingUCMutSetDeleteEffect effect) {
      root.EffectLightningChargingUCMutSetDelete(effect.id);
    }
    public void visitLightningChargingUCMutSetAddEffect(LightningChargingUCMutSetAddEffect effect) {
     root.EffectLightningChargingUCMutSetAdd(effect.id, effect.element);
 }
    public void visitLightningChargingUCMutSetRemoveEffect(LightningChargingUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectLightningChargingUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitWanderAICapabilityUCMutSetEffect(IWanderAICapabilityUCMutSetEffect effect) { effect.visitIWanderAICapabilityUCMutSetEffect(this); }
    public void visitWanderAICapabilityUCMutSetCreateEffect(WanderAICapabilityUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectWanderAICapabilityUCMutSetCreateWithId(effect.id);
    }
    public void visitWanderAICapabilityUCMutSetDeleteEffect(WanderAICapabilityUCMutSetDeleteEffect effect) {
      root.EffectWanderAICapabilityUCMutSetDelete(effect.id);
    }
    public void visitWanderAICapabilityUCMutSetAddEffect(WanderAICapabilityUCMutSetAddEffect effect) {
     root.EffectWanderAICapabilityUCMutSetAdd(effect.id, effect.element);
 }
    public void visitWanderAICapabilityUCMutSetRemoveEffect(WanderAICapabilityUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectWanderAICapabilityUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitTemporaryCloneAICapabilityUCMutSetEffect(ITemporaryCloneAICapabilityUCMutSetEffect effect) { effect.visitITemporaryCloneAICapabilityUCMutSetEffect(this); }
    public void visitTemporaryCloneAICapabilityUCMutSetCreateEffect(TemporaryCloneAICapabilityUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectTemporaryCloneAICapabilityUCMutSetCreateWithId(effect.id);
    }
    public void visitTemporaryCloneAICapabilityUCMutSetDeleteEffect(TemporaryCloneAICapabilityUCMutSetDeleteEffect effect) {
      root.EffectTemporaryCloneAICapabilityUCMutSetDelete(effect.id);
    }
    public void visitTemporaryCloneAICapabilityUCMutSetAddEffect(TemporaryCloneAICapabilityUCMutSetAddEffect effect) {
     root.EffectTemporaryCloneAICapabilityUCMutSetAdd(effect.id, effect.element);
 }
    public void visitTemporaryCloneAICapabilityUCMutSetRemoveEffect(TemporaryCloneAICapabilityUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectTemporaryCloneAICapabilityUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitSummonAICapabilityUCMutSetEffect(ISummonAICapabilityUCMutSetEffect effect) { effect.visitISummonAICapabilityUCMutSetEffect(this); }
    public void visitSummonAICapabilityUCMutSetCreateEffect(SummonAICapabilityUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectSummonAICapabilityUCMutSetCreateWithId(effect.id);
    }
    public void visitSummonAICapabilityUCMutSetDeleteEffect(SummonAICapabilityUCMutSetDeleteEffect effect) {
      root.EffectSummonAICapabilityUCMutSetDelete(effect.id);
    }
    public void visitSummonAICapabilityUCMutSetAddEffect(SummonAICapabilityUCMutSetAddEffect effect) {
     root.EffectSummonAICapabilityUCMutSetAdd(effect.id, effect.element);
 }
    public void visitSummonAICapabilityUCMutSetRemoveEffect(SummonAICapabilityUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectSummonAICapabilityUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitKamikazeAICapabilityUCMutSetEffect(IKamikazeAICapabilityUCMutSetEffect effect) { effect.visitIKamikazeAICapabilityUCMutSetEffect(this); }
    public void visitKamikazeAICapabilityUCMutSetCreateEffect(KamikazeAICapabilityUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectKamikazeAICapabilityUCMutSetCreateWithId(effect.id);
    }
    public void visitKamikazeAICapabilityUCMutSetDeleteEffect(KamikazeAICapabilityUCMutSetDeleteEffect effect) {
      root.EffectKamikazeAICapabilityUCMutSetDelete(effect.id);
    }
    public void visitKamikazeAICapabilityUCMutSetAddEffect(KamikazeAICapabilityUCMutSetAddEffect effect) {
     root.EffectKamikazeAICapabilityUCMutSetAdd(effect.id, effect.element);
 }
    public void visitKamikazeAICapabilityUCMutSetRemoveEffect(KamikazeAICapabilityUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectKamikazeAICapabilityUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitGuardAICapabilityUCMutSetEffect(IGuardAICapabilityUCMutSetEffect effect) { effect.visitIGuardAICapabilityUCMutSetEffect(this); }
    public void visitGuardAICapabilityUCMutSetCreateEffect(GuardAICapabilityUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectGuardAICapabilityUCMutSetCreateWithId(effect.id);
    }
    public void visitGuardAICapabilityUCMutSetDeleteEffect(GuardAICapabilityUCMutSetDeleteEffect effect) {
      root.EffectGuardAICapabilityUCMutSetDelete(effect.id);
    }
    public void visitGuardAICapabilityUCMutSetAddEffect(GuardAICapabilityUCMutSetAddEffect effect) {
     root.EffectGuardAICapabilityUCMutSetAdd(effect.id, effect.element);
 }
    public void visitGuardAICapabilityUCMutSetRemoveEffect(GuardAICapabilityUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectGuardAICapabilityUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitTimeCloneAICapabilityUCMutSetEffect(ITimeCloneAICapabilityUCMutSetEffect effect) { effect.visitITimeCloneAICapabilityUCMutSetEffect(this); }
    public void visitTimeCloneAICapabilityUCMutSetCreateEffect(TimeCloneAICapabilityUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectTimeCloneAICapabilityUCMutSetCreateWithId(effect.id);
    }
    public void visitTimeCloneAICapabilityUCMutSetDeleteEffect(TimeCloneAICapabilityUCMutSetDeleteEffect effect) {
      root.EffectTimeCloneAICapabilityUCMutSetDelete(effect.id);
    }
    public void visitTimeCloneAICapabilityUCMutSetAddEffect(TimeCloneAICapabilityUCMutSetAddEffect effect) {
     root.EffectTimeCloneAICapabilityUCMutSetAdd(effect.id, effect.element);
 }
    public void visitTimeCloneAICapabilityUCMutSetRemoveEffect(TimeCloneAICapabilityUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectTimeCloneAICapabilityUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitDoomedUCMutSetEffect(IDoomedUCMutSetEffect effect) { effect.visitIDoomedUCMutSetEffect(this); }
    public void visitDoomedUCMutSetCreateEffect(DoomedUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectDoomedUCMutSetCreateWithId(effect.id);
    }
    public void visitDoomedUCMutSetDeleteEffect(DoomedUCMutSetDeleteEffect effect) {
      root.EffectDoomedUCMutSetDelete(effect.id);
    }
    public void visitDoomedUCMutSetAddEffect(DoomedUCMutSetAddEffect effect) {
     root.EffectDoomedUCMutSetAdd(effect.id, effect.element);
 }
    public void visitDoomedUCMutSetRemoveEffect(DoomedUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectDoomedUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitMiredUCMutSetEffect(IMiredUCMutSetEffect effect) { effect.visitIMiredUCMutSetEffect(this); }
    public void visitMiredUCMutSetCreateEffect(MiredUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectMiredUCMutSetCreateWithId(effect.id);
    }
    public void visitMiredUCMutSetDeleteEffect(MiredUCMutSetDeleteEffect effect) {
      root.EffectMiredUCMutSetDelete(effect.id);
    }
    public void visitMiredUCMutSetAddEffect(MiredUCMutSetAddEffect effect) {
     root.EffectMiredUCMutSetAdd(effect.id, effect.element);
 }
    public void visitMiredUCMutSetRemoveEffect(MiredUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectMiredUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitAttackAICapabilityUCMutSetEffect(IAttackAICapabilityUCMutSetEffect effect) { effect.visitIAttackAICapabilityUCMutSetEffect(this); }
    public void visitAttackAICapabilityUCMutSetCreateEffect(AttackAICapabilityUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectAttackAICapabilityUCMutSetCreateWithId(effect.id);
    }
    public void visitAttackAICapabilityUCMutSetDeleteEffect(AttackAICapabilityUCMutSetDeleteEffect effect) {
      root.EffectAttackAICapabilityUCMutSetDelete(effect.id);
    }
    public void visitAttackAICapabilityUCMutSetAddEffect(AttackAICapabilityUCMutSetAddEffect effect) {
     root.EffectAttackAICapabilityUCMutSetAdd(effect.id, effect.element);
 }
    public void visitAttackAICapabilityUCMutSetRemoveEffect(AttackAICapabilityUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectAttackAICapabilityUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitCounteringUCMutSetEffect(ICounteringUCMutSetEffect effect) { effect.visitICounteringUCMutSetEffect(this); }
    public void visitCounteringUCMutSetCreateEffect(CounteringUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectCounteringUCMutSetCreateWithId(effect.id);
    }
    public void visitCounteringUCMutSetDeleteEffect(CounteringUCMutSetDeleteEffect effect) {
      root.EffectCounteringUCMutSetDelete(effect.id);
    }
    public void visitCounteringUCMutSetAddEffect(CounteringUCMutSetAddEffect effect) {
     root.EffectCounteringUCMutSetAdd(effect.id, effect.element);
 }
    public void visitCounteringUCMutSetRemoveEffect(CounteringUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectCounteringUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitLightningChargedUCMutSetEffect(ILightningChargedUCMutSetEffect effect) { effect.visitILightningChargedUCMutSetEffect(this); }
    public void visitLightningChargedUCMutSetCreateEffect(LightningChargedUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectLightningChargedUCMutSetCreateWithId(effect.id);
    }
    public void visitLightningChargedUCMutSetDeleteEffect(LightningChargedUCMutSetDeleteEffect effect) {
      root.EffectLightningChargedUCMutSetDelete(effect.id);
    }
    public void visitLightningChargedUCMutSetAddEffect(LightningChargedUCMutSetAddEffect effect) {
     root.EffectLightningChargedUCMutSetAdd(effect.id, effect.element);
 }
    public void visitLightningChargedUCMutSetRemoveEffect(LightningChargedUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectLightningChargedUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitInvincibilityUCMutSetEffect(IInvincibilityUCMutSetEffect effect) { effect.visitIInvincibilityUCMutSetEffect(this); }
    public void visitInvincibilityUCMutSetCreateEffect(InvincibilityUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectInvincibilityUCMutSetCreateWithId(effect.id);
    }
    public void visitInvincibilityUCMutSetDeleteEffect(InvincibilityUCMutSetDeleteEffect effect) {
      root.EffectInvincibilityUCMutSetDelete(effect.id);
    }
    public void visitInvincibilityUCMutSetAddEffect(InvincibilityUCMutSetAddEffect effect) {
     root.EffectInvincibilityUCMutSetAdd(effect.id, effect.element);
 }
    public void visitInvincibilityUCMutSetRemoveEffect(InvincibilityUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectInvincibilityUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitDefyingUCMutSetEffect(IDefyingUCMutSetEffect effect) { effect.visitIDefyingUCMutSetEffect(this); }
    public void visitDefyingUCMutSetCreateEffect(DefyingUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectDefyingUCMutSetCreateWithId(effect.id);
    }
    public void visitDefyingUCMutSetDeleteEffect(DefyingUCMutSetDeleteEffect effect) {
      root.EffectDefyingUCMutSetDelete(effect.id);
    }
    public void visitDefyingUCMutSetAddEffect(DefyingUCMutSetAddEffect effect) {
     root.EffectDefyingUCMutSetAdd(effect.id, effect.element);
 }
    public void visitDefyingUCMutSetRemoveEffect(DefyingUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectDefyingUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitBideAICapabilityUCMutSetEffect(IBideAICapabilityUCMutSetEffect effect) { effect.visitIBideAICapabilityUCMutSetEffect(this); }
    public void visitBideAICapabilityUCMutSetCreateEffect(BideAICapabilityUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectBideAICapabilityUCMutSetCreateWithId(effect.id);
    }
    public void visitBideAICapabilityUCMutSetDeleteEffect(BideAICapabilityUCMutSetDeleteEffect effect) {
      root.EffectBideAICapabilityUCMutSetDelete(effect.id);
    }
    public void visitBideAICapabilityUCMutSetAddEffect(BideAICapabilityUCMutSetAddEffect effect) {
     root.EffectBideAICapabilityUCMutSetAdd(effect.id, effect.element);
 }
    public void visitBideAICapabilityUCMutSetRemoveEffect(BideAICapabilityUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectBideAICapabilityUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitBaseSightRangeUCMutSetEffect(IBaseSightRangeUCMutSetEffect effect) { effect.visitIBaseSightRangeUCMutSetEffect(this); }
    public void visitBaseSightRangeUCMutSetCreateEffect(BaseSightRangeUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectBaseSightRangeUCMutSetCreateWithId(effect.id);
    }
    public void visitBaseSightRangeUCMutSetDeleteEffect(BaseSightRangeUCMutSetDeleteEffect effect) {
      root.EffectBaseSightRangeUCMutSetDelete(effect.id);
    }
    public void visitBaseSightRangeUCMutSetAddEffect(BaseSightRangeUCMutSetAddEffect effect) {
     root.EffectBaseSightRangeUCMutSetAdd(effect.id, effect.element);
 }
    public void visitBaseSightRangeUCMutSetRemoveEffect(BaseSightRangeUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectBaseSightRangeUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitBaseMovementTimeUCMutSetEffect(IBaseMovementTimeUCMutSetEffect effect) { effect.visitIBaseMovementTimeUCMutSetEffect(this); }
    public void visitBaseMovementTimeUCMutSetCreateEffect(BaseMovementTimeUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectBaseMovementTimeUCMutSetCreateWithId(effect.id);
    }
    public void visitBaseMovementTimeUCMutSetDeleteEffect(BaseMovementTimeUCMutSetDeleteEffect effect) {
      root.EffectBaseMovementTimeUCMutSetDelete(effect.id);
    }
    public void visitBaseMovementTimeUCMutSetAddEffect(BaseMovementTimeUCMutSetAddEffect effect) {
     root.EffectBaseMovementTimeUCMutSetAdd(effect.id, effect.element);
 }
    public void visitBaseMovementTimeUCMutSetRemoveEffect(BaseMovementTimeUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectBaseMovementTimeUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitBaseCombatTimeUCMutSetEffect(IBaseCombatTimeUCMutSetEffect effect) { effect.visitIBaseCombatTimeUCMutSetEffect(this); }
    public void visitBaseCombatTimeUCMutSetCreateEffect(BaseCombatTimeUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectBaseCombatTimeUCMutSetCreateWithId(effect.id);
    }
    public void visitBaseCombatTimeUCMutSetDeleteEffect(BaseCombatTimeUCMutSetDeleteEffect effect) {
      root.EffectBaseCombatTimeUCMutSetDelete(effect.id);
    }
    public void visitBaseCombatTimeUCMutSetAddEffect(BaseCombatTimeUCMutSetAddEffect effect) {
     root.EffectBaseCombatTimeUCMutSetAdd(effect.id, effect.element);
 }
    public void visitBaseCombatTimeUCMutSetRemoveEffect(BaseCombatTimeUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectBaseCombatTimeUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitManaPotionMutSetEffect(IManaPotionMutSetEffect effect) { effect.visitIManaPotionMutSetEffect(this); }
    public void visitManaPotionMutSetCreateEffect(ManaPotionMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectManaPotionMutSetCreateWithId(effect.id);
    }
    public void visitManaPotionMutSetDeleteEffect(ManaPotionMutSetDeleteEffect effect) {
      root.EffectManaPotionMutSetDelete(effect.id);
    }
    public void visitManaPotionMutSetAddEffect(ManaPotionMutSetAddEffect effect) {
     root.EffectManaPotionMutSetAdd(effect.id, effect.element);
 }
    public void visitManaPotionMutSetRemoveEffect(ManaPotionMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectManaPotionMutSetRemove(effect.id, effect.element);
    }
       
    public void visitHealthPotionMutSetEffect(IHealthPotionMutSetEffect effect) { effect.visitIHealthPotionMutSetEffect(this); }
    public void visitHealthPotionMutSetCreateEffect(HealthPotionMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectHealthPotionMutSetCreateWithId(effect.id);
    }
    public void visitHealthPotionMutSetDeleteEffect(HealthPotionMutSetDeleteEffect effect) {
      root.EffectHealthPotionMutSetDelete(effect.id);
    }
    public void visitHealthPotionMutSetAddEffect(HealthPotionMutSetAddEffect effect) {
     root.EffectHealthPotionMutSetAdd(effect.id, effect.element);
 }
    public void visitHealthPotionMutSetRemoveEffect(HealthPotionMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectHealthPotionMutSetRemove(effect.id, effect.element);
    }
       
    public void visitSpeedRingMutSetEffect(ISpeedRingMutSetEffect effect) { effect.visitISpeedRingMutSetEffect(this); }
    public void visitSpeedRingMutSetCreateEffect(SpeedRingMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectSpeedRingMutSetCreateWithId(effect.id);
    }
    public void visitSpeedRingMutSetDeleteEffect(SpeedRingMutSetDeleteEffect effect) {
      root.EffectSpeedRingMutSetDelete(effect.id);
    }
    public void visitSpeedRingMutSetAddEffect(SpeedRingMutSetAddEffect effect) {
     root.EffectSpeedRingMutSetAdd(effect.id, effect.element);
 }
    public void visitSpeedRingMutSetRemoveEffect(SpeedRingMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectSpeedRingMutSetRemove(effect.id, effect.element);
    }
       
    public void visitGlaiveMutSetEffect(IGlaiveMutSetEffect effect) { effect.visitIGlaiveMutSetEffect(this); }
    public void visitGlaiveMutSetCreateEffect(GlaiveMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectGlaiveMutSetCreateWithId(effect.id);
    }
    public void visitGlaiveMutSetDeleteEffect(GlaiveMutSetDeleteEffect effect) {
      root.EffectGlaiveMutSetDelete(effect.id);
    }
    public void visitGlaiveMutSetAddEffect(GlaiveMutSetAddEffect effect) {
     root.EffectGlaiveMutSetAdd(effect.id, effect.element);
 }
    public void visitGlaiveMutSetRemoveEffect(GlaiveMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectGlaiveMutSetRemove(effect.id, effect.element);
    }
       
    public void visitSlowRodMutSetEffect(ISlowRodMutSetEffect effect) { effect.visitISlowRodMutSetEffect(this); }
    public void visitSlowRodMutSetCreateEffect(SlowRodMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectSlowRodMutSetCreateWithId(effect.id);
    }
    public void visitSlowRodMutSetDeleteEffect(SlowRodMutSetDeleteEffect effect) {
      root.EffectSlowRodMutSetDelete(effect.id);
    }
    public void visitSlowRodMutSetAddEffect(SlowRodMutSetAddEffect effect) {
     root.EffectSlowRodMutSetAdd(effect.id, effect.element);
 }
    public void visitSlowRodMutSetRemoveEffect(SlowRodMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectSlowRodMutSetRemove(effect.id, effect.element);
    }
       
    public void visitBlastRodMutSetEffect(IBlastRodMutSetEffect effect) { effect.visitIBlastRodMutSetEffect(this); }
    public void visitBlastRodMutSetCreateEffect(BlastRodMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectBlastRodMutSetCreateWithId(effect.id);
    }
    public void visitBlastRodMutSetDeleteEffect(BlastRodMutSetDeleteEffect effect) {
      root.EffectBlastRodMutSetDelete(effect.id);
    }
    public void visitBlastRodMutSetAddEffect(BlastRodMutSetAddEffect effect) {
     root.EffectBlastRodMutSetAdd(effect.id, effect.element);
 }
    public void visitBlastRodMutSetRemoveEffect(BlastRodMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectBlastRodMutSetRemove(effect.id, effect.element);
    }
       
    public void visitArmorMutSetEffect(IArmorMutSetEffect effect) { effect.visitIArmorMutSetEffect(this); }
    public void visitArmorMutSetCreateEffect(ArmorMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectArmorMutSetCreateWithId(effect.id);
    }
    public void visitArmorMutSetDeleteEffect(ArmorMutSetDeleteEffect effect) {
      root.EffectArmorMutSetDelete(effect.id);
    }
    public void visitArmorMutSetAddEffect(ArmorMutSetAddEffect effect) {
     root.EffectArmorMutSetAdd(effect.id, effect.element);
 }
    public void visitArmorMutSetRemoveEffect(ArmorMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectArmorMutSetRemove(effect.id, effect.element);
    }
       
    public void visitSorcerousUCMutSetEffect(ISorcerousUCMutSetEffect effect) { effect.visitISorcerousUCMutSetEffect(this); }
    public void visitSorcerousUCMutSetCreateEffect(SorcerousUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectSorcerousUCMutSetCreateWithId(effect.id);
    }
    public void visitSorcerousUCMutSetDeleteEffect(SorcerousUCMutSetDeleteEffect effect) {
      root.EffectSorcerousUCMutSetDelete(effect.id);
    }
    public void visitSorcerousUCMutSetAddEffect(SorcerousUCMutSetAddEffect effect) {
     root.EffectSorcerousUCMutSetAdd(effect.id, effect.element);
 }
    public void visitSorcerousUCMutSetRemoveEffect(SorcerousUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectSorcerousUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitBaseOffenseUCMutSetEffect(IBaseOffenseUCMutSetEffect effect) { effect.visitIBaseOffenseUCMutSetEffect(this); }
    public void visitBaseOffenseUCMutSetCreateEffect(BaseOffenseUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectBaseOffenseUCMutSetCreateWithId(effect.id);
    }
    public void visitBaseOffenseUCMutSetDeleteEffect(BaseOffenseUCMutSetDeleteEffect effect) {
      root.EffectBaseOffenseUCMutSetDelete(effect.id);
    }
    public void visitBaseOffenseUCMutSetAddEffect(BaseOffenseUCMutSetAddEffect effect) {
     root.EffectBaseOffenseUCMutSetAdd(effect.id, effect.element);
 }
    public void visitBaseOffenseUCMutSetRemoveEffect(BaseOffenseUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectBaseOffenseUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitBaseDefenseUCMutSetEffect(IBaseDefenseUCMutSetEffect effect) { effect.visitIBaseDefenseUCMutSetEffect(this); }
    public void visitBaseDefenseUCMutSetCreateEffect(BaseDefenseUCMutSetCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectBaseDefenseUCMutSetCreateWithId(effect.id);
    }
    public void visitBaseDefenseUCMutSetDeleteEffect(BaseDefenseUCMutSetDeleteEffect effect) {
      root.EffectBaseDefenseUCMutSetDelete(effect.id);
    }
    public void visitBaseDefenseUCMutSetAddEffect(BaseDefenseUCMutSetAddEffect effect) {
     root.EffectBaseDefenseUCMutSetAdd(effect.id, effect.element);
 }
    public void visitBaseDefenseUCMutSetRemoveEffect(BaseDefenseUCMutSetRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectBaseDefenseUCMutSetRemove(effect.id, effect.element);
    }
       
    public void visitTerrainTileByLocationMutMapEffect(ITerrainTileByLocationMutMapEffect effect) { effect.visitITerrainTileByLocationMutMapEffect(this); }
    public void visitTerrainTileByLocationMutMapCreateEffect(TerrainTileByLocationMutMapCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectTerrainTileByLocationMutMapCreateWithId(effect.id);
    }
    public void visitTerrainTileByLocationMutMapDeleteEffect(TerrainTileByLocationMutMapDeleteEffect effect) {
      root.EffectTerrainTileByLocationMutMapDelete(effect.id);
    }
    public void visitTerrainTileByLocationMutMapAddEffect(TerrainTileByLocationMutMapAddEffect effect) {
      root.EffectTerrainTileByLocationMutMapAdd(effect.id, effect.key, effect.value);
    }
    public void visitTerrainTileByLocationMutMapRemoveEffect(TerrainTileByLocationMutMapRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectTerrainTileByLocationMutMapRemove(effect.id, effect.key);
    }
     
    public void visitKamikazeTargetTTCStrongByLocationMutMapEffect(IKamikazeTargetTTCStrongByLocationMutMapEffect effect) { effect.visitIKamikazeTargetTTCStrongByLocationMutMapEffect(this); }
    public void visitKamikazeTargetTTCStrongByLocationMutMapCreateEffect(KamikazeTargetTTCStrongByLocationMutMapCreateEffect effect) {
      // For now we're just feeding the remote ID in. Someday we might want to have a map
      // in the applier instead.
      root.EffectKamikazeTargetTTCStrongByLocationMutMapCreateWithId(effect.id);
    }
    public void visitKamikazeTargetTTCStrongByLocationMutMapDeleteEffect(KamikazeTargetTTCStrongByLocationMutMapDeleteEffect effect) {
      root.EffectKamikazeTargetTTCStrongByLocationMutMapDelete(effect.id);
    }
    public void visitKamikazeTargetTTCStrongByLocationMutMapAddEffect(KamikazeTargetTTCStrongByLocationMutMapAddEffect effect) {
      root.EffectKamikazeTargetTTCStrongByLocationMutMapAdd(effect.id, effect.key, effect.value);
    }
    public void visitKamikazeTargetTTCStrongByLocationMutMapRemoveEffect(KamikazeTargetTTCStrongByLocationMutMapRemoveEffect effect) {
      root.CheckUnlocked();
      root.EffectKamikazeTargetTTCStrongByLocationMutMapRemove(effect.id, effect.key);
    }
     
  public void Apply(IEffect effect) {
    effect.visitIEffect(this);
  }
}
         
}
