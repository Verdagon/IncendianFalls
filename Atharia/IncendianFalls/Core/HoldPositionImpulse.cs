using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class HoldPositionImpulse {
  public readonly Root root;
  public readonly int id;
  public HoldPositionImpulse(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public HoldPositionImpulseIncarnation incarnation { get { return root.GetHoldPositionImpulseIncarnation(id); } }
  public void AddObserver(IHoldPositionImpulseEffectObserver observer) {
    root.AddHoldPositionImpulseObserver(id, observer);
  }
  public void RemoveObserver(IHoldPositionImpulseEffectObserver observer) {
    root.RemoveHoldPositionImpulseObserver(id, observer);
  }
  public void Delete() {
    root.EffectHoldPositionImpulseDelete(id);
  }
  public static HoldPositionImpulse Null = new HoldPositionImpulse(null, 0);
  public bool Exists() { return root != null && root.HoldPositionImpulseExists(id); }
  public bool NullableIs(HoldPositionImpulse that) {
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
  public bool Is(HoldPositionImpulse that) {
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
  public int duration {
    get { return incarnation.duration; }
  }
}
}
