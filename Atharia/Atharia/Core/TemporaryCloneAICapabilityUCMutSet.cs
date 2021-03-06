using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TemporaryCloneAICapabilityUCMutSet {
  public readonly Root root;
  public readonly int id;
  public TemporaryCloneAICapabilityUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public TemporaryCloneAICapabilityUCMutSetIncarnation incarnation {
    get { return root.GetTemporaryCloneAICapabilityUCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ITemporaryCloneAICapabilityUCMutSetEffectObserver observer) {
    broadcaster.AddTemporaryCloneAICapabilityUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ITemporaryCloneAICapabilityUCMutSetEffectObserver observer) {
    broadcaster.RemoveTemporaryCloneAICapabilityUCMutSetObserver(id, observer);
  }
  public void Add(TemporaryCloneAICapabilityUC element) {
      root.EffectTemporaryCloneAICapabilityUCMutSetAdd(id, element.id);
  }
  public void Remove(TemporaryCloneAICapabilityUC element) {
      root.EffectTemporaryCloneAICapabilityUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectTemporaryCloneAICapabilityUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectTemporaryCloneAICapabilityUCMutSetRemove(id, element);
    }
  }
  public bool Contains(TemporaryCloneAICapabilityUC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<TemporaryCloneAICapabilityUC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetTemporaryCloneAICapabilityUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<TemporaryCloneAICapabilityUC>();
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
      if (!root.TemporaryCloneAICapabilityUCExists(element.id)) {
        violations.Add("Null constraint violated! TemporaryCloneAICapabilityUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.TemporaryCloneAICapabilityUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
