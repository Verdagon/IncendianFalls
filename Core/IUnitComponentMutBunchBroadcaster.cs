using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IUnitComponentMutBunchBroadcaster:IKillDirectiveUCMutSetEffectObserver, IKillDirectiveUCMutSetEffectVisitor, IMoveDirectiveUCMutSetEffectObserver, IMoveDirectiveUCMutSetEffectVisitor, IWanderAICapabilityUCMutSetEffectObserver, IWanderAICapabilityUCMutSetEffectVisitor, IBideAICapabilityUCMutSetEffectObserver, IBideAICapabilityUCMutSetEffectVisitor, IAttackAICapabilityUCMutSetEffectObserver, IAttackAICapabilityUCMutSetEffectVisitor, IShieldingUCMutSetEffectObserver, IShieldingUCMutSetEffectVisitor, IBidingOperationUCMutSetEffectObserver, IBidingOperationUCMutSetEffectVisitor {
  IUnitComponentMutBunch bunch;
  private List<IIUnitComponentMutBunchObserver> observers;

  public IUnitComponentMutBunchBroadcaster(IUnitComponentMutBunch bunch) {
    this.bunch = bunch;
    this.observers = new List<IIUnitComponentMutBunchObserver>();
    bunch.membersKillDirectiveUCMutSet.AddObserver(this);
    bunch.membersMoveDirectiveUCMutSet.AddObserver(this);
    bunch.membersWanderAICapabilityUCMutSet.AddObserver(this);
    bunch.membersBideAICapabilityUCMutSet.AddObserver(this);
    bunch.membersAttackAICapabilityUCMutSet.AddObserver(this);
    bunch.membersShieldingUCMutSet.AddObserver(this);
    bunch.membersBidingOperationUCMutSet.AddObserver(this);

  }
  public void Stop() {
    bunch.membersKillDirectiveUCMutSet.RemoveObserver(this);
    bunch.membersMoveDirectiveUCMutSet.RemoveObserver(this);
    bunch.membersWanderAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersBideAICapabilityUCMutSet.RemoveObserver(this);
    bunch.membersAttackAICapabilityUCMutSet.RemoveObserver(this);
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
