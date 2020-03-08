using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SummonImpulseStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public SummonImpulseStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public SummonImpulseStrongMutSetIncarnation incarnation {
    get { return root.GetSummonImpulseStrongMutSetIncarnation(id); }
  }
  public void AddObserver(ISummonImpulseStrongMutSetEffectObserver observer) {
    root.AddSummonImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(ISummonImpulseStrongMutSetEffectObserver observer) {
    root.RemoveSummonImpulseStrongMutSetObserver(id, observer);
  }
  public void Add(SummonImpulse element) {
    root.EffectSummonImpulseStrongMutSetAdd(id, element.id);
  }
  public void Remove(SummonImpulse element) {
    root.EffectSummonImpulseStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectSummonImpulseStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectSummonImpulseStrongMutSetRemove(id, elementId);
    }
  }
  public bool Contains(SummonImpulse element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<SummonImpulse> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetSummonImpulse(element);
    }
  }
  public void Destruct() {
    var elements = new List<SummonImpulse>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.SummonImpulseExists(element.id)) {
        violations.Add("Null constraint violated! SummonImpulseStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.SummonImpulseExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
