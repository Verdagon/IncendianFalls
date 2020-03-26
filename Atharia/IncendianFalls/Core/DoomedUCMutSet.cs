using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DoomedUCMutSet {
  public readonly Root root;
  public readonly int id;
  public DoomedUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DoomedUCMutSetIncarnation incarnation {
    get { return root.GetDoomedUCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IDoomedUCMutSetEffectObserver observer) {
    broadcaster.AddDoomedUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IDoomedUCMutSetEffectObserver observer) {
    broadcaster.RemoveDoomedUCMutSetObserver(id, observer);
  }
  public void Add(DoomedUC element) {
      root.EffectDoomedUCMutSetAdd(id, element.id);
  }
  public void Remove(DoomedUC element) {
      root.EffectDoomedUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectDoomedUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectDoomedUCMutSetRemove(id, element);
    }
  }
  public bool Contains(DoomedUC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<DoomedUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetDoomedUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<DoomedUC>();
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
      if (!root.DoomedUCExists(element.id)) {
        violations.Add("Null constraint violated! DoomedUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.DoomedUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
