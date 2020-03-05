using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseCombatTimeUCMutSet {
  public readonly Root root;
  public readonly int id;
  public BaseCombatTimeUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BaseCombatTimeUCMutSetIncarnation incarnation {
    get { return root.GetBaseCombatTimeUCMutSetIncarnation(id); }
  }
  public void AddObserver(IBaseCombatTimeUCMutSetEffectObserver observer) {
    root.AddBaseCombatTimeUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IBaseCombatTimeUCMutSetEffectObserver observer) {
    root.RemoveBaseCombatTimeUCMutSetObserver(id, observer);
  }
  public void Add(BaseCombatTimeUC element) {
    root.EffectBaseCombatTimeUCMutSetAdd(id, element.id);
  }
  public void Remove(BaseCombatTimeUC element) {
    root.EffectBaseCombatTimeUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectBaseCombatTimeUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectBaseCombatTimeUCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(BaseCombatTimeUC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<BaseCombatTimeUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetBaseCombatTimeUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<BaseCombatTimeUC>();
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
      if (!root.BaseCombatTimeUCExists(element.id)) {
        violations.Add("Null constraint violated! BaseCombatTimeUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.BaseCombatTimeUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
