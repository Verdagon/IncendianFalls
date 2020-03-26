using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseOffenseUCMutSet {
  public readonly Root root;
  public readonly int id;
  public BaseOffenseUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BaseOffenseUCMutSetIncarnation incarnation {
    get { return root.GetBaseOffenseUCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IBaseOffenseUCMutSetEffectObserver observer) {
    broadcaster.AddBaseOffenseUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IBaseOffenseUCMutSetEffectObserver observer) {
    broadcaster.RemoveBaseOffenseUCMutSetObserver(id, observer);
  }
  public void Add(BaseOffenseUC element) {
      root.EffectBaseOffenseUCMutSetAdd(id, element.id);
  }
  public void Remove(BaseOffenseUC element) {
      root.EffectBaseOffenseUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectBaseOffenseUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectBaseOffenseUCMutSetRemove(id, element);
    }
  }
  public bool Contains(BaseOffenseUC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<BaseOffenseUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetBaseOffenseUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<BaseOffenseUC>();
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
      if (!root.BaseOffenseUCExists(element.id)) {
        violations.Add("Null constraint violated! BaseOffenseUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.BaseOffenseUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
