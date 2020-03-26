using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class CliffLandingTTC {
  public readonly Root root;
  public readonly int id;
  public CliffLandingTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public CliffLandingTTCIncarnation incarnation { get { return root.GetCliffLandingTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, ICliffLandingTTCEffectObserver observer) {
    broadcaster.AddCliffLandingTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ICliffLandingTTCEffectObserver observer) {
    broadcaster.RemoveCliffLandingTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectCliffLandingTTCDelete(id);
  }
  public static CliffLandingTTC Null = new CliffLandingTTC(null, 0);
  public bool Exists() { return root != null && root.CliffLandingTTCExists(id); }
  public bool NullableIs(CliffLandingTTC that) {
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
  public bool Is(CliffLandingTTC that) {
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
