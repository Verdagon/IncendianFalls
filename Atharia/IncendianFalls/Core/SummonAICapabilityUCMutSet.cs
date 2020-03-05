using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SummonAICapabilityUCMutSet {
  public readonly Root root;
  public readonly int id;
  public SummonAICapabilityUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public SummonAICapabilityUCMutSetIncarnation incarnation {
    get { return root.GetSummonAICapabilityUCMutSetIncarnation(id); }
  }
  public void AddObserver(ISummonAICapabilityUCMutSetEffectObserver observer) {
    root.AddSummonAICapabilityUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(ISummonAICapabilityUCMutSetEffectObserver observer) {
    root.RemoveSummonAICapabilityUCMutSetObserver(id, observer);
  }
  public void Add(SummonAICapabilityUC element) {
    root.EffectSummonAICapabilityUCMutSetAdd(id, element.id);
  }
  public void Remove(SummonAICapabilityUC element) {
    root.EffectSummonAICapabilityUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectSummonAICapabilityUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectSummonAICapabilityUCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(SummonAICapabilityUC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<SummonAICapabilityUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetSummonAICapabilityUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<SummonAICapabilityUC>();
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
      if (!root.SummonAICapabilityUCExists(element.id)) {
        violations.Add("Null constraint violated! SummonAICapabilityUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.SummonAICapabilityUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
