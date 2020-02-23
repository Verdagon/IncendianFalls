using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class InertiaRingMutSet {
  public readonly Root root;
  public readonly int id;
  public InertiaRingMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public InertiaRingMutSetIncarnation incarnation {
    get { return root.GetInertiaRingMutSetIncarnation(id); }
  }
  public void AddObserver(IInertiaRingMutSetEffectObserver observer) {
    root.AddInertiaRingMutSetObserver(id, observer);
  }
  public void RemoveObserver(IInertiaRingMutSetEffectObserver observer) {
    root.RemoveInertiaRingMutSetObserver(id, observer);
  }
  public void Add(InertiaRing element) {
    root.EffectInertiaRingMutSetAdd(id, element.id);
  }
  public void Remove(InertiaRing element) {
    root.EffectInertiaRingMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectInertiaRingMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectInertiaRingMutSetRemove(id, elementId);
    }
  }
  public bool Contains(InertiaRing element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<InertiaRing> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetInertiaRing(element);
    }
  }
  public void Destruct() {
    var elements = new List<InertiaRing>();
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
      if (!root.InertiaRingExists(element.id)) {
        violations.Add("Null constraint violated! InertiaRingMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.InertiaRingExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
