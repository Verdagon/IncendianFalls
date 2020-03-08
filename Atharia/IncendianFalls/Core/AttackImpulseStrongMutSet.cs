using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class AttackImpulseStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public AttackImpulseStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public AttackImpulseStrongMutSetIncarnation incarnation {
    get { return root.GetAttackImpulseStrongMutSetIncarnation(id); }
  }
  public void AddObserver(IAttackImpulseStrongMutSetEffectObserver observer) {
    root.AddAttackImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(IAttackImpulseStrongMutSetEffectObserver observer) {
    root.RemoveAttackImpulseStrongMutSetObserver(id, observer);
  }
  public void Add(AttackImpulse element) {
    root.EffectAttackImpulseStrongMutSetAdd(id, element.id);
  }
  public void Remove(AttackImpulse element) {
    root.EffectAttackImpulseStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectAttackImpulseStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectAttackImpulseStrongMutSetRemove(id, elementId);
    }
  }
  public bool Contains(AttackImpulse element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<AttackImpulse> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetAttackImpulse(element);
    }
  }
  public void Destruct() {
    var elements = new List<AttackImpulse>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.AttackImpulseExists(element.id)) {
        violations.Add("Null constraint violated! AttackImpulseStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.AttackImpulseExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
