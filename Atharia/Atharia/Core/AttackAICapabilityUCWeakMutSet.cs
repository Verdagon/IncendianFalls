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
  public void AddObserver(EffectBroadcaster broadcaster, IAttackAICapabilityUCWeakMutSetEffectObserver observer) {
    broadcaster.AddAttackAICapabilityUCWeakMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IAttackAICapabilityUCWeakMutSetEffectObserver observer) {
    broadcaster.RemoveAttackAICapabilityUCWeakMutSetObserver(id, observer);
  }
  public void Add(AttackAICapabilityUC element) {
      root.EffectAttackAICapabilityUCWeakMutSetAdd(id, element.id);
  }
  public void Remove(AttackAICapabilityUC element) {
      root.EffectAttackAICapabilityUCWeakMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectAttackAICapabilityUCWeakMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectAttackAICapabilityUCWeakMutSetRemove(id, element);
    }
  }
  public bool Contains(AttackAICapabilityUC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<AttackAICapabilityUC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetAttackAICapabilityUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<AttackAICapabilityUC>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
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
