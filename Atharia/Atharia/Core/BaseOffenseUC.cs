using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class BaseOffenseUC {
  public readonly Root root;
  public readonly int id;
  public BaseOffenseUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BaseOffenseUCIncarnation incarnation { get { return root.GetBaseOffenseUCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IBaseOffenseUCEffectObserver observer) {
    broadcaster.AddBaseOffenseUCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IBaseOffenseUCEffectObserver observer) {
    broadcaster.RemoveBaseOffenseUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectBaseOffenseUCDelete(id);
  }
  public static BaseOffenseUC Null = new BaseOffenseUC(null, 0);
  public bool Exists() { return root != null && root.BaseOffenseUCExists(id); }
  public bool NullableIs(BaseOffenseUC that) {
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
  public bool Is(BaseOffenseUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int outgoingDamageAddConstant {
    get { return incarnation.outgoingDamageAddConstant; }
  }
  public int outgoingDamageMultiplierPercent {
    get { return incarnation.outgoingDamageMultiplierPercent; }
  }
}
}
