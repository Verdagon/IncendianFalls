using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class BaseDefenseUC {
  public readonly Root root;
  public readonly int id;
  public BaseDefenseUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BaseDefenseUCIncarnation incarnation { get { return root.GetBaseDefenseUCIncarnation(id); } }
  public void AddObserver(IBaseDefenseUCEffectObserver observer) {
    root.AddBaseDefenseUCObserver(id, observer);
  }
  public void RemoveObserver(IBaseDefenseUCEffectObserver observer) {
    root.RemoveBaseDefenseUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectBaseDefenseUCDelete(id);
  }
  public static BaseDefenseUC Null = new BaseDefenseUC(null, 0);
  public bool Exists() { return root != null && root.BaseDefenseUCExists(id); }
  public bool NullableIs(BaseDefenseUC that) {
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
  public bool Is(BaseDefenseUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int incomingDamageAddConstant {
    get { return incarnation.incomingDamageAddConstant; }
  }
  public int incomingDamageMultiplierPercent {
    get { return incarnation.incomingDamageMultiplierPercent; }
  }
}
}
