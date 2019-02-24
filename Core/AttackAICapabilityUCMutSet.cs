using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class AttackAICapabilityUCMutSet {
  public readonly Root root;
  public readonly int id;
  public AttackAICapabilityUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public AttackAICapabilityUCMutSetIncarnation incarnation {
    get { return root.GetAttackAICapabilityUCMutSetIncarnation(id); }
  }
  public void AddObserver(IAttackAICapabilityUCMutSetEffectObserver observer) {
    root.AddAttackAICapabilityUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IAttackAICapabilityUCMutSetEffectObserver observer) {
    root.RemoveAttackAICapabilityUCMutSetObserver(id, observer);
  }
  public void Add(AttackAICapabilityUC element) {
    root.EffectAttackAICapabilityUCMutSetAdd(id, element.id);
  }
  public void Remove(AttackAICapabilityUC element) {
    root.EffectAttackAICapabilityUCMutSetRemove(id, element.id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectAttackAICapabilityUCMutSetRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<AttackAICapabilityUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetAttackAICapabilityUC(element);
    }
  }
  public void Destruct() {
    foreach (var element in this) {
      element.Destruct();
    }
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.AttackAICapabilityUCExists(element.id)) {
        violations.Add("Null constraint violated! AttackAICapabilityUCMutSet#" + id + "." + element.id);
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
