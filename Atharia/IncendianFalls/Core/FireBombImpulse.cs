using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class FireBombImpulse {
  public readonly Root root;
  public readonly int id;
  public FireBombImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public FireBombImpulseIncarnation incarnation { get { return root.GetFireBombImpulseIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IFireBombImpulseEffectObserver observer) {
    broadcaster.AddFireBombImpulseObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IFireBombImpulseEffectObserver observer) {
    broadcaster.RemoveFireBombImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectFireBombImpulseDelete(id);
  }
  public static FireBombImpulse Null = new FireBombImpulse(null, 0);
  public bool Exists() { return root != null && root.FireBombImpulseExists(id); }
  public bool NullableIs(FireBombImpulse that) {
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
  public bool Is(FireBombImpulse that) {
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
  public Location location {
    get { return incarnation.location; }
  }
}
}
