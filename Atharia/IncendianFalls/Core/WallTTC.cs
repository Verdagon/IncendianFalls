using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class WallTTC {
  public readonly Root root;
  public readonly int id;
  public WallTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public WallTTCIncarnation incarnation { get { return root.GetWallTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IWallTTCEffectObserver observer) {
    broadcaster.AddWallTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IWallTTCEffectObserver observer) {
    broadcaster.RemoveWallTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectWallTTCDelete(id);
  }
  public static WallTTC Null = new WallTTC(null, 0);
  public bool Exists() { return root != null && root.WallTTCExists(id); }
  public bool NullableIs(WallTTC that) {
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
  public bool Is(WallTTC that) {
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
