using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TimeScriptDirectiveUCMutSet {
  public readonly Root root;
  public readonly int id;
  public TimeScriptDirectiveUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public TimeScriptDirectiveUCMutSetIncarnation incarnation {
    get { return root.GetTimeScriptDirectiveUCMutSetIncarnation(id); }
  }
  public void AddObserver(ITimeScriptDirectiveUCMutSetEffectObserver observer) {
    root.AddTimeScriptDirectiveUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(ITimeScriptDirectiveUCMutSetEffectObserver observer) {
    root.RemoveTimeScriptDirectiveUCMutSetObserver(id, observer);
  }
  public void Add(TimeScriptDirectiveUC element) {
    root.EffectTimeScriptDirectiveUCMutSetAdd(id, element.id);
  }
  public void Remove(TimeScriptDirectiveUC element) {
    root.EffectTimeScriptDirectiveUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectTimeScriptDirectiveUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectTimeScriptDirectiveUCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(TimeScriptDirectiveUC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<TimeScriptDirectiveUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetTimeScriptDirectiveUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<TimeScriptDirectiveUC>();
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
      if (!root.TimeScriptDirectiveUCExists(element.id)) {
        violations.Add("Null constraint violated! TimeScriptDirectiveUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.TimeScriptDirectiveUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
