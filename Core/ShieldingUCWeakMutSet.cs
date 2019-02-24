using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ShieldingUCWeakMutSet {
  public readonly Root root;
  public readonly int id;
  public ShieldingUCWeakMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ShieldingUCWeakMutSetIncarnation incarnation {
    get { return root.GetShieldingUCWeakMutSetIncarnation(id); }
  }
  public void AddObserver(IShieldingUCWeakMutSetEffectObserver observer) {
    root.AddShieldingUCWeakMutSetObserver(id, observer);
  }
  public void RemoveObserver(IShieldingUCWeakMutSetEffectObserver observer) {
    root.RemoveShieldingUCWeakMutSetObserver(id, observer);
  }
  public void Add(ShieldingUC element) {
    root.EffectShieldingUCWeakMutSetAdd(id, element.id);
  }
  public void Remove(ShieldingUC element) {
    root.EffectShieldingUCWeakMutSetRemove(id, element.id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectShieldingUCWeakMutSetRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<ShieldingUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetShieldingUC(element);
    }
  }
  public void Destruct() {
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.ShieldingUCExists(element.id)) {
        violations.Add("Null constraint violated! ShieldingUCWeakMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.ShieldingUCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
