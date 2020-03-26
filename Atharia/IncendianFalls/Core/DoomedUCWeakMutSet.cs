using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DoomedUCWeakMutSet {
  public readonly Root root;
  public readonly int id;
  public DoomedUCWeakMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DoomedUCWeakMutSetIncarnation incarnation {
    get { return root.GetDoomedUCWeakMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IDoomedUCWeakMutSetEffectObserver observer) {
    broadcaster.AddDoomedUCWeakMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IDoomedUCWeakMutSetEffectObserver observer) {
    broadcaster.RemoveDoomedUCWeakMutSetObserver(id, observer);
  }
  public void Add(DoomedUC element) {
      root.EffectDoomedUCWeakMutSetAdd(id, element.id);
  }
  public void Remove(DoomedUC element) {
      root.EffectDoomedUCWeakMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectDoomedUCWeakMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectDoomedUCWeakMutSetRemove(id, element);
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
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.DoomedUCExists(element.id)) {
        violations.Add("Null constraint violated! DoomedUCWeakMutSet#" + id + "." + element.id);
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
