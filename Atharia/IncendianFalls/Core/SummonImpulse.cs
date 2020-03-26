using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class SummonImpulse {
  public readonly Root root;
  public readonly int id;
  public SummonImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public SummonImpulseIncarnation incarnation { get { return root.GetSummonImpulseIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, ISummonImpulseEffectObserver observer) {
    broadcaster.AddSummonImpulseObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ISummonImpulseEffectObserver observer) {
    broadcaster.RemoveSummonImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectSummonImpulseDelete(id);
  }
  public static SummonImpulse Null = new SummonImpulse(null, 0);
  public bool Exists() { return root != null && root.SummonImpulseExists(id); }
  public bool NullableIs(SummonImpulse that) {
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
  public bool Is(SummonImpulse that) {
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
  public string blueprintName {
    get { return incarnation.blueprintName; }
  }
  public Location location {
    get { return incarnation.location; }
  }
}
}
