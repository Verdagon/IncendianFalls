using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ItemTTC {
  public readonly Root root;
  public readonly int id;
  public ItemTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ItemTTCIncarnation incarnation { get { return root.GetItemTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IItemTTCEffectObserver observer) {
    broadcaster.AddItemTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IItemTTCEffectObserver observer) {
    broadcaster.RemoveItemTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectItemTTCDelete(id);
  }
  public static ItemTTC Null = new ItemTTC(null, 0);
  public bool Exists() { return root != null && root.ItemTTCExists(id); }
  public bool NullableIs(ItemTTC that) {
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
      violations.Add("Null constraint violated! ItemTTC#" + id + ".item");
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
  public bool Is(ItemTTC that) {
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
