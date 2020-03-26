using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class HealthPotion {
  public readonly Root root;
  public readonly int id;
  public HealthPotion(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public HealthPotionIncarnation incarnation { get { return root.GetHealthPotionIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IHealthPotionEffectObserver observer) {
    broadcaster.AddHealthPotionObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IHealthPotionEffectObserver observer) {
    broadcaster.RemoveHealthPotionObserver(id, observer);
  }
  public void Delete() {
    root.EffectHealthPotionDelete(id);
  }
  public static HealthPotion Null = new HealthPotion(null, 0);
  public bool Exists() { return root != null && root.HealthPotionExists(id); }
  public bool NullableIs(HealthPotion that) {
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
  public bool Is(HealthPotion that) {
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
