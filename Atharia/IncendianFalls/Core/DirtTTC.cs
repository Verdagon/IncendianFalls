using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DirtTTC {
  public readonly Root root;
  public readonly int id;
  public DirtTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DirtTTCIncarnation incarnation { get { return root.GetDirtTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IDirtTTCEffectObserver observer) {
    broadcaster.AddDirtTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IDirtTTCEffectObserver observer) {
    broadcaster.RemoveDirtTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectDirtTTCDelete(id);
  }
  public static DirtTTC Null = new DirtTTC(null, 0);
  public bool Exists() { return root != null && root.DirtTTCExists(id); }
  public bool NullableIs(DirtTTC that) {
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
  public bool Is(DirtTTC that) {
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
