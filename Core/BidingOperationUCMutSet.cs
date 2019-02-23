using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BidingOperationUCMutSet {
  public readonly Root root;
  public readonly int id;
  public BidingOperationUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BidingOperationUCMutSetIncarnation incarnation {
    get { return root.GetBidingOperationUCMutSetIncarnation(id); }
  }
  public void AddObserver(IBidingOperationUCMutSetEffectObserver observer) {
    root.AddBidingOperationUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IBidingOperationUCMutSetEffectObserver observer) {
    root.RemoveBidingOperationUCMutSetObserver(id, observer);
  }
  public void Add(BidingOperationUC element) {
    root.EffectBidingOperationUCMutSetAdd(id, element.id);
  }
  public void Remove(BidingOperationUC element) {
    root.EffectBidingOperationUCMutSetRemove(id, element.id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectBidingOperationUCMutSetRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  //public int GetDeterministicHashCode() {
  //  return incarnation.GetDeterministicHashCode();
  //}
  public IEnumerator<BidingOperationUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetBidingOperationUC(element);
    }
  }
}
         
}
