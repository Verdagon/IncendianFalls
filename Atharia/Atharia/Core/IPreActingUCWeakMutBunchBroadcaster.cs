using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IPreActingUCWeakMutBunchBroadcaster:IDoomedUCWeakMutSetEffectObserver, IDoomedUCWeakMutSetEffectVisitor, IMiredUCWeakMutSetEffectObserver, IMiredUCWeakMutSetEffectVisitor, IInvincibilityUCWeakMutSetEffectObserver, IInvincibilityUCWeakMutSetEffectVisitor, IOnFireUCWeakMutSetEffectObserver, IOnFireUCWeakMutSetEffectVisitor, IDefyingUCWeakMutSetEffectObserver, IDefyingUCWeakMutSetEffectVisitor, ICounteringUCWeakMutSetEffectObserver, ICounteringUCWeakMutSetEffectVisitor, IAttackAICapabilityUCWeakMutSetEffectObserver, IAttackAICapabilityUCWeakMutSetEffectVisitor {
  EffectBroadcaster broadcaster;
  IPreActingUCWeakMutBunch bunch;
  private List<IIPreActingUCWeakMutBunchObserver> observers;

  public IPreActingUCWeakMutBunchBroadcaster(EffectBroadcaster broadcaster, IPreActingUCWeakMutBunch bunch) {
    this.broadcaster = broadcaster;
    this.bunch = bunch;
    this.observers = new List<IIPreActingUCWeakMutBunchObserver>();
    bunch.membersDoomedUCWeakMutSet.AddObserver(broadcaster, this);
    bunch.membersMiredUCWeakMutSet.AddObserver(broadcaster, this);
    bunch.membersInvincibilityUCWeakMutSet.AddObserver(broadcaster, this);
    bunch.membersOnFireUCWeakMutSet.AddObserver(broadcaster, this);
    bunch.membersDefyingUCWeakMutSet.AddObserver(broadcaster, this);
    bunch.membersCounteringUCWeakMutSet.AddObserver(broadcaster, this);
    bunch.membersAttackAICapabilityUCWeakMutSet.AddObserver(broadcaster, this);

  }
  public void Stop() {
    bunch.membersDoomedUCWeakMutSet.RemoveObserver(broadcaster, this);
    bunch.membersMiredUCWeakMutSet.RemoveObserver(broadcaster, this);
    bunch.membersInvincibilityUCWeakMutSet.RemoveObserver(broadcaster, this);
    bunch.membersOnFireUCWeakMutSet.RemoveObserver(broadcaster, this);
    bunch.membersDefyingUCWeakMutSet.RemoveObserver(broadcaster, this);
    bunch.membersCounteringUCWeakMutSet.RemoveObserver(broadcaster, this);
    bunch.membersAttackAICapabilityUCWeakMutSet.RemoveObserver(broadcaster, this);

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
    effect.visitIDoomedUCWeakMutSetEffect(this);
  }
  public void visitDoomedUCWeakMutSetAddEffect(DoomedUCWeakMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitDoomedUCWeakMutSetRemoveEffect(DoomedUCWeakMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitDoomedUCWeakMutSetCreateEffect(DoomedUCWeakMutSetCreateEffect effect) { }
  public void visitDoomedUCWeakMutSetDeleteEffect(DoomedUCWeakMutSetDeleteEffect effect) { }
  public void OnMiredUCWeakMutSetEffect(IMiredUCWeakMutSetEffect effect) {
    effect.visitIMiredUCWeakMutSetEffect(this);
  }
  public void visitMiredUCWeakMutSetAddEffect(MiredUCWeakMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitMiredUCWeakMutSetRemoveEffect(MiredUCWeakMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitMiredUCWeakMutSetCreateEffect(MiredUCWeakMutSetCreateEffect effect) { }
  public void visitMiredUCWeakMutSetDeleteEffect(MiredUCWeakMutSetDeleteEffect effect) { }
  public void OnInvincibilityUCWeakMutSetEffect(IInvincibilityUCWeakMutSetEffect effect) {
    effect.visitIInvincibilityUCWeakMutSetEffect(this);
  }
  public void visitInvincibilityUCWeakMutSetAddEffect(InvincibilityUCWeakMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitInvincibilityUCWeakMutSetRemoveEffect(InvincibilityUCWeakMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitInvincibilityUCWeakMutSetCreateEffect(InvincibilityUCWeakMutSetCreateEffect effect) { }
  public void visitInvincibilityUCWeakMutSetDeleteEffect(InvincibilityUCWeakMutSetDeleteEffect effect) { }
  public void OnOnFireUCWeakMutSetEffect(IOnFireUCWeakMutSetEffect effect) {
    effect.visitIOnFireUCWeakMutSetEffect(this);
  }
  public void visitOnFireUCWeakMutSetAddEffect(OnFireUCWeakMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitOnFireUCWeakMutSetRemoveEffect(OnFireUCWeakMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitOnFireUCWeakMutSetCreateEffect(OnFireUCWeakMutSetCreateEffect effect) { }
  public void visitOnFireUCWeakMutSetDeleteEffect(OnFireUCWeakMutSetDeleteEffect effect) { }
  public void OnDefyingUCWeakMutSetEffect(IDefyingUCWeakMutSetEffect effect) {
    effect.visitIDefyingUCWeakMutSetEffect(this);
  }
  public void visitDefyingUCWeakMutSetAddEffect(DefyingUCWeakMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitDefyingUCWeakMutSetRemoveEffect(DefyingUCWeakMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitDefyingUCWeakMutSetCreateEffect(DefyingUCWeakMutSetCreateEffect effect) { }
  public void visitDefyingUCWeakMutSetDeleteEffect(DefyingUCWeakMutSetDeleteEffect effect) { }
  public void OnCounteringUCWeakMutSetEffect(ICounteringUCWeakMutSetEffect effect) {
    effect.visitICounteringUCWeakMutSetEffect(this);
  }
  public void visitCounteringUCWeakMutSetAddEffect(CounteringUCWeakMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitCounteringUCWeakMutSetRemoveEffect(CounteringUCWeakMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitCounteringUCWeakMutSetCreateEffect(CounteringUCWeakMutSetCreateEffect effect) { }
  public void visitCounteringUCWeakMutSetDeleteEffect(CounteringUCWeakMutSetDeleteEffect effect) { }
  public void OnAttackAICapabilityUCWeakMutSetEffect(IAttackAICapabilityUCWeakMutSetEffect effect) {
    effect.visitIAttackAICapabilityUCWeakMutSetEffect(this);
  }
  public void visitAttackAICapabilityUCWeakMutSetAddEffect(AttackAICapabilityUCWeakMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitAttackAICapabilityUCWeakMutSetRemoveEffect(AttackAICapabilityUCWeakMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitAttackAICapabilityUCWeakMutSetCreateEffect(AttackAICapabilityUCWeakMutSetCreateEffect effect) { }
  public void visitAttackAICapabilityUCWeakMutSetDeleteEffect(AttackAICapabilityUCWeakMutSetDeleteEffect effect) { }

}
       
}
