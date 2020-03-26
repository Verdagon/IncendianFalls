using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class RavaNestTTC {
  public readonly Root root;
  public readonly int id;
  public RavaNestTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public RavaNestTTCIncarnation incarnation { get { return root.GetRavaNestTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IRavaNestTTCEffectObserver observer) {
    broadcaster.AddRavaNestTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IRavaNestTTCEffectObserver observer) {
    broadcaster.RemoveRavaNestTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectRavaNestTTCDelete(id);
  }
  public static RavaNestTTC Null = new RavaNestTTC(null, 0);
  public bool Exists() { return root != null && root.RavaNestTTCExists(id); }
  public bool NullableIs(RavaNestTTC that) {
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
  public bool Is(RavaNestTTC that) {
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
