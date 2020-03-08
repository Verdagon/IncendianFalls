using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DoomedUC {
  public readonly Root root;
  public readonly int id;
  public DoomedUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DoomedUCIncarnation incarnation { get { return root.GetDoomedUCIncarnation(id); } }
  public void AddObserver(IDoomedUCEffectObserver observer) {
    root.AddDoomedUCObserver(id, observer);
  }
  public void RemoveObserver(IDoomedUCEffectObserver observer) {
    root.RemoveDoomedUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectDoomedUCDelete(id);
  }
  public static DoomedUC Null = new DoomedUC(null, 0);
  public bool Exists() { return root != null && root.DoomedUCExists(id); }
  public bool NullableIs(DoomedUC that) {
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
  public bool Is(DoomedUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int deathTime {
    get { return incarnation.deathTime; }
  }
}
}
