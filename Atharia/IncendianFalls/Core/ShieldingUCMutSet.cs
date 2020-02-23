using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ShieldingUCMutSet {
  public readonly Root root;
  public readonly int id;
  public ShieldingUCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ShieldingUCMutSetIncarnation incarnation {
    get { return root.GetShieldingUCMutSetIncarnation(id); }
  }
  public void AddObserver(IShieldingUCMutSetEffectObserver observer) {
    root.AddShieldingUCMutSetObserver(id, observer);
  }
  public void RemoveObserver(IShieldingUCMutSetEffectObserver observer) {
    root.RemoveShieldingUCMutSetObserver(id, observer);
  }
  public void Add(ShieldingUC element) {
    root.EffectShieldingUCMutSetAdd(id, element.id);
  }
  public void Remove(ShieldingUC element) {
    root.EffectShieldingUCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectShieldingUCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectShieldingUCMutSetRemove(id, elementId);
    }
  }
  public bool Contains(ShieldingUC element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<ShieldingUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetShieldingUC(element);
    }
  }
  public void Destruct() {
    var elements = new List<ShieldingUC>();
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
      if (!root.ShieldingUCExists(element.id)) {
        violations.Add("Null constraint violated! ShieldingUCMutSet#" + id + "." + element.id);
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
