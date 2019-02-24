using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class AttackAICapabilityUCWeakMutSet {
  public readonly Root root;
  public readonly int id;
  public AttackAICapabilityUCWeakMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public AttackAICapabilityUCWeakMutSetIncarnation incarnation {
    get { return root.GetAttackAICapabilityUCWeakMutSetIncarnation(id); }
  }
  public void AddObserver(IAttackAICapabilityUCWeakMutSetEffectObserver observer) {
    root.AddAttackAICapabilityUCWeakMutSetObserver(id, observer);
  }
  public void RemoveObserver(IAttackAICapabilityUCWeakMutSetEffectObserver observer) {
    root.RemoveAttackAICapabilityUCWeakMutSetObserver(id, observer);
  }
  public void Add(AttackAICapabilityUC element) {
    root.EffectAttackAICapabilityUCWeakMutSetAdd(id, element.id);
  }
  public void Remove(AttackAICapabilityUC element) {
    root.EffectAttackAICapabilityUCWeakMutSetRemove(id, element.id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectAttackAICapabilityUCWeakMutSetRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<AttackAICapabilityUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetAttackAICapabilityUC(element);
    }
  }
  public void Destruct() {
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.AttackAICapabilityUCExists(element.id)) {
        violations.Add("Null constraint violated! AttackAICapabilityUCWeakMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.AttackAICapabilityUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
