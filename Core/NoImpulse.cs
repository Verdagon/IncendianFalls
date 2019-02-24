using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NoImpulse {
  public readonly Root root;
  public readonly int id;
  public NoImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public NoImpulseIncarnation incarnation { get { return root.GetNoImpulseIncarnation(id); } }
  public void AddObserver(INoImpulseEffectObserver observer) {
    root.AddNoImpulseObserver(id, observer);
  }
  public void RemoveObserver(INoImpulseEffectObserver observer) {
    root.RemoveNoImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectNoImpulseDelete(id);
  }
  public static NoImpulse Null = new NoImpulse(null, 0);
  public bool Exists() { return root != null && root.NoImpulseExists(id); }
  public bool NullableIs(NoImpulse that) {
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
  public bool Is(NoImpulse that) {
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
