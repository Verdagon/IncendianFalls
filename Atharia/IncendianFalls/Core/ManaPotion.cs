using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ManaPotion {
  public readonly Root root;
  public readonly int id;
  public ManaPotion(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ManaPotionIncarnation incarnation { get { return root.GetManaPotionIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IManaPotionEffectObserver observer) {
    broadcaster.AddManaPotionObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IManaPotionEffectObserver observer) {
    broadcaster.RemoveManaPotionObserver(id, observer);
  }
  public void Delete() {
    root.EffectManaPotionDelete(id);
  }
  public static ManaPotion Null = new ManaPotion(null, 0);
  public bool Exists() { return root != null && root.ManaPotionExists(id); }
  public bool NullableIs(ManaPotion that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
  }
  public bool Is(ManaPotion that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
       }
}
