using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DefendImpulse {
  public readonly Root root;
  public readonly int id;
  public DefendImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DefendImpulseIncarnation incarnation { get { return root.GetDefendImpulseIncarnation(id); } }
  public void AddObserver(IDefendImpulseEffectObserver observer) {
    root.AddDefendImpulseObserver(id, observer);
  }
  public void RemoveObserver(IDefendImpulseEffectObserver observer) {
    root.RemoveDefendImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectDefendImpulseDelete(id);
  }
  public static DefendImpulse Null = new DefendImpulse(null, 0);
  public bool Exists() { return root != null && root.DefendImpulseExists(id); }
  public bool NullableIs(DefendImpulse that) {
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
  public bool Is(DefendImpulse that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int weight {
    get { return incarnation.weight; }
  }
}
}
