using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class EvolvifyImpulse {
  public readonly Root root;
  public readonly int id;
  public EvolvifyImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public EvolvifyImpulseIncarnation incarnation { get { return root.GetEvolvifyImpulseIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IEvolvifyImpulseEffectObserver observer) {
    broadcaster.AddEvolvifyImpulseObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IEvolvifyImpulseEffectObserver observer) {
    broadcaster.RemoveEvolvifyImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectEvolvifyImpulseDelete(id);
  }
  public static EvolvifyImpulse Null = new EvolvifyImpulse(null, 0);
  public bool Exists() { return root != null && root.EvolvifyImpulseExists(id); }
  public bool NullableIs(EvolvifyImpulse that) {
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
  public bool Is(EvolvifyImpulse that) {
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
  public Location moveToLocation {
    get { return incarnation.moveToLocation; }
  }
}
}
