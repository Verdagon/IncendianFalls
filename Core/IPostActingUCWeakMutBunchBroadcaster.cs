using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IPostActingUCWeakMutBunchBroadcaster {
  IPostActingUCWeakMutBunch bunch;
  private List<IIPostActingUCWeakMutBunchObserver> observers;

  public IPostActingUCWeakMutBunchBroadcaster(IPostActingUCWeakMutBunch bunch) {
    this.bunch = bunch;
    this.observers = new List<IIPostActingUCWeakMutBunchObserver>();

  }
  public void Stop() {

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

}
       
}
