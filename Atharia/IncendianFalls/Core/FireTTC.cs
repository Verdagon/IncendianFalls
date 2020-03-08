using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class FireTTC {
  public readonly Root root;
  public readonly int id;
  public FireTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public FireTTCIncarnation incarnation { get { return root.GetFireTTCIncarnation(id); } }
  public void AddObserver(IFireTTCEffectObserver observer) {
    root.AddFireTTCObserver(id, observer);
  }
  public void RemoveObserver(IFireTTCEffectObserver observer) {
    root.RemoveFireTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectFireTTCDelete(id);
  }
  public static FireTTC Null = new FireTTC(null, 0);
  public bool Exists() { return root != null && root.FireTTCExists(id); }
  public bool NullableIs(FireTTC that) {
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
  public bool Is(FireTTC that) {
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
