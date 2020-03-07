using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IPreActingUCWeakMutBunchBroadcaster:IShieldingUCWeakMutSetEffectObserver, IShieldingUCWeakMutSetEffectVisitor, ICounteringUCWeakMutSetEffectObserver, ICounteringUCWeakMutSetEffectVisitor, IAttackAICapabilityUCWeakMutSetEffectObserver, IAttackAICapabilityUCWeakMutSetEffectVisitor {
  IPreActingUCWeakMutBunch bunch;
  private List<IIPreActingUCWeakMutBunchObserver> observers;

  public IPreActingUCWeakMutBunchBroadcaster(IPreActingUCWeakMutBunch bunch) {
    this.bunch = bunch;
    this.observers = new List<IIPreActingUCWeakMutBunchObserver>();
    bunch.membersShieldingUCWeakMutSet.AddObserver(this);
    bunch.membersCounteringUCWeakMutSet.AddObserver(this);
    bunch.membersAttackAICapabilityUCWeakMutSet.AddObserver(this);

  }
  public void Stop() {
    bunch.membersShieldingUCWeakMutSet.RemoveObserver(this);
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
  public void OnShieldingUCWeakMutSetEffect(IShieldingUCWeakMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitShieldingUCWeakMutSetAddEffect(ShieldingUCWeakMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitShieldingUCWeakMutSetRemoveEffect(ShieldingUCWeakMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitShieldingUCWeakMutSetCreateEffect(ShieldingUCWeakMutSetCreateEffect effect) { }
  public void visitShieldingUCWeakMutSetDeleteEffect(ShieldingUCWeakMutSetDeleteEffect effect) { }
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
