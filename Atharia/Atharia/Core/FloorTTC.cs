using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class FloorTTC {
  public readonly Root root;
  public readonly int id;
  public FloorTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public FloorTTCIncarnation incarnation { get { return root.GetFloorTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IFloorTTCEffectObserver observer) {
    broadcaster.AddFloorTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IFloorTTCEffectObserver observer) {
    broadcaster.RemoveFloorTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectFloorTTCDelete(id);
  }
  public static FloorTTC Null = new FloorTTC(null, 0);
  public bool Exists() { return root != null && root.FloorTTCExists(id); }
  public bool NullableIs(FloorTTC that) {
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
  public bool Is(FloorTTC that) {
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
