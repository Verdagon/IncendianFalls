using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class OnFireTTC {
  public readonly Root root;
  public readonly int id;
  public OnFireTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public OnFireTTCIncarnation incarnation { get { return root.GetOnFireTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IOnFireTTCEffectObserver observer) {
    broadcaster.AddOnFireTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IOnFireTTCEffectObserver observer) {
    broadcaster.RemoveOnFireTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectOnFireTTCDelete(id);
  }
  public static OnFireTTC Null = new OnFireTTC(null, 0);
  public bool Exists() { return root != null && root.OnFireTTCExists(id); }
  public bool NullableIs(OnFireTTC that) {
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
  public bool Is(OnFireTTC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int turnsRemaining {
    get { return incarnation.turnsRemaining; }
    set { root.EffectOnFireTTCSetTurnsRemaining(id, value); }
  }
}
}
