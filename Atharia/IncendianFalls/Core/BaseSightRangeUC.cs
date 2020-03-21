using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class BaseSightRangeUC {
  public readonly Root root;
  public readonly int id;
  public BaseSightRangeUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BaseSightRangeUCIncarnation incarnation { get { return root.GetBaseSightRangeUCIncarnation(id); } }
  public void AddObserver(IBaseSightRangeUCEffectObserver observer) {
    root.AddBaseSightRangeUCObserver(id, observer);
  }
  public void RemoveObserver(IBaseSightRangeUCEffectObserver observer) {
    root.RemoveBaseSightRangeUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectBaseSightRangeUCDelete(id);
  }
  public static BaseSightRangeUC Null = new BaseSightRangeUC(null, 0);
  public bool Exists() { return root != null && root.BaseSightRangeUCExists(id); }
  public bool NullableIs(BaseSightRangeUC that) {
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
  public bool Is(BaseSightRangeUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int sightRangeAddConstant {
    get { return incarnation.sightRangeAddConstant; }
  }
  public int sightRangeMultiplierPercent {
    get { return incarnation.sightRangeMultiplierPercent; }
  }
}
}
