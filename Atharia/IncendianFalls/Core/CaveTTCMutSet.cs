using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CaveTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public CaveTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public CaveTTCMutSetIncarnation incarnation {
    get { return root.GetCaveTTCMutSetIncarnation(id); }
  }
  public void AddObserver(ICaveTTCMutSetEffectObserver observer) {
    root.AddCaveTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(ICaveTTCMutSetEffectObserver observer) {
    root.RemoveCaveTTCMutSetObserver(id, observer);
  }
  public void Add(CaveTTC element) {
    root.EffectCaveTTCMutSetAdd(id, element.id);
  }
  public void Remove(CaveTTC element) {
    root.EffectCaveTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectCaveTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectCaveTTCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(CaveTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<CaveTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetCaveTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<CaveTTC>();
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
      if (!root.CaveTTCExists(element.id)) {
        violations.Add("Null constraint violated! CaveTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.CaveTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
