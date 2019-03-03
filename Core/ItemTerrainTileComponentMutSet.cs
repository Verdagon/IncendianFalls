using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ItemTerrainTileComponentMutSet {
  public readonly Root root;
  public readonly int id;
  public ItemTerrainTileComponentMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ItemTerrainTileComponentMutSetIncarnation incarnation {
    get { return root.GetItemTerrainTileComponentMutSetIncarnation(id); }
  }
  public void AddObserver(IItemTerrainTileComponentMutSetEffectObserver observer) {
    root.AddItemTerrainTileComponentMutSetObserver(id, observer);
  }
  public void RemoveObserver(IItemTerrainTileComponentMutSetEffectObserver observer) {
    root.RemoveItemTerrainTileComponentMutSetObserver(id, observer);
  }
  public void Add(ItemTerrainTileComponent element) {
    root.EffectItemTerrainTileComponentMutSetAdd(id, element.id);
  }
  public void Remove(ItemTerrainTileComponent element) {
    root.EffectItemTerrainTileComponentMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectItemTerrainTileComponentMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectItemTerrainTileComponentMutSetRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<ItemTerrainTileComponent> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetItemTerrainTileComponent(element);
    }
  }
  public void Destruct() {
    var elements = new List<ItemTerrainTileComponent>();
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
      if (!root.ItemTerrainTileComponentExists(element.id)) {
        violations.Add("Null constraint violated! ItemTerrainTileComponentMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.ItemTerrainTileComponentExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
