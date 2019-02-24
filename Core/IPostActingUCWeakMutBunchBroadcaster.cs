using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IPostActingUCWeakMutBunchBroadcaster:IShieldingUCWeakMutSetEffectObserver, IShieldingUCWeakMutSetEffectVisitor {
  IPostActingUCWeakMutBunch bunch;
  private List<IIPostActingUCWeakMutBunchObserver> observers;

  public IPostActingUCWeakMutBunchBroadcaster(IPostActingUCWeakMutBunch bunch) {
    this.bunch = bunch;
    this.observers = new List<IIPostActingUCWeakMutBunchObserver>();
    bunch.membersShieldingUCWeakMutSet.AddObserver(this);

  }
  public void Stop() {
    bunch.membersShieldingUCWeakMutSet.RemoveObserver(this);

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

}
       
}
