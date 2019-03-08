using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IUnitComponentMutBunchBroadcaster:IArmorMutSetEffectObserver, IArmorMutSetEffectVisitor, IInertiaRingMutSetEffectObserver, IInertiaRingMutSetEffectVisitor, IGlaiveMutSetEffectObserver, IGlaiveMutSetEffectVisitor, IManaPotionMutSetEffectObserver, IManaPotionMutSetEffectVisitor, IHealthPotionMutSetEffectObserver, IHealthPotionMutSetEffectVisitor, ITimeScriptDirectiveUCMutSetEffectObserver, ITimeScriptDirectiveUCMutSetEffectVisitor, IKillDirectiveUCMutSetEffectObserver, IKillDirectiveUCMutSetEffectVisitor, IMoveDirectiveUCMutSetEffectObserver, IMoveDirectiveUCMutSetEffectVisitor, IWanderAICapabilityUCMutSetEffectObserver, IWanderAICapabilityUCMutSetEffectVisitor, IBideAICapabilityUCMutSetEffectObserver, IBideAICapabilityUCMutSetEffectVisitor, ITimeCloneAICapabilityUCMutSetEffectObserver, ITimeCloneAICapabilityUCMutSetEffectVisitor, IAttackAICapabilityUCMutSetEffectObserver, IAttackAICapabilityUCMutSetEffectVisitor, ICounteringUCMutSetEffectObserver, ICounteringUCMutSetEffectVisitor, IShieldingUCMutSetEffectObserver, IShieldingUCMutSetEffectVisitor, IBidingOperationUCMutSetEffectObserver, IBidingOperationUCMutSetEffectVisitor {
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
    bunch.membersTimeScriptDirectiveUCMutSet.AddObserver(this);
    bunch.membersKillDirectiveUCMutSet.AddObserver(this);
    bunch.membersMoveDirectiveUCMutSet.AddObserver(this);
    bunch.membersWanderAICapabilityUCMutSet.AddObserver(this);
    bunch.membersBideAICapabilityUCMutSet.AddObserver(this);
    bunch.membersTimeCloneAICapabilityUCMutSet.AddObserver(this);
    bunch.membersAttackAICapabilityUCMutSet.AddObserver(this);
    bunch.membersCounteringUCMutSet.AddObserver(this);
    bunch.membersShieldingUCMutSet.AddObserver(this);
    bunch.membersBidingOperationUCMutSet.AddObserver(this);

  }
  public void Stop() {
    bunch.membersArmorMutSet.RemoveObserver(this);
    bunch.membersInertiaRingMutSet.RemoveObserver(this);
    bunch.membersGlaiveMutSet.RemoveObserver(this);
    bunch.membersManaPotionMutSet.RemoveObserver(this);
    bunch.membersHealthPotionMutSet.RemoveObserver(this);
    bunch.membersTimeScriptDirectiveUCMutSet.RemoveObserver(this);
    bunch.membersKillDirectiveUCMutSet.RemoveObserver(this);
    bunch.membersMoveDirectiveUCMutSet.RemoveObserver(this);
    bunch.membersWanderAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersBideAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersTimeCloneAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersAttackAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersCounteringUCMutSet.RemoveObserver(this);
    bunch.membersShieldingUCMutSet.RemoveObserver(this);
    bunch.membersBidingOperationUCMutSet.RemoveObserver(this);

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
  public void OnTimeScriptDirectiveUCMutSetEffect(ITimeScriptDirectiveUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitTimeScriptDirectiveUCMutSetAddEffect(TimeScriptDirectiveUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitTimeScriptDirectiveUCMutSetRemoveEffect(TimeScriptDirectiveUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitTimeScriptDirectiveUCMutSetCreateEffect(TimeScriptDirectiveUCMutSetCreateEffect effect) { }
  public void visitTimeScriptDirectiveUCMutSetDeleteEffect(TimeScriptDirectiveUCMutSetDeleteEffect effect) { }
  public void OnKillDirectiveUCMutSetEffect(IKillDirectiveUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitKillDirectiveUCMutSetAddEffect(KillDirectiveUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitKillDirectiveUCMutSetRemoveEffect(KillDirectiveUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitKillDirectiveUCMutSetCreateEffect(KillDirectiveUCMutSetCreateEffect effect) { }
  public void visitKillDirectiveUCMutSetDeleteEffect(KillDirectiveUCMutSetDeleteEffect effect) { }
  public void OnMoveDirectiveUCMutSetEffect(IMoveDirectiveUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitMoveDirectiveUCMutSetAddEffect(MoveDirectiveUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitMoveDirectiveUCMutSetRemoveEffect(MoveDirectiveUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitMoveDirectiveUCMutSetCreateEffect(MoveDirectiveUCMutSetCreateEffect effect) { }
  public void visitMoveDirectiveUCMutSetDeleteEffect(MoveDirectiveUCMutSetDeleteEffect effect) { }
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
  public void OnBidingOperationUCMutSetEffect(IBidingOperationUCMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitBidingOperationUCMutSetAddEffect(BidingOperationUCMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitBidingOperationUCMutSetRemoveEffect(BidingOperationUCMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitBidingOperationUCMutSetCreateEffect(BidingOperationUCMutSetCreateEffect effect) { }
  public void visitBidingOperationUCMutSetDeleteEffect(BidingOperationUCMutSetDeleteEffect effect) { }

}
       
}
