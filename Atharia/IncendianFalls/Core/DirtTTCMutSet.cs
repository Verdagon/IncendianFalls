using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DirtTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public DirtTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DirtTTCMutSetIncarnation incarnation {
    get { return root.GetDirtTTCMutSetIncarnation(id); }
  }
  public void AddObserver(IDirtTTCMutSetEffectObserver observer) {
    root.AddDirtTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IDirtTTCMutSetEffectObserver observer) {
    root.RemoveDirtTTCMutSetObserver(id, observer);
  }
  public void Add(DirtTTC element) {
    root.EffectDirtTTCMutSetAdd(id, element.id);
  }
  public void Remove(DirtTTC element) {
    root.EffectDirtTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectDirtTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectDirtTTCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(DirtTTC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<DirtTTC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetDirtTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<DirtTTC>();
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
      if (!root.DirtTTCExists(element.id)) {
        violations.Add("Null constraint violated! DirtTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.DirtTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
