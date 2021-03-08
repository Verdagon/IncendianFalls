using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class SpeedRing {
  public readonly Root root;
  public readonly int id;
  public SpeedRing(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public SpeedRingIncarnation incarnation { get { return root.GetSpeedRingIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, ISpeedRingEffectObserver observer) {
    broadcaster.AddSpeedRingObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ISpeedRingEffectObserver observer) {
    broadcaster.RemoveSpeedRingObserver(id, observer);
  }
  public void Delete() {
    root.EffectSpeedRingDelete(id);
  }
  public static SpeedRing Null = new SpeedRing(null, 0);
  public bool Exists() { return root != null && root.SpeedRingExists(id); }
  public bool NullableIs(SpeedRing that) {
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
  public bool Is(SpeedRing that) {
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
