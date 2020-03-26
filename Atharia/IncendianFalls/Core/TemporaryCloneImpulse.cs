using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class TemporaryCloneImpulse {
  public readonly Root root;
  public readonly int id;
  public TemporaryCloneImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public TemporaryCloneImpulseIncarnation incarnation { get { return root.GetTemporaryCloneImpulseIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, ITemporaryCloneImpulseEffectObserver observer) {
    broadcaster.AddTemporaryCloneImpulseObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ITemporaryCloneImpulseEffectObserver observer) {
    broadcaster.RemoveTemporaryCloneImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectTemporaryCloneImpulseDelete(id);
  }
  public static TemporaryCloneImpulse Null = new TemporaryCloneImpulse(null, 0);
  public bool Exists() { return root != null && root.TemporaryCloneImpulseExists(id); }
  public bool NullableIs(TemporaryCloneImpulse that) {
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
  public bool Is(TemporaryCloneImpulse that) {
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
  public string blueprintName {
    get { return incarnation.blueprintName; }
  }
  public Location location {
    get { return incarnation.location; }
  }
  public int hp {
    get { return incarnation.hp; }
  }
}
}
