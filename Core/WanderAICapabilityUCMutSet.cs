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
  public void AddObserver(IWanderAICapabilityUCMutSetEffectObserver observer) {
    root.AddWanderAICapabilityUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IWanderAICapabilityUCMutSetEffectObserver observer) {
    root.RemoveWanderAICapabilityUCMutSetObserver(id, observer);
  }
  public void Add(WanderAICapabilityUC element) {
    root.EffectWanderAICapabilityUCMutSetAdd(id, element.id);
  }
  public void Remove(WanderAICapabilityUC element) {
    root.EffectWanderAICapabilityUCMutSetRemove(id, element.id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectWanderAICapabilityUCMutSetRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  //public int GetDeterministicHashCode() {
  //  return incarnation.GetDeterministicHashCode();
  //}
  public IEnumerator<WanderAICapabilityUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetWanderAICapabilityUC(element);
    }
  }
}
         
}
