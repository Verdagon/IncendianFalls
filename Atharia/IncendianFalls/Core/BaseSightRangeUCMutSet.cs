using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseSightRangeUCMutSet {
  public readonly Root root;
  public readonly int id;
  public BaseSightRangeUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BaseSightRangeUCMutSetIncarnation incarnation {
    get { return root.GetBaseSightRangeUCMutSetIncarnation(id); }
  }
  public void AddObserver(IBaseSightRangeUCMutSetEffectObserver observer) {
    root.AddBaseSightRangeUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IBaseSightRangeUCMutSetEffectObserver observer) {
    root.RemoveBaseSightRangeUCMutSetObserver(id, observer);
  }
  public void Add(BaseSightRangeUC element) {
    root.EffectBaseSightRangeUCMutSetAdd(id, element.id);
  }
  public void Remove(BaseSightRangeUC element) {
    root.EffectBaseSightRangeUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectBaseSightRangeUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectBaseSightRangeUCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(BaseSightRangeUC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<BaseSightRangeUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetBaseSightRangeUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<BaseSightRangeUC>();
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
      if (!root.BaseSightRangeUCExists(element.id)) {
        violations.Add("Null constraint violated! BaseSightRangeUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.BaseSightRangeUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
