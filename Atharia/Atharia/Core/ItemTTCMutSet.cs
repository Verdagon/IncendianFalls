using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ItemTTCMutSet {
  public readonly Root root;
  public readonly int id;
  public ItemTTCMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ItemTTCMutSetIncarnation incarnation {
    get { return root.GetItemTTCMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IItemTTCMutSetEffectObserver observer) {
    broadcaster.AddItemTTCMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IItemTTCMutSetEffectObserver observer) {
    broadcaster.RemoveItemTTCMutSetObserver(id, observer);
  }
  public void Add(ItemTTC element) {
      root.EffectItemTTCMutSetAdd(id, element.id);
  }
  public void Remove(ItemTTC element) {
      root.EffectItemTTCMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectItemTTCMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.elements)) {
      root.EffectItemTTCMutSetRemove(id, element);
    }
  }
  public bool Contains(ItemTTC element) {
      return incarnation.elements.Contains(element.id);
  }
  public int Count { get { return incarnation.elements.Count; } }
  public IEnumerator<ItemTTC> GetEnumerator() {
    foreach (var element in incarnation.elements) {
      yield return root.GetItemTTC(element);
    }
  }
  public void Destruct() {
    var elements = new List<ItemTTC>();
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
      if (!root.ItemTTCExists(element.id)) {
        violations.Add("Null constraint violated! ItemTTCMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.ItemTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
