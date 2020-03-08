using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DefyImpulseStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public DefyImpulseStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DefyImpulseStrongMutSetIncarnation incarnation {
    get { return root.GetDefyImpulseStrongMutSetIncarnation(id); }
  }
  public void AddObserver(IDefyImpulseStrongMutSetEffectObserver observer) {
    root.AddDefyImpulseStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(IDefyImpulseStrongMutSetEffectObserver observer) {
    root.RemoveDefyImpulseStrongMutSetObserver(id, observer);
  }
  public void Add(DefyImpulse element) {
    root.EffectDefyImpulseStrongMutSetAdd(id, element.id);
  }
  public void Remove(DefyImpulse element) {
    root.EffectDefyImpulseStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectDefyImpulseStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectDefyImpulseStrongMutSetRemove(id, elementId);
    }
  }
  public bool Contains(DefyImpulse element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<DefyImpulse> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetDefyImpulse(element);
    }
  }
  public void Destruct() {
    var elements = new List<DefyImpulse>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.DefyImpulseExists(element.id)) {
        violations.Add("Null constraint violated! DefyImpulseStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.DefyImpulseExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
