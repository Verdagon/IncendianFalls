using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class WaterTTC {
  public readonly Root root;
  public readonly int id;
  public WaterTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public WaterTTCIncarnation incarnation { get { return root.GetWaterTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IWaterTTCEffectObserver observer) {
    broadcaster.AddWaterTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IWaterTTCEffectObserver observer) {
    broadcaster.RemoveWaterTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectWaterTTCDelete(id);
  }
  public static WaterTTC Null = new WaterTTC(null, 0);
  public bool Exists() { return root != null && root.WaterTTCExists(id); }
  public bool NullableIs(WaterTTC that) {
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
  public bool Is(WaterTTC that) {
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
