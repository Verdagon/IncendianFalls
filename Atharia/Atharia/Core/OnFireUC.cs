using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class OnFireUC {
  public readonly Root root;
  public readonly int id;
  public OnFireUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public OnFireUCIncarnation incarnation { get { return root.GetOnFireUCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IOnFireUCEffectObserver observer) {
    broadcaster.AddOnFireUCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IOnFireUCEffectObserver observer) {
    broadcaster.RemoveOnFireUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectOnFireUCDelete(id);
  }
  public static OnFireUC Null = new OnFireUC(null, 0);
  public bool Exists() { return root != null && root.OnFireUCExists(id); }
  public bool NullableIs(OnFireUC that) {
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
  public bool Is(OnFireUC that) {
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
    set { root.EffectOnFireUCSetTurnsRemaining(id, value); }
  }
}
}
