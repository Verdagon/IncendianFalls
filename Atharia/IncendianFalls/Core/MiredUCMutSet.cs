using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MiredUCMutSet {
  public readonly Root root;
  public readonly int id;
  public MiredUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public MiredUCMutSetIncarnation incarnation {
    get { return root.GetMiredUCMutSetIncarnation(id); }
  }
  public void AddObserver(IMiredUCMutSetEffectObserver observer) {
    root.AddMiredUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IMiredUCMutSetEffectObserver observer) {
    root.RemoveMiredUCMutSetObserver(id, observer);
  }
  public void Add(MiredUC element) {
    root.EffectMiredUCMutSetAdd(id, element.id);
  }
  public void Remove(MiredUC element) {
    root.EffectMiredUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectMiredUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectMiredUCMutSetRemove(id, elementId);
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
    foreach (var element in elements) {
      element.Destruct();
    }
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.MiredUCExists(element.id)) {
        violations.Add("Null constraint violated! MiredUCMutSet#" + id + "." + element.id);
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
