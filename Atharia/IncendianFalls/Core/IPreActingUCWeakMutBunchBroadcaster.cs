using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IPreActingUCWeakMutBunchBroadcaster:IDoomedUCWeakMutSetEffectObserver, IDoomedUCWeakMutSetEffectVisitor, IMiredUCWeakMutSetEffectObserver, IMiredUCWeakMutSetEffectVisitor, IInvincibilityUCWeakMutSetEffectObserver, IInvincibilityUCWeakMutSetEffectVisitor, IDefyingUCWeakMutSetEffectObserver, IDefyingUCWeakMutSetEffectVisitor, ICounteringUCWeakMutSetEffectObserver, ICounteringUCWeakMutSetEffectVisitor, IAttackAICapabilityUCWeakMutSetEffectObserver, IAttackAICapabilityUCWeakMutSetEffectVisitor {
  IPreActingUCWeakMutBunch bunch;
  private List<IIPreActingUCWeakMutBunchObserver> observers;

  public IPreActingUCWeakMutBunchBroadcaster(IPreActingUCWeakMutBunch bunch) {
    this.bunch = bunch;
    this.observers = new List<IIPreActingUCWeakMutBunchObserver>();
    bunch.membersDoomedUCWeakMutSet.AddObserver(this);
    bunch.membersMiredUCWeakMutSet.AddObserver(this);
    bunch.membersInvincibilityUCWeakMutSet.AddObserver(this);
    bunch.membersDefyingUCWeakMutSet.AddObserver(this);
    bunch.membersCounteringUCWeakMutSet.AddObserver(this);
    bunch.membersAttackAICapabilityUCWeakMutSet.AddObserver(this);

  }
  public void Stop() {
    bunch.membersDoomedUCWeakMutSet.RemoveObserver(this);
    bunch.membersMiredUCWeakMutSet.RemoveObserver(this);
    bunch.membersInvincibilityUCWeakMutSet.RemoveObserver(this);
    bunch.membersDefyingUCWeakMutSet.RemoveObserver(this);
    bunch.membersCounteringUCWeakMutSet.RemoveObserver(this);
    bunch.membersAttackAICapabilityUCWeakMutSet.RemoveObserver(this);

  }
  public void AddObserver(IIPreActingUCWeakMutBunchObserver observer) {
    this.observers.Add(observer);
  }
  public void RemoveObserver(IIPreActingUCWeakMutBunchObserver observer) {
    this.observers.Remove(observer);
  }
  private void BroadcastAdd(int id) {
    foreach (var observer in observers) {
      observer.OnIPreActingUCWeakMutBunchAdd(id);
    }
  }
  private void BroadcastRemove(int id) {
    foreach (var observer in observers) {
      observer.OnIPreActingUCWeakMutBunchRemove(id);
    }
  }
  public void OnDoomedUCWeakMutSetEffect(IDoomedUCWeakMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitDoomedUCWeakMutSetAddEffect(DoomedUCWeakMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitDoomedUCWeakMutSetRemoveEffect(DoomedUCWeakMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitDoomedUCWeakMutSetCreateEffect(DoomedUCWeakMutSetCreateEffect effect) { }
  public void visitDoomedUCWeakMutSetDeleteEffect(DoomedUCWeakMutSetDeleteEffect effect) { }
  public void OnMiredUCWeakMutSetEffect(IMiredUCWeakMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitMiredUCWeakMutSetAddEffect(MiredUCWeakMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitMiredUCWeakMutSetRemoveEffect(MiredUCWeakMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitMiredUCWeakMutSetCreateEffect(MiredUCWeakMutSetCreateEffect effect) { }
  public void visitMiredUCWeakMutSetDeleteEffect(MiredUCWeakMutSetDeleteEffect effect) { }
  public void OnInvincibilityUCWeakMutSetEffect(IInvincibilityUCWeakMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitInvincibilityUCWeakMutSetAddEffect(InvincibilityUCWeakMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitInvincibilityUCWeakMutSetRemoveEffect(InvincibilityUCWeakMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitInvincibilityUCWeakMutSetCreateEffect(InvincibilityUCWeakMutSetCreateEffect effect) { }
  public void visitInvincibilityUCWeakMutSetDeleteEffect(InvincibilityUCWeakMutSetDeleteEffect effect) { }
  public void OnDefyingUCWeakMutSetEffect(IDefyingUCWeakMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitDefyingUCWeakMutSetAddEffect(DefyingUCWeakMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitDefyingUCWeakMutSetRemoveEffect(DefyingUCWeakMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitDefyingUCWeakMutSetCreateEffect(DefyingUCWeakMutSetCreateEffect effect) { }
  public void visitDefyingUCWeakMutSetDeleteEffect(DefyingUCWeakMutSetDeleteEffect effect) { }
  public void OnCounteringUCWeakMutSetEffect(ICounteringUCWeakMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitCounteringUCWeakMutSetAddEffect(CounteringUCWeakMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitCounteringUCWeakMutSetRemoveEffect(CounteringUCWeakMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitCounteringUCWeakMutSetCreateEffect(CounteringUCWeakMutSetCreateEffect effect) { }
  public void visitCounteringUCWeakMutSetDeleteEffect(CounteringUCWeakMutSetDeleteEffect effect) { }
  public void OnAttackAICapabilityUCWeakMutSetEffect(IAttackAICapabilityUCWeakMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitAttackAICapabilityUCWeakMutSetAddEffect(AttackAICapabilityUCWeakMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitAttackAICapabilityUCWeakMutSetRemoveEffect(AttackAICapabilityUCWeakMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitAttackAICapabilityUCWeakMutSetCreateEffect(AttackAICapabilityUCWeakMutSetCreateEffect effect) { }
  public void visitAttackAICapabilityUCWeakMutSetDeleteEffect(AttackAICapabilityUCWeakMutSetDeleteEffect effect) { }

}
       
}
