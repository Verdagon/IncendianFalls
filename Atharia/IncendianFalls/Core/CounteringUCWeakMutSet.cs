using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CounteringUCWeakMutSet {
  public readonly Root root;
  public readonly int id;
  public CounteringUCWeakMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public CounteringUCWeakMutSetIncarnation incarnation {
    get { return root.GetCounteringUCWeakMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, ICounteringUCWeakMutSetEffectObserver observer) {
    broadcaster.AddCounteringUCWeakMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ICounteringUCWeakMutSetEffectObserver observer) {
    broadcaster.RemoveCounteringUCWeakMutSetObserver(id, observer);
  }
  public void Add(CounteringUC element) {
      root.EffectCounteringUCWeakMutSetAdd(id, element.id);
  }
  public void Remove(CounteringUC element) {
      root.EffectCounteringUCWeakMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectCounteringUCWeakMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectCounteringUCWeakMutSetRemove(id, element);
    }
  }
  public bool Contains(CounteringUC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<CounteringUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetCounteringUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<CounteringUC>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.CounteringUCExists(element.id)) {
        violations.Add("Null constraint violated! CounteringUCWeakMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.CounteringUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
