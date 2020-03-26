using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TimeCloneAICapabilityUCWeakMutSet {
  public readonly Root root;
  public readonly int id;
  public TimeCloneAICapabilityUCWeakMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public TimeCloneAICapabilityUCWeakMutSetIncarnation incarnation {
    get { return root.GetTimeCloneAICapabilityUCWeakMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ITimeCloneAICapabilityUCWeakMutSetEffectObserver observer) {
    broadcaster.AddTimeCloneAICapabilityUCWeakMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ITimeCloneAICapabilityUCWeakMutSetEffectObserver observer) {
    broadcaster.RemoveTimeCloneAICapabilityUCWeakMutSetObserver(id, observer);
  }
  public void Add(TimeCloneAICapabilityUC element) {
      root.EffectTimeCloneAICapabilityUCWeakMutSetAdd(id, element.id);
  }
  public void Remove(TimeCloneAICapabilityUC element) {
      root.EffectTimeCloneAICapabilityUCWeakMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectTimeCloneAICapabilityUCWeakMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectTimeCloneAICapabilityUCWeakMutSetRemove(id, element);
    }
  }
  public bool Contains(TimeCloneAICapabilityUC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<TimeCloneAICapabilityUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetTimeCloneAICapabilityUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<TimeCloneAICapabilityUC>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.TimeCloneAICapabilityUCExists(element.id)) {
        violations.Add("Null constraint violated! TimeCloneAICapabilityUCWeakMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.TimeCloneAICapabilityUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
