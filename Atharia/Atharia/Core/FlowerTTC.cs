using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class FlowerTTC {
  public readonly Root root;
  public readonly int id;
  public FlowerTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public FlowerTTCIncarnation incarnation { get { return root.GetFlowerTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IFlowerTTCEffectObserver observer) {
    broadcaster.AddFlowerTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IFlowerTTCEffectObserver observer) {
    broadcaster.RemoveFlowerTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectFlowerTTCDelete(id);
  }
  public static FlowerTTC Null = new FlowerTTC(null, 0);
  public bool Exists() { return root != null && root.FlowerTTCExists(id); }
  public bool NullableIs(FlowerTTC that) {
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
  public bool Is(FlowerTTC that) {
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
