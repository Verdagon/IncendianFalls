using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class HealthPotionStrongMutSet {
  public readonly Root root;
  public readonly int id;
  public HealthPotionStrongMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public HealthPotionStrongMutSetIncarnation incarnation {
    get { return root.GetHealthPotionStrongMutSetIncarnation(id); }
  }
  public void AddObserver(EffectBroadcaster broadcaster, IHealthPotionStrongMutSetEffectObserver observer) {
    broadcaster.AddHealthPotionStrongMutSetObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IHealthPotionStrongMutSetEffectObserver observer) {
    broadcaster.RemoveHealthPotionStrongMutSetObserver(id, observer);
  }
  public void Add(HealthPotion element) {
      root.EffectHealthPotionStrongMutSetAdd(id, element.id);
  }
  public void Remove(HealthPotion element) {
      root.EffectHealthPotionStrongMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectHealthPotionStrongMutSetDelete(id);
  }
  public void Clear() {
    foreach (var element in new List<int>(incarnation.set)) {
      root.EffectHealthPotionStrongMutSetRemove(id, element);
    }
  }
  public bool Contains(HealthPotion element) {
      return incarnation.set.Contains(element.id);
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<HealthPotion> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetHealthPotion(element);
    }
  }
  public void Destruct() {
    var elements = new List<HealthPotion>();
    foreach (var element in this) {
      elements.Add(element);
    }
    this.Delete();
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.HealthPotionExists(element.id)) {
        violations.Add("Null constraint violated! HealthPotionStrongMutSet#" + id + "." + element.id);
      }
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var element in this) {
      if (root.HealthPotionExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
}
       
}
