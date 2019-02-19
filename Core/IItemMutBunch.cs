using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class IItemMutBunch {// : IEnumerable<IItem> {
  public readonly Root root;
  public readonly int id;

  public IItemMutBunch(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public IItemMutBunchIncarnation incarnation {
    get { return root.GetIItemMutBunchIncarnation(id); }
  }
  public void AddObserver(IIItemMutBunchEffectObserver observer) {
    root.AddIItemMutBunchObserver(id, observer);
  }
  public void RemoveObserver(IIItemMutBunchEffectObserver observer) {
    root.RemoveIItemMutBunchObserver(id, observer);
  }
  public void Add(IItem element) {
    root.EffectIItemMutBunchAdd(id, element.id);
  }
  public void Remove(IItem element) {
    root.EffectIItemMutBunchRemove(id, element.id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectIItemMutBunchRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  public int GetDeterministicHashCode() {
    return incarnation.GetDeterministicHashCode();
  }
  public IEnumerator<IItem> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetIItem(element);
    }
  }
}
         
}
