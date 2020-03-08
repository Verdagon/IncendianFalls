using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ObsidianFloorTTC {
  public readonly Root root;
  public readonly int id;
  public ObsidianFloorTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ObsidianFloorTTCIncarnation incarnation { get { return root.GetObsidianFloorTTCIncarnation(id); } }
  public void AddObserver(IObsidianFloorTTCEffectObserver observer) {
    root.AddObsidianFloorTTCObserver(id, observer);
  }
  public void RemoveObserver(IObsidianFloorTTCEffectObserver observer) {
    root.RemoveObsidianFloorTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectObsidianFloorTTCDelete(id);
  }
  public static ObsidianFloorTTC Null = new ObsidianFloorTTC(null, 0);
  public bool Exists() { return root != null && root.ObsidianFloorTTCExists(id); }
  public bool NullableIs(ObsidianFloorTTC that) {
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
  public bool Is(ObsidianFloorTTC that) {
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
