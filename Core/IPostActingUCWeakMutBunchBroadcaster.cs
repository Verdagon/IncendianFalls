using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IPostActingUCWeakMutBunchBroadcaster:ITimeCloneAICapabilityUCWeakMutSetEffectObserver, ITimeCloneAICapabilityUCWeakMutSetEffectVisitor {
  IPostActingUCWeakMutBunch bunch;
  private List<IIPostActingUCWeakMutBunchObserver> observers;

  public IPostActingUCWeakMutBunchBroadcaster(IPostActingUCWeakMutBunch bunch) {
    this.bunch = bunch;
    this.observers = new List<IIPostActingUCWeakMutBunchObserver>();
    bunch.membersTimeCloneAICapabilityUCWeakMutSet.AddObserver(this);

  }
  public void Stop() {
    bunch.membersTimeCloneAICapabilityUCWeakMutSet.RemoveObserver(this);

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
  public void OnTimeCloneAICapabilityUCWeakMutSetEffect(ITimeCloneAICapabilityUCWeakMutSetEffect effect) {
    effect.visit(this);
  }
  public void visitTimeCloneAICapabilityUCWeakMutSetAddEffect(TimeCloneAICapabilityUCWeakMutSetAddEffect effect) {
    BroadcastAdd(effect.elementId);
  }
  public void visitTimeCloneAICapabilityUCWeakMutSetRemoveEffect(TimeCloneAICapabilityUCWeakMutSetRemoveEffect effect) {
    BroadcastRemove(effect.elementId);
  }
  public void visitTimeCloneAICapabilityUCWeakMutSetCreateEffect(TimeCloneAICapabilityUCWeakMutSetCreateEffect effect) { }
  public void visitTimeCloneAICapabilityUCWeakMutSetDeleteEffect(TimeCloneAICapabilityUCWeakMutSetDeleteEffect effect) { }

}
       
}
