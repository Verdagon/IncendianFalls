using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class WarperTTC {
  public readonly Root root;
  public readonly int id;
  public WarperTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public WarperTTCIncarnation incarnation { get { return root.GetWarperTTCIncarnation(id); } }
  public void AddObserver(IWarperTTCEffectObserver observer) {
    root.AddWarperTTCObserver(id, observer);
  }
  public void RemoveObserver(IWarperTTCEffectObserver observer) {
    root.RemoveWarperTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectWarperTTCDelete(id);
  }
  public static WarperTTC Null = new WarperTTC(null, 0);
  public bool Exists() { return root != null && root.WarperTTCExists(id); }
  public bool NullableIs(WarperTTC that) {
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
  public bool Is(WarperTTC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public Location destinationLocation {
    get { return incarnation.destinationLocation; }
  }
}
}
