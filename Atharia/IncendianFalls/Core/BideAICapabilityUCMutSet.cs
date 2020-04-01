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
  public void AddObserver(EffectBroadcaster broadcaster, IBideAICapabilityUCMutSetEffectObserver observer) {
    broadcaster.AddBideAICapabilityUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IBideAICapabilityUCMutSetEffectObserver observer) {
    broadcaster.RemoveBideAICapabilityUCMutSetObserver(id, observer);
  }
  public void Add(BideAICapabilityUC element) {
      root.EffectBideAICapabilityUCMutSetAdd(id, element.id);
  }
  public void Remove(BideAICapabilityUC element) {
      root.EffectBideAICapabilityUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectBideAICapabilityUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectBideAICapabilityUCMutSetRemove(id, element);
    }
  }
  public bool Contains(BideAICapabilityUC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<BideAICapabilityUC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetBideAICapabilityUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<BideAICapabilityUC>();
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
