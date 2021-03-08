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
  public void AddObserver(EffectBroadcaster broadcaster, ISummonImpulseStrongMutSetEffectObserver observer) {
    broadcaster.AddSummonImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ISummonImpulseStrongMutSetEffectObserver observer) {
    broadcaster.RemoveSummonImpulseStrongMutSetObserver(id, observer);
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
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectSummonImpulseStrongMutSetRemove(id, element);
    }
  }
  public bool Contains(SummonImpulse element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<SummonImpulse> GetEnumerator() {
    foreach (var element in incarnation.elements) {
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
