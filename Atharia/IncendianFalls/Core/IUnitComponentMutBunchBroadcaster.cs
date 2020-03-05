using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IUnitComponentMutBunchBroadcaster:IManaPotionMutSetEffectObserver, IManaPotionMutSetEffectVisitor, IHealthPotionMutSetEffectObserver, IHealthPotionMutSetEffectVisitor, IBaseCombatTimeUCMutSetEffectObserver, IBaseCombatTimeUCMutSetEffectVisitor, ISpeedRingMutSetEffectObserver, ISpeedRingMutSetEffectVisitor, IBaseMovementTimeUCMutSetEffectObserver, IBaseMovementTimeUCMutSetEffectVisitor, IGlaiveMutSetEffectObserver, IGlaiveMutSetEffectVisitor, IArmorMutSetEffectObserver, IArmorMutSetEffectVisitor, IWanderAICapabilityUCMutSetEffectObserver, IWanderAICapabilityUCMutSetEffectVisitor, ISummonAICapabilityUCMutSetEffectObserver, ISummonAICapabilityUCMutSetEffectVisitor, IBideAICapabilityUCMutSetEffectObserver, IBideAICapabilityUCMutSetEffectVisitor, ITimeCloneAICapabilityUCMutSetEffectObserver, ITimeCloneAICapabilityUCMutSetEffectVisitor, IShieldingUCMutSetEffectObserver, IShieldingUCMutSetEffectVisitor, IAttackAICapabilityUCMutSetEffectObserver, IAttackAICapabilityUCMutSetEffectVisitor, ICounteringUCMutSetEffectObserver, ICounteringUCMutSetEffectVisitor, ISorcerousUCMutSetEffectObserver, ISorcerousUCMutSetEffectVisitor, IBaseOffenseUCMutSetEffectObserver, IBaseOffenseUCMutSetEffectVisitor, IBaseDefenseUCMutSetEffectObserver, IBaseDefenseUCMutSetEffectVisitor {
  IUnitComponentMutBunch bunch;
  private List<IIUnitComponentMutBunchObserver> observers;

  public IUnitComponentMutBunchBroadcaster(IUnitComponentMutBunch bunch) {
    this.bunch = bunch;
    this.observers = new List<IIUnitComponentMutBunchObserver>();
    bunch.membersManaPotionMutSet.AddObserver(this);
    bunch.membersHealthPotionMutSet.AddObserver(this);
    bunch.membersBaseCombatTimeUCMutSet.AddObserver(this);
    bunch.membersSpeedRingMutSet.AddObserver(this);
    bunch.membersBaseMovementTimeUCMutSet.AddObserver(this);
    bunch.membersGlaiveMutSet.AddObserver(this);
    bunch.membersArmorMutSet.AddObserver(this);
    bunch.membersWanderAICapabilityUCMutSet.AddObserver(this);
    bunch.membersSummonAICapabilityUCMutSet.AddObserver(this);
    bunch.membersBideAICapabilityUCMutSet.AddObserver(this);
    bunch.membersTimeCloneAICapabilityUCMutSet.AddObserver(this);
    bunch.membersShieldingUCMutSet.AddObserver(this);
    bunch.membersAttackAICapabilityUCMutSet.AddObserver(this);
    bunch.membersCounteringUCMutSet.AddObserver(this);
    bunch.membersSorcerousUCMutSet.AddObserver(this);
    bunch.membersBaseOffenseUCMutSet.AddObserver(this);
    bunch.membersBaseDefenseUCMutSet.AddObserver(this);

  }
  public void Stop() {
    bunch.membersManaPotionMutSet.RemoveObserver(this);
    bunch.membersHealthPotionMutSet.RemoveObserver(this);
    bunch.membersBaseCombatTimeUCMutSet.RemoveObserver(this);
    bunch.membersSpeedRingMutSet.RemoveObserver(this);
    bunch.membersBaseMovementTimeUCMutSet.RemoveObserver(this);
    bunch.membersGlaiveMutSet.RemoveObserver(this);
    bunch.membersArmorMutSet.RemoveObserver(this);
    bunch.membersWanderAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersSummonAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersBideAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersTimeCloneAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersShieldingUCMutSet.RemoveObserver(this);
    bunch.membersAttackAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersCounteringUCMutSet.RemoveObserver(this);
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
  public void OnShieldingUCMutSetEffect(IShieldingUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitShieldingUCMutSetAddEffect(ShieldingUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitShieldingUCMutSetRemoveEffect(ShieldingUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitShieldingUCMutSetCreateEffect(ShieldingUCMutSetCreateEffect effect) { }
  public void visitShieldingUCMutSetDeleteEffect(ShieldingUCMutSetDeleteEffect effect) { }
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
