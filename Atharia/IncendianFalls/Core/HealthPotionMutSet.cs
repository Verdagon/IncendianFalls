using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class HealthPotionMutSet {
  public readonly Root root;
  public readonly int id;
  public HealthPotionMutSet(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public HealthPotionMutSetIncarnation incarnation {
    get { return root.GetHealthPotionMutSetIncarnation(id); }
  }
  public void AddObserver(IHealthPotionMutSetEffectObserver observer) {
    root.AddHealthPotionMutSetObserver(id, observer);
  }
  public void RemoveObserver(IHealthPotionMutSetEffectObserver observer) {
    root.RemoveHealthPotionMutSetObserver(id, observer);
  }
  public void Add(HealthPotion element) {
    root.EffectHealthPotionMutSetAdd(id, element.id);
  }
  public void Remove(HealthPotion element) {
    root.EffectHealthPotionMutSetRemove(id, element.id);
  }
  public void Delete() {
    root.EffectHealthPotionMutSetDelete(id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectHealthPotionMutSetRemove(id, elementId);
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
    foreach (var element in elements) {
      element.Destruct();
    }
  }
  public void CheckForNullViolations(List<string> violations) {
    foreach (var element in this) {
      if (!root.HealthPotionExists(element.id)) {
        violations.Add("Null constraint violated! HealthPotionMutSet#" + id + "." + element.id);
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