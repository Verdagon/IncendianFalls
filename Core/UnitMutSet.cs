using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnitMutSet {
  public readonly Root root;
  public readonly int id;
  public UnitMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public UnitMutSetIncarnation incarnation {
    get { return root.GetUnitMutSetIncarnation(id); }
  }
  public void AddObserver(IUnitMutSetEffectObserver observer) {
    root.AddUnitMutSetObserver(id, observer);
  }
  public void RemoveObserver(IUnitMutSetEffectObserver observer) {
    root.RemoveUnitMutSetObserver(id, observer);
  }
  public void Add(Unit element) {
    root.EffectUnitMutSetAdd(id, element.id);
  }
  public void Remove(Unit element) {
    root.EffectUnitMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectUnitMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectUnitMutSetRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<Unit> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetUnit(element);
    }
  }
  public void Destruct() {
    var elements = new List<Unit>();
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
      if (!root.UnitExists(element.id)) {
        violations.Add("Null constraint violated! UnitMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.UnitExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
