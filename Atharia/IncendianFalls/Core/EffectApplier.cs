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
    var instance = root.EffectRandCreate(
  effect.incarnation.rand    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
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
    var instance = root.EffectHoldPositionImpulseCreate(
  effect.incarnation.weight,
  effect.incarnation.duration    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitHoldPositionImpulseDeleteEffect(HoldPositionImpulseDeleteEffect effect) {
    root.EffectHoldPositionImpulseDelete(effect.id);
  }

     
public void visitWanderAICapabilityUCEffect(IWanderAICapabilityUCEffect effect) { effect.visitIWanderAICapabilityUCEffect(this); }
  public void visitWanderAICapabilityUCCreateEffect(WanderAICapabilityUCCreateEffect effect) {
    var instance = root.EffectWanderAICapabilityUCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitWanderAICapabilityUCDeleteEffect(WanderAICapabilityUCDeleteEffect effect) {
    root.EffectWanderAICapabilityUCDelete(effect.id);
  }

     
public void visitTutorialDefyCounterUCEffect(ITutorialDefyCounterUCEffect effect) { effect.visitITutorialDefyCounterUCEffect(this); }
  public void visitTutorialDefyCounterUCCreateEffect(TutorialDefyCounterUCCreateEffect effect) {
    var instance = root.EffectTutorialDefyCounterUCCreate(
  effect.incarnation.numDefiesRemaining,
  effect.incarnation.onChangeTriggerName    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
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
    var instance = root.EffectUnitCreate(
  effect.incarnation.evvent,
  effect.incarnation.lifeEndTime,
  effect.incarnation.location,
  effect.incarnation.classId,
  effect.incarnation.nextActionTime,
  effect.incarnation.hp,
  effect.incarnation.maxHp,
  root.GetIUnitComponentMutBunch(effect.incarnation.components),
  effect.incarnation.good    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
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
    var instance = root.EffectIUnitComponentMutBunchCreate(
  root.GetTutorialDefyCounterUCMutSet(effect.incarnation.membersTutorialDefyCounterUCMutSet),
  root.GetLightningChargingUCMutSet(effect.incarnation.membersLightningChargingUCMutSet),
  root.GetWanderAICapabilityUCMutSet(effect.incarnation.membersWanderAICapabilityUCMutSet),
  root.GetTemporaryCloneAICapabilityUCMutSet(effect.incarnation.membersTemporaryCloneAICapabilityUCMutSet),
  root.GetSummonAICapabilityUCMutSet(effect.incarnation.membersSummonAICapabilityUCMutSet),
  root.GetKamikazeAICapabilityUCMutSet(effect.incarnation.membersKamikazeAICapabilityUCMutSet),
  root.GetGuardAICapabilityUCMutSet(effect.incarnation.membersGuardAICapabilityUCMutSet),
  root.GetTimeCloneAICapabilityUCMutSet(effect.incarnation.membersTimeCloneAICapabilityUCMutSet),
  root.GetDoomedUCMutSet(effect.incarnation.membersDoomedUCMutSet),
  root.GetMiredUCMutSet(effect.incarnation.membersMiredUCMutSet),
  root.GetAttackAICapabilityUCMutSet(effect.incarnation.membersAttackAICapabilityUCMutSet),
  root.GetCounteringUCMutSet(effect.incarnation.membersCounteringUCMutSet),
  root.GetLightningChargedUCMutSet(effect.incarnation.membersLightningChargedUCMutSet),
  root.GetInvincibilityUCMutSet(effect.incarnation.membersInvincibilityUCMutSet),
  root.GetDefyingUCMutSet(effect.incarnation.membersDefyingUCMutSet),
  root.GetBideAICapabilityUCMutSet(effect.incarnation.membersBideAICapabilityUCMutSet),
  root.GetBaseSightRangeUCMutSet(effect.incarnation.membersBaseSightRangeUCMutSet),
  root.GetBaseMovementTimeUCMutSet(effect.incarnation.membersBaseMovementTimeUCMutSet),
  root.GetBaseCombatTimeUCMutSet(effect.incarnation.membersBaseCombatTimeUCMutSet),
  root.GetManaPotionMutSet(effect.incarnation.membersManaPotionMutSet),
  root.GetHealthPotionMutSet(effect.incarnation.membersHealthPotionMutSet),
  root.GetSpeedRingMutSet(effect.incarnation.membersSpeedRingMutSet),
  root.GetGlaiveMutSet(effect.incarnation.membersGlaiveMutSet),
  root.GetSlowRodMutSet(effect.incarnation.membersSlowRodMutSet),
  root.GetBlastRodMutSet(effect.incarnation.membersBlastRodMutSet),
  root.GetArmorMutSet(effect.incarnation.membersArmorMutSet),
  root.GetSorcerousUCMutSet(effect.incarnation.membersSorcerousUCMutSet),
  root.GetBaseOffenseUCMutSet(effect.incarnation.membersBaseOffenseUCMutSet),
  root.GetBaseDefenseUCMutSet(effect.incarnation.membersBaseDefenseUCMutSet)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitIUnitComponentMutBunchDeleteEffect(IUnitComponentMutBunchDeleteEffect effect) {
    root.EffectIUnitComponentMutBunchDelete(effect.id);
  }

     
public void visitLightningChargedUCEffect(ILightningChargedUCEffect effect) { effect.visitILightningChargedUCEffect(this); }
  public void visitLightningChargedUCCreateEffect(LightningChargedUCCreateEffect effect) {
    var instance = root.EffectLightningChargedUCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitLightningChargedUCDeleteEffect(LightningChargedUCDeleteEffect effect) {
    root.EffectLightningChargedUCDelete(effect.id);
  }

     
public void visitLightningChargingUCEffect(ILightningChargingUCEffect effect) { effect.visitILightningChargingUCEffect(this); }
  public void visitLightningChargingUCCreateEffect(LightningChargingUCCreateEffect effect) {
    var instance = root.EffectLightningChargingUCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitLightningChargingUCDeleteEffect(LightningChargingUCDeleteEffect effect) {
    root.EffectLightningChargingUCDelete(effect.id);
  }

     
public void visitDoomedUCEffect(IDoomedUCEffect effect) { effect.visitIDoomedUCEffect(this); }
  public void visitDoomedUCCreateEffect(DoomedUCCreateEffect effect) {
    var instance = root.EffectDoomedUCCreate(
  effect.incarnation.deathTime    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitDoomedUCDeleteEffect(DoomedUCDeleteEffect effect) {
    root.EffectDoomedUCDelete(effect.id);
  }

     
public void visitTemporaryCloneImpulseEffect(ITemporaryCloneImpulseEffect effect) { effect.visitITemporaryCloneImpulseEffect(this); }
  public void visitTemporaryCloneImpulseCreateEffect(TemporaryCloneImpulseCreateEffect effect) {
    var instance = root.EffectTemporaryCloneImpulseCreate(
  effect.incarnation.weight,
  effect.incarnation.blueprintName,
  effect.incarnation.location,
  effect.incarnation.hp    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitTemporaryCloneImpulseDeleteEffect(TemporaryCloneImpulseDeleteEffect effect) {
    root.EffectTemporaryCloneImpulseDelete(effect.id);
  }

     
public void visitTemporaryCloneAICapabilityUCEffect(ITemporaryCloneAICapabilityUCEffect effect) { effect.visitITemporaryCloneAICapabilityUCEffect(this); }
  public void visitTemporaryCloneAICapabilityUCCreateEffect(TemporaryCloneAICapabilityUCCreateEffect effect) {
    var instance = root.EffectTemporaryCloneAICapabilityUCCreate(
  effect.incarnation.blueprintName,
  effect.incarnation.charges    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
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
    var instance = root.EffectSummonImpulseCreate(
  effect.incarnation.weight,
  effect.incarnation.blueprintName,
  effect.incarnation.location    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitSummonImpulseDeleteEffect(SummonImpulseDeleteEffect effect) {
    root.EffectSummonImpulseDelete(effect.id);
  }

     
public void visitSummonAICapabilityUCEffect(ISummonAICapabilityUCEffect effect) { effect.visitISummonAICapabilityUCEffect(this); }
  public void visitSummonAICapabilityUCCreateEffect(SummonAICapabilityUCCreateEffect effect) {
    var instance = root.EffectSummonAICapabilityUCCreate(
  effect.incarnation.blueprintName,
  effect.incarnation.charges    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
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
    var instance = root.EffectSorcerousUCCreate(
  effect.incarnation.mp,
  effect.incarnation.maxMp    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
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
    var instance = root.EffectBaseOffenseUCCreate(
  effect.incarnation.outgoingDamageAddConstant,
  effect.incarnation.outgoingDamageMultiplierPercent    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitBaseOffenseUCDeleteEffect(BaseOffenseUCDeleteEffect effect) {
    root.EffectBaseOffenseUCDelete(effect.id);
  }

     
public void visitBaseSightRangeUCEffect(IBaseSightRangeUCEffect effect) { effect.visitIBaseSightRangeUCEffect(this); }
  public void visitBaseSightRangeUCCreateEffect(BaseSightRangeUCCreateEffect effect) {
    var instance = root.EffectBaseSightRangeUCCreate(
  effect.incarnation.sightRangeAddConstant,
  effect.incarnation.sightRangeMultiplierPercent    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitBaseSightRangeUCDeleteEffect(BaseSightRangeUCDeleteEffect effect) {
    root.EffectBaseSightRangeUCDelete(effect.id);
  }

     
public void visitBaseMovementTimeUCEffect(IBaseMovementTimeUCEffect effect) { effect.visitIBaseMovementTimeUCEffect(this); }
  public void visitBaseMovementTimeUCCreateEffect(BaseMovementTimeUCCreateEffect effect) {
    var instance = root.EffectBaseMovementTimeUCCreate(
  effect.incarnation.movementTimeAddConstant,
  effect.incarnation.movementTimeMultiplierPercent    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitBaseMovementTimeUCDeleteEffect(BaseMovementTimeUCDeleteEffect effect) {
    root.EffectBaseMovementTimeUCDelete(effect.id);
  }

     
public void visitBaseDefenseUCEffect(IBaseDefenseUCEffect effect) { effect.visitIBaseDefenseUCEffect(this); }
  public void visitBaseDefenseUCCreateEffect(BaseDefenseUCCreateEffect effect) {
    var instance = root.EffectBaseDefenseUCCreate(
  effect.incarnation.incomingDamageAddConstant,
  effect.incarnation.incomingDamageMultiplierPercent    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitBaseDefenseUCDeleteEffect(BaseDefenseUCDeleteEffect effect) {
    root.EffectBaseDefenseUCDelete(effect.id);
  }

     
public void visitBaseCombatTimeUCEffect(IBaseCombatTimeUCEffect effect) { effect.visitIBaseCombatTimeUCEffect(this); }
  public void visitBaseCombatTimeUCCreateEffect(BaseCombatTimeUCCreateEffect effect) {
    var instance = root.EffectBaseCombatTimeUCCreate(
  effect.incarnation.combatTimeAddConstant,
  effect.incarnation.combatTimeMultiplierPercent    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitBaseCombatTimeUCDeleteEffect(BaseCombatTimeUCDeleteEffect effect) {
    root.EffectBaseCombatTimeUCDelete(effect.id);
  }

     
public void visitMiredUCEffect(IMiredUCEffect effect) { effect.visitIMiredUCEffect(this); }
  public void visitMiredUCCreateEffect(MiredUCCreateEffect effect) {
    var instance = root.EffectMiredUCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitMiredUCDeleteEffect(MiredUCDeleteEffect effect) {
    root.EffectMiredUCDelete(effect.id);
  }

     
public void visitMireImpulseEffect(IMireImpulseEffect effect) { effect.visitIMireImpulseEffect(this); }
  public void visitMireImpulseCreateEffect(MireImpulseCreateEffect effect) {
    var instance = root.EffectMireImpulseCreate(
  effect.incarnation.weight,
  root.GetUnit(effect.incarnation.targetUnit)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitMireImpulseDeleteEffect(MireImpulseDeleteEffect effect) {
    root.EffectMireImpulseDelete(effect.id);
  }

     
public void visitEvaporateImpulseEffect(IEvaporateImpulseEffect effect) { effect.visitIEvaporateImpulseEffect(this); }
  public void visitEvaporateImpulseCreateEffect(EvaporateImpulseCreateEffect effect) {
    var instance = root.EffectEvaporateImpulseCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitEvaporateImpulseDeleteEffect(EvaporateImpulseDeleteEffect effect) {
    root.EffectEvaporateImpulseDelete(effect.id);
  }

     
public void visitTimeCloneAICapabilityUCEffect(ITimeCloneAICapabilityUCEffect effect) { effect.visitITimeCloneAICapabilityUCEffect(this); }
  public void visitTimeCloneAICapabilityUCCreateEffect(TimeCloneAICapabilityUCCreateEffect effect) {
    var instance = root.EffectTimeCloneAICapabilityUCCreate(
  root.GetIRequestMutListOrNull(effect.incarnation.script)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitTimeCloneAICapabilityUCDeleteEffect(TimeCloneAICapabilityUCDeleteEffect effect) {
    root.EffectTimeCloneAICapabilityUCDelete(effect.id);
  }

     
public void visitMoveImpulseEffect(IMoveImpulseEffect effect) { effect.visitIMoveImpulseEffect(this); }
  public void visitMoveImpulseCreateEffect(MoveImpulseCreateEffect effect) {
    var instance = root.EffectMoveImpulseCreate(
  effect.incarnation.weight,
  effect.incarnation.stepLocation    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitMoveImpulseDeleteEffect(MoveImpulseDeleteEffect effect) {
    root.EffectMoveImpulseDelete(effect.id);
  }

     
public void visitKamikazeTargetTTCEffect(IKamikazeTargetTTCEffect effect) { effect.visitIKamikazeTargetTTCEffect(this); }
  public void visitKamikazeTargetTTCCreateEffect(KamikazeTargetTTCCreateEffect effect) {
    var instance = root.EffectKamikazeTargetTTCCreate(
  root.GetKamikazeAICapabilityUC(effect.incarnation.capability)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitKamikazeTargetTTCDeleteEffect(KamikazeTargetTTCDeleteEffect effect) {
    root.EffectKamikazeTargetTTCDelete(effect.id);
  }

     
public void visitKamikazeJumpImpulseEffect(IKamikazeJumpImpulseEffect effect) { effect.visitIKamikazeJumpImpulseEffect(this); }
  public void visitKamikazeJumpImpulseCreateEffect(KamikazeJumpImpulseCreateEffect effect) {
    var instance = root.EffectKamikazeJumpImpulseCreate(
  effect.incarnation.weight,
  root.GetKamikazeAICapabilityUC(effect.incarnation.capability),
  effect.incarnation.jumpTarget    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitKamikazeJumpImpulseDeleteEffect(KamikazeJumpImpulseDeleteEffect effect) {
    root.EffectKamikazeJumpImpulseDelete(effect.id);
  }

     
public void visitKamikazeTargetImpulseEffect(IKamikazeTargetImpulseEffect effect) { effect.visitIKamikazeTargetImpulseEffect(this); }
  public void visitKamikazeTargetImpulseCreateEffect(KamikazeTargetImpulseCreateEffect effect) {
    var instance = root.EffectKamikazeTargetImpulseCreate(
  effect.incarnation.weight,
  root.GetKamikazeAICapabilityUC(effect.incarnation.capability),
  effect.incarnation.targetLocationCenter,
  effect.incarnation.targetLocations    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitKamikazeTargetImpulseDeleteEffect(KamikazeTargetImpulseDeleteEffect effect) {
    root.EffectKamikazeTargetImpulseDelete(effect.id);
  }

     
public void visitKamikazeAICapabilityUCEffect(IKamikazeAICapabilityUCEffect effect) { effect.visitIKamikazeAICapabilityUCEffect(this); }
  public void visitKamikazeAICapabilityUCCreateEffect(KamikazeAICapabilityUCCreateEffect effect) {
    var instance = root.EffectKamikazeAICapabilityUCCreate(
  root.GetKamikazeTargetTTCStrongByLocationMutMap(effect.incarnation.targetByLocation),
  effect.incarnation.targetLocationCenter    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
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
    var instance = root.EffectInvincibilityUCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitInvincibilityUCDeleteEffect(InvincibilityUCDeleteEffect effect) {
    root.EffectInvincibilityUCDelete(effect.id);
  }

     
public void visitGuardAICapabilityUCEffect(IGuardAICapabilityUCEffect effect) { effect.visitIGuardAICapabilityUCEffect(this); }
  public void visitGuardAICapabilityUCCreateEffect(GuardAICapabilityUCCreateEffect effect) {
    var instance = root.EffectGuardAICapabilityUCCreate(
  effect.incarnation.guardCenterLocation,
  effect.incarnation.guardRadius    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitGuardAICapabilityUCDeleteEffect(GuardAICapabilityUCDeleteEffect effect) {
    root.EffectGuardAICapabilityUCDelete(effect.id);
  }

     
public void visitNoImpulseEffect(INoImpulseEffect effect) { effect.visitINoImpulseEffect(this); }
  public void visitNoImpulseCreateEffect(NoImpulseCreateEffect effect) {
    var instance = root.EffectNoImpulseCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitNoImpulseDeleteEffect(NoImpulseDeleteEffect effect) {
    root.EffectNoImpulseDelete(effect.id);
  }

     
public void visitFireImpulseEffect(IFireImpulseEffect effect) { effect.visitIFireImpulseEffect(this); }
  public void visitFireImpulseCreateEffect(FireImpulseCreateEffect effect) {
    var instance = root.EffectFireImpulseCreate(
  effect.incarnation.weight,
  root.GetUnit(effect.incarnation.targetUnit)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitFireImpulseDeleteEffect(FireImpulseDeleteEffect effect) {
    root.EffectFireImpulseDelete(effect.id);
  }

     
public void visitDefyingUCEffect(IDefyingUCEffect effect) { effect.visitIDefyingUCEffect(this); }
  public void visitDefyingUCCreateEffect(DefyingUCCreateEffect effect) {
    var instance = root.EffectDefyingUCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitDefyingUCDeleteEffect(DefyingUCDeleteEffect effect) {
    root.EffectDefyingUCDelete(effect.id);
  }

     
public void visitDefyImpulseEffect(IDefyImpulseEffect effect) { effect.visitIDefyImpulseEffect(this); }
  public void visitDefyImpulseCreateEffect(DefyImpulseCreateEffect effect) {
    var instance = root.EffectDefyImpulseCreate(
  effect.incarnation.weight    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitDefyImpulseDeleteEffect(DefyImpulseDeleteEffect effect) {
    root.EffectDefyImpulseDelete(effect.id);
  }

     
public void visitCounteringUCEffect(ICounteringUCEffect effect) { effect.visitICounteringUCEffect(this); }
  public void visitCounteringUCCreateEffect(CounteringUCCreateEffect effect) {
    var instance = root.EffectCounteringUCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitCounteringUCDeleteEffect(CounteringUCDeleteEffect effect) {
    root.EffectCounteringUCDelete(effect.id);
  }

     
public void visitCounterImpulseEffect(ICounterImpulseEffect effect) { effect.visitICounterImpulseEffect(this); }
  public void visitCounterImpulseCreateEffect(CounterImpulseCreateEffect effect) {
    var instance = root.EffectCounterImpulseCreate(
  effect.incarnation.weight    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitCounterImpulseDeleteEffect(CounterImpulseDeleteEffect effect) {
    root.EffectCounterImpulseDelete(effect.id);
  }

     
public void visitUnleashBideImpulseEffect(IUnleashBideImpulseEffect effect) { effect.visitIUnleashBideImpulseEffect(this); }
  public void visitUnleashBideImpulseCreateEffect(UnleashBideImpulseCreateEffect effect) {
    var instance = root.EffectUnleashBideImpulseCreate(
  effect.incarnation.weight    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitUnleashBideImpulseDeleteEffect(UnleashBideImpulseDeleteEffect effect) {
    root.EffectUnleashBideImpulseDelete(effect.id);
  }

     
public void visitContinueBidingImpulseEffect(IContinueBidingImpulseEffect effect) { effect.visitIContinueBidingImpulseEffect(this); }
  public void visitContinueBidingImpulseCreateEffect(ContinueBidingImpulseCreateEffect effect) {
    var instance = root.EffectContinueBidingImpulseCreate(
  effect.incarnation.weight    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitContinueBidingImpulseDeleteEffect(ContinueBidingImpulseDeleteEffect effect) {
    root.EffectContinueBidingImpulseDelete(effect.id);
  }

     
public void visitStartBidingImpulseEffect(IStartBidingImpulseEffect effect) { effect.visitIStartBidingImpulseEffect(this); }
  public void visitStartBidingImpulseCreateEffect(StartBidingImpulseCreateEffect effect) {
    var instance = root.EffectStartBidingImpulseCreate(
  effect.incarnation.weight    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitStartBidingImpulseDeleteEffect(StartBidingImpulseDeleteEffect effect) {
    root.EffectStartBidingImpulseDelete(effect.id);
  }

     
public void visitBideAICapabilityUCEffect(IBideAICapabilityUCEffect effect) { effect.visitIBideAICapabilityUCEffect(this); }
  public void visitBideAICapabilityUCCreateEffect(BideAICapabilityUCCreateEffect effect) {
    var instance = root.EffectBideAICapabilityUCCreate(
  effect.incarnation.charge    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
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
    var instance = root.EffectAttackImpulseCreate(
  effect.incarnation.weight,
  root.GetUnit(effect.incarnation.targetUnit)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitAttackImpulseDeleteEffect(AttackImpulseDeleteEffect effect) {
    root.EffectAttackImpulseDelete(effect.id);
  }

     
public void visitPursueImpulseEffect(IPursueImpulseEffect effect) { effect.visitIPursueImpulseEffect(this); }
  public void visitPursueImpulseCreateEffect(PursueImpulseCreateEffect effect) {
    var instance = root.EffectPursueImpulseCreate(
  effect.incarnation.weight,
  effect.incarnation.isClearPath    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitPursueImpulseDeleteEffect(PursueImpulseDeleteEffect effect) {
    root.EffectPursueImpulseDelete(effect.id);
  }

     
public void visitKillDirectiveEffect(IKillDirectiveEffect effect) { effect.visitIKillDirectiveEffect(this); }
  public void visitKillDirectiveCreateEffect(KillDirectiveCreateEffect effect) {
    var instance = root.EffectKillDirectiveCreate(
  root.GetUnitOrNull(effect.incarnation.targetUnit),
  root.GetLocationMutList(effect.incarnation.pathToLastSeenLocation)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitKillDirectiveDeleteEffect(KillDirectiveDeleteEffect effect) {
    root.EffectKillDirectiveDelete(effect.id);
  }

     
public void visitAttackAICapabilityUCEffect(IAttackAICapabilityUCEffect effect) { effect.visitIAttackAICapabilityUCEffect(this); }
  public void visitAttackAICapabilityUCCreateEffect(AttackAICapabilityUCCreateEffect effect) {
    var instance = root.EffectAttackAICapabilityUCCreate(
  root.GetKillDirectiveOrNull(effect.incarnation.killDirective)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
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
    var instance = root.EffectWarperTTCCreate(
  effect.incarnation.destinationLocation    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitWarperTTCDeleteEffect(WarperTTCDeleteEffect effect) {
    root.EffectWarperTTCDelete(effect.id);
  }

     
public void visitTimeAnchorTTCEffect(ITimeAnchorTTCEffect effect) { effect.visitITimeAnchorTTCEffect(this); }
  public void visitTimeAnchorTTCCreateEffect(TimeAnchorTTCCreateEffect effect) {
    var instance = root.EffectTimeAnchorTTCCreate(
  effect.incarnation.pastVersion    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitTimeAnchorTTCDeleteEffect(TimeAnchorTTCDeleteEffect effect) {
    root.EffectTimeAnchorTTCDelete(effect.id);
  }

     
public void visitTerrainTileEffect(ITerrainTileEffect effect) { effect.visitITerrainTileEffect(this); }
  public void visitTerrainTileCreateEffect(TerrainTileCreateEffect effect) {
    var instance = root.EffectTerrainTileCreate(
  effect.incarnation.evvent,
  effect.incarnation.elevation,
  root.GetITerrainTileComponentMutBunch(effect.incarnation.components)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
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
    var instance = root.EffectITerrainTileComponentMutBunchCreate(
  root.GetSimplePresenceTriggerTTCMutSet(effect.incarnation.membersSimplePresenceTriggerTTCMutSet),
  root.GetItemTTCMutSet(effect.incarnation.membersItemTTCMutSet),
  root.GetKamikazeTargetTTCMutSet(effect.incarnation.membersKamikazeTargetTTCMutSet),
  root.GetWarperTTCMutSet(effect.incarnation.membersWarperTTCMutSet),
  root.GetTimeAnchorTTCMutSet(effect.incarnation.membersTimeAnchorTTCMutSet),
  root.GetFireBombTTCMutSet(effect.incarnation.membersFireBombTTCMutSet),
  root.GetMarkerTTCMutSet(effect.incarnation.membersMarkerTTCMutSet),
  root.GetLevelLinkTTCMutSet(effect.incarnation.membersLevelLinkTTCMutSet),
  root.GetMudTTCMutSet(effect.incarnation.membersMudTTCMutSet),
  root.GetDirtTTCMutSet(effect.incarnation.membersDirtTTCMutSet),
  root.GetObsidianTTCMutSet(effect.incarnation.membersObsidianTTCMutSet),
  root.GetDownStairsTTCMutSet(effect.incarnation.membersDownStairsTTCMutSet),
  root.GetUpStairsTTCMutSet(effect.incarnation.membersUpStairsTTCMutSet),
  root.GetWallTTCMutSet(effect.incarnation.membersWallTTCMutSet),
  root.GetBloodTTCMutSet(effect.incarnation.membersBloodTTCMutSet),
  root.GetRocksTTCMutSet(effect.incarnation.membersRocksTTCMutSet),
  root.GetTreeTTCMutSet(effect.incarnation.membersTreeTTCMutSet),
  root.GetWaterTTCMutSet(effect.incarnation.membersWaterTTCMutSet),
  root.GetFloorTTCMutSet(effect.incarnation.membersFloorTTCMutSet),
  root.GetCaveWallTTCMutSet(effect.incarnation.membersCaveWallTTCMutSet),
  root.GetCaveTTCMutSet(effect.incarnation.membersCaveTTCMutSet),
  root.GetFallsTTCMutSet(effect.incarnation.membersFallsTTCMutSet),
  root.GetFireTTCMutSet(effect.incarnation.membersFireTTCMutSet),
  root.GetObsidianFloorTTCMutSet(effect.incarnation.membersObsidianFloorTTCMutSet),
  root.GetMagmaTTCMutSet(effect.incarnation.membersMagmaTTCMutSet),
  root.GetCliffTTCMutSet(effect.incarnation.membersCliffTTCMutSet),
  root.GetRavaNestTTCMutSet(effect.incarnation.membersRavaNestTTCMutSet),
  root.GetCliffLandingTTCMutSet(effect.incarnation.membersCliffLandingTTCMutSet),
  root.GetStoneTTCMutSet(effect.incarnation.membersStoneTTCMutSet),
  root.GetGrassTTCMutSet(effect.incarnation.membersGrassTTCMutSet),
  root.GetIncendianFallsLevelLinkerTTCMutSet(effect.incarnation.membersIncendianFallsLevelLinkerTTCMutSet),
  root.GetEmberDeepLevelLinkerTTCMutSet(effect.incarnation.membersEmberDeepLevelLinkerTTCMutSet)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitITerrainTileComponentMutBunchDeleteEffect(ITerrainTileComponentMutBunchDeleteEffect effect) {
    root.EffectITerrainTileComponentMutBunchDelete(effect.id);
  }

     
public void visitTerrainEffect(ITerrainEffect effect) { effect.visitITerrainEffect(this); }
  public void visitTerrainCreateEffect(TerrainCreateEffect effect) {
    var instance = root.EffectTerrainCreate(
  effect.incarnation.pattern,
  effect.incarnation.elevationStepHeight,
  root.GetTerrainTileByLocationMutMap(effect.incarnation.tiles)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
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
    var instance = root.EffectSimplePresenceTriggerTTCCreate(
  effect.incarnation.name    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitSimplePresenceTriggerTTCDeleteEffect(SimplePresenceTriggerTTCDeleteEffect effect) {
    root.EffectSimplePresenceTriggerTTCDelete(effect.id);
  }

     
public void visitFireBombImpulseEffect(IFireBombImpulseEffect effect) { effect.visitIFireBombImpulseEffect(this); }
  public void visitFireBombImpulseCreateEffect(FireBombImpulseCreateEffect effect) {
    var instance = root.EffectFireBombImpulseCreate(
  effect.incarnation.weight,
  effect.incarnation.location    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitFireBombImpulseDeleteEffect(FireBombImpulseDeleteEffect effect) {
    root.EffectFireBombImpulseDelete(effect.id);
  }

     
public void visitFireBombTTCEffect(IFireBombTTCEffect effect) { effect.visitIFireBombTTCEffect(this); }
  public void visitFireBombTTCCreateEffect(FireBombTTCCreateEffect effect) {
    var instance = root.EffectFireBombTTCCreate(
  effect.incarnation.turnsUntilExplosion    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
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
    var instance = root.EffectMarkerTTCCreate(
  effect.incarnation.name    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitMarkerTTCDeleteEffect(MarkerTTCDeleteEffect effect) {
    root.EffectMarkerTTCDelete(effect.id);
  }

     
public void visitLevelLinkTTCEffect(ILevelLinkTTCEffect effect) { effect.visitILevelLinkTTCEffect(this); }
  public void visitLevelLinkTTCCreateEffect(LevelLinkTTCCreateEffect effect) {
    var instance = root.EffectLevelLinkTTCCreate(
  effect.incarnation.destroyThisLevel,
  root.GetLevel(effect.incarnation.destinationLevel),
  effect.incarnation.destinationLevelLocation    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitLevelLinkTTCDeleteEffect(LevelLinkTTCDeleteEffect effect) {
    root.EffectLevelLinkTTCDelete(effect.id);
  }

     
public void visitMudTTCEffect(IMudTTCEffect effect) { effect.visitIMudTTCEffect(this); }
  public void visitMudTTCCreateEffect(MudTTCCreateEffect effect) {
    var instance = root.EffectMudTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitMudTTCDeleteEffect(MudTTCDeleteEffect effect) {
    root.EffectMudTTCDelete(effect.id);
  }

     
public void visitDirtTTCEffect(IDirtTTCEffect effect) { effect.visitIDirtTTCEffect(this); }
  public void visitDirtTTCCreateEffect(DirtTTCCreateEffect effect) {
    var instance = root.EffectDirtTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitDirtTTCDeleteEffect(DirtTTCDeleteEffect effect) {
    root.EffectDirtTTCDelete(effect.id);
  }

     
public void visitObsidianTTCEffect(IObsidianTTCEffect effect) { effect.visitIObsidianTTCEffect(this); }
  public void visitObsidianTTCCreateEffect(ObsidianTTCCreateEffect effect) {
    var instance = root.EffectObsidianTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitObsidianTTCDeleteEffect(ObsidianTTCDeleteEffect effect) {
    root.EffectObsidianTTCDelete(effect.id);
  }

     
public void visitDownStairsTTCEffect(IDownStairsTTCEffect effect) { effect.visitIDownStairsTTCEffect(this); }
  public void visitDownStairsTTCCreateEffect(DownStairsTTCCreateEffect effect) {
    var instance = root.EffectDownStairsTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitDownStairsTTCDeleteEffect(DownStairsTTCDeleteEffect effect) {
    root.EffectDownStairsTTCDelete(effect.id);
  }

     
public void visitUpStairsTTCEffect(IUpStairsTTCEffect effect) { effect.visitIUpStairsTTCEffect(this); }
  public void visitUpStairsTTCCreateEffect(UpStairsTTCCreateEffect effect) {
    var instance = root.EffectUpStairsTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitUpStairsTTCDeleteEffect(UpStairsTTCDeleteEffect effect) {
    root.EffectUpStairsTTCDelete(effect.id);
  }

     
public void visitWallTTCEffect(IWallTTCEffect effect) { effect.visitIWallTTCEffect(this); }
  public void visitWallTTCCreateEffect(WallTTCCreateEffect effect) {
    var instance = root.EffectWallTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitWallTTCDeleteEffect(WallTTCDeleteEffect effect) {
    root.EffectWallTTCDelete(effect.id);
  }

     
public void visitBloodTTCEffect(IBloodTTCEffect effect) { effect.visitIBloodTTCEffect(this); }
  public void visitBloodTTCCreateEffect(BloodTTCCreateEffect effect) {
    var instance = root.EffectBloodTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitBloodTTCDeleteEffect(BloodTTCDeleteEffect effect) {
    root.EffectBloodTTCDelete(effect.id);
  }

     
public void visitRocksTTCEffect(IRocksTTCEffect effect) { effect.visitIRocksTTCEffect(this); }
  public void visitRocksTTCCreateEffect(RocksTTCCreateEffect effect) {
    var instance = root.EffectRocksTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitRocksTTCDeleteEffect(RocksTTCDeleteEffect effect) {
    root.EffectRocksTTCDelete(effect.id);
  }

     
public void visitTreeTTCEffect(ITreeTTCEffect effect) { effect.visitITreeTTCEffect(this); }
  public void visitTreeTTCCreateEffect(TreeTTCCreateEffect effect) {
    var instance = root.EffectTreeTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitTreeTTCDeleteEffect(TreeTTCDeleteEffect effect) {
    root.EffectTreeTTCDelete(effect.id);
  }

     
public void visitWaterTTCEffect(IWaterTTCEffect effect) { effect.visitIWaterTTCEffect(this); }
  public void visitWaterTTCCreateEffect(WaterTTCCreateEffect effect) {
    var instance = root.EffectWaterTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitWaterTTCDeleteEffect(WaterTTCDeleteEffect effect) {
    root.EffectWaterTTCDelete(effect.id);
  }

     
public void visitFloorTTCEffect(IFloorTTCEffect effect) { effect.visitIFloorTTCEffect(this); }
  public void visitFloorTTCCreateEffect(FloorTTCCreateEffect effect) {
    var instance = root.EffectFloorTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitFloorTTCDeleteEffect(FloorTTCDeleteEffect effect) {
    root.EffectFloorTTCDelete(effect.id);
  }

     
public void visitCaveWallTTCEffect(ICaveWallTTCEffect effect) { effect.visitICaveWallTTCEffect(this); }
  public void visitCaveWallTTCCreateEffect(CaveWallTTCCreateEffect effect) {
    var instance = root.EffectCaveWallTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitCaveWallTTCDeleteEffect(CaveWallTTCDeleteEffect effect) {
    root.EffectCaveWallTTCDelete(effect.id);
  }

     
public void visitCaveTTCEffect(ICaveTTCEffect effect) { effect.visitICaveTTCEffect(this); }
  public void visitCaveTTCCreateEffect(CaveTTCCreateEffect effect) {
    var instance = root.EffectCaveTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitCaveTTCDeleteEffect(CaveTTCDeleteEffect effect) {
    root.EffectCaveTTCDelete(effect.id);
  }

     
public void visitFallsTTCEffect(IFallsTTCEffect effect) { effect.visitIFallsTTCEffect(this); }
  public void visitFallsTTCCreateEffect(FallsTTCCreateEffect effect) {
    var instance = root.EffectFallsTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitFallsTTCDeleteEffect(FallsTTCDeleteEffect effect) {
    root.EffectFallsTTCDelete(effect.id);
  }

     
public void visitFireTTCEffect(IFireTTCEffect effect) { effect.visitIFireTTCEffect(this); }
  public void visitFireTTCCreateEffect(FireTTCCreateEffect effect) {
    var instance = root.EffectFireTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitFireTTCDeleteEffect(FireTTCDeleteEffect effect) {
    root.EffectFireTTCDelete(effect.id);
  }

     
public void visitObsidianFloorTTCEffect(IObsidianFloorTTCEffect effect) { effect.visitIObsidianFloorTTCEffect(this); }
  public void visitObsidianFloorTTCCreateEffect(ObsidianFloorTTCCreateEffect effect) {
    var instance = root.EffectObsidianFloorTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitObsidianFloorTTCDeleteEffect(ObsidianFloorTTCDeleteEffect effect) {
    root.EffectObsidianFloorTTCDelete(effect.id);
  }

     
public void visitMagmaTTCEffect(IMagmaTTCEffect effect) { effect.visitIMagmaTTCEffect(this); }
  public void visitMagmaTTCCreateEffect(MagmaTTCCreateEffect effect) {
    var instance = root.EffectMagmaTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitMagmaTTCDeleteEffect(MagmaTTCDeleteEffect effect) {
    root.EffectMagmaTTCDelete(effect.id);
  }

     
public void visitCliffTTCEffect(ICliffTTCEffect effect) { effect.visitICliffTTCEffect(this); }
  public void visitCliffTTCCreateEffect(CliffTTCCreateEffect effect) {
    var instance = root.EffectCliffTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitCliffTTCDeleteEffect(CliffTTCDeleteEffect effect) {
    root.EffectCliffTTCDelete(effect.id);
  }

     
public void visitRavaNestTTCEffect(IRavaNestTTCEffect effect) { effect.visitIRavaNestTTCEffect(this); }
  public void visitRavaNestTTCCreateEffect(RavaNestTTCCreateEffect effect) {
    var instance = root.EffectRavaNestTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitRavaNestTTCDeleteEffect(RavaNestTTCDeleteEffect effect) {
    root.EffectRavaNestTTCDelete(effect.id);
  }

     
public void visitCliffLandingTTCEffect(ICliffLandingTTCEffect effect) { effect.visitICliffLandingTTCEffect(this); }
  public void visitCliffLandingTTCCreateEffect(CliffLandingTTCCreateEffect effect) {
    var instance = root.EffectCliffLandingTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitCliffLandingTTCDeleteEffect(CliffLandingTTCDeleteEffect effect) {
    root.EffectCliffLandingTTCDelete(effect.id);
  }

     
public void visitStoneTTCEffect(IStoneTTCEffect effect) { effect.visitIStoneTTCEffect(this); }
  public void visitStoneTTCCreateEffect(StoneTTCCreateEffect effect) {
    var instance = root.EffectStoneTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitStoneTTCDeleteEffect(StoneTTCDeleteEffect effect) {
    root.EffectStoneTTCDelete(effect.id);
  }

     
public void visitGrassTTCEffect(IGrassTTCEffect effect) { effect.visitIGrassTTCEffect(this); }
  public void visitGrassTTCCreateEffect(GrassTTCCreateEffect effect) {
    var instance = root.EffectGrassTTCCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitGrassTTCDeleteEffect(GrassTTCDeleteEffect effect) {
    root.EffectGrassTTCDelete(effect.id);
  }

     
public void visitLevelEffect(ILevelEffect effect) { effect.visitILevelEffect(this); }
  public void visitLevelCreateEffect(LevelCreateEffect effect) {
    var instance = root.EffectLevelCreate(
  effect.incarnation.cameraAngle,
  root.GetTerrain(effect.incarnation.terrain),
  root.GetUnitMutSet(effect.incarnation.units),
  root.GetILevelControllerOrNull(effect.incarnation.controller),
  effect.incarnation.time    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
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
    var instance = root.EffectSpeedRingCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitSpeedRingDeleteEffect(SpeedRingDeleteEffect effect) {
    root.EffectSpeedRingDelete(effect.id);
  }

     
public void visitManaPotionEffect(IManaPotionEffect effect) { effect.visitIManaPotionEffect(this); }
  public void visitManaPotionCreateEffect(ManaPotionCreateEffect effect) {
    var instance = root.EffectManaPotionCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitManaPotionDeleteEffect(ManaPotionDeleteEffect effect) {
    root.EffectManaPotionDelete(effect.id);
  }

     
public void visitWatEffect(IWatEffect effect) { effect.visitIWatEffect(this); }
  public void visitWatCreateEffect(WatCreateEffect effect) {
    var instance = root.EffectWatCreate(
  root.GetIItemStrongMutBunch(effect.incarnation.items),
  root.GetIImpulseStrongMutBunch(effect.incarnation.impulses),
  root.GetIPostActingUCWeakMutBunch(effect.incarnation.blah),
  root.GetIPreActingUCWeakMutBunch(effect.incarnation.bloop)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitWatDeleteEffect(WatDeleteEffect effect) {
    root.EffectWatDelete(effect.id);
  }

     
public void visitIPreActingUCWeakMutBunchEffect(IIPreActingUCWeakMutBunchEffect effect) { effect.visitIIPreActingUCWeakMutBunchEffect(this); }
  public void visitIPreActingUCWeakMutBunchCreateEffect(IPreActingUCWeakMutBunchCreateEffect effect) {
    var instance = root.EffectIPreActingUCWeakMutBunchCreate(
  root.GetDoomedUCWeakMutSet(effect.incarnation.membersDoomedUCWeakMutSet),
  root.GetMiredUCWeakMutSet(effect.incarnation.membersMiredUCWeakMutSet),
  root.GetInvincibilityUCWeakMutSet(effect.incarnation.membersInvincibilityUCWeakMutSet),
  root.GetDefyingUCWeakMutSet(effect.incarnation.membersDefyingUCWeakMutSet),
  root.GetCounteringUCWeakMutSet(effect.incarnation.membersCounteringUCWeakMutSet),
  root.GetAttackAICapabilityUCWeakMutSet(effect.incarnation.membersAttackAICapabilityUCWeakMutSet)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitIPreActingUCWeakMutBunchDeleteEffect(IPreActingUCWeakMutBunchDeleteEffect effect) {
    root.EffectIPreActingUCWeakMutBunchDelete(effect.id);
  }

     
public void visitIPostActingUCWeakMutBunchEffect(IIPostActingUCWeakMutBunchEffect effect) { effect.visitIIPostActingUCWeakMutBunchEffect(this); }
  public void visitIPostActingUCWeakMutBunchCreateEffect(IPostActingUCWeakMutBunchCreateEffect effect) {
    var instance = root.EffectIPostActingUCWeakMutBunchCreate(
  root.GetLightningChargedUCWeakMutSet(effect.incarnation.membersLightningChargedUCWeakMutSet),
  root.GetTimeCloneAICapabilityUCWeakMutSet(effect.incarnation.membersTimeCloneAICapabilityUCWeakMutSet)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitIPostActingUCWeakMutBunchDeleteEffect(IPostActingUCWeakMutBunchDeleteEffect effect) {
    root.EffectIPostActingUCWeakMutBunchDelete(effect.id);
  }

     
public void visitIImpulseStrongMutBunchEffect(IIImpulseStrongMutBunchEffect effect) { effect.visitIIImpulseStrongMutBunchEffect(this); }
  public void visitIImpulseStrongMutBunchCreateEffect(IImpulseStrongMutBunchCreateEffect effect) {
    var instance = root.EffectIImpulseStrongMutBunchCreate(
  root.GetHoldPositionImpulseStrongMutSet(effect.incarnation.membersHoldPositionImpulseStrongMutSet),
  root.GetTemporaryCloneImpulseStrongMutSet(effect.incarnation.membersTemporaryCloneImpulseStrongMutSet),
  root.GetSummonImpulseStrongMutSet(effect.incarnation.membersSummonImpulseStrongMutSet),
  root.GetMireImpulseStrongMutSet(effect.incarnation.membersMireImpulseStrongMutSet),
  root.GetEvaporateImpulseStrongMutSet(effect.incarnation.membersEvaporateImpulseStrongMutSet),
  root.GetMoveImpulseStrongMutSet(effect.incarnation.membersMoveImpulseStrongMutSet),
  root.GetKamikazeJumpImpulseStrongMutSet(effect.incarnation.membersKamikazeJumpImpulseStrongMutSet),
  root.GetKamikazeTargetImpulseStrongMutSet(effect.incarnation.membersKamikazeTargetImpulseStrongMutSet),
  root.GetNoImpulseStrongMutSet(effect.incarnation.membersNoImpulseStrongMutSet),
  root.GetFireImpulseStrongMutSet(effect.incarnation.membersFireImpulseStrongMutSet),
  root.GetDefyImpulseStrongMutSet(effect.incarnation.membersDefyImpulseStrongMutSet),
  root.GetCounterImpulseStrongMutSet(effect.incarnation.membersCounterImpulseStrongMutSet),
  root.GetUnleashBideImpulseStrongMutSet(effect.incarnation.membersUnleashBideImpulseStrongMutSet),
  root.GetContinueBidingImpulseStrongMutSet(effect.incarnation.membersContinueBidingImpulseStrongMutSet),
  root.GetStartBidingImpulseStrongMutSet(effect.incarnation.membersStartBidingImpulseStrongMutSet),
  root.GetAttackImpulseStrongMutSet(effect.incarnation.membersAttackImpulseStrongMutSet),
  root.GetPursueImpulseStrongMutSet(effect.incarnation.membersPursueImpulseStrongMutSet),
  root.GetFireBombImpulseStrongMutSet(effect.incarnation.membersFireBombImpulseStrongMutSet)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitIImpulseStrongMutBunchDeleteEffect(IImpulseStrongMutBunchDeleteEffect effect) {
    root.EffectIImpulseStrongMutBunchDelete(effect.id);
  }

     
public void visitIItemStrongMutBunchEffect(IIItemStrongMutBunchEffect effect) { effect.visitIIItemStrongMutBunchEffect(this); }
  public void visitIItemStrongMutBunchCreateEffect(IItemStrongMutBunchCreateEffect effect) {
    var instance = root.EffectIItemStrongMutBunchCreate(
  root.GetManaPotionStrongMutSet(effect.incarnation.membersManaPotionStrongMutSet),
  root.GetHealthPotionStrongMutSet(effect.incarnation.membersHealthPotionStrongMutSet),
  root.GetSpeedRingStrongMutSet(effect.incarnation.membersSpeedRingStrongMutSet),
  root.GetGlaiveStrongMutSet(effect.incarnation.membersGlaiveStrongMutSet),
  root.GetSlowRodStrongMutSet(effect.incarnation.membersSlowRodStrongMutSet),
  root.GetBlastRodStrongMutSet(effect.incarnation.membersBlastRodStrongMutSet),
  root.GetArmorStrongMutSet(effect.incarnation.membersArmorStrongMutSet)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitIItemStrongMutBunchDeleteEffect(IItemStrongMutBunchDeleteEffect effect) {
    root.EffectIItemStrongMutBunchDelete(effect.id);
  }

     
public void visitItemTTCEffect(IItemTTCEffect effect) { effect.visitIItemTTCEffect(this); }
  public void visitItemTTCCreateEffect(ItemTTCCreateEffect effect) {
    var instance = root.EffectItemTTCCreate(
  root.GetIItem(effect.incarnation.item)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitItemTTCDeleteEffect(ItemTTCDeleteEffect effect) {
    root.EffectItemTTCDelete(effect.id);
  }

     
public void visitHealthPotionEffect(IHealthPotionEffect effect) { effect.visitIHealthPotionEffect(this); }
  public void visitHealthPotionCreateEffect(HealthPotionCreateEffect effect) {
    var instance = root.EffectHealthPotionCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitHealthPotionDeleteEffect(HealthPotionDeleteEffect effect) {
    root.EffectHealthPotionDelete(effect.id);
  }

     
public void visitGlaiveEffect(IGlaiveEffect effect) { effect.visitIGlaiveEffect(this); }
  public void visitGlaiveCreateEffect(GlaiveCreateEffect effect) {
    var instance = root.EffectGlaiveCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitGlaiveDeleteEffect(GlaiveDeleteEffect effect) {
    root.EffectGlaiveDelete(effect.id);
  }

     
public void visitSlowRodEffect(ISlowRodEffect effect) { effect.visitISlowRodEffect(this); }
  public void visitSlowRodCreateEffect(SlowRodCreateEffect effect) {
    var instance = root.EffectSlowRodCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitSlowRodDeleteEffect(SlowRodDeleteEffect effect) {
    root.EffectSlowRodDelete(effect.id);
  }

     
public void visitBlastRodEffect(IBlastRodEffect effect) { effect.visitIBlastRodEffect(this); }
  public void visitBlastRodCreateEffect(BlastRodCreateEffect effect) {
    var instance = root.EffectBlastRodCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitBlastRodDeleteEffect(BlastRodDeleteEffect effect) {
    root.EffectBlastRodDelete(effect.id);
  }

     
public void visitArmorEffect(IArmorEffect effect) { effect.visitIArmorEffect(this); }
  public void visitArmorCreateEffect(ArmorCreateEffect effect) {
    var instance = root.EffectArmorCreate(
    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitArmorDeleteEffect(ArmorDeleteEffect effect) {
    root.EffectArmorDelete(effect.id);
  }

     
public void visitSquareCaveLevelControllerEffect(ISquareCaveLevelControllerEffect effect) { effect.visitISquareCaveLevelControllerEffect(this); }
  public void visitSquareCaveLevelControllerCreateEffect(SquareCaveLevelControllerCreateEffect effect) {
    var instance = root.EffectSquareCaveLevelControllerCreate(
  root.GetLevel(effect.incarnation.level),
  effect.incarnation.depth    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitSquareCaveLevelControllerDeleteEffect(SquareCaveLevelControllerDeleteEffect effect) {
    root.EffectSquareCaveLevelControllerDelete(effect.id);
  }

     
public void visitRavashrikeLevelControllerEffect(IRavashrikeLevelControllerEffect effect) { effect.visitIRavashrikeLevelControllerEffect(this); }
  public void visitRavashrikeLevelControllerCreateEffect(RavashrikeLevelControllerCreateEffect effect) {
    var instance = root.EffectRavashrikeLevelControllerCreate(
  root.GetLevel(effect.incarnation.level)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitRavashrikeLevelControllerDeleteEffect(RavashrikeLevelControllerDeleteEffect effect) {
    root.EffectRavashrikeLevelControllerDelete(effect.id);
  }

     
public void visitPentagonalCaveLevelControllerEffect(IPentagonalCaveLevelControllerEffect effect) { effect.visitIPentagonalCaveLevelControllerEffect(this); }
  public void visitPentagonalCaveLevelControllerCreateEffect(PentagonalCaveLevelControllerCreateEffect effect) {
    var instance = root.EffectPentagonalCaveLevelControllerCreate(
  root.GetLevel(effect.incarnation.level),
  effect.incarnation.depth    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitPentagonalCaveLevelControllerDeleteEffect(PentagonalCaveLevelControllerDeleteEffect effect) {
    root.EffectPentagonalCaveLevelControllerDelete(effect.id);
  }

     
public void visitIncendianFallsLevelLinkerTTCEffect(IIncendianFallsLevelLinkerTTCEffect effect) { effect.visitIIncendianFallsLevelLinkerTTCEffect(this); }
  public void visitIncendianFallsLevelLinkerTTCCreateEffect(IncendianFallsLevelLinkerTTCCreateEffect effect) {
    var instance = root.EffectIncendianFallsLevelLinkerTTCCreate(
  effect.incarnation.thisLevelDepth    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitIncendianFallsLevelLinkerTTCDeleteEffect(IncendianFallsLevelLinkerTTCDeleteEffect effect) {
    root.EffectIncendianFallsLevelLinkerTTCDelete(effect.id);
  }

     
public void visitCliffLevelControllerEffect(ICliffLevelControllerEffect effect) { effect.visitICliffLevelControllerEffect(this); }
  public void visitCliffLevelControllerCreateEffect(CliffLevelControllerCreateEffect effect) {
    var instance = root.EffectCliffLevelControllerCreate(
  root.GetLevel(effect.incarnation.level),
  effect.incarnation.depth    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitCliffLevelControllerDeleteEffect(CliffLevelControllerDeleteEffect effect) {
    root.EffectCliffLevelControllerDelete(effect.id);
  }

     
public void visitPreGauntletLevelControllerEffect(IPreGauntletLevelControllerEffect effect) { effect.visitIPreGauntletLevelControllerEffect(this); }
  public void visitPreGauntletLevelControllerCreateEffect(PreGauntletLevelControllerCreateEffect effect) {
    var instance = root.EffectPreGauntletLevelControllerCreate(
  root.GetLevel(effect.incarnation.level)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitPreGauntletLevelControllerDeleteEffect(PreGauntletLevelControllerDeleteEffect effect) {
    root.EffectPreGauntletLevelControllerDelete(effect.id);
  }

     
public void visitGauntletLevelControllerEffect(IGauntletLevelControllerEffect effect) { effect.visitIGauntletLevelControllerEffect(this); }
  public void visitGauntletLevelControllerCreateEffect(GauntletLevelControllerCreateEffect effect) {
    var instance = root.EffectGauntletLevelControllerCreate(
  root.GetLevel(effect.incarnation.level)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitGauntletLevelControllerDeleteEffect(GauntletLevelControllerDeleteEffect effect) {
    root.EffectGauntletLevelControllerDelete(effect.id);
  }

     
public void visitCommEffect(ICommEffect effect) { effect.visitICommEffect(this); }
  public void visitCommCreateEffect(CommCreateEffect effect) {
    var instance = root.EffectCommCreate(
  effect.incarnation.template,
  effect.incarnation.actions,
  effect.incarnation.texts    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitCommDeleteEffect(CommDeleteEffect effect) {
    root.EffectCommDelete(effect.id);
  }

     
public void visitGameEffect(IGameEffect effect) { effect.visitIGameEffect(this); }
  public void visitGameCreateEffect(GameCreateEffect effect) {
    var instance = root.EffectGameCreate(
  root.GetRand(effect.incarnation.rand),
  effect.incarnation.squareLevelsOnly,
  root.GetLevelMutSet(effect.incarnation.levels),
  root.GetUnitOrNull(effect.incarnation.player),
  root.GetLevelOrNull(effect.incarnation.level),
  effect.incarnation.time,
  root.GetUnitOrNull(effect.incarnation.actingUnit),
  effect.incarnation.pauseBeforeNextUnit,
  effect.incarnation.actionNum,
  effect.incarnation.instructions,
  effect.incarnation.hideInput,
  effect.incarnation.evvent,
  root.GetCommMutList(effect.incarnation.comms)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
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
    var instance = root.EffectVolcaetusLevelControllerCreate(
  root.GetLevel(effect.incarnation.level)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitVolcaetusLevelControllerDeleteEffect(VolcaetusLevelControllerDeleteEffect effect) {
    root.EffectVolcaetusLevelControllerDelete(effect.id);
  }

     
public void visitTutorial2LevelControllerEffect(ITutorial2LevelControllerEffect effect) { effect.visitITutorial2LevelControllerEffect(this); }
  public void visitTutorial2LevelControllerCreateEffect(Tutorial2LevelControllerCreateEffect effect) {
    var instance = root.EffectTutorial2LevelControllerCreate(
  root.GetLevel(effect.incarnation.level)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitTutorial2LevelControllerDeleteEffect(Tutorial2LevelControllerDeleteEffect effect) {
    root.EffectTutorial2LevelControllerDelete(effect.id);
  }

     
public void visitTutorial1LevelControllerEffect(ITutorial1LevelControllerEffect effect) { effect.visitITutorial1LevelControllerEffect(this); }
  public void visitTutorial1LevelControllerCreateEffect(Tutorial1LevelControllerCreateEffect effect) {
    var instance = root.EffectTutorial1LevelControllerCreate(
  root.GetLevel(effect.incarnation.level)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitTutorial1LevelControllerDeleteEffect(Tutorial1LevelControllerDeleteEffect effect) {
    root.EffectTutorial1LevelControllerDelete(effect.id);
  }

     
public void visitRetreatLevelControllerEffect(IRetreatLevelControllerEffect effect) { effect.visitIRetreatLevelControllerEffect(this); }
  public void visitRetreatLevelControllerCreateEffect(RetreatLevelControllerCreateEffect effect) {
    var instance = root.EffectRetreatLevelControllerCreate(
  root.GetLevel(effect.incarnation.level)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitRetreatLevelControllerDeleteEffect(RetreatLevelControllerDeleteEffect effect) {
    root.EffectRetreatLevelControllerDelete(effect.id);
  }

     
public void visitSotaventoLevelControllerEffect(ISotaventoLevelControllerEffect effect) { effect.visitISotaventoLevelControllerEffect(this); }
  public void visitSotaventoLevelControllerCreateEffect(SotaventoLevelControllerCreateEffect effect) {
    var instance = root.EffectSotaventoLevelControllerCreate(
  root.GetLevel(effect.incarnation.level)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitSotaventoLevelControllerDeleteEffect(SotaventoLevelControllerDeleteEffect effect) {
    root.EffectSotaventoLevelControllerDelete(effect.id);
  }

     
public void visitNestLevelControllerEffect(INestLevelControllerEffect effect) { effect.visitINestLevelControllerEffect(this); }
  public void visitNestLevelControllerCreateEffect(NestLevelControllerCreateEffect effect) {
    var instance = root.EffectNestLevelControllerCreate(
  root.GetLevel(effect.incarnation.level)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitNestLevelControllerDeleteEffect(NestLevelControllerDeleteEffect effect) {
    root.EffectNestLevelControllerDelete(effect.id);
  }

     
public void visitLakeLevelControllerEffect(ILakeLevelControllerEffect effect) { effect.visitILakeLevelControllerEffect(this); }
  public void visitLakeLevelControllerCreateEffect(LakeLevelControllerCreateEffect effect) {
    var instance = root.EffectLakeLevelControllerCreate(
  root.GetLevel(effect.incarnation.level)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitLakeLevelControllerDeleteEffect(LakeLevelControllerDeleteEffect effect) {
    root.EffectLakeLevelControllerDelete(effect.id);
  }

     
public void visitEmberDeepLevelLinkerTTCEffect(IEmberDeepLevelLinkerTTCEffect effect) { effect.visitIEmberDeepLevelLinkerTTCEffect(this); }
  public void visitEmberDeepLevelLinkerTTCCreateEffect(EmberDeepLevelLinkerTTCCreateEffect effect) {
    var instance = root.EffectEmberDeepLevelLinkerTTCCreate(
  effect.incarnation.nextLevelDepth    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitEmberDeepLevelLinkerTTCDeleteEffect(EmberDeepLevelLinkerTTCDeleteEffect effect) {
    root.EffectEmberDeepLevelLinkerTTCDelete(effect.id);
  }

     
public void visitDirtRoadLevelControllerEffect(IDirtRoadLevelControllerEffect effect) { effect.visitIDirtRoadLevelControllerEffect(this); }
  public void visitDirtRoadLevelControllerCreateEffect(DirtRoadLevelControllerCreateEffect effect) {
    var instance = root.EffectDirtRoadLevelControllerCreate(
  root.GetLevel(effect.incarnation.level)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitDirtRoadLevelControllerDeleteEffect(DirtRoadLevelControllerDeleteEffect effect) {
    root.EffectDirtRoadLevelControllerDelete(effect.id);
  }

     
public void visitCaveLevelControllerEffect(ICaveLevelControllerEffect effect) { effect.visitICaveLevelControllerEffect(this); }
  public void visitCaveLevelControllerCreateEffect(CaveLevelControllerCreateEffect effect) {
    var instance = root.EffectCaveLevelControllerCreate(
  root.GetLevel(effect.incarnation.level),
  effect.incarnation.depth    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitCaveLevelControllerDeleteEffect(CaveLevelControllerDeleteEffect effect) {
    root.EffectCaveLevelControllerDelete(effect.id);
  }

     
public void visitBridgesLevelControllerEffect(IBridgesLevelControllerEffect effect) { effect.visitIBridgesLevelControllerEffect(this); }
  public void visitBridgesLevelControllerCreateEffect(BridgesLevelControllerCreateEffect effect) {
    var instance = root.EffectBridgesLevelControllerCreate(
  root.GetLevel(effect.incarnation.level)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitBridgesLevelControllerDeleteEffect(BridgesLevelControllerDeleteEffect effect) {
    root.EffectBridgesLevelControllerDelete(effect.id);
  }

     
public void visitAncientTownLevelControllerEffect(IAncientTownLevelControllerEffect effect) { effect.visitIAncientTownLevelControllerEffect(this); }
  public void visitAncientTownLevelControllerCreateEffect(AncientTownLevelControllerCreateEffect effect) {
    var instance = root.EffectAncientTownLevelControllerCreate(
  root.GetLevel(effect.incarnation.level)    );

  // If this fails, then we have to add a translation layer.
  // We shouldn't allow the user to specify the internal ID, because that's
  // core to a bunch of optimizations (such as how it's a generational index).
  Asserts.Assert(instance.id == effect.id, "New ID mismatch!");
}

  public void visitAncientTownLevelControllerDeleteEffect(AncientTownLevelControllerDeleteEffect effect) {
    root.EffectAncientTownLevelControllerDelete(effect.id);
  }

     
    public void visitCommMutListEffect(ICommMutListEffect effect) { effect.visitICommMutListEffect(this); }
    public void visitCommMutListCreateEffect(CommMutListCreateEffect effect) {
      var list = root.EffectCommMutListCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectLocationMutListCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectIRequestMutListCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectLevelMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectManaPotionStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectHealthPotionStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectSpeedRingStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectGlaiveStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectSlowRodStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectBlastRodStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectArmorStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectHoldPositionImpulseStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectTemporaryCloneImpulseStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectSummonImpulseStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectMireImpulseStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectEvaporateImpulseStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectMoveImpulseStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectKamikazeJumpImpulseStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectKamikazeTargetImpulseStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectNoImpulseStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectFireImpulseStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectDefyImpulseStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectCounterImpulseStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectUnleashBideImpulseStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectContinueBidingImpulseStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectStartBidingImpulseStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectAttackImpulseStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectPursueImpulseStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectFireBombImpulseStrongMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectLightningChargedUCWeakMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectTimeCloneAICapabilityUCWeakMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectDoomedUCWeakMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectMiredUCWeakMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectInvincibilityUCWeakMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectDefyingUCWeakMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectCounteringUCWeakMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectAttackAICapabilityUCWeakMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectUnitMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectSimplePresenceTriggerTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectItemTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectKamikazeTargetTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectWarperTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectTimeAnchorTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectFireBombTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectMarkerTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectLevelLinkTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectMudTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectDirtTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectObsidianTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectDownStairsTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectUpStairsTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectWallTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectBloodTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectRocksTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectTreeTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectWaterTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectFloorTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectCaveWallTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectCaveTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectFallsTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectFireTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectObsidianFloorTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectMagmaTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectCliffTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectRavaNestTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectCliffLandingTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectStoneTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectGrassTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectIncendianFallsLevelLinkerTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectEmberDeepLevelLinkerTTCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectTutorialDefyCounterUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectLightningChargingUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectWanderAICapabilityUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectTemporaryCloneAICapabilityUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectSummonAICapabilityUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectKamikazeAICapabilityUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectGuardAICapabilityUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectTimeCloneAICapabilityUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectDoomedUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectMiredUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectAttackAICapabilityUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectCounteringUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectLightningChargedUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectInvincibilityUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectDefyingUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectBideAICapabilityUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectBaseSightRangeUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectBaseMovementTimeUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectBaseCombatTimeUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectManaPotionMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectHealthPotionMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectSpeedRingMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectGlaiveMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectSlowRodMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectBlastRodMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectArmorMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectSorcerousUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectBaseOffenseUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectBaseDefenseUCMutSetCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectTerrainTileByLocationMutMapCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
      var list = root.EffectKamikazeTargetTTCStrongByLocationMutMapCreate();
      // If this fails, then we have to add a translation layer.
      // We shouldn't allow the user to specify the internal ID, because that's
      // core to a bunch of optimizations (such as how it's a generational index).
      Asserts.Assert(list.id == effect.id, "New ID mismatch!");
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
