using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ShieldingUC {
  public readonly Root root;
  public readonly int id;
  public ShieldingUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ShieldingUCIncarnation incarnation { get { return root.GetShieldingUCIncarnation(id); } }
  public void AddObserver(IShieldingUCEffectObserver observer) {
    root.AddShieldingUCObserver(id, observer);
  }
  public void RemoveObserver(IShieldingUCEffectObserver observer) {
    root.RemoveShieldingUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectShieldingUCDelete(id);
  }
  public static ShieldingUC Null = new ShieldingUC(null, 0);
  public bool Exists() { return root != null && root.ShieldingUCExists(id); }
  public bool NullableIs(ShieldingUC that) {
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
  public bool Is(ShieldingUC that) {
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
