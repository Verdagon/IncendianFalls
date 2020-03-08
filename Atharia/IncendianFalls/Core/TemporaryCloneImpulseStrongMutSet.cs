using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TemporaryCloneImpulseStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public TemporaryCloneImpulseStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public TemporaryCloneImpulseStrongMutSetIncarnation incarnation {
    get { return root.GetTemporaryCloneImpulseStrongMutSetIncarnation(id); }
  }
  public void AddObserver(ITemporaryCloneImpulseStrongMutSetEffectObserver observer) {
    root.AddTemporaryCloneImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(ITemporaryCloneImpulseStrongMutSetEffectObserver observer) {
    root.RemoveTemporaryCloneImpulseStrongMutSetObserver(id, observer);
  }
  public void Add(TemporaryCloneImpulse element) {
    root.EffectTemporaryCloneImpulseStrongMutSetAdd(id, element.id);
  }
  public void Remove(TemporaryCloneImpulse element) {
    root.EffectTemporaryCloneImpulseStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectTemporaryCloneImpulseStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectTemporaryCloneImpulseStrongMutSetRemove(id, elementId);
    }
  }
  public bool Contains(TemporaryCloneImpulse element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<TemporaryCloneImpulse> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetTemporaryCloneImpulse(element);
    }
  }
  public void Destruct() {
    var elements = new List<TemporaryCloneImpulse>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.TemporaryCloneImpulseExists(element.id)) {
        violations.Add("Null constraint violated! TemporaryCloneImpulseStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.TemporaryCloneImpulseExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
