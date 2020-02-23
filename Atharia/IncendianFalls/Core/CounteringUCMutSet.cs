using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CounteringUCMutSet {
  public readonly Root root;
  public readonly int id;
  public CounteringUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public CounteringUCMutSetIncarnation incarnation {
    get { return root.GetCounteringUCMutSetIncarnation(id); }
  }
  public void AddObserver(ICounteringUCMutSetEffectObserver observer) {
    root.AddCounteringUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(ICounteringUCMutSetEffectObserver observer) {
    root.RemoveCounteringUCMutSetObserver(id, observer);
  }
  public void Add(CounteringUC element) {
    root.EffectCounteringUCMutSetAdd(id, element.id);
  }
  public void Remove(CounteringUC element) {
    root.EffectCounteringUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectCounteringUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectCounteringUCMutSetRemove(id, elementId);
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
    foreach (var element in elements) {
      element.Destruct();
    }
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.CounteringUCExists(element.id)) {
        violations.Add("Null constraint violated! CounteringUCMutSet#" + id + "." + element.id);
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
