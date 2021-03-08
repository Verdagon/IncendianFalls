using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DefyingUCWeakMutSet {
  public readonly Root root;
  public readonly int id;
  public DefyingUCWeakMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DefyingUCWeakMutSetIncarnation incarnation {
    get { return root.GetDefyingUCWeakMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IDefyingUCWeakMutSetEffectObserver observer) {
    broadcaster.AddDefyingUCWeakMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IDefyingUCWeakMutSetEffectObserver observer) {
    broadcaster.RemoveDefyingUCWeakMutSetObserver(id, observer);
  }
  public void Add(DefyingUC element) {
      root.EffectDefyingUCWeakMutSetAdd(id, element.id);
  }
  public void Remove(DefyingUC element) {
      root.EffectDefyingUCWeakMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectDefyingUCWeakMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectDefyingUCWeakMutSetRemove(id, element);
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
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.DefyingUCExists(element.id)) {
        violations.Add("Null constraint violated! DefyingUCWeakMutSet#" + id + "." + element.id);
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
