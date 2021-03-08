using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IPostActingUCWeakMutBunchBroadcaster:ILightningChargedUCWeakMutSetEffectObserver, ILightningChargedUCWeakMutSetEffectVisitor, ITimeCloneAICapabilityUCWeakMutSetEffectObserver, ITimeCloneAICapabilityUCWeakMutSetEffectVisitor {
  EffectBroadcaster broadcaster;
  IPostActingUCWeakMutBunch bunch;
  private List<IIPostActingUCWeakMutBunchObserver> observers;

  public IPostActingUCWeakMutBunchBroadcaster(EffectBroadcaster broadcaster, IPostActingUCWeakMutBunch bunch) {
    this.broadcaster = broadcaster;
    this.bunch = bunch;
    this.observers = new List<IIPostActingUCWeakMutBunchObserver>();
    bunch.membersLightningChargedUCWeakMutSet.AddObserver(broadcaster, this);
    bunch.membersTimeCloneAICapabilityUCWeakMutSet.AddObserver(broadcaster, this);

  }
  public void Stop() {
    bunch.membersLightningChargedUCWeakMutSet.RemoveObserver(broadcaster, this);
    bunch.membersTimeCloneAICapabilityUCWeakMutSet.RemoveObserver(broadcaster, this);

  }
  public void AddObserver(IIPostActingUCWeakMutBunchObserver observer) {
    this.observers.Add(observer);
  }
  public void RemoveObserver(IIPostActingUCWeakMutBunchObserver observer) {
    this.observers.Remove(observer);
  }
  private void BroadcastAdd(int id) {
    foreach (var observer in observers) {
      observer.OnIPostActingUCWeakMutBunchAdd(id);
    }
  }
  private void BroadcastRemove(int id) {
    foreach (var observer in observers) {
      observer.OnIPostActingUCWeakMutBunchRemove(id);
    }
  }
  public void OnLightningChargedUCWeakMutSetEffect(ILightningChargedUCWeakMutSetEffect effect) {
    effect.visitILightningChargedUCWeakMutSetEffect(this);
  }
  public void visitLightningChargedUCWeakMutSetAddEffect(LightningChargedUCWeakMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitLightningChargedUCWeakMutSetRemoveEffect(LightningChargedUCWeakMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitLightningChargedUCWeakMutSetCreateEffect(LightningChargedUCWeakMutSetCreateEffect effect) { }
  public void visitLightningChargedUCWeakMutSetDeleteEffect(LightningChargedUCWeakMutSetDeleteEffect effect) { }
  public void OnTimeCloneAICapabilityUCWeakMutSetEffect(ITimeCloneAICapabilityUCWeakMutSetEffect effect) {
    effect.visitITimeCloneAICapabilityUCWeakMutSetEffect(this);
  }
  public void visitTimeCloneAICapabilityUCWeakMutSetAddEffect(TimeCloneAICapabilityUCWeakMutSetAddEffect effect) {
    BroadcastAdd(effect.element);
  }
  public void visitTimeCloneAICapabilityUCWeakMutSetRemoveEffect(TimeCloneAICapabilityUCWeakMutSetRemoveEffect effect) {
    BroadcastRemove(effect.element);
  }
  public void visitTimeCloneAICapabilityUCWeakMutSetCreateEffect(TimeCloneAICapabilityUCWeakMutSetCreateEffect effect) { }
  public void visitTimeCloneAICapabilityUCWeakMutSetDeleteEffect(TimeCloneAICapabilityUCWeakMutSetDeleteEffect effect) { }

}
       
}
