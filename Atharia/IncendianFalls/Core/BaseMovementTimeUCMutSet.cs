using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseMovementTimeUCMutSet {
  public readonly Root root;
  public readonly int id;
  public BaseMovementTimeUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BaseMovementTimeUCMutSetIncarnation incarnation {
    get { return root.GetBaseMovementTimeUCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IBaseMovementTimeUCMutSetEffectObserver observer) {
    broadcaster.AddBaseMovementTimeUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IBaseMovementTimeUCMutSetEffectObserver observer) {
    broadcaster.RemoveBaseMovementTimeUCMutSetObserver(id, observer);
  }
  public void Add(BaseMovementTimeUC element) {
      root.EffectBaseMovementTimeUCMutSetAdd(id, element.id);
  }
  public void Remove(BaseMovementTimeUC element) {
      root.EffectBaseMovementTimeUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectBaseMovementTimeUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectBaseMovementTimeUCMutSetRemove(id, element);
    }
  }
  public bool Contains(BaseMovementTimeUC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<BaseMovementTimeUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetBaseMovementTimeUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<BaseMovementTimeUC>();
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
      if (!root.BaseMovementTimeUCExists(element.id)) {
        violations.Add("Null constraint violated! BaseMovementTimeUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.BaseMovementTimeUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
