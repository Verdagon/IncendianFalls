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
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectShieldingUCMutSetRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  //public int GetDeterministicHashCode() {
  //  return incarnation.GetDeterministicHashCode();
  //}
  public IEnumerator<ShieldingUC> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetShieldingUC(element);
    }
  }
}
         
}
