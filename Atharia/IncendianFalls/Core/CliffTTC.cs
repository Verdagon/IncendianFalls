using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class CliffTTC {
  public readonly Root root;
  public readonly int id;
  public CliffTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public CliffTTCIncarnation incarnation { get { return root.GetCliffTTCIncarnation(id); } }
  public void AddObserver(ICliffTTCEffectObserver observer) {
    root.AddCliffTTCObserver(id, observer);
  }
  public void RemoveObserver(ICliffTTCEffectObserver observer) {
    root.RemoveCliffTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectCliffTTCDelete(id);
  }
  public static CliffTTC Null = new CliffTTC(null, 0);
  public bool Exists() { return root != null && root.CliffTTCExists(id); }
  public bool NullableIs(CliffTTC that) {
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
  public bool Is(CliffTTC that) {
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
