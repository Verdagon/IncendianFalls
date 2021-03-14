using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IUnitComponentMutBunchBroadcaster:ITutorialDefyCounterUCMutSetEffectObserver, ITutorialDefyCounterUCMutSetEffectVisitor, ILightningChargingUCMutSetEffectObserver, ILightningChargingUCMutSetEffectVisitor, IWanderAICapabilityUCMutSetEffectObserver, IWanderAICapabilityUCMutSetEffectVisitor, ITemporaryCloneAICapabilityUCMutSetEffectObserver, ITemporaryCloneAICapabilityUCMutSetEffectVisitor, ISummonAICapabilityUCMutSetEffectObserver, ISummonAICapabilityUCMutSetEffectVisitor, IKamikazeAICapabilityUCMutSetEffectObserver, IKamikazeAICapabilityUCMutSetEffectVisitor, IGuardAICapabilityUCMutSetEffectObserver, IGuardAICapabilityUCMutSetEffectVisitor, IEvolvifyAICapabilityUCMutSetEffectObserver, IEvolvifyAICapabilityUCMutSetEffectVisitor, ITimeCloneAICapabilityUCMutSetEffectObserver, ITimeCloneAICapabilityUCMutSetEffectVisitor, IDoomedUCMutSetEffectObserver, IDoomedUCMutSetEffectVisitor, IMiredUCMutSetEffectObserver, IMiredUCMutSetEffectVisitor, IOnFireUCMutSetEffectObserver, IOnFireUCMutSetEffectVisitor, IAttackAICapabilityUCMutSetEffectObserver, IAttackAICapabilityUCMutSetEffectVisitor, ICounteringUCMutSetEffectObserver, ICounteringUCMutSetEffectVisitor, ILightningChargedUCMutSetEffectObserver, ILightningChargedUCMutSetEffectVisitor, IInvincibilityUCMutSetEffectObserver, IInvincibilityUCMutSetEffectVisitor, IDefyingUCMutSetEffectObserver, IDefyingUCMutSetEffectVisitor, IBideAICapabilityUCMutSetEffectObserver, IBideAICapabilityUCMutSetEffectVisitor, IBaseSightRangeUCMutSetEffectObserver, IBaseSightRangeUCMutSetEffectVisitor, IBaseMovementTimeUCMutSetEffectObserver, IBaseMovementTimeUCMutSetEffectVisitor, IBaseCombatTimeUCMutSetEffectObserver, IBaseCombatTimeUCMutSetEffectVisitor, IManaPotionMutSetEffectObserver, IManaPotionMutSetEffectVisitor, IHealthPotionMutSetEffectObserver, IHealthPotionMutSetEffectVisitor, ISpeedRingMutSetEffectObserver, ISpeedRingMutSetEffectVisitor, IGlaiveMutSetEffectObserver, IGlaiveMutSetEffectVisitor, ISlowRodMutSetEffectObserver, ISlowRodMutSetEffectVisitor, IExplosionRodMutSetEffectObserver, IExplosionRodMutSetEffectVisitor, IBlazeRodMutSetEffectObserver, IBlazeRodMutSetEffectVisitor, IBlastRodMutSetEffectObserver, IBlastRodMutSetEffectVisitor, IArmorMutSetEffectObserver, IArmorMutSetEffectVisitor, ISorcerousUCMutSetEffectObserver, ISorcerousUCMutSetEffectVisitor, IBaseOffenseUCMutSetEffectObserver, IBaseOffenseUCMutSetEffectVisitor, IBaseDefenseUCMutSetEffectObserver, IBaseDefenseUCMutSetEffectVisitor {
  EffectBroadcaster broadcaster;
  IUnitComponentMutBunch bunch;
  private List<IIUnitComponentMutBunchObserver> observers;

  public IUnitComponentMutBunchBroadcaster(EffectBroadcaster broadcaster, IUnitComponentMutBunch bunch) {
    this.broadcaster = broadcaster;
    this.bunch = bunch;
    this.observers = new List<IIUnitComponentMutBunchObserver>();
    bunch.membersTutorialDefyCounterUCMutSet.AddObserver(broadcaster, this);
    bunch.membersLightningChargingUCMutSet.AddObserver(broadcaster, this);
    bunch.membersWanderAICapabilityUCMutSet.AddObserver(broadcaster, this);
    bunch.membersTemporaryCloneAICapabilityUCMutSet.AddObserver(broadcaster, this);
    bunch.membersSummonAICapabilityUCMutSet.AddObserver(broadcaster, this);
    bunch.membersKamikazeAICapabilityUCMutSet.AddObserver(broadcaster, this);
    bunch.membersGuardAICapabilityUCMutSet.AddObserver(broadcaster, this);
    bunch.membersEvolvifyAICapabilityUCMutSet.AddObserver(broadcaster, this);
    bunch.membersTimeCloneAICapabilityUCMutSet.AddObserver(broadcaster, this);
    bunch.membersDoomedUCMutSet.AddObserver(broadcaster, this);
    bunch.membersMiredUCMutSet.AddObserver(broadcaster, this);
    bunch.membersOnFireUCMutSet.AddObserver(broadcaster, this);
    bunch.membersAttackAICapabilityUCMutSet.AddObserver(broadcaster, this);
    bunch.membersCounteringUCMutSet.AddObserver(broadcaster, this);
    bunch.membersLightningChargedUCMutSet.AddObserver(broadcaster, this);
    bunch.membersInvincibilityUCMutSet.AddObserver(broadcaster, this);
    bunch.membersDefyingUCMutSet.AddObserver(broadcaster, this);
    bunch.membersBideAICapabilityUCMutSet.AddObserver(broadcaster, this);
    bunch.membersBaseSightRangeUCMutSet.AddObserver(broadcaster, this);
    bunch.membersBaseMovementTimeUCMutSet.AddObserver(broadcaster, this);
    bunch.membersBaseCombatTimeUCMutSet.AddObserver(broadcaster, this);
    bunch.membersManaPotionMutSet.AddObserver(broadcaster, this);
    bunch.membersHealthPotionMutSet.AddObserver(broadcaster, this);
    bunch.membersSpeedRingMutSet.AddObserver(broadcaster, this);
    bunch.membersGlaiveMutSet.AddObserver(broadcaster, this);
    bunch.membersSlowRodMutSet.AddObserver(broadcaster, this);
    bunch.membersExplosionRodMutSet.AddObserver(broadcaster, this);
    bunch.membersBlazeRodMutSet.AddObserver(broadcaster, this);
    bunch.membersBlastRodMutSet.AddObserver(broadcaster, this);
    bunch.membersArmorMutSet.AddObserver(broadcaster, this);
    bunch.membersSorcerousUCMutSet.AddObserver(broadcaster, this);
    bunch.membersBaseOffenseUCMutSet.AddObserver(broadcaster, this);
    bunch.membersBaseDefenseUCMutSet.AddObserver(broadcaster, this);

  }
  public void Stop() {
    bunch.membersTutorialDefyCounterUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersLightningChargingUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersWanderAICapabilityUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersTemporaryCloneAICapabilityUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersSummonAICapabilityUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersKamikazeAICapabilityUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersGuardAICapabilityUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersEvolvifyAICapabilityUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersTimeCloneAICapabilityUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersDoomedUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersMiredUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersOnFireUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersAttackAICapabilityUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersCounteringUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersLightningChargedUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersInvincibilityUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersDefyingUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersBideAICapabilityUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersBaseSightRangeUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersBaseMovementTimeUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersBaseCombatTimeUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersManaPotionMutSet.RemoveObserver(broadcaster, this);
    bunch.membersHealthPotionMutSet.RemoveObserver(broadcaster, this);
    bunch.membersSpeedRingMutSet.RemoveObserver(broadcaster, this);
    bunch.membersGlaiveMutSet.RemoveObserver(broadcaster, this);
    bunch.membersSlowRodMutSet.RemoveObserver(broadcaster, this);
    bunch.membersExplosionRodMutSet.RemoveObserver(broadcaster, this);
    bunch.membersBlazeRodMutSet.RemoveObserver(broadcaster, this);
    bunch.membersBlastRodMutSet.RemoveObserver(broadcaster, this);
    bunch.membersArmorMutSet.RemoveObserver(broadcaster, this);
    bunch.membersSorcerousUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersBaseOffenseUCMutSet.RemoveObserver(broadcaster, this);
    bunch.membersBaseDefenseUCMutSet.RemoveObserver(broadcaster, this);

  }
  public void AddObserver(IIUnitComponentMutBunchObserver observer) {
    this.observers.Add(observer);
  }
  public void RemoveObserver(IIUnitComponentMutBunchObserver observer) {
    this.observers.Remove(observer);
  }
  private void BroadcastAdd(int id) {
    foreach (var observer in observers) {
      observer.OnIUnitComponentMutBunchAdd(id);
    }
  }
  private void BroadcastRemove(int id) {
    foreach (var observer in observers) {
      observer.OnIUnitComponentMutBunchRemove(id);
    }
  }
  public void OnTutorialDefyCounterUCMutSetEffect(ITutorialDefyCounterUCMutSetEffect effect) {
    effect.visitITutorialDefyCounterUCMutSetEffect(this);
  }
  public void visitTutorialDefyCounterUCMutSetAddEffect(TutorialDefyCounterUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitTutorialDefyCounterUCMutSetRemoveEffect(TutorialDefyCounterUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitTutorialDefyCounterUCMutSetCreateEffect(TutorialDefyCounterUCMutSetCreateEffect effect) { }
  public void visitTutorialDefyCounterUCMutSetDeleteEffect(TutorialDefyCounterUCMutSetDeleteEffect effect) { }
  public void OnLightningChargingUCMutSetEffect(ILightningChargingUCMutSetEffect effect) {
    effect.visitILightningChargingUCMutSetEffect(this);
  }
  public void visitLightningChargingUCMutSetAddEffect(LightningChargingUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitLightningChargingUCMutSetRemoveEffect(LightningChargingUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitLightningChargingUCMutSetCreateEffect(LightningChargingUCMutSetCreateEffect effect) { }
  public void visitLightningChargingUCMutSetDeleteEffect(LightningChargingUCMutSetDeleteEffect effect) { }
  public void OnWanderAICapabilityUCMutSetEffect(IWanderAICapabilityUCMutSetEffect effect) {
    effect.visitIWanderAICapabilityUCMutSetEffect(this);
  }
  public void visitWanderAICapabilityUCMutSetAddEffect(WanderAICapabilityUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitWanderAICapabilityUCMutSetRemoveEffect(WanderAICapabilityUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitWanderAICapabilityUCMutSetCreateEffect(WanderAICapabilityUCMutSetCreateEffect effect) { }
  public void visitWanderAICapabilityUCMutSetDeleteEffect(WanderAICapabilityUCMutSetDeleteEffect effect) { }
  public void OnTemporaryCloneAICapabilityUCMutSetEffect(ITemporaryCloneAICapabilityUCMutSetEffect effect) {
    effect.visitITemporaryCloneAICapabilityUCMutSetEffect(this);
  }
  public void visitTemporaryCloneAICapabilityUCMutSetAddEffect(TemporaryCloneAICapabilityUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitTemporaryCloneAICapabilityUCMutSetRemoveEffect(TemporaryCloneAICapabilityUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitTemporaryCloneAICapabilityUCMutSetCreateEffect(TemporaryCloneAICapabilityUCMutSetCreateEffect effect) { }
  public void visitTemporaryCloneAICapabilityUCMutSetDeleteEffect(TemporaryCloneAICapabilityUCMutSetDeleteEffect effect) { }
  public void OnSummonAICapabilityUCMutSetEffect(ISummonAICapabilityUCMutSetEffect effect) {
    effect.visitISummonAICapabilityUCMutSetEffect(this);
  }
  public void visitSummonAICapabilityUCMutSetAddEffect(SummonAICapabilityUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitSummonAICapabilityUCMutSetRemoveEffect(SummonAICapabilityUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitSummonAICapabilityUCMutSetCreateEffect(SummonAICapabilityUCMutSetCreateEffect effect) { }
  public void visitSummonAICapabilityUCMutSetDeleteEffect(SummonAICapabilityUCMutSetDeleteEffect effect) { }
  public void OnKamikazeAICapabilityUCMutSetEffect(IKamikazeAICapabilityUCMutSetEffect effect) {
    effect.visitIKamikazeAICapabilityUCMutSetEffect(this);
  }
  public void visitKamikazeAICapabilityUCMutSetAddEffect(KamikazeAICapabilityUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitKamikazeAICapabilityUCMutSetRemoveEffect(KamikazeAICapabilityUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitKamikazeAICapabilityUCMutSetCreateEffect(KamikazeAICapabilityUCMutSetCreateEffect effect) { }
  public void visitKamikazeAICapabilityUCMutSetDeleteEffect(KamikazeAICapabilityUCMutSetDeleteEffect effect) { }
  public void OnGuardAICapabilityUCMutSetEffect(IGuardAICapabilityUCMutSetEffect effect) {
    effect.visitIGuardAICapabilityUCMutSetEffect(this);
  }
  public void visitGuardAICapabilityUCMutSetAddEffect(GuardAICapabilityUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitGuardAICapabilityUCMutSetRemoveEffect(GuardAICapabilityUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitGuardAICapabilityUCMutSetCreateEffect(GuardAICapabilityUCMutSetCreateEffect effect) { }
  public void visitGuardAICapabilityUCMutSetDeleteEffect(GuardAICapabilityUCMutSetDeleteEffect effect) { }
  public void OnEvolvifyAICapabilityUCMutSetEffect(IEvolvifyAICapabilityUCMutSetEffect effect) {
    effect.visitIEvolvifyAICapabilityUCMutSetEffect(this);
  }
  public void visitEvolvifyAICapabilityUCMutSetAddEffect(EvolvifyAICapabilityUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitEvolvifyAICapabilityUCMutSetRemoveEffect(EvolvifyAICapabilityUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitEvolvifyAICapabilityUCMutSetCreateEffect(EvolvifyAICapabilityUCMutSetCreateEffect effect) { }
  public void visitEvolvifyAICapabilityUCMutSetDeleteEffect(EvolvifyAICapabilityUCMutSetDeleteEffect effect) { }
  public void OnTimeCloneAICapabilityUCMutSetEffect(ITimeCloneAICapabilityUCMutSetEffect effect) {
    effect.visitITimeCloneAICapabilityUCMutSetEffect(this);
  }
  public void visitTimeCloneAICapabilityUCMutSetAddEffect(TimeCloneAICapabilityUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitTimeCloneAICapabilityUCMutSetRemoveEffect(TimeCloneAICapabilityUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitTimeCloneAICapabilityUCMutSetCreateEffect(TimeCloneAICapabilityUCMutSetCreateEffect effect) { }
  public void visitTimeCloneAICapabilityUCMutSetDeleteEffect(TimeCloneAICapabilityUCMutSetDeleteEffect effect) { }
  public void OnDoomedUCMutSetEffect(IDoomedUCMutSetEffect effect) {
    effect.visitIDoomedUCMutSetEffect(this);
  }
  public void visitDoomedUCMutSetAddEffect(DoomedUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitDoomedUCMutSetRemoveEffect(DoomedUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitDoomedUCMutSetCreateEffect(DoomedUCMutSetCreateEffect effect) { }
  public void visitDoomedUCMutSetDeleteEffect(DoomedUCMutSetDeleteEffect effect) { }
  public void OnMiredUCMutSetEffect(IMiredUCMutSetEffect effect) {
    effect.visitIMiredUCMutSetEffect(this);
  }
  public void visitMiredUCMutSetAddEffect(MiredUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitMiredUCMutSetRemoveEffect(MiredUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitMiredUCMutSetCreateEffect(MiredUCMutSetCreateEffect effect) { }
  public void visitMiredUCMutSetDeleteEffect(MiredUCMutSetDeleteEffect effect) { }
  public void OnOnFireUCMutSetEffect(IOnFireUCMutSetEffect effect) {
    effect.visitIOnFireUCMutSetEffect(this);
  }
  public void visitOnFireUCMutSetAddEffect(OnFireUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitOnFireUCMutSetRemoveEffect(OnFireUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitOnFireUCMutSetCreateEffect(OnFireUCMutSetCreateEffect effect) { }
  public void visitOnFireUCMutSetDeleteEffect(OnFireUCMutSetDeleteEffect effect) { }
  public void OnAttackAICapabilityUCMutSetEffect(IAttackAICapabilityUCMutSetEffect effect) {
    effect.visitIAttackAICapabilityUCMutSetEffect(this);
  }
  public void visitAttackAICapabilityUCMutSetAddEffect(AttackAICapabilityUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitAttackAICapabilityUCMutSetRemoveEffect(AttackAICapabilityUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitAttackAICapabilityUCMutSetCreateEffect(AttackAICapabilityUCMutSetCreateEffect effect) { }
  public void visitAttackAICapabilityUCMutSetDeleteEffect(AttackAICapabilityUCMutSetDeleteEffect effect) { }
  public void OnCounteringUCMutSetEffect(ICounteringUCMutSetEffect effect) {
    effect.visitICounteringUCMutSetEffect(this);
  }
  public void visitCounteringUCMutSetAddEffect(CounteringUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitCounteringUCMutSetRemoveEffect(CounteringUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitCounteringUCMutSetCreateEffect(CounteringUCMutSetCreateEffect effect) { }
  public void visitCounteringUCMutSetDeleteEffect(CounteringUCMutSetDeleteEffect effect) { }
  public void OnLightningChargedUCMutSetEffect(ILightningChargedUCMutSetEffect effect) {
    effect.visitILightningChargedUCMutSetEffect(this);
  }
  public void visitLightningChargedUCMutSetAddEffect(LightningChargedUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitLightningChargedUCMutSetRemoveEffect(LightningChargedUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitLightningChargedUCMutSetCreateEffect(LightningChargedUCMutSetCreateEffect effect) { }
  public void visitLightningChargedUCMutSetDeleteEffect(LightningChargedUCMutSetDeleteEffect effect) { }
  public void OnInvincibilityUCMutSetEffect(IInvincibilityUCMutSetEffect effect) {
    effect.visitIInvincibilityUCMutSetEffect(this);
  }
  public void visitInvincibilityUCMutSetAddEffect(InvincibilityUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitInvincibilityUCMutSetRemoveEffect(InvincibilityUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitInvincibilityUCMutSetCreateEffect(InvincibilityUCMutSetCreateEffect effect) { }
  public void visitInvincibilityUCMutSetDeleteEffect(InvincibilityUCMutSetDeleteEffect effect) { }
  public void OnDefyingUCMutSetEffect(IDefyingUCMutSetEffect effect) {
    effect.visitIDefyingUCMutSetEffect(this);
  }
  public void visitDefyingUCMutSetAddEffect(DefyingUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitDefyingUCMutSetRemoveEffect(DefyingUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitDefyingUCMutSetCreateEffect(DefyingUCMutSetCreateEffect effect) { }
  public void visitDefyingUCMutSetDeleteEffect(DefyingUCMutSetDeleteEffect effect) { }
  public void OnBideAICapabilityUCMutSetEffect(IBideAICapabilityUCMutSetEffect effect) {
    effect.visitIBideAICapabilityUCMutSetEffect(this);
  }
  public void visitBideAICapabilityUCMutSetAddEffect(BideAICapabilityUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitBideAICapabilityUCMutSetRemoveEffect(BideAICapabilityUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitBideAICapabilityUCMutSetCreateEffect(BideAICapabilityUCMutSetCreateEffect effect) { }
  public void visitBideAICapabilityUCMutSetDeleteEffect(BideAICapabilityUCMutSetDeleteEffect effect) { }
  public void OnBaseSightRangeUCMutSetEffect(IBaseSightRangeUCMutSetEffect effect) {
    effect.visitIBaseSightRangeUCMutSetEffect(this);
  }
  public void visitBaseSightRangeUCMutSetAddEffect(BaseSightRangeUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitBaseSightRangeUCMutSetRemoveEffect(BaseSightRangeUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitBaseSightRangeUCMutSetCreateEffect(BaseSightRangeUCMutSetCreateEffect effect) { }
  public void visitBaseSightRangeUCMutSetDeleteEffect(BaseSightRangeUCMutSetDeleteEffect effect) { }
  public void OnBaseMovementTimeUCMutSetEffect(IBaseMovementTimeUCMutSetEffect effect) {
    effect.visitIBaseMovementTimeUCMutSetEffect(this);
  }
  public void visitBaseMovementTimeUCMutSetAddEffect(BaseMovementTimeUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitBaseMovementTimeUCMutSetRemoveEffect(BaseMovementTimeUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitBaseMovementTimeUCMutSetCreateEffect(BaseMovementTimeUCMutSetCreateEffect effect) { }
  public void visitBaseMovementTimeUCMutSetDeleteEffect(BaseMovementTimeUCMutSetDeleteEffect effect) { }
  public void OnBaseCombatTimeUCMutSetEffect(IBaseCombatTimeUCMutSetEffect effect) {
    effect.visitIBaseCombatTimeUCMutSetEffect(this);
  }
  public void visitBaseCombatTimeUCMutSetAddEffect(BaseCombatTimeUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitBaseCombatTimeUCMutSetRemoveEffect(BaseCombatTimeUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitBaseCombatTimeUCMutSetCreateEffect(BaseCombatTimeUCMutSetCreateEffect effect) { }
  public void visitBaseCombatTimeUCMutSetDeleteEffect(BaseCombatTimeUCMutSetDeleteEffect effect) { }
  public void OnManaPotionMutSetEffect(IManaPotionMutSetEffect effect) {
    effect.visitIManaPotionMutSetEffect(this);
  }
  public void visitManaPotionMutSetAddEffect(ManaPotionMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitManaPotionMutSetRemoveEffect(ManaPotionMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitManaPotionMutSetCreateEffect(ManaPotionMutSetCreateEffect effect) { }
  public void visitManaPotionMutSetDeleteEffect(ManaPotionMutSetDeleteEffect effect) { }
  public void OnHealthPotionMutSetEffect(IHealthPotionMutSetEffect effect) {
    effect.visitIHealthPotionMutSetEffect(this);
  }
  public void visitHealthPotionMutSetAddEffect(HealthPotionMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitHealthPotionMutSetRemoveEffect(HealthPotionMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitHealthPotionMutSetCreateEffect(HealthPotionMutSetCreateEffect effect) { }
  public void visitHealthPotionMutSetDeleteEffect(HealthPotionMutSetDeleteEffect effect) { }
  public void OnSpeedRingMutSetEffect(ISpeedRingMutSetEffect effect) {
    effect.visitISpeedRingMutSetEffect(this);
  }
  public void visitSpeedRingMutSetAddEffect(SpeedRingMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitSpeedRingMutSetRemoveEffect(SpeedRingMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitSpeedRingMutSetCreateEffect(SpeedRingMutSetCreateEffect effect) { }
  public void visitSpeedRingMutSetDeleteEffect(SpeedRingMutSetDeleteEffect effect) { }
  public void OnGlaiveMutSetEffect(IGlaiveMutSetEffect effect) {
    effect.visitIGlaiveMutSetEffect(this);
  }
  public void visitGlaiveMutSetAddEffect(GlaiveMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitGlaiveMutSetRemoveEffect(GlaiveMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitGlaiveMutSetCreateEffect(GlaiveMutSetCreateEffect effect) { }
  public void visitGlaiveMutSetDeleteEffect(GlaiveMutSetDeleteEffect effect) { }
  public void OnSlowRodMutSetEffect(ISlowRodMutSetEffect effect) {
    effect.visitISlowRodMutSetEffect(this);
  }
  public void visitSlowRodMutSetAddEffect(SlowRodMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitSlowRodMutSetRemoveEffect(SlowRodMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitSlowRodMutSetCreateEffect(SlowRodMutSetCreateEffect effect) { }
  public void visitSlowRodMutSetDeleteEffect(SlowRodMutSetDeleteEffect effect) { }
  public void OnExplosionRodMutSetEffect(IExplosionRodMutSetEffect effect) {
    effect.visitIExplosionRodMutSetEffect(this);
  }
  public void visitExplosionRodMutSetAddEffect(ExplosionRodMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitExplosionRodMutSetRemoveEffect(ExplosionRodMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitExplosionRodMutSetCreateEffect(ExplosionRodMutSetCreateEffect effect) { }
  public void visitExplosionRodMutSetDeleteEffect(ExplosionRodMutSetDeleteEffect effect) { }
  public void OnBlazeRodMutSetEffect(IBlazeRodMutSetEffect effect) {
    effect.visitIBlazeRodMutSetEffect(this);
  }
  public void visitBlazeRodMutSetAddEffect(BlazeRodMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitBlazeRodMutSetRemoveEffect(BlazeRodMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitBlazeRodMutSetCreateEffect(BlazeRodMutSetCreateEffect effect) { }
  public void visitBlazeRodMutSetDeleteEffect(BlazeRodMutSetDeleteEffect effect) { }
  public void OnBlastRodMutSetEffect(IBlastRodMutSetEffect effect) {
    effect.visitIBlastRodMutSetEffect(this);
  }
  public void visitBlastRodMutSetAddEffect(BlastRodMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitBlastRodMutSetRemoveEffect(BlastRodMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitBlastRodMutSetCreateEffect(BlastRodMutSetCreateEffect effect) { }
  public void visitBlastRodMutSetDeleteEffect(BlastRodMutSetDeleteEffect effect) { }
  public void OnArmorMutSetEffect(IArmorMutSetEffect effect) {
    effect.visitIArmorMutSetEffect(this);
  }
  public void visitArmorMutSetAddEffect(ArmorMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitArmorMutSetRemoveEffect(ArmorMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitArmorMutSetCreateEffect(ArmorMutSetCreateEffect effect) { }
  public void visitArmorMutSetDeleteEffect(ArmorMutSetDeleteEffect effect) { }
  public void OnSorcerousUCMutSetEffect(ISorcerousUCMutSetEffect effect) {
    effect.visitISorcerousUCMutSetEffect(this);
  }
  public void visitSorcerousUCMutSetAddEffect(SorcerousUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitSorcerousUCMutSetRemoveEffect(SorcerousUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitSorcerousUCMutSetCreateEffect(SorcerousUCMutSetCreateEffect effect) { }
  public void visitSorcerousUCMutSetDeleteEffect(SorcerousUCMutSetDeleteEffect effect) { }
  public void OnBaseOffenseUCMutSetEffect(IBaseOffenseUCMutSetEffect effect) {
    effect.visitIBaseOffenseUCMutSetEffect(this);
  }
  public void visitBaseOffenseUCMutSetAddEffect(BaseOffenseUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitBaseOffenseUCMutSetRemoveEffect(BaseOffenseUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitBaseOffenseUCMutSetCreateEffect(BaseOffenseUCMutSetCreateEffect effect) { }
  public void visitBaseOffenseUCMutSetDeleteEffect(BaseOffenseUCMutSetDeleteEffect effect) { }
  public void OnBaseDefenseUCMutSetEffect(IBaseDefenseUCMutSetEffect effect) {
    effect.visitIBaseDefenseUCMutSetEffect(this);
  }
  public void visitBaseDefenseUCMutSetAddEffect(BaseDefenseUCMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitBaseDefenseUCMutSetRemoveEffect(BaseDefenseUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitBaseDefenseUCMutSetCreateEffect(BaseDefenseUCMutSetCreateEffect effect) { }
  public void visitBaseDefenseUCMutSetDeleteEffect(BaseDefenseUCMutSetDeleteEffect effect) { }

}
       
}
