using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class OnFireUCWeakMutSet {
  public readonly Root root;
  public readonly int id;
  public OnFireUCWeakMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public OnFireUCWeakMutSetIncarnation incarnation {
    get { return root.GetOnFireUCWeakMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IOnFireUCWeakMutSetEffectObserver observer) {
    broadcaster.AddOnFireUCWeakMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IOnFireUCWeakMutSetEffectObserver observer) {
    broadcaster.RemoveOnFireUCWeakMutSetObserver(id, observer);
  }
  public void Add(OnFireUC element) {
      root.EffectOnFireUCWeakMutSetAdd(id, element.id);
  }
  public void Remove(OnFireUC element) {
      root.EffectOnFireUCWeakMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectOnFireUCWeakMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectOnFireUCWeakMutSetRemove(id, element);
    }
  }
  public bool Contains(OnFireUC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<OnFireUC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetOnFireUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<OnFireUC>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.OnFireUCExists(element.id)) {
        violations.Add("Null constraint violated! OnFireUCWeakMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.OnFireUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
