using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class LeafTTC {
  public readonly Root root;
  public readonly int id;
  public LeafTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public LeafTTCIncarnation incarnation { get { return root.GetLeafTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, ILeafTTCEffectObserver observer) {
    broadcaster.AddLeafTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ILeafTTCEffectObserver observer) {
    broadcaster.RemoveLeafTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectLeafTTCDelete(id);
  }
  public static LeafTTC Null = new LeafTTC(null, 0);
  public bool Exists() { return root != null && root.LeafTTCExists(id); }
  public bool NullableIs(LeafTTC that) {
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
  public bool Is(LeafTTC that) {
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
