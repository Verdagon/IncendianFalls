using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class BidingUC {
  public readonly Root root;
  public readonly int id;
  public BidingUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BidingUCIncarnation incarnation { get { return root.GetBidingUCIncarnation(id); } }
  public void AddObserver(IBidingUCEffectObserver observer) {
    root.AddBidingUCObserver(id, observer);
  }
  public void RemoveObserver(IBidingUCEffectObserver observer) {
    root.RemoveBidingUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectBidingUCDelete(id);
  }
  public static BidingUC Null = new BidingUC(null, 0);
  public bool Exists() { return root != null && root.BidingUCExists(id); }
  public bool NullableIs(BidingUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(BidingUC that) {
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
