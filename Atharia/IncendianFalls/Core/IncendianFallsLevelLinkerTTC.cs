using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class IncendianFallsLevelLinkerTTC {
  public readonly Root root;
  public readonly int id;
  public IncendianFallsLevelLinkerTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public IncendianFallsLevelLinkerTTCIncarnation incarnation { get { return root.GetIncendianFallsLevelLinkerTTCIncarnation(id); } }
  public void AddObserver(IIncendianFallsLevelLinkerTTCEffectObserver observer) {
    root.AddIncendianFallsLevelLinkerTTCObserver(id, observer);
  }
  public void RemoveObserver(IIncendianFallsLevelLinkerTTCEffectObserver observer) {
    root.RemoveIncendianFallsLevelLinkerTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectIncendianFallsLevelLinkerTTCDelete(id);
  }
  public static IncendianFallsLevelLinkerTTC Null = new IncendianFallsLevelLinkerTTC(null, 0);
  public bool Exists() { return root != null && root.IncendianFallsLevelLinkerTTCExists(id); }
  public bool NullableIs(IncendianFallsLevelLinkerTTC that) {
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
  public bool Is(IncendianFallsLevelLinkerTTC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int thisLevelDepth {
    get { return incarnation.thisLevelDepth; }
  }
}
}
