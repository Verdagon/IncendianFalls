using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class CounterImpulse {
  public readonly Root root;
  public readonly int id;
  public CounterImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public CounterImpulseIncarnation incarnation { get { return root.GetCounterImpulseIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, ICounterImpulseEffectObserver observer) {
    broadcaster.AddCounterImpulseObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ICounterImpulseEffectObserver observer) {
    broadcaster.RemoveCounterImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectCounterImpulseDelete(id);
  }
  public static CounterImpulse Null = new CounterImpulse(null, 0);
  public bool Exists() { return root != null && root.CounterImpulseExists(id); }
  public bool NullableIs(CounterImpulse that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
  }
  public bool Is(CounterImpulse that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int weight {
    get { return incarnation.weight; }
  }
}
}
