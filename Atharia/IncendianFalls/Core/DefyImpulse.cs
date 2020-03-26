using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DefyImpulse {
  public readonly Root root;
  public readonly int id;
  public DefyImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DefyImpulseIncarnation incarnation { get { return root.GetDefyImpulseIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IDefyImpulseEffectObserver observer) {
    broadcaster.AddDefyImpulseObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IDefyImpulseEffectObserver observer) {
    broadcaster.RemoveDefyImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectDefyImpulseDelete(id);
  }
  public static DefyImpulse Null = new DefyImpulse(null, 0);
  public bool Exists() { return root != null && root.DefyImpulseExists(id); }
  public bool NullableIs(DefyImpulse that) {
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
  public bool Is(DefyImpulse that) {
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
