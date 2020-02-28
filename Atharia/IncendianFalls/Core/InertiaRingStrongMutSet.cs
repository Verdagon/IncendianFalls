using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class InertiaRingStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public InertiaRingStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public InertiaRingStrongMutSetIncarnation incarnation {
    get { return root.GetInertiaRingStrongMutSetIncarnation(id); }
  }
  public void AddObserver(IInertiaRingStrongMutSetEffectObserver observer) {
    root.AddInertiaRingStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(IInertiaRingStrongMutSetEffectObserver observer) {
    root.RemoveInertiaRingStrongMutSetObserver(id, observer);
  }
  public void Add(InertiaRing element) {
    root.EffectInertiaRingStrongMutSetAdd(id, element.id);
  }
  public void Remove(InertiaRing element) {
    root.EffectInertiaRingStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectInertiaRingStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectInertiaRingStrongMutSetRemove(id, elementId);
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
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.InertiaRingExists(element.id)) {
        violations.Add("Null constraint violated! InertiaRingStrongMutSet#" + id + "." + element.id);
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
