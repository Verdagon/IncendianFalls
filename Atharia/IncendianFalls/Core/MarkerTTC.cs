using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class MarkerTTC {
  public readonly Root root;
  public readonly int id;
  public MarkerTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public MarkerTTCIncarnation incarnation { get { return root.GetMarkerTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IMarkerTTCEffectObserver observer) {
    broadcaster.AddMarkerTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IMarkerTTCEffectObserver observer) {
    broadcaster.RemoveMarkerTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectMarkerTTCDelete(id);
  }
  public static MarkerTTC Null = new MarkerTTC(null, 0);
  public bool Exists() { return root != null && root.MarkerTTCExists(id); }
  public bool NullableIs(MarkerTTC that) {
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
  public bool Is(MarkerTTC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public string name {
    get { return incarnation.name; }
  }
}
}
