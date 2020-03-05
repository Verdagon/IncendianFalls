using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SorcerousUCMutSet {
  public readonly Root root;
  public readonly int id;
  public SorcerousUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public SorcerousUCMutSetIncarnation incarnation {
    get { return root.GetSorcerousUCMutSetIncarnation(id); }
  }
  public void AddObserver(ISorcerousUCMutSetEffectObserver observer) {
    root.AddSorcerousUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(ISorcerousUCMutSetEffectObserver observer) {
    root.RemoveSorcerousUCMutSetObserver(id, observer);
  }
  public void Add(SorcerousUC element) {
    root.EffectSorcerousUCMutSetAdd(id, element.id);
  }
  public void Remove(SorcerousUC element) {
    root.EffectSorcerousUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectSorcerousUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectSorcerousUCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(SorcerousUC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<SorcerousUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetSorcerousUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<SorcerousUC>();
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
      if (!root.SorcerousUCExists(element.id)) {
        violations.Add("Null constraint violated! SorcerousUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.SorcerousUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
