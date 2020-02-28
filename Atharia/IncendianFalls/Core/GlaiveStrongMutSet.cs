using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GlaiveStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public GlaiveStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public GlaiveStrongMutSetIncarnation incarnation {
    get { return root.GetGlaiveStrongMutSetIncarnation(id); }
  }
  public void AddObserver(IGlaiveStrongMutSetEffectObserver observer) {
    root.AddGlaiveStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(IGlaiveStrongMutSetEffectObserver observer) {
    root.RemoveGlaiveStrongMutSetObserver(id, observer);
  }
  public void Add(Glaive element) {
    root.EffectGlaiveStrongMutSetAdd(id, element.id);
  }
  public void Remove(Glaive element) {
    root.EffectGlaiveStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectGlaiveStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectGlaiveStrongMutSetRemove(id, elementId);
    }
  }
  public bool Contains(Glaive element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<Glaive> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetGlaive(element);
    }
  }
  public void Destruct() {
    var elements = new List<Glaive>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.GlaiveExists(element.id)) {
        violations.Add("Null constraint violated! GlaiveStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.GlaiveExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
