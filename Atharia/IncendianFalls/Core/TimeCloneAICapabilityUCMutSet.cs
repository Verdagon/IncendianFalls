using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TimeCloneAICapabilityUCMutSet {
  public readonly Root root;
  public readonly int id;
  public TimeCloneAICapabilityUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public TimeCloneAICapabilityUCMutSetIncarnation incarnation {
    get { return root.GetTimeCloneAICapabilityUCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ITimeCloneAICapabilityUCMutSetEffectObserver observer) {
    broadcaster.AddTimeCloneAICapabilityUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ITimeCloneAICapabilityUCMutSetEffectObserver observer) {
    broadcaster.RemoveTimeCloneAICapabilityUCMutSetObserver(id, observer);
  }
  public void Add(TimeCloneAICapabilityUC element) {
      root.EffectTimeCloneAICapabilityUCMutSetAdd(id, element.id);
  }
  public void Remove(TimeCloneAICapabilityUC element) {
      root.EffectTimeCloneAICapabilityUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectTimeCloneAICapabilityUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectTimeCloneAICapabilityUCMutSetRemove(id, element);
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
    foreach (var element in elements) {
      element.Destruct();
    }
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.TimeCloneAICapabilityUCExists(element.id)) {
        violations.Add("Null constraint violated! TimeCloneAICapabilityUCMutSet#" + id + "." + element.id);
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
