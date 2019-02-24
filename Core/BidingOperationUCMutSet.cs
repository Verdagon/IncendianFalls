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
  public void Delete() {
    root.EffectBidingOperationUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectBidingOperationUCMutSetRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<BidingOperationUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetBidingOperationUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<BidingOperationUC>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
    foreach (var element in elements) {
      element.Destruct();
    }
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.BidingOperationUCExists(element.id)) {
        violations.Add("Null constraint violated! BidingOperationUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.BidingOperationUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
