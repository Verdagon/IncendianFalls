using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class PursueImpulse {
  public readonly Root root;
  public readonly int id;
  public PursueImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public PursueImpulseIncarnation incarnation { get { return root.GetPursueImpulseIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IPursueImpulseEffectObserver observer) {
    broadcaster.AddPursueImpulseObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IPursueImpulseEffectObserver observer) {
    broadcaster.RemovePursueImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectPursueImpulseDelete(id);
  }
  public static PursueImpulse Null = new PursueImpulse(null, 0);
  public bool Exists() { return root != null && root.PursueImpulseExists(id); }
  public bool NullableIs(PursueImpulse that) {
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
  public bool Is(PursueImpulse that) {
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
  public bool isClearPath {
    get { return incarnation.isClearPath; }
  }
}
}
