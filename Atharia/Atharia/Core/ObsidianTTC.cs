using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ObsidianTTC {
  public readonly Root root;
  public readonly int id;
  public ObsidianTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ObsidianTTCIncarnation incarnation { get { return root.GetObsidianTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IObsidianTTCEffectObserver observer) {
    broadcaster.AddObsidianTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IObsidianTTCEffectObserver observer) {
    broadcaster.RemoveObsidianTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectObsidianTTCDelete(id);
  }
  public static ObsidianTTC Null = new ObsidianTTC(null, 0);
  public bool Exists() { return root != null && root.ObsidianTTCExists(id); }
  public bool NullableIs(ObsidianTTC that) {
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
  public bool Is(ObsidianTTC that) {
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
