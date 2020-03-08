using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IUnitComponentMutBunchBroadcaster:ILightningChargingUCMutSetEffectObserver, ILightningChargingUCMutSetEffectVisitor, IWanderAICapabilityUCMutSetEffectObserver, IWanderAICapabilityUCMutSetEffectVisitor, ITemporaryCloneAICapabilityUCMutSetEffectObserver, ITemporaryCloneAICapabilityUCMutSetEffectVisitor, ISummonAICapabilityUCMutSetEffectObserver, ISummonAICapabilityUCMutSetEffectVisitor, IKamikazeAICapabilityUCMutSetEffectObserver, IKamikazeAICapabilityUCMutSetEffectVisitor, IGuardAICapabilityUCMutSetEffectObserver, IGuardAICapabilityUCMutSetEffectVisitor, ITimeCloneAICapabilityUCMutSetEffectObserver, ITimeCloneAICapabilityUCMutSetEffectVisitor, IDoomedUCMutSetEffectObserver, IDoomedUCMutSetEffectVisitor, IMiredUCMutSetEffectObserver, IMiredUCMutSetEffectVisitor, IAttackAICapabilityUCMutSetEffectObserver, IAttackAICapabilityUCMutSetEffectVisitor, ICounteringUCMutSetEffectObserver, ICounteringUCMutSetEffectVisitor, ILightningChargedUCMutSetEffectObserver, ILightningChargedUCMutSetEffectVisitor, IInvincibilityUCMutSetEffectObserver, IInvincibilityUCMutSetEffectVisitor, IDefyingUCMutSetEffectObserver, IDefyingUCMutSetEffectVisitor, IBideAICapabilityUCMutSetEffectObserver, IBideAICapabilityUCMutSetEffectVisitor, IBaseMovementTimeUCMutSetEffectObserver, IBaseMovementTimeUCMutSetEffectVisitor, IBaseCombatTimeUCMutSetEffectObserver, IBaseCombatTimeUCMutSetEffectVisitor, IManaPotionMutSetEffectObserver, IManaPotionMutSetEffectVisitor, IHealthPotionMutSetEffectObserver, IHealthPotionMutSetEffectVisitor, ISpeedRingMutSetEffectObserver, ISpeedRingMutSetEffectVisitor, IGlaiveMutSetEffectObserver, IGlaiveMutSetEffectVisitor, IBlastRodMutSetEffectObserver, IBlastRodMutSetEffectVisitor, IArmorMutSetEffectObserver, IArmorMutSetEffectVisitor, ISorcerousUCMutSetEffectObserver, ISorcerousUCMutSetEffectVisitor, IBaseOffenseUCMutSetEffectObserver, IBaseOffenseUCMutSetEffectVisitor, IBaseDefenseUCMutSetEffectObserver, IBaseDefenseUCMutSetEffectVisitor {
  IUnitComponentMutBunch bunch;
  private List<IIUnitComponentMutBunchObserver> observers;

  public IUnitComponentMutBunchBroadcaster(IUnitComponentMutBunch bunch) {
    this.bunch = bunch;
    this.observers = new List<IIUnitComponentMutBunchObserver>();
    bunch.membersLightningChargingUCMutSet.AddObserver(this);
    bunch.membersWanderAICapabilityUCMutSet.AddObserver(this);
    bunch.membersTemporaryCloneAICapabilityUCMutSet.AddObserver(this);
    bunch.membersSummonAICapabilityUCMutSet.AddObserver(this);
    bunch.membersKamikazeAICapabilityUCMutSet.AddObserver(this);
    bunch.membersGuardAICapabilityUCMutSet.AddObserver(this);
    bunch.membersTimeCloneAICapabilityUCMutSet.AddObserver(this);
    bunch.membersDoomedUCMutSet.AddObserver(this);
    bunch.membersMiredUCMutSet.AddObserver(this);
    bunch.membersAttackAICapabilityUCMutSet.AddObserver(this);
    bunch.membersCounteringUCMutSet.AddObserver(this);
    bunch.membersLightningChargedUCMutSet.AddObserver(this);
    bunch.membersInvincibilityUCMutSet.AddObserver(this);
    bunch.membersDefyingUCMutSet.AddObserver(this);
    bunch.membersBideAICapabilityUCMutSet.AddObserver(this);
    bunch.membersBaseMovementTimeUCMutSet.AddObserver(this);
    bunch.membersBaseCombatTimeUCMutSet.AddObserver(this);
    bunch.membersManaPotionMutSet.AddObserver(this);
    bunch.membersHealthPotionMutSet.AddObserver(this);
    bunch.membersSpeedRingMutSet.AddObserver(this);
    bunch.membersGlaiveMutSet.AddObserver(this);
    bunch.membersBlastRodMutSet.AddObserver(this);
    bunch.membersArmorMutSet.AddObserver(this);
    bunch.membersSorcerousUCMutSet.AddObserver(this);
    bunch.membersBaseOffenseUCMutSet.AddObserver(this);
    bunch.membersBaseDefenseUCMutSet.AddObserver(this);

  }
  public void Stop() {
    bunch.membersLightningChargingUCMutSet.RemoveObserver(this);
    bunch.membersWanderAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersTemporaryCloneAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersSummonAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersKamikazeAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersGuardAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersTimeCloneAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersDoomedUCMutSet.RemoveObserver(this);
    bunch.membersMiredUCMutSet.RemoveObserver(this);
    bunch.membersAttackAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersCounteringUCMutSet.RemoveObserver(this);
    bunch.membersLightningChargedUCMutSet.RemoveObserver(this);
    bunch.membersInvincibilityUCMutSet.RemoveObserver(this);
    bunch.membersDefyingUCMutSet.RemoveObserver(this);
    bunch.membersBideAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersBaseMovementTimeUCMutSet.RemoveObserver(this);
    bunch.membersBaseCombatTimeUCMutSet.RemoveObserver(this);
    bunch.membersManaPotionMutSet.RemoveObserver(this);
    bunch.membersHealthPotionMutSet.RemoveObserver(this);
    bunch.membersSpeedRingMutSet.RemoveObserver(this);
    bunch.membersGlaiveMutSet.RemoveObserver(this);
    bunch.membersBlastRodMutSet.RemoveObserver(this);
    bunch.membersArmorMutSet.RemoveObserver(this);
    bunch.membersSorcerousUCMutSet.RemoveObserver(this);
    bunch.membersBaseOffenseUCMutSet.RemoveObserver(this);
    bunch.membersBaseDefenseUCMutSet.RemoveObserver(this);

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
  public void OnLightningChargingUCMutSetEffect(ILightningChargingUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitLightningChargingUCMutSetAddEffect(LightningChargingUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitLightningChargingUCMutSetRemoveEffect(LightningChargingUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitLightningChargingUCMutSetCreateEffect(LightningChargingUCMutSetCreateEffect effect) { }
  public void visitLightningChargingUCMutSetDeleteEffect(LightningChargingUCMutSetDeleteEffect effect) { }
  public void OnWanderAICapabilityUCMutSetEffect(IWanderAICapabilityUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitWanderAICapabilityUCMutSetAddEffect(WanderAICapabilityUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitWanderAICapabilityUCMutSetRemoveEffect(WanderAICapabilityUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitWanderAICapabilityUCMutSetCreateEffect(WanderAICapabilityUCMutSetCreateEffect effect) { }
  public void visitWanderAICapabilityUCMutSetDeleteEffect(WanderAICapabilityUCMutSetDeleteEffect effect) { }
  public void OnTemporaryCloneAICapabilityUCMutSetEffect(ITemporaryCloneAICapabilityUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitTemporaryCloneAICapabilityUCMutSetAddEffect(TemporaryCloneAICapabilityUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitTemporaryCloneAICapabilityUCMutSetRemoveEffect(TemporaryCloneAICapabilityUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitTemporaryCloneAICapabilityUCMutSetCreateEffect(TemporaryCloneAICapabilityUCMutSetCreateEffect effect) { }
  public void visitTemporaryCloneAICapabilityUCMutSetDeleteEffect(TemporaryCloneAICapabilityUCMutSetDeleteEffect effect) { }
  public void OnSummonAICapabilityUCMutSetEffect(ISummonAICapabilityUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitSummonAICapabilityUCMutSetAddEffect(SummonAICapabilityUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitSummonAICapabilityUCMutSetRemoveEffect(SummonAICapabilityUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitSummonAICapabilityUCMutSetCreateEffect(SummonAICapabilityUCMutSetCreateEffect effect) { }
  public void visitSummonAICapabilityUCMutSetDeleteEffect(SummonAICapabilityUCMutSetDeleteEffect effect) { }
  public void OnKamikazeAICapabilityUCMutSetEffect(IKamikazeAICapabilityUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitKamikazeAICapabilityUCMutSetAddEffect(KamikazeAICapabilityUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitKamikazeAICapabilityUCMutSetRemoveEffect(KamikazeAICapabilityUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitKamikazeAICapabilityUCMutSetCreateEffect(KamikazeAICapabilityUCMutSetCreateEffect effect) { }
  public void visitKamikazeAICapabilityUCMutSetDeleteEffect(KamikazeAICapabilityUCMutSetDeleteEffect effect) { }
  public void OnGuardAICapabilityUCMutSetEffect(IGuardAICapabilityUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitGuardAICapabilityUCMutSetAddEffect(GuardAICapabilityUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitGuardAICapabilityUCMutSetRemoveEffect(GuardAICapabilityUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitGuardAICapabilityUCMutSetCreateEffect(GuardAICapabilityUCMutSetCreateEffect effect) { }
  public void visitGuardAICapabilityUCMutSetDeleteEffect(GuardAICapabilityUCMutSetDeleteEffect effect) { }
  public void OnTimeCloneAICapabilityUCMutSetEffect(ITimeCloneAICapabilityUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitTimeCloneAICapabilityUCMutSetAddEffect(TimeCloneAICapabilityUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitTimeCloneAICapabilityUCMutSetRemoveEffect(TimeCloneAICapabilityUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitTimeCloneAICapabilityUCMutSetCreateEffect(TimeCloneAICapabilityUCMutSetCreateEffect effect) { }
  public void visitTimeCloneAICapabilityUCMutSetDeleteEffect(TimeCloneAICapabilityUCMutSetDeleteEffect effect) { }
  public void OnDoomedUCMutSetEffect(IDoomedUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitDoomedUCMutSetAddEffect(DoomedUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitDoomedUCMutSetRemoveEffect(DoomedUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitDoomedUCMutSetCreateEffect(DoomedUCMutSetCreateEffect effect) { }
  public void visitDoomedUCMutSetDeleteEffect(DoomedUCMutSetDeleteEffect effect) { }
  public void OnMiredUCMutSetEffect(IMiredUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitMiredUCMutSetAddEffect(MiredUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitMiredUCMutSetRemoveEffect(MiredUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitMiredUCMutSetCreateEffect(MiredUCMutSetCreateEffect effect) { }
  public void visitMiredUCMutSetDeleteEffect(MiredUCMutSetDeleteEffect effect) { }
  public void OnAttackAICapabilityUCMutSetEffect(IAttackAICapabilityUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitAttackAICapabilityUCMutSetAddEffect(AttackAICapabilityUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitAttackAICapabilityUCMutSetRemoveEffect(AttackAICapabilityUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitAttackAICapabilityUCMutSetCreateEffect(AttackAICapabilityUCMutSetCreateEffect effect) { }
  public void visitAttackAICapabilityUCMutSetDeleteEffect(AttackAICapabilityUCMutSetDeleteEffect effect) { }
  public void OnCounteringUCMutSetEffect(ICounteringUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitCounteringUCMutSetAddEffect(CounteringUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitCounteringUCMutSetRemoveEffect(CounteringUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitCounteringUCMutSetCreateEffect(CounteringUCMutSetCreateEffect effect) { }
  public void visitCounteringUCMutSetDeleteEffect(CounteringUCMutSetDeleteEffect effect) { }
  public void OnLightningChargedUCMutSetEffect(ILightningChargedUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitLightningChargedUCMutSetAddEffect(LightningChargedUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitLightningChargedUCMutSetRemoveEffect(LightningChargedUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitLightningChargedUCMutSetCreateEffect(LightningChargedUCMutSetCreateEffect effect) { }
  public void visitLightningChargedUCMutSetDeleteEffect(LightningChargedUCMutSetDeleteEffect effect) { }
  public void OnInvincibilityUCMutSetEffect(IInvincibilityUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitInvincibilityUCMutSetAddEffect(InvincibilityUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitInvincibilityUCMutSetRemoveEffect(InvincibilityUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitInvincibilityUCMutSetCreateEffect(InvincibilityUCMutSetCreateEffect effect) { }
  public void visitInvincibilityUCMutSetDeleteEffect(InvincibilityUCMutSetDeleteEffect effect) { }
  public void OnDefyingUCMutSetEffect(IDefyingUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitDefyingUCMutSetAddEffect(DefyingUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitDefyingUCMutSetRemoveEffect(DefyingUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitDefyingUCMutSetCreateEffect(DefyingUCMutSetCreateEffect effect) { }
  public void visitDefyingUCMutSetDeleteEffect(DefyingUCMutSetDeleteEffect effect) { }
  public void OnBideAICapabilityUCMutSetEffect(IBideAICapabilityUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitBideAICapabilityUCMutSetAddEffect(BideAICapabilityUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitBideAICapabilityUCMutSetRemoveEffect(BideAICapabilityUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitBideAICapabilityUCMutSetCreateEffect(BideAICapabilityUCMutSetCreateEffect effect) { }
  public void visitBideAICapabilityUCMutSetDeleteEffect(BideAICapabilityUCMutSetDeleteEffect effect) { }
  public void OnBaseMovementTimeUCMutSetEffect(IBaseMovementTimeUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitBaseMovementTimeUCMutSetAddEffect(BaseMovementTimeUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitBaseMovementTimeUCMutSetRemoveEffect(BaseMovementTimeUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitBaseMovementTimeUCMutSetCreateEffect(BaseMovementTimeUCMutSetCreateEffect effect) { }
  public void visitBaseMovementTimeUCMutSetDeleteEffect(BaseMovementTimeUCMutSetDeleteEffect effect) { }
  public void OnBaseCombatTimeUCMutSetEffect(IBaseCombatTimeUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitBaseCombatTimeUCMutSetAddEffect(BaseCombatTimeUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitBaseCombatTimeUCMutSetRemoveEffect(BaseCombatTimeUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitBaseCombatTimeUCMutSetCreateEffect(BaseCombatTimeUCMutSetCreateEffect effect) { }
  public void visitBaseCombatTimeUCMutSetDeleteEffect(BaseCombatTimeUCMutSetDeleteEffect effect) { }
  public void OnManaPotionMutSetEffect(IManaPotionMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitManaPotionMutSetAddEffect(ManaPotionMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitManaPotionMutSetRemoveEffect(ManaPotionMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitManaPotionMutSetCreateEffect(ManaPotionMutSetCreateEffect effect) { }
  public void visitManaPotionMutSetDeleteEffect(ManaPotionMutSetDeleteEffect effect) { }
  public void OnHealthPotionMutSetEffect(IHealthPotionMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitHealthPotionMutSetAddEffect(HealthPotionMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitHealthPotionMutSetRemoveEffect(HealthPotionMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitHealthPotionMutSetCreateEffect(HealthPotionMutSetCreateEffect effect) { }
  public void visitHealthPotionMutSetDeleteEffect(HealthPotionMutSetDeleteEffect effect) { }
  public void OnSpeedRingMutSetEffect(ISpeedRingMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitSpeedRingMutSetAddEffect(SpeedRingMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitSpeedRingMutSetRemoveEffect(SpeedRingMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitSpeedRingMutSetCreateEffect(SpeedRingMutSetCreateEffect effect) { }
  public void visitSpeedRingMutSetDeleteEffect(SpeedRingMutSetDeleteEffect effect) { }
  public void OnGlaiveMutSetEffect(IGlaiveMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitGlaiveMutSetAddEffect(GlaiveMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitGlaiveMutSetRemoveEffect(GlaiveMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitGlaiveMutSetCreateEffect(GlaiveMutSetCreateEffect effect) { }
  public void visitGlaiveMutSetDeleteEffect(GlaiveMutSetDeleteEffect effect) { }
  public void OnBlastRodMutSetEffect(IBlastRodMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitBlastRodMutSetAddEffect(BlastRodMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitBlastRodMutSetRemoveEffect(BlastRodMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitBlastRodMutSetCreateEffect(BlastRodMutSetCreateEffect effect) { }
  public void visitBlastRodMutSetDeleteEffect(BlastRodMutSetDeleteEffect effect) { }
  public void OnArmorMutSetEffect(IArmorMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitArmorMutSetAddEffect(ArmorMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitArmorMutSetRemoveEffect(ArmorMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitArmorMutSetCreateEffect(ArmorMutSetCreateEffect effect) { }
  public void visitArmorMutSetDeleteEffect(ArmorMutSetDeleteEffect effect) { }
  public void OnSorcerousUCMutSetEffect(ISorcerousUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitSorcerousUCMutSetAddEffect(SorcerousUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitSorcerousUCMutSetRemoveEffect(SorcerousUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitSorcerousUCMutSetCreateEffect(SorcerousUCMutSetCreateEffect effect) { }
  public void visitSorcerousUCMutSetDeleteEffect(SorcerousUCMutSetDeleteEffect effect) { }
  public void OnBaseOffenseUCMutSetEffect(IBaseOffenseUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitBaseOffenseUCMutSetAddEffect(BaseOffenseUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitBaseOffenseUCMutSetRemoveEffect(BaseOffenseUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitBaseOffenseUCMutSetCreateEffect(BaseOffenseUCMutSetCreateEffect effect) { }
  public void visitBaseOffenseUCMutSetDeleteEffect(BaseOffenseUCMutSetDeleteEffect effect) { }
  public void OnBaseDefenseUCMutSetEffect(IBaseDefenseUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitBaseDefenseUCMutSetAddEffect(BaseDefenseUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitBaseDefenseUCMutSetRemoveEffect(BaseDefenseUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitBaseDefenseUCMutSetCreateEffect(BaseDefenseUCMutSetCreateEffect effect) { }
  public void visitBaseDefenseUCMutSetDeleteEffect(BaseDefenseUCMutSetDeleteEffect effect) { }

}
       
}
