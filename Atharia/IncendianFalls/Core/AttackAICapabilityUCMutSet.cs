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
  public void AddObserver(EffectBroadcaster broadcaster, IAttackAICapabilityUCMutSetEffectObserver observer) {
    broadcaster.AddAttackAICapabilityUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IAttackAICapabilityUCMutSetEffectObserver observer) {
    broadcaster.RemoveAttackAICapabilityUCMutSetObserver(id, observer);
  }
  public void Add(AttackAICapabilityUC element) {
      root.EffectAttackAICapabilityUCMutSetAdd(id, element.id);
  }
  public void Remove(AttackAICapabilityUC element) {
      root.EffectAttackAICapabilityUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectAttackAICapabilityUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectAttackAICapabilityUCMutSetRemove(id, element);
    }
  }
  public bool Contains(AttackAICapabilityUC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<AttackAICapabilityUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetAttackAICapabilityUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<AttackAICapabilityUC>();
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
