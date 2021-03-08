using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class Glaive {
  public readonly Root root;
  public readonly int id;
  public Glaive(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public GlaiveIncarnation incarnation { get { return root.GetGlaiveIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IGlaiveEffectObserver observer) {
    broadcaster.AddGlaiveObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IGlaiveEffectObserver observer) {
    broadcaster.RemoveGlaiveObserver(id, observer);
  }
  public void Delete() {
    root.EffectGlaiveDelete(id);
  }
  public static Glaive Null = new Glaive(null, 0);
  public bool Exists() { return root != null && root.GlaiveExists(id); }
  public bool NullableIs(Glaive that) {
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
  public bool Is(Glaive that) {
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
