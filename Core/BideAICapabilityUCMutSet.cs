using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BideAICapabilityUCMutSet {
  public readonly Root root;
  public readonly int id;
  public BideAICapabilityUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BideAICapabilityUCMutSetIncarnation incarnation {
    get { return root.GetBideAICapabilityUCMutSetIncarnation(id); }
  }
  public void AddObserver(IBideAICapabilityUCMutSetEffectObserver observer) {
    root.AddBideAICapabilityUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IBideAICapabilityUCMutSetEffectObserver observer) {
    root.RemoveBideAICapabilityUCMutSetObserver(id, observer);
  }
  public void Add(BideAICapabilityUC element) {
    root.EffectBideAICapabilityUCMutSetAdd(id, element.id);
  }
  public void Remove(BideAICapabilityUC element) {
    root.EffectBideAICapabilityUCMutSetRemove(id, element.id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectBideAICapabilityUCMutSetRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<BideAICapabilityUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetBideAICapabilityUC(element);
    }
  }
  public void Destruct() {
    foreach (var element in this) {
      element.Destruct();
    }
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.BideAICapabilityUCExists(element.id)) {
        violations.Add("Null constraint violated! BideAICapabilityUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.BideAICapabilityUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
