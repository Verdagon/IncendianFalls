using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class StoneTTC {
  public readonly Root root;
  public readonly int id;
  public StoneTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public StoneTTCIncarnation incarnation { get { return root.GetStoneTTCIncarnation(id); } }
  public void AddObserver(IStoneTTCEffectObserver observer) {
    root.AddStoneTTCObserver(id, observer);
  }
  public void RemoveObserver(IStoneTTCEffectObserver observer) {
    root.RemoveStoneTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectStoneTTCDelete(id);
  }
  public static StoneTTC Null = new StoneTTC(null, 0);
  public bool Exists() { return root != null && root.StoneTTCExists(id); }
  public bool NullableIs(StoneTTC that) {
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
  public bool Is(StoneTTC that) {
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
