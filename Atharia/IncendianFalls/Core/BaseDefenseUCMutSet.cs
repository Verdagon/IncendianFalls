using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseDefenseUCMutSet {
  public readonly Root root;
  public readonly int id;
  public BaseDefenseUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BaseDefenseUCMutSetIncarnation incarnation {
    get { return root.GetBaseDefenseUCMutSetIncarnation(id); }
  }
  public void AddObserver(IBaseDefenseUCMutSetEffectObserver observer) {
    root.AddBaseDefenseUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IBaseDefenseUCMutSetEffectObserver observer) {
    root.RemoveBaseDefenseUCMutSetObserver(id, observer);
  }
  public void Add(BaseDefenseUC element) {
    root.EffectBaseDefenseUCMutSetAdd(id, element.id);
  }
  public void Remove(BaseDefenseUC element) {
    root.EffectBaseDefenseUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectBaseDefenseUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectBaseDefenseUCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(BaseDefenseUC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<BaseDefenseUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetBaseDefenseUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<BaseDefenseUC>();
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
      if (!root.BaseDefenseUCExists(element.id)) {
        violations.Add("Null constraint violated! BaseDefenseUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.BaseDefenseUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
