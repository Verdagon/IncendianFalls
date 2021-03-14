using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class OnFireUCMutSet {
  public readonly Root root;
  public readonly int id;
  public OnFireUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public OnFireUCMutSetIncarnation incarnation {
    get { return root.GetOnFireUCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IOnFireUCMutSetEffectObserver observer) {
    broadcaster.AddOnFireUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IOnFireUCMutSetEffectObserver observer) {
    broadcaster.RemoveOnFireUCMutSetObserver(id, observer);
  }
  public void Add(OnFireUC element) {
      root.EffectOnFireUCMutSetAdd(id, element.id);
  }
  public void Remove(OnFireUC element) {
      root.EffectOnFireUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectOnFireUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectOnFireUCMutSetRemove(id, element);
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
    foreach (var element in elements) {
      element.Destruct();
    }
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.OnFireUCExists(element.id)) {
        violations.Add("Null constraint violated! OnFireUCMutSet#" + id + "." + element.id);
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
