using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ItemTerrainTileComponent {
  public readonly Root root;
  public readonly int id;
  public ItemTerrainTileComponent(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ItemTerrainTileComponentIncarnation incarnation { get { return root.GetItemTerrainTileComponentIncarnation(id); } }
  public void AddObserver(IItemTerrainTileComponentEffectObserver observer) {
    root.AddItemTerrainTileComponentObserver(id, observer);
  }
  public void RemoveObserver(IItemTerrainTileComponentEffectObserver observer) {
    root.RemoveItemTerrainTileComponentObserver(id, observer);
  }
  public void Delete() {
    root.EffectItemTerrainTileComponentDelete(id);
  }
  public static ItemTerrainTileComponent Null = new ItemTerrainTileComponent(null, 0);
  public bool Exists() { return root != null && root.ItemTerrainTileComponentExists(id); }
  public bool NullableIs(ItemTerrainTileComponent that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {

    if (!root.IItemExists(item.id)) {
      violations.Add("Null constraint violated! ItemTerrainTileComponent#" + id + ".item");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.IItemExists(item.id)) {
      item.FindReachableObjects(foundIds);
    }
  }
  public bool Is(ItemTerrainTileComponent that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public IItem item {
    get { return root.GetIItem(incarnation.item); }
  }
}
}
