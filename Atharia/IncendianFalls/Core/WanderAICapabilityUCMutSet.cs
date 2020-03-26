using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WanderAICapabilityUCMutSet {
  public readonly Root root;
  public readonly int id;
  public WanderAICapabilityUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public WanderAICapabilityUCMutSetIncarnation incarnation {
    get { return root.GetWanderAICapabilityUCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IWanderAICapabilityUCMutSetEffectObserver observer) {
    broadcaster.AddWanderAICapabilityUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IWanderAICapabilityUCMutSetEffectObserver observer) {
    broadcaster.RemoveWanderAICapabilityUCMutSetObserver(id, observer);
  }
  public void Add(WanderAICapabilityUC element) {
      root.EffectWanderAICapabilityUCMutSetAdd(id, element.id);
  }
  public void Remove(WanderAICapabilityUC element) {
      root.EffectWanderAICapabilityUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectWanderAICapabilityUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectWanderAICapabilityUCMutSetRemove(id, element);
    }
  }
  public bool Contains(WanderAICapabilityUC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<WanderAICapabilityUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetWanderAICapabilityUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<WanderAICapabilityUC>();
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
      if (!root.WanderAICapabilityUCExists(element.id)) {
        violations.Add("Null constraint violated! WanderAICapabilityUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.WanderAICapabilityUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
