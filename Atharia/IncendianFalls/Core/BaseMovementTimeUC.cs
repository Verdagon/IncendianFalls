using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class BaseMovementTimeUC {
  public readonly Root root;
  public readonly int id;
  public BaseMovementTimeUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BaseMovementTimeUCIncarnation incarnation { get { return root.GetBaseMovementTimeUCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IBaseMovementTimeUCEffectObserver observer) {
    broadcaster.AddBaseMovementTimeUCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IBaseMovementTimeUCEffectObserver observer) {
    broadcaster.RemoveBaseMovementTimeUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectBaseMovementTimeUCDelete(id);
  }
  public static BaseMovementTimeUC Null = new BaseMovementTimeUC(null, 0);
  public bool Exists() { return root != null && root.BaseMovementTimeUCExists(id); }
  public bool NullableIs(BaseMovementTimeUC that) {
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
  public bool Is(BaseMovementTimeUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int movementTimeAddConstant {
    get { return incarnation.movementTimeAddConstant; }
  }
  public int movementTimeMultiplierPercent {
    get { return incarnation.movementTimeMultiplierPercent; }
  }
}
}
