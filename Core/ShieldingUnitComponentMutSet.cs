using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class ShieldingUnitComponentMutSet {
  public readonly Root root;
  public readonly int id;
  public ShieldingUnitComponentMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ShieldingUnitComponentMutSetIncarnation incarnation {
    get { return root.GetShieldingUnitComponentMutSetIncarnation(id); }
  }
  public void AddObserver(IShieldingUnitComponentMutSetEffectObserver observer) {
    root.AddShieldingUnitComponentMutSetObserver(id, observer);
  }
  public void RemoveObserver(IShieldingUnitComponentMutSetEffectObserver observer) {
    root.RemoveShieldingUnitComponentMutSetObserver(id, observer);
  }
  public void Add(ShieldingUnitComponent element) {
    root.EffectShieldingUnitComponentMutSetAdd(id, element.id);
  }
  public void Remove(ShieldingUnitComponent element) {
    root.EffectShieldingUnitComponentMutSetRemove(id, element.id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectShieldingUnitComponentMutSetRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  //public int GetDeterministicHashCode() {
  //  return incarnation.GetDeterministicHashCode();
  //}
  public IEnumerator<ShieldingUnitComponent> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetShieldingUnitComponent(element);
    }
  }
}
         
}
