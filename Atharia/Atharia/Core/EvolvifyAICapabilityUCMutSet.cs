using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class EvolvifyAICapabilityUCMutSet {
  public readonly Root root;
  public readonly int id;
  public EvolvifyAICapabilityUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public EvolvifyAICapabilityUCMutSetIncarnation incarnation {
    get { return root.GetEvolvifyAICapabilityUCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IEvolvifyAICapabilityUCMutSetEffectObserver observer) {
    broadcaster.AddEvolvifyAICapabilityUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IEvolvifyAICapabilityUCMutSetEffectObserver observer) {
    broadcaster.RemoveEvolvifyAICapabilityUCMutSetObserver(id, observer);
  }
  public void Add(EvolvifyAICapabilityUC element) {
      root.EffectEvolvifyAICapabilityUCMutSetAdd(id, element.id);
  }
  public void Remove(EvolvifyAICapabilityUC element) {
      root.EffectEvolvifyAICapabilityUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectEvolvifyAICapabilityUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectEvolvifyAICapabilityUCMutSetRemove(id, element);
    }
  }
  public bool Contains(EvolvifyAICapabilityUC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<EvolvifyAICapabilityUC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetEvolvifyAICapabilityUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<EvolvifyAICapabilityUC>();
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
      if (!root.EvolvifyAICapabilityUCExists(element.id)) {
        violations.Add("Null constraint violated! EvolvifyAICapabilityUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.EvolvifyAICapabilityUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
