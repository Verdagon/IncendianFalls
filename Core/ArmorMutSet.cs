using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ArmorMutSet {
  public readonly Root root;
  public readonly int id;
  public ArmorMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ArmorMutSetIncarnation incarnation {
    get { return root.GetArmorMutSetIncarnation(id); }
  }
  public void AddObserver(IArmorMutSetEffectObserver observer) {
    root.AddArmorMutSetObserver(id, observer);
  }
  public void RemoveObserver(IArmorMutSetEffectObserver observer) {
    root.RemoveArmorMutSetObserver(id, observer);
  }
  public void Add(Armor element) {
    root.EffectArmorMutSetAdd(id, element.id);
  }
  public void Remove(Armor element) {
    root.EffectArmorMutSetRemove(id, element.id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectArmorMutSetRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  //public int GetDeterministicHashCode() {
  //  return incarnation.GetDeterministicHashCode();
  //}
  public IEnumerator<Armor> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetArmor(element);
    }
  }
}
         
}
