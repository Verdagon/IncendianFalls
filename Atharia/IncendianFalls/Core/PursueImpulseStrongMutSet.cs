using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class PursueImpulseStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public PursueImpulseStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public PursueImpulseStrongMutSetIncarnation incarnation {
    get { return root.GetPursueImpulseStrongMutSetIncarnation(id); }
  }
  public void AddObserver(IPursueImpulseStrongMutSetEffectObserver observer) {
    root.AddPursueImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(IPursueImpulseStrongMutSetEffectObserver observer) {
    root.RemovePursueImpulseStrongMutSetObserver(id, observer);
  }
  public void Add(PursueImpulse element) {
    root.EffectPursueImpulseStrongMutSetAdd(id, element.id);
  }
  public void Remove(PursueImpulse element) {
    root.EffectPursueImpulseStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectPursueImpulseStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectPursueImpulseStrongMutSetRemove(id, elementId);
    }
  }
  public bool Contains(PursueImpulse element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<PursueImpulse> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetPursueImpulse(element);
    }
  }
  public void Destruct() {
    var elements = new List<PursueImpulse>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.PursueImpulseExists(element.id)) {
        violations.Add("Null constraint violated! PursueImpulseStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.PursueImpulseExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
