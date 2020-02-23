using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class StoneTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public StoneTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public StoneTTCMutSetIncarnation incarnation {
    get { return root.GetStoneTTCMutSetIncarnation(id); }
  }
  public void AddObserver(IStoneTTCMutSetEffectObserver observer) {
    root.AddStoneTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IStoneTTCMutSetEffectObserver observer) {
    root.RemoveStoneTTCMutSetObserver(id, observer);
  }
  public void Add(StoneTTC element) {
    root.EffectStoneTTCMutSetAdd(id, element.id);
  }
  public void Remove(StoneTTC element) {
    root.EffectStoneTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectStoneTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectStoneTTCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(StoneTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<StoneTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetStoneTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<StoneTTC>();
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
      if (!root.StoneTTCExists(element.id)) {
        violations.Add("Null constraint violated! StoneTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.StoneTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
