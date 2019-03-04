using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class BidingOperationUC {
  public readonly Root root;
  public readonly int id;
  public BidingOperationUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BidingOperationUCIncarnation incarnation { get { return root.GetBidingOperationUCIncarnation(id); } }
  public void AddObserver(IBidingOperationUCEffectObserver observer) {
    root.AddBidingOperationUCObserver(id, observer);
  }
  public void RemoveObserver(IBidingOperationUCEffectObserver observer) {
    root.RemoveBidingOperationUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectBidingOperationUCDelete(id);
  }
  public static BidingOperationUC Null = new BidingOperationUC(null, 0);
  public bool Exists() { return root != null && root.BidingOperationUCExists(id); }
  public bool NullableIs(BidingOperationUC that) {
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
  public bool Is(BidingOperationUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int charge {
    get { return incarnation.charge; }
    set { root.EffectBidingOperationUCSetCharge(id, value); }
  }
}
}
