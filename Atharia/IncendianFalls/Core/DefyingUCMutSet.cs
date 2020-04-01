using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DefyingUCMutSet {
  public readonly Root root;
  public readonly int id;
  public DefyingUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DefyingUCMutSetIncarnation incarnation {
    get { return root.GetDefyingUCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IDefyingUCMutSetEffectObserver observer) {
    broadcaster.AddDefyingUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IDefyingUCMutSetEffectObserver observer) {
    broadcaster.RemoveDefyingUCMutSetObserver(id, observer);
  }
  public void Add(DefyingUC element) {
      root.EffectDefyingUCMutSetAdd(id, element.id);
  }
  public void Remove(DefyingUC element) {
      root.EffectDefyingUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectDefyingUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectDefyingUCMutSetRemove(id, element);
    }
  }
  public bool Contains(DefyingUC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<DefyingUC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetDefyingUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<DefyingUC>();
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
      if (!root.DefyingUCExists(element.id)) {
        violations.Add("Null constraint violated! DefyingUCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.DefyingUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
