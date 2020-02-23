using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IUnitComponentMutBunchBroadcaster:IArmorMutSetEffectObserver, IArmorMutSetEffectVisitor, IInertiaRingMutSetEffectObserver, IInertiaRingMutSetEffectVisitor, IGlaiveMutSetEffectObserver, IGlaiveMutSetEffectVisitor, IManaPotionMutSetEffectObserver, IManaPotionMutSetEffectVisitor, IHealthPotionMutSetEffectObserver, IHealthPotionMutSetEffectVisitor, IWanderAICapabilityUCMutSetEffectObserver, IWanderAICapabilityUCMutSetEffectVisitor, ITimeCloneAICapabilityUCMutSetEffectObserver, ITimeCloneAICapabilityUCMutSetEffectVisitor, IAttackAICapabilityUCMutSetEffectObserver, IAttackAICapabilityUCMutSetEffectVisitor, ICounteringUCMutSetEffectObserver, ICounteringUCMutSetEffectVisitor, IShieldingUCMutSetEffectObserver, IShieldingUCMutSetEffectVisitor, IBideAICapabilityUCMutSetEffectObserver, IBideAICapabilityUCMutSetEffectVisitor {
  IUnitComponentMutBunch bunch;
  private List<IIUnitComponentMutBunchObserver> observers;

  public IUnitComponentMutBunchBroadcaster(IUnitComponentMutBunch bunch) {
    this.bunch = bunch;
    this.observers = new List<IIUnitComponentMutBunchObserver>();
    bunch.membersArmorMutSet.AddObserver(this);
    bunch.membersInertiaRingMutSet.AddObserver(this);
    bunch.membersGlaiveMutSet.AddObserver(this);
    bunch.membersManaPotionMutSet.AddObserver(this);
    bunch.membersHealthPotionMutSet.AddObserver(this);
    bunch.membersWanderAICapabilityUCMutSet.AddObserver(this);
    bunch.membersTimeCloneAICapabilityUCMutSet.AddObserver(this);
    bunch.membersAttackAICapabilityUCMutSet.AddObserver(this);
    bunch.membersCounteringUCMutSet.AddObserver(this);
    bunch.membersShieldingUCMutSet.AddObserver(this);
    bunch.membersBideAICapabilityUCMutSet.AddObserver(this);

  }
  public void Stop() {
    bunch.membersArmorMutSet.RemoveObserver(this);
    bunch.membersInertiaRingMutSet.RemoveObserver(this);
    bunch.membersGlaiveMutSet.RemoveObserver(this);
    bunch.membersManaPotionMutSet.RemoveObserver(this);
    bunch.membersHealthPotionMutSet.RemoveObserver(this);
    bunch.membersWanderAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersTimeCloneAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersAttackAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersCounteringUCMutSet.RemoveObserver(this);
    bunch.membersShieldingUCMutSet.RemoveObserver(this);
    bunch.membersBideAICapabilityUCMutSet.RemoveObserver(this);

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
  public void OnInertiaRingMutSetEffect(IInertiaRingMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitInertiaRingMutSetAddEffect(InertiaRingMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitInertiaRingMutSetRemoveEffect(InertiaRingMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitInertiaRingMutSetCreateEffect(InertiaRingMutSetCreateEffect effect) { }
  public void visitInertiaRingMutSetDeleteEffect(InertiaRingMutSetDeleteEffect effect) { }
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

}
       
}
