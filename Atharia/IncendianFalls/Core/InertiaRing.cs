using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class InertiaRing {
  public readonly Root root;
  public readonly int id;
  public InertiaRing(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public InertiaRingIncarnation incarnation { get { return root.GetInertiaRingIncarnation(id); } }
  public void AddObserver(IInertiaRingEffectObserver observer) {
    root.AddInertiaRingObserver(id, observer);
  }
  public void RemoveObserver(IInertiaRingEffectObserver observer) {
    root.RemoveInertiaRingObserver(id, observer);
  }
  public void Delete() {
    root.EffectInertiaRingDelete(id);
  }
  public static InertiaRing Null = new InertiaRing(null, 0);
  public bool Exists() { return root != null && root.InertiaRingExists(id); }
  public bool NullableIs(InertiaRing that) {
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
  public bool Is(InertiaRing that) {
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
