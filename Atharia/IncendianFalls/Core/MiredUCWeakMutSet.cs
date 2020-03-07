using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MiredUCWeakMutSet {
  public readonly Root root;
  public readonly int id;
  public MiredUCWeakMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public MiredUCWeakMutSetIncarnation incarnation {
    get { return root.GetMiredUCWeakMutSetIncarnation(id); }
  }
  public void AddObserver(IMiredUCWeakMutSetEffectObserver observer) {
    root.AddMiredUCWeakMutSetObserver(id, observer);
  }
  public void RemoveObserver(IMiredUCWeakMutSetEffectObserver observer) {
    root.RemoveMiredUCWeakMutSetObserver(id, observer);
  }
  public void Add(MiredUC element) {
    root.EffectMiredUCWeakMutSetAdd(id, element.id);
  }
  public void Remove(MiredUC element) {
    root.EffectMiredUCWeakMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectMiredUCWeakMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectMiredUCWeakMutSetRemove(id, elementId);
    }
  }
  public bool Contains(MiredUC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<MiredUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetMiredUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<MiredUC>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.MiredUCExists(element.id)) {
        violations.Add("Null constraint violated! MiredUCWeakMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.MiredUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
