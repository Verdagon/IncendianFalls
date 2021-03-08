using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class CaveTTC {
  public readonly Root root;
  public readonly int id;
  public CaveTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public CaveTTCIncarnation incarnation { get { return root.GetCaveTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, ICaveTTCEffectObserver observer) {
    broadcaster.AddCaveTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ICaveTTCEffectObserver observer) {
    broadcaster.RemoveCaveTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectCaveTTCDelete(id);
  }
  public static CaveTTC Null = new CaveTTC(null, 0);
  public bool Exists() { return root != null && root.CaveTTCExists(id); }
  public bool NullableIs(CaveTTC that) {
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
  public bool Is(CaveTTC that) {
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
