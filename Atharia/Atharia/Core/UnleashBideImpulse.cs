using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UnleashBideImpulse {
  public readonly Root root;
  public readonly int id;
  public UnleashBideImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public UnleashBideImpulseIncarnation incarnation { get { return root.GetUnleashBideImpulseIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IUnleashBideImpulseEffectObserver observer) {
    broadcaster.AddUnleashBideImpulseObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IUnleashBideImpulseEffectObserver observer) {
    broadcaster.RemoveUnleashBideImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectUnleashBideImpulseDelete(id);
  }
  public static UnleashBideImpulse Null = new UnleashBideImpulse(null, 0);
  public bool Exists() { return root != null && root.UnleashBideImpulseExists(id); }
  public bool NullableIs(UnleashBideImpulse that) {
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
  public bool Is(UnleashBideImpulse that) {
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
