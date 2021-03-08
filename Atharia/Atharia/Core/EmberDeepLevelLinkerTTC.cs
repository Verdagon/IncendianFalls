using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class EmberDeepLevelLinkerTTC {
  public readonly Root root;
  public readonly int id;
  public EmberDeepLevelLinkerTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public EmberDeepLevelLinkerTTCIncarnation incarnation { get { return root.GetEmberDeepLevelLinkerTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IEmberDeepLevelLinkerTTCEffectObserver observer) {
    broadcaster.AddEmberDeepLevelLinkerTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IEmberDeepLevelLinkerTTCEffectObserver observer) {
    broadcaster.RemoveEmberDeepLevelLinkerTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectEmberDeepLevelLinkerTTCDelete(id);
  }
  public static EmberDeepLevelLinkerTTC Null = new EmberDeepLevelLinkerTTC(null, 0);
  public bool Exists() { return root != null && root.EmberDeepLevelLinkerTTCExists(id); }
  public bool NullableIs(EmberDeepLevelLinkerTTC that) {
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
  public bool Is(EmberDeepLevelLinkerTTC that) {
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
