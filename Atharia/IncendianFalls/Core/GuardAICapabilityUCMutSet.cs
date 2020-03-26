using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GuardAICapabilityUCMutSet {
  public readonly Root root;
  public readonly int id;
  public GuardAICapabilityUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public GuardAICapabilityUCMutSetIncarnation incarnation {
    get { return root.GetGuardAICapabilityUCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IGuardAICapabilityUCMutSetEffectObserver observer) {
    broadcaster.AddGuardAICapabilityUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IGuardAICapabilityUCMutSetEffectObserver observer) {
    broadcaster.RemoveGuardAICapabilityUCMutSetObserver(id, observer);
  }
  public void Add(GuardAICapabilityUC element) {
      root.EffectGuardAICapabilityUCMutSetAdd(id, element.id);
  }
  public void Remove(GuardAICapabilityUC element) {
      root.EffectGuardAICapabilityUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectGuardAICapabilityUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectGuardAICapabilityUCMutSetRemove(id, element);
    }
  }
  public bool Contains(GuardAICapabilityUC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<GuardAICapabilityUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetGuardAICapabilityUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<GuardAICapabilityUC>();
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
      if (!root.GuardAICapabilityUCExists(element.id)) {
        violations.Add("Null constraint violated! GuardAICapabilityUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.GuardAICapabilityUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
