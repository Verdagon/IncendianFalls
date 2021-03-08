using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class EvaporateImpulse {
  public readonly Root root;
  public readonly int id;
  public EvaporateImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public EvaporateImpulseIncarnation incarnation { get { return root.GetEvaporateImpulseIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IEvaporateImpulseEffectObserver observer) {
    broadcaster.AddEvaporateImpulseObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IEvaporateImpulseEffectObserver observer) {
    broadcaster.RemoveEvaporateImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectEvaporateImpulseDelete(id);
  }
  public static EvaporateImpulse Null = new EvaporateImpulse(null, 0);
  public bool Exists() { return root != null && root.EvaporateImpulseExists(id); }
  public bool NullableIs(EvaporateImpulse that) {
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
  public bool Is(EvaporateImpulse that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
       }
}
