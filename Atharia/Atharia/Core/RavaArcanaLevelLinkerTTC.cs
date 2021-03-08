using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class RavaArcanaLevelLinkerTTC {
  public readonly Root root;
  public readonly int id;
  public RavaArcanaLevelLinkerTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public RavaArcanaLevelLinkerTTCIncarnation incarnation { get { return root.GetRavaArcanaLevelLinkerTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IRavaArcanaLevelLinkerTTCEffectObserver observer) {
    broadcaster.AddRavaArcanaLevelLinkerTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IRavaArcanaLevelLinkerTTCEffectObserver observer) {
    broadcaster.RemoveRavaArcanaLevelLinkerTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectRavaArcanaLevelLinkerTTCDelete(id);
  }
  public static RavaArcanaLevelLinkerTTC Null = new RavaArcanaLevelLinkerTTC(null, 0);
  public bool Exists() { return root != null && root.RavaArcanaLevelLinkerTTCExists(id); }
  public bool NullableIs(RavaArcanaLevelLinkerTTC that) {
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
  public bool Is(RavaArcanaLevelLinkerTTC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int nextLevelDepth {
    get { return incarnation.nextLevelDepth; }
  }
}
}
