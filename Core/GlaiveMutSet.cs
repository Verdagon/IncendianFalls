using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GlaiveMutSet {
  public readonly Root root;
  public readonly int id;
  public GlaiveMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public GlaiveMutSetIncarnation incarnation {
    get { return root.GetGlaiveMutSetIncarnation(id); }
  }
  public void AddObserver(IGlaiveMutSetEffectObserver observer) {
    root.AddGlaiveMutSetObserver(id, observer);
  }
  public void RemoveObserver(IGlaiveMutSetEffectObserver observer) {
    root.RemoveGlaiveMutSetObserver(id, observer);
  }
  public void Add(Glaive element) {
    root.EffectGlaiveMutSetAdd(id, element.id);
  }
  public void Remove(Glaive element) {
    root.EffectGlaiveMutSetRemove(id, element.id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectGlaiveMutSetRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  //public int GetDeterministicHashCode() {
  //  return incarnation.GetDeterministicHashCode();
  //}
  public IEnumerator<Glaive> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetGlaive(element);
    }
  }
}
         
}
