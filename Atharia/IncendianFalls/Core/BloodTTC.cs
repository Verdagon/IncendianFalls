using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class BloodTTC {
  public readonly Root root;
  public readonly int id;
  public BloodTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BloodTTCIncarnation incarnation { get { return root.GetBloodTTCIncarnation(id); } }
  public void AddObserver(IBloodTTCEffectObserver observer) {
    root.AddBloodTTCObserver(id, observer);
  }
  public void RemoveObserver(IBloodTTCEffectObserver observer) {
    root.RemoveBloodTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectBloodTTCDelete(id);
  }
  public static BloodTTC Null = new BloodTTC(null, 0);
  public bool Exists() { return root != null && root.BloodTTCExists(id); }
  public bool NullableIs(BloodTTC that) {
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
  public bool Is(BloodTTC that) {
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
